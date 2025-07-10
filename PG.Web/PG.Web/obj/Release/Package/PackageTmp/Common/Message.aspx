<%@ Page Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="Message.aspx.cs" Inherits="PG.Web.Common.Message" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
// <!CDATA[

        var isPageResize = false;
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



function btnBack_onclick() {
    //alert(window.name);
    //history.back();

    // frames['framename'].history.back();

   // document.getElementById(window.name).history.back();
    //frames[window.name].history.back();
    self.history.back();
}

// ]]>
    </script>
    <style type="text/css">
        .style1
        {
            width: 25%;
            height: 27px;
        }
        .style2
        {
            width: 57px;
        }
        .style3
        {
            width: 57px;
            height: 27px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div id="dvPageContent" style="width:100%; height:auto;">
    <div id="dvContentHeader" class="dvContentHeader">
       <div id="dvHeader" class="dvHeader" >
        <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" 
            Text="Application Message"></asp:Label>
    </div>
</div>
	<div id="dvContentMain" class="dvContentMain">
    <div id="dvMsg" runat="server"
        style="width:100%; min-height:20px; height:auto; text-align: center; ">
        <asp:Label ID="lblMessage" runat="server" Font-Size="Small" Width="100%" 
            Height="16px"></asp:Label>
    </div>

    <div id = "dvControls" 
            style="height:auto; width:100%">
             <table>
               <tr>
                <td>
                </td>
                <td>
                    <asp:Label ID="lblMsg1" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                </td>
                <td>
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
                <td  width="20px">
                </td>
                <td>
                    <asp:Label ID="lblMsg2" runat="server" Text=""></asp:Label>
                </td>
                <td>
                </td>
               </tr>
                 <tr>

                 </tr>
                 <tr>
                     <td width="20px"></td>
                   <td>
             <input id="btnBack" type="button" runat="server" class="buttoncommon" value="Go back previous page" onclick="return btnBack_onclick()" />
            </td>
            <td></td>
                 </tr>
             </table>

        </div>  
    </div>
       <div id="dvContentFooter" class="dvContentFooter">  
         <div id="dvCommnanButton" class="dvCommandButtons">
          <div style="width:80%;">
          <table>
            <tr>
                <td style="width:20px">
                
                </td>
            
                  
                <td> &nbsp;</td>
            </tr>
         </table>
         </div>      
                
        </div>
           </div>
    </div> 
</asp:Content>

