<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="SetPassword.aspx.cs" Inherits="PG.Web.Admin.SetPassword" %>

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


    var url = "/Master/EmpSalaryInfo.aspx?eid=" + key
    //if (pageInTab == 1)
    if (ZForm.PageMode == Enums.PageMode.InTab) {

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
  <div id="dvPageContent" style="width:100%; height:100%;">
     <div id="dvContentHeader" class="dvContentHeader">  
        <div id="dvHeader" class="dvHeader" >
          <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Set Password"></asp:Label>
        </div>
        <div id="dvMsg" runat="server" class="dvMessage">
            <asp:Label ID="lblMessage" runat="server" Font-Size="Small" Width="100%"  Height="16px"></asp:Label>
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
                                <span>Change User Password</span> 
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
                                <asp:Label ID="Label2" runat="server" Text="User"></asp:Label>
                            </td>
                            <td>
                              <asp:DropDownList ID="ddlUser" runat="server" CssClass="dropDownList" Width="163px">
                                </asp:DropDownList>
                            </td> 
                           </tr>
                           <tr>
                             <td>
                             </td>
                              
                             <td>
                              <asp:Label ID="Label1" runat="server" Text="New Password"></asp:Label>
                             
                             </td>
                             <td>
                              <asp:TextBox id="txtNewPassword" runat="server" CssClass="textBox" 
                                     width="164px" TextMode="Password"></asp:TextBox>
                             </td>

                           </tr>

                           <tr>
                             <td>
                             
                             </td>

                             <td>
                              <asp:Label id="Label5" runat="server" Text="Confirm New Password" ></asp:Label>
                             </td>
                             <td>
                                 <asp:TextBox id="txtNewPasswordConfirm" runat="server" CssClass="textBox" 
                                            width="164px" TextMode="Password"></asp:TextBox>                            
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
                <td style="width:20px">
                
                </td>
                <td>
                </td>
                <td> <asp:Button ID="btnChangePass" runat="server" Text="Set Password" CssClass="buttoncommon" 
                onclick="btnSave_Click" Width="142px" />
                </td>
            </tr>
         </table>
       
       </div>

  </div>

</asp:Content>  

