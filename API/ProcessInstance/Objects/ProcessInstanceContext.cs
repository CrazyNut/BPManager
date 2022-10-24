namespace API.ProcessInstance.Objects
{
    public class ProcessInstanceContext
    {
        public delegate void DestroyContext(int userId, int processId);
        public DestroyContext destroyContext;

        public string CurrentElementCode;
        public int CurrentElementStage;
        public int UserId;
        public int ProcessId;
        public List<ProcessParam> ProcessParamList;
        public Dictionary<string, ProcessParam> LastProcessElementsResults = new();
        public void DestroyThisContext()
        {
            if(destroyContext != null)
            {
                destroyContext(UserId, ProcessId);
            }
        }
    }
}
