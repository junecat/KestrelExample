using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KestrelExample.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class MyController : ControllerBase {
        static readonly string[] WeekDays = new[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday", };

        private readonly ILogger<MyController> _logger;

        public MyController(ILogger<MyController> logger) {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get() {
            var rng = new Random();
            return Enumerable.Range(1, 7).Select(index => new string( WeekDays[rng.Next(0, 6)]))
            .ToArray();
        }

    }
}
