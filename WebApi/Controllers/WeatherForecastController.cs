using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NServiceBus;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] _summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMessageSession _messageSession;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
                                         IMessageSession messageSession)
        {
            _logger = logger;
            _messageSession = messageSession;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> GetAsync()
        {
            var rng = new Random();
            await _messageSession.Publish(new TestMessage
            {
                Id = Guid.NewGuid(),
                Name = "Dummy"
            });


            return Enumerable.Range(1, 15)
                            .Select(index => new WeatherForecast
                            {
                                Date = DateTime.Now.AddDays(index),
                                TemperatureC = rng.Next(-20, 55),
                                Summary = _summaries[rng.Next(_summaries.Length)]
                            })
            .ToArray();
        }
    }
}
