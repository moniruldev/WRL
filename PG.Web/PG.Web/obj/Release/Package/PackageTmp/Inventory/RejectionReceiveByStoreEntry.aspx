<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="RejectionReceiveByStoreEntry.aspx.cs" Inherits="PG.Web.Inventory.RejectionReceiveByStoreEntry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
           function UserDeleteConfirmation() {
            return confirm("Are you sure you?");
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
     <asp:HiddenField ID="hdnLoggedInUser" runat="server" />
    <asp:HiddenField ID="hdnStoreID" runat="server" Value="1" />
    <asp:HiddenField ID="hdnDeptId" runat="server" Value="26" />
    <div id="dvPageContent" style="width: 100%; height: 100%;">

        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader" align="left">
                <asp:Label ID="lblHeader" runat="server" Text="IRR:IRR Against ITC" CssClass="lblHeader" Font-Bold="true" Font-Size="15px"></asp:Label>
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
                            <asp:Label ID="Label5" runat="server" Text="Receive No :" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left" class="auto-style3">

                            <asp:TextBox ID="txtReceiveNo" ReadOnly="true" runat="server" CssClass="colourdisabletextBox" Font-Bold="true" BackColor="#FFFFCC" Width="200px"></asp:TextBox>
                        </td>
                        <td align="right" class="auto-style2">
                            <asp:Label ID="lblReceiveDate" runat="server" Text="Receive Date :" Font-Bold="true"></asp:Label><span style="color:red">*</span></td>
                       
                        <td align="left" class="auto-style3">

                            <asp:TextBox ID="txtReceiveDate" runat="server" CssClass="textBox textDate" Font-Bold="true" Width="200px"></asp:TextBox>
                        </td>
                    </tr>


              

                    <tr>
                        <td align="right" class="auto-style2">
                            <asp:Label ID="lblIssueNo" runat="server" Text="Issue No :" Font-Bold="true" ></asp:Label>
                        </td>
                        <td align="left" class="auto-style3">
                            <asp:TextBox ID="txtIssueNo" runat="server" CssClass="textBox" ReadOnly="true" Font-Bold="true" Width="200"></asp:TextBox>
                        </td>

                       
                        <td align="right" class="auto-style2">
                            <asp:Label ID="lblIssueDate" runat="server" Text="Issue Date :" Font-Bold="true" ></asp:Label>
                        </td>
                        <td align="left" class="auto-style3">

                            <asp:TextBox ID="txtIssueDate" runat="server" ReadOnly="true" CssClass="textBox" Font-Bold="true" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                      <tr>
                        <td align="right" class="auto-style2">
                            <asp:Label ID="lblRemarks" runat="server" Text="Remarks :" Font-Bold="true"></asp:Label>
                        </td>
                        <td colspan="" align="left" class="auto-style2">
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="textBox" TextMode="MultiLine" Height="30px" Width="200px"></asp:TextBox>

                        </td>

                            <td align="right" class="auto-style2">
                            <asp:Label ID="lblIssueFromDept" runat="server" Text="Issue From :" Font-Bold="true"></asp:Label>
                        </td>
                        <td colspan="" align="left" class="auto-style2">
                            <asp:TextBox ID="txtIssueFrom" runat="server" CssClass="textBox"  Width="200px" Enabled="false"></asp:TextBox>

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
                                EmptyDataText="There is no record" Width="100%" Style="margin-bottom: 0px;">
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
                                              <asp:HiddenField runat="server" ID="hdnItemType" Value='<%# Bind("ITEM_TYPE_ID") %>'  />
                                            <asp:HiddenField ID="hdnUomId" runat="server" Value='<%# Bind("UOM_ID") %>' />
                                            <asp:Label ID="lblSlNo" runat="server" Text='<%# Bind("REJ_RCV_DET_SLNO") %>' Style="text-align: center;" Width="20px"> </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" Width="20px" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Item Name" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemName" runat="server" Text='<%# Bind("ITEM_NAME") %>' Style="text-align: center;" Width="300px"></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" Width="300px" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Uom" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUomName" runat="server" Text='<%# Bind("UOM_NAME") %>' Style="text-align: center;" Width="60px"></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" Width="60px" />
                                    </asp:TemplateField>
                                 

                                    <asp:TemplateField HeaderText="Issue Qty" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="txtIssueQty" runat="server" Text='<%# Bind("ISSUE_QTY") %>' Style="text-align: center;" Width="60px"></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" Width="60px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rcv Qty" Visible="true">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRcvQty" Text='<%# Bind("RCV_QNTY") %>' runat="server" CssClass=" textBox textNumberOnly" Style="text-align: center;" Width="60px" OnTextChanged="txtRcvQty_TextChanged" AutoPostBack="true" ReadOnly="true" onkeypress="return isNumberKey(event,this);" ></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" Width="60px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="IRR.Remarks" Visible="true">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRemarks" Text='<%# Bind("RECEIVE_NOTE") %>' TextMode="MultiLine" CssClass="textBox" runat="server" Style="text-align: center;" Width="100px" Rows="1" ></asp:TextBox>

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" Width="100px" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="hdnTransDetId" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransDetId" runat="server" Text='<%# Bind("REJ_RECEIVE_DET_ID") %>' Style="text-align: center;" Width="100px"></asp:Label>

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
