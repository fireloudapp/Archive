using System.Net.Http.Json;
using System.Net.NetworkInformation;
using BlazorDB;
using FC.BlazorWeb.Components;
using FC.BlazorWeb.Model;

namespace FC.BlazorWeb.Pages;

public partial class LocationStorage
{
    IndexedDbManager manager;
    string dbName = "AddressSection";
    private Country[]? _countries;
    private Country _selectedCountry;
    private State[]? _states;
    private State[]? _filteredStates;
    private City[]? _cities;
    private City[]? _filteredCities;
    
    private string _countryUrl = "https://raw.githubusercontent.com/dr5hn/countries-states-cities-database/master/countries.json";
    private string _stateUrl = "https://raw.githubusercontent.com/dr5hn/countries-states-cities-database/master/states.json";
    private string _cityUrl = "https://raw.githubusercontent.com/dr5hn/countries-states-cities-database/master/cities.json";
    
    
    protected override async Task OnInitializedAsync()
    {
        manager = await _dbFactory.GetDbManager(dbName);
        manager.ActionCompleted += (_, __) =>
        {
            Console.WriteLine(__.Message);
        };
        _countries =  await _http.GetFromJsonAsync<Country[]>(_countryUrl);
        Console.WriteLine("Country Loaded...");
        _states =  await _http.GetFromJsonAsync<State[]>(_stateUrl);
        if (_states != null) _filteredStates = _states.Where(ct => ct.country_id == 101).ToArray();//Default country "India".
        Console.WriteLine("State Loaded...");
        if (_cities != null)
            _filteredCities = _cities
                .Where(st => st.country_id == 101)
                .Where(st => st.state_id == 4035).ToArray();//Default state & Country
        _cities =  await _http.GetFromJsonAsync<City[]>(_cityUrl);
        Console.WriteLine("City Loaded...");
        Console.WriteLine("Location Storage Loaded & Initialized.");
    }
    
    
    private void SelectedCountry(Syncfusion.Blazor.DropDowns.ChangeEventArgs<string, Country> args)
    {
        _selectedCountry = args.ItemData;
        if(_selectedCountry == null) return;
        if (_states != null) _filteredStates = _states.Where(ct => ct.country_id == _selectedCountry.id).ToArray();
        Console.WriteLine($"Selected Country : {_selectedCountry.Name} {_selectedCountry.id}");
    }
    State _selectedState = null;
    private void SelectedState(Syncfusion.Blazor.DropDowns.ChangeEventArgs<string, State> args)
    {
        _selectedState = args.ItemData;
        if(_selectedState == null) return;
        if (_cities != null)
            _filteredCities = _cities.Where(st => st.country_id == _selectedCountry.id)
                .Where(st => st.state_id == _selectedState.id).ToArray();
        Console.WriteLine($"Selected State : {_selectedState.Name} {_selectedState.id}");
    }
    
    City _selectedCity = null;
    private void SelectedCity(Syncfusion.Blazor.DropDowns.ChangeEventArgs<string, City> args)
    {
        _selectedCity = args.ItemData;
        if(_selectedCity == null) return;
        Console.WriteLine($"Selected City : {_selectedCity.Name} {_selectedCity.id}");
    }
    
   
}