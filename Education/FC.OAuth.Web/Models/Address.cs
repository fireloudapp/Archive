using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FC.Admin.Services.Models;

public class Address
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id
    {
        get;
        set;
    }

    public string AccountId
    {
        get;
        set;
    }

    public string AddressLine_1
    {
        get;
        set;
    }

    public string AddressLine_2
    {
        get;
        set;
    }

    public int CountryId
    {
        get;
        set;
    }

    public int StateId
    {
        get;
        set;
    }

    public int CityId
    {
        get;
        set;
    }
    //[NpgsqlTypeMap(NpgsqlDbType.Json)]
    //public Country CountryJSON { get; set; }
}
public class Country
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string ShortName { get; set; }
}

public class State
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string ShortName { get; set; }
}

public class City
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PostalCode { get; set; }
}