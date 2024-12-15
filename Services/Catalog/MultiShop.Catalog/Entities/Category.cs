using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Diagnostics.CodeAnalysis;

namespace MultiShop.Catalog.Entities
{
    public class Category
    {
        //MongoDb'de id'ler string türünde tutulur.
        // ID olarak belirtmek için bsonId ve ObjectId verilir.

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        
        public string? CategoryID { get; set; }

        public string? CategoryName { get; set; }
        public string? ImageUrl { get; set; }
    }
}
