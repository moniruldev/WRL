<%@ Page Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="AccSettings.aspx.cs" Inherits="PG.Web.Accounting.AccSettings" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
// <!CDATA[

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

        $(document).ready(function () {
            //document.forms[0]
            var formID = "<%= Form.ClientID %>"

//            $('#' + formID).submit(function () {
//                alert('Handler for .submit() called.');
//                return true;
//            });


            //document.forms[0].onsubmit = beforePostBackTask();

           // document.getElementById(formID).onsubmit = beforePostBackTask;


        });



//        function beforePostBackTask() {
//            alert('before postback');
//            return true;
//        }

//        var formID = "<%= Form.ClientID %>";

//        var oldPostBack;
//        window.onload = function () {
//            if (beforePostBackTask) {
//                if (document.getElementById(formID)) {
//                    document.getElementById(formID).onsubmit = beforePostBackTask;
//                }
//            }


//            oldPostBack = __doPostBack;
//            __doPostBack = function (eventTarget, eventArgument) {
//                if (beforePostBackTask) {
//                    beforePostBackTask();
//                }
//                oldPostBack(eventTarget, eventArgument);
//            };
//        }


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
    <div id="dvPageContent" style="width:100%; height:auto;">
    <div id="dvContentHeader" class="dvContentHeader">
    <div id="dvHeader" class="dvHeader" >
        <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Accounting Settings"></asp:Label>
    </div>
    <!--Message Div -->
    <div id="dvMsg" runat="server" class="dvMessage">
        <asp:Label ID="lblMessage" runat="server" Font-Size="Small" Width="100%"> </asp:Label>
    </div>
     <div id="dvHeaderControl" class="dvHeaderControl">
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
                                <span>Accounting Settings</span> 
                             </td>
                            </tr>
                         </table>
                         
                      </div>
                      
                  </div>
                  <div id="groupContent" class="groupContent" style="width:100%;height:300px; overflow:auto;">
                  <div id="groupContenInner" style="width:100%;height:auto; text-align:left;">
              <table style="text-align:left;" border="0" cellspacing="4" cellpadding="2">
                 <tr>
                   <td style="width:10px;">
                   </td>
                   <td>
                       &nbsp;</td>
                   <td>
                       <asp:HiddenField ID="hdnAccSettingsID" runat="server" Value="0" />
                   </td>
                                      <td>
                   </td>
                   <td>
                   </td>
                 </tr>
                 <tr>
                 <td>
                   </td>
                 <td style="" align="right">
                   <asp:Label id="Label1" runat="server" Text="Current Year" ></asp:Label>
                 </td>
                 <td style="" align="left">
                     <asp:DropDownList ID="ddlAccYear" runat="server" CssClass="dropDownList" 
                         Width="90" 
                         onselectedindexchanged="ddlAccYear_SelectedIndexChanged">
                     </asp:DropDownList>
                 </td>
                  <td style="" align="right">
                      </td>
                 <td style="" align="left">
                     </td>
                     
                     <td>
                     </td>
                 </tr>
                 
                <tr>
                <td>
                   </td>
                 <td style="" align="right">
                     &nbsp;</td>
                 <td style="" align="left">
                      &nbsp;</td>
                  <td style="" align="right">
                   
                    </td>
                 <td style="" align="left">
                     &nbsp;</td>
                     <td>
                     </td>
                 </tr>
                  
                 <tr>
                 <td>
                   </td>
                 <td style="" align="right">
                     <asp:Label ID="lblDisabledReportExport" runat="server" Text="Disable Report Export"></asp:Label>
                     </td>
                 <td style="" align="left">
                     <asp:DropDownList ID="ddlReportExport" runat="server">
                         <asp:ListItem Text="No" Value="0"></asp:ListItem>
                         <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                     </asp:DropDownList>

                 </td>
                  <td style="" align="right">
                      </td>
                 <td style="" align="left">
                   </td>
                     
                      <td>
                     </td>
                 </tr>
                <tr>
                <td>
                   </td>
                 <td style="" align="right">
                     <asp:Label ID="lblReportPrint" runat="server" Text="Disable Report Print"></asp:Label>
                 </td>
                 <td style="" align="left">
                    <asp:DropDownList ID="ddlReportPrint" runat="server">
                         <asp:ListItem Text="No" Value="0"></asp:ListItem>
                         <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                     </asp:DropDownList>  
                    </td>
                  <td style="" align="right">
                    </td>
                 <td style="" align="left">
                    </td>
                      <td>
                     </td>
                 </tr>  
                 
                <tr>
                <td>
                   </td>
                 <td style="" align="right">
                     <asp:Label ID="Label2" runat="server" Text="Disable PDF Print"></asp:Label>
                 </td>
                 <td style="" align="left">
                    <asp:DropDownList ID="ddlPDFPrint" runat="server">
                         <asp:ListItem Text="No" Value="0"></asp:ListItem>
                         <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                     </asp:DropDownList>  
                    </td>
                  <td style="" align="right">
                    </td>
                 <td style="" align="left">
                    </td>
                      <td>
                     </td>
                 </tr>   

                 
                 
 
              </table>

        </div>
              </div>

              </div>
                  <div id="groupFooter" class="groupFooter">
                      <div style="width:100%;height:12px;">
                      
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
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonCancel checkIsDirty" 
                     onclick="btnCancel_Click"   />
                </td>
                <td>
                  <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonSave" 
                    onclick="btnSave_Click" />
                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="buttonEdit" 
                        onclick="btnEdit_Click"   />
                </td>
                <td>
                 <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="buttonDelete" 
                        onclick="btnDelete_Click"   />
                </td>
                
                <td>
                   <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="buttonRefresh checkIsDirty" 
                        onclick="btnRefresh_Click"   />
                   </td>

               
                <td>
                   
                 </td>
                

                 <td>
                    <input id="btnClose" type="button" runat="server" class="buttonClose" value="Close" onclick="if (ContentForm){ ContentForm.CloseForm();}" />
                </td>


              </tr>
           </table>    
    
    </div>
    </div> 
</asp:Content>

