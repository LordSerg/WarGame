using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarLib
{
    public struct Price
    {
        public int w, r, g, c;
        public Price(int wood=0, int rock=0, int gold=0, int crystal=0)
        {
            w = wood;
            r = rock;
            g = gold;
            c = crystal;
        }
    }
    //enum Act:int
    //{
    //    None=1,
    //    Go=2,
    //    Fight=3,
    //
    //}
    public class Unit:GameObject
    {
        public int Speed { get; protected set; }
        public int Atack { get; protected set; }
        public int AtackRadius { get; protected set; }
        public Price price { get; protected set; }
        public int Experience { get; protected set; }
        public int Action { get; protected set; }
        public List<Coord> path { get; protected set; }
        //public Iwalker walker { get; set; }
        //Coord - то, куда надо делать дейcтвие, int
        virtual public void Do() { }
        virtual public void OrderTo(Coord to) { }
        protected int cur_path_id;
        protected int CurID;
        protected float divider;//= скорость
        virtual protected void GoTo() 
        {
            if (path != null && path.Count() > 0)
            {
                if (cur_path_id == divider - 1)
                {
                    cur_path_id = 0;
                    Location.x = path[1].x;
                    Location.y = path[1].y;
                    CurID = Game.map.graph.v.FindIndex(vert => (vert.x == x && vert.y == y));//положение на глобальной карте
                    //CurID = graph_local.v.FindIndex(vert => (vert.x == x && vert.y == y));
                    path.RemoveAt(0);
                    if (path.Count() > 1)
                    {
                        dx = (path[1].x - path[0].x) / divider;
                        dy = (path[1].y - path[0].y) / divider;
                    }
                    else if (path.Count() > 0)
                    {
                        path.RemoveAt(0);
                        //dx = (x - path[0].x) / divider;
                        //dy = (y - path[0].y) / divider;
                    }
                    else
                        return;
                }
                else if (path.Count() == 1)
                {
                    path.RemoveAt(0);
                }
                else if (cur_path_id == 0)
                {
                    dx = (path[1].x - path[0].x) / divider;
                    dy = (path[1].y - path[0].y) / divider;
                }
                Move();
                //Show(g);
            }

        }
        private void Move()
        {
            Location.x += dx;
            Location.y += dy;
            cur_path_id++;
        }
        virtual protected void BuildTo() { }
        virtual protected void AtackTo() { }
    }
}
