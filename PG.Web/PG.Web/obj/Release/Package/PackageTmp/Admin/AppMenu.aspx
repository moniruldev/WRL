<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AppMenu.aspx.cs" Inherits="PG.Web.Admin.AppMenu" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
// <!CDATA[

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

function tbopenSalInfo(key) {
    if (!key) {
        key = '';
    }


    var url = "/Salary/EmpSalaryInfo.aspx?id=" + key
    //if (pageInTab == 1)
    if (ZForm.PageMode == Enums.PageMode.InTab) {

        var tdata = new xtabdata();
        tdata.linktype = Enums.LinkType.Direct;
        tdata.id = 6320;
        tdata.name = "EmpSalaryInfo";
        //tdata.label = "User: " + userid;
        tdata.label = "Emp. Salary Info";
        tdata.type = 0;
        tdata.url = url;
        tdata.tabaction = Enums.TabAction.InNewTab;
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

function tbopenEmpTax(key) {
    if (!key) {
        key = '';
    }


    var url = "/Salary/EmpIncomeTax.aspx?id=" + key
    //if (pageInTab == 1)
    if (ZForm.PageMode == Enums.PageMode.InTab) {

        var tdata = new xtabdata();
        tdata.linktype = Enums.LinkType.Direct;
        tdata.id = 6320;
        tdata.name = "EmpIncomeTax";
        //tdata.label = "User: " + userid;
        tdata.label = "Emp. Tax";
        tdata.type = 0;
        tdata.url = url;
        tdata.tabaction = Enums.TabAction.InNewTab;
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
  //document.getElementById("btnSave").click();
  ///__doPostBack("<%= this.btnSave.UniqueID %>", "");
  __doPostBack("btnSave", "");
}


function btnSalaryInfo_onclick() {

}

function btnSalaryInfo_onclick() {

}

// ]]>
    </script>
    <style type="text/css">



    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvPageContent" style="width:100%; height:536px;">
    <div id="dvHeader" class="dvHeader" >
        <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="App Menu"></asp:Label>
    </div>
    <div id="dvMsg" runat="server"
        style="width:100%; min-height:20px; height:auto; text-align: center; ">
        <asp:Label ID="lblMessage" runat="server" Font-Size="Small" Width="100%" 
            Height="16px"></asp:Label>
    </div>

    <div id = "dvControls" 
            style="auto; width:100%">
              <table style="width: 95%;" border="0">
               <tr>
                 <td align="right">
                   <asp:Label id="Label2" runat="server" Text="App ID:" ></asp:Label>
                 </td>
                 <td align="left" >
                    <asp:TextBox id="txtAppID" runat="server" CssClass="textBox" 
                        width="164px"></asp:TextBox>
                 </td>
                  <td align="right">
                      &nbsp;</td>
                 <td align="left">
                     &nbsp;</td>
                 </tr>
              
                 <tr>
                 <td align="right">
                   <asp:Label id="Label1" runat="server" Text="Menu ID:" ></asp:Label>
                 </td>
                 <td align="left" >
                    <asp:TextBox id="txtAppMenuID" runat="server" CssClass="textBox" 
                        width="164px"></asp:TextBox>
                 </td>
                  <td align="right">
                      &nbsp;</td>
                 <td align="left">
                     &nbsp;</td>
                 </tr>
                 
                <tr>
                 <td align="right">
                   <asp:Label id="Label3" runat="server" Text="Menu Name:" ></asp:Label>
                 </td>
                 <td  align="left">
                    <asp:TextBox id="txtAppMenuName" runat="server" CssClass="textBox" 
                        width="164px"></asp:TextBox>
                    </td>
                  <td  align="right">
                      &nbsp;</td>
                 <td  align="left">
                     &nbsp;</td>
                 </tr>
                 
                 <tr>
                 <td  align="right">
                    <asp:Label  id="Label9" runat="server" Text="Menu Text" ></asp:Label>
                     </td>
                 <td  align="left">
                        <asp:TextBox id="txtAppMenuText" runat="server" Width="164px" 
                         CssClass="textBox" ></asp:TextBox>
                     </td>
                  <td  align="right">
                      &nbsp;</td>
                 <td  align="left">
                        &nbsp;</td>
                 </tr>
                  <tr>
                 <td align="right">
                   <asp:Label id="Label10" runat="server" Text="Parent Menu" ></asp:Label>
                      </td>
                 <td align="left">
                     <asp:DropDownList ID="ddlParentMenu" runat="server" Height="20px" Width="162px" 
                         CssClass="dropDownList">
                     </asp:DropDownList>
                      </td>
                  <td  align="right">
                      &nbsp;</td>
                 <td  align="left">
                     &nbsp;</td>
                 </tr>
                <tr>
                 <td  align="center">
                     &nbsp;</td>
                 <td " align="left" valign="middle">
   
                         
                             
                         
                 </td>

                 <td  align="left">
                     &nbsp;</td>
                     
                     <td></td>
                 </tr>
                <tr>
                 <td  align="right">
                   <asp:Label id="Label38" runat="server" Text="SL No:" ></asp:Label>
                      </td>
                 <td align="left">
   
                         
                        <asp:TextBox id="txtSLNo" runat="server" Width="120px" style="text-align:left"
                         CssClass="textBox" ></asp:TextBox>
                             
                         
                      </td>
                  <td  align="right">

                      &nbsp;</td>
                 <td  align="left">

                     &nbsp;</td>
                 </tr>
                 <tr>
                 <td align="right">
                   <asp:Label id="Label32" runat="server" Text="Menu Path:" ></asp:Label>
                      </td>
                 <td  align="left" valign="middle">
                             
                         
                        <asp:TextBox id="txtAppMenuPath" runat="server" Width="352px" style="text-align:left"
                         CssClass="textBox" Height="16px" ></asp:TextBox>
                             
                         
                      </td>
                  <td  align="right">
                      &nbsp;</td>
                 <td  align="left">
                     &nbsp;</td>
                 </tr>
                  <tr>
                 <td  align="right">
                   <asp:Label id="Label39" runat="server" Text="IsAppURL" ></asp:Label>
                      </td>
                 <td " align="left" valign="middle">
   
                         
                     <asp:DropDownList ID="ddlIsAppURL" runat="server" Width="50px" 
                         CssClass="dropDownList">
                         <asp:ListItem Value="0">No</asp:ListItem>
                         <asp:ListItem Value="1">Yes</asp:ListItem>
                     </asp:DropDownList>
                             
                         
                      </td>
                  <td  align="right">

                      &nbsp;</td>
                 <td  align="left">

                     &nbsp;</td>
                 </tr>
                                  <tr>
                 <td align="right">
                   <asp:Label id="Label35" runat="server" Text="Menu URL:" ></asp:Label>
                                      </td>
                 <td align="left">
   
                         
                        <asp:TextBox id="txtAppMenuURL" runat="server" Width="350px" style="text-align:left"
                         CssClass="textBox" Height="16px" ></asp:TextBox>
                             
                         
                                      </td>
                  <td align="right">

                      &nbsp;</td>
                 <td align="left">
                       &nbsp;</td>
                 </tr>
                                  <tr>
                 <td align="right">
                   <asp:Label id="Label36" runat="server" Text="Set App Header:" ></asp:Label>
                                      </td>
                 <td align="left">
   
                         
                     <asp:DropDownList ID="ddlSetAppHeader" runat="server" Width="50px" 
                         CssClass="dropDownList">
                         <asp:ListItem Value="0">No</asp:ListItem>
                         <asp:ListItem Value="1">Yes</asp:ListItem>
                     </asp:DropDownList>
                             
                         
                                      </td>
                  <td align="right">

                      &nbsp;</td>
                 <td align="left">

                     &nbsp;</td>
                 </tr>
                                  <tr>
                 <td align="right" >
                   <asp:Label id="Label37" runat="server" Text="App Header Text:" ></asp:Label>
                                      </td>
                 <td align="left">
   
                         
                    <asp:TextBox id="txtAppHeaderText" runat="server" CssClass="textBox" 
                        width="164px"></asp:TextBox>
                                      </td>
                  <td align="right" >
                                      </td>
                 <td align="left" >
                                      </td>
                 </tr>
                 
                  <tr>
                 <td align="right" >
                   <asp:Label id="Label40" runat="server" Text="Select Action:" ></asp:Label>
                                      </td>
                 <td align="left">
   
                         
                     <asp:DropDownList ID="ddlSelectAction" runat="server" Width="120px" 
                         CssClass="dropDownList">
                         <asp:ListItem Value="0">Select</asp:ListItem>
                         <asp:ListItem Value="1">Expand</asp:ListItem>
                         <asp:ListItem Value="2">SelectExpand</asp:ListItem>
                         <asp:ListItem Value="3">None</asp:ListItem>
                     </asp:DropDownList>
                             
                         
                                      </td>
                  <td align="right" >
                      &nbsp;</td>
                 <td align="left">
                     &nbsp;</td>
                 </tr>
                 
                <tr>
                 <td  align="right">
                   <asp:Label id="Label34" runat="server" Text="Tab Action:" ></asp:Label>
                                      </td>
                 <td align="left">
   
                         
                     <asp:DropDownList ID="ddlTabAction" runat="server" Width="160px" 
                         CssClass="dropDownList">
                         <asp:ListItem Value="0">None</asp:ListItem>
                         <asp:ListItem Value="1">In Tab</asp:ListItem>
                         <asp:ListItem Value="2">In New Tab</asp:ListItem>
                         <asp:ListItem Value="3">In Tab Find/Reuse</asp:ListItem>
                         <asp:ListItem Value="4">In Tab Find/Reuse by Param</asp:ListItem>
                     </asp:DropDownList>
                             
                         
                                      </td>
                  <td  align="right">

                      &nbsp;</td>
                 <td  align="left">

                     &nbsp;</td>
                 </tr>
                 
                 <tr>
                 <td align="right">
                   <asp:Label id="Label41" runat="server" Text="Expanded:" ></asp:Label>
                                      </td>
                 <td align="left">
   
                         
                     <asp:DropDownList ID="ddlExpanded" runat="server" Width="50px" 
                         CssClass="dropDownList">
                         <asp:ListItem Value="0">No</asp:ListItem>
                         <asp:ListItem Value="1">Yes</asp:ListItem>
                     </asp:DropDownList>
                             
                         
                                      </td>
                  <td align="right">
                         </td>
                 <td align="left">
                      </td>
                 </tr>
                 
                 
                  <tr>
                 <td  align="right">
                   <asp:Label id="Label42" runat="server" Text="Show Menu:" ></asp:Label>
                 </td>
                 <td  align="left">

                         
                     <asp:DropDownList ID="ddlShowMenu" runat="server" Width="50px" 
                         CssClass="dropDownList">
                         <asp:ListItem Value="0">No</asp:ListItem>
                         <asp:ListItem Value="1">Yes</asp:ListItem>
                     </asp:DropDownList>
                             
                         
                                      </td>
                  <td  align="right">

                 </td>
                 <td  align="left">

                 </td>
                 </tr>   
                                   <tr>
                 <td  align="right">
                 </td>
                 <td  align="left">

                         
                     &nbsp;</td>
                  <td  align="right">

                 </td>
                 <td  align="left">

                 </td>
                 </tr> 
                                   <tr>
                 <td  align="right">
                 </td>
                 <td  align="left">

                         
                     &nbsp;</td>
                  <td  align="right">

                 </td>
                 <td  align="left">

                 </td>
                 </tr>      
              </table>
            
            


        </div>  
        <div id="dvCommnanButton" class="dvCommandButtons">
            <div style="width:80%; padding-left:30px">
                <asp:Button ID="btnAddNew" runat="server" Text="New" CssClass="buttoncommon" 
                onclick="btnAddNew_Click" />
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttoncommon" 
                onclick="btnSave_Click" AccessKey="s" />
                &nbsp;</div>      
                
        </div>
    </div> 
</asp:Content>

