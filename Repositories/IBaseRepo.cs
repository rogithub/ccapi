using System;
using Entities;

namespace Repositories
{
    public interface IBaseRepo<T>
    {
        IObservable<T> Get(Guid id);
        IObservable<T> Get(Int64 id);
        IObservable<Resultset<T>> GetAll(int limit, int offset, string search);
        IObservable<int> Delete(Guid id);
        IObservable<int> Update(T entity);
        IObservable<int> Save(T entity);
    }
}
