
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace VoltageSensor.Models.VoltageSensor
{
    public class RawSensor
    {
        public DateTime TimeStamp { get; set; }
        public double CurrentVoltage { get; set; }
        public bool error { get; set; }
    }
    public class Sensor : RawSensor
    {
        public Sensor(RawSensor s)
        {
            TimeStamp = s.TimeStamp;
            CurrentVoltage = s.CurrentVoltage;
            error = s.error;
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

    }
}
