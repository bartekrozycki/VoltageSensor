using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using VoltageSensor.Models.VoltageSensor;
using VoltageSensor.Services;

namespace VoltageSensor.Controllers
{
    [Route("")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        private readonly SensorService _sensor;

        public SensorController(SensorService sensor)
        {
            _sensor = sensor;
        }

        [HttpGet]
        public ActionResult<Sensor> Get()
        {
            return _sensor.GetRecent();
        }
        [Route("/all")]
        [HttpGet]
        public ActionResult<List<Sensor>> GetA()
        {
            return _sensor.Get();
        }
    }
}
