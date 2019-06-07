using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OnlineEnterprice.Domain.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Weight { get; set; }
        public int Ammount { get; set; }
        public decimal Price { get; set; }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Category { get; set; }
    }
}
