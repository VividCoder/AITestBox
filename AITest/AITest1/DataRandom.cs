
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AITest1
{
    public class DataRandom
    {
        public float[] pdat;
       
        public DataRandom(int num)
        {

            Random r = new Random(Environment.TickCount);

            pdat = new float[num];

            for (int i = 0; i < num; i++)
            {

                pdat[i] = (float)r.NextDouble();

            }

  


        }
    }
}
