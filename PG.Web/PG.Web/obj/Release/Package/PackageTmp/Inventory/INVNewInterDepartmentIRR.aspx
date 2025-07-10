<%@ Page Title="" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="INVNewInterDepartmentIRR.aspx.cs" Inherits="PG.Web.Inventory.INVNewInterDepartmentIRR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function UserDeleteConfirmation() {
            return confirm("Are you sure you?");
        }

    </script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js" type="text/javascript"></script> 
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css"/> 
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap-theme.min.css"/> 

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:HiddenField ID="hdnLoggedInUser" runat="server" />
    <div id="dvPageContent" style="width: 100%; height: 100%;">
        <asp:HiddenField ID="hdnTRANSFER_TYPE_ID" runat="server" />
        <asp:HiddenField ID="hdnITCId" runat="server" />
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader_Prod" align="left">
                <asp:Label ID="lblHeader" runat="server" Text="IRR:IRR Against ITC(INTER DEPARTMENT)" CssClass="lblHeader" Font-Bold="true" Font-Size="15px"></asp:Label>
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
                        <td colspan="" align="left" class="auto-style2">
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="textBox" TextMode="MultiLine" Height="30px" Width="200px"></asp:TextBox>

                        </td>
                           <td style="" align="right">
                           <asp:Label ID="lblStorageLocation" runat="server" Text="Storage Location:" Visible="True" Font-Bold="true"></asp:Label>
                          </td>
                         <td align="left">
                          <asp:DropDownList ID="ddlStorageLocation" runat="server" Width="180px" CssClass="dropDownList" Visible="True">
                          </asp:DropDownList>

                         </td>

                    </tr>

                    <tr>
                        <td align="right" class="auto-style2">
                            <asp:Label ID="Label10" runat="server" Text="ITC No :" Font-Bold="true" ></asp:Label>
                        </td>
                        <td align="left" class="auto-style3">
                            <asp:TextBox ID="txtIssueNo" runat="server" CssClass="textBox" ReadOnly="true" Font-Bold="true" Width="150"></asp:TextBox>
                            <asp:CheckBox ID="chkDIRCT_ITC" runat="server" />
                        </td>
                        <td align="right" class="auto-style2">
                            <asp:Label ID="Label11" runat="server" Text="ITC Date :" Font-Bold="true" ></asp:Label>
                        </td>
                        <td align="left" class="auto-style3">

                            <asp:TextBox ID="txtIssueDate" runat="server" ReadOnly="true" CssClass="textBox" Font-Bold="true"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td align="right" class="auto-style2">
                            <asp:Label ID="Label2" runat="server" Text="ITC Remarks :" Font-Bold="true"></asp:Label>
                        </td>
                        <td colspan="" align="left" class="auto-style2">
                            <asp:TextBox ID="txtITCRemarks" runat="server" CssClass="textBox" TextMode="MultiLine" Height="30px" Width="250px" Enabled="false"></asp:TextBox>

                        </td>

                         <td align="right" class="auto-style2">
                            <asp:Label ID="lblReqNo" runat="server" Text="Req No. :" Font-Bold="true"></asp:Label>
                        </td>
                        <td colspan="" align="left" class="auto-style2">
                            <asp:TextBox ID="txtReqNo" runat="server" CssClass="textBox"  Enabled="false"></asp:TextBox>

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
                                CellPadding="1" CellSpacing="1" ForeColor="#CC00CC" GridLines="None" OnRowCommand="GridView1_RowCommand"
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
                                            <asp:Label ID="lblMrrDetSlNo" runat="server" Text='<%# Bind("ISS_RCV_DET_SLNO") %>' Style="text-align: center;" Width="80px"> </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Item Name" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItemName" runat="server" Text='<%# Bind("item_name") %>' Style="text-align: center;" Width="150px"></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Req. Qty" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="txtReqQty" runat="server" Text='<%# Bind("REQ_QNTY") %>' Style="text-align: center;" Width="100px"></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Issue Qty" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="txtPurchaseQty" runat="server" Text='<%# Bind("RCV_QNTY") %>' Style="text-align: center;" Width="100px"></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Batch" Visible="true">
                                        <ItemTemplate>
                                            <%--<asp:TextBox ID="txtBatchNo" TextMode="MultiLine" Text='<%# Bind("BATCH_NO") %>' runat="server" Style="text-align: center;" Width="150px" Rows="1"></asp:TextBox>--%>
                                           <asp:LinkButton ID="lnkdetailcom"  CommandName="itemdtl"  CssClass="buttoncommon"  CommandArgument='<%# Bind("ITEM_ID") %>' runat="server" CausesValidation="false" Width="52px" Height="17" >Batch</asp:LinkButton>  
                                            
                                        </ItemTemplate>
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Rcv Qty" Visible="true">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtMrrQty" Text='<%# Bind("RCV_QNTY") %>' runat="server" CssClass=" textBox textNumberOnly" Style="text-align: center;" Width="100px" OnTextChanged="txtMrrQty_TextChanged" AutoPostBack="true" ReadOnly="true" ></asp:TextBox>


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
                                <HeaderStyle BackColor="#FF66FF" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#999999" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            </asp:GridView>

                              <div id="myModalItemDetails" class="modal fade" >  
            <div class="modal-dialog" style="max-width:600px; align-content:space-around;"> <%-- --%>
                <div class="modal-content" style="max-width:600px;">  
                    <div class="modal-header" style="max-width:600px;">  
                        
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>  
                        <h4 class="modal-title">Item Details</h4>  
                    </div> 
                    <div class="col-md-12">
                        <asp:Label ID="lblItemName" runat="server" Text="Item Name : " Visible="true" ></asp:Label>
                        <asp:Label ID="lblItemNameText" runat="server" Visible="true"  Font-Bold="true" ></asp:Label>
                    </div> 
                    <div class="modal-body" style="overflow-y:scroll; overflow-x:scroll; max-height:300px; max-width:600px; margin-top: 10px; margin-bottom: 10px;"> <%-- --%>
                        <asp:Label ID="Label8" runat="server" ClientIDMode="Static" Visible="false"></asp:Label>  
                    
                        <asp:HiddenField ID="hdnBatchItemId" runat="server" />
                         <asp:HiddenField ID="hdnBatchMachineId" runat="server" />
                         <asp:HiddenField ID="hdnIssueStock" runat="server" />
                         <asp:HiddenField ID="hdnEditMode" runat="server" />
                 
                       <asp:GridView ID="grdItemDetails" runat="server" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="Black"
            RowStyle-BackColor="White" AlternatingRowStyle-BackColor="#A1DCF2" AlternatingRowStyle-ForeColor="#000"
            BorderStyle="None" BorderWidth="5px" CellPadding="10"  GridLines="Vertical" CssClass="gridView"
            AutoGenerateColumns="false">
            <Columns>
                 <asp:TemplateField HeaderText="SELECT">  
                    <ItemTemplate> 
                       <asp:CheckBox ID="chkSelect" runat="server" Width="52px"  Checked="false"  />  
                     </ItemTemplate> 
                   <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" /> 
                 </asp:TemplateField>  
                 <asp:TemplateField HeaderText="SL" ItemStyle-Width="30px">   
                     <ItemTemplate>
                         <asp:Label runat="server" ID="lblBatchSL" Text=" <%# Container.DataItemIndex + 1 %> "></asp:Label>
                              
                          <asp:HiddenField ID="hdnBatchNo" runat="server" Value='<%# Bind("PROD_BATCH_NO") %>' /> 
                          <asp:HiddenField ID="hdnOpeningQty" runat="server" Value='<%# Bind("SYSTEM_OPENING_STOCK") %>' /> 
                     </ItemTemplate>
                 </asp:TemplateField>
                <asp:TemplateField HeaderText="Batch">
                    <ItemTemplate>
                        <asp:Label ID="txtProdBatchNo" runat="server" CssClass="" Text='<%# Bind("PROD_BATCH_NO") %>' Width="80px" ReadOnly="true"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Stock">
                    <ItemTemplate>
                       <asp:TextBox ID="txtSysOpening" runat="server" CssClass="form-control" Text='<%# Bind("SYSTEM_OPENING_STOCK") %>' Width="80px" ReadOnly="true" ></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                      <asp:TemplateField HeaderText="UOM">
                    <ItemTemplate>
                        <asp:Label ID="txtClsUom" runat="server" CssClass="" Text='<%# Bind("CLOSING_UOM_NAME") %>' Width="50px" ></asp:Label>
                        <asp:HiddenField runat="server" ID="hdnClsUomId" Value='<%# Bind("CLOSING_UOM_ID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Receive Qty">
                    <ItemTemplate>
                        <asp:TextBox ID="txtusedQty" runat="server" CssClass="form-control" Text='<%# Bind("USED_QTY") %>' Width="100px" onkeypress=" return isNumberKey(event,this);" Enabled="false"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

                    <asp:TemplateField HeaderText="Req Qty">
                    <ItemTemplate>
                        <asp:TextBox ID="txtTotIssueQty" runat="server" CssClass="form-control" Text='<%# Bind("ISSUE_STOCK") %>' Width="100px" ReadOnly="true"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                
          
            </Columns>
        </asp:GridView>
 
                    </div>  
                
                    <div class="modal-footer"> 
                        <table>
                            <tr>
                        <td>
                         <asp:Button ID="btnStkAdd" runat="server" Text="Add" CssClass="buttonSave" OnClientClick="return ValidateBatchStock();"   OnClick="btnStkAdd_Click" Visible="true" />
                         <button type="button"  Class="buttonClose"  data-dismiss="modal">Close</button> 
                            
                             </td>
                                </tr>
                          </table> 
                    </div>  
                </div>  
            </div>  
        </div>  
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
