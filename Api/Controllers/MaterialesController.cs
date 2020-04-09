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
    public class MaterialesController : RepoController<Entities.Material, Models.Material>
    {
        public MaterialesController
        (
            ILogger logger,
            IBaseRepo<Material> repo,
            IMapper mapper,
            LinkGenerator linkGen) : base(logger, repo, mapper, linkGen)
        {
        }
    }
}
