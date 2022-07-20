using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace War
{
    public enum Cell
    {
        Free=0,//только если клетка пуста, то по ней можно передвигаться
        Woods=1,//при добыче рес-ов они освобождают место
        RockMount=2,
        CrystalMount=3,
        GoldMount=4,
        Unit=5,
        Building=6
        //Water//???
    }
    public struct point
    {
        public int x, y;
        public point(int X, int Y)
        {
            x = X;
            y = Y;
        }
    }
    public class Map
    {//карта
        //карта будет состоять из квадратов.
        //карта будет прямоугольной и замкнутой
        //возможные разновидности клеток: 
        //- пустота - только тут можно ходить
        //- лес//реурс - дерево
        //- горы//ресурс - камень
        //- кристаллические горы//ресурс - кристалл
        //- золотые горы//ресурс - золото
        //- юнит
        //- здание
        //- вода//ОООЧЕНЬ под вопросом т.к. мне лень придумывать еще водную механику игры
        public static int w, h;
        public static int x, y;//координаты для показания карты
        public string path, name;
        public static Cell[,] all;
        public static double[,] wayBuf;
        public static List<Resource> resourcs;
        public static List<Unit> AllUnits;
        public static List<Building> AllBuildings;
        private string read;
        public static int magicNum = 8;//число, отвечающее за масштаб карты
        int Woods, MountAll, MountR, MountC, MountG;//wtf?
        public static int wood, rock, crystal, gold;
        public static Col userColor;
        public Map(string pth)
        {//считывать карту
            this.path = pth;
            FileStream fs = File.OpenRead(path);
            StreamReader sr = new StreamReader(fs);
            //читаем размеры карты
            string a=sr.ReadLine();
            string []b = a.Split(' ');
            string[] c;
            w = Convert.ToInt32(b[0]);
            h = Convert.ToInt32(b[1]);
            resourcs = new List<Resource>();
            resourcs.Add(new Tree(1, 0, 0));
            AllUnits = new List<Unit>();
            AllBuildings = new List<Building>();
            all = new Cell[w, h];
            //читаем и записаваем каждую ячейку
            for (int i=0;i<h;i++)
            {
                if (sr.Peek() < 0)
                    break;
                a = sr.ReadLine();
                b = a.Split(' ');
                for (int j=0;j<w;j++)
                {
                    if (b[j] == "0")//если клетка пуста
                        all[j, i] = Cell.Free;
                    else
                    {//если что-то есть
                        c = b[j].Split(',');
                        if (c[0] == "1")
                        {//дерево
                            all[j, i] = Cell.Woods;
                            resourcs.Add(new Tree(int.Parse(c[1]), j, i));
                        }
                        else if (c[0] == "2")
                        {//камень
                            all[j, i] = Cell.RockMount;
                            resourcs.Add(new Rock(Convert.ToInt32(c[1]), j, i));
                        }
                        else if (c[0] == "3")
                        {//кристалл
                            all[j, i] = Cell.CrystalMount;
                            resourcs.Add(new Crystal(Convert.ToInt32(c[1]), j, i));
                        }
                        else if (c[0] == "4")
                        {//золото
                            all[j, i] = Cell.GoldMount;
                            resourcs.Add(new Gold(Convert.ToInt32(c[1]), j, i));
                        }
                        else if (c[0] == "5")
                        {//юнит
                            all[j, i] = Cell.Unit;
                            if (c[1] == "1")
                            {
                                if (c[12] != "0")
                                    AllUnits.Add(new Worker(j, i, int.Parse(c[2]), int.Parse(c[3]), (Col)int.Parse(c[4]), (Rase)int.Parse(c[5]), int.Parse(c[6]),
                                        int.Parse(c[7]), int.Parse(c[8]), int.Parse(c[9]), int.Parse(c[10]), int.Parse(c[11]), (ToDo)int.Parse(c[12])));
                                else
                                    AllUnits.Add(new Worker(j, i, int.Parse(c[2]), int.Parse(c[3]), (Col)int.Parse(c[4]), (Rase)int.Parse(c[5]), int.Parse(c[6]),
                                        int.Parse(c[7]), int.Parse(c[8]), -1,-1, int.Parse(c[11]), (ToDo)int.Parse(c[12])));
                            }
                            if (c[1] == "2")//тоже самое как и у рабочего, но пожже
                                AllUnits.Add(new Warrior());
                            if (c[1] == "3")
                                AllUnits.Add(new Archer());
                            if (c[1] == "4")
                                AllUnits.Add(new Worker());
                        }
                        else if (c[0] == "6")
                        {//строение

                        }
                    }

                }
            }
            //считываем ресурсы
            a = sr.ReadLine();
            b = a.Split(' ');
            wood = int.Parse(b[0]);
            rock = int.Parse(b[1]);
            crystal = int.Parse(b[2]);
            gold = int.Parse(b[3]);
            a = sr.ReadLine();
            userColor = (Col)int.Parse(a);
            sr.Close();
            fs.Close();
        }
        public Map()
        {

        }
        public Map(int Width, int Height/*,string terrain*/, string Path, string Name)
        {//создание лысой карты

        }
        public Map(int Width, int Height, double Woods, double Mounts, double Rock, double Crystal, double Gold, /*string terrain,*/ string Path, string Name)
        {//создание карты с автоматически заполненым ландшафтом
            /*
             __________________
            |  _____________|__|
            | |             |
            | |             | _
            | |             |/ |
            | |             /_/
            | |            /|\
            | |           / | \
            | |           | | |
            | |           | | |
            | |             /\
            | |            |  \
            | |             \  \
            | |              |  |
            | |             /  /
            | |            /  /
            | |            \  \
            | |            
            | |           
            
             */
        }

        public static void Add(Unit someone, int x, int y)
        {
            int i = x, j = y;
            //for (int i = x; i < x + someone.Size; i++)
                //for (int j = y; j < y + someone.Size; j++)
                    all[i % w, j % h] = Cell.Unit;
            
            AllUnits.Add(someone);
        }
        public static void Add(Building somehome, int x, int y)
        {//все проверки уже были сделаны
            for (int i = x; i < x + somehome.Size; i++)
                for (int j = y; j < y + somehome.Size; j++)
                    all[i % w, j % h] = Cell.Building;
            AllBuildings.Add(somehome);
        }
        void ReadMap(string all)
        {

        }
        void ReadSavedMap(string all)
        {

        }
        public static void ShowMap(Graphics g,int pictW,int pictH)
        {//отображаем magicNum*magicNum клеток
            int viewWidth=magicNum, viewHeightl= magicNum;
            g.Clear(Color.DarkOrange);//этим цветом будут отображаться пустые клетки
            Brush bruh = Brushes.Green;
            int k = pictW > pictH ? pictH : pictW;
            k = k / viewWidth;
            Image imwood = Image.FromFile(@"d:\_my_games\_war\war\war\woods.png");
            imwood = new Bitmap(imwood, new Size(k, k));
            Image imrock = Image.FromFile(@"d:\_my_games\_war\war\war\RockMountians.png");
            imrock = new Bitmap(imrock, new Size(k, k));
            Image imcryst = Image.FromFile(@"d:\_my_games\_war\war\war\crystalmountains.png");
            imcryst = new Bitmap(imcryst, new Size(k, k));
            Image imgold = Image.FromFile(@"d:\_my_games\_war\war\war\goldmountains.png");
            imgold = new Bitmap(imgold, new Size(k, k));
            Image imworker = Image.FromFile(@"d:\_my_games\_war\war\war\worker1.png");
            imworker = new Bitmap(imworker, new Size(k, k));
            for (int i = 0; i < viewWidth; i++)
                for (int j = 0; j < viewHeightl; j++)
                {
                    if (all[(i + x) % w, (j+y) % h] != Cell.Free)
                    {
                        if (all[(i + x) % w, (j + y) % h] == Cell.Woods)
                        {
                            bruh = Brushes.Green;
                            g.DrawImage(imwood, i * k, j * k);
                            //g.FillRectangle(bruh, i * k, j * k, k, k);
                        }
                        else if (all[(i + x) % w, (j + y) % h] == Cell.RockMount)
                        {
                            bruh = Brushes.Black;
                            g.DrawImage(imrock, i * k, j * k);
                            //g.FillRectangle(bruh, i * k, j * k, k, k);
                        }
                        else if (all[(i + x) % w, (j + y) % h] == Cell.CrystalMount)
                        {
                            bruh = Brushes.Cyan;
                            g.DrawImage(imcryst, i * k, j * k);
                            //g.FillRectangle(bruh, i * k, j * k, k, k);
                        }
                        else if (all[(i + x) % w, (j + y) % h] == Cell.GoldMount)
                        {
                            bruh = Brushes.Gold;
                            g.DrawImage(imgold, i * k, j * k);
                            //g.FillRectangle(bruh, i * k, j * k, k, k);
                        }
                        else if (all[(i + x) % w, (j + y) % h] == Cell.Unit)
                        {
                            bruh = Brushes.Red;
                            g.DrawImage(imworker, i * k, j * k);
                            //g.DrawImage(,,,,System.Drawing.Imaging.ImageAttributes);//для расцветки чел-а
                            //g.FillEllipse(bruh, i * k, j * k, k, k);
                            //g.DrawEllipse(new Pen(Color.Black), i * k, j * k, k, k);
                        }
                        else if (all[(i + x) % w, (j + y) % h] == Cell.Building)
                        {
                            bruh = Brushes.Green;
                            g.FillRectangle(bruh, i * k, j * k, k, k);
                        }
                    }
                }
            for (int i = 0; i < viewWidth; i++)
            {
                g.DrawLine(new Pen(Color.White), i * k, 0, i * k, pictH);
                g.DrawLine(new Pen(Color.White), 0, i * k, pictW, i * k);
            }
            imwood.Dispose();
            imrock.Dispose();
            imcryst.Dispose();
            imgold.Dispose();
            imworker.Dispose();
        }
        public static int FindUnit(int xc,int yc)
        {//возвращаем индекс найденого юнита
            /*int answer=-1,i=0;
            AllUnits.Find(x => (x.X == xc) && (x.Y == yc));
            foreach (Unit u in AllUnits)
            {
                if (u.X == xc && u.Y == yc)
                {
                    answer = i;
                    break;
                }
                i++;
            }
            return answer;*/
            if (!AllUnits.Exists(x => (x.X == xc) && (x.Y == yc)))
                return -1;
            return AllUnits.Find(x => (x.X == xc) && (x.Y == yc)).ID;
        }
        /*...*/
    }
}
