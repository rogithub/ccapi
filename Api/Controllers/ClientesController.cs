using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Entities;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repositories;
using AutoMapper;


namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Clientes : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private IClientesRepo _repo;
        private readonly IMapper _mapper;

        public Clientes(
            ILogger<WeatherForecastController> logger,
            IClientesRepo repo,
            IMapper mapper)
        {
            _logger = logger;
            _repo = repo;
            _mapper = mapper;

        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Models.Cliente>> Get(Guid id)
        {
            var entity = await _repo.Get(id).FirstOrDefaultAsync();
            return _mapper.Map<Models.Cliente>(entity);
        }
    }
}
