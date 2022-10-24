using API.Data;
using API.Entities;
using API.Interfaces;

namespace API.UnitsOfWork
{
    public class AppUnitOfWork
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IRepository<ProcessEntity> _processRepository;
        private readonly IRepository<ProcessElementEntity> _processElementRepository;
        private readonly IRepository<ProcessElementConnectionEntity> _processElementConnectionRepository;

        public AppUnitOfWork(
            ApplicationContext applicationContext,
            IRepository<ProcessEntity> processRepository,
            IRepository<ProcessElementEntity> processElementRepository,
            IRepository<ProcessElementConnectionEntity> processElementConnectionRepository)
        {
            _applicationContext = applicationContext;
            _processRepository = processRepository;
            _processElementRepository = processElementRepository;
            _processElementConnectionRepository = processElementConnectionRepository;
        }

        public IRepository<ProcessEntity> ProcessRepository { get => _processRepository; }
        public IRepository<ProcessElementEntity> ProcessElementRepository { get => _processElementRepository; }
        public IRepository<ProcessElementConnectionEntity> ProcessElementConnectionRepository { get => _processElementConnectionRepository; }
        public ApplicationContext ApplicationContext { get => _applicationContext; }
        public async Task SaveAsync()
        {
            await _applicationContext.SaveChangesAsync();
        }
    }
}
