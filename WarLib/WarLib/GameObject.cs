using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarLib
{
    public struct Coord
    {
        public int x, y;
        public Coord(int _x,int _y)
        {
            x = _x;
            y = _y;
        }
    }
    public class GameObject
    {
        public Coord Location;//{ get; set; }
        public int Life { get; protected set; }
        public int size { get; protected set; }
        public int color { get; protected set; }
        public int viewRadius { get; protected set; }
        public bool is_Selected { get; set; }
        public void Select() { is_Selected = true; }
        virtual public void Draw() { }
        public GameObject()
        {

        }
    }
}
