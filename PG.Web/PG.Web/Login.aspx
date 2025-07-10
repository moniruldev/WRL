<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PG.Web.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/form.css" rel="stylesheet" type="text/css" />
    <script src="javascript/jquery-latest.min.js" type="text/javascript"></script>
    <script src="javascript/DetectPrivateMode.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="https://cdnjs.cloudflare.com/ajax/libs/normalize/5.0.0/normalize.min.css"/>
    
 <style type="text/css">
body {
  background:url('/image/BG.png');
  width:100%;
  height:100%;
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover;
  opacity:0.8;
  font-size: 1.6rem;
  font-family: "Open Sans", sans-serif;
  color: #2b3e51;
}

h2 {
  font-weight: 300;
  text-align: center;
  color:#3ca9e2;
}

p {
  position: relative;
}

a,
a:link,
a:visited,
a:active {
  color: #3ca9e2;
  -webkit-transition: all 0.2s ease;
  transition: all 0.2s ease;
}
a:focus, a:hover,
a:link:focus,
a:link:hover,
a:visited:focus,
a:visited:hover,
a:active:focus,
a:active:hover {
  color: #329dd5;
  -webkit-transition: all 0.2s ease;
  transition: all 0.2s ease;
}

#login-form-wrap {
  background-color: #fff;
  width: 35%;
  margin: 30px auto;
  text-align: center;
  padding: 20px 0 0 0;
  border-radius: 4px;
  box-shadow: 0px 30px 50px 0px rgba(0, 0, 0, 0.2);
}

#loginform {
  padding: 0 60px;
}

input {
  display: block;
  box-sizing: border-box;
  width: 100%;
  outline: none;
  height: 60px;
  line-height: 60px;
  border-radius: 4px;
}

input[type="text"],
input[type="password"] {
  width: 100%;
  padding: 0 0 0 10px;
  margin: 0;
  color: black;
  border: 1px solid #c2c0ca;
  font-style: normal;
  font-size: 16px;
  -webkit-appearance: none;
     -moz-appearance: none;
          appearance: none;
  position: relative;
  display: inline-block;
  background: none;
}
input[type="text"]:focus,
input[type="password"]:focus {
  border-color: #3ca9e2;
   /*box-shadow: 0 0 10px #719ECE;*/
}

input[type="text"]:hover,
input[type="password"]:hover {
  border-color: #3ca9e2;
   box-shadow: 0 0 10px #719ECE;
}
input[type="text"]:focus:invalid,
input[type="password"]:focus:invalid {
  color: #cc1e2b;
  border-color: #cc1e2b;
}
input[type="text"]:valid ~ .validation,
input[type="password"]:valid ~ .validation {
  display: block;
  border-color: #0C0;
}
input[type="text"]:valid ~ .validation span,
input[type="password"]:valid ~ .validation span {
  background: #0C0;
  position: absolute;
  border-radius: 6px;
}
input[type="text"]:valid ~ .validation span:first-child,
input[type="password"]:valid ~ .validation span:first-child {
  top: 30px;
  left: 14px;
  width: 20px;
  height: 3px;
  -webkit-transform: rotate(-45deg);
          transform: rotate(-45deg);
}
input[type="text"]:valid ~ .validation span:last-child,
input[type="password"]:valid ~ .validation span:last-child {
  top: 35px;
  left: 8px;
  width: 11px;
  height: 3px;
  -webkit-transform: rotate(45deg);
          transform: rotate(45deg);
}

.validation {
  display: none;
  position: absolute;
  content: " ";
  height: 60px;
  width: 30px;
  right: 15px;
  top: 0px;
}

input[type="submit"] {
  border: none;
  display: block;
  background-color: #3ca9e2;
  color: black;
  font-weight: bold;
  text-transform: uppercase;
  cursor: pointer;
  -webkit-transition: all 0.2s ease;
  transition: all 0.2s ease;
  font-size: 18px;
  position: relative;
  display: inline-block;
  cursor: pointer;
  text-align: center;
}
input[type="submit"]:hover {
  background-color: #329dd5;
  -webkit-transition: all 0.2s ease;
  transition: all 0.2s ease;
  box-shadow: 0 0 10px #719ECE;
}

