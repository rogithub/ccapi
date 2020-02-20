using System;
using Entities;

namespace Repositories
{
    public interface IClientesRepo
    {
        IObservable<Cliente> Get(long id);
    }
}
