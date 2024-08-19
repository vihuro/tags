using System.Text.Json.Serialization;

namespace api.tags.Dto
{
    public class ResponseCustomerDto<T>
    {
        [JsonPropertyName("data")]
        public ReponseCustomerDataDto<T> Data { get; set; }
    }
    public class ReponseCustomerDataDto<T>
    {
        [JsonPropertyName("items")]
        public List<T> Items { get; set; }
        [JsonPropertyName("number")]

        public int Number { get; set; }
        [JsonPropertyName("paginationInfo")]

        public PaginationInfoDto PaginationInfo { get; set; }
    }
    public class PaginationInfoDto
    {
        [JsonPropertyName("items")]
        public long Items { get; set; }
        [JsonPropertyName("limitByPage")]
        public int LimitByPage { get; set; }
        [JsonPropertyName("actual")]

        public long Actual { get; set; }

    }
}
