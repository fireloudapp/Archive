namespace FC.Admin.Services.Models;

public class AccountExpiry
{
    public Account_ViewModel AccountDetails
    {
        get;
        set;
    }

    public DateTime ExpiryDate
    {
        get;
        set;
    }

    public TimeSpan RemainingDays
    {
        get;
        set;
    }

    public string RemainingDaysReadable
    {
        get;
        set;
    }

    public int RemainingNoOfDays
    {
        get;
        set;
    }
}