<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="INVNewReceiveFromReturn.aspx.cs" Inherits="PG.Web.Inventory.INVNewReceiveFromReturn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function UserDeleteConfirmation() {
            return confirm("Are you sure you want to Save and Authorized?");
        }

    </script>
    <style type="text/css">
        .auto-style1 {
            height: 36px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hdnLoggedInUser" runat="server" />
    <asp:HiddenField ID="hdnStoreID" runat="server" />
    <asp:HiddenField ID="hdnIsQC" runat="server" />
    <asp:HiddenField ID="hdnPurchaseId" runat="server" />
    <div id="dvPageContent" style="width: 100%; height: 100%;">

        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader" align="left">
                <asp:Label ID="lblHeader" runat="server" Text="Receive From Supplier Return" CssClass="lblHeader" Font-Bold="true" Font-Size="15px"></asp:Label>
            </div>
            <div id="dvMessage" class="dvMessage">
                <asp:Label ID="lblMessage" runat="server" CssClass="lblMessage" Text="" Width="100%"></asp:Label>
            </div>

            <div id="dvHeaderControl" class="dvHeaderControl">
            </div>

        </div>

        <div id="dvContentMain" class="dvContentMain" align="center">
            <div id="dvControlsHead" style="height: auto; width: 100%;" align="center">
                <table border="0" cellspacing="4" cellpadding="2" align="center" style="width: 100%" id="tblItemEntry">

                    <tr>
                        <td align="right" class="auto-style2">
                            <asp:Label ID="lblReceiveNo" runat="server" Text="Receive No :" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left" class="auto-style3">

                            <asp:TextBox ID="txtReceiveNo" ReadOnly="true" runat="server" CssClass="textBox" Font-Bold="true"></asp:TextBox>
                        </td>
                        <td align="right" class="auto-style2">
                            <asp:Label ID="lblReceiveDate" runat="server" Text="Receive Date :" Font-Bold="true"></asp:Label><span style="color: red">*</span></td>
                        <td align="left" class="auto-style3">
                            <asp:TextBox ID="txtReceiveDate" runat="server" CssClass="textBox textDate" Font-Bold="true"></asp:TextBox>
                        </td>
                    </tr>


                    <tr>
                        <td align="right" class="auto-style1">
                            <asp:Label ID="lblRemarks" runat="server" Text="Remarks :" Font-Bold="true"></asp:Label>
                        </td>
                        <td colspan="" align="left" class="auto-style1">
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="textBox" TextMode="MultiLine" Height="30px" Width="200px"></asp:TextBox>

                        </td > 
                         <td align="right" class="auto-style1">
                             <asp:Label ID="lblFromDepartment" runat="server" Font-Bold="true" Text="Department:"></asp:Label><span style="color: red">*</span>

                       </td>
                       <td align="left" class="auto-style1">
                       <asp:DropDownList ID="ddlFromDepartment" runat="server" CssClass="dropDownList">
                       </asp:DropDownList>
                       </td>

                    </tr>

                    <tr>
                        <td align="right" class="auto-style2">
                            <asp:Label ID="lblPurchaseNo" runat="server" Text="Purchase No :" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left" class="auto-style3">
                            <asp:TextBox ID="txtPurchaseNo" runat="server" CssClass="textBox" ReadOnly="true" Font-Bold="true"></asp:TextBox>
                            &nbsp;
                            
                            
                        </td>
                     
                        <td align="right" class="auto-style2">
                            <asp:Label ID="lblReturnNo" runat="server" Text="Return No :" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left" class="auto-style3">

                            <asp:TextBox ID="txtReturnNo" runat="server" ReadOnly="true" CssClass="textBox" Font-Bold="true"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>

                        <td align="right" class="auto-style2">
                            <asp:Label ID="lblSupplier" runat="server" Text="Supplier :" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left" class="auto-style2">
                            <asp:TextBox ID="txtSupplier" runat="server" CssClass="textBox" Width="300px" Enabled="false"></asp:TextBox>
                            <asp:HiddenField ID="hdnSupplierId" runat="server" />
                        </td>
                       <td align="right" class="auto-style2">
                            <asp:Label ID="lblChallanNo" runat="server" Text="Challan No :" Font-Bold="true"></asp:Label><span style="color: red">*</span>
                        </td>
                        <td colspan="4" align="left" class="auto-style2">
                            <asp:TextBox ID="txtPrefix" runat="server" Width="80px" CssClass="textBox" Style="border-right:0; float:left;"  ></asp:TextBox>
                            <asp:TextBox ID="txtChallanNo" runat="server" CssClass="textBox" Width="200px"  Enabled="true" Style="border-left:0; float:left"></asp:TextBox>

                        </td>

                    </tr>
                    <tr>
                         <td align="right"> 
                            <asp:Label ID="lblMrrNo" runat="server" Text="MRR No :" Font-Bold="true"></asp:Label>

                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtMrrNo" runat="server" CssClass="textBox" ReadOnly="true" Font-Bold="true"></asp:TextBox>
                        </td>

                    </tr>
                    
                </table>

            </div>

            <div id="dvControls" style="height: auto; width: 100%;">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="height: auto; width:900px; float:left;">
                    <div id="dvGridContainer" style="width: 100%; height: 100%;">
                        <div id="dvGrid" style="width: 100%; height: 300px; overflow: auto;">
                            <asp:UpdatePanel ID="updatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound"
                                        CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="None"
                                        Font-Names="Arial" Font-Size="9pt" PageSize="15"
                                        EmptyDataText="There is no record" Width="100%" Style="margin-bottom: 0px">
                                        <PagerSettings Mode="NumericFirstLast" />
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL#" ControlStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdnItemId" runat="server" Value='<%# Bind("ITEM_ID") %>' />
                                                    <%--<asp:HiddenField ID="hdnUomId" runat="server" Value='<%# Bind("UOM_ID") %>' />--%>
                                                    <asp:Label ID="lblRcvDetSlNo" runat="server" Text='<%# Bind("RCV_DET_SLNO") %>' Style="text-align: center;" Width="50px"> </asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Top" Width="50px" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Item Name" Visible="true" ControlStyle-Width="200px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemName" runat="server" Text='<%# Bind("ITEM_NAME") %>' Style="text-align: center;" Width="200px"></asp:Label>
                                                     <%--<asp:HiddenField ID="hdnIsQcItem" runat="server" Value='<%# Bind("IS_QC") %>' />--%>
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Top" Width="200" />
                                            </asp:TemplateField>

                                               <asp:TemplateField HeaderText="Item Type" Visible="true" ControlStyle-Width="60px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemType" runat="server" Text='<%# Bind("ITEM_TYPE_CODE") %>' Style="text-align: center;" Width="60px"></asp:Label>
                                                    <asp:HiddenField ID="hdnItemTypeID" runat="server" Value='<%# Bind("ITEM_TYPE_ID") %>' />
                                                    
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Top" Width="60px" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Return Qty" Visible="true" ControlStyle-Width="100px">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtRtnQty" Text='<%# Bind("RTN_QTY") %>' runat="server" CssClass="textBox textNumberOnly " Style="text-align: center;" Width="100px" Enabled="true" ></asp:Label>


                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Top" Width="100px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Receive Qty" Visible="true" ControlStyle-Width="100px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtRcvQty" Text='<%# Bind("RECEIVE_QNTY") %>' runat="server" CssClass="textBox textNumberOnly " Style="text-align: center;" Width="100px" Enabled="true" ></asp:TextBox>


                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Top" Width="100px" />
                                            </asp:TemplateField>

                                               <asp:TemplateField HeaderText="UOM" Visible="true" ControlStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUomName" runat="server" Text='<%# Bind("UOM_NAME") %>' Style="text-align: center;" Width="50px"></asp:Label>
                                                      <asp:HiddenField ID="hdnUomID" runat="server" Value='<%# Bind("UOM_ID") %>' />
                                                      <asp:DropDownList ID="ddlUOM" runat="server"  CssClass="dropDownList" Visible="false"> </asp:DropDownList>
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Top" Width="50px" />
                                            </asp:TemplateField>

                                       


                                            <asp:TemplateField HeaderText="Unit Price" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtUnitPrice" Text='<%# Bind("PURCHASE_RATE") %>' CssClass="textBox" runat="server" Style="text-align: center;" Width="100px"></asp:Label>
                                                    
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Top" />
                                            </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Unit Price" Visible="false">
                                                <ItemTemplate>
                                                     <asp:Label ID="txtPurchaseQty" Text='<%# Bind("PURCHASE_QTY") %>' CssClass="textBox" runat="server" ></asp:Label>
                                                     <asp:Label ID="txtPurchaseDtlId" Text='<%# Bind("PURCHASE_DET_ID") %>' CssClass="textBox" runat="server" ></asp:Label>
                                                     <asp:Label ID="txtPurchaseUom" Text='<%# Bind("PURCHASE_UOM_ID") %>' CssClass="textBox" runat="server"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Cost" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtTotalCost" Text='<%# Bind("PURCHASE_TOTAL_PRICE") %>'  CssClass="textBox" runat="server" Style="text-align: center;" Width="100px" onkeypress="return onlyNos(event,this);"></asp:Label>

                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Top" />
                                            </asp:TemplateField>

                                      
                                            <asp:TemplateField HeaderText="Rcv. Remarks" Visible="true">
                                                <ItemTemplate>


                                                    <asp:TextBox ID="txtRemarks" Text='<%# Bind("RCV_DET_NOTE") %>' TextMode="MultiLine" CssClass="textBox" runat="server" Style="text-align: center;" Width="100px" Height="21px"></asp:TextBox>

                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Top" Width="100px" />
                                            </asp:TemplateField>




                                            <asp:TemplateField HeaderText="hdnTransDetId" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTranDetId" runat="server" Text='<%# Bind("RTN_DET_ID") %>' Style="text-align: center;" Width="100px"></asp:Label>

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
                        <asp:Button ID="btnMRRPrint" runat="server" Text="Print MRR" CssClass="buttoncommon" Visible="true" Enabled="True" OnClick="btnMRRPrint_Click" />
                    </td>

                    <td>
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="buttonDelete" Visible="false" />
                    </td>

                    <td>
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonCancel checkIsDirty" Visible="false" Enabled="false" />

                    </td>


                    <td>
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="buttonRefresh checkIsDirty" Visible="false" />
                    </td>


                    <td></td>


                    <td>
                        <input id="btnClose" type="button" runat="server" class="buttonClose" value="Close" onclick="if (ContentForm) { ContentForm.CloseForm(); }" />
                    </td>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Print Format:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlReportFormat" runat="server" CssClass="dropDownList" Width="100px">
                        </asp:DropDownList>
                    </td>

                </tr>
            </table>
        </div>


    </div>
</asp:Content>
