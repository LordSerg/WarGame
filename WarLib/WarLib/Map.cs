using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarLib
{
    public struct Cell
    {
        public bool is_busy;
        public bool is_unit;
        public bool is_tree;
        public bool is_main;
        public bool is_building;
        public Cell(bool is_busy, bool is_unit, bool is_tree, bool is_main, bool is_building)
        {
            this.is_busy = is_busy;
            this.is_unit = is_unit;
            this.is_tree = is_tree;
            this.is_main = is_main;
            this.is_building = is_building;
        }
    }
    //class Map
    public static class Map
    {
        public static int Width { get; private set; }
        public static int Height { get; private set; }
        public static Camera Camera { get; private set; }
        private static Graph graph { get; set; }
        public static Cell[,] Cells;
        public static void ChangeScale(int newScale)
        {
            Camera.ChangeScale(newScale);
        }
        public static List<Coord> FindPath(Coord from, Coord to)
        {//поиск пути
            int f = from.x + from.y * Width;
            int t = to.x + to.y * Width;
            List <Vertex> preAnswer = graph.FindPath(f, t);//возвращаем последовательность вершин
            List<Coord> answer = new List<Coord>();
            foreach(Vertex v in preAnswer)
                answer.Add(new Coord(v.x,v.y));
            return answer;
        }
        //public static void init(Map otherMap)
        //{
        //    graph = new Graph(otherMap.graph);
        //    Width = otherMap.Width;
        //    Height = otherMap.Height;
        //}
        public static void init(int w, int h)
        {
            graph = new Graph(w,h);
            Width = w;
            Height = h;
            //Camera = new Camera();
        }
        public static void init(string pathToFile)
        {

        }
    }
}
