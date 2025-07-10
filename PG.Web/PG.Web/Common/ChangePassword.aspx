<%@ Page Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="PG.Web.Common.ChangePassword" %>

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
    if (TabVar.PageMode == Enums.PageMode.InTab)
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
        window.parent.OpenMenuByData(tdata);
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

function tbopenSalInfo(key) {
    if (!key) {
        key = '';
    }


    var url = "/Master/EmpSalaryInfo.aspx?eid=" + key
    //if (pageInTab == 1)
    if (TabVar.PageMode == Enums.PageMode.InTab) {

        var tdata = new xtabdata();
        tdata.linktype = Enums.LinkType.Direct;
        tdata.id = 6320;
        tdata.name = "EmpSalaryInfo";
        //tdata.label = "User: " + userid;
        tdata.label = "Emp. Salary Sturture";
        tdata.type = 0;
        tdata.url = url;
        tdata.tabaction = Enums.TabAction.InTabReuse;
        tdata.selecttab = 1;
        tdata.reload = 0;
        tdata.param = "";
        
        try {
            window.parent.OpenMenuByData(tdata);
        }
        catch (err) {
            alert("error in page");
        }
    }
    else {
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
//
}


function btnSalaryInfo_onclick() {

}

function btnSalaryInfo_onclick() {

}

// ]]>
    </script>
    <style type="text/css">
        .style1
        {
            width: 25%;
            height: 27px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div id="dvPageContent" style="width:100%; height:536px;">
    
     <div id="dvContentHeader" class="dvContentHeader">  
    <div id="dvHeader" class="dvHeader" >
        <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" 
            Text="Change Password"></asp:Label>
    </div>
     <div id="dvMessage" class="dvMessage" >
        <asp:Label ID="lblMessage" runat="server" Font-Size="Small" Width="100%"></asp:Label>
    </div>
    </div>

    <div id="dvContentMain" class="dvContentMain"> 

    <div id = "dvControls" 
            style="height:auto; width:100%">
              <table style="" border="0" cellpadding="3" cellspacing="3">
                 <tr>
                   <td style="width:50px;">
                   </td>
                   <td style="width:150px;" align="right">
                     
                   </td>
                   <td style="" align="left">
                      
                   </td>
                 </tr> 

              <tr style="height:10px;">
                   <td style="">
                   </td>
                   <td style="" align="right">
                     
                   </td>
                   <td style="" align="left">
                      
                   </td>
                 </tr>


                 <tr>
                   <td style="">
                   </td>
                   <td style="" align="right">
                      <asp:Label id="Label1" runat="server" Text="User" ></asp:Label>
                   </td>
                   <td style="" align="left">
                      <asp:TextBox id="txtUser" runat="server" CssClass="textBox" 
                        width="234px" ReadOnly="True"></asp:TextBox>
                   </td>
                 </tr>
                 
                <tr>
                   <td style="">
                   </td>
                   <td style="" align="right">
                      <asp:Label id="Label3" runat="server" Text="Current Password" ></asp:Label>
                   </td>
                   <td style="" align="left">
                        <asp:TextBox id="txtCurPassword" runat="server" CssClass="textBox" 
                        width="164px" TextMode="Password"></asp:TextBox>
                   </td>
                 </tr>

                  <tr>
                   <td style="">
                   </td>
                   <td style="" align="right">
                     <asp:Label id="Label4" runat="server" Text="New Password" ></asp:Label>
                   </td>
                   <td style="" align="left">
                       <asp:TextBox id="txtNewPassword" runat="server" CssClass="textBox" 
                        width="164px" TextMode="Password"></asp:TextBox>
                   </td>
                 </tr>

                <tr>
                   <td style="">
                   </td>
                   <td style="" align="right">
                       <asp:Label id="Label5" runat="server" Text="Confirm New Password" ></asp:Label>
                   </td>
                   <td style="" align="left">
                      <asp:TextBox id="txtNewPasswordConfirm" runat="server" CssClass="textBox" 
                        width="164px" TextMode="Password"></asp:TextBox>
                   </td>
                   <td>
                      <asp:CompareValidator ID="CompareValidator1" runat="server" 
                          ErrorMessage="Passowrd Not Matched!" ControlToCompare="txtNewPassword" 
                          ControlToValidate="txtNewPasswordConfirm"></asp:CompareValidator>
                   </td>
                 </tr>

                <tr>
                   <td style="">
                   </td>
                   <td style="" align="right">
                     
                   </td>
                   <td style="" align="left">
                      
                   </td>
                 </tr>

                <tr>
                   <td style="">
                   </td>
                   <td style="" align="right">
                     
                   </td>
                   <td style="" align="left">
                          <asp:Button ID="btnChangePass" runat="server" Text="Change Password" CssClass="buttoncommon" 
                              onclick="btnSave_Click" Width="142px" />
                   </td>
                 </tr>


                 
              </table>

        </div>  
     
     </div>

     <div id="dvContentFooter" class="dvContentFooter">
        <table>
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
</asp:Content>

