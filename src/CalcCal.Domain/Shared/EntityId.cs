using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CalcCal.Domain.Shared;

public abstract record EntityId
{
    [BsonId]
    public string Id { get; init; }

    private EntityId(){}

    protected EntityId(Guid id) => Id = id.ToString();

    protected EntityId(string id) : this(Guid.Parse(id)){}
}