using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Lab_1ViewModel;
using OxyPlot;
using OxyPlot.Series;

namespace wpf_lab_1
{

    public partial class MainWindow : Window
    {
        public ViewData? viewData = new(new MessageBoxErrorReporter());
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewData(new MessageBoxErrorReporter());
        }
    }

    public class MessageBoxErrorReporter : IErrorReporter
    {
        public void ReportError(string message) => MessageBox.Show(message);

    }
}
