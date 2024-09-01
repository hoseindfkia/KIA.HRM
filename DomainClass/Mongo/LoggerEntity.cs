using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClass.Mongo
{
    public class LoggerEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string ExceptionMessage { get; set; } = null!;

        public string Device { get; set; } = null!;

        public long? UserId { get; set; }

        public string Ip { get; set; }
        public string Path { get; set; }
        public string Browser { get; set; }

        public string Platform { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}
