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

        [HttpGet("{id:int}")]
        public IEnumerable<Material> Get(int id)
        {
            return _repo.Get(id).ToEnumerable();
        }

        [Route("search/{limit:int}/{offset:int}/{search?}")]
        [HttpGet()]
        public IEnumerable<Resultset<Material>> Search(int limit, int offset, string search)
        {
            return _repo.GetAll(limit, offset, search).ToEnumerable();
        }
    }
}
