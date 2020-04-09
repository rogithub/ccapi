using System;
using Entities;

namespace Repositories
{
    public interface ISubtableBaseRepo<T>
    {
        IObservable<T> Get(Guid id);
        IObservable<T> Get(Int64 id);
        IObservable<Resultset<T>> Search(SearchData entity, Guid parent);
        IObservable<int> Delete(Guid id);
        IObservable<int> Update(T entity);
        IObservable<int> Save(T entity);
    }
}
