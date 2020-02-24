using AutoMapper;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Repositories;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DatosFacturacionController : RepoController<Entities.DatosFacturacion, Models.DatosFacturacion>
    {
        public DatosFacturacionController
        (
            IBaseRepo<DatosFacturacion> repo,
            IMapper mapper,
            LinkGenerator linkGen) : base(repo, mapper, linkGen)
        {
        }
    }
}
