using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FC.Admin.Services.Entities;

using System.Text.Json.Serialization;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone
    {
        get;
        set;
    }

    public string Email
    {
        get;
        set;
    }

    public string PasswordHash
    {
        get;
        set;
    }

    public bool IsActive
    {
        get;
        set;
    } = true;


    public string Role
    {
        get;
        set;
    }

    public string Token
    {
        get;
        set;
    }

    public string LoginIp
    {
        get;
        set;
    }

    public string ClientDBName
    {
        get;
        set;
    }

    public int StaffId
    {
        get;
        set;
    }

    public DateTime LastLogin
    {
        get;
        set;
    }

    public int AccountId
    {
        get;
        set;
    }

    public string Username { get; set; }
    [JsonIgnore]
    public string Password { get; set; }
    
}
//Ref: C:\Works\_FireCloudApps\FC.Web.Clients
public class ClientAccount
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public int Id { get; set; }
    [Required]
    [StringLength(150, ErrorMessage = "Name length can't be more than 150.")]
    public string BusinessName { get; set; }
    public bool IsActive { get; set; }
    [StringLength(15, ErrorMessage = "Phone length can't be more than 15.")]
    public string Phone { get; set; }
    [StringLength(20, ErrorMessage = "GSTIN length can't be more than 20.")]
    public string GSTIN { get; set; }
    public string BusinessType { get; set; }
    public string Description { get; set; }
    public string BusinessCategory { get; set; }
    //public int AddressId { get; set; } // Moved to Address Table
    public string Email { get; set; }
    public string Logo { get; set; }
    public string WebSite { get; set; }
    [Required]
    public int ActivateNoOfDays { get; set; }
    /// <summary>
    /// Should be updated or assigned the date based on 'IsActive' state;
    /// </summary>
    public DateTime ActivationDate
    {
        get;
        set;
    }
}