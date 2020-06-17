using System;
using System.Threading.Tasks;
using Entities;

namespace Repositories
{
    public interface IBaseRepo<T>
    {
        Task<T> Get(Guid id);
        Task<T> Get(Int64 id);
        Task<Resultset<T>> Search(SearchData entity);
        Task<int> Delete(Guid id);
        Task<int> Update(T entity);
        Task<int> Save(T entity);
    }
}
