using API.Entities;
using API.Interfaces;
using System.Data.Entity;
using System.Linq;

namespace API.Data.Repositories
{
    public class ProcessElementConnectionRepository : IRepository<ProcessElementConnectionEntity>
    {
        private readonly ApplicationContext _applicationContext;

        public ProcessElementConnectionRepository(
            ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public void Create(ProcessElementConnectionEntity processSample)
        {
            _applicationContext.ProcessConnections.Add(processSample);
        }

        public void Delete(ProcessElementConnectionEntity processSample)
        {
            _applicationContext.ProcessConnections.Remove(processSample);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _applicationContext.ProcessConnections.FindAsync(id);

            if (entity != null)
            {
                _applicationContext.ProcessConnections.Remove(entity);
            }
        }
        public void DeleteMany(List<ProcessElementConnectionEntity> entities)
        {
            _applicationContext.ProcessConnections.RemoveRange(entities);
        }

        public async Task<IEnumerable<ProcessElementConnectionEntity>> GetAllAsync()
        {
            return await _applicationContext.ProcessConnections
                .ToListAsync();
        }

        public async Task<ProcessElementConnectionEntity> GetAsync(int id, bool IsLazy = false)
        {
            return await _applicationContext.ProcessConnections.FindAsync(id);
        }

        public async Task<List<ProcessElementConnectionEntity>> GetManyAsync(int[] entitiesId)
        {
            return await _applicationContext.ProcessConnections
                .Where(t => entitiesId.Contains(t.Id))
                .ToListAsync();
        }

        public async Task UpdateAsync(ProcessElementConnectionEntity processSample)
        {
            var entity = await _applicationContext.ProcessConnections.FindAsync(processSample.Id);
            if (entity == null)
            {
                return;
            }

            _applicationContext.Entry(entity).CurrentValues.SetValues(processSample);
        }

        public void Update(ProcessElementConnectionEntity processSample)
        {
            _applicationContext.Update(processSample);
        }
    }
}
