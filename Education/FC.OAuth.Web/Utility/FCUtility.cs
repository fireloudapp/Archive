using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FC.OAuth.Services.Utility;

public class FCUtility
{
    public static bool IsAuthorized(PageModel page)
    {
        var value = page.HttpContext.Session.GetString("LoginUser_Session");
        //UserCredential user = value.ToModel();
        return value != null;
    }
}