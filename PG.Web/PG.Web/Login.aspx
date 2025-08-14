<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PG.Web.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <title></title>
  <%--  <link href="css/form.css" rel="stylesheet" type="text/css" />--%>
    <script src="javascript/jquery-latest.min.js" type="text/javascript"></script>
    <script src="javascript/DetectPrivateMode.js" type="text/javascript"></script>
    <link href="https://fonts.googleapis.com/css2?family=Outfit:wght@400;600&display=swap&family=DM+Serif+Display&display=swap" rel="stylesheet" />

       <!-- Bootstrap and Fonts -->
    <link href="dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="plugins/fontawesome-free/css/all.min.css" rel="stylesheet" />
      <link href="css/toastr.css" rel="stylesheet" />
     <script src="javascript/toastr.js" type="text/javascript"></script>
    <style>

         body, input, button {
            font-family: 'Outfit', sans-serif;
        }
        .logo-text, .card-header h1 {
            font-family: 'DM Serif Display', serif;
        }

        .splash-container {
           
            max-width: 1000px;
            width:100%;
            height:100%;
        }
        html, body {
            height: 100%;
        }
        .auth-boxs {
            border:none;
        }
        .logo-img {
            max-height: 50px;
        }
        .footer-img img {
            padding: 10px;
        }
        .card-header {
   
            border-bottom:none;
            background-color: white !important;
        }
        .form-check-input {
            margin-top: 3px !important;
             border-radius: 10px !important;
        }
        .form-check {
            font-size: 0.875rem !important;
        }
      
    </style>

     <script type="text/javascript">
         // <!CDATA[

         var hdnBrowserSupported = '<%=hdnBrowserSupported.ClientID%>';
         var hdnBrowserPrivateMode = '<%=hdnBrowserPrivateMode.ClientID%>';

         var GetJSonDataServiceLink = '<%=this.GetJSonDataServiceLink%>';
         var txtUser = '<%= txtUser.ClientID %>';
         var txtPassword = '<%= txtPassword.ClientID %>';
         var btnLoginID = '<%= btnLogin.ClientID %>';


         $(document).ready(function () {
             //alert($("#hdnPass").val());

             if ($("#hdnIsDebug").val() == '1') {
                 return;
             }


             $("#dvMain").hide();

         });

         function showToastr(type, message, title) {
             toastr.options = {
                 "closeButton": true,
                 "progressBar": true,
                 "positionClass": "toast-top-right",
                 "timeOut": "3000"
             };

             toastr[type](message);
         }

         $(document).ready(function () {
             //alert($("#hdnPass").val());

             if ($("#hdnIsDebug").val() == '1') {
                 $("#txtUser").val($("#hdnUserID").val());
                 $("#txtPassword").val($("#hdnPass").val());
             }



             $("#" + txtUser).keydown(function (e) {
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

         // ]]>
</script>

</head>
<body >

  <form id="loginform" runat="server">
<div class="container-fluid min-vh-100 d-flex justify-content-center align-items-center">
  <div class="row h-100 justify-content-center align-items-center">
    <div class="splash-container">
      <div class="card p-4">
        <div class="row no-gutters">
          <div class="col-lg-5">
            <div class="card auth-boxs">
              <div class="card-header text-center">
                <img class="logo-img" src="image/wr.png" alt="Logo" />
              </div>
              <div class="card-body">
                <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />
                <div class="form-group">
                  <asp:TextBox ID="txtUser" runat="server" CssClass="form-control form-control-lg" placeholder="Enter Username"></asp:TextBox>
                </div>
                <div class="form-group">
                  <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control form-control-lg" placeholder="Enter Password"></asp:TextBox>
                </div>
                <div class="form-group form-check">
                  <asp:CheckBox ID="chkRememberMe" runat="server" CssClass="form-check-input" />
                  <label class="form-check-label" for="chkRememberMe">Remember Me</label>
                </div>
                <asp:Button ID="btnLogin" runat="server" Text="Log in" CssClass="btn btn-primary btn-lg btn-block" OnClick="btnLogin_Click" />
              </div>
              <div class="card-footer bg-white p-2 border-0 d-flex justify-content-end">
                <div class="card-footer-item card-footer-item-bordered">
                  <a href="#" class="footer-link">Forgot Password?</a>
                </div>
              </div>
            </div>
          </div>
          <div class="col-lg-7 footer-img">
            <img src="image/we-courier-process.png" class="img-fluid" />
          </div>
        </div>
      </div>
    </div>
  </div>
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
  </form>
  <div id="create-account-wrap">
  <asp:Label ID="lblStatus" runat="server" ForeColor="Red" Font-Size="10" Visible="false"></asp:Label>
  <asp:TextBox ID="txtLocationCode" runat="server" CssClass="textBox" Style="width: 42px;height:20px;" Text="00" TabIndex="1" Visible="false"></asp:TextBox>
  </div>

    
</body>
</html>
