using System;
using Entities;

namespace Repositories
{
    public interface IClientesRepo
    {
        IObservable<Cliente> Get(Guid id);
        IObservable<Cliente> GetAll();
        IObservable<int> Delete(Guid id);
        IObservable<int> Update(Cliente cliente);
        IObservable<int> Save(Cliente cliente);
    }
}
