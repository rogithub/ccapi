using AutoMapper;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Repositories;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProveedoresController : RepoController<Entities.Proveedor, Models.Proveedor>
    {
        public ProveedoresController
        (
            IBaseRepo<Proveedor> repo,
            IMapper mapper,
            LinkGenerator linkGen) : base(repo, mapper, linkGen)
        {
        }
    }
}
