using AutoMapper;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Repositories;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : RepoController<Entities.Cliente, Models.Cliente>
    {
        public ClientesController
        (
            IBaseRepo<Cliente> repo,
            IMapper mapper,
            LinkGenerator linkGen) : base(repo, mapper, linkGen)
        {
        }
    }
}
