using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repositories;
using AutoMapper;


namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly ILogger<ClientesController> _logger;
        private readonly IClientesRepo _repo;
        private readonly IMapper _mapper;

        public ClientesController(
            ILogger<ClientesController> logger,
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
            if (entity == null) return NotFound();

            return _mapper.Map<Models.Cliente>(entity);

        }
    }
}
