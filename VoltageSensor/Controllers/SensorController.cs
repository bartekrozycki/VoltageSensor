using MathNet.Numerics;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using VoltageSensor.Models.VoltageSensor;
using VoltageSensor.Services;

namespace VoltageSensor.Controllers
{
    [ApiController]
    public class SensorController : ControllerBase
    {
        private readonly SensorDbService _sensor;

        public SensorController(SensorDbService sensor)
        {
            _sensor = sensor;
        }

        [HttpGet]
        [Route("/recent")]
        public ActionResult<Sensor> Get()
        {
            return _sensor.GetRecent();
        }
        [HttpGet]
        [Route("/predict/{seconds:int=0}")]
        [Route("/predict/{minutes:int=0}/{seconds:int=0}")]
        [Route("/predict/{hours:int=0}/{minutes:int=0}/{seconds:int=0}")]
        public ActionResult<double> PredictGet(int seconds, int minutes, int hours)
        {
            List<Sensor> list = _sensor.GetRecent(100);
            DateTime predictTime = DateTime.Now;
            predictTime.AddSeconds(seconds);
            predictTime.AddMinutes(minutes);
            predictTime.AddHours(hours);

            Models.SensorMath m = new Models.SensorMath();
            double prob = m.regression(list, predictTime);

            return prob;
        }

        [HttpGet]
        [Route("/random/{x}")]
        public ActionResult<List<Sensor>> generateRandom(int x)                                      
        {
            List<Sensor> data_list = new List<Sensor>();

            Random rand = new Random();
            
            for (int i = 0; i < x; ++i)
            {
                Sensor randomDoc = new Sensor();
                randomDoc.Id = ObjectId.GenerateNewId().ToString();
                randomDoc.CurrentVoltage = rand.NextDouble()*3 + 2;
                randomDoc.error = rand.Next(0, 100) < 70 ? true : false;
                randomDoc.TimeStamp = DateTime.Now.AddSeconds(i);

                _sensor.Create(randomDoc);
                data_list.Add(randomDoc);
            }
            return data_list;
        }


    }
}
