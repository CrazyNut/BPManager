using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Interfaces
{
   public interface IRepository<T>
        where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id, bool IsLazy = true);
        void Create(T item);
        Task UpdateAsync(T item);
        void Update(T item);
        Task DeleteAsync(int id);
        void Delete(T entity);
        Task<List<T>> GetManyAsync(int[] entitiesId);
        void DeleteMany(List<T> entities);
    }
}