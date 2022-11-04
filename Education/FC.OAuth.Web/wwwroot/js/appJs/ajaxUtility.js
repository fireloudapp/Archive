var _authToken;
$(function () {
    fnGetUserToke(); // assing the global token value
    console.log("user token assigned.");
});

function fnGetUserToke() {
    var token = Cookies.get('UserToken');
    _authToken = "Bearer " + token;
    return _authToken;
}


function fnInvokeAjaxDelete(urlValue, successFn, failFn) {
    var ajaxSetting = {
        "url": urlValue,
        "method": "DELETE",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json",
            "Authorization": fnGetUserToke()
        }
    };
    $.ajax(ajaxSetting)
        .done(successFn)
        .fail(failFn);
}


function fnInvokeAjaxPost(urlValue, inputData, successFn, failFn) {
    var ajaxSetting = {
        "url": urlValue,
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json",
            "Authorization": fnGetUserToke()
        },
        "data": inputData,
    };

    $.ajax(ajaxSetting)
        .done(successFn)
        .fail(failFn);
    //.done(function (data) { successFn(data); })
    //.fail(function (jqXHR, textStatus, errorThrown) { serrorFunction(); });
}

function fnInvokeAjaxGET(urlValue, successFn, failFn) {
    var token = Cookies.get('UserToken');
    var authToken = "Bearer " + token;

    var ajaxSetting = {
        "url": urlValue,
        "method": "GET",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json",
            "Authorization": authToken
        }
    };

    $.ajax(ajaxSetting)
        .done(successFn)
        .fail(failFn);
    //.done(function (data) { successFn(data); })
    //.fail(function (jqXHR, textStatus, errorThrown) { serrorFunction(); });
}


function fnAjaxFailCase(jqXHR, textStatus, errorThrown) {

}

function fnAjaxSuccesCase(content) {

}