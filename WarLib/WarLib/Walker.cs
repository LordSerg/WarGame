using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarLib
{
    interface Iwalker
    {
        void GoNextStep(Coord coordFrom, Coord coordTo);
    }
    class Walker : Iwalker
    {
        public void GoNextStep(Coord From, Coord To)
        {
            //Map.graph.v[From.x + From.y * Map.Width].cell = new Cell(false, false, false, false, false);
            //Map.graph.v[To.x + To.y * Map.Width].cell = new Cell(true, true, false, false, false);
            Map.Cells[From.x, From.y] = new Cell(false, false, false, false, false);
            Map.Cells[To.x, To.y] = new Cell(true, true, false, false, false);
        }
    }
}
