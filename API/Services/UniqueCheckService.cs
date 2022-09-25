using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using API.Data;
using API.Entities.ProcessExecutor;
using API.Interfaces;

namespace API.Services
{
    public class UniqueCheckService : IUniqueCheckServiceInterface
    {
        private readonly ApplicationContext applicationContext;

        public UniqueCheckService(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public bool CheckUniqueParam<T>(Guid modelId, string paramName, string paramValue) where T : BaseEntity
        {
            return applicationContext.Set<T>().Where(paramName + "==@0", paramValue).Where(m => m.Id != modelId).Count() > 0;
        }
    }
}