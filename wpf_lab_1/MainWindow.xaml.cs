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
using System.Windows.Navigation;
using System.Windows.Shapes;
using сlass_library_lab1;

namespace wpf_lab_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public ViewData? viewData = new();
        public MainWindow()
        {
            InitializeComponent();
            combo_func.ItemsSource = FRawEnum.GetValues(typeof(FRawEnum));
            this.DataContext = viewData;

        }

        private void button_raw_data_from_controls_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Loading from controls");
            try
            {
                //MessageBox.Show($"a: {viewData.a}, b: {viewData.b}, raw_cnt: {viewData.NodeCnt}, isuniform: {viewData.IsUniform}, func: {viewData.fRawEnum}");
                viewData.make_raw_data();
                list_box_raw.ItemsSource = viewData.raw_values;
                list_box_raw.Items.Refresh();
                text_box_left_int_b.Text = viewData.a.ToString();
                text_box_right_int_b.Text = viewData.b.ToString();
                text_box_node_count_raw.Text = viewData.NodeCnt.ToString();
                //MessageBox.Show($"spline_node_cnt: {viewData.SplineNodeCnt}, left: {viewData.SplineInfo[0]}, right: {viewData.SplineInfo[1]}");
                viewData.make_spline_data();
                text_box_node_count_spline.Text = viewData.SplineNodeCnt.ToString();
                bool tmp = viewData.splineData.CalculateInerpolation();
                //MessageBox.Show(tmp.ToString());
                list_box_spline.ItemsSource = viewData.splineData.Data.ToArray();
                text_block_integral.Text = "integral: " + viewData.splineData.IntegralValue.ToString("0.00");
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Couldnt load from controls because of {ex}");
            }
        }

        public void menu_item_raw_data_from_controls_click(object sender, RoutedEventArgs e)
        {
            button_raw_data_from_controls.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

            //System.Windows.Controls.MenuItem

            //button_raw_data_from_controls_Click(sender, e);


            //MessageBox.Show("Loading from controls");
            //try
            //{
            //    MessageBox.Show($"a: {viewData.a}, b: {viewData.b}, raw_cnt: {viewData.NodeCnt}, isuniform: {viewData.IsUniform}, func: {viewData.fRawEnum}");
            //    viewData.make_raw_data();
            //    list_box_raw.ItemsSource = viewData.raw_values;
            //    list_box_raw.Items.Refresh();
            //    text_box_left_int_b.Text = viewData.a.ToString();
            //    text_box_right_int_b.Text = viewData.b.ToString();
            //    text_box_node_count_raw.Text = viewData.NodeCnt.ToString();
            //    MessageBox.Show($"spline_node_cnt: {viewData.SplineNodeCnt}, left: {viewData.SplineInfo[0]}, right: {viewData.SplineInfo[1]}");
            //    viewData.make_spline_data();
            //    text_box_node_count_spline.Text = viewData.SplineNodeCnt.ToString();
            //    bool tmp = viewData.splineData.CalculateInerpolation();
            //    MessageBox.Show(tmp.ToString());
            //    list_box_spline.ItemsSource = viewData.splineData.Data.ToArray();
            //    text_block_integral.Text = "integral: " + viewData.splineData.IntegralValue.ToString("0.00");
            //}

            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Couldnt load from controls because of {ex}");
            //}
        }

        private void button_save_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new()
            {
                FileName = "Document", // Default file name
                DefaultExt = ".txt" // Default file extension
            };
            // Show save file dialog box
            bool? result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;
                viewData.Save(filename);
            }

        }

        private void menu_item_save_click(object sender, RoutedEventArgs e)
        {
            button_save.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }

        private void button_raw_data_from_file_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new()
            {
               
            };


            // Display OpenFileDialog by calling ShowDialog method 
            bool? result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                MessageBox.Show($"Loading from {filename}");
                viewData.make_raw_data_from_file(filename);
                list_box_raw.ItemsSource = viewData.raw_values;
                list_box_raw.Items.Refresh();
                text_box_left_int_b.Text = viewData.a.ToString();
                text_box_right_int_b.Text = viewData.b.ToString();
                text_box_node_count_raw.Text = viewData.NodeCnt.ToString();
                viewData.make_spline_data();
                text_box_node_count_spline.Text = viewData.SplineNodeCnt.ToString();
                bool tmp = viewData.splineData.CalculateInerpolation();
               // MessageBox.Show(tmp.ToString());
                list_box_spline.ItemsSource = viewData.splineData.Data.ToArray();
                text_block_integral.Text = "integral: " + viewData.splineData.IntegralValue.ToString("0.00");
            }
        }

        private void menu_item_raw_data_from_file_click(object sender, RoutedEventArgs e)
        {
            button_raw_data_from_file.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }

        private void lb_change(object sender, SelectionChangedEventArgs e)
        {
            if (list_box_spline.SelectedItem == null)
            {
                return;
            }
            SplineDataItem it = (SplineDataItem)list_box_spline.SelectedItem;
            text_block_chosen_item.Text = it.ToString();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void text_box_left_int_b_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
