namespace API.DTO
{
    public class ProcessTypeDTO
    {
        public string Name {get;set;}
        public string Type { get; set; }
        public string Icon { get; set; }
        public List<ProcessParamDTO> Params { get; set; }
    }
}
