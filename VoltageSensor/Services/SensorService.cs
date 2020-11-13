using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using VoltageSensor.Models.VoltageSensor;

namespace VoltageSensor.Services
{
    public class SensorService
    {
        private readonly IMongoCollection<Sensor> _sensor;

        public SensorService(ISensorDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _sensor = database.GetCollection<Sensor>(settings.SensorCollectionName);
        }

        public List<Sensor> Get()
            => _sensor.Find(doc => true).ToList();
        public Sensor Get(string Id)
            => _sensor.Find(doc => doc.Id == Id).FirstOrDefault();
        public Sensor GetRecent()
            => _sensor.Find(doc => true).Sort(new BsonDocument("_id", -1)).FirstOrDefault();
        public Sensor Create(Sensor entry)
        {
            _sensor.InsertOne(entry);
            return entry;
        }

        public void Update(string Id, Sensor entry) 
            => _sensor.ReplaceOne(doc => doc.Id == Id, entry);

        public void Remove(Sensor entry)
            => _sensor.DeleteOne(doc => doc.Id == entry.Id);
        public void Remove(string Id)
            => _sensor.DeleteOne(doc => doc.Id == Id);
    }
}
