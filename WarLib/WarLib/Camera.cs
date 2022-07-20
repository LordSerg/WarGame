using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WarLib
{
    public class Camera
    {
        //центр камеры указывает на координату:
        public Coord coord { get; private set; }
        //высота и ширина окна(точнее пикчербокса) в котором отображается игра:
        private int width_view, height_view;
        public int scale { get; set; }
        //высота и ширина, количество отображаемых клеток:
        public int Width { get; private set; }
        public int Height { get; private set; }
        //клетки для отображения:
        public Cell[,] Cells { get; private set; }
        public Camera(PictureBox pb, Cell [,]ViewCells)
        {
            width_view = pb.Width;
            height_view = pb.Height;
            Width = (width_view / scale) + 1;
            Height = (height_view / scale) + 1;
            for (int i = 0; i < ViewCells.GetLength(0); i++)
                for (int j = 0; j < ViewCells.GetLength(1); j++)
                    Cells[i, j] = ViewCells[i, j];
            coord = new Coord(Width / 2, Height / 2);
        }
        public void ChangeScale(int newScale)
        {

        }
        public void ChangePosition(Coord newPosition)
        {

        }
        public void Show()
        {

        }
    }
}
