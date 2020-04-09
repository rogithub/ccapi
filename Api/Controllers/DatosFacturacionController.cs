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
    public class DatosFacturacionController : RepoController<Entities.DatosFacturacion, Models.DatosFacturacion>
    {
        public DatosFacturacionController
        (
            ILogger logger,
            IBaseRepo<DatosFacturacion> repo,
            IMapper mapper,
            LinkGenerator linkGen) : base(logger, repo, mapper, linkGen)
        {
        }
    }
}
