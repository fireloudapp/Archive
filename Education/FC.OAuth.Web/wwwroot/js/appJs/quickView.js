(function () {
    //Temp to be removed.
    //Cookies.set('UserToken', 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjAiLCJyb2xlIjoiRkMgT3dlbmVyIiwiaW5mbyI6IntcclxuICBcIlJlZnJlc2hUb2tlbnNcIjogbnVsbCxcclxuICBcIklkXCI6IDAsXHJcbiAgXCJGaXJzdE5hbWVcIjogXCJHYW5lc2ggUmFtXCIsXHJcbiAgXCJMYXN0TmFtZVwiOiBudWxsLFxyXG4gIFwiUGhvbmVcIjogbnVsbCxcclxuICBcIkVtYWlsXCI6IFwic3IuZ2FuZXNocmFtQGdtYWlsLmNvbVwiLFxyXG4gIFwiUGFzc3dvcmRIYXNoXCI6IFwiXCIsXHJcbiAgXCJJc0FjdGl2ZVwiOiB0cnVlLFxyXG4gIFwiUm9sZVwiOiBcIkZDIE93ZW5lclwiLFxyXG4gIFwiVG9rZW5cIjogbnVsbCxcclxuICBcIkxvZ2luSXBcIjogbnVsbCxcclxuICBcIkNsaWVudERCTmFtZVwiOiBcIkZDLUNvbW1vblwiLFxyXG4gIFwiU3RhZmZJZFwiOiAwLFxyXG4gIFwiTGFzdExvZ2luXCI6IFwiMDAwMS0wMS0wMVQwMDowMDowMFwiLFxyXG4gIFwiQWNjb3VudElkXCI6IDAsXHJcbiAgXCJJc1N5bmNcIjogZmFsc2VcclxufSIsIm5iZiI6MTYzMjMyMzIzMCwiZXhwIjoxNjM0OTE1MjMwLCJpYXQiOjE2MzIzMjMyMzB9.tNS5kzCE5vGATPr4IE2KhYMJAzvlynk70Gg9_ZBKTXo');
    //console.log("Temp - User Token Cookie Created");
    //const string = 'This is a string.';
    //const message = `${string} This is also a string.`;
})();

//#region Initialization  Data Grid and Load Data
var _params;
var $dtTable = $('#accountTable');

function fu_TaxDataLoad(params) {
    var jsonData = JSON.parse(params.data);
    var customParam = {
        "search": jsonData.search,
        "offset": jsonData.offset,
        "limit": jsonData.limit,
        "searchcolumn": "BusinessName"
    }

    fnInvokeAjaxPost(_urlDT_POSTAccount, JSON.stringify(customParam), callback_AccountDataLoadSuccess, fnAjaxFailCase);
    _params = params;
}

function callback_AccountDataLoadSuccess(response) {
    console.log(response);
    _params.success(response);
}
//#endregion

function activateAction(value, row) {
    var iconActive = `<div><i style="height:60px;color:green;" class="ion-checkmark-circled"></i></div>`;
    var iconInActive = `<div><i style="height:60px;color:red;" class="ion-close-circled"></i></div>`;

    if (value) {
        return iconActive;
    } else {
        return iconInActive;
    }
}
//#region Edit Action
function editLink(value, row) {
    var editTag =
        '<a href="/Organization/QuickAdd?id=' + row.id + '" '
        + ' class="" target="_blank" > ' + value + '</a>';
    return editTag;
}
//#endregion

//#region Delete Action
function deleteAction(value, row) {
    var buttonDelete =
        '<button '
        + 'onclick = "deleteTax(' + row.id + ')" '
        + 'dataId = "' + row.id + '" class="btn btn-danger btn-outline-danger btn-icon" > '
        + _deleteIcon + '</button>';
    buttonDelete =
        '<i class="icofont icofont-ui-delete" style="color:red;" '
        + 'onclick = "deleteTax(' + row.id + ')" '
        + '></i>';
    return buttonDelete;
}
//#endregion