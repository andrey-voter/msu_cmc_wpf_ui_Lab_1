using OxyPlot;
using OxyPlot.Legends;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using сlass_library_lab1;

namespace сlass_library_lab1
{
    public class MyPlotModel
    {
        public PlotModel plotModel { get; private set; }
        SplineData data;
        RawData rawData;
        public MyPlotModel(SplineData data, RawData rawData)
        {
            this.data = data;
            this.rawData = rawData;
            this.plotModel = new PlotModel 
            { 
                Title = "Spline Graphic" 
            };
            AddSeries();
        }
        public void AddSeries()
        {
            this.plotModel.Series.Clear();
            Legend legend = new Legend();
            LineSeries lineSeries = new LineSeries();
            for (int i = 0; i < data.NodeCnt; i++)
            {
                OxyColor color = (i == 0) ? OxyColors.Green : OxyColors.Blue;
                lineSeries.Points.Add(new DataPoint(data.Data[i].coord, data.Data[i].SplineValue));
                lineSeries.Color = color;
                lineSeries.Title = "Spline interpolation";
            }

            plotModel.Legends.Add(legend);
            this.plotModel.Series.Add(lineSeries);

            Legend legend_rd = new Legend();
            LineSeries lineSeries_rd = new LineSeries();
            for (int i = 0; i < rawData.NodeCount; i++)
            {
                OxyColor color = (i == 0) ? OxyColors.Red : OxyColors.Black;
                lineSeries_rd.Points.Add(new DataPoint(rawData.nodes[i], rawData.values[i]));
                lineSeries_rd.Color = color;

                lineSeries_rd.MarkerType = MarkerType.Circle;
                lineSeries_rd.MarkerSize = 4;
                lineSeries_rd.MarkerStroke = color;
                lineSeries_rd.MarkerFill = color;
                lineSeries_rd.Title = "Function to interpolate";
            }
            plotModel.Legends.Add(legend_rd);
            plotModel.Series.Add(lineSeries_rd);
        }
    }
}
