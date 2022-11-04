//API Constants
var _apiBase = '/api/'; //running in console.
var _authAppURL = '/';
var _businessTypeURL = 'js/json/businessType.json';
var _businessCategoryURL = 'js/json/businessCategory.json';
var _activationDaysURL = 'js/json/activationDays.json';

var _urlAccountFake = _apiBase + 'Organization/AccountView/GetFakeData?model=Account_ViewModel&count=1&canGenerateId=false';
var _urlDT_POSTAccount = _apiBase + 'Organization/Account/GetByBatch';
var _urlGetAccountDetails = _apiBase + 'Organization/AccountView/GetDetail?id=';


//Settings or constant definition.

var _editIcon = ' &nbsp;<i class="icofont icofont-copy-black"></i> ';
var _deleteIcon = ' &nbsp;<i class="icofont icofont-ui-delete"></i> ';
