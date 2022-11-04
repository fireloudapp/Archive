using AutoMapper;
using FC.Extension.SQL.Interface;
using FC.Extension.SQL.Mongo;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bogus.DataSets;
using FC.Admin.Services.Helpers;
using FC.Admin.Services.Models;
using FC.Core.Extension.StringHandlers;
using FC.Extension.SQL.Engine;
using FC.Extension.SQL.Helper;
using MongoDB.Bson;
using MongoDB.Driver;
using Address = FC.Admin.Services.Models.Address;

namespace FC.Admin.Services.Organization.AccountHandler
{
    public class AccountCreateHandler : MongoDataAccess<Account_ViewModel>, ICommand<Account_ViewModel>
    {
        string _conString = string.Empty;
        //INoSQLBaseAccess<Account> _baseAccess;
        private SQLConfig _config;
        public string Message { get; set; }

        #region Constructor
        public AccountCreateHandler(SQLConfig config) : base(config)
        {
            _config = config;
            //_baseAccess = new MongoDataAccess<Account>(_config);
        }
        #endregion

        public Account_ViewModel CommandHandler(Account_ViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<Account_ViewModel> CommandHandlerAsync(Account_ViewModel model)
        {
            try
            {
                #region 1.Insert into Account

                //1. Insert into Account
                //Initialize the mapper
                var configAccount = new MapperConfiguration(cfg =>
                    cfg.CreateMap<Account_ViewModel, Account>()
                );
                //accessing the object
                var mapperAccount = new Mapper(configAccount);
                var accountDTO = mapperAccount.Map<Account_ViewModel, Account>(model);
                //accountDTO.AddressId = addressId;
                if (accountDTO.IsActive)
                {
                    accountDTO.ActivationDate = DateTime.Now;
                }
                else
                {
                    accountDTO.ActivationDate = DateTime.Now.AddDays(-30);
                }

                accountDTO.ClientKey = Guid.NewGuid().ToString();
                accountDTO.DBName = accountDTO.BusinessName.Trim();
                accountDTO.DBName = RemoveSpecialCharacters(accountDTO.DBName);
                accountDTO.DBName = accountDTO.DBName + ".FC.Edu";
                //Connection string to be build.
                INoSQLBaseAccess<Account> baseAccess = new MongoDataAccess<Account>(_config);
                accountDTO.Id = null;
                //Saves the date to MongoDB.
                var account = await baseAccess.CreateAsync(accountDTO);

                #endregion

                #region 2.Insert into Address
                //2.0 Insert into Address
                //Initialize the mapper
                var configAddress = new MapperConfiguration
                (cfg =>
                    cfg.CreateMap<Account_ViewModel, Address>()
                );

                //Assigning the object
                var mapperAddress = new Mapper(configAddress);
                var addressDTO = mapperAddress.Map<Account_ViewModel, Address>(model);
                addressDTO.AccountId = account.Id;
                INoSQLBaseAccess<Address> baseAddress = new MongoDataAccess<Address>(_config);
                addressDTO.Id = null;
                var address = await baseAddress.CreateAsync(addressDTO);
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
    
} 
