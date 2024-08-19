namespace api.tags.Dto
{
    public class FilterBaseDto
    {
        public int CurrentePage { get; set; }
        public int PageSize { get; set; }
        public List<ParametersDto> Parameters {get;set;}
        public List<MultiSortDto> MultiSort { get; set;}
    }
    public class ParametersDto
    {
        public string FilterCondition { get; set; }
        public string FilterDataField { get; set; }
        public string FilterValue { get; set; }
    }
    public class MultiSortDto
    {
        public string Field { get; set; }
        public string Type { get; set; }
    }
}
