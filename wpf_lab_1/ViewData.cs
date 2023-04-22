using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using сlass_library_lab1;

namespace wpf_lab_1
{
    public class ViewData : IDataErrorInfo
    {
        public RawData? rawData;
        public SplineData? splineData;

        //rawData
        public double a { get; set; }
        public double b { get; set; }
        public int NodeCnt { get; set; }
        public bool IsUniform { get; set; }
        public FRawEnum fRawEnum { get; set; }

        public List<string> raw_values { get; set; }

        //splineData
        public int SplineNodeCnt { get; set; }
        public double Spline2Left { get; set; }
        public double Spline2Right { get; set; }

        public double[] SplineInfo { get; set; }

        public string Error
        {
            get { return "Введены некорректные данные"; }
        }

        public string this[string columnName]
        {
            get
            {
                string result = String.Empty;
                switch (columnName)
                {
                    case "NodeCnt":
                        if (NodeCnt < 2)
                                result = "Должно быть хотя бы 2 точки у интерполируемой функции!";
                        break;
                    case "SplineNodeCnt":
                        if (SplineNodeCnt < 2)
                            result = "Интерполяция должна быть хотя бы 2 точках!";
                        break;
                    case "b":
                        if (b <= a)
                            result = "Права граница должна быть меньше левой";
                        break;

                }
                return result;
            }
        }

        public void Save(string filename)
        {
            try
            {
                rawData.Save(filename);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Coudlnt save because of: " + exc.ToString());
            }

        }

        public int Load(string filename)
        {
            try
            {
                RawData.Load(filename, out rawData);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Coudlnt load because of: " + exc.ToString());
                return 1;
            }
            return 0;

        }
        //public ViewData() { }
        public ViewData()
        {
            SplineInfo = new double[2];
            raw_values = new List<string>();
            a = 1;
            b = 2;
            NodeCnt = 10;
            IsUniform = true;
            fRawEnum = FRawEnum.LinearFunc;
            SplineNodeCnt = 10;
            //FRaw func;
            //func = RawData.LinearFunc;                                             
            //rawData = new RawData(0.0, 1.0, 2, true, func);s
        }

    public void make_raw_data()
        {
            FRaw func;
            if ((int)fRawEnum == 0)
                func = RawData.LinearFunc;
            else if ((int)fRawEnum == 1)
                func = RawData.CubeFunc;
            else
                func = RawData.RandomFunc;
            rawData = new RawData(a, b, NodeCnt, IsUniform, func);
            //a = rawData.a;
            //b = rawData.b;
            //NodeCnt = rawData.NodeCount;
            //IsUniform = rawData.uniform;
            raw_values = new List<string>();
            for (int i = 0; i < NodeCnt; i++)
                raw_values.Add("x: " + rawData.nodes[i].ToString("0.00") + " y: " + rawData.values[i].ToString("0.00"));
        }

        public int make_raw_data_from_file(string filename)
        {
            int is_ok = Load(filename);
            if (is_ok != 0)
            {
                MessageBox.Show("Problems with loading, try again");
                return 1;
            }
            a = rawData.a;
            b = rawData.b;
            NodeCnt = rawData.NodeCount;
            IsUniform = rawData.uniform;
            raw_values = new List<string>();
            for (int i = 0; i < rawData.NodeCount; i++)
                raw_values.Add("x: " + rawData.nodes[i].ToString("0.00") + " y: " + rawData.values[i].ToString("0.00"));
            return 0;
        }
        public void make_spline_data()
        {
           // MessageBox.Show($"spline node cnt before constructor: {SplineNodeCnt.ToString()}");
            splineData = new SplineData(rawData, SplineInfo[0], SplineInfo[1], SplineNodeCnt);
        }

    }
}
