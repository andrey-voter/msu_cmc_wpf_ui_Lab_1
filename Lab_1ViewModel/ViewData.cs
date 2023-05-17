using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using сlass_library_lab1;
using OxyPlot;
using System.Windows.Input;
using System.Diagnostics;

namespace Lab_1ViewModel
{

    public interface IErrorReporter
    { 
        void ReportError(string message);
    }


    public class ViewData : ViewModelBase, IDataErrorInfo 
    {
        public RawData? rawData;
        public SplineData? splineData;

        public readonly IErrorReporter errorReporter;

        //rawData
        public double a { get; set; }
        public double b { get; set; }
        public int NodeCnt { get; set; }
        public bool IsUniform { get; set; }
        public FRawEnum fRawEnum { get; set; }
        
        public Array funcs { 
            get
            {
                return FRawEnum.GetValues(typeof(FRawEnum));
            }
            set { }
        }

        public List<string> raw_values { get; set; }

        //splineData
        public int SplineNodeCnt { get; set; }
        public double Spline2Left { get; set; }
        public double Spline2Right { get; set; }

        public double[] SplineInfo { get; set; }

        public List<SplineDataItem> SplineList { get; set; }

        public double Integral { get; set; }

        //Plotting
        public MyPlotModel ChartData { get; set; }

        public PlotModel PlotModel { get; set; }
 

        public ICommand SaveCommand { get; private set; }
        public ICommand FromControlsCommand { get; private set; }
        public ICommand FromFileCommand { get; private set; }

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
                errorReporter.ReportError(exc.Message);
            }

        }

        private void ToFile()
        {
            Microsoft.Win32.SaveFileDialog dlg = new()
            {
                FileName = "Document",
                DefaultExt = ".txt" 
            };
            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                Save(filename);
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
                errorReporter.ReportError(exc.Message);
                return 1;
            }
            return 0;

        }
        
        public ViewData(IErrorReporter errorReporter)
        {
            this.errorReporter = errorReporter;
            a = 0;
            b = 10;
            NodeCnt = 10;
            IsUniform = true;
            fRawEnum = FRawEnum.CubeFunc;
            RaisePropertyChanged(nameof(fRawEnum));


            SplineNodeCnt = 3;
            SplineInfo = new double[2];

            make_raw_data();
            make_spline_data();


            FromControls();
            SaveCommand = new RelayCommand(_ => ToFile());
            FromControlsCommand = new RelayCommand(_ => FromControls());
            FromFileCommand = new RelayCommand(_ => FromFile());
        }

        public void make_raw_data()
        {
            FRaw func;
            if (fRawEnum == 0)
                func = RawData.LinearFunc;
            else if ((int)fRawEnum == 1)
                func = RawData.CubeFunc;
            else
                func = RawData.RandomFunc;
            rawData = new RawData(a, b, NodeCnt, IsUniform, func);
            raw_values = new List<string>();
            for (int i = 0; i < NodeCnt; i++)
                raw_values.Add("x: " + rawData.nodes[i].ToString("0.00") + " y: " + rawData.values[i].ToString("0.00"));
            RaisePropertyChanged(nameof(raw_values));
        }

        public void make_spline_data()
        {
            // MessageBox.Show($"spline node cnt before constructor: {SplineNodeCnt.ToString()}");
            splineData = new SplineData(rawData, SplineInfo[0], SplineInfo[1], SplineNodeCnt);
        }

        public int make_raw_data_from_file(string filename)
        {
            int is_ok = Load(filename);
            if (is_ok != 0)
            {
                errorReporter.ReportError("Problems with loading, try again");
                return 1;
            }
            raw_values = new List<string>();
            for (int i = 0; i < rawData.NodeCount; i++)
                raw_values.Add("x: " + rawData.nodes[i].ToString("0.00") + " y: " + rawData.values[i].ToString("0.00"));

            update_raw_data_info();
            return 0;
        }

        public void update_raw_data_info()
        {
            a = rawData.a;
            b = rawData.b;
            NodeCnt = rawData.NodeCount;

            RaisePropertyChanged(nameof(a));
            RaisePropertyChanged(nameof(b));
            RaisePropertyChanged(nameof(NodeCnt));
            RaisePropertyChanged(nameof(raw_values));
            RaisePropertyChanged(nameof(IsUniform));
            RaisePropertyChanged(nameof(fRawEnum));
        }

        public void update_spline_info()
        {
            SplineList = splineData.Data;
            Integral = splineData.IntegralValue;
            RaisePropertyChanged(nameof(SplineNodeCnt));
            RaisePropertyChanged(nameof(SplineList));
            RaisePropertyChanged(nameof(Integral));    
        }

        private void FromControls()
        {
            try
            {
                make_raw_data();
                make_spline_data();
                bool tmp = splineData.CalculateInerpolation();
                update_spline_info();
                update_raw_data_info();
                ChartData = new MyPlotModel(splineData, rawData);
                PlotModel = ChartData.plotModel;
                RaisePropertyChanged(nameof(PlotModel));
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Couldnt load from controls because of {ex}");
            }
        }

        private void FromFile()
        {
            Microsoft.Win32.OpenFileDialog dlg = new()
            {

            };

            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                errorReporter.ReportError($"Loading from {filename}");
                int is_ok = make_raw_data_from_file(filename);
                if (is_ok != 0) { return; }        
                make_spline_data();
                bool tmp = splineData.CalculateInerpolation();
                update_spline_info();
                ChartData = new MyPlotModel(splineData, rawData);
                PlotModel = ChartData.plotModel;
                RaisePropertyChanged(nameof(PlotModel));
            }
        }

    }
}
