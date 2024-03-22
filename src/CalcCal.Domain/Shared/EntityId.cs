using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CalcCal.Domain.Shared;

// TODO Fix strong typed ID in mongo
public abstract record EntityId
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; init; }

    protected EntityId(){}

    protected EntityId(Guid id) => Id = id.ToString();

    protected EntityId(string id) : this(Guid.Parse(id)){}
}