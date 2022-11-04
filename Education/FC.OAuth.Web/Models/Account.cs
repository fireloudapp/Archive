﻿using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FC.Admin.Services.Models;

/// <summary>
/// Account is also called as Organization
/// </summary>
public class Account
{
    #region  Basic Account Details
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

    public string BusinessType { get; set; }
    public string Description { get; set; }
    public string Email { get; set; }
    #endregion

    #region Additional Details

    [StringLength(20, ErrorMessage = "GSTIN length can't be more than 20.")]
    public string GSTIN { get; set; }
    public string BusinessCategory { get; set; }
    
    public string Logo { get; set; }
    public string WebSite { get; set; }

    #endregion
    
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

public class ClientAccount
{
    #region  Basic Account Details
    /// <summary>
    /// A Unique Id to get account details.
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; } = string.Empty;
    [Required]
    [StringLength(150, ErrorMessage = "Name length can't be more than 150.")]
    public string BusinessName { get; set; }
    
    /// <summary>
    /// This indicates whether the account is active or not, activation/deactivation to
    /// be done manually.
    /// </summary>
    public bool IsActive { get; set; }

    public string BusinessType { get; set; }
    public string Description { get; set; }
    public string Email { get; set; }
    [StringLength(15, ErrorMessage = "Phone length can't be more than 15.")]
    public string Phone { get; set; }
    
    /// <summary>
    /// Auto generated by using Business Name
    /// </summary>
    public string DBName { get; set; }
    #endregion
}

public class ClientBilling
{
    // <summary>
    /// A Unique Id to get client billing details.
    ///</summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; } = string.Empty;
    
    /// <summary>
    /// ClientAccount mapped by this field.
    /// </summary>
    public string ClientAccountId { get; set; }
    
    /// <summary>
    /// Cost value indicates what is the primary record  to calculate the bill.
    /// Eg. for School, No of students, 1 Student cost = Rs.5, so 5 is the CostValue.
    /// </summary>
    public float CostValue { get; set; }
    
    /// <summary>
    /// Field used to calculate cost 
    /// </summary>
    public string CostField { get; set; }
    
    /// <summary>
    /// Bill Generated Date
    /// </summary>
    public DateTime BillGeneratedDate { get; set; }
    /// <summary>
    /// Is the bill payed for the given month.
    /// </summary>
    public bool IsBillPayed { get; set; }
}