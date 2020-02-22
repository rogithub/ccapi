using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Repositories;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class RepoController<TEntity, TModel>
    : ControllerBase
    where TEntity : I2ids
    where TModel : I2ids
    {
        protected readonly IBaseRepo<TEntity> _repo;
        protected readonly IMapper _mapper;
        protected readonly LinkGenerator _linkGen;

        public RepoController(
            IBaseRepo<TEntity> repo,
            IMapper mapper,
            LinkGenerator linkGen)
        {
            _repo = repo;
            _mapper = mapper;
            _linkGen = linkGen;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TModel>> Get(Guid id)
        {
            var entity = await _repo.Get(id).FirstOrDefaultAsync();
            if (entity == null) return NotFound();

            return _mapper.Map<TModel>(entity);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TModel>> Get(int id)
        {
            var entity = await _repo.Get(id).FirstOrDefaultAsync();
            if (entity == null) return NotFound();

            return _mapper.Map<TModel>(entity);
        }

        [Route("search/{limit:int}/{offset:int}/{search?}")]
        [HttpGet()]
        public async Task<ActionResult<Resultset<IEnumerable<TModel>>>> Search(int limit, int offset, string search)
        {
            var rs = _repo.GetAll(limit, offset, search).ToAsyncEnumerable();
            var list = new List<TModel>();
            Int64 rowCount = 0;

            await foreach (var item in rs)
            {
                list.Add(_mapper.Map<TModel>(item.Payload));
                rowCount = item.TotalRows;
            }

            var result = new Resultset<IEnumerable<TModel>>(rowCount, list);

            return Ok(result);
        }

        [HttpPost()]
        public async Task<ActionResult<TModel>> Post(TModel model)
        {
            var entity = await _repo.Get(model.Guid).FirstOrDefaultAsync();
            if (entity != null) return BadRequest("Already exists!");
            entity.Id = 0;

            var item = _mapper.Map<TModel, TEntity>(model);
            var affectedRows = await _repo.Save(item);
            if (affectedRows > 0)
            {
                var location = _linkGen.GetPathByAction("Get", "Materiales", new { model.Id });
                return Created(location, _mapper.Map<TModel>(item));
            }

            return BadRequest();
        }

        [HttpPut()]
        public async Task<ActionResult<int>> Put(TModel model)
        {
            var entity = await _repo.Get(model.Guid).FirstOrDefaultAsync();
            if (entity == null) return NotFound();

            var item = _mapper.Map<TModel, TEntity>(model);
            var affectedRows = await _repo.Update(item);
            if (affectedRows > 0)
            {
                return affectedRows;
            }

            return BadRequest();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<int>> Delete(Guid id)
        {
            var item = await _repo.Get(id).FirstOrDefaultAsync();
            if (item == null) return NotFound();

            var affectedRows = await _repo.Delete(id);
            if (affectedRows > 0)
            {
                return affectedRows;
            }

            return BadRequest();
        }
    }
}
