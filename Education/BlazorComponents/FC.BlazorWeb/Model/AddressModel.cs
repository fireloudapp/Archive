using System.ComponentModel.DataAnnotations;

namespace FC.BlazorWeb.Model;

/// <summary>
/// Address Model
/// </summary>
public class AddressModel
{
    [Required(ErrorMessage = "Please enter 1st line of address.")]
    [MaxLength (300, ErrorMessage = "Line 1, cannot exceed more than 300 Lines")]
    public string? AddressLine1 { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter 2st line of address.")]
    [MaxLength (300, ErrorMessage = "Line 2, cannot exceed more than 300 Lines")]
    public string? AddressLine2 { get; set; }

    public Country? SelectedCountry { get; set; } = new Country()
    {
        id = 0, Name = string.Empty
    };

    public State? SelectedState { get; set; } = new State()
    {
        id = 0, Name = string.Empty
    };

    public City? SelectedCity { get; set; } = new City()
    {
        id = 0, Name = string.Empty
    };
    public string? PinCode { get; set; }
    public string? MobileNumber { get; set; }

    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    public string? EmailId { get; set; }

}