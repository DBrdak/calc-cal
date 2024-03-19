using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CalcCal.Domain.Shared
{
    public abstract record EntityId
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public Guid Id { get; init; }

        protected EntityId(){}

        protected EntityId(Guid id) => Id = id;

        protected EntityId(string id) : this(Guid.Parse(id)){}
    }
}
