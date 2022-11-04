using System.Text;
using FC.Admin.Services.Models;
using FC.Extension.SQL.Helper;
using FC.Extension.SQL.Interface;
using FC.Extension.SQL.Mongo;

namespace FC.OAuth.Services.DataAccess.Organization.ClientAccontHandler;

public class ClientAccount_UpdateHandler : MongoDataAccess<ClientAccount>, ICommand<ClientAccount>
{
    string _conString = string.Empty;
    //INoSQLBaseAccess<Account> _baseAccess;
    private SQLConfig _config;
    public string Message { get; set; }

    #region Constructor
    public ClientAccount_UpdateHandler(SQLConfig config) : base(config)
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
            #region 1.Update into Client Account
            //1. Update into Client Account
            INoSQLBaseAccess<ClientAccount> baseAccess = new MongoDataAccess<ClientAccount>(_config);
            model.Id = null;
            //Saves the date to MongoDB.
            var account = await baseAccess.UpdateAsync(acc => acc.Id == model.Id, model);
            model = account;
            #endregion

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        return model;
    }
}