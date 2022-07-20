using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace War
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {//играть
            panel1.Visible = false;
            panel2.Visible = true;
        }
        private void button8_Click(object sender, EventArgs e)
        {//
            panel1.Visible = true;
            panel2.Visible = false;
        }
        private void button2_Click(object sender, EventArgs e)
        {//автор (я)
            panel1.Visible = false;
            panel5.Visible = true;
        }
        private void button3_Click(object sender, EventArgs e)
        {//чет еще

        }
        private void button4_Click(object sender, EventArgs e)
        {//выход
            Application.Exit();
        }
        private void button5_Click(object sender, EventArgs e)
        {//продолжить
            panel2.Visible = false;
            panel3.Visible = true;
        }
        private void button9_Click(object sender, EventArgs e)
        {//назад2
            panel2.Visible = true;
            panel3.Visible = false;
        }
        private void button7_Click(object sender, EventArgs e)
        {//новая игра
            panel2.Visible = false;
            panel4.Visible = true;
        }
        private void button13_Click(object sender, EventArgs e)
        {//назад3
            panel2.Visible = true;
            panel4.Visible = false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //this.Size = new Size(MinimumSize.Width, MinimumSize.Height);
            int midx =(this.Width-panel1.Width)/2;
            int midy = (this.Height) / 2;
            panel1.Location = new Point(midx, midy);
            panel2.Location = new Point(midx, midy);
            panel3.Location = new Point(midx, midy);
            panel4.Location = new Point(midx, midy);
            panel5.Location = new Point(midx, midy);
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            listBox1.Items.Clear();
            listBox1.Items.Add("map1.warsave");
        }
        string path;
        //кнопки под начало игры:
        private void button6_Click(object sender, EventArgs e)
        {//продолжить игру
            string pth = Directory.GetCurrentDirectory();
            path = pth +"\\"+ listBox1.Items[0].ToString();//listBox1.SelectedItem.ToString();
            Game g = new Game();
            g.m =new Map(path);
            g.ShowDialog();
            this.Hide();
        }

        private void button12_Click(object sender, EventArgs e)
        {//начать новую игру
            Game g = new Game();
            g.m = new Map(path);
            g.ShowDialog();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {//назад
            panel1.Visible = true;
            panel5.Visible = false;
        }
    }
}
