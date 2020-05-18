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
            nn.SetNeuronsForLayers(8, (uint)(iw * ih * 3));
            nn.SetWeightsInitializer(InitializerWeights.Random);
            nn.SetBiasNeurons(true, InitializerBias.Random);
            nn.SetActivationFunc(ActivationFunc.Sigmoid);
            nn.SetLearningOptimizing(LearningOptimizing.SGDM);
            nn.SetLossFunc(LossFunc.MSE);
            nn.SetLearningRate(0.1f);
            nn.SetMomentumRate(0.9f);
            
            ann = nn.Build();


            for (int k = 0; k < epochs; k++)
            {
                for (int i = 0; i < imgs.Count; i++)
                {
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
                        float err = ann.Learn(na, ed, 5);
                        Console.WriteLine("Err:" + err + " J:" + j + "/" + times);
                    }
                    Console.WriteLine("E:" + k + "/" + epochs);
                    //{
                    //   Console.Write($"Epoch: {ann.LearningCounter}, Loss: {loss.ToString("P4")}");
                    //}
                }
            }
            //var res = ann.Activate(new float[][] { new DataRandom(rc).pdat });

           

        }

        public Bitmap BuildImg()
        {

            var res = ann.Activate(new float[][] { new DataRandom(rcc).pdat});

            float[] aimg = res[0];
            int al = 0;

            Bitmap bm = new Bitmap(IW, IH);

            for(int y = 0; y < IH; y++)
            {
                for(int x = 0; x < IW; x++)
                {

                    int cr, cg, cb;

                    cr = (int)(aimg[al++]*255.0f);
                    cg = (int)(aimg[al++] * 255.0f);
                    cb = (int)(aimg[al++] * 255.0f);
                    //cr = cg = cb = 0;

                    bm.SetPixel(x, y, Color.FromArgb(255, cr, cg, cb));

                }
            }
            return bm;

        }

    }
}
