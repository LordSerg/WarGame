using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarLib
{
    public class Worker:Unit
    {
        private int pictureState;//для отрисовки чувака, шоб была хоть какая-то анимация
        //public override void BuildTo(Coord to, int BuildingIndex)
        //{
        //    
        //}
        int expStorae = 0;//каждые 5 профильных действий повышают опыт
        private Coord ExtractPoint;
        private List<Coord> pathToMain;
        private List<Coord> pathToHome;

        public Iwolker wolker;

        public override void Do()
        {
            if (Action == 0) { }//ниче
            else if (Action == 1)//идти в точку
            { GoTo(); }
            else if (Action == 2)//строить
            { BuildTo(); }
            else if (Action == 3)//атаковать
            { AtackTo(); }
            else if (Action == 4)//добывать
            { ExtractTo(); }
        }

        //эти функции будут вызывать с помощью ф-ии "do", которая в свою очередь будет вызываться в главном цикле игры
        public void ExtractTo()
        {//добывать

        }
        public override void Draw()
        {
            if(pictureState==0)
            {
                //
            }
        }
        public Worker()
        {
            Location = new Coord(0, 0);
            Life = 10;
            color = Game.PlayerColor;
            size = 1;
            Speed = 5;
            Atack = 2;
            AtackRadius = 1;
            price = new Price();
            Experience = 0;
            Action = 0;
            path = new List<Coord>();
        }
    }
}
