using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
    public class ProcessRepository : IRepository<ProcessEntity>
    {
        private readonly ApplicationContext _applicationContext;

        public ProcessRepository(
            ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public void Create(ProcessEntity processSample)
        {
            _applicationContext.Processes.Add(processSample);
        }

        public void Delete(ProcessEntity processSample)
        {
            _applicationContext.Processes.Remove(processSample);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _applicationContext.Processes.FindAsync(id);

            if (entity != null)
            {
                _applicationContext.Processes.Remove(entity);
            }
        }
        public void DeleteMany(List<ProcessEntity> entities)
        {
            _applicationContext.Processes.RemoveRange(entities);
        }

        public async Task<IEnumerable<ProcessEntity>> GetAllAsync()
        {
            return await _applicationContext.Processes
                .ToListAsync();
        }

        public async Task<ProcessEntity> GetAsync(int id, bool IsLazy = true)
        {
            var query = _applicationContext.Processes
                .Where(p => p.Id == id);
            
            if(!IsLazy)
            {
                query = query
                        .AsNoTracking()
                        .Include(process => process.ProcessParams)
                        .Include(process => process.ProcessElementsConnections)
                        .Include(process => process.ProcessElements)
                        .ThenInclude(element => element.ProcessElementParams);
            }
                
            return await query.FirstAsync();
        }

        public async Task<List<ProcessEntity>> GetManyAsync(int[] entitiesId)
        {
            return await _applicationContext.Processes
                .Where(t => entitiesId.Contains(t.Id))
                .ToListAsync();
        }

        public async Task UpdateAsync(ProcessEntity processSample)
        {
            var entity = await _applicationContext.Processes.FindAsync(processSample.Id);
            if (entity == null)
            {
                return;
            }

            _applicationContext.Entry(entity).CurrentValues.SetValues(processSample);
            entity.ProcessParams = new List<ProcessParamEntity>();
            foreach (var item in processSample.ProcessParams)
            {
                entity.ProcessParams.Add(item);
            }
        }

        public void Update(ProcessEntity processSample)
        {
            _applicationContext.Update(processSample);
        }
    }
}