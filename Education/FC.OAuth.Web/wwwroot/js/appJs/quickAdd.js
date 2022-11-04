//#region Load Script
(function () {
    //Temp to be removed.
    //Cookies.set('UserToken', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjAiLCJyb2xlIjoiRkMgT3dlbmVyIiwiaW5mbyI6IntcclxuICBcIlJlZnJlc2hUb2tlbnNcIjogbnVsbCxcclxuICBcIklkXCI6IDAsXHJcbiAgXCJGaXJzdE5hbWVcIjogXCJHYW5lc2ggUmFtXCIsXHJcbiAgXCJMYXN0TmFtZVwiOiBudWxsLFxyXG4gIFwiUGhvbmVcIjogbnVsbCxcclxuICBcIkVtYWlsXCI6IFwic3IuZ2FuZXNocmFtQGdtYWlsLmNvbVwiLFxyXG4gIFwiUGFzc3dvcmRIYXNoXCI6IFwiXCIsXHJcbiAgXCJJc0FjdGl2ZVwiOiB0cnVlLFxyXG4gIFwiUm9sZVwiOiBcIkZDIE93ZW5lclwiLFxyXG4gIFwiVG9rZW5cIjogbnVsbCxcclxuICBcIkxvZ2luSXBcIjogbnVsbCxcclxuICBcIkNsaWVudERCTmFtZVwiOiBcIkZDLUNvbW1vblwiLFxyXG4gIFwiU3RhZmZJZFwiOiAwLFxyXG4gIFwiTGFzdExvZ2luXCI6IFwiMDAwMS0wMS0wMVQwMDowMDowMFwiLFxyXG4gIFwiQWNjb3VudElkXCI6IDAsXHJcbiAgXCJJc1N5bmNcIjogZmFsc2VcclxufSIsIm5iZiI6MTYzMjMyMzIzMCwiZXhwIjoxNjM0OTE1MjMwLCJpYXQiOjE2MzIzMjMyMzB9.tNS5kzCE5vGATPr4IE2KhYMJAzvlynk70Gg9_ZBKTXo');
    //console.log("Temp - User Token Cookie Created");
    GetBusinessType();
    GetBusinessCategory();
    GetActivationDays();

    const urlSearchParams = new URLSearchParams(window.location.search);
    const params = Object.fromEntries(urlSearchParams.entries());
    //var accountId = param.id;
    GetAccountDetails(params.id);//Pass Account Id

    const obj = { 0: 'adam', 1: 'billy', 2: 'chris' };
    console.log(Object.entries(obj)[1]);
})();


//#endregion

//#region Get Account Details for Edit
function GetAccountDetails(accountId) {
    if (typeof accountId === 'undefined') {
        //Add Mode
    }
    else {
        var getURL = _urlGetAccountDetails + accountId;
        //2. Get the data
        fnInvokeAjaxGET(getURL, fnCallback_AssignAccount, fnAjaxFailCase);
    }
}

function fnCallback_AssignAccount(response) {
    // Gets and assign the data for edit.
    console.log(response);
    vm.$data.id = response.id;
    vm.$data.businessName = response.businessName;
    vm.$data.businessType = response.businessType;
    vm.$data.businessCategory = response.businessCategory;
    vm.$data.description = response.description;
    vm.$data.logo = response.logo;
    vm.$data.website = response.webSite;
    vm.$data.gstin = response.gstin;
    vm.$data.addressLine_1 = response.addressLine_1;
    vm.$data.addressLine_2 = response.addressLine_2;
    vm.$data.cityId = response.cityId;
    vm.$data.stateId = response.stateId;
    vm.$data.countryId = response.countryId;
    vm.$data.postalCode = response.postalCode;
    vm.$data.phone = response.phone;
    vm.$data.email = response.email;
    vm.$data.activateNoOfDays = response.activateNoOfDays;
    vm.$data.isActive = response.isActive;
    vm.$data.activateActivate = response.isActive;

    //Newly added columns.
    vm.$data.DBName = response.dbName;
    vm.$data.ClientKey = response.clientKey;
    vm.$data.ClientConnectionString = response.clientConnectionString;
}
//#endregion

