
namespace VoltageSensor.Models.VoltageSensor
{
    public class SensorDatabaseSettings : ISensorDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string SensorCollectionName { get; set; }
    }

}
