using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SqlKata;
using System.Threading.Tasks;
using System.Linq;
using System;
using Humanizer;
using FC.Extension.HTTP.APIHandler;
using FC.Extension.SQL.PostgreSQL;
using FC.Extension.SQL.Interface;
using System.Collections.Generic;
using FC.Admin.Services.Helpers;
using FC.Admin.Services.Models;
using FC.Admin.Services.Organization.AccountHandler;
using FC.Extension.SQL.Engine;
using FC.Extension.SQL.Helper;
using FC.OAuth.Services.Fake;
using RepoDb.Enumerations;

namespace FC.Admin.Services.Controllers.Organization
{
    /// <summary>
    /// Account Controller Web API
    /// </summary>
    /// [Authorize]
    [Route("api/Organization/AccountView/")]
    [ApiController]
    public class AccountViewController : PostgreBaseAPI<Account_ViewModel, AccountViewController>
    {

        #region Variables
        ICommand<Account_ViewModel> _commandCreateHadler;
        ICommand<Account_ViewModel> _commandUpdateHadler;
        readonly string _connectionString = string.Empty;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor of Account service
        /// </summary>
        /// <param name="logger">Log dependency injection</param>
        /// <param name="configuration">Configuration dependency injection</param>
        public AccountViewController(ILogger<AccountViewController> logger, IConfiguration configuration,
            IConnectionService connectionService, IHttpContextAccessor httpContext)
            : base(logger, configuration, connectionService, httpContext)
        {
            var sqlConfig = connectionService.GetNoSQLConfig(configuration);
            SQLExtension.SQLConfig = sqlConfig;
            
            //If we need to write some business logic then only we need to create CreateHandler else we can use BaseAPI for CRUD.
            _commandCreateHadler = new AccountCreateHandler(sqlConfig);
            _commandUpdateHadler = new AccountUpdateHandler(sqlConfig);
            _connectionString = configuration.GetValue<string>("DBSettings:AuthenticationDB");
            _baseAccess = new PostgreSQLDataAccess<Account_ViewModel>(_connectionString, new TraceDB());
            //For account alone we will use Account Database hence the logic was hotcoded.
            _logger = logger;
        }
        #endregion

        //Basic Create, Update, Delete, Get by Id, Get by Paging & Get by Batch will appear from Base API.
        //Write only custom logic here.

        #region Create Account with dependency of Address
        /// <summary>
        /// Create Service Ticket
        /// </summary>
        /// <param name="model">Service Ticket detailed object</param>
        /// <returns>retuns the data of the inserted service Ticket</returns>
        [Route("CreateAccount")]
        [HttpPost]
        public async Task<Account_ViewModel> CreateHandler(Account_ViewModel model)
        {
            Account_ViewModel resultModel = await _commandCreateHadler.CommandHandlerAsync(model);
            return resultModel;
        }
        #endregion

        #region Create Update with dependency of Address
        /// <summary>
        /// Create Service Ticket
        /// </summary>
        /// <param name="model">Service Ticket detailed object</param>
        /// <returns>retuns the data of the inserted service Ticket</returns>
        [Route("UpdateAccount")]
        [HttpPut]
        public async Task<Account_ViewModel> UpdateAccount(Account_ViewModel model)
        {
            Account_ViewModel resultModel = await _commandUpdateHadler.CommandHandlerAsync(model);
            return resultModel;
        }
        #endregion

        #region Get Account Data with Details
        /// <summary>
        /// Get the Model details by Batch and splits into Page.
        /// </summary>
        /// <param name="id">Accuont Id </param>        
        /// <returns>returns no of Models</returns>
        [Route("GetDetail")]
        [HttpGet]
        public async Task<Account_ViewModel> GetDetailsById(int id = 0)
        {
            // Query query = ManagementQuery.Instance.AccountDetailed_QueryView.Where("ac.Id", id);
            //
            // var accountView = await _baseAccess.ExecuteQuery(query);
            // Account_ViewModel account = accountView.FirstOrDefault();

            //_logger.LogInformation($"GetDetailsById API Data : {account.DumpToJSON()}");
            //return account;
            return null;
        }
        #endregion

