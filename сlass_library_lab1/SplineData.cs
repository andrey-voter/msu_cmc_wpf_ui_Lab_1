using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace сlass_library_lab1
{
    public class SplineData
    {
        public RawData? rawData;
        public int NodeCnt;
        public List<SplineDataItem> Data;
        public double Spline2Left;
        public double Spline2Right;

        public SplineData(RawData? rawData, double spline2Left, double spline2Right, int nodeCnt)
        {

            Data = new List<SplineDataItem>();
            this.rawData = rawData;
            NodeCnt = nodeCnt;
            IntegralValue = 0;
            Spline2Left = spline2Left;
            Spline2Right = spline2Right;
        }

        public double IntegralValue;
        
        public bool CalculateInerpolation()
        {
            int ret = 0;
            double[] y = rawData.values;

            int number_x = rawData.NodeCount;

            double[] scoeff = new double[4 * (number_x - 1)];

            int nsite = NodeCnt;
             
            double[] bc = new double[2] { Spline2Left, Spline2Right };

            double[] site = new double[2] { rawData.a, rawData.b };

            int[] dorder = new int[3] { 1, 1, 1 };

            double[] result = new double[3 * nsite];

            double[] x = rawData.nodes;

            double[] leftLim = new double[1] { rawData.a };
            double[] rightLim = new double[1] { rawData.b };

            double[] intRes = new double[1];

            //try
            {
                Interpolate(number_x, 1, x, y, bc, scoeff, nsite, site, 3, dorder, result, ref ret, leftLim, rightLim, intRes);
                if (ret == -1)
                {
                    return false;
                }

                IntegralValue = intRes[0];

                for (int i = 0; i < NodeCnt; i++)
                {
                    int idx = 3 * i;
                    double X = (rawData.a * (NodeCnt - i - 1) + rawData.b * i) / (NodeCnt - 1);
                    Data.Add(new SplineDataItem(X, result[idx], result[idx + 1], result[idx + 2]));
                }
            }
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex);
            //    return false;
            //}
            return true;
        }

        [DllImport("..\\..\\..\\..\\x64\\DEBUG\\Dll_c++_lab1.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern void Interpolate(int number_x, int number_y, double[] x, double[] y, double[] bc, double[] scoeff,
            int nsite, double[] site, int ndorder, int[] dorder, double[] result, ref int ret,
            double[] leftLim, double[] rightLim, double[] intRes);
    }
}
    

