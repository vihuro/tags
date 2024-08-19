using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace api.tags.Model
{
    public class WipTagModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string InternalId { get; set; }
        public string RegionRfid { get; set; }
        public string EpcCode { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime DeleteDate { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