        #region Days to Expire
        /// <summary>
        /// Returns no of days to expire
        /// </summary>
        /// <param name="id">Account Id</param>
        /// <param name="hint">Readable String hit starts from 1 to 5. Eg. '2 weeks, 1 day, 1 hour'</param>
        /// <returns>Expiry Date with details</returns>
        [Route("GetDaysToExpire")]
        [HttpGet]
        public async Task<AccountExpiry> GetDaysToExpire(int id = 0, int hint = 0)
        {
            // AccountExpiry accountExpiry = new AccountExpiry();
            // Query query = ManagementQuery.Instance.AccountDetailed_QueryView.Where("ac.Id", id);
            //
            // var accountView = await _baseAccess.ExecuteQuery(query);
            // Account_ViewModel account = accountView.FirstOrDefault();
            //
            // if (account.IsActive)
            // {
            //     //1. In which day it will expire.,
            //     accountExpiry.ExpiryDate = account.ActivationDate.AddDays(account.ActivateNoOfDays);
            //     //2. How many remaining days?                
            //     accountExpiry.RemainingDays = accountExpiry.ExpiryDate.Subtract(DateTime.Now);
            //     accountExpiry.RemainingDaysReadable = accountExpiry.RemainingDays.Humanize(hint);
            //     accountExpiry.RemainingNoOfDays = accountExpiry.RemainingDays.Days;
            // }
            //
            // accountExpiry.AccountDetails = account;
            // _logger.LogInformation($"GetDaysToExpire API Data : {accountExpiry.DumpToJSON()}");
            // return accountExpiry;
            return null;
        }

        #endregion

        #region Fake Data
        /// <summary>
        /// Based on domain type we need to initialize the object and call the respective method.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="count"></param>
        /// <param name="canGenerateId"></param>
        /// <returns></returns>
        [Route("GetFakeData")]
        [HttpGet]
        public IList<Account_ViewModel> GetFake(string model, int count, bool canGenerateId = true)
        {
            IList<Account_ViewModel> modelList = null;
            modelList = Account_ViewModelFake.GetData(count, canGenerateId);
            Console.WriteLine(modelList.DumpToJSON());
            return modelList;
        }
        #endregion


        //[Route("GetAccountByBatchData")]
        //[HttpPost]
        //public async Task<MetaPagination<Account_ViewModel>> GetAccountByBatchData
        //     (MetaPagingParams metaParams)
        //{
        //    var metaPagination = new MetaPagination<Account_ViewModel>();
        //    var dataList = await GetDataFromDB(metaParams);
        //    var count = await _baseAccess.GetRecordCountAsync();
        //    metaPagination.Total = count;
        //    metaPagination.TotalNotFiltered = count;
        //    metaPagination.Rows = dataList;
        //    return metaPagination;
        //}

        //private async Task<IEnumerable<Account_ViewModel>> GetDataFromDB(MetaPagingParams metaParams)
        //{
        //    metaParams.order = string.IsNullOrEmpty(metaParams.order) ? string.Empty : metaParams.order;

        //    var query = new Query(typeof(Account_ViewModel).Name)
        //        .Limit(metaParams.limit)
        //        .Offset(metaParams.offset);
        //    query = metaParams.order.Equals("asc") ? query.OrderBy("Id") : query.OrderByDesc("Id");
        //    if (!string.IsNullOrEmpty(metaParams.search))
        //    {
        //        query.WhereRaw($"\"{metaParams.searchcolumn}\" ILIKE '%{metaParams.search}%'");
        //    }
        //    IEnumerable<Account_ViewModel> taxData = await _baseAccess.ExecuteQuery(query);

        //    _logger.LogInformation($"{LogClassAndMethod()} API Executed Count : {taxData.Count()}");
        //    return taxData;
        //}

    }


}
