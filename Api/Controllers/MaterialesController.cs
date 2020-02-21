using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Routing;
using Repositories;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MaterialesController : ControllerBase
    {
        private readonly ILogger<MaterialesController> _logger;
        private readonly IMaterialesRepo _repo;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGen;


        public MaterialesController(
            ILogger<MaterialesController> logger,
            IMaterialesRepo repo,
            IMapper mapper,
            LinkGenerator linkGen)
        {
            _logger = logger;
            _repo = repo;
            _mapper = mapper;
            _linkGen = linkGen;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Models.Material>> Get(Guid id)
        {
            var entity = await _repo.Get(id).FirstOrDefaultAsync();
            if (entity == null) return NotFound();

            return _mapper.Map<Models.Material>(entity);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Models.Material>> Get(int id)
        {
            var entity = await _repo.Get(id).FirstOrDefaultAsync();
            if (entity == null) return NotFound();

            return _mapper.Map<Models.Material>(entity);
        }

        [Route("search/{limit:int}/{offset:int}/{search?}")]
        [HttpGet()]
        public async Task<ActionResult<Resultset<IEnumerable<Models.Material>>>> Search(int limit, int offset, string search)
        {
            var rs = _repo.GetAll(limit, offset, search).ToAsyncEnumerable();
            var list = new List<Models.Material>();
            Int64 rowCount = 0;

            await foreach (var item in rs)
            {
                list.Add(_mapper.Map<Models.Material>(item.Payload));
                rowCount = item.TotalRows;
            }

            var result = new Resultset<IEnumerable<Models.Material>>(rowCount, list);

            return Ok(result);
        }

        [HttpPost()]
        public async Task<ActionResult<Models.Material>> Post(Models.Material model)
        {
            var item = _mapper.Map<Models.Material, Entities.Material>(model);
            var affectedRows = await _repo.Save(item);
            if (affectedRows > 0)
            {
                var location = _linkGen.GetPathByAction("Get", "Materiales", new { model.Id });
                return Created(location, _mapper.Map<Models.Material>(item));
            }

            return BadRequest();
        }
    }
}
