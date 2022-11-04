using System.Text;
using AutoMapper;
using FC.Admin.Services.Models;
using FC.Extension.SQL.Helper;
using FC.Extension.SQL.Interface;
using FC.Extension.SQL.Mongo;

namespace FC.OAuth.Services.DataAccess.Organization.ClientAccontHandler;


 public class ClientAccount_CreateHandler : MongoDataAccess<ClientAccount>, ICommand<ClientAccount>
{
    string _conString = string.Empty;
    //INoSQLBaseAccess<Account> _baseAccess;
    private SQLConfig _config;
    public string Message { get; set; }

    #region Constructor
    public ClientAccount_CreateHandler(SQLConfig config) : base(config)
    {
        _config = config;
        //_baseAccess = new MongoDataAccess<Account>(_config);
    }
    #endregion

    public ClientAccount CommandHandler(ClientAccount model)
    {
        throw new NotImplementedException();
    }

    public async Task<ClientAccount> CommandHandlerAsync(ClientAccount model)
    {
        try
        {
            #region 1.Insert into Client Account
            //1. Insert into Client Account
            //model.ClientKey = Guid.NewGuid().ToString();
            model.DBName = model.BusinessName.Trim();
            model.DBName = RemoveSpecialCharacters(model.DBName);
            model.DBName = model.DBName + ".FC.Edu";
            //Connection string to be build.
            INoSQLBaseAccess<ClientAccount> baseAccess = new MongoDataAccess<ClientAccount>(_config);
            model.Id = null;
            //Saves the date to MongoDB.
            var account = await baseAccess.CreateAsync(model: model);
            model = account;
            #endregion

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        return model;
    }

    public static string RemoveSpecialCharacters(string str)
    {
        StringBuilder sb = new StringBuilder();
        foreach (char c in str)
        {
            if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
            {
                sb.Append(c);
            }
        }
        return sb.ToString();
    }
}
    
