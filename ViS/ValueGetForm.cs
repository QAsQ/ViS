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
    public partial class ValueGetForm : Form
    {
        public ValueGetForm()
        {
            InitializeComponent();
            ValueTextBox.Text = "42";
        }
        private int val;
        public int valer
        {
            get
            {
                return val;
            }
        }
        private void ok_Click(object sender, EventArgs e)
        {
            bool viser = false;
            try
            {
                val = Convert.ToInt32(ValueTextBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("输入的数字不合法");
                viser = false;
            }
            this.Visible = viser;
        }
    }
}
