using FC.Edu.Domain;
using MongoDB.Bson;
using Syncfusion.Blazor.DropDowns;

namespace FCEdu.Web.Blazor.UI;

public partial class AccountUI
{
    private Account _account = new Account();
   
    List<BusinessType> _businessTypes = new List<BusinessType> {
        new BusinessType() { ID= "1", Text= "School" },
        new BusinessType() { ID= "2", Text= "Institute/College" },
        new BusinessType() { ID= "3", Text= "University" },
    };
    
    List<SubscriptionModel> Subscriptions = new List<SubscriptionModel> {
        new SubscriptionModel() { ID= 1, Text= "30 Days" , ActivatedDays = 30},
        new SubscriptionModel() { ID= 2, Text= "60 Days", ActivatedDays = 60 },
        new SubscriptionModel() { ID= 3, Text= "90 Days", ActivatedDays = 90 },
        new SubscriptionModel() { ID= 4, Text= "180 Days", ActivatedDays = 180 },
        new SubscriptionModel() { ID= 5, Text= "365 Days", ActivatedDays = 365 }
        
    };

    private void BusinessType_ChangeHandler(ChangeEventArgs<string, BusinessType> args)
    {
        _account.BusinessType = args.ItemData;
    }
    private void Subscription_ChangeHandler(ChangeEventArgs<string, SubscriptionModel> args)
    {
        var subscription = args.ItemData;
        _account.ActivationDate = DateTime.Now.AddDays(subscription.ActivatedDays);
        _account.SubscriptionModel = args.ItemData;
        Console.WriteLine(DateTime.Now.AddDays(subscription.ActivatedDays));
        //subscription.ActivatedDays
    }
    
    private void onSubmitClick(Microsoft.AspNetCore.Components.Web.MouseEventArgs args)
    {
        Console.WriteLine(_account.ToJson());
        Console.WriteLine("Clicked.");
    }

}