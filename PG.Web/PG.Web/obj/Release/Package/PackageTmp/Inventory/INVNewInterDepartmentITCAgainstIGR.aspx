<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="INVNewInterDepartmentITCAgainstIGR.aspx.cs" Inherits="PG.Web.Inventory.INVNewInterDepartmentITCAgainstIGR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function UserDeleteConfirmation() {
            return confirm("Are you sure you want to Save and Authorized?");
        }
        function isNumberKey(evt, obj) {

            var charCode = (evt.which) ? evt.which : event.keyCode
            var value = obj.value;
            var dotcontains = value.indexOf(".") != -1;
            if (dotcontains)
                if (charCode == 46) return false;
            if (charCode == 46) return true;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div id="dvPageContent" style="width: 100%; height: 100%;">

        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader_Prod" align="left">
                <asp:Label ID="lblHeader" runat="server" Text="ITC:ITC Against IGR" CssClass="lblHeader" Font-Bold="true" Font-Size="15px"></asp:Label>
            </div>
            <div id="dvMessage" class="dvMessage">
                <asp:Label ID="lblMessage" runat="server" CssClass="lblMessage" Text="" Width="100%"></asp:Label>
            </div>

            <div id="dvHeaderControl" class="dvHeaderControl">
            </div>

        </div>

        <div id="dvContentMain" class="dvContentMain" align="center">
            <div id="dvControlsHead" style="height: auto; width: 100%;" align="center">
                <asp:HiddenField ID="hdnREQId" runat="server" />
                <asp:HiddenField ID="hdnLoggedInUser" runat="server" />
                <table border="0" cellspacing="4" cellpadding="2" align="center" style="width: 100%" id="tblItemEntry">

                    <tr>
                        <td align="right" class="auto-style2">
                            <asp:Label ID="Label5" runat="server" Text="ITC No :" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left" class="auto-style3">

                            <asp:TextBox ID="txtIssueNo" ReadOnly="true" runat="server" CssClass="colourdisabletextBox" Font-Bold="true" BackColor="#FFFFCC"></asp:TextBox>
                        </td>
                        <td align="right" class="auto-style2">
                            <asp:Label ID="Label1" runat="server" Text="ITC Date :" Font-Bold="true"></asp:Label><span style="color: red">*</span>

                        </td>

                        <td align="left" class="auto-style3">

                            <asp:TextBox ID="txtIssueDate" runat="server" CssClass="textBox textDate" Font-Bold="true"></asp:TextBox>
                        </td>
                    </tr>


                    <tr>
                        <td align="right" class="auto-style2">
                            <asp:Label ID="Label4" runat="server" Text="Remarks :" Font-Bold="true"></asp:Label>
                        </td>
                        <td colspan="9" align="left" class="auto-style2">
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="textBox" TextMode="MultiLine" Height="30px" Width="700px"></asp:TextBox>

                        </td>

                    </tr>

                    <tr>
                        <td align="right" class="auto-style2">
                            <asp:Label ID="Label10" runat="server" Text="IGR No :" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left" class="auto-style3">
                            <asp:TextBox ID="txtIGRNo" runat="server" CssClass="textBox" ReadOnly="true" Font-Bold="true"></asp:TextBox>
                        </td>
                        <td align="right" class="auto-style2">
                            <asp:Label ID="Label11" runat="server" Text="IGR Date :" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left" class="auto-style3">

                            <asp:TextBox ID="txtIGRDate" runat="server" ReadOnly="true" CssClass="textBox" Font-Bold="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="auto-style2">
                            <asp:Label ID="Label2" runat="server" Text="IGR Remarks :" Font-Bold="true"></asp:Label>
                        </td>
                        <td colspan="6" align="left" class="auto-style2">
                            <asp:TextBox ID="txtIGRRemarks" runat="server" CssClass="textBox" TextMode="MultiLine" Height="30px" Width="700px" Enabled="false"></asp:TextBox>
                            <asp:HiddenField ID="hdnFromDeptID" runat="server" />
                            <asp:HiddenField ID="hdnToDeptID" runat="server" />
                        </td>
                        <td>IGR From</td>
                        <td>
                            <asp:TextBox ID="txtIGRFrom" runat="server" ReadOnly="true" CssClass="textBox" Font-Bold="true" Enabled="false"></asp:TextBox>
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
                                EmptyDataText="There Data Found" Width="95%" Style="margin-bottom: 0px" OnRowDataBound="GridView1_RowDataBound" align="Left">
                                <PagerSettings Mode="NumericFirstLast" />
                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                <Columns>
                                    <asp:TemplateField HeaderText="SL#" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnItemId" runat="server" Value='<%# Bind("ITEM_ID") %>' />
                                            <asp:HiddenField ID="hdnReqDetId" runat="server" Value='<%# Bind("REQ_DET_ID") %>' />
                                            <asp:HiddenField ID="hdnReqIssueDetId" runat="server" Value='<%# Bind("REQ_ISSUE_DET_ID") %>' />
                                            <asp:HiddenField ID="hdnUomId" runat="server" Value='<%# Bind("UOM_ID") %>' />
                                            <asp:Label ID="lblMrrDetSlNo" runat="server" Text='<%# Bind("REQ_ISSUE_DET_SLNO") %>' Style="text-align: center;" Width="80px"> </asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" Width="80px"></HeaderStyle>

                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Item Name" Visible="true" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemName" runat="server" Text='<%# Bind("item_name") %>' Style="text-align: center;" Width="150px"></asp:Label>

                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" Width="150px"></HeaderStyle>

                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Req Qty" Visible="true" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="txtPurchaseQty" runat="server" Text='<%# Bind("REQ_APRV_QNTY") %>' Style="text-align: center;" Width="100px"></asp:Label>

                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>

                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Already Issued" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="txtAlreadyIssued" runat="server" Text='<%# Bind("ALREADRY_ISSUED_QTY") %>' Style="text-align: center;" Width="100px"></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Issue Qty" Visible="true">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtMrrQty" Text='<%# Bind("ISSUE_QNTY") %>' runat="server" CssClass=" textBox textNumberOnly" Style="text-align: center;" Width="100px" onkeypress="return isNumberKey(event,this);" ></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Batch No" Visible="true">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtBatchNo" TextMode="MultiLine" Text='<%# Bind("BATCH_NO") %>' runat="server" Style="text-align: center;" Width="150px" Rows="1"></asp:TextBox>
                                            
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Specification" Visible="true">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnSpecificationId" runat="server" Value='<%# Bind("ITEM_SPECIFICATION_ID") %>' />
                                            <asp:Label ID="lblSpecification" runat="server" Text='<%# Bind("SPECIFICATION_TYPE") %>' Style="text-align: center;" Width="100px"></asp:Label>                 
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Stock Qty" Visible="true" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtStockQty" Text='<%# Bind("CLOSING_QTY") %>' runat="server" CssClass=" textBox textNumberOnly" Style="text-align: center;" Width="100px"></asp:TextBox>


                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>

                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Unit Name" Visible="true" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnitName" runat="server" Text='<%# Bind("UOM_NAME") %>' Style="text-align: center;" Width="100px"></asp:Label>

                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>

                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="IGR.Remarks" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="txtIGRRemarks" runat="server" Text='<%# Bind("REQ_NOTE") %>' Style="text-align: center;" Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="ITC.Remarks" Visible="true">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRemarks" TextMode="MultiLine" Text='<%# Bind("REQ_ISSUE_NOTE") %>' runat="server" Style="text-align: center;" Width="150px" Rows="1"></asp:TextBox>

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#CC33FF" Font-Bold="True" ForeColor="White" />
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
                        <asp:Button ID="btnAddNew" runat="server" Text="New" CssClass="buttonNew" Visible="false" OnClick="btnAddNew_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonSave" OnClick="btnSave_Click" OnClientClick="if ( ! UserDeleteConfirmation()) return false;" />

                    </td>
                    <td>
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="buttonEdit" />
                    </td>
                    <td>
                        <asp:Button ID="btnMRRPrint" runat="server" Text="Print IGR" CssClass="buttoncommon" Enabled="True" OnClick="btnMRRPrint_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnITCPrint" runat="server" Text="Print ITC" CssClass="buttoncommon" Enabled="True" OnClick="btnITCPrint_Click" />
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
        </div>


    </div>


</asp:Content>
