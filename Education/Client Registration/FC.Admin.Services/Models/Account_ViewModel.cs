using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FC.Admin.Services.Models;

public class Account_ViewModel : Account
{
    public string AddressLine_1
    {
        get;
        set;
    }
    
    public int AccountId
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
    public string PostalCode
    {
        get;
        set;
    }
}