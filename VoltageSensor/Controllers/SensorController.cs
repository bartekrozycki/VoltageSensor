using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using VoltageSensor.Models.VoltageSensor;
using VoltageSensor.Services;

namespace VoltageSensor.Controllers
{
    [Route("api/")]
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
        [HttpGet]
        [Route("/all")]
        public ActionResult<List<Sensor>> GetAll()
        {
            return _sensor.Get();
        }

    }
}
