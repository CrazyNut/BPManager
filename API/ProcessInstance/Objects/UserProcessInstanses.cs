namespace API.ProcessInstance.Objects
{
    public class UserProcessInstanses
    {
        private Dictionary<int, ProcessInstanceContext> _processInstanceStatuses = new();

        public void AddProcess(ProcessInstanceContext processInstanceStatus)
        {
            _processInstanceStatuses[processInstanceStatus.ProcessId] = processInstanceStatus;
        }
        public void RemoveProcess(int ProcessId)
        {
            if(_processInstanceStatuses.ContainsKey(ProcessId))
            {
                _processInstanceStatuses.Remove(ProcessId);
            }
        }
        public ProcessInstanceContext GetProcess(int ProcessId)
        {
            if(_processInstanceStatuses.ContainsKey(ProcessId))
            {
                return _processInstanceStatuses[ProcessId];
            }
            return null;
        }
    }
}
