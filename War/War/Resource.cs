using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace War
{
    //Все возможные ресурсы;
    //их жизнь = то, сколько надо снести жизни, чтобы взять единицу ресурса
    //num = количество рес-ов на данной клетке
    public class Resource:GameObject
    {
        public int num=0;
        public Resource() { }
    }
    public class Tree : Resource
    {
        //public Tree() { }
        public Tree(int xCoord,int yCoord)
        {
            Life = 20;
            num = 5;
            X = xCoord;
            Y = yCoord;
        }
        public Tree(int Num, int xCoord, int yCoord)
        {
            Life = 20;
            num = Num;
            X = xCoord;
            Y = yCoord;
        }
    }
    public class Rock : Resource
    {
        public Rock(int xCoord, int yCoord)
        {
            Life = 50;
            num = 5;
            X = xCoord;
            Y = yCoord;
        }
        public Rock(int Num, int xCoord, int yCoord)
        {
            Life = 50;
            num = Num;
            X = xCoord;
            Y = yCoord;
        }
    }
    public class Crystal: Resource
    {
        public Crystal(int xCoord, int yCoord)
        {
            Life = 70;
            num = 5;
            X = xCoord;
            Y = yCoord;
        }
        public Crystal(int Num, int xCoord, int yCoord)
        {
            Life = 70;
            num = Num;
            X = xCoord;
            Y = yCoord;
        }
    }
    public class Gold : Resource
    {
        public Gold(int xCoord, int yCoord)
        {
            Life = 100;
            num = 5;
            X = xCoord;
            Y = yCoord;
        }
        public Gold(int Num, int xCoord, int yCoord)
        {
            Life = 100;
            num = Num;
            X = xCoord;
            Y = yCoord;
        }
    }
}
