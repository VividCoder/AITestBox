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
using AITest1;

namespace ImageEngine1
{
    public partial class Form1 : Form
    {

        public static int ImgW = 220, ImgH = 220;
        public List<DataImage> Imgs = new List<DataImage>();



        public Form1()
        {
            InitializeComponent();
        }

        private void addFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {

            folderBrowserDialog1.ShowDialog();

            var fold = folderBrowserDialog1.SelectedPath;

            foreach(var f in new DirectoryInfo(fold).GetFiles())
            {

                var n_img = new DataImage(f.FullName, ImgW, ImgH);
                Imgs.Add(n_img);
                Console.WriteLine("Loaded:" + f.Name);
            }

        }

        private void testOut_Paint(object sender, PaintEventArgs e)
        {

        }

        DataTrainer DT = null;
        

        private void button1_Click(object sender, EventArgs e)
        {

            DT = new DataTrainer(ImgW, ImgH,8, Imgs,1,120);


        }

        private void button2_Click(object sender, EventArgs e)
        {
            var img = DT.BuildImg();
            Controls.Remove(testOut);
            testOut = new Panel();
           
            testOut.BackgroundImage = img;

            Controls.Add(testOut);
            testOut.Location = new Point(200, 40);
            testOut.Size = new Size(ImgW*2, ImgH*2);
            testOut.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void addImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

            var name = openFileDialog1.FileName;

            var n_img = new AITest1.DataImage(name,ImgW,ImgH);

            Imgs.Add(n_img);
            Console.WriteLine("Loaded:" + name);
        }
    }
}
