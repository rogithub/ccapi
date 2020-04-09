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
    public class ClientesController : RepoController<Entities.Cliente, Models.Cliente>
    {
        public ClientesController
        (
            ILogger logger,
            IBaseRepo<Cliente> repo,
            IMapper mapper,
            LinkGenerator linkGen) : base(logger, repo, mapper, linkGen)
        {
        }
    }
}
