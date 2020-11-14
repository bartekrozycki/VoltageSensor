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

            double[] poly = Fit.Polynomial(x, y, 4);
            double myPredict = 0;
            double p = predictTime.Ticks;

            for (i = 0; i < poly.Length; ++i)
                myPredict += (Math.Pow(p, i) * poly[i]);

            return myPredict;
        }
    }
}
