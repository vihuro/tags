using System.Text.Json.Serialization;

namespace api.tags.Dto
{
    public class WipTagDto
    {
        [JsonPropertyName("regionRFID")]
        public string RegionRfid { get; set; }
        [JsonPropertyName("epccode")]
        public string EpcCode { get; set; }
        [JsonPropertyName("insertdate")]
        public DateTime InsertDate { get; set; }
        [JsonPropertyName("deletedate")]
        public DateTime DeleteDate { get; set; }
        [JsonPropertyName("lastupdate")]

        public DateTime LastUpdate { get; set; }
    }
}
