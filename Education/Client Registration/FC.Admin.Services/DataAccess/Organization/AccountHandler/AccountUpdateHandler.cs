using AutoMapper;
using FC.Admin.Services.Models;
using FC.Extension.SQL.Engine;
using FC.Extension.SQL.Helper;
using FC.Extension.SQL.PostgreSQL;
using FC.Extension.SQL.Interface;
using FC.Extension.SQL.Mongo;
using SqlKata;

namespace FC.Admin.Services.Organization.AccountHandler
{

    public class AccountUpdateHandler : MongoDataAccess<Account_ViewModel>, ICommand<Account_ViewModel>
    {
        string _conString = string.Empty;
        public string Message { get; set; }
        private SQLConfig _config;
        //PostgreSQLDataAccess<Account> _baseAccount;
        //PostgreSQLDataAccess<Address> _baseAddress;

        #region Constructor
        public AccountUpdateHandler(SQLConfig config) : base(config)
        {
            _config = config;
            //_conString = connectionString;
            //_baseAccount = new PostgreSQLDataAccess<Account>(_conString);
            //_baseAddress = new PostgreSQLDataAccess<Address>(_conString);
        }
        #endregion

        public Account_ViewModel CommandHandler(Account_ViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<Account_ViewModel> CommandHandlerAsync(Account_ViewModel model)
        {
            //return null;
            try
            {
                //1.0 Update into Address
                //Initialize the mapper
                var configAddress = new MapperConfiguration(cfg =>
                    cfg.CreateMap<Account_ViewModel, Address>()
                );
                //passing the object
                var mapperAddress = new Mapper(configAddress);
                var addressDTO = mapperAddress.Map<Account_ViewModel, Address>(model);
                
                INoSQLBaseAccess<Address> baseAddress = new MongoDataAccess<Address>(_config);
                var addressList =
                    baseAddress.GetAnyAsync(addr => addr.AccountId == model.Id).Result.ToList();
                
                var address = addressList.FirstOrDefault();
                addressDTO.Id = address.Id;
                addressDTO.AccountId = model.Id;
                await baseAddress.UpdateAsync(addr => addr.Id == addressDTO.Id, addressDTO);
            
                //2.0 Update into Account
                //Initialize the mapper
                var configAccount = new MapperConfiguration(cfg =>
                    cfg.CreateMap<Account_ViewModel, Account>()
                );
                //assigning the object
                var mapperAccount = new Mapper(configAccount);
                var accountDTO = mapperAccount.Map<Account_ViewModel, Account>(model);
                await HandleDeActivatedAccount(accountDTO);
                
                INoSQLBaseAccess<Account> baseAccount = new MongoDataAccess<Account>(_config);
                await baseAccount.UpdateAsync(acc => acc.Id == accountDTO.Id, accountDTO);
            }
            catch (Exception ex)
            {
            
            }
            
            return model;
        }

        /// <summary>
        /// Get if previously status of the account and activates or deactivates based on the new action.
        /// </summary>
        /// <param name="accountDTO"></param>
        /// <returns></returns>
        private async Task HandleDeActivatedAccount(Account accountDTO)
        {
            INoSQLBaseAccess<Account> baseAccount = new MongoDataAccess<Account>(_config);
            var acc = baseAccount.GetAnyAsync(acc => acc.Id == accountDTO.Id).Result.ToList().FirstOrDefault();
            accountDTO.ActivationDate = DateTime.Now;
            if (!acc.IsActive)// Indicates that the account is currenlty not active, 
            {
                if (accountDTO.IsActive)//Now the currently activated for the decativated account
                {
                    accountDTO.ActivationDate = DateTime.Now;
                }
                else
                {
                    accountDTO.ActivationDate = DateTime.Now.AddDays(-30);
                }
            }
        }
    }
}
