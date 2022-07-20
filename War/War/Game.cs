using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace War
{
    //основная форма, где и будет происходить сама игра.
    public partial class Game : Form
    {
        //передаем стартовые данные:
        //bool NewGame;//true -> 
        //false ->
        List<int> Units;//индексы юнитов, пренадлежащие игроку
        List<int> SelectedUnits;//индексы выделенных юнитов
        public Map m;
        Bitmap bit;
        Graphics g;
        int k, msX, msY, msX2, msY2;
        bool msDown;
        int magicNum;
        public Game()
        {
            InitializeComponent();
        }
        int min(int a, int b) { return a < b ? a : b; }
        int max(int a, int b) { return a > b ? a : b; }
        private void Game_Load(object sender, EventArgs e)
        {
            //карта уже загружена и считана;
            //осталось её отобразить на двух пикчербоксах: миникарта? и игровое поле
            //(пока что - просто отобразить)
            bit =new Bitmap(pictureBox1.Width,pictureBox1.Height);
            g = Graphics.FromImage(bit);
            g.Clear(Color.White);
            Map.ShowMap(g, pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bit;
            Units = new List<int>();
            SelectedUnits = new List<int>();
            magicNum = Map.magicNum;
            k = pictureBox1.Width > pictureBox1.Height ? pictureBox1.Height : pictureBox1.Width;
            k /= magicNum;
            timer1.Interval = 50;
            timer1.Enabled = true;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            int x = Cursor.Position.X - pictureBox1.Location.X;
            int y = Cursor.Position.Y - pictureBox1.Location.Y;
            msX2 = msX;
            msY2 = msY;
            x /= k;
            y /= k;
            x = mod(x+Map.x, Map.w);
            y = mod(y+Map.y, Map.h);
            if (e.Button == MouseButtons.Left)
            {
                int a=-1;
                SelectedUnits.Clear();
                panel2.Visible = false;
                if (Map.all[x, y] == Cell.Unit)
                {
                    a = Map.FindUnit(x, y);
                    if (a != -1)
                        if (Map.AllUnits[a].Color == Map.userColor)
                        {
                            SelectedUnits.Add(a);
                            panel2.Visible = true;
                        }
                }
                msDown = true;
            }
            else if(e.Button==MouseButtons.Right)
            {
                //if(SelectedUnits.Count>0)
                {
                    //Map.AllUnits[SelectedUnits[0]].act = ToDo.GoToPoint;
                    //Map.AllUnits[SelectedUnits[0]].Go(x, y);
                    for (int i = 0; i < SelectedUnits.Count; i++)
                        Map.AllUnits[SelectedUnits[i]].NewOrder(x, y);
                }

            }
        }
        int mod(int a,int b)
        {
            while (a < 0)
                a += b;
            while (a >= b)
                a -= b;
            return a;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            msDown = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
            if (timer1.Enabled)
                button2.Text = "Преостановить";
            else
                button2.Text = "Продолжить";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //показываем фон - карту:
            Map.ShowMap(g,pictureBox1.Width,pictureBox1.Height);
            //если происходит выделение, то активно его поддерживаем:
            if (msDown && (msX != msX2 || msY != msY2))
            {
                g.DrawRectangle(new Pen(Color.LightGreen), min(msX, msX2) - pictureBox1.Location.X, min(msY, msY2), Math.Abs(msX - msX2)-pictureBox1.Location.Y, Math.Abs(msY - msY2));
                SelectedUnits.Clear();
                panel2.Visible = false;
                int Xfrom = mod(((msX - pictureBox1.Location.X) / k) + Map.x, Map.w);
                int Xto = mod(((msX2 - pictureBox1.Location.X) / k) + Map.x, Map.w);
                int Yfrom = mod(((msY - pictureBox1.Location.Y) / k) + Map.y, Map.h);
                int Yto = mod(((msY2 - pictureBox1.Location.Y) / k) + Map.y, Map.h);
                if(Xfrom>Xto)
                {
                    Xfrom += Xto;
                    Xto = Xfrom - Xto;
                    Xfrom -= Xto;
                }
                if (Yfrom > Yto)
                {
                    Yfrom += Yto;
                    Yto = Yfrom - Yto;
                    Yfrom -= Yto;
                }
                int a = -1; ;
                for (int i = Xfrom; i <= Xto; i++)
                    for (int j = Yfrom; j <= Yto; j++)
                    {
                        if (Map.all[i, j] == Cell.Unit)
                        {
                            a = Map.FindUnit(i, j);
                            if (a != -1)
                                if (Map.AllUnits[a].Color == Map.userColor)
                                {
                                    SelectedUnits.Add(a);
                                    panel2.Visible = true;
                                }
                            a = -1;
                        }
                    }
            }
            //отрисовка выделенного (если есть):
            if (SelectedUnits.Count > 0)
                foreach (int u in SelectedUnits)
                {
                    g.DrawRectangle(new Pen(Color.LightGreen), mod((Map.AllUnits[u].X - Map.x), Map.w) * k + 15,
                        mod((Map.AllUnits[u].Y - Map.y), Map.h) * k + 15, k - 30, k - 30);
                }
            //отрисовка выделения:
            
            pictureBox1.Image = bit;
            
            if (SelectedUnits.Count > 0)
            {
                panel2.Visible = true;
                string cst = "";
                string actToDo = "";
                if (Map.AllUnits[SelectedUnits[0]].cast == 0) cst = "Holop";
                else if ((int)Map.AllUnits[SelectedUnits[0]].cast == 1) cst = "Pidaras s mechom";
                else if ((int)Map.AllUnits[SelectedUnits[0]].cast == 2) cst = "Huesos with luk & strelami";
                else if ((int)Map.AllUnits[SelectedUnits[0]].cast == 3) cst = "Kaldun";
                if ((int)Map.AllUnits[SelectedUnits[0]].act == 0) actToDo = "Nihuia";
                else if ((int)Map.AllUnits[SelectedUnits[0]].act == 1) actToDo = "Eboshit to tochka";
                else if ((int)Map.AllUnits[SelectedUnits[0]].act == 2) actToDo = "Deretsya";
                label1.Text = "Kto\t: " + cst +
                    "\nX\t: " + Map.AllUnits[SelectedUnits[0]].X +
                    "\nY\t: " + Map.AllUnits[SelectedUnits[0]].Y +
                    "\nDjituha\t: " + Map.AllUnits[SelectedUnits[0]].Life +
                    "\nAttack\t: " + Map.AllUnits[SelectedUnits[0]].attack +
                    "\nAttack Radius\t: " + Map.AllUnits[SelectedUnits[0]].FightRadius +
                    "\nZorkost'\t: " + Map.AllUnits[SelectedUnits[0]].ViewRadius +
                    "\nChto delaet\t: " + actToDo;
            }
            else if (SelectedUnits.Count == 1)
            {

            }
            else if (SelectedUnits.Count > 1)
            {
                if (SelectedUnits.Count == 3)
                {//для будующих пасхалок с комбинированным выделением и особыми постройками

                }
            }

            foreach (Unit u in Map.AllUnits)
            {
                u.Act();
                //if (u.act == ToDo.GoToPoint)
                //{
                //    if (u.WayToGo.Count > 0)
                //        u.Move();
                //    else
                //        u.act = ToDo.None;
                //}
            }
            if (msX < 10)
                Map.x = mod(Map.x - 1, Map.w);
            if (msY < 10)
                Map.y = mod(Map.y - 1, Map.h);
            if (msX > this.Width - 10)
                Map.x = mod(Map.x + 1, Map.w);
            if (msY > this.Height - 10)
                Map.y = mod(Map.y + 1, Map.h);
        }

        private void Game_MouseMove(object sender, MouseEventArgs e)
        {
            msX = Cursor.Position.X;
            msY = Cursor.Position.Y;
        }
    }
}
