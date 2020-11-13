namespace VoltageSensor.Models.VoltageSensor
{
    public interface ISensorDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string SensorCollectionName { get; set; }

    }

}
