<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="INVNewIRRByStore.aspx.cs" Inherits="PG.Web.Inventory.INVNewIRRByStore" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function UserDeleteConfirmation() {
            return confirm("Are you sure you want to Save and Authorized?");
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hdnLoggedInUser" runat="server" />
    <asp:HiddenField ID="hdnStoreID" runat="server"  Value="0"/>
    <div id="dvPageContent" style="width: 100%; height: 100%;">

        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader" align="left">
                <asp:Label ID="lblHeader" runat="server" Text="IRR:Against ITC(By Store)" CssClass="lblHeader" Font-Bold="true" Font-Size="15px"></asp:Label>
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
                            <asp:Label ID="Label5" runat="server" Text="IRR No :" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left" class="auto-style3">

                            <asp:TextBox ID="txtIRRNo" ReadOnly="true" runat="server" CssClass="colourdisabletextBox" Font-Bold="true" BackColor="#FFFFCC"></asp:TextBox>
                        </td>
                        <td align="right" class="auto-style2">
                            <asp:Label ID="Label1" runat="server" Text="IRR Date :" Font-Bold="true"></asp:Label><span style="color:red">*</span></td>
                       
                        <td align="left" class="auto-style3">

                            <asp:TextBox ID="txtIRRDate" runat="server" CssClass="textBox textDate" Font-Bold="true"></asp:TextBox>
                        </td>
                    </tr>


                    <tr>
                        <td align="right" class="auto-style2">
                            <asp:Label ID="Label4" runat="server" Text="Remarks :" Font-Bold="true"></asp:Label>
                        </td>
                        <td colspan="9" align="left" class="auto-style2">
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="textBox" TextMode="MultiLine" Height="30px" Width="850px"></asp:TextBox>

                        </td>

                    </tr>
                    <tr>
                             <td align="right">
                             <asp:Label ID="Label7" runat="server" Font-Bold="true" Text="Department:"></asp:Label><span style="color: red">*</span>

                       </td>
                       <td align="left">
                       <asp:DropDownList ID="ddlFromDepartment" runat="server" CssClass="dropDownList required">
                       </asp:DropDownList>
                       </td>
                    </tr>

                    <tr>
                        <td align="right" class="auto-style2">
                            <asp:Label ID="Label10" runat="server" Text="ITC No :" Font-Bold="true" Font-Size="9"></asp:Label>
                        </td>
                        <td align="left" class="auto-style3">
                            <asp:TextBox ID="txtIssueNo" runat="server" CssClass="textBox" ReadOnly="true" Font-Bold="true" Width="150"></asp:TextBox>
                        </td>
                        <td align="right" class="auto-style2">
                            <asp:Label ID="Label11" runat="server" Text="ITC Date :" Font-Bold="true" Font-Size="9"></asp:Label>
                        </td>
                        <td align="left" class="auto-style3">

                            <asp:TextBox ID="txtIssueDate" runat="server" ReadOnly="true" CssClass="textBox" Font-Bold="true"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td align="right" class="auto-style2">
                            <asp:Label ID="Label2" runat="server" Text="ITC Remarks :" Font-Bold="true"></asp:Label>
                        </td>
                        <td colspan="9" align="left" class="auto-style2">
                            <asp:TextBox ID="txtITCRemarks" runat="server" CssClass="textBox" TextMode="MultiLine" Height="30px" Width="850px" Enabled="false"></asp:TextBox>

                        </td>

                    </tr>
                    
                     <tr>
                        <td align="right" class="auto-style2">
                            <asp:Label ID="lblBatteryType" runat="server" Text="Battery Type :" Font-Bold="true" Font-Size="9" Visible="false"></asp:Label>
                        </td>
                        <td align="left" class="auto-style3">
                            <asp:Label ID="lblBatteryTypetxt" runat="server"  Font-Bold="true" Font-Size="9" Visible="false"></asp:Label>
                             <asp:HiddenField ID="hdnBatteryTypeID" runat="server" />
                           
                        </td>
                        <td align="right" class="auto-style2">
                            <asp:Label ID="lblIsRepair" runat="server" Text="Is Repair :" Font-Bold="true" Font-Size="9" Visible="false"></asp:Label>
                        </td>
                        <td align="left" class="auto-style3">

                           <asp:Label ID="lblIsRepairtxt" runat="server" Font-Bold="true" Font-Size="9" Visible="false"></asp:Label>
                             <asp:HiddenField ID="hdnIsRepair" runat="server" />
                        </td>
                    </tr>

                    <tr>
                        <td align="right" class="auto-style2">
                            <asp:Label ID="lblSNDNOTTRANSFER" runat="server" Text="SND Transfer Type :" Font-Bold="true" Font-Size="9" ForeColor="Red" Visible="false"></asp:Label>
                        </td>
                        <td align="left" class="auto-style3">
                           <asp:DropDownList ID="ddlSNDTRANSFERTYPE" runat="server" CssClass="dropDownList" Visible="false">
                               <asp:ListItem Selected="True" Text="All" Value=""></asp:ListItem>
                               <asp:ListItem Text="YES" Value="Y"></asp:ListItem>
                               <asp:ListItem  Text="NO" Value="N"></asp:ListItem>
                           </asp:DropDownList>
                           
                        </td>
                        <td align="right" class="auto-style2">
                            <asp:Label ID="Label3" runat="server" Text="Return:"></asp:Label>
                        </td>
                        <td align="left" class="auto-style3">
                             <asp:CheckBox ID="ChkISRETURN" runat="server"  />
                          
                        </td>
                        
                    </tr>

                </table>

            </div>
            <br />
            <br />
            <div id="dvControls" style="height: auto; width: 100%;">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="height: auto; width: 100%;">


                    <div id="dvGridContainer" style="width: 100%; height: 100%;">


                        <div id="dvGrid" style="height: 300px; overflow: auto;">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                                CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="None"
                                Font-Names="Arial" Font-Size="9pt" PageSize="15"
                                EmptyDataText="There is no record" Width="100%" Style="margin-bottom: 0px">
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
                                            <asp:HiddenField ID="hdnUomId" runat="server" Value='<%# Bind("UOM_ID") %>' />
                                            <asp:Label ID="lblMrrDetSlNo" runat="server" Text='<%# Bind("ISS_RCV_DET_SLNO") %>' Style="text-align: center;" Width="20px"> </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Item Name" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemName" runat="server" Text='<%# Bind("item_name") %>' Style="text-align: center;" Width="150px"></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Item Type" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemType" runat="server" Text='<%# Bind("ITEM_TYPE_CODE") %>' Style="text-align: center;" Width="100px"></asp:Label>
                                            <asp:HiddenField ID="hdnItemTypeID" runat="server" Value='<%# Bind("ITEM_TYPE_ID") %>' />
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="UOM" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUomName" runat="server" Text='<%# Bind("uom_code") %>' Style="text-align: center;" Width="40px"></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    
                                    <asp:TemplateField HeaderText="Issue Qty" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="txtPurchaseQty" runat="server" Text='<%# Bind("RCV_QNTY") %>' Style="text-align: center;" Width="100px"></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rcv Qty" Visible="true">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtMrrQty" Text='<%# Bind("RCV_QNTY") %>' runat="server" CssClass=" textBox textNumberOnly" Style="text-align: center;" Width="100px" OnTextChanged="txtMrrQty_TextChanged" AutoPostBack="true" ReadOnly="true" ></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                             <%--         <asp:TemplateField HeaderText="Unit Price" Visible="true">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtUNIT_PRICE" Text='<%# Bind("UNIT_PRICE") %>' runat="server" CssClass=" textBox textNumberOnly" Style="text-align: center;" Width="100px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>--%>

                                     <asp:TemplateField HeaderText="Specification" Visible="true">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnSpecificationId" runat="server" Value='<%# Bind("ITEM_SPECIFICATION_ID") %>' />
                                            <asp:Label ID="lblSpecification" runat="server" Text='<%# Bind("SPECIFICATION_TYPE") %>' Style="text-align: center;" Width="100px"></asp:Label>                 
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                   

                                     <asp:TemplateField HeaderText="ITC.Remarks" Visible="true">
                                        <ItemTemplate>
                                              <asp:Label ID="lblItcNote" runat="server" Text='<%# Bind("REQ_ISSUE_NOTE") %>' Style="text-align: center;" Width="100px"></asp:Label>
                                          

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="IRR.Remarks" Visible="true">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRemarks" Text='<%# Bind("RECEIVE_NOTE") %>' TextMode="MultiLine" CssClass="textBox" runat="server" Style="text-align: center;" Width="100px" Rows="1" ></asp:TextBox>

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="hdnTransDetId" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTranDetId" runat="server" Text='<%# Bind("ISSUE_RECEIVE_DET_ID") %>' Style="text-align: center;" Width="100px"></asp:Label>

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
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonSave"  OnClick="btnSave_Click" OnClientClick="if ( ! UserDeleteConfirmation()) return false;" />

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
        </div>


    </div>
</asp:Content>
