using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace сlass_library_lab1
{
    public struct SplineDataItem
    {
        public double coord { get; set; }
        public double SplineValue { get; set; }
        public double Spline1 { get; set; }
        public double Spline2 { get; set; }

        public SplineDataItem(double coord, double SplineValue, double Spline1, double Spline2)
        {
            this.coord = coord;
            this.SplineValue = SplineValue;
            this.Spline1 = Spline1;
            this.Spline2 = Spline2;
        }

        public string ToString(string format)
        {
            return "Coord: " + coord.ToString(format) + '\n' + "SplineValue: " + SplineValue.ToString(format) +
                '\n' + "1 Derivative: " + Spline1.ToString(format) + '\n' + "2 Derivative: " + Spline2.
                ToString(format) + '\n';
        }

        public override string ToString()
        {
            return this.ToString("0.00");
        }

    }
}
