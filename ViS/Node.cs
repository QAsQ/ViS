using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViS
{
    class Node
    {
        public int[] son = new int[2];
        int val, fa;
        public Node(int V)
        {
            son[0] = son[1] = -1;
            fa = -1;
            val = V;
        }
        public int l
        {
            set
            {
                son[0] = value;
            }
            get
            {
                return son[0];
            }
        }
        public int r
        {
            set
            {
                son[1] = value;
            }
            get
            {
                return son[1];
            }
        }
    }
}
