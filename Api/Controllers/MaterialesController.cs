using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repositories;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Materiales : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private IMaterialesRepo _repo;
        public Materiales(
            ILogger<WeatherForecastController> logger,
            IMaterialesRepo repo)
        {
            _logger = logger;
            _repo = repo;

        }

        [HttpGet("{id:guid}")]
        public IEnumerable<Material> Get(Guid id)
        {
            return _repo.Get(id).ToEnumerable();
        }

        [Route("all/{limit:int}/{offset:int}")]
        [HttpGet()]
        public IEnumerable<Material> GetAll(int limit, int offset)
        {
            return _repo.GetAll(limit, offset).ToEnumerable();
        }
    }
}
