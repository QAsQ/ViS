using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace ViS
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            nodeSeleced = -1;
            nodeLists.Add(new Node(0));
            nodeCenter.Add(StartPoint);
            nodeExist.Add(true);
            nodeOrder.Add(0);
            root = 0;

            BUF = new Bitmap(this.Width, this.Height);
            buffer = Graphics.FromImage(BUF);
            formGraphics =  this.CreateGraphics();

            fa = x => nodeLists[x].fa;
            geter = x => nodeLists[fa(x)].l == x ? 0 : 1;
            val = x => nodeLists[x].val;

            SpaceIsDown = false;
            MouseLeftIsDown = false;

            Info.Visible = false;
        }

        const int Node_size = 20;
        const int Row_size = 50; /// 水平偏移量
        const int Column_size = 50; ///垂直偏移量
                                    ///
        List<Node> nodeLists = new List<Node>();
        List<Point> nodeCenter = new List<Point>();
        Point StartPoint = new Point(500, 100);
        List<bool> nodeExist = new List<bool>();
        List<int> nodeOrder = new List<int>();

        int nodeSeleced;

        Bitmap BUF;
        Graphics buffer;
        Graphics formGraphics;

        private int GetNodeid(Point locate)
        {
            for (int i = 0; i < nodeCenter.Count(); i++)
            {
                if (nodeExist[i] == false) continue;
                if (inNode(nodeCenter[i], locate)) return i;
            }
            return -1;
        }

        delegate int del(int x);
        del fa;
        del geter;

        private bool inNode(Point center, Point locate)
        {
            center -= (Size)locate;
            del squ = x => x*x;
            return squ(Node_size) >= squ(center.X) + squ(center.Y);
        }

        private void DrawNode(Point locate, Node v,Color color)
        {
            Rectangle rect = new Rectangle(locate - new Size(Node_size, Node_size)
                                           ,new Size(Node_size * 2, Node_size * 2));
            buffer.DrawEllipse(new Pen(color), rect);
            Font font = new Font("微软雅黑",18);
            Brush brush = new SolidBrush(color);
            Size wordSize = buffer.MeasureString(v.val.ToString(),font).ToSize();
            Point startPoint = new Point(locate.X - wordSize.Width / 2
                                        ,locate.Y - wordSize.Height / 2);
            buffer.DrawString(v.val.ToString(), font, brush, startPoint);
        }

        private void DrawLine(Point start, Point end,Color color)
        {
            Geom.scaleLine(ref start,ref end, Node_size);
            buffer.DrawLine(new Pen(color), start, end);
        }

        private void DrawAll()
        {
            DrawAll(new List<int>(), new List<int>(), new List<Point>(), new List<Point>());
        }

        bool inList(int st, int ed,List<int> s, List<int> e)
        {
            for (int i = 0; i < s.Count; i++)
            {
                if (s[i] == st && e[i] == ed)
                    return true;
            }
            return false;
        }
        Color initNode_color =  Color.Green;
        Color selectNode_color = Color.Blue;
        Color initLine_color = Color.Gray;
        Color MoveLine_color =   Color.Black;
        private void DrawAll(List<int> st, List<int> ed, List<Point> ns, List<Point> ne)
        {
            buffer.Clear(Color.White);
            for (int i = 0; i < nodeLists.Count; i++)
            {
                if (nodeExist[i] == false) continue;
                for (int k = 0; k < 2; k++)
                {
                    if (nodeLists[i].son[k] != -1 && inList(i, nodeLists[i].son[k], st, ed) == false)
                    {
                        DrawLine(nodeCenter[i], nodeCenter[nodeLists[i].son[k]], initLine_color);
                    }
                }
            }
            for (int i = 0; i < nodeLists.Count; i++)
            {
                if (nodeExist[i] == false) continue;
                DrawNode(nodeCenter[i], nodeLists[i], initNode_color);
            }
            if (nodeSeleced != -1)
                DrawNode(nodeCenter[nodeSeleced], nodeLists[nodeSeleced], selectNode_color);
            for (int i = 0; i < ns.Count; i++)
            {
                DrawLine(ns[i], ne[i], MoveLine_color);
            }
            formGraphics.DrawImage(BUF, new Point(0, 0));
        }


        private void ChangeSubLocate(int st)
        {
            int l = nodeLists[st].l,r = nodeLists[st].r;
            int offset;
            if (l != -1)
            {
                offset = nodeOrder[st] - nodeOrder[l];
                nodeCenter[l] = nodeCenter[st] + new Size(-Row_size * offset, Column_size);
                ChangeSubLocate(l);
            }
            if (r != -1)
            {
                offset = nodeOrder[r] - nodeOrder[st];
                nodeCenter[r] = nodeCenter[st] + new Size(Row_size * offset, Column_size);
                ChangeSubLocate(r);
            }
        }


        int root;
        const int Tran = 20; 
        const int Mov = 20;

        private void rotate(int x)
        {
            int st = fa(x);
            int parent = fa(st);
            int d = geter(x);
            int dson = nodeLists[x].son[d ^ 1];

            Point pris = nodeCenter[x];
            for (int i = 1; i <= Tran; i++)
            {
                nodeCenter[x] = pris + new Size(0, -Column_size * i / Tran);
                ChangeSubLocate(x);
                DrawAll();
            }

            List<int> from, to;
            from = new List<int>(); 
            to = new List<int>();

            from.Add(parent);to.Add(st);
            from.Add(x);to.Add(dson);

            int offset = (nodeOrder[st] - nodeOrder[x] ) * Row_size / Mov;

            for (int i = 1; i <= Mov; i++)
            {
                List<Point> ns = new List<Point>();
                List<Point> ne = new List<Point>();
                if (parent != -1)
                {
                    ns.Add(nodeCenter[parent]);
                    ne.Add(nodeCenter[st] - new Size(offset * i, 0));
                }
                if (dson != -1)
                {
                    ns.Add(nodeCenter[dson]);
                    ne.Add(nodeCenter[x] + new Size(offset * i, 0));
                }
                DrawAll(from,to,ns,ne);
            }

            //---rotate_Start---
            if (parent != -1) nodeLists[parent].son[geter(st)] = x; 

            if (dson != -1) nodeLists[dson].fa = st;

            nodeLists[st].son[d] = dson;
            nodeLists[x].son[d^1] = st;

            nodeLists[x].fa = parent;
            nodeLists[st].fa = x;
            //----rotate_End---


            pris = nodeCenter[st];
            for (int i = 0; i <= Tran; i++)
            {
                nodeCenter[st] = pris + new Size(0, Column_size * i/ Tran);
                ChangeSubLocate(st);
                DrawAll();
            }
        }

        public void Insert(int x)
        {
            nodeLists.Add(new Node(x));
            nodeExist.Add(true);
            nodeCenter.Add(new Point(0, 0));
            nodeOrder.Add(0);
            int nodeid = nodeLists.Count - 1;
            insert(root,nodeid);
            ChangeNodeOrder(root);
            ChangeSubLocate(root);
           // Splay(nodeid);
            DrawAll();
        }

        List<int> tempNodeOrder;

        private void getTempOrder(int st, ref List<int> list)
        {
            int l = nodeLists[st].l, r = nodeLists[st].r;
            if (l != -1) getTempOrder(l, ref list);
            list.Add(st);
            if (r != -1) getTempOrder(r, ref list);
        }

        private void ChangeNodeOrder(int root)
        {
            tempNodeOrder = new List<int>();
            getTempOrder(root, ref tempNodeOrder);
            for (int i = 0; i < tempNodeOrder.Count; i++)
                nodeOrder[tempNodeOrder[i]] = i + 1;
        }

        del val;
        private void insert(int st, int locate)
        {
            int d = val(st) < val(locate) ? 1 : 0;
            if (nodeLists[st].son[d] != -1) insert(nodeLists[st].son[d], locate);
            else
            {
                nodeLists[st].son[d] = locate;
                nodeLists[locate].fa = st;
                return;
            }
        }
        public void Delete(int st)
        {
            //todo
        }
        public void Splay(int st)
        {
            while (fa(st) != -1 && fa(fa(st)) != -1)
            {
                int p = fa(st);
                if (geter(st) == geter(p)) rotate(p);
                else rotate(st);
                rotate(st);
            }
            if (fa(st) != -1) rotate(st);
            root = st;
        }

        bool SpaceIsDown;
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                SpaceIsDown = true;
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                SpaceIsDown = false;
        }

        bool MouseLeftIsDown;
        Point MouseBeforeLocate;
        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                nodeSeleced = GetNodeid(e.Location);
                if (nodeSeleced == -1)
                {
                    if (e.Button == System.Windows.Forms.MouseButtons.Left && SpaceIsDown)
                    {
                        MouseLeftIsDown = true;
                        MouseBeforeLocate = e.Location;
                    }
                }
                DrawAll();
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                context.Items[0].Visible = nodeSeleced != -1;
                context.Show(e.Location + (Size)this.Location + new Size(9,30));
            }
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseLeftIsDown && SpaceIsDown)
            {
                Size offset = (Size)e.Location - (Size)MouseBeforeLocate;
                MouseBeforeLocate = e.Location;
                for (int i = 0; i < nodeCenter.Count; i++)
                {
                    nodeCenter[i] += offset;
                }
            StartPoint += offset;
            }
            DrawAll();
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == System.Windows.Forms.MouseButtons.Left)
                MouseLeftIsDown = false;
        }

        private void revert_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < nodeLists.Count; i++)
            {
                if (nodeExist[i] == false) continue;
                Splay(i);
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            BUF = new Bitmap(this.Width, this.Height);
            buffer = Graphics.FromImage(BUF);
            formGraphics = this.CreateGraphics();
        }

        private void splayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Splay(nodeSeleced);
        }

        private void buildToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < 15; i++)
            {
                Insert(i);
            }
        }
        private void insertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ValueGetForm valueGeter = new ValueGetForm();
            valueGeter.ShowDialog();
            Insert(valueGeter.valer);
        }
        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Creater QAsQ\nLook at https://github.com/QAsQ/ViS");
        }
    }
}