//#region Load Business Type Data
function GetBusinessType() {
    var getURL = _authAppURL + _businessTypeURL;
    //2. Get the data
    fnInvokeAjaxGET(getURL, fnCallback_BusinessType, fnAjaxFailCase);
}

function fnCallback_BusinessType(response) {
    // Gets and assign the data for edit.
    console.log(response);
    vm.$data.businessTypeList = response.BusinessType;

}
//#endregion

//#region Load Business Category Data
function GetBusinessCategory() {
    var getURL = _authAppURL + _businessCategoryURL;
    //2. Get the data
    fnInvokeAjaxGET(getURL, fnCallback_BusinessCategory, fnAjaxFailCase);
}

function fnCallback_BusinessCategory(response) {
    // Gets and assign the data for edit.
    console.log(response);
    vm.$data.businessCategoryList = response.BusinessCategory;

}
//#endregion

//#region Load Activatate No Of Days
function GetActivationDays() {
    var getURL = _authAppURL + _activationDaysURL;
    //2. Get the data
    fnInvokeAjaxGET(getURL, fnCallback_ActivationDays, fnAjaxFailCase);
}

function fnCallback_ActivationDays(response) {
    // Gets and assign the data for edit.
    console.log(response);
    vm.$data.activationDaysList = response.ActivateNoOfDays;

}
//#endregion

//#region Customer Add Response
function AddCustomer(xhr) {
    console.log(xhr);
    var res = JSON.parse(xhr.responseText);
    if (res.IsSuccess) {
        fnMessage('success', "Customer Updated Successfuly!");
    } else {
        fnMessage('error', "Something went wrong!.");
    }
}
//#endregion

//#region Vue Js Initialization

var data =
{
    id: 0,
    businessName: "",
    businessType: "",
    businessCategory: "",
    description: "",
    logo: "",
    website: "",
    gstin: "",
    addressLine_1: "",
    addressLine_2: "",
    cityId: "",
    stateId: "",
    countryId: "",
    postalCode: "",
    phone: "",
    email: "",
    activateNoOfDays: "",
    isActive: '',
    DBName: '',
    ClientKey: '',
    ClientConnectionString: '',
    activateActivate: '',
    businessTypeList: [],
    businessCategoryList: [],
    activationDaysList: []
};

// The object is added to a Vue instance
var vm = new Vue({
    el: '#customerVM',
    data: data,
    methods: {
        getFake: function (event) {
            var settings = {
                "url": _urlAccountFake,
                "method": "GET",
                "timeout": 0,
                "headers": {
                    "Content-Type": "application/json",
                    "Authorization": _authToken
                }
            };
            $.ajax(settings).done(function (response) {
                console.log(response);

                data.id = "0"; //response[0].id
                data.businessName = response[0].businessName;
                data.businessType = response[0].businessType;
                data.businessCategory = response[0].businessCategory;
                data.description = response[0].description;
                data.logo = response[0].logo;
                data.website = response[0].webSite;
                data.gstin = response[0].gstin;
                data.addressLine_1 = response[0].addressLine_1;
                data.addressLine_2 = response[0].addressLine_2;
                data.cityId = response[0].cityId;
                data.stateId = response[0].stateId;
                data.countryId = response[0].countryId;
                data.postalCode = response[0].postalCode;
                data.phone = response[0].phone;
                data.email = response[0].email;
                data.activateNoOfDays = response[0].activateNoOfDays;
                data.isActive = response[0].isActive;
            });
        },
        vueDelete: function (event) {
            deleteTax(data.id);
        },
        activated: function (event) {
            //data.isActive = data.activateActivate;
        }

    }
});
//to consume outside //vm.$data === data
//#endregion