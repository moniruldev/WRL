<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="ProductionQA_Pass_View.aspx.cs" Inherits="PG.Web.Production.ProductionQA_Pass_View" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function UserDeleteConfirmation() {
            return confirm("Are you sure you want to Save and Authorized?");
        }

    </script>
     <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">

        var ReportViewPageLink = '<%=this.ReportViewPageLink%>';
        var ReportViewPDFPageLink = '<%=this.ReportViewPDFPageLink%>';
        var ReportPrintPageLink = '<%=this.ReportPrintPageLink%>';
        var ReportPDFPageLink = '<%=this.ReportPDFPageLink%>';

        

        function checkFilled() {
            var inputVal = document.getElementById("<%=txtRemarks.ClientID%>");
            if (inputVal.value == "") {
                inputVal.style.backgroundColor = "";
              
            }
            else {
                inputVal.style.backgroundColor = "";
            }
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

        $(document).ready(function () {

            checkFilled();

            //if ($('#' + txtSupplierName).is(':visible')) {

            //    bindSupplierList();
            //}


        });
        function bindSupplierList() {
            var cgColumns = [{ 'columnName': 'supcode', 'width': '80', 'align': 'left', 'highlight': 2, 'label': 'Code' }
                             , { 'columnName': 'supname', 'width': '150', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                             , { 'columnName': 'supaddress', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Address' }
                             , { 'columnName': 'supphone', 'width': '80', 'align': 'left', 'highlight': 0, 'label': 'phone' }



            ];


            //var companyid = $('#' + hdnCompanyID).val();
            //var depthead = $('#' + hdnEmpCode).val();
            //var locationid = $('#' + ddlLocation).val();
            // var seid = $('#' + txtExecutiveID).val();

            //var serviceURL = GLAccountServiceLink + "?isterm=1&includeempty=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            var serviceURL = SupplierListServiceLink + "?isterm=1&includeempty=0&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;
            //serviceURL += "&companyid=" + companyid;
            serviceURL += "&ispaging=1";
            // serviceURL += "&locationid=" + locationid;
            //serviceURL += "&seid=" + seid;
            // serviceURL += "&empstatus=" + "A";
            //serviceURL += "&acctypefilter=" + Enums.GLAccountTypeFilter.AllAccount;
            // serviceURL += "&namecomptype=" + Enums.DataCompareType.Contains;



            var supplierIDElem = $('#' + txtSupplierName);

            $('#' + btnSupplierID).click(function (e) {
                //elmID = $(elem).attr('id');
                //$(elem).combogrid("show");
                $(supplierIDElem).combogrid("dropdownClick");
            });


            $(supplierIDElem).combogrid({
                debug: true,
                searchButton: false,
                resetButton: false,
                alternate: true,
                munit: 'px',
                scrollBar: true,
                showPager: true,
                colModel: cgColumns,
                autoFocus: true,
                showError: true,
                width: 600,
                url: serviceURL,
                search: function (event, ui) {
                    //var companyCode = $('#' + ddlCompany).val();
                    //var branchCode = $('#' + hdnBranch).val();
                    //var deptCode = $('#' + hdnDepartment).val();
                    //var locationid = $('#' + lblLocationID).val();
                    // var seid = $('#' + txtExecutiveID).val();
                    var newServiceURL = serviceURL;
                    $(this).combogrid("option", "url", newServiceURL);


                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();

                        // $('#' + hdnDealerID).val('0');
                        $('#' + txtDealerID).val('');
                        return false;
                        //ClearGLAccountData(elemID);
                    }


                    if (ui.item.dealerid == '') {
                        event.preventDefault();
                        return false;
                        //ClearGLAccountData(elemID);
                    }
                    else {
                        // $('#' + hdnDealerID).val(ui.item.dealerid);
                        $('#' + hdnSupplierID).val(ui.item.supid);
                        $('#' + txtSupplierName).val(ui.item.supname);


                    }
                    return false;
                },

                lc: ''
            });


            $(supplierIDElem).blur(function () {
                var self = this;

                var customerID = $(supplierIDElem).val();
                if (customerID == '') {
                    // $('#' + hdnDealerID).val('0');
                    $('#' + txtSupplierName).val('');

                }
            });
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div id="dvPageContent" style="width: 100%; height: 100%;">

        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader" align="left">
                <asp:Label ID="lblHeader" runat="server" Text="Production QA Pass View" CssClass="lblHeader" Font-Bold="true" Font-Size="15px"></asp:Label>
            </div>
            <div id="dvMessage" class="dvMessage">
                <asp:Label ID="lblMessage" runat="server" CssClass="lblMessage" Text="" Width="100%"></asp:Label>
            </div>

            <div id="dvHeaderControl" class="dvHeaderControl">
            </div>

        </div>

        <div id="dvContentMain" class="dvContentMain" align="center">
            <asp:HiddenField ID="hdnSupplierID" runat="server" Value="0" />
            <asp:HiddenField ID="hdnIsTransfer" runat="server" />
            <asp:HiddenField ID="hdnQCID" runat="server" Value="0" />
            <asp:HiddenField ID="hdnStoreID" runat="server" Value="0" />
            <div id="dvControlsHead" style="height: auto; width: 100%;" align="center">
                <table border="0" cellspacing="4" cellpadding="2" align="center" style="width: 100%" id="tblItemEntry">

                    <tr>
                        <td align="right" >
                            <asp:Label ID="lblQAPassNo" runat="server" Text="QA Pass No :" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left" >

                            <asp:TextBox ID="txtQAPassNo" ReadOnly="true" runat="server" CssClass="colourdisabletextBox" Font-Bold="true" BackColor="#FFFFCC"></asp:TextBox>
                        </td>
                        <td align="right" >
                            <asp:Label ID="lblPassDate" runat="server" Text="Pass Date :" Font-Bold="true"></asp:Label><span style="color: red">*</span>

                        </td>

                        <td align="left" >

                            <asp:TextBox ID="txtPassDate" runat="server" CssClass="textBox textDate" Font-Bold="true"></asp:TextBox>
                        </td>
                    
                    </tr>


                   

                    <tr>
                        <td align="right">
                            <asp:Label ID="lblDepartment" runat="server" Text="Department :" Font-Bold="true"></asp:Label>
                        </td>
                         <td align="left">
                            <asp:Label ID="lblDepartmenttxt" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                      <td align="right" >
                            <asp:Label ID="lblRemarks" runat="server" Text="Remarks :" Font-Bold="true"></asp:Label>
                        </td>
                        <td  align="left"  >
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="textBox" onchange=" return checkFilled();"  TextMode="MultiLine" Height="30px" Width="300px"></asp:TextBox>

                        </td>
                     
                       
                    </tr>
                    

                </table>

            </div>

            <div id="dvControls" style="height: auto; width: 100%;">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="height: auto; width: 100%;">


                    <div id="dvGridContainer" style="width: 100%; height: 100%;">


                        <div id="dvGrid" style="width: 95%; height: 300px; overflow: auto; align-content: flex-start;">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="None"
                                Font-Names="Arial" Font-Size="9pt" PageSize="15"
                                EmptyDataText="No Data Found" Width="95%" Style="margin-bottom: 0px" OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting" OnRowCommand="GridView1_RowCommand" align="Left">
                                <PagerSettings Mode="NumericFirstLast" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <Columns>
                                    <asp:TemplateField HeaderText="SL#" HeaderStyle-Width="60px" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnItemId" runat="server" Value='<%# Bind("ITEM_ID") %>' />
                                            <%--<asp:HiddenField ID="hdnMRRDetId" runat="server" Value='<%# Bind("TRANS_DET_ID") %>' />--%>
                                            <asp:HiddenField ID="hdnPassDetId" runat="server" Value='<%# Bind("PROD_QA_DTL_ID") %>' />
                                            <%--<asp:HiddenField ID="hdnUomId" runat="server" Value='<%# Bind("UOM_ID") %>' />--%>
                                            <asp:Label ID="lblItemSlNo" runat="server" Text='<%# Container.DataItemIndex + 1 %>' Style="text-align: center;" Width="60px"> </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    
                                    <asp:TemplateField HeaderText="Batch No" Visible="true" HeaderStyle-Width="70px" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProdDtlBatchNo" runat="server" Text='<%# Bind("PROD_BATCH_NO_DTL") %>' Style="text-align: center;" Width="70px"></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                     <%-- <asp:TemplateField HeaderText="Status" Visible="true" HeaderStyle-Width="70px" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="dllStatus" runat="server" CssClass="dropDownList" Width="70px"  >
                                                
                                            </asp:DropDownList>

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>--%>

                                    <asp:TemplateField HeaderText="Item Name" Visible="true" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemName" runat="server" Text='<%# Bind("item_name") %>' Style="text-align: center;" Width="150px"></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Prod Item Qty" Visible="true" HeaderStyle-Width="70px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtItemQty" Text='<%# Bind("ITEM_QTY") %>' runat="server" CssClass=" textBox textNumberOnly" Style="text-align: center;" Width="70px"></asp:TextBox>
                                            <asp:HiddenField ID="hdnItemQty" runat="server" Value='<%# Bind("ITEM_QTY") %>' />
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Already Transfer" Visible="true" HeaderStyle-Width="70px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtAlreadyPassQty" Text='<%# Bind("ALREADY_PASS_QTY") %>' runat="server" CssClass=" textBox textNumberOnly" Style="text-align: center;" Width="70px"></asp:TextBox>
                                            
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    
                                     <asp:TemplateField HeaderText="Pending Qty" Visible="true" HeaderStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtItemPendingQty" Text='<%# Bind("PENDING_QTY") %>' runat="server" CssClass=" textBox textNumberOnly" Style="text-align: center;" Width="60px"></asp:TextBox>
                                            
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Pass Qty" Visible="true" HeaderStyle-Width="50px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtItemPassQty" Text='<%# Bind("PASS_QTY") %>' runat="server" CssClass=" textBox textNumberOnly" Style="text-align: center;" Width="50px"></asp:TextBox>
                                            
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    
                                    <asp:TemplateField HeaderText="Reject Qty" Visible="true" HeaderStyle-Width="80px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtItemRejectQty" Text='<%# Bind("REJECT_QTY") %>' runat="server" CssClass=" textBox textNumberOnly" Style="text-align: center;" Width="80px"></asp:TextBox>
                                            
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    
                                     <asp:TemplateField HeaderText="Weight1" Visible="true" HeaderStyle-Width="70px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtWeight1" Text='<%# Bind("WEIGHT") %>' runat="server" CssClass=" textBox textNumberOnly" Style="text-align: center;" Width="70px"></asp:TextBox>
                                            
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Weight2" Visible="true" HeaderStyle-Width="70px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtWeight2" Text='<%# Bind("WEIGHT_2") %>' runat="server" CssClass=" textBox textNumberOnly" Style="text-align: center;" Width="70px"></asp:TextBox>
                                            
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Thikness" Visible="true" HeaderStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtTHIKNESS1" Text='<%# Bind("THIKNESS") %>' runat="server" CssClass=" textBox textNumberOnly" Style="text-align: center;" Width="60px"></asp:TextBox>
                                            
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Spine Diameter" Visible="true" HeaderStyle-Width="70px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSPINE_DIAMETER" Text='<%# Bind("SPINE_DIAMETER") %>' runat="server" CssClass=" textBox textNumberOnly" Style="text-align: center;" Width="70px"></asp:TextBox>
                                            
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Frame Crack" Visible="true" HeaderStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnFrameCrack" runat="server" Value='<%# Bind("FRAME_CRACK") %>' />
                                            <asp:DropDownList ID="dllFrameCrack" runat="server" CssClass="dropDownList" Width="60px"  >
                                                
                                            </asp:DropDownList>
                                            
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Cavity" Visible="true" HeaderStyle-Width="60px">
                                        <ItemTemplate>
                                             <asp:HiddenField ID="hdnCavity" runat="server" Value='<%# Bind("CAVITY") %>' />
                                            <asp:DropDownList ID="dllCavity" runat="server" CssClass="dropDownList"  Width="50px" >
                                                
                                            </asp:DropDownList>
                                            
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Window Missing" Visible="true" HeaderStyle-Width="70px">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnWindowMissing" runat="server" Value='<%# Bind("WINDOW_MISSING") %>' />
                                            <asp:DropDownList ID="dllWindowMissing" runat="server" CssClass="dropDownList" Width="70px"  >
                                                
                                            </asp:DropDownList>
                                            
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Feather" Visible="true" HeaderStyle-Width="60px">
                                        <ItemTemplate>
                                             <asp:HiddenField ID="hdnFeather" runat="server" Value='<%# Bind("FEATHER") %>' />
                                            <asp:DropDownList ID="dllFeather" runat="server" CssClass="dropDownList"  Width="60px" >
                                                
                                            </asp:DropDownList>
                                            
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="QC Remarks" Visible="true" HeaderStyle-Width="100px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRemarks" TextMode="MultiLine" Text='<%# Bind("REMARKS") %>' runat="server" Style="text-align: center;" Width="100px" Rows="1"></asp:TextBox>

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                   <%--<asp:TemplateField HeaderText="Delete" Visible="true">
                                    <ItemTemplate>
                                    <asp:LinkButton ID="btnDeleteRow" CssClass="buttonDeleteIcon" Height="16px" Width="16px" CommandName="delete" runat="server">
                                    </asp:LinkButton>
                                   </ItemTemplate>
                                   <ItemStyle VerticalAlign="Top" />
                                   </asp:TemplateField>--%>




                                </Columns>
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#999999" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            </asp:GridView>
                        </div>



                    </div>

                </div>
            </div>

        </div>

        <div id="dvContentFooter" class="dvContentFooter">
            <table align="center">
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnAddNew" runat="server" Text="New" CssClass="buttonNew" Visible="false" OnClick="btnAddNew_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonSave" OnClick="btnSave_Click" OnClientClick="if ( ! UserDeleteConfirmation()) return false;" Visible="false" />

                    </td>
                    <td>
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="buttonEdit" OnClick="btnEdit_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="buttonDelete" Visible="false" />
                    </td>
                     <td>
                        <asp:Button ID="btnQCTransfer" runat="server" Text="Transfer to Dept." CssClass="buttoncommon" OnClick="btnQCTransfer_Click" />
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

                          <td>
                       <%-- <asp:Button ID="btnMRRPrint" runat="server" Visible="true" Text="Print PO" CssClass="buttoncommon" Enabled="True" OnClick="btnMRRPrint_Click" />--%>
                    </td>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="Print Format:"></asp:Label>
                    </td>
                   <%-- <td>--%>
                        <td>
                            <asp:DropDownList ID="ddlReportViewType" runat="server" CssClass="dropDownList">
                                <asp:ListItem Value="0">Screen</asp:ListItem>
                                <asp:ListItem Selected="True" Value="1">PDF</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                   <%-- </td>--%>
                    <td>
                        <asp:DropDownList ID="ddlReportViewMode" runat="server" CssClass="dropDownList">
                            <asp:ListItem Value="0">In This Tab</asp:ListItem>
                            <asp:ListItem Value="1">In New Tab</asp:ListItem>
                            <asp:ListItem Selected="True" Value="2">In New Window</asp:ListItem>
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


    </div>


</asp:Content>
