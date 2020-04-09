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
using Serilog;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class RepoController<TEntity, TModel>
    : ControllerBase
    where TEntity : I2ids
    where TModel : I2ids
    {
        protected ILogger Logger { get; }
        protected IBaseRepo<TEntity> Repo { get; }
        protected IMapper Mapper { get; }
        protected LinkGenerator LinkGen { get; }

        public RepoController(
            ILogger logger,
            IBaseRepo<TEntity> repo,
            IMapper mapper,
            LinkGenerator linkGen)
        {
            Logger = logger;
            Repo = repo;
            Mapper = mapper;
            LinkGen = linkGen;
        }

        [HttpGet("{id:guid}")]
        [Route("get/{id}")]
        public async Task<ActionResult<TModel>> Get(Guid id)
        {
            var entity = await Repo.Get(id).FirstOrDefaultAsync();
            if (entity == null) return NotFound();

            return Mapper.Map<TModel>(entity);
        }

        [HttpGet("{id:int}")]
        [Route("getFolio/{id}")]
        public async Task<ActionResult<TModel>> GetFolio(int id)
        {
            var entity = await Repo.Get(id).FirstOrDefaultAsync();
            if (entity == null) return NotFound();

            return Mapper.Map<TModel>(entity);
        }

        [Route("search")]
        [HttpPost()]
        public async Task<ActionResult<Resultset<IEnumerable<TModel>>>> Search
        (Models.SearchData model)
        {
            var entity = Mapper.Map<Entities.SearchData>(model);
            var rs = Repo.Search(entity).ToAsyncEnumerable();
            var list = new List<TModel>();
            Int64 rowCount = 0;

            await foreach (var item in rs)
            {
                list.Add(Mapper.Map<TModel>(item.Payload));
                rowCount = item.TotalRows;
            }

            var result = new Resultset<IEnumerable<TModel>>(rowCount, list);

            return Ok(result);
        }

        [HttpPost()]
        public async Task<ActionResult<TModel>> Post(TModel model)
        {
            var entity = await Repo.Get(model.Guid).FirstOrDefaultAsync();
            if (entity != null) return BadRequest("Already exists!");
            model.Id = 0;

            var item = Mapper.Map<TModel, TEntity>(model);
            var affectedRows = await Repo.Save(item);
            if (affectedRows > 0)
            {
                var location = LinkGen.GetPathByAction("Get", "Materiales", new { model.Id });
                return Created(location, Mapper.Map<TModel>(item));
            }

            return BadRequest();
        }

        [HttpPut()]
        public async Task<ActionResult<int>> Put(TModel model)
        {
            var entity = await Repo.Get(model.Guid).FirstOrDefaultAsync();
            if (entity == null) return NotFound();

            var item = Mapper.Map<TModel, TEntity>(model);
            var affectedRows = await Repo.Update(item);
            if (affectedRows > 0)
            {
                return affectedRows;
            }

            return BadRequest();
        }

        [HttpDelete("{id:guid}")]
        [Route("delete/{id}")]
        public async Task<ActionResult<int>> Delete(Guid id)
        {
            var item = await Repo.Get(id).FirstOrDefaultAsync();
            if (item == null) return NotFound();

            var affectedRows = await Repo.Delete(id);
            if (affectedRows > 0)
            {
                return affectedRows;
            }

            return BadRequest();
        }
    }
}
