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
    public class Clientes : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private IClientesRepo _repo;
        public Clientes(
            ILogger<WeatherForecastController> logger,
            IClientesRepo repo)
        {
            _logger = logger;
            _repo = repo;

        }

        [HttpGet("{id:guid}")]
        public IEnumerable<Cliente> Get(Guid id)
        {
            return _repo.Get(id).ToEnumerable();
        }
    }
}
