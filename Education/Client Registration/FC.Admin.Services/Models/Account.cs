using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FC.Admin.Services.Models;

/// <summary>
/// Account is also called as Organization
/// </summary>
public class Account
{
    /// <summary>
    /// A Unique Id to get account details.
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; } = string.Empty;
    
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
    public string DBName { get; set; }
    public string ClientKey { get; set; }
    public string ClientConnectionString { get; set; }
}