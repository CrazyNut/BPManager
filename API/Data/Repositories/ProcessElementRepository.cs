using API.Entities;
using API.Interfaces;
using System.Data.Entity;

namespace API.Data.Repositories
{
    public class ProcessElementRepository : IRepository<ProcessElementEntity>
    {
        private readonly ApplicationContext _applicationContext;

        public ProcessElementRepository(
            ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public void Create(ProcessElementEntity processSample)
        {
            _applicationContext.ProcessElements.Add(processSample);
        }

        public void Delete(ProcessElementEntity processSample)
        {
            _applicationContext.ProcessElements.Remove(processSample);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _applicationContext.ProcessElements.FindAsync(id);

            if (entity != null)
            {
                _applicationContext.ProcessElements.Remove(entity);
            }
        }
        public void DeleteMany(List<ProcessElementEntity> entities)
        {
            _applicationContext.ProcessElements.RemoveRange(entities);
        }

        public async Task<IEnumerable<ProcessElementEntity>> GetAllAsync()
        {
            return await _applicationContext.ProcessElements
                .ToListAsync();
        }

        public async Task<ProcessElementEntity> GetAsync(int id, bool IsLazy = false)
        {
            return await _applicationContext.ProcessElements.FindAsync(id);
        }

        public async Task<List<ProcessElementEntity>> GetManyAsync(int[] entitiesId)
        {
            return await _applicationContext.ProcessElements
                .Where(t => entitiesId.Contains(t.Id))
                .ToListAsync();
        }

        public async Task UpdateAsync(ProcessElementEntity processSample)
        {
            var entity = await _applicationContext.ProcessElements.SingleOrDefaultAsync(s => s.Id == processSample.Id);
            if (entity == null)
            {
                return;
            }

            _applicationContext.Entry(entity).CurrentValues.SetValues(processSample);
            entity.ProcessElementParams = new List<ProcessParamEntity>();
            foreach (var item in processSample.ProcessElementParams)
            {
                entity.ProcessElementParams.Add(item);
            }
        }

        public void Update(ProcessElementEntity processSample)
        {
            _applicationContext.Update(processSample);
        }
    }
}
