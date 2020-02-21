using System;
using Entities;

namespace Repositories
{
    public interface IMaterialesRepo
    {
        IObservable<Material> Get(Guid id);
        IObservable<Material> Get(Int64 id);
        IObservable<Resultset<Material>> GetAll(int limit, int offset, string search);
        IObservable<int> Delete(Guid id);
        IObservable<int> Update(Material cliente);
        IObservable<int> Save(Material cliente);
    }
}
