using AutoMapper;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Repositories;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MaterialesController : RepoController<Entities.Material, Models.Material>
    {
        public MaterialesController
        (
            IBaseRepo<Material> repo,
            IMapper mapper,
            LinkGenerator linkGen) : base(repo, mapper, linkGen)
        {
        }
    }
}
