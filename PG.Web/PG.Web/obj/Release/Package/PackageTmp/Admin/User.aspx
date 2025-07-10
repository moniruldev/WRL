<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="PG.Web.Admin.User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
// <!CDATA[

var isPageResize = true;

function PageResizeCompleted(pg, cntMain) {
    resizeContentInner(cntMain);

}

function resizeContentInner(cntMain) {
    var contHeight = $("#dvContentMain").height();
    var contHead = $("#dvControlsHead").height();
    var contFooter = $("#dvControlsFooter").height();

    var contInnerHeight = contHeight - contHead - contFooter - 5;
    $("#dvControls").height(contInnerHeight);

    $("#dvControlsInner").height(contInnerHeight - 10);


    $("#groupBox").height(contInnerHeight - 10);
    var groupHeight = $("#groupBox").height();
    var groupHeaderHeight = $("#groupHeader").height();
    var groupFooterHeight = $("#groupFooter").height();
    $("#groupContent").height(groupHeight - groupHeaderHeight - groupFooterHeight - 2);

}




function tbopen(key)
{
     if(!key)
     {
       key = '';
     }
 
    
    var url = "/Admin/SetPassword.aspx?uid=" + key
    //if (pageInTab == 1)
    if (ZForm.PageMode == Enums.PageMode.InTab)
    {

       var tdata = new xtabdata();
       tdata.linktype = Enums.LinkType.Direct;
       tdata.id = 6320;
       tdata.name = "SetPassword";
       //tdata.label = "User: " + userid;
       tdata.label = "Set Password";
       tdata.type = 0;
       tdata.url = url;
       tdata.tabaction = Enums.TabAction.InTabReuse;
       tdata.selecttab = 1;
       tdata.reload = 0;
       tdata.param = "";
       
       
                             
       try
       {                                          
        window.parent.TabMenu.OpenMenuByData(tdata);
       }
       catch(err)
       {
           alert("error in page");
       }
   }
   else
   {
      //on new window/tab
       //window.open(url,'_blank');   
   
       window.location = url;
   }
}

function fromParent(val1)
{
    alert('this is called from parent: ' + val1);
}

function Button1_onclick() {
  //document.getElementById("btnSave").click();
  ///__doPostBack("<%= this.btnSave.UniqueID %>", "");
  __doPostBack("btnSave", "");
}

// ]]>
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div id="dvPageContent" style="width:100%; height:100%;">
   
    <div id="dvContentHeader" class="dvContentHeader">  
        <div id="dvHeader" class="dvHeader" >
            <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="User"></asp:Label>
        </div>
        <div id="dvMessage" class="dvMessage" >
            <asp:Label ID="lblMessage" runat="server" Text="" Font-Size="Small" Width="100%"></asp:Label>
        </div>
   </div>

   <div id="dvContentMain" class="dvContentMain"> 
    <div id = "dvControls" style="height:auto; width:100%">
         <div id="dvControlsInner" class="groupBoxContainer boxShadow">    
           <div id="groupBox">
             <div id="groupHeader" class="groupHeader">
                      <div style="width:100%;height:20px;">
                         <table>
                            <tr>
                             <td>
                                <div id="dvIconEditMode" class="iconView" runat="server" ></div>
                             </td>
                             <td>
                                <span>User</span> 
                             </td>
                            </tr>
                         </table>
                         
                      </div>
                      
                  </div>
             
             <div id="groupContent" class="groupContent" style="width:100%;height:300px; overflow:auto;">
               <div id="groupContenInner" style="width:100%;height:auto; text-align:left;">
                <table cellpadding="2" cellspacing="4">
               <tr>
                 <td style="width:20px;">
                 </td>
                 <td>
                 
                 </td>
                 <td>
                 
                 </td>
               </tr>
            
            
              <tr>
                 <td>
                 </td>
                 <td>
                  <asp:Label id="Label1" runat="server" Text="User ID" ></asp:Label>
                 </td>
                 <td>
                  <asp:TextBox id="txtUserID" runat="server" CssClass="textBox"></asp:TextBox>
                 </td>
               </tr>

                <tr>
                 <td>
                 </td>
                 <td>
                   <asp:Label  id="Label2" runat="server" Text="Full Name" ></asp:Label>
                 </td>
                 <td>
                    <asp:TextBox id="txtFullName" runat="server" Width="352px" CssClass="textBox"></asp:TextBox>
                 </td>
               </tr>


                 <tr>
                 <td>
                 </td>
                 <td>
                 
                 </td>
                 <td>
                 
                 </td>
               </tr>

                <tr>
                 <td>
                 </td>
                 <td>
                  <asp:Label ID="Label3" runat="server" Text="Role" ></asp:Label>
                 </td>
                 <td>
                  <asp:DropDownList id="ddlRole" runat="server" Width="160px" CssClass="dropDownList">
                        </asp:DropDownList>
                 </td>
               </tr>
                <tr>
                 <td>
                 </td>
                 <td>
                 <asp:Label id="Label4" runat="server" Text="Passowrd" ></asp:Label>
                 </td>
                 <td>
                    <asp:TextBox id="txtPassword" runat="server"  CssClass="textBox" TextMode="Password"></asp:TextBox> 
                    <asp:HyperLink id="lnkSetPass" runat="server" NavigateUrl="javascript:tbopen()">Set Password</asp:HyperLink>
                 </td>
                 <td>
                 </td>
               </tr>
                <tr>
                 <td>
                 </td>
                 <td>
                    <asp:Label id="lblPassword2" runat="server" Text="Password Confirm" ></asp:Label> 
                 </td>
                 <td>
                    <asp:TextBox id="txtPassword2" runat="server" CssClass="textBox" TextMode="Password"></asp:TextBox> 
                    <asp:CompareValidator id="CompareValidator1" runat="server" SetFocusOnError="True" ErrorMessage="Password Not Matched" ControlToValidate="txtPassword2" ControlToCompare="txtPassword"></asp:CompareValidator> 
                 </td>
                 <td>
                 
                 </td>
               </tr>
               <tr>
                 <td>
                 </td>
                 <td>
                   <asp:Label id="Label6" runat="server" Text="IsActive"></asp:Label>
                 </td>
                 <td>
                  <asp:DropDownList id="ddlIsActive" runat="server" Width="160px" 
                         CssClass="dropDownList">
                      <asp:ListItem Value="0">No</asp:ListItem>
                      <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
                        </asp:DropDownList>
                 </td>
               </tr>


               <tr>
                 <td>
                 </td>
                 <td>
                 
                 </td>
                 <td>
                 
                 </td>
               </tr>
            
            </table>

               </div>
             </div>

             <div id="groupFooter" class="groupFooter">
                      <div style="width:100%;height:12px;">
                      
                      </div>
                  </div>

            </div>
          </div>
      </div>  

        </div>  
       
   
      <div id="dvContentFooter" class="dvContentFooter">
           <table>
              <tr>
                <td>
                </td>
                <td>
                 <asp:Button ID="btnAddNew" runat="server" Text="New" CssClass="buttonNew" 
                onclick="btnAddNew_Click" />
                </td>
                <td>
                 <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonSave" 
                onclick="btnSave_Click" />
                </td>
                </tr> 
            </table>
        </div>
    </div> 
</asp:Content>

