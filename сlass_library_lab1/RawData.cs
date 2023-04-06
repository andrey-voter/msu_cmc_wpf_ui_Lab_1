using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Windows;

namespace сlass_library_lab1
{
    public class RawData
    {
        public double a;
        public double b;
        public int NodeCount;
        public bool uniform = true;
        public FRaw? func;
        public double[]? nodes;
        public double[]? values;

        public RawData(double a, double b, int NodeCount, bool uniform, FRaw func)
        {
            if (NodeCount <= 0)
                throw new Exception("Empty data in RawData constructor");
            if (a > b)
                throw new Exception("Incorrect borders RawData constructor");
            this.a = a;
            this.b = b;
            this.NodeCount = NodeCount;
            this.uniform = uniform;
            this.func = func;
            nodes = new double[NodeCount];
            values = new double[NodeCount];
            if (NodeCount == 1)
            {
                nodes[0] = a;
                values[0] = func(a);
            }
            else if (NodeCount == 2)
            {
                nodes[0] = a;
                nodes[1] = b;
                values[0] = func(a);
                values[1] = func(b);
            }
            else
            {
                nodes[0] = a;
                nodes[NodeCount - 1] = b;
                values[0] = func(a);
                values[NodeCount - 1] = func(b);
                for (int i = 1; i < NodeCount - 1; ++i)
                {
                    nodes[i] = a + i * (b - a) / NodeCount;
                    values[i] = func(nodes[i]);
                }
            }
        }

        public RawData(string FileName)
        {
            FileStream? file = null;
            StreamReader? reader = null;
            try
            {
                file = File.OpenRead(FileName);
                reader = new StreamReader(file);
                double a = double.Parse(reader.ReadLine());
                double b = double.Parse(reader.ReadLine());
                int NodeCount = int.Parse(reader.ReadLine());
                if (NodeCount <= 0)
                    throw new Exception("Empty data in RawData constructor");

                this.a = a;
                this.b = b;
                this.NodeCount = NodeCount;
                nodes = new double[NodeCount];
                values = new double[NodeCount];
               
                reader.ReadLine();
                reader.ReadLine();
                for (int i = 0; i < NodeCount; i++)
                {
                    nodes[i] = double.Parse(reader.ReadLine());
                    values[i] = double.Parse(reader.ReadLine());
                    reader.ReadLine();
                }
            }
            //catch (Exception exc)
            //{
            //    Console.WriteLine($"The exception was catched:: {exc}");
            //}
            finally
            {
                if (reader != null) { reader.Dispose(); }
                if (file != null) { file.Close(); }
            }
        }

        public static double LinearFunc(double x)
        {
            return 2 * x + 3;
        }

        public static double CubeFunc(double x)
        {
            return Math.Pow(x, 3) + 2 * Math.Pow(x, 2) + 3 * x + 1;
        }

        public static double RandomFunc(double x)
        {
            Random rnd = new Random();
            return rnd.NextDouble() * 10;
        }

        public void Save(string FileName)
        {
            FileStream file = null;
            StreamWriter writer = null;
            try
            {
                file = File.Create(FileName);
                writer = new StreamWriter(file);
                writer.WriteLine(a);
                writer.WriteLine(b);
                writer.WriteLine(NodeCount);
                writer.WriteLine();
                writer.WriteLine();
                for (int i = 0; i < NodeCount; i++)
                {
                    writer.WriteLine(nodes[i].ToString());
                    writer.WriteLine(values[i].ToString());
                    writer.WriteLine();
                }
            }
           // catch (Exception exc)
           // {
            //    Console.WriteLine($"The exception was catched: {exc}");
           //}
            finally
            {
                if (writer != null) { writer.Dispose(); }
                if (file != null) { file.Close(); }
            }
        }
        public static void Load(string FileName, out RawData rawData)
        {
            rawData = new RawData(FileName);
            //FileStream? file = null;
            //StreamReader? reader = null;
            //try
            //{
            //    file = File.OpenRead(FileName);
            //    reader = new StreamReader(file);
            //    rawData = new RawData(FileName);
            //    rawData.a = double.Parse(reader.ReadLine());
            //    rawData.b = double.Parse(reader.ReadLine());
            //    rawData.NodeCount = int.Parse(reader.ReadLine());
            //    reader.ReadLine();
            //    reader.ReadLine();
            //    for (int i = 0; i < rawData.NodeCount; i++)
            //    {
            //        rawData.nodes[i] = double.Parse(reader.ReadLine());
            //        rawData.values[i] = double.Parse(reader.ReadLine());
            //        reader.ReadLine();
            //    }
            //}
            //catch (Exception exc)
            //{
            //    Console.WriteLine($"The exception was catched:: {exc}");
            //}
            //finally
            //{
            //    if (reader != null) { reader.Dispose(); }
            //    if (file != null) { file.Close(); }
            //}

        }
    }
        
}
