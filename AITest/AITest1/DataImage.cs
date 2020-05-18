
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Drawing;
namespace AITest1
{
    public class DataImage
    {
       
        public float[] pdat;


        public DataRandom RandData;

        public DataImage(string path,int w,int h)
        {


            var bit = new Bitmap(new Bitmap(path), new Size(w, h));

            pdat = new float[bit.Width * bit.Height * 3];
            int ploc = 0;


            for(int y = 0; y < bit.Height; y++)
            {

                for(int x = 0; x < bit.Width; x++)
                {

                    pdat[ploc++] = (float)bit.GetPixel(x,y).R / 255.0f;
                    pdat[ploc++] = (float)bit.GetPixel(x, y).G / 255.0f;
                    pdat[ploc++] = (float)bit.GetPixel(x, y).B / 255.0f;

                }

            }

       
            RandData = new DataRandom(8);

        }

    }
}
