using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace War_MapMaker
{
    //по сути GameObject - это игрок
    public class GameObject
    {
        //текущее количество рес-ов у игрока
        public static int wood, rock, crystal, gold;
        public static Map map;
        public static List<Unit> AllUnits;
        public static List<Building> AllBuildings;
        public GameObject() { wood = rock = crystal = gold = 10; }
        public GameObject(int w,int r,int c,int g)
        {
            wood = w;
            rock = r;
            crystal = c;
            gold = g;
        }
        private int x, y;//координаты
        public int X { get; set; }
        public int Y { get; set; }
        private int life;//жизнь
        public int Life { get { return life < 0 ? 0 : life; } set { life = value; } }
        private int h, w;//габбариты объекта
        public int Size
        {//все габбариты в игре - квадратной формы
            get
            {
                if (h == w)
                    return h / 2;
                //(промежуточные клетки - для передвижений)
                //т.е. если h==w==4, то юзер это будет видеть как размер 2*2
                //соответственно - h==w==8 => 4*4
                else
                    return 0;
            }
            set
            {
                h = value * 2;
                w = value * 2;
            }
        }
        private Col color;//игрок(цвет)
        public Col Color { get { return color; } set { color = (Col)((int)value % 8); } }
        private Rase rase;//раса
        public Rase Rase { get { return rase; } set { rase = (Rase)((int)value % 2); } }
        public int ViewRadius { get; set; }//радиус видимости(то, что видит)
        public virtual void Show(Graphics g) { }
    }
}
