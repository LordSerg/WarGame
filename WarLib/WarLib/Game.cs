using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarLib
{
    public static class Game
    {
        static public Map map;
        static public int PlayerColor { get; private set; }
        static public List<GameObject> objects { get; private set; }
        static public List<GameObject> SelectedObjects { get; private set; }
        //statistics:
        //...
        //actions
        //static public Game(string path)
        //{
        //    ReadGame(path);
        //}
        //static public Game(Map newMap)
        //{
        //    map = new Map(newMap);
        //}
        static private void ReadGame(string path)
        {

        }
        static public void SaveGame(string path)
        {

        }
        static public void LoadGame(string path)
        {

        }
    }
}
