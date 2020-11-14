
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VoltageSensor.Models.VoltageSensor
{
    public class Sensor : RawSensor
    {
        public Sensor(RawSensor s = null)
        {
            if (s == null) return;

            TimeStamp = s.TimeStamp;
            CurrentVoltage = s.CurrentVoltage;
            error = s.error;
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }



    }
}
