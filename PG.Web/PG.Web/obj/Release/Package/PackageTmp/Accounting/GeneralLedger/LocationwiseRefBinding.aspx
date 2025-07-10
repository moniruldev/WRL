<%@ Page Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="LocationwiseRefBinding.aspx.cs" Inherits="PG.Web.Accounting.GeneralLedger.LocationwiseRefBinding" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

        <script src="../../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <link href="../../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        // <!CDATA[
        var isPageResize = true;

        var AccRefServiceLink = '<%=this.AccRefServiceLink%>';

        var hdnCompanyID = '<%=hdnCompanyID.ClientID%>';


        var txtAccRefCode = '<%=txtAccRefCode.ClientID%>';
        var btnAccRefCode = '<%=btnAccRefCode.ClientID%>';
        var hdnAccRefID = '<%=hdnAccRefID.ClientID%>';
        var txtAccRefName = '<%=txtAccRefName.ClientID%>';


        var hdnAccRefTypeID = '<%=hdnAccRefTypeID.ClientID%>';
        var ddlAccRefCategory = '<%=ddlAccRefCategory.ClientID%>';
        var ddlLocation = '<%=ddlLocation.ClientID%>'
       

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

        function tbopen(key) {
            if (!key) {
                key = '';
            }


            var url = "/Admin/SetPassword.aspx?uid=" + key
            //if (pageInTab == 1)
            if (TabVar.PageMode == Enums.PageMode.InTab) {

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

        

        function ReportPrint(key, isPDFAutoPrint) {
            var rptPageLink = ReportViewPageLink;
            if (isPDFAutoPrint) {
                //rptPageLink = ReportPDFPageLink;
                rptPageLink = ReportViewPDFPageLink;
            }

            //var url = "./Report/ReportView.aspx?rk=" + key
            var now = new Date();
            var strTime = now.getTime().toString();
            var url = ReportViewPageLink + "?rk=" + key + "&_tt=" + strTime;

            //var url = rptPageLink + "?rk=" + key;

            iframe = document.getElementById(ifPrintButton);
            if (iframe === null) {
                iframe = document.createElement('iframe');
                iframe.id = hiddenIFrameID;
                //        iframe.style.display = 'none';
                //        iframe.style = 'none';
                document.body.appendChild(iframe);
            }
            iframe.src = url;
        }
        

        function fromParent(val1) {
    alert('this is called from parent: ' + val1);
}

        $(document).ready(function () {
    str = document.body.innerHTML


    //    $('#tblParam tr').each(function () {
    //        if ($(this).find('td:empty').length) $(this).remove();
    //    });

    $("#tblParam tr.rowParam").each(function () {
        var cell = $.trim($(this).find('td').text());
        if (cell.length == 0) {
            //console.log('empty');
            //$(this).addClass('nodisplay');
            $(this).hide();
        }
    });


});

        $(document).ready(function () {

           

            if ($('#' + txtAccRefCode).is(':visible')) {
                bindAccRefListAC();
            }
   

});

        function bindAccRefListAC() {

            var companyid = $('#' + hdnCompanyID).val();
            var typeid = $('#' + hdnAccRefTypeID).val();
            var catagoryid = $('#' + ddlAccRefCategory).val();

            //var selected = $("#q_7 input:radio:checked").val();

            //var typeid = $("#" + rblAccRefTypeID + " input:radio:checked").val();

            var serviceURL = AccRefServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains + "&ispaging=1";
            //serviceURL += "&typeid=" + typeid;
            serviceURL += "&companyid=" + companyid;


            var cgColumns = [{ 'columnName': 'code', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Code' }
                             , { 'columnName': 'name', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'categoryname', 'width': '130', 'align': 'left', 'highlight': 0, 'label': 'Category' }
            ];

            var accRefElem = $('#' + txtAccRefCode);

            $('#' + btnAccRefCode).click(function (e) {
                //elmID = $(elem).attr('id');
                //$(elem).combogrid("show");
                $(accRefElem).combogrid("dropdownClick");
            });

            $(accRefElem).combogrid({
                debug: true,
                searchButton: false,
                resetButton: false,
                alternate: true,
                munit: 'px',
                scrollBar: true,
                showPager: true,
                showError: true,
                colModel: cgColumns,
                width: 400,
                url: serviceURL,
                //"select item" event handler to set input field
                search: function (event, ui) {
                    //var typeid = $('#' + ddlAccRefType).val();
                    var typeid = $('#' + hdnAccRefTypeID).val();
                    var catagoryid = $('#' + ddlAccRefCategory).val();
                    var newServiceURL = serviceURL + "&typeid=" + typeid;
                    newServiceURL = newServiceURL + "&categoryid=" + catagoryid;
                    $(this).combogrid("option", "url", newServiceURL);
                },
                select: function (event, ui) {
                    elemID = $(accRefElem).attr('id');

                    if (!ui.item) {
                        event.preventDefault();
                        ClearAccRefData();
                        return false;
                        //ClearGLAccountData(elemID);
                    }


                    if (ui.item.id == 0) {
                        event.preventDefault();
                        ClearAccRefData();
                    }
                    else {
                        SetAccRefData(ui.item);
                    }
                    return false;
                }
            });

            $(accRefElem).blur(function () {
                var self = this;
                elemID = $(accRefElem).attr('id');
                ttCode = $(accRefElem).val();

                if (ttCode == '') {
                    ClearAccRefData(elemID);
                }
                else {

                }

                ttID = $('#' + hdnAccRefID).val();
                if (ttID == '0' | ttID == '') {
                    $(self).addClass('textError');
                }
            });

            $(accRefElem).focus(function () {
                var self = this;
                $(self).removeClass('textError');
                //$(elem).removeClass('fldDataError');
            });

            $('#' + ddlAccRefCategory).change(function () {
                var self = this;
                //$(self).removeClass('textError');
                //$(elem).removeClass('fldDataError');

                ClearAccRefData();
            });


        } //bind ref


        function SetAccRefData(data) {
            $('#' + txtAccRefCode).val(data.code);
            $('#' + hdnAccRefID).val(data.id);
            $('#' + txtAccRefName).val(data.name);
        }

        function ClearAccRefData() {
            //$('#' + elemID).val(data.code);

            $('#' + txtAccRefCode).val('');
            $('#' + hdnAccRefID).val('0');
            $('#' + txtAccRefName).val('');
            $('#' + txtAccRefCode).removeClass('fldDataError');

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
    
             .dvGroup
        {
         width: 182px;
         height: 20px; 
         border: 1px solid lightgrey;
         
        }
        
        
        .textPopup1
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            border: 1px #1B68B8 solid;
            BACKGROUND-COLOR: #FFFFFF;
            COLOR: #000000;
            FONT-SIZE:11px;
            WIDTH: 160px;
            HEIGHT:16px;
            padding-left:2px;
        }
       
       
   
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvPageContent" style="width:100%; height:auto;">
    <div id="dvContentHeader" class="dvContentHeader">
    <div id="dvHeader" class="dvHeader" >
        <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Location wise Reference Binding"></asp:Label>
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
                                <span>Location wise Reference Binding</span> 
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
                      <%-- <asp:HiddenField ID="hdnGLAccountID" runat="server" Value="0" />--%>
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
                   <asp:Label id="Label9" runat="server" Text="Location:" ></asp:Label>
                    </td>
                 <td style="" align="left">
                     <asp:DropDownList ID="ddlLocation" runat="server" CssClass="dropDownList" Width="130">
                     </asp:DropDownList>
                      <asp:HiddenField ID="hdnCompanyID" runat="server" Value="0" />
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
                    </td>
                      <td>
                     </td>
                 </tr> 

                  <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblAccRefCategory" runat="server" Text="Reference Category:"></asp:Label>
                                            </td>
                                            <td style="">
                                                <asp:DropDownList ID="ddlAccRefCategory" runat="server" CssClass="dropDownList" Width="170px">
                                                    <asp:ListItem Value="0">All</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                               <asp:HiddenField ID="hdnAccRefTypeID" runat="server" Value="0" />
                                            </td>
                                            <td style="" align="right">
                                                &nbsp;
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                 
                <tr class="rowParam">
                                            <td>
                                            </td>
                                            <td style="" align="right">
                                                <asp:Label ID="lblAccRefCode" runat="server" Text="Reference:"></asp:Label>
                                            </td>
                                            <td colspan="4">
                                                <table cellspacing="0" cellpadding="0">
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtAccRefCode" runat="server" Width="150px" CssClass="textBox"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <input id="btnAccRefCode" type="button" value="" runat="server" class="buttonDropdown"
                                                                tabindex="-1" />
                                                        </td>
                                                        <td class="tdSpacer">
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAccRefName" runat="server" Width="150px" CssClass="textBox"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:HiddenField ID="hdnAccRefID" runat="server" Value="0" />
                                                        </td>
                                                    </tr>
                                                </table>
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

                      </td>
                 <td style="" align="left">
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

