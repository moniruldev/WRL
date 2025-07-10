/* File Created: October 18, 2014 */


var LoginTask = new function () {
    var $dlgcontrol;

    this.CompanyID = 1;
    this.LoginSilentURL = "loginsilent.ashx";
    this.LoginFormText = "";
    this.IsLoginExpired = false;

    this.KeepLiveURL = "keeplive.ashx";


    this.ShowLogin = function (pUserID, pPassword) {
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
                    self.OkClicked();
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


    this.OkClicked = function () {
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


    this.CheckAndLoginAjax = function (XMLHttpRequest, textStatus, pUserID) {
        var self = this;
        self.IsLoginExpired = false;
        var contentType = XMLHttpRequest.getResponseHeader("Content-Type");
        if (XMLHttpRequest.status === 200 && contentType.toLowerCase().indexOf("text/html") >= 0) {
            // assume that our login has expired - reload our current page
            //window.location.reload();
            self.ShowLogin(pUserID, '')
            self.IsLoginExpired = true;
        }
        else {
            if (textStatus != '') {
                alert(textStatus);
            }
        }
    }

    ////
}                                             ///
