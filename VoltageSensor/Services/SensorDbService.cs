using MongoDB.Driver;
using System.Collections.Generic;
using VoltageSensor.Models.VoltageSensor;

namespace VoltageSensor.Services
{
    public class SensorDbService
    {
        private readonly IMongoCollection<Sensor> _sensor;

        public SensorDbService(ISensorDatabaseSettings settings)
        {
            MongoClient _client = new MongoClient(settings.ConnectionString);

            var database = _client.GetDatabase(settings.DatabaseName);

            _sensor = database.GetCollection<Sensor>(settings.SensorCollectionName);
        }
        public List<Sensor> Get()
            => _sensor.Find(bson => true).ToList();
        public Sensor Get(string Id)
            => _sensor.Find(bson => bson.Id == Id).FirstOrDefault();
        public Sensor GetRecent()
            => _sensor.Find(bson => true).SortByDescending(bson => bson.TimeStamp).FirstOrDefault();
        public List<Sensor> GetRecent(int count)
            => _sensor.Find(bson => bson.error == false).SortByDescending(bson => bson.TimeStamp).Limit(count).ToList();
        public Sensor Create(Sensor entry)
        {
            _sensor.InsertOne(entry);
            return entry;
        }

        public void Update(string Id, Sensor entry)
            => _sensor.ReplaceOne(bson => bson.Id == Id, entry);

        public void Remove(Sensor entry)
            => _sensor.DeleteOne(bson => bson.Id == entry.Id);
        public void Remove(string Id)
            => _sensor.DeleteOne(bson => bson.Id == Id);
    }
}
