using System.Net;
using FC.Admin.Services.Helpers;
using FC.Admin.Services.Models;
using FC.Core.Extension.StringHandlers;
using FC.Extension.HTTP.HTTPHandlers;
using FC.OAuth.Services.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;

namespace FC.Admin.Services.Pages.Organization;

public class QuickAdd : PageModel
{
    [BindProperty] public Account_ViewModel ModelValue { get; set; }

    public IActionResult OnGet()
    {
        IActionResult actionResult;
        actionResult = InitializeActivity();
        return actionResult;
        //return Page();//TODO:This should be removed, the above 3 line of code to be enabled.
    }

    private IActionResult InitializeActivity()
    {
        ViewData["ActivateClient"] = "active pcoded-trigger";
        ViewData["ActivateQuickAdd"] = "active";

        if (FCUtility.IsAuthorized(this))
        {
            ViewData["ActivateClient"] = "active pcoded-trigger";
            ViewData["ActivateQuickAdd"] = "active";
            return Page();
        }
        else
        {
            var baseURL = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/";
            return Redirect(baseURL + "Login");
        }
    }

    public async Task<IActionResult> OnPostSaveCustomerAsync()
    {
        Console.WriteLine(Environment.NewLine);
        Console.WriteLine("Customer Save Triggered");
        Console.WriteLine(ModelValue.ToJSON());

        RestClient _client = new RestClient();
        string accURL = URLResource.BaseAPI;
        accURL = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/" + accURL;
        Method method;
        method = EditOrCreate(ModelValue, ref accURL);

        var header = new Dictionary<string, string>();
        header.Add("Authorization", "Bearer " + HttpContext.Session.GetString("UserToken"));
        ModelValue.DBName = ModelValue.BusinessName;
        ModelValue.ClientKey = "sampleId";
        ModelValue.ClientConnectionString = "connection string sample";

        var response = await InvokeWebAPIUtility.InvokeApi(
            new Uri(accURL), _client, method, ModelValue,
            header
        );

        var apiResponse = new APIResponse()
        {
            IsSuccess = response.StatusCode == HttpStatusCode.OK,
            ResponseData = response.Content
        };
        return Content(apiResponse.ToJSON());

        //return Content(ModelValue.ToJSON());

        #region Model State IsValid not removed

        //At this time this code is not required.
        //if (!ModelState.IsValid)
        //{
        //    Console.WriteLine("Model Is Invalid");
        //    return Page();
        //}

        #endregion
    }

    private static Method EditOrCreate(Account_ViewModel modelInfo, ref string URL)
    {
        Method method;
        if (string.IsNullOrEmpty(modelInfo.Id) || modelInfo.Id == "0")
        {
            URL = URL + URLResource.CreateAccount;
            method = Method.POST;
        }
        else
        {
            URL = URL + URLResource.UpdateAccount; // URLResource.EditTax;//_accountUniqueId
            method = Method.PUT;
        }

        return method;
    }
}