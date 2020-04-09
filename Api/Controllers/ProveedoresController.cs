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
    public class ProveedoresController : RepoController<Entities.Proveedor, Models.Proveedor>
    {
        public ProveedoresController
        (
            ILogger logger,
            IBaseRepo<Proveedor> repo,
            IMapper mapper,
            LinkGenerator linkGen) : base(logger, repo, mapper, linkGen)
        {
        }
    }
}
