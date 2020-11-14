using System;

namespace VoltageSensor.Models.VoltageSensor
{
    public class RawSensor
    {
        public DateTime TimeStamp { get; set; }
        public double CurrentVoltage { get; set; }
        public bool error { get; set; }
    }
}
