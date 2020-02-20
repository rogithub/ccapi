using System;
using Entities;

namespace Repositories
{
    public interface IMaterialesRepo
    {
        IObservable<Material> Get(Guid id);
        IObservable<Material> GetAll();
        IObservable<int> Delete(Guid id);
        IObservable<int> Update(Material cliente);
        IObservable<int> Save(Material cliente);
    }
}
