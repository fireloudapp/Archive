using FC.Extension.HTTP.APIHandler;
using FC.Extension.SQL.Interface;
using Microsoft.AspNetCore.Mvc;
using FC.Admin.Services.Models;
using FC.Admin.Services.Organization.AccountHandler;
using FC.Extension.SQL.Helper;
using FC.Extension.SQL.Mongo;

namespace FC.Admin.Services.Controllers
{
    [Route("api/Organization/AccountView/")]
    [ApiController]
    public class AccountController :  ControllerBase //MongoDBBaseAPI<Account, AccountController>
    {
        #region Variables
        ICommand<Account> _commandCreateHadler;
        ICommand<Account> _commandUpdateHadler;
        private INoSQLBaseAccess<Account> _baseAccess;
        private SQLConfig _sqlConfig;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor of Account service
        /// </summary>
        /// <param name="logger">Log dependency injection</param>
        /// <param name="configuration">Configuration dependency injection</param>
        public AccountController(ILogger<AccountController> logger, IConfiguration configuration,
            IConnectionService connectionService, IHttpContextAccessor httpContext)
            //: base(logger, configuration, connectionService, httpContext)
        {
            //If we need to write some business logic then only we need to create CreateHandler else we can use BaseAPI for CRUD.
            configuration.GetValue<string>("DBSettings:ClientDB");
            _sqlConfig = connectionService.GetNoSQLConfig(configuration,"DBSettings:ClientDB", "DBSettings:CollectionName","DBSettings:DataBaseName");
            
            _baseAccess = new MongoDataAccess<Account>(_sqlConfig);
            //For account alone we will use Account Database hence the logic was hot coded.
            //_logger = logger;
        }
        #endregion

        //Basic Get by Id, Get by Paging & Get by Batch will appear from Base API.
        //Write only custom logic here.
        //Update & Delete API's to be done.

        #region CRUD

        /// <summary>
        /// Create Account_ViewModel based on the Model
        /// </summary>
        /// <param name="model">Account_ViewModel model</param>
        /// <returns>returns Account_ViewModel as json data</returns>
        [Route("CreateAccount")]
        [HttpPost]
        public async Task<Account> Create(Account_ViewModel model)
        {
            ICommand<Account_ViewModel> commandCreate = new AccountCreateHandler(_sqlConfig);
            var account = await commandCreate.CommandHandlerAsync(model);
            return account;
        }
        /// <summary>
        /// Update Account_ViewModel based on the Model
        /// </summary>
        /// <param name="model">Account_ViewModel model</param>
        /// <returns>returns Account_ViewModel as json data</returns>
        [Route("UpdateAccount")]
        [HttpPost]
        public async Task<Account> Update(Account_ViewModel model)
        {
            ICommand<Account_ViewModel> commandCreate = new AccountUpdateHandler(_sqlConfig);
            var account = await commandCreate.CommandHandlerAsync(model);
            return account;
        }
        //TODO: Delete & Get

        #endregion
    }
    
}
