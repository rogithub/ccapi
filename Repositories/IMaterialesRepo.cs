using System;
using Entities;

namespace Repositories
{
    public interface IMaterialesRepo
    {
        IObservable<Material> Get(Guid id);
        IObservable<Material> IObservable<Material> GetAll(int limit, int offset);
        IObservable<int> Delete(Guid id);
        IObservable<int> Update(Material cliente);
        IObservable<int> Save(Material cliente);
    }
}
