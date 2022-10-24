using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IEntityService<T>
        where T : class
    {
        Task<T> AddAsync(T item);
        Task<List<T>> AddManyAsync(List<T> items);
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<string> DeleteAsync(int id);
        Task<T> UpdateAsync(T item);
        Task<string> DeleteManyAsync(int[] entitiesId);
    }
}