using System.Net.Http.Json;
using BlazorDB;
using FC.BlazorWeb.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace FC.BlazorWeb.Components;

public partial class AddressSection
{
    #region Global Variables
    [Parameter]
    public string? CardTitle { get; set; }
    [Parameter]
    public EventCallback<string> CardTitleChanged { get; set; }

    EditForm addressForm;
    
    public bool CanSubmit()
    {
        return addressForm.EditContext.Validate();
    }

    [Parameter]
    public AddressModel GetAddressModel
    {
        get;
        set;
    }

    [Parameter] public EventCallback<AddressModel>? GetAddressChanged { get; set; }

    private Country[]? _countries;
    private Country _selectedCountry;
    private State[]? _states;
    private City[]? _cities;

    private string _domainUrl = "https://fireloudapp.github.io/location/";
    private string _countryUrl = "world/countries.json";
    private string _stateUrl = "world/states/{0}-{1}.json";
    private string _cityUrl = "world/cities/{0}-{1}.json";
    private string _countryIso3 = "IND";
    private string _countryId = "101";
    private string _stateCode = "TN";
    private string _stateId = "4035";
    
    


    #endregion

    #region Initialization
    
    protected override async Task OnInitializedAsync()
    {
        Console.WriteLine("Country Loaded...");
        _stateUrl = string.Format(_stateUrl, _countryIso3, _countryId);
        _countries =  await _http.GetFromJsonAsync<Country[]>(_domainUrl + _countryUrl);
        Console.WriteLine("Default Country India Loaded..");
        
        _cityUrl = string.Format(_cityUrl, _stateCode, _stateId);
        _states =  await _http.GetFromJsonAsync<State[]>(_domainUrl + _stateUrl);
        Console.WriteLine("Default State Loaded..");
       
    }
    #endregion

    private Task OnCardTitleChanged(ChangeEventArgs e)
    {
        CardTitle = e.Value?.ToString();
        return CardTitleChanged.InvokeAsync(CardTitle);
    }

    private Task OnGetAddressChanged(ChangeEventArgs e)
    {
        GetAddressModel = e.Value as AddressModel;
        return GetAddressChanged.Value.InvokeAsync(GetAddressModel);
        //return  GetAddressChanged .InvokeAsync(CardTitle);
    }

    private async Task SelectedCountry(Syncfusion.Blazor.DropDowns.ChangeEventArgs<string, Country> args)
    {
        _selectedCountry = args.ItemData;
        if(_selectedCountry == null) return;
        _stateUrl = string.Format(_stateUrl, _selectedCountry.iso3, _selectedCountry.id);
        Console.WriteLine($"State URL {_domainUrl}{_stateUrl}");
        _states =  await _http.GetFromJsonAsync<State[]>(_domainUrl + _stateUrl);
    }
    
    State _selectedState = null;
    private async Task SelectedState(Syncfusion.Blazor.DropDowns.ChangeEventArgs<string, State> args)
    {
        _selectedState = args.ItemData;
        if(_selectedState == null) return;
        
        _cityUrl = string.Format(_cityUrl, _selectedState.state_code, _selectedState.id);
        Console.WriteLine($"City URL {_domainUrl}{_cityUrl}");
        _cities =  await _http.GetFromJsonAsync<City[]>(_domainUrl + _cityUrl);
    }
    
    City _selectedCity = null;
    private void SelectedCity(Syncfusion.Blazor.DropDowns.ChangeEventArgs<string, City> args)
    {
        _selectedCity = args.ItemData;
        if(_selectedCity == null) return;
        Console.WriteLine($"Selected City : {_selectedCity.Name} {_selectedCity.id}");
    }

}