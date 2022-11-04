using BlazorDB;
using FC.BlazorWeb;
using FC.BlazorWeb.Model;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syncfusion.Blazor;

{
    var builder = WebAssemblyHostBuilder.CreateDefault(args);
    builder.RootComponents.Add<App>("#app");
    builder.RootComponents.Add<HeadOutlet>("head::after");


    Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(
        "NTkxNjU3QDMxMzkyZTM0MmUzMENUMXk2TnRaOE4xU21KVEc4TEc1L0dReEs3Smp6cnNYRHNLbU9SOTVjdW89;NTkxNjU4QDMxMzkyZTM0MmUzMGNQNTNhRFBwV2NkcjRRM1dLK3V6M3VsYWozVkFoL25UUEpRMDlqYkQ0Wk09;NTkxNjU5QDMxMzkyZTM0MmUzMFNsLzdNQ2Fkb1htRmx6STNaUDdkampXWWpjZ21SQUxVZ2hqSHdpVHkvNjQ9;NTkxNjYwQDMxMzkyZTM0MmUzMFhGMHJKSmREVlFaT3FtUW1PTTU3SjdqNmg1cVQ2Z29hQ2hBTXFUd3FJczA9;NTkxNjYxQDMxMzkyZTM0MmUzMEhPVTlpbnlRU0Z6b3BSR2VrN2lIVy9VRHBLYUVDREN3dkdQTUpvZDAzMnc9;NTkxNjYyQDMxMzkyZTM0MmUzMEFxd1VvMUpUT2pDWHdaYjdVOTNWMUFRckRydDNTVzMzTHlBNXhoVWlLMkE9;NTkxNjYzQDMxMzkyZTM0MmUzMEg2UGN4SERmUHJxY1p2TjdCemJQcDJzelFhSnc0ZnpTYVR3QU15dmtrTWs9;NTkxNjY0QDMxMzkyZTM0MmUzMG0zalh1dnpXaVpBQlFIdUdaVTRXUHVwMVVrUWt4czFrc0VML1A4L3k0TEk9;NTkxNjY1QDMxMzkyZTM0MmUzMG9ZRStVeStEV3VXWlBaWFFjMW5zaTcwYW93dXFOV3J4YmRHTk83UkdGM3M9;NTkxNjY2QDMxMzkyZTM0MmUzMFE4SWJyOGxHZVNSQU83YkliUWVORFVydnNndE9reWZUM3h5L3AvaWlkRTg9;NTkxNjY3QDMxMzkyZTM0MmUzMEFYTHZmSE80QWhMc1FsMnMzd2g5OUR3Z21aQkh2cHVHbVFXcXIycWZwRzQ9");

    builder.Services.AddSyncfusionBlazor();

    builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
    builder.Services.AddBlazorDB(options =>
    {
        options.Name = "AddressSection"; //DB Name
        options.Version = 1;
        options.StoreSchemas = new List<StoreSchema>()
        {
            // new StoreSchema()
            // {
            //     Name = "Country", //Table Name
            //     PrimaryKey = "id",
            //     PrimaryKeyAuto = true,
            //     UniqueIndexes = new List<string> { "id" },
            //     Indexes = new List<string>{ "name"}
            // },
            // new StoreSchema()
            // {
            //     Name = "City",
            //     PrimaryKey = "id",
            //     PrimaryKeyAuto = true,
            //     UniqueIndexes = new List<string> { "id" },
            //     Indexes = new List<string> { "name" }
            // },
            // new StoreSchema()
            // {
            //     Name = "State",
            //     PrimaryKey = "id",
            //     PrimaryKeyAuto = true,
            //     UniqueIndexes = new List<string> { "id" },
            //     Indexes = new List<string> { "name" }
            // },
            new StoreSchema()
            {
                Name = "Locations", //Table Name
                PrimaryKey = "id",
                PrimaryKeyAuto = true,
                UniqueIndexes = new List<string> { "id" },
                Indexes = new List<string>{ "name"}
            },
        };
    });

    Console.WriteLine($"FC.BlazorWeb Component Build @{DateTime.Now.TimeOfDay}.");
    await builder.Build().RunAsync();
}