using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace War_MapMaker
{
    public partial class Editor : Form
    {
        public Editor()
        {
            InitializeComponent();
        }
        public Map m;
        string path;
        public string Path
        {
            set{path = value;}
        }
    }
}
