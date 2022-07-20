using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace War_MapMaker
{
    public partial class Attention : Form
    {
        public Attention()
        {
            InitializeComponent();
        }
        public string txt;
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Attention_Load(object sender, EventArgs e)
        {
            label1.Text = txt;
        }
    }
}
