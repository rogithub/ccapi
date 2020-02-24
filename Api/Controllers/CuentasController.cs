using AutoMapper;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Repositories;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CuentasController : RepoController<Entities.Cuenta, Models.Cuenta>
    {
        public CuentasController
        (
            IBaseRepo<Cuenta> repo,
            IMapper mapper,
            LinkGenerator linkGen) : base(repo, mapper, linkGen)
        {
        }
    }
}
