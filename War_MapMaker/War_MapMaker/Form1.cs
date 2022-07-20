using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace War_MapMaker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            treeView1.BeforeSelect += treeView_BeforeSelect;
            treeView1.BeforeExpand += treeView_BeforeExpand;
            FillDriveNodes();
        }
        private void treeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.Nodes.Clear();
            string[] dirs;
            string[] files;
            try
            {
                if (Directory.Exists(e.Node.FullPath))
                {
                    dirs = Directory.GetDirectories(e.Node.FullPath);
                    files = Directory.GetFiles(e.Node.FullPath);
                    if (dirs.Length != 0||files.Length!=0)
                    {
                        for (int i = 0; i < dirs.Length; i++)
                        {
                            TreeNode dirNode = new TreeNode(new DirectoryInfo(dirs[i]).Name);
                            FillTreeNode(dirNode, dirs[i]);
                            e.Node.Nodes.Add(dirNode);
                        }
                        for (int i = 0; i < files.Length; i++)
                        {
                            TreeNode dirNode = new TreeNode(new DirectoryInfo(files[i]).Name);
                            FillTreeNode(dirNode, files[i]);
                            e.Node.Nodes.Add(dirNode);
                        }
                    }
                }
            }
            catch (Exception ex) { }
            label11.Text = e.Node.FullPath.ToString();

        }
        private void treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.Nodes.Clear();
            string[] dirs;
            string[] files;
            try
            {
                if (Directory.Exists(e.Node.FullPath))
                {
                    dirs = Directory.GetDirectories(e.Node.FullPath);
                    files = Directory.GetFiles(e.Node.FullPath);
                    if (dirs.Length != 0||files.Length!=0)
                    {
                        for (int i = 0; i < dirs.Length; i++)
                        {
                            TreeNode dirNode = new TreeNode(new DirectoryInfo(dirs[i]).Name);
                            FillTreeNode(dirNode, dirs[i]);
                            e.Node.Nodes.Add(dirNode);
                        }
                        for (int i = 0; i < files.Length; i++)
                        {
                            TreeNode dirNode = new TreeNode(new DirectoryInfo(files[i]).Name);
                            FillTreeNode(dirNode, files[i]);
                            e.Node.Nodes.Add(dirNode);
                        }
                    }
                }
            }
            catch (Exception ex) { }
            label11.Text = e.Node.FullPath.ToString();
        }
        private void FillDriveNodes()
        {
            try
            {
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    TreeNode driveNode = new TreeNode { Text = drive.Name };
                    FillTreeNode(driveNode, drive.Name);
                    treeView1.Nodes.Add(driveNode);
                }
            }
            catch (Exception ex) { }
        }
        private void FillTreeNode(TreeNode driveNode, string path)
        {
            try
            {
                string[] dirs = Directory.GetDirectories(path);
                foreach (string dir in dirs)
                {
                    TreeNode dirNode = new TreeNode();
                    dirNode.Text = dir.Remove(0, dir.LastIndexOf("\\") + 1);
                    driveNode.Nodes.Add(dirNode);
                }
                string[] files = Directory.GetFiles(path);
                foreach (string file in files)
                {
                    TreeNode dirNode = new TreeNode();
                    dirNode.Text = file.Remove(0, file.LastIndexOf("\\") + 1);
                    driveNode.Nodes.Add(dirNode);
                }
            }
            catch (Exception ex) { }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.Enabled = false;
            tabControl1.SelectedIndex = 0;
            button1.Text = "Open";
            comboBox1.SelectedIndex = 0;
            label11.Visible = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            panel1.Enabled = checkBox1.Checked;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                button1.Text = "Open";
                label11.Visible = true;
            }
            else
            {
                button1.Text = "Create";
                label11.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        enum howMuch
        {
            very_few,
            few,
            middle,
            planty,
            maximum
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            howMuch h=(howMuch)trackBar1.Value;
            label5.Text =h.ToString();
        }

        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            howMuch h = (howMuch)trackBar5.Value;
            label6.Text = h.ToString();
        }
        Map m;
        private void button1_Click(object sender, EventArgs e)
        {
            string pth="";
            if (button1.Text == "Open")
            {
                //считываем карту
                pth = label11.Text;

                m = new Map(pth);
                GoEdit(pth);
            }
            else if (button1.Text == "Create")
            {
                pth = Directory.GetCurrentDirectory();
                string file = textBox1.Text;
                if (file != "")
                {
                    //DirectoryInfo dir = new Directory(pth);
                    string[] s = pth.Split('\\');
                    pth = "";
                    for (int i = 0; i < s.Length - 4; i++)
                    {
                        pth += s[i] + '\\';
                    }
                    pth += "Map\\" + file + ".warmp";
                    label11.Visible = true;
                    label11.Text = pth;
                    if (!File.Exists(pth))
                    {
                        File.Create(pth);
                        if(checkBox1.Checked)
                        {//создаем карту с автоматическим наложением
                            m = new Map();
                        }
                        else
                        {//создаем "лысую" карту
                            m = new Map();
                        }
                        GoEdit(pth);
                    }
                    else
                    {
                        Attention f = new Attention();
                        f.txt = "Unfortunately, map with this name alredy exists. Try to rename your map.";
                        f.ShowDialog();
                    }
                }
                else
                {
                    Attention f = new Attention();
                    f.txt = "You didn't give a name to your map";
                    f.ShowDialog();
                }
            }
            
        }
        void GoEdit(string pth)
        {
            Editor editor = new Editor();
            editor.Path = pth;
            //this.Close();
            editor.ShowDialog();
        }
    }
}
