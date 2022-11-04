namespace FC.Admin.Services.Helpers;

public class APIResponse
{
    private static APIResponse _instance;

    public static APIResponse Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new APIResponse();
            }

            return _instance;
        }
    }

    public string ResponseData
    {
        get;
        set;
    }

    public bool IsSuccess
    {
        get;
        set;
    }

    public string Error
    {
        get;
        set;
    }
}