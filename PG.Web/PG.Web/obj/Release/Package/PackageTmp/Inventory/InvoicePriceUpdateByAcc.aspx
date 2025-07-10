<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="InvoicePriceUpdateByAcc.aspx.cs" Inherits="PG.Web.Inventory.InvoicePriceUpdateByAcc"  %>  

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">   
        // <!CDATA[

        var isPageResize = true;
        ContentForm.CalendarImageURL = "../image/calendar.png";

<%--        var hdnCompanyID = '<%=hdnCompanyID.ClientID%>';--%>

        var ReportViewPageLink = '<%=this.ReportViewPageLink%>';
        var ReportViewPDFPageLink = '<%=this.ReportViewPDFPageLink%>';
        var ReportPrintPageLink = '<%=this.ReportPrintPageLink%>';
        var ReportPDFPageLink = '<%=this.ReportPDFPageLink%>';











        function onlyNos(e, t) {
            try {
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                return true;
            }
            catch (err) {
                alert(err.Description);
            }
        }


        function ShowProgress() {
            $('#' + updateProgressID).show();
        }

        function UserDeleteConfirmation() {
            return confirm("Are you sure you want to Generate DC,GP and Stock Issue !!?");
        }


        function PageResizeCompleted(pg, cntMain) {
            resizeContentInner(cntMain);
        }

        function resizeContentInner(cntMain) {
            var contHeight = $("#dvContentMainInner").height();

            var topHeight = $("#dvTop").height();

            var middleHeight = contHeight - topHeight;

            $("#dvMiddle").height(middleHeight);
            $("#tblMiddle").height(middleHeight);

            $("#dvReportList").height(middleHeight);
            $("#dvParam").height(middleHeight);

        }




        function tbopen(key, isPrint, isPDFAutoPrint, showWait) {
            key = key || '';
            isPrint = isPrint || false;
            showWait = showWait || true;

            if (isPrint) {
                if (key != '') {
                    ReportPrint(key, isPDFAutoPrint);
                    return;
                }
            }

            //var url = "/Report/ReportView.aspx?rk=" + key

            var now = new Date();
            var strTime = now.getTime().toString();
            var url = ReportViewPageLink + "?rk=" + key + "&_tt=" + strTime;
            //var url = ReportViewPageLink + "?rk=" + key;

            //if (pageInTab == 1)
            if (TabVar.PageMode == Enums.PageMode.InTab) {

                var tdata = new xtabdata();
                tdata.linktype = Enums.LinkType.Direct;
                tdata.id = 7999;
                tdata.name = "Report view";
                //tdata.label = "User: " + userid;
                tdata.label = "Report view";
                tdata.type = 0;
                tdata.url = url;
                tdata.tabaction = Enums.TabAction.InNewTab;
                tdata.selecttab = 1;
                tdata.reload = 0;
                tdata.param = "";
                tdata.showWait = showWait;

                try {
                    //window.parent.OpenMenuByData(tdata);
                    window.parent.TabMenu.OpenMenuByData(tdata);
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

        // alert('OK');
        $(document).ready(function () {

            var pageInstance = Sys.WebForms.PageRequestManager.getInstance();

            pageInstance.add_pageLoaded(function (sender, args) {
                var panels = args.get_panelsUpdated();
                for (i = 0; i < panels.length; i++) {
                    //alert(panels[i].id);
                    //ContentForm.InitDefualtFeatureInScope(panels[i].id);

                    //if (panels[i].id == gridUpdatePanelIDDet) {
                    //    bindItemList(gridViewIDDet);
                    //    bindItemGroupList(gridViewIDDet);
                    //}

                }
                //$('#' + dvGridDetailsPopup).parent().appendTo(jQuery("form:first"));
                //gridTaskAfter();

            });


            // alert('OK 1');

            //if ($('#' + txtCompanyName).is(':visible')) {

            //    bindCompanyList();
            //}

            //if ($('#' + txtCustomerName).is(':visible')) {

            //    bindCustomerList();
            //}
            ////alert('OK 1');
            //bindItemGroupList(gridViewIDDet);
            ////alert('OK 2');

            //bindItemList(gridViewIDDet);
        });




    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div id="dvPageContent" style="width: 100%; height: 100%;">
 
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader" align="left">
                <asp:Label ID="lblHeader" runat="server" Text="Invoice Price Update" CssClass="lblHeader" Font-Bold="true" Font-Size="15px"></asp:Label>
            </div>
            <div id="dvMessage" class="dvMessage">
                <asp:Label ID="lblMessage" runat="server" CssClass="lblMessage" Text="" Width="100%"></asp:Label>
            </div>

            <div id="dvHeaderControl" class="dvHeaderControl">
            </div>

        </div>

        <div id="dvContentMain" class="dvContentMain" align="left">
            <div id="dvControlsHead" style="height: auto; width: 100%;" align="left">
                <table border="0" cellspacing="4" cellpadding="2" align="left" style="width: 1124px" id="tblItemEntry">

                    <tr>
                        <td align="right">
                            <asp:Label ID="lblInvoiceNo" runat="server" Text="Invoice No :" Font-Bold="true"></asp:Label>
                        </td>
                        <td>

                            <asp:TextBox ID="txtInvoiceNo" ReadOnly="true" runat="server" CssClass="colourdisabletextBox" Font-Bold="true" Width="200px"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblInvoiceDate" runat="server" Text="Invoice Date :" Font-Bold="true"></asp:Label><span style="color: red">*</span>

                        </td>

                        <td>

                            <asp:TextBox ID="txtInvoiceDate" runat="server" CssClass="textBox textDate" Font-Bold="true" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblInvoiceTime" runat="server" Text="Invoice Time :" Font-Bold="true"></asp:Label><span style="color: red">*</span>

                        </td>
                        <td>

                            <asp:TextBox ID="txtInvoicetime" runat="server" CssClass="textBox textDate" Font-Bold="true" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td></td>
                    </tr>




                    <tr>
                        <td align="right">
                            <asp:Label ID="lblCustomerName" runat="server" Text="Customer Name :" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtCustomerName" runat="server" CssClass="textBox" ReadOnly="true" Font-Bold="true" Width="200px"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblCompanyName" runat="server" Text="Company Name :" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left">

                            <asp:TextBox ID="txtCompanyName" runat="server" ReadOnly="true" CssClass="textBox" Font-Bold="true" Width="200px"></asp:TextBox>
                        </td>
                        <td align="right">
                            <b>DC:</b>
                        </td>
                        <td>
                            <asp:Label ID="lblDCNO" runat="server" Width="150px"></asp:Label>

                        </td>
                        <td>

                            <asp:HiddenField ID="hdnDCID" runat="server" Value="0" />
                        </td>
                    </tr>

                    <tr>
                        <td align="right">
                            <asp:Label ID="lblCustomerAddress" runat="server" Text="Address :" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtAddress" runat="server" CssClass="textBox" ReadOnly="true" Font-Bold="true" Width="200px"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblCustPhone" runat="server" Text="Phone :" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left">

                            <asp:TextBox ID="txtPhone" runat="server" ReadOnly="true" CssClass="textBox" Font-Bold="true" Width="200px"></asp:TextBox>
                        </td>
                        <td align="right"><b>GP:</b></td>
                        <td>


                            <asp:Label ID="lblGPNO" runat="server" Width="150px"></asp:Label>
                        </td>
                        <td></td>
                    </tr>

                    <tr>
                        <td align="right">
                            <asp:Label ID="lblInvoiceRemarks" runat="server" Text="Invoice Remarks:" Style="font-weight: 700"></asp:Label>
                        </td>
                        <td colspan="6">
                            <asp:Label ID="lblInvoiceRemarkstxt" runat="server" CssClass="textBox" Height="20px" Width="700px"></asp:Label>

                        </td>

                    </tr>

                    <tr>
                        <td align="right">
                            <asp:Label ID="lblTRANSPORTDTL" runat="server" Text="Transport Details:" Style="font-weight: 700"></asp:Label>
                        </td>
                        <td colspan="6">
                            <asp:TextBox ID="txtTRANSPORTDTL" runat="server" CssClass="textBox" Height="20px" Width="700px"></asp:TextBox>

                        </td>

                    </tr>


                    <tr>
                        <td align="right">
                            <asp:Label ID="lblRemarks" runat="server" Text="Remarks:" Style="font-weight: 700"></asp:Label>
                        </td>
                        <td colspan="6">
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="textBox" Height="20px" Width="700px" Enabled="true"></asp:TextBox>

                        </td>

                    </tr>

                    <tr>
                        <td align="right">
                            <asp:Label ID="lblCashAmount" runat="server" Text="Cash Amount:" Style="font-weight: 700"></asp:Label>
                        </td>
                        <td colspan="6">
                            <asp:TextBox ID="txtCashAmount" runat="server" CssClass="textBox" Height="20px"></asp:TextBox>

                        </td>

                    </tr>
                </table>

            </div>

            <div id="dvControls" style="height: auto; width: 100%;">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="height: auto; width: 100%;">


                    <div id="dvGridContainer" style="width: 100%; height: 100%;">


                        <div id="dvGrid" style="width: 90%; height: 300px; overflow: auto; align-content: flex-start;">
                          
                             <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>

                              
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="None"
                                Font-Names="Arial" Font-Size="9pt" PageSize="15" OnRowDataBound="GridView1_RowDataBound"
                                EmptyDataText="There Data Found" Width="70%" Style="margin-bottom: 0px" align="Left">
                                <PagerSettings Mode="NumericFirstLast" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <Columns>


                                    <%--  <asp:TemplateField HeaderText="IsVisible">
                <ItemTemplate >
                    <asp:CheckBox ID="chk" runat="server"   />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center"  />
                <ItemStyle BorderColor="Gray" HorizontalAlign="Center" Width="100px"  />
            </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="SL#">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnItemId" runat="server" Value='<%# Bind("ITEM_ID") %>' />
                                          <%--  <asp:HiddenField ID="hdnUomId" runat="server" Value='<%# Bind("UOM_ID") %>' />--%>
                                            <asp:Label ID="lblMrrDetSlNo" runat="server" Text='<%# Bind("DC_DET_SLNO") %>' Style="text-align: center;" Width="80px"> </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="DetailID" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDetID" runat="server" Text='<%# Bind("DC_DET_ID") %>' Style="text-align: center;" Width="150px"></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>
                                  
                                    <asp:TemplateField HeaderText="Item Name" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemName" runat="server" Text='<%# Bind("item_name") %>' Style="text-align: center;" Width="150px"></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="INV Qty" Visible="true">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtItemQty" runat="server" Text='<%# Bind("ITEM_QTY") %>' CssClass=" textBox textNumberOnly" Style="text-align: center;" Width="100px" OnTextChanged="txtITEMRate_TextChanged" AutoPostBack="true"></asp:TextBox>

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>


                                   <asp:TemplateField HeaderText="UOM" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUomcode" runat="server" Text='<%# Bind("UOM_CODE") %>'  Style="text-align: center;" Width="100px"></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Weigh. Avg Rate">  
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtItemWeighAvgRate" runat="server" Text='<%# Bind("WEIGHTED_AVERAGE_PRICE") %>' Enabled="false" CssClass=" textBox textNumberOnly" ReadOnly="true" Style="text-align: center;" Width="100px" OnTextChanged="txtITEMRate_TextChanged" AutoPostBack="true"></asp:TextBox>

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Item Sale Rate">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtITEMRate" runat="server" Text='<%# Bind("ITEM_RATE") %>' CssClass=" textBox textNumberOnly" Style="text-align: center;" Width="100px" OnTextChanged="txtITEMRate_TextChanged" AutoPostBack="true"></asp:TextBox>

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Total Cost">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtItemTotalCost" runat="server" Text='<%# Bind("TOTAL_COST") %>' CssClass=" textBox textNumberOnly" Style="text-align: center;" Width="100px" Enabled="false" ></asp:TextBox>  

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    

                                   

                                    <asp:TemplateField HeaderText="Remarks" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="txtDC_DETAILS_REMARKS" runat="server" Text='<%# Bind("DC_DET_REMARKS") %>' Style="text-align: center;" Width="150px"></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>






                                </Columns>
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#999999" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            </asp:GridView>
                                                    </ContentTemplate>
                                 </asp:UpdatePanel>
                                  
                        </div>

    

                    </div>

                </div>
            </div>

        </div>



        <div id="dvContentFooter" class="dvContentFooter">
            <table>
                <tr>
                    <td></td>
                    <td>
                        <asp:DropDownList ID="ddlReportViewType" runat="server" CssClass="dropDownList">
                            <asp:ListItem Value="0">Screen</asp:ListItem>
                            <asp:ListItem Selected="True" Value="1">PDF</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlReportViewMode" runat="server" CssClass="dropDownList">
                            <asp:ListItem Value="0">In This Tab</asp:ListItem>
                            <asp:ListItem Value="1">In New Tab</asp:ListItem>
                            <asp:ListItem Selected="True" Value="2">In New Window</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnAddNew" runat="server" Text="New" CssClass="buttonNew" Visible="false" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonCancel checkIsDirty" />
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="Update" CssClass="buttonSave checkRequired" AccessKey="s" OnClick="btnSave_Click" OnClientClick="if ( ! UserDeleteConfirmation()) return false;" Width="100px" />
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="buttonEdit" Visible="false" />
                    </td>
                     <td>
                        <asp:Button ID="btnInvoicePrint" runat="server" Text="Print Invoice" CssClass="buttoncommon" Enabled="true" OnClick="btnInvoicePrint_Click" />
                    </td>

                    <td>
                        <asp:Button ID="btnDcPrint" runat="server" Text="Print DC" CssClass="buttoncommon" Enabled="false" OnClick="btnDcPrint_Click" Visible="false" />
                    </td>

                    <td>
                        <asp:Button ID="btnGpPrint" runat="server" Text="Print GP" CssClass="buttoncommon" Enabled="false" OnClick="btnGpPrint_Click" OnClientClick="if ( ! UserDeleteConfirmation()) return false;" Visible="false" />
                    </td>

                    <td>
                        <asp:Button ID="btnPost" runat="server" Text="Post" CssClass="buttoncommon" Visible="false" />
                    </td>
                    <td>
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="buttonDelete" Visible="false" />
                    </td>
                    <td>
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="buttonRefresh checkIsDirty" Visible="false" />
                    </td>
                    <td>
                        <%--<uc:PrintButton runat="server" ID="ucPrintButton" DefaultPrintAction="Preview" AutoPrint="False" />--%>
                    </td>
                    <td>
                        <input id="btnClose" type="button" runat="server" class="buttonClose" value="Close" onclick="if (ContentForm) { ContentForm.CloseForm(); }" />
                    </td>
                    <td></td>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Print Format:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlReportFormat" runat="server" CssClass="dropDownList" Width="100px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnPopupTrigger" runat="server" Text="Button" CssClass="buttonHidden" />
                        <asp:HiddenField ID="hdnPopupTriggerID" runat="server" Value="" />
                        <asp:HiddenField ID="hdnPopupCommand" runat="server" Value="" />
                    </td>
                </tr>
            </table>
        </div>

        <%--<div id="dvContentFooter" class="dvContentFooter">
            <table align="center">
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnAddNew" runat="server" Text="New" CssClass="buttonNew" Visible="false"  />
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonSave"    OnClientClick="if ( ! UserDeleteConfirmation()) return false;"  />

                    </td>
                    <td>
                          <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="buttonEdit" />
                    </td>
                      <td>
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="buttonDelete" />
                    </td>
                    <td>
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonCancel checkIsDirty" />

                    </td>


                    <td>
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="buttonRefresh checkIsDirty" />
                    </td>


                    <td></td>


                    <td>
                        <input id="btnClose" type="button" runat="server" class="buttonClose" value="Close" onclick="if (ContentForm) { ContentForm.CloseForm(); }" />
                    </td>


                </tr>
            </table>
        </div>--%>
    </div>


</asp:Content>
