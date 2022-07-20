using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace War_MapMaker
{
    /*enum Tree
    {
        Tree1,
        Tree2,
        Tree3
    }*/
    public enum Cell
    {
        Free,//только если клетка пуста, то по ней можно передвигаться
        Woods,//при добыче рес-ов они освобождают место
        RockMount,
        CrystalMount,
        GoldMount,
        Unit,
        Building
        //Water//???
    }
    public struct point
    {
        public int x, y;
        public point(int X,int Y) 
        { 
            x = X;
            y = Y; 
        }
    }
    /*
        public Map(string Path, bool IsNew)
        {
            path = Path;
            //FileStream x = File.OpenRead(path);
            //StreamReader y = new StreamReader(x);
            string r = File.ReadAllText(path);

            if (!IsNew)
            {//продолжить игру
                //ReadMap(path);//считываем карту из файла .s1/.s2/.../s10
            }
            else
            {//новая
                //ReadSavedMap(path);//считываем карту из файла .warmp
            }


        }
        */
    //ro в данном случае показывает размер карты (ro=радиус планеты):
        //ro = 10 -> S = 4*PI*ro*ro ~ 1256,6  ~ 1257  клеток
        //тогда h=20 w=60 ~ tiny
        //ro = 20 -> S = 4*PI*ro*ro ~ 5026,6  ~ 5027  клеток
        //тогда h=40 w=126 ~ small
        //ro = 50 -> S = 4*PI*ro*ro ~ 31415,9 ~ 31416 клеток
        //тогда h=100 w=314 ~ middle
        //ro = 100 -> S = 4*PI*ro*ro ~ 125663,7 ~ 125664 клеток
        //тогда h=200 w=628 ~ big
        //ro = 500 -> S = 4*PI*ro*ro ~ 3141592,6 ~ 3141593 клеток
        //тогда h=1000 w=3142 ~ huge

    //

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
        public int w, h;
        public string path,name;
        public Cell[,] all;
        private string read;
        int Woods,MountAll, MountR, MountC, MountG;
        public Map(string path)
        {//считывать карту
            this.path = path;
            read = File.ReadAllText(this.path);
        }
        public Map()
        {//создаем карту с автоматическим наложением

        }
        public Map (int Width,int Height/*,string terrain*/,string Path,string Name)
        {//создание лысой карты

        }
        public Map(int Width, int Height,double Woods,double Mounts, double Rock,double Crystal,double Gold, /*string terrain,*/ string Path, string Name)
        {//создание карты с автоматически заполненым ландшафтом
         //TODO
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

        public void Add(Unit someone,int x,int y)
        {

        }
        public void Add(Building somehome,int x,int y)
        {//все проверки уже были сделаны
            for (int i = x; i < x + somehome.Size; i++)
                for (int j = y; j < y + somehome.Size; j++)
                    all[i % w, j % h] = Cell.Building;
        }
        void ReadMap(string all)
        {

        }
        void ReadSavedMap(string all)
        {

        }
        /*...*/
    }
}
