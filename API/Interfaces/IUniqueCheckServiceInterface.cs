using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities.ProcessExecutor;

namespace API.Interfaces
{
    public interface IUniqueCheckServiceInterface
    {
        public bool CheckUniqueParam<T>(Guid modelId, string paramName, string paramValue) where T : BaseEntity;
    }
}