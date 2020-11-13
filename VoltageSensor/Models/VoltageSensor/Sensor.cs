
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace VoltageSensor.Models.VoltageSensor
{
    public class Sensor
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime TimeStamp { get; set; }
        public double CurrentVoltage { get; set; }

        public bool error { get; set; }

    }
}
