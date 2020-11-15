using MathNet.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VoltageSensor.Models.VoltageSensor;

namespace VoltageSensor.Models
{
    public class SensorMath
    {
        public double regression(List<Sensor> list, DateTime predictTime)
        {
            if (list == null) throw new ArgumentException();

            double[] x = new double[list.Count];
            double[] y = new double[list.Count];

            int i = 0;
            foreach (Sensor s in list)
            {
                x[i] = s.TimeStamp.Ticks;
                y[i] = s.CurrentVoltage;

                ++i;
            }

            Tuple<double, double> linear = Fit.Line(x, y);
            double p = predictTime.Ticks;

            double predict = linear.Item1 + (p * linear.Item2);

            return predict;
        }
    }
}
