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
    public class CuentasController : RepoController<Entities.Cuenta, Models.Cuenta>
    {
        public CuentasController
        (
            ILogger logger,
            IBaseRepo<Cuenta> repo,
            IMapper mapper,
            LinkGenerator linkGen) : base(logger, repo, mapper, linkGen)
        {
        }
    }
}
