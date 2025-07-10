$(document).on('keyup', '.numberOnly', function (e) {
    if (!$.isNumeric($(this).val())) {
        $(this).val('');
        $(this).addClass('has-warning');
        return false;
    } else {
        $(this).removeClass('has-warning');
        return false;
    }
});

function ValidateInput(divId) {
    debugger;
    var isValid = true;
    if (divId != "") {
        $(divId + " .required").each(function () {
            if ($(this).val() != "" && $(this).val().length > 0) {
                $(this).removeClass('has-warning');
            } else {
                isValid = false;
                $(this).addClass('has-warning');
            }
        })
    }
    if (isValid) {
        return true;
    }
    return false;

}



function ResetMessage() {
    $('#' + lblMessage).text("");
}
function SetMessage(value, color) {
    $('#' + lblMessage).text(value);
    $('#' + lblMessage).css('color', color);
}
function ResetAndSetMessage(controlName, value, color) {
    $('#' + lblMessage).text("");
    if (controlName == "S") {
        $('#' + lblMessage).text(value);
        $('#' + lblMessage).css('color', color);
    }
}

$(document).on('click', '.deleteTempRow', function () {
    var parentTableId = $(this).closest('table').attr('id');
    $(this).parent().parent().remove();
    GenerateNewSlNo(parentTableId);
});

function GenerateNewSlNo(parentTableId) {
    var newSl = 1;
    $("#" + parentTableId + " .slno").each(function () {
        $(this).text(newSl);
        newSl = newSl + 1;
    });
}

function ResetInput(divId) {
    $('#' + divId + " .required").removeClass('has-warning');
    $('#' + divId + " input[type=checkbox]").attr('checked', false);
    $('#' + divId + " input[type='text']").val("");   
    $('#' + divId + " input[type=radio]").attr('checked', false);
  
}

function Resetddl(divId) {
    $('#' + divId + " select option:first-child").attr('selected', 'selected');   
}
function ResetddlById(ddlId) {
    $('#' + ddlId + " option:first-child").attr('selected', 'selected');
}
function ResetCheckBoxAndRadioById(id) {
    $('#'+ id).attr('checked', false);
}

function ResetTextAreaById(id) {
    $('#' + id).val("");
}

function ResetTable(tblId,isShow) {
    $('#' + tblId).find('tbody').empty();
    if(isShow)
    {
        $('#' + tblId).show();
    } else {
        $('#' + tblId).hide();
    }
}

var monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];

function GetCurrentDate() {

    var date = new Date();
    var day = Number(date.getDate());
    if (day < 10) {
        day = "0" + day;
    }
    var monthName = monthNames[date.getMonth()];
    var year = date.getFullYear();
    var fStart = day + '-' + monthName + '-' + year;
    return fStart;
}
function GetJsonDate(dateToFormat) {

    if (dateToFormat != null) {
        var date = new Date(parseInt(dateToFormat.replace("/Date(", "").replace(")/", ""), 10));
        var day = Number(date.getDate()) + 1;
        if (day < 10) {
            day = "0" + day;
        }
        var monthName = monthNames[date.getMonth()];
        var year = date.getFullYear();
        var fStart = day + '-' + monthName + '-' + year;
        return fStart;
    } else {
        return "";
    }


}