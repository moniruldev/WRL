<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login_Bak.aspx.cs" Inherits="PG.Web.Login_Bak" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/form.css" rel="stylesheet" type="text/css" />
    <script src="javascript/jquery-latest.min.js" type="text/javascript"></script>
    <script src="javascript/DetectPrivateMode.js" type="text/javascript"></script>

    <style type="text/css">


        html {
          height: 100%;
        }

        body {
          display: flex;
          flex-direction: column;
          justify-content: center;
          align-items: center;
          position: relative;
          min-height: 100%;
          background: #F1F1F1;
        }


        /* Animation Keyframes */
        @-webkit-keyframes scale_header {
          0%   {max-height: 0px; margin-bottom: 0px; opacity: 0;}
          100% {max-height: 117px; margin-bottom: 25px; opacity: 1;}
        }
        @keyframes scale_header {
          0%   {max-height: 0px; margin-bottom: 0px; opacity: 0;}
          100% {max-height: 117px; margin-bottom: 25px; opacity: 1;}
        }

        @-webkit-keyframes input_opacity {
          0%   {-webkit-transform: translateY(-10px);transform: translateY(-10px); opacity: 0}
          100% {-webkit-transform: translateY(0px);transform: translateY(0px); opacity: 1}
        }

        @keyframes input_opacity {
          0%   {-webkit-transform: translateY(-10px);transform: translateY(-10px); opacity: 0}
          100% {-webkit-transform: translateY(0px);transform: translateY(0px); opacity: 1}
        }

        @-webkit-keyframes text_opacity {
          0% {color: transparent;}
        }

        @keyframes text_opacity {
          0% {color: transparent;}
        }

        @-webkit-keyframes error_before {
          0%   {height: 5px; background: rgba(0, 0, 0, 0.156); color: transparent;}
          10%  {height: 117px; background: #FFFFFF; color: #C62828}
          90%  {height: 117px; background: #FFFFFF; color: #C62828}
          100% {height: 5px; background: rgba(0, 0, 0, 0.156); color: transparent;}
        }

        @keyframes error_before {
          0%   {height: 5px; background: rgba(0, 0, 0, 0.156); color: transparent;}
          10%  {height: 117px; background: #FFFFFF; color: #C62828}
          90%  {height: 117px; background: #FFFFFF; color: #C62828}
          100% {height: 5px; background: rgba(0, 0, 0, 0.156); color: transparent;}
        }

        .login-container {
          display: flex;
          flex-direction: column;
          align-items: center;
          position: relative;
          width: 430px;
          height: auto;
          padding: 5px;
          box-sizing: border-box;
        }

        .login-container img {
          /*width: 200px;
          margin: 0 0 20px 0;
              */
        }

        .login-container p {
          align-self: flex-start;
          font-family: 'Roboto', sans-serif;
          font-size: 0.8rem;
          color: rgba(0, 0, 0, 0.5);
        }

        .login-container p a {
          color: rgba(0, 0, 0, 0.4);
        }
       

        .login-form {
          padding: 15px;
          box-sizing: border-box;
        }

        .login {
          position: relative;
          width: 100%;
          padding: 10px;
          margin: 0 0 10px 0;
          box-sizing: border-box;
          border-radius: 3px;
          background: #FAFAFA;
          overflow: hidden;
          -webkit-animation: input_opacity 0.2s cubic-bezier(.55, 0, .1, 1);
                  animation: input_opacity 0.2s cubic-bezier(.55, 0, .1, 1);
          box-shadow: 0 2px 2px 0 rgba(0, 0, 0, 0.14),
                      0 1px 5px 0 rgba(0, 0, 0, 0.12),
                      0 3px 1px -2px rgba(0, 0, 0, 0.2);
        }

        .login > header {
          position: relative;
          width: 100%;
          padding: 10px;
          margin: -10px -10px 5px -10px;

          border-bottom: 1px solid rgba(0, 0, 0, 0.1);
          background: #009688;
          /*font-family: 'Roboto', sans-serif/*;
          /*font-size: 1.3rem;*/
          color: #FAFAFA;
          -webkit-animation: scale_header 0.6s cubic-bezier(.55, 0, .1, 1), text_opacity 1s cubic-bezier(.55, 0, .1, 1);
                  animation: scale_header 0.6s cubic-bezier(.55, 0, .1, 1), text_opacity 1s cubic-bezier(.55, 0, .1, 1);
          box-shadow: 0px 2px 2px 0px rgba(0, 0, 0, 0.14),
                      0px 1px 5px 0px rgba(0, 0, 0, 0.12),
                      0px 3px 1px -2px rgba(0, 0, 0, 0.2);
        }

        .login > header:before {
          content: '';
          display: flex;
          justify-content: center;
          align-items: center;
          position: absolute;
          width: 100%;
          height: 5px;
          padding: 10px;
          margin: -10px 0 0 -10px;
          box-sizing: border-box;
          background: rgba(0, 0, 0, 0.156);
          font-family: 'Roboto', sans-serif;
          font-size: 0.9rem;
          color: transparent;
          z-index: 5;
        }

        .login.error_1 > header:before,
        .login.error_2 > header:before {
          -webkit-animation: error_before 3s cubic-bezier(.55, 0, .1, 1);
                  animation: error_before 3s cubic-bezier(.55, 0, .1, 1);
        }

        .login.error_1 > header:before {
          content: 'Invalid username or password!';
        }

        .login.error_2 > header:before {
          content: 'Invalid or expired Token!';
        }

        .login > header h2 {
          /*margin: 50px 0 10px 0;*/
          margin: 30px 0 0px 0;
        }

        .login > header h4 {
         /* font-size: 0.7em; */
          -webkit-animation: text_opacity 1.5s cubic-bezier(.55, 0, .1, 1);
                  animation: text_opacity 1.5s cubic-bezier(.55, 0, .1, 1);
          color: rgba(255, 255, 255, 0.4);
        }

        /* Form */
        .login-form {
          padding: 15px;
          box-sizing: border-box;
        }

        .submit-container {
          display: flex;
          flex-direction: row;
          justify-content: flex-end;
          position: relative;
          padding: 10px;
          border-top: 1px solid rgba(0, 0, 0, 0.1);
        }

         .submit-container22 {
          display: flex;
          flex-direction: row;
          justify-content: flex-end;
          position: relative;
          padding: 10px;
           /* margin: 35px -25px -25px -25px; */
         margin: 35px 0px 0px 0px;
          border-top: 1px solid rgba(0, 0, 0, 0.1);
        }

        .login-button {
          padding: 10px;
          border: none;
          border-radius: 3px;
          background: transparent;
          font-family: 'Roboto', sans-serif;
          font-size: 0.9rem;
          font-weight: 500;
          color: #E37F00;
          cursor: pointer;
          opacity: 1;
          -webkit-animation: input_opacity 0.8s cubic-bezier(.55, 0, .1, 1);
                  animation: input_opacity 0.8s cubic-bezier(.55, 0, .1, 1);
          transition: background 0.2s ease-in-out;
        }




     </style>


     <script type="text/javascript">
         // <!CDATA[

         var hdnBrowserSupported = '<%=hdnBrowserSupported.ClientID%>';
         var hdnBrowserPrivateMode = '<%=hdnBrowserPrivateMode.ClientID%>';

         var hdnCompanyID = '<%=hdnCompanyID.ClientID%>';
         var hdnLocationID = '<%=hdnLocationID.ClientID%>';
         var txtLocationCode = '<%=txtLocationCode.ClientID%>';
         var txtLocationName = '<%=txtLocationName.ClientID%>';

         var txtUser = '<%=txtUser.ClientID%>';
         var txtPassword = '<%=txtPassword.ClientID%>';

         var btnLoginUnID = '<%=btnLogin.UniqueID%>';



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


             $("#" + txtPassword).keydown(function (e) {
                 if (e.keyCode == 13) {
                     __doPostBack(btnLoginUnID, '');
                 }
             });
         });


         function getLocationData() {
             //alert(1);
             var bReturn = true;
             var locCode = $("#" + txtLocationCode).val();
             var compID = $("#" + hdnCompanyID).val();

             if (locCode == '') {
                 alert('Please enter locaction code');
                 return false;
             }

             if (compID == '' || compID == '0') {
                 alert('Please enter company code');
                 return false
             }

             var locObj = GetLocation(locCode, compID)

             if (locObj != null) {
                 //alert(locObj);
                 //alert(locObj.name);
                 //alert('data found');
                 $("#" + txtLocationName).val(locObj.name);
                 $("#" + hdnLocationID).val(locObj.id);
                 bReturn = true;
             }
             else {
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
<body>
    <form id="form1" runat="server">
     <div class="login-container">
          <section class="login" id="login">
              <header>
                  <div style="height:15px;"></div>
                  <div style="width:50%; margin-right:auto;margin-left:auto;text-align:center;">
                      <asp:Image ID="Image1" runat="server" 
									style="top:2px;" 
									ImageAlign="AbsMiddle" ImageUrl="image/comlogosmall2.png" Width="150px" Height="40px"
									BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="0px"  />
                  </div>
                  <div style="height:10px;"></div>
                   <div style="width:80%; margin-right:auto;margin-left:auto;text-align:center;">
              <asp:Label ID="lblAppName" runat="server" Text="SPB Inventory" 
                           Font-Bold="True" Font-Size="13pt" ForeColor="White"></asp:Label>
<br />
                   <asp:Label ID="lblVersion" runat="server" ForeColor="White" ></asp:Label> 
                        </div>
                  <div style="height:5px;"></div>
                  <span style="font-size:12pt;font-weight:bold;">Login</span>
                 <div style="height:3px;"></div>
            </header>
              <div style="height:10px;">

           </div>
           <div id="dvLoginControls">
                 <table>
                     <tr style="height: auto;">
                         <td style="width:100px;"></td>
                         <td  align="right">
                             <asp:Label ID="lblLocation" runat="server" Font-Bold="True"
                                 Style="" Text="Location"></asp:Label>
                         </td>
                         <td style="padding-left: 2px;">
                             <asp:TextBox ID="txtLocationCode" runat="server" CssClass="textBox" Style="width: 42px;height:20px;" TabIndex="1"></asp:TextBox>

                         </td>
                         <td>
                             <asp:TextBox ID="txtLocationName" runat="server" CssClass="textBox" Style="width: 125px;height:20px;" TabIndex="-1"></asp:TextBox>
                         </td>
                     </tr>

                     <tr style="height:5px;">
                         <td style="width:2px;">

                         </td>
                     </tr>

                     <tr>
                         <td ></td>
                         <td align="right">
                             <asp:Label ID="Label3" runat="server" Font-Bold="True"
                                 Style="" Text="User ID"></asp:Label>
                         </td>
                         <td colspan="2" style="padding-left: 2px;">
                             <asp:TextBox ID="txtUser" runat="server" CssClass="textBox" MaxLength="25" Style="width: 172px;height:20px;" TabIndex="1" ValidationGroup="UserID"></asp:TextBox>

                         </td>
                         <td ></td>
                     </tr>

                     <tr style="height: auto;">
                         <td></td>
                         <td></td>
                         <td colspan="2" style="padding-left: 2px;">
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUser"
                                 EnableViewState="False" ErrorMessage="User ID Required!!" Font-Size="Smaller" SetFocusOnError="True"
                                 Style="" ValidationGroup="UserID"></asp:RequiredFieldValidator>

                         </td>
                         <td></td>
                     </tr>

                     <tr style="height: auto;">
                         <td></td>
                         <td align="right">
                             <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                 Style="" Text="Password"></asp:Label>

                         </td>
                         <td colspan="2" style="padding-left: 2px;">
                             <asp:TextBox ID="txtPassword" runat="server" CssClass="textBox" EnableViewState="False" Style="width: 172px;height:20px;" TabIndex="2" TextMode="Password" ValidationGroup="UserID">abcd</asp:TextBox>

                         </td>
                         <td></td>
                     </tr>
                 </table>
                 
           </div>
           <div style="height:15px;">

           </div>
           <div class="submit-container">
                 <asp:Button ID="btnLogin" CssClass="buttoncommon"  runat="server" Style=""
                                 Text="Login" Width="65px" OnClick="btnLogin_Click" UseSubmitBehavior="False" ValidationGroup="UserID" TabIndex="3" />
           </div>

           </section>
           <p>&copy; Saif Powertec Ltd</p>
          <div id="dvLoginStatus" style="height:auto;width:100%;text-align:center;">
              <asp:Label ID="lblStatus" runat="server" ForeColor="Red" Font-Size="10"></asp:Label>
           </div>
         <div id="dvNoScript">
              <noscript>
                 <span style="color:Red;font-weight:bold;font-size:medium">please enable javascript</span>
               </noscript>
           </div>
            
           <div id="dvLoginHidden" style="visibility:hidden">
               <asp:HiddenField ID="hdnBrowserSupported" runat="server" Value = "0" />
               <asp:HiddenField ID="hdnBrowserPrivateMode" runat="server" Value = "0" />
               <asp:HiddenField ID="hdnCompanyID" runat="server" Value="0" />
               <asp:HiddenField ID="hdnLocationID" runat="server" Value="0" />
               <asp:HiddenField ID="hdnIsDebug" runat="server" Value = "0" />
               <asp:HiddenField ID="hdnUserID" runat="server" Value = "" /> 
               <asp:HiddenField ID="hdnPass" runat="server" Value = "" />
           </div>
      </div>
    </form>
</body>
</html>