#create-account-wrap {
  background-color: #eeedf1;
  color: #8a8b8e;
  font-size: 14px;
  width: 100%;
  padding: 10px 0;
  border-radius: 0 0 4px 4px;
}
#loginImg{
    border:1px solid #3ca9e2;
    border-radius:4px;
    transition: transform .2s;
}


 #loginImg:hover
  {
   box-shadow: 0 0 10px #719ECE;
   transform: scale(1.03);
  }
</style>


     <script type="text/javascript">
 // <!CDATA[

        var hdnBrowserSupported = '<%=hdnBrowserSupported.ClientID%>';
        var hdnBrowserPrivateMode = '<%=hdnBrowserPrivateMode.ClientID%>';

    <%--    var hdnCompanyID = '<%=hdnCompanyID.ClientID%>';
        var hdnLocationID = '<%=hdnLocationID.ClientID%>';
        var txtLocationCode = '<%=txtLocationCode.ClientID%>';
        var txtLocationName = '<%=txtLocationName.ClientID%>';

        var txtUser = '<%=txtUser.ClientID%>';
        var txtPassword = '<%=txtPassword.ClientID%>';

        var btnLoginUnID = '<%=btnLogin.UniqueID%>';--%>



        var GetJSonDataServiceLink = '<%=this.GetJSonDataServiceLink%>';


        $(document).ready(function () {
            //alert($("#hdnPass").val());
            
            if ($("#hdnIsDebug").val() == '1') {
                return;
            }


            $("#dvMain").hide();


            if ($("#" + hdnBrowserSupported).val() == '1') {

                if ($("#" + hdnBrowserPrivateMode).val() == '1') {

                    detectPrivateMode(
                              function (is_private) {

                                  var isPrivate = false;
                                  if (typeof is_private === 'undefined') {
                                      isPrivate = false;
                                  } else if (is_private) {
                                      isPrivate = true;
                                  }
                                  else {
                                      isPrivate = false;
                                  }

                                  // document.getElementById('spPrivateMode').innerHTML = typeof is_private === 'undefined' ? 'cannot detect' : is_private ? 'private' : 'not private';

                                  if (isPrivate == false) {
                                      $("#dvMain").hide();

                                      //$("#spPrivateModeText").text('you are not in private/incognito mode. cannot contiue.')
                                      $("#spPrivateModeText").text('This site cannot be reached. Error!!')

                                      alert('This site cannot be reached. Error!!');
                                  }
                                  else {
                                      $("#dvMain").show();
                                  }

                              }
                      );
                }
                else {
                    $("#dvMain").show();
                }
            }
            else {
                $("#spPrivateModeText").text('This site cannot be reached. Error!!!')

                alert('This site cannot be reached. Error!!!');
            }

        });



        $(document).ready(function () {
            //alert($("#hdnPass").val());
              
            if ($("#hdnIsDebug").val() == '1') {
                $("#txtUser").val($("#hdnUserID").val());
                $("#txtPassword").val($("#hdnPass").val());
            }

            //if ($("#txtUser").val() == '') {
            //    $("#txtUser").focus();
            //}
            //else {
            //    $("#txtPassword").focus();
            //}

            //$("#" + hdnLocationID).val('0');
           // $("#" + txtLocationCode).val('');
            //$("#" + txtLocationName).val('');

            $("#" + txtLocationCode).focus();

            $("#" + txtLocationCode).keypress(function (e) {
                $("#" + txtLocationName).val('');
                $("#" + hdnLocationID).val('0');
            });

            $("#" + txtLocationCode).blur(function (e) {
                if ($("#" + txtLocationCode).val().trim() == '') {

                }
                else {
                    if (getLocationData() == false) {
                        alert('Location Not Found!');
                        //$("#" + txtLocationCode).focus();

                        //var self = $(this);
                        //setTimeout(function () { self.focus(); }, 50);
                    }
                }
            });


            //$("#" + txtLocationCode).focusout(function (e) {
            //    if ($("#" + txtLocationCode).val().trim() == '') {

            //    }
            //    else {
            //        if (getLocationData() == false) {
            //            //$(this).focus();
            //            e.preventDefault;
            //            //e.stopPropagation;
            //            alert('Location Not Found!');
            //            //$("#" + txtLocationCode).focus();
            //            //e.preventDefault;
            //            var self = $(this);
            //            setTimeout(function () { self.focus(); }, 50);
            //        }
            //    }
            //});


            $("#" + txtLocationCode).keydown(function (e) {
                //getData();

                if (e.keyCode == 13) {
                    $("#" + txtUser).focus();
                }
               
            });


            $("#" + txtUser).keydown(function (e) {
                //getData();
                if (e.keyCode == 13) {
                    $("#" + txtPassword).focus();
                }
            });


            $("#" + txtPassword).keydown(function(e){
                if (e.keyCode == 13)
                {
                    __doPostBack(btnLoginUnID, '');
                }
            });
        });


        function getLocationData()
        {
            //alert(1);
            var bReturn = true;
            var locCode = $("#" + txtLocationCode).val();
            var compID = $("#" + hdnCompanyID).val();

            if (locCode == '')
            {
                alert('Please enter locaction code');
                return false;
            }

            if(compID == '' || compID == '0')
            {
                alert('Please enter company code');
                return false
            }

            var locObj = GetLocation(locCode, compID)

            if (locObj != null)
            {
                //alert(locObj);
                //alert(locObj.name);
                //alert('data found');
                $("#" + txtLocationName).val(locObj.name);
                $("#" + hdnLocationID).val(locObj.id);
                bReturn = true;
            }
            else
            {
                $("#" + txtLocationName).val('');
                $("#" + hdnLocationID).val('0');
                bReturn = false;
                //alert('Location Not Found!');
            }
            return bReturn;

        }

        function GetLocation(locCode, compID) {

            locCode == locCode || '';
            compID == compID || 1;

            var locObj = null;
            var isError = false;
            var isComplete = false;
            //ajax call

            //var companyid = $('#' + hdnCompanyID).val();

            var taskName = "location";

            var serviceURL = GetJSonDataServiceLink + "?task=" + taskName;
            serviceURL += "&companyid=" + compID;
            serviceURL += "&locationcode=" + locCode;

            //alert(serviceURL);

            var dummyVar = $.ajax({
                type: "GET",
                cache: false,
                async: false,
                dataType: "json",
                url: serviceURL,
                //data: strData,
                //data: '{name: "' + locCode + '" }',
                success: function (locdata) {
                    //            if (accdata.menu[0].count > 0) {
                    //                menu = menudata.menu[0];
                    //            }
                    //alert(locdata.totalpage);
                    if (locdata.rows.length > 0) {
                        locObj = locdata.rows[0];
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
                    alert(textStatus);
                }
            });
            return locObj;
        }

 
 
 // ]]>
</script>

</head>
<body >
    <div id="login-form-wrap">
 
    <div>
     <%--<asp:Image ID="loginImg" runat="server" ImageUrl="image/ayaterp1.png"  Style="" />--%>
            <h2>Cuorier Management</h2>
   </div>
 
  <form id="loginform" runat="server">
    <p>
        <asp:TextBox runat="server" id="txtUser" name="username" placeholder="Username" required ></asp:TextBox><i class="validation"><span></span><span></span></i>
    </p>
     <p>
        <asp:TextBox runat="server" id="txtPassword" name="password" placeholder="Password" EnableViewState="False"  TextMode="Password" required ></asp:TextBox><i class="validation"><span></span><span></span></i>
    </p>
 
    <p>
        <asp:Button runat="server" id="btnLogin" value="Login" Text="Login" OnClick="btnLogin_Click" />
    </p>
            <div id="dvLoginHidden" style="visibility:hidden">
               <asp:HiddenField ID="hdnBrowserSupported" runat="server" Value = "0" />
               <asp:HiddenField ID="hdnBrowserPrivateMode" runat="server" Value = "0" />
               <asp:HiddenField ID="hdnCompanyID" runat="server" Value="0" />
               <asp:HiddenField ID="hdnLocationID" runat="server" Value="0" />
               <asp:HiddenField ID="hdnIsDebug" runat="server" Value = "0" />
               <asp:HiddenField ID="hdnUserID" runat="server" Value = "" /> 
               <asp:HiddenField ID="hdnPass" runat="server" Value = "" />
           </div>
  </form>
  <div id="create-account-wrap">
  <asp:Label ID="lblStatus" runat="server" ForeColor="Red" Font-Size="10"></asp:Label>
  <asp:TextBox ID="txtLocationCode" runat="server" CssClass="textBox" Style="width: 42px;height:20px;" Text="00" TabIndex="1" Visible="false"></asp:TextBox>
  </div><!--create-account-wrap-->
</div>

            
     
    
</body>
</html>
