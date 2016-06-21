using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            root = 0;
            leftOffset = new Size(-Row_size, Column_size);
            rightOffset = new Size(Row_size, Column_size);

            BUF = new Bitmap(this.Width, this.Height);
            buffer = Graphics.FromImage(BUF);

            fa = x => nodeLists[x].fa;
            geter = x => nodeLists[fa(x)].l == x ? 0 : 1;
            val = x => nodeLists[x].val;

            Info.Visible = false;
        }
        const int Node_size = 20;
        const int Row_size = 50; //行
        const int Column_size = 50; //列
        List<Node> nodeLists = new List<Node>();
        List<Point> nodeCenter = new List<Point>();
        Point StartPoint = new Point(500, 100);
        List<bool> nodeExist = new List<bool>();
        int nodeSeleced;
        Size leftOffset, rightOffset;

        Bitmap BUF;
        Graphics buffer;

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            nodeSeleced = GetNodeid(e.Location);
            DrawAll();
        }
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
            Font font = new Font("微软雅黑", 11);
            Brush brush = new SolidBrush(Color.Black);
            Size wordSize = buffer.MeasureString(v.val.ToString(),font).ToSize();
            Point startPoint = new Point(locate.X - wordSize.Width / 2
                                        ,locate.Y - wordSize.Height / 2);
            buffer.DrawString(v.val.ToString(), font, brush, startPoint);
        }
        private void DrawLine(Point start, Point end)
        {
            buffer.DrawLine(new Pen(Color.Black), start, end);
        }
        private void DrawAll()
        {
            buffer.Clear(Color.White);
            for (int i = 0; i < nodeLists.Count; i++)
            {
                if (nodeExist[i] == false) continue;
                for (int k = 0; k < 2; k++)
                {
                    if (nodeLists[i].son[k] != -1)
                    {
                        DrawLine(nodeCenter[i], nodeCenter[nodeLists[i].son[k]]);
                    }
                }
            }
            for (int i = 0; i < nodeLists.Count; i++)
            {
                if (nodeExist[i] == false) continue;
                DrawNode(nodeCenter[i], nodeLists[i],Color.Black);
            }
            if (nodeSeleced != -1) 
                DrawNode(nodeCenter[nodeSeleced], nodeLists[nodeSeleced], Color.Red);
            Graphics formGraphics = this.CreateGraphics();
            formGraphics.DrawImage(BUF,new Point(0,0));
        }
        List<int> nodeOrder;
        private void GetSubOrder(int x)
        {
            if (nodeLists[x].l != -1) GetSubOrder(nodeLists[x].l);
            nodeOrder.Add(x);
            if (nodeLists[x].r != -1) GetSubOrder(nodeLists[x].r);
        }
        private int getID(int x)
        {
            for (int i = 0; i < nodeOrder.Count; i++)
            {
                if (nodeOrder[i] == x)
                    return i;
            }
            return -1;
        }
        private void ChangeNodeLocate(int st)
        {
            int l = nodeLists[st].l,r = nodeLists[st].r;
            int offset;
            if (l != -1)
            {
                offset = getID(st) - getID(l);
                nodeCenter[l] = nodeCenter[st] + new Size(-Row_size * offset, Column_size);
                ChangeNodeLocate(l);
            }
            if (r != -1)
            {
                offset = getID(r) - getID(st);
                nodeCenter[r] = nodeCenter[st] + new Size(Row_size * offset, Column_size);
                ChangeNodeLocate(r);
            }
        }
        private void ChangeSubLocate(int st)
        {
            nodeOrder = new List<int>();
            GetSubOrder(st);
            ChangeNodeLocate(st);   
        }
        int root;

        del geter;
        private void rotate(int x)
        {
            int st = fa(x);
            int parent = fa(st);

            if (parent != -1)
                nodeLists[parent].son[geter(st)] = x;

            int d = geter(x);

            int dson = nodeLists[x].son[d ^ 1];
            if(dson!=-1)
                nodeLists[dson].fa = st;

            nodeLists[st].son[d] = dson;
            nodeLists[x].son[d^1] = st;

            nodeLists[x].fa = parent;
            nodeLists[st].fa = x;

        }
        public void Insert(int x)
        {
            nodeLists.Add(new Node(x));
            nodeExist.Add(true);
            nodeCenter.Add(new Point(0, 0));
            int nodeid = nodeLists.Count - 1;
            insert(root,nodeid);
            ChangeSubLocate(root);
            DrawAll();
          //  Splay(nodeid);
        }
        del val;
        private void insert(int st,int locate)
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

        private void Start_Click(object sender, EventArgs e)
        {
            Insert(5);
            Insert(4);
            Insert(2);
            Insert(1);
            Insert(3);
        }

        private void sp_Click(object sender, EventArgs e)
        {
            if (nodeSeleced == -1) return;
            Splay(nodeSeleced);
            Info.Text = "";
            foreach(var x in nodeLists){
                Info.Text += x.toString() + "\n";
            }
            Info.Text += "ROOT = " + root.ToString();
            nodeCenter[root] = StartPoint;
            ChangeSubLocate(root);
            DrawAll();
        }
    }
}
