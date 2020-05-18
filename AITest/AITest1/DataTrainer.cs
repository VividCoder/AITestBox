using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Drawing;
using NeuralNetwork;

namespace AITest1
{
    public class DataTrainer
    {

        NeuralNet ann;
        int rcc = 0;
        int IW, IH;
        // Sequential Model;
        public DataTrainer(int iw, int ih, int rc, List<DataImage> imgs, int times =5, int epochs = 10)
        {

            IW = iw;
            IH = ih;
            rcc = rc;
            var nn = new NeuralNet.Builder();
            nn.SetNeuronsInputLayer((uint)rc);
            nn.SetNeuronsForLayers(16, (uint)(iw * ih * 3));
            nn.SetWeightsInitializer(InitializerWeights.Random);
            nn.SetBiasNeurons(true, InitializerBias.Random);
            nn.SetActivationFunc(ActivationFunc.Sigmoid);
            nn.SetLearningOptimizing(LearningOptimizing.SGDM);
            nn.SetLossFunc(LossFunc.Arctan);
            nn.SetLearningRate(0.3f);
            nn.SetMomentumRate(0.2f);
            
            ann = nn.Build();
            Random rnd = new Random(Environment.TickCount);

            for (int k = 0; k < epochs; k++)
            {
                int i = rnd.Next(0, imgs.Count - 1);
                //for (int i = 0; i < imgs.Count; i++)
                //{

                imgs[i].RandData = new DataRandom(8);
                    Console.WriteLine("Img:" + i + "/" + imgs.Count);
                    float[] dat = imgs[i].RandData.pdat;




                    float[][] na = new float[][]
                    {
                    dat
                    };

                    float[][] ed = new float[][]
                    {
                    imgs[i].pdat
                    };

                    //float[][] na = new float[][]
                    //{
                    //    new float[]
                    //}


                    for (int j = 0; j < times; j++)
                    {
                        float err = ann.Learn(na, ed, 20);
                        Console.WriteLine("Err:" + err + " J:" + j + "/" + times);
                    }
                    Console.WriteLine("E:" + k + "/" + epochs);
                    //{
                    //   Console.Write($"Epoch: {ann.LearningCounter}, Loss: {loss.ToString("P4")}");
                    //}
                //}
            }
            //var res = ann.Activate(new float[][] { new DataRandom(rc).pdat });

           

        }

        int tc = 0;
        public Bitmap BuildImg()
        {

            tc = tc + 150;
            var dat = new float[][] { new DataRandom(rcc).pdat };

            var res = ann.Activate(dat);

            float[] aimg = res[0];
            int al = 0;

            Bitmap bm = new Bitmap(IW, IH);
            Random rnd = new Random(tc);

            for(int y = 0; y < IH; y++)
            {
                for(int x = 0; x < IW; x++)
                {

                    int cr, cg, cb;

                    cr = (int)(aimg[al++]*255.0f);
                    cg = (int)(aimg[al++] * 255.0f);
                    cb = (int)(aimg[al++] * 255.0f);

                    //cr = cg = cb = 0;
                    if (cr < 0) cr = 0;
                    if (cg < 0) cg = 0;
                    if (cb < 0) cb = 0;
                    if (cr > 255) cr = 255;
                    if (cg > 255) cg = 255;
                    if (cb > 255) cb = 255;
                    bm.SetPixel(x, y, Color.FromArgb(255, cr, cg, cb));

                }
            }
            return bm;

        }

    }
}
