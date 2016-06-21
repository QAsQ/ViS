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
            nodeCenter.Add(new Point(100, 100));
            nodeExist.Add(true);
            root = 0;
            leftOffset = new Size(-Row_size, Column_size);
            rightOffset = new Size(Row_size, Column_size);

            BUF = new Bitmap(this.Width, this.Height);
            buffer = Graphics.FromImage(BUF);

            fa = x => nodeLists[x].fa;
            geter = x => nodeLists[fa(x)].l == x ? 0 : 1;
            val = x => nodeLists[x].val;
        }
        const int Node_size = 20;
        const int Row_size = 30; //行
        const int Column_size = 30; //列
        List<Node> nodeLists = new List<Node>();
        List<Point> nodeCenter = new List<Point>();
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

        private void ChangeSubLocate(int id)
        {
            int l = nodeLists[id].l, r = nodeLists[id].r;
            if (l != -1)
            {
                nodeCenter[l] = nodeCenter[id] + leftOffset;
                ChangeSubLocate(l);
            }
            if (r != -1)
            {
                nodeCenter[r] = nodeCenter[id] + rightOffset;
                ChangeSubLocate(r);
            }
        }
        int root;

        del geter;
        private void rotate(int x)
        {
            int st = fa(x);
            int parent = fa(st);

            int d = geter(x);

            int dson = nodeLists[x].son[d ^ 1];
            if(dson!=-1)
                nodeLists[dson].fa = st;

            nodeLists[st].son[d] = dson;
            nodeLists[x].son[d^1] = st;

            nodeLists[x].fa = parent;
            nodeLists[st].fa = x;

            if (parent != -1)
               nodeLists[parent].son[geter(st)] = x;
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
            for (int i = 1; i < 10; i++)
            {
                Insert((i+5)%3);
            }
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
            ChangeSubLocate(root);
            DrawAll();
        }
    }
}
