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
        }
        const int Node_size = 10;
        const int Row_size = 10; //行
        const int Column_size = 10; //列
        List<Node> nodeLists = new List<Node>();
        List<Point> nodeCenter = new List<Point>();
        List<bool> nodeExist = new List<bool>();
        int nodeSeleced;
        Size leftOffset, rightOffset;
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
        private bool inNode(Point center, Point locate)
        {
            center -= (Size)locate;
            del squ = x => x*x;
            return squ(Node_size) <= squ(center.X) + squ(center.Y);
        }
        private void DrawNode(Point locate, Node v)
        {
            // todo
        }
        private void DrawLine(Point start, Point end)
        {
            // todo
        }
        private void DrawAll()
        {
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
                DrawNode(nodeCenter[i], nodeLists[i]);
            }
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
        public void Insert(int x)
        {
            //todo 
        }
        public void Delete(int id)
        {
            //todo
        }
        public void Splay(int id)
        {
            //todo
        }
    }
}
