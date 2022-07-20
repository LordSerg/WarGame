using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace War
{
    public class Building : GameObject
    {

    }

    public class Farm : Building
    {
        public Farm(int xCoord,int yCoord)
        {
            Size = 2;
            Life = 100;
            X = xCoord;
            Y = yCoord;

        }
    }
}
