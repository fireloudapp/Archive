using Bogus;
using Bogus.DataSets;
using FC.Admin.Services.Helpers;
using FC.Admin.Services.Models;
using Address = FC.Admin.Services.Models.Address;

namespace FC.OAuth.Services.Fake;

    public class Account_ViewModelFake
    {
        public static IList<Account_ViewModel> GetData(int count, bool canGenerateId = true)
        {
            IList<Account_ViewModel> fakeData = new List<Account_ViewModel>();
            for (int iCount = 1; iCount <= count; iCount++)
            {
                Account_ViewModel accountVM = new Account_ViewModel();
                var account = AccountFake.GetData(1);
                var address = AddressFake.GetData(1);
                accountVM.Id = account[0].Id;
                accountVM.BusinessName = account[0].BusinessName;
                accountVM.GSTIN = account[0].GSTIN;
                accountVM.IsActive = account[0].IsActive;
                accountVM.Email = account[0].Email;
                accountVM.BusinessType = account[0].BusinessType;

                accountVM.BusinessCategory = account[0].BusinessCategory;
                accountVM.Description = account[0].Description;
                //accountVM.AddressId = address[0].Id;
                accountVM.Logo = account[0].Logo;
                accountVM.ActivateNoOfDays = account[0].ActivateNoOfDays;
                accountVM.ActivationDate = account[0].ActivationDate;

                accountVM.Phone = account[0].Phone;
                accountVM.WebSite = "https://" + account[0].WebSite;
                //accountVM.IsSync = account[0].IsSync;

                accountVM.StateId = address[0].StateId;
                accountVM.CityId = address[0].CityId;
                accountVM.CountryId = address[0].CountryId;
                accountVM.AddressLine_1 = address[0].AddressLine_1;
                accountVM.AddressLine_2 = address[0].AddressLine_2;

                //var fakeDat = new Faker<string>();
                //fakeDat.RuleFor(g => g, f => f.Address.ZipCode());
                //var code = fakeDat.Generate(1);
                //accountVM.PostalCode = code[0];
                accountVM.PostalCode = "635221";
                fakeData.Add(accountVM);
            }
            return fakeData;
        }
    }

    public class AccountFake
    {
        public static IList<Account> GetData(int count, bool canGenerateId = true)
        {

            var activationDays = new List<int> { 30, 60, 90, 120, 180, 365, 730, 1095 };
            var fakeDat = new Faker<Account>();

            //fakeDat.RuleFor(g => g.Id, f => f.Random.Short(1, 250));
            fakeDat.RuleFor(g => g.BusinessName, f => f.Company.CompanyName());
            fakeDat.RuleFor(g => g.Description, f => f.Company.CatchPhrase());
            fakeDat.RuleFor(g => g.GSTIN, f => f.Finance.Account());
            fakeDat.RuleFor(g => g.IsActive, f => f.Random.Bool());

            fakeDat.RuleFor(g => g.Email, f => f.Person.Email);
            fakeDat.RuleFor(g => g.BusinessType, f => f.Random.Short(1, 10).ToString());
            fakeDat.RuleFor(g => g.BusinessCategory, f => f.Random.Short(1, 10).ToString());
            //fakeDat.RuleFor(g => g.AddressId, f => f.Random.Short(1, 250));

            fakeDat.RuleFor(g => g.Logo, f => f.Image.PlaceholderUrl(128, 128)); //LoremPixelUrl("random", 128, 128));            
            fakeDat.RuleFor(g => g.Phone, f => f.Random.Replace("+##-#####-#####"));
            fakeDat.RuleFor(g => g.WebSite, f => f.Internet.DomainName());
            fakeDat.RuleFor(g => g.IsActive, f => f.Random.Bool());
            fakeDat.RuleFor(g => g.ActivateNoOfDays, f => f.PickRandom(activationDays));
            //fakeDat.RuleFor(g => g.ActivationDate, f => f.Date.Recent(2));

            IList<Account> fakeList = null;
            if (!canGenerateId)
            {
                fakeDat.FinishWith((f, u) =>
                {
                    Console.WriteLine("Before Id Set to '0'. Id={0}", u.Id);
                    u.Id = string.Empty;
                    Console.WriteLine("After Id Set to '0'. Id={0}", u.Id);
                });
            }
            fakeList = fakeDat.Generate(count);
            Console.WriteLine(fakeList.DumpToJSON());

            return fakeList;
        }
    }
    public class AddressFake
    {
        public static IList<Address> GetData(int count, bool canGenerateId = true)
        {
            var fakeDat = new Faker<Address>();

            //fakeDat.RuleFor(g => g.Id, f => f.Random.Short(1, 250));
            fakeDat.RuleFor(g => g.AddressLine_1, f => f.Address.StreetName());
            fakeDat.RuleFor(g => g.AddressLine_2, f => f.Address.StreetAddress());
            fakeDat.RuleFor(g => g.CityId, f => f.Random.Short(1, 3));

            fakeDat.RuleFor(g => g.CountryId, f => f.Random.Short(1, 3));
            fakeDat.RuleFor(g => g.StateId, f => f.Random.Short(1, 3));

            IList<Address> fakeList = null;
            if (!canGenerateId)
            {
                fakeDat.FinishWith((f, u) =>
                {
                    Console.WriteLine("Before Id Set to '0'. Id={0}", u.Id);
                    u.Id = string.Empty;
                    Console.WriteLine("After Id Set to '0'. Id={0}", u.Id);
                });
            }
            fakeList = fakeDat.Generate(count);
            Console.WriteLine(fakeList.DumpToJSON());

            return fakeList;
        }
    }