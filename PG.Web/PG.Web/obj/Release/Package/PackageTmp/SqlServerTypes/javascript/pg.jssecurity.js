
//Version: 1.2.0
//Date: March 02, 2014



/// <reference path="jquery-latest.min.js" />
/// <reference path="jquery-ui-latest.min.js" />

/// <reference path="PG.enums.js" />


var JSSecurity = new function () {

    this.AppID = 0;
    this.CompanyID = 1;

    this.RootPath = "";

    this.GetDataInfoURL = "Service/GetDataInfo.ashx";
    this.GetObjectPermissionURL = "Service/Systems/GetObjectPermission.ashx";

    this.LoginUserID = 0;
    this.LoginUserName = "";

    this.LoginRoleID = 0;
    this.LoginRoleName = "";

    var $dlgcontrol;

    this.LoginSilentURL = "loginsilent.ashx";
    this.LoginFormText = "";
    this.IsLoginExpired = false;

    this.KeepLiveURL = "keeplive.ashx";

    this.PermissionEnum = { None: 0, Read: 1, Add: 2, Edit: 4, Delete: 8
                         , Execute: 16, Enabled: 32, Visible: 64, Full: 127
    };


    this.GetObjectPermissionInfo = function (pAppID, pUserID, pObjectID, pSeek) {
        var self = this;
        var params = "?appid=" + pAppID + "&userid=" + pUserID + "&objectid=" + pObjectID + "&seekperm=" + pSeek; ;
        var serviceURL = self.RootPath + self.GetObjectPermissionURL + params;
        var permInfo = null;

        var dummyVar = $.ajax({
            type: "GET",
            cache: false,
            async: false,
            dataType: "json",
            url: serviceURL,
            success: function (data) {
                permInfo = data.info;
                //permInfo('ddd');
            },
            complete: function () {
                //if (!isError) {
                //return;
                //alert (menu.name);
                //}
                //isComplete = true;
                //alert(permInfo.permgranted);
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                //isError = true;
                alert(textStatus);
            }
        });
        dummyVar = null;

        return permInfo;
    };


    this.CheckObjectPermission = function (pAppID, pUserID, pObjectID, pSeek) {

        var self = this;
        var retValue = false;

        var perminfo = self.GetObjectPermissionInfo(pAppID, pUserID, pObjectID, pSeek);

        if (perminfo == null) {
            alert('Permission Denied!');
            return;
        }

        if (perminfo.permgranted == 1) {
            retValue = true;
        }
        else {
            alert(perminfo.permmsg);
        }

        //$("<div>This is a test</div>").dialog();
        //alert(permInfo.permmsg);

        //return permInfo;
    };



    this.ShowLoginDialog = function (pUserID, pPassword) {
        var self = this;

        //CompanyID = pCompanyID;

        var htmlForm = "<div><table>";

        if (pUserID != '') {
            htmlForm += "<tr><td align=\"right\">User ID :</td><td><input type=\"text\"  readonly=\"readonly\"  class=\"textBox txtUserID\" value=\"" + pUserID + "  \" /></td></tr>";
        }
        else {
            htmlForm += "<tr><td align=\"right\">User ID :</td><td><input type=\"text\" class=\"textBox txtUserID\" /></td></tr>";
        }

        htmlForm += "<tr><td align=\"right\">Password :</td><td><input type=\"password\" class=\"textBox txtPassword\" /></td></tr>";
        htmlForm += "<tr></tr>";

        htmlForm += "<tr><td></td><td align=\"right\"><input type=\"button\" class=\"buttoncommon btnOk stopEnterToTab\" value=\"Ok\"  /></td></tr>";

        htmlForm += "</table></div>";

        $dlgcontrol = $('<div />').html(htmlForm).dialog({
            title: "Login Expired!",
            autoOpen: true,
            resizable: false,
            position: 'center',
            modal: true,
            create: function (event, ui) {
                //alert('created');

                $(this).find('.btnOk').click(function (e) {
                    //                    alert('ok');
                    //                    dlg.dialog("close");
                    self.LoginOkClicked();
                });

                //alert($(ui.element).find('.btnOk').length);
            },
            open: function (event, ui) {

            },
            close: function (event, ui) {
                //on close event
                $dlgcontrol = null;
                $(this).remove();
            }
        });
        //dlg.dialog("open");
    };

    this.DoLogin = function (pUserID, pPassowrd) {
        var self = this;
        var isSuccess = false;
        var isError = false;
        var isComplete = false;
        //ajax call

        var serviceURL = self.LoginSilentURL + "?userid=" + pUserID;
        serviceURL += "&password=" + pPassowrd;

        var dummyVar = $.ajax({
            type: "GET",
            cache: false,
            async: false,
            dataType: "text",
            url: serviceURL,
            success: function (logindata) {
                if (logindata == 'ok') {
                    isSuccess = true
                }
            },
            complete: function () {
                if (!isError) {
                    //return;
                    //alert (menu.name);
                }
                isComplete = true;
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                isError = true;
                //alert(textStatus);
            }
        });
        return isSuccess;
    };


    this.LoginOkClicked = function () {
        var self = this;

        var userid = jQuery.trim($dlgcontrol.find('.txtUserID').val());
        var password = jQuery.trim($dlgcontrol.find('.txtPassword').val());

        if (userid == '') {
            alert('plase enter user id');
            $dlgcontrol.find('.txtUserID').focus();
            return;
        }

        if (password == '') {
            alert('plase enter passowrd');
            $dlgcontrol.find('.txtPassword').focus();
            return;
        }

        if (self.DoLogin(userid, password)) {
            self.IsLoginExpired = false;
            alert('Login Success! Now continue your task.');
            $dlgcontrol.dialog("close");
        }
        else {
            self.IsLoginExpired = true;
            alert('Login Failed!');
        }
    };


    this.CheckAndLoginAjax = function (XMLHttpRequest, textStatus) {
        var self = this;
        self.IsLoginExpired = false;
        var contentType = XMLHttpRequest.getResponseHeader("Content-Type");
        if (XMLHttpRequest.status === 200 && contentType.toLowerCase().indexOf("text/html") >= 0) {
            // assume that our login has expired - reload our current page
            //window.location.reload();
            self.ShowLoginDialog(self.LoginUserName, '')
            self.IsLoginExpired = true;
        }
        else {
            if (textStatus != '') {
                alert(textStatus);
            }
        }
    }




}             //end of class function