function displayErrors(errors, val) {
    for (var i = 0; i < errors.responseJSON.length; i++) {        
        var inputName = errors.responseJSON[i].fieldKey;
        var dotPos = inputName.indexOf(".");
        if (dotPos > 0) {
            var name = inputName.substr(dotPos + 1);
        }
        else {
            name = inputName;
        }
        var errorMsg = errors.responseJSON[i].errorMessage;
        $("#" + name + val).html(errorMsg);
    }
}