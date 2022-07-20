using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarLib;

namespace tryWarLib
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        void mainCycle()
        {
            foreach (Unit unit in Game.objects)
                unit.Do();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //mainCycle();
        }
    }
}
