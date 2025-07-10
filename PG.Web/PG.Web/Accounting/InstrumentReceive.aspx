<%@ Page Title="Instrument Receive" Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="InstrumentReceive.aspx.cs" Inherits="PG.Web.Accounting.InstrumentReceive" %>
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


           function tbopen(key) {
               key = key || '';

               var url = IForm.RootPath + "Accounting/GeneralLedger/Journal.aspx?id=" + key;

               if (IForm.PageMode == Enums.PageMode.InTab) {

                   var tdata = new xtabdata();
                   tdata.linktype = Enums.LinkType.Direct;
                   tdata.id = 0;
                   tdata.name = "Journal";
                   //tdata.label = "User: " + userid;
                   tdata.label = "Journal";
                   tdata.type = 0;
                   tdata.url = url;
                   tdata.tabaction = Enums.TabAction.InNewTab;
                   tdata.selecttab = 1;
                   tdata.reload = 0;
                   tdata.param = "";


                   try {
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

           function tbopenSalInfo(key) {
               key = key || '';


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

           $(document).ready(function () {


           });




           function fromParent(val1) {
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
            background-color: #FFFFFF;
            color: #000000;
            font-size: 11px;
            width: 160px;
            height: 16px;
            padding-left: 2px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

 <div id="dvPageContent" style="width: 100%; height: auto;">
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader">
                <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Instrument Receive"></asp:Label>
            </div>
            <!--Message Div -->
            <div id="dvMsg" runat="server" class="dvMessage">
                <asp:Label ID="lblMessage" runat="server" Font-Size="Small" Width="100%"> </asp:Label>
            </div>
            <div id="dvHeaderControl" class="dvHeaderControl">
            </div>
        </div>
        <div id="dvContentMain" class="dvContentMain">
            <div id="dvControls" style="height: auto; width: 100%">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="width:750px;">
                    <div id="groupBox">
                        <div id="groupHeader" class="groupHeader">
                            <div style="width: 100%; height: 20px;">
                                <table>
                                    <tr>
                                        <td>
                                            <div id="dvIconEditMode" class="iconView" runat="server">
                                            </div>
                                        </td>
                                        <td>
                                            <span>Instrument Receive</span>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div id="groupContent" class="groupContent" style="width: 100%; height: 300px; overflow: auto;">
                            <div id="groupContenInner" style="width: 100%; height: auto; text-align: left;">
                               <div id="groupContentHeader" style="Width:100%;height:auto;">
                                <table style="text-align: left;" border="0" cellspacing="4" cellpadding="2">
                                    <tr>
                                        <td style="width: 10px;">
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="hdnInstrumentID" runat="server" Value="0" />
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="hdnCompanyID" runat="server" Value="0" />
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td style="" align="right">
                                            <asp:Label ID="Label4" runat="server" Text="Mode:" Visible="false"></asp:Label>
                                        </td>
                                        <td style="" align="left">
                                            <asp:DropDownList ID="ddlInstrumentMode" runat="server" CssClass="dropDownList" Width="130" Visible="false">
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
                                            <asp:Label ID="Label9" runat="server" Text="Type:"></asp:Label>
                                        </td>
                                        <td style="" align="left">
                                            <asp:DropDownList ID="ddlInstrumentType" runat="server" CssClass="dropDownList" Width="130">
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
                                            <asp:Label ID="Label1" runat="server" Text="Instrument No"></asp:Label>
                                        </td>
                                        <td style="" align="left">
                                            <asp:TextBox ID="txtInstrumentNo" runat="server" CssClass="textBox fldRequired enableIsDirty"
                                                Width="200px"></asp:TextBox>
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
                                            <asp:Label ID="Label7" runat="server" Text="Instrument Date:"></asp:Label>
                                        </td>
                                        <td style="" align="left">
                                            <asp:TextBox ID="txtInstrumentDate" runat="server" Width="80px" CssClass="textBox textDate dateParse fldRequired enableIsDirty"></asp:TextBox>
                                        </td>
                                        <td style="" align="right">
                                        </td>
                                        <td style="" align="left">
                                            &nbsp;
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td style="" align="right">
                                            <asp:Label ID="Label3" runat="server" Text="Favouring Name:"></asp:Label>
                                        </td>
                                        <td style="" align="left">
                                            <asp:TextBox ID="txtIssueName" runat="server" Width="200px" CssClass="textBox fldRequired enableIsDirty"></asp:TextBox>
                                        </td>
                                        <td style="" align="right">
                                        </td>
                                        <td style="" align="left">
                                            &nbsp;
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td style="" align="right">
                                            <asp:Label ID="Label5" runat="server" Text="Bank Name"></asp:Label>
                                        </td>
                                        <td style="" align="left">
                                            <asp:TextBox ID="txtBankName" runat="server" Width="200px" CssClass="textBox fldRequired enableIsDirty"></asp:TextBox>
                                        </td>
                                        <td style="" align="right">
                                        </td>
                                        <td style="" align="left">
                                            &nbsp;
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td style="" align="right">
                                            <asp:Label ID="Label6" runat="server" Text="Branch Name"></asp:Label>
                                        </td>
                                        <td style="" align="left">
                                            <asp:TextBox ID="txtBranchName" runat="server" Width="200px" CssClass="textBox fldRequired enableIsDirty"></asp:TextBox>
                                        </td>
                                        <td style="" align="right">
                                        </td>
                                        <td style="" align="left">
                                            &nbsp;
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td style="" align="right">
                                            <asp:Label ID="Label8" runat="server" Text="Instrument Amount"></asp:Label>
                                        </td>
                                        <td style="" align="left">
                                            <asp:TextBox ID="txtInstrumentAmt" runat="server" Width="120px" CssClass="textBox textNumberOnly textDecimalFormat fldRequired enableIsDirty"></asp:TextBox>
                                        </td>
                                        <td style="" align="right">
                                        </td>
                                        <td style="" align="left">
                                            &nbsp;
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td style="" align="right">
                                            <asp:Label ID="Label2" runat="server" Text="Status:"></asp:Label>
                                        </td>
                                        <td style="" align="left">
                                            <table cellspacing="0" cellpadding="0"> 
                                              <tr>
                                                <td>
                                                <asp:DropDownList ID="ddlInstrumentStatus" runat="server" CssClass="dropDownList"
                                                Width="130">
                                               </asp:DropDownList>
                                                </td>
                                                <td style="width:5px;">
                                                
                                                </td>

                                                <td align="right">
                                                   <asp:Label ID="Label12" runat="server" Text="Date:"></asp:Label>
                                                </td>
                                                <td style="width:2px;">
                                                
                                                </td>

                                                <td>
                                                   <asp:TextBox ID="txtInstrumentStatusDate" runat="server" Width="80px" CssClass="textBox textDate dateParse enableIsDirty"></asp:TextBox>
                                                </td>
                                              </tr>
                                            
                                            </table>
                                            
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
                                            &nbsp;
                                        </td>
                                        <td style="" align="left">
                                            &nbsp;
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

                               <div id="groupContentDetails" style="Width:100%;height:auto;">
                                   <div id="dvGridContainer" style="width:100%; height: auto; text-align:left;">
        <div id="dvGridHeader" style="width:100%;height:25px; font-size: smaller;" class="subHeader">
            <table style="height: 100%; color: #FFFFFF; font-weight: bold; text-align: center;" class="defFont" 
                cellspacing="1" cellpadding="1" rules="all" >
            <tr>
                <td width="52px" align="left">
                  
                </td>
                <td width="102px" align="left">JournalNo</td>
                <td width="82px" align="left">Date</td>
                <td width="102px" align="left">Type</td>
                <td width="202px" align="left">Ledger/Narration</td>
                <td width="102px" align="right">Amount</td>
            </tr>
            </table>
        </div> 
            <div id="dvGrid" style="width:auto;height:auto; min-height: 100px; overflow:auto;">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="None" 
            Font-Names="Arial" Font-Size="9pt" 
            DataKeyNames="JournalDetInsID,JournalID" onrowdatabound="GridView1_RowDataBound" 
            onrowdeleting="GridView1_RowDeleting" EmptyDataText="There is no record" PageSize="25" 
            onpageindexchanging="GridView1_PageIndexChanging" 
            onselectedindexchanged="GridView1_SelectedIndexChanged" 
            ShowHeader="False" RowStyle-VerticalAlign="Top">
            <PagerSettings Mode="NumericFirstLast" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
                <asp:HyperLinkField HeaderText="" Text="" >
                <ControlStyle CssClass="buttonViewGrid" Height="20px" Width="40px" />
                <ItemStyle Width="50px" />
                </asp:HyperLinkField>
                <asp:BoundField DataField="JournalNo" HeaderText="No" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Top" />
                <asp:BoundField DataField="JournalDate" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-Width="80"  ItemStyle-VerticalAlign="Top" />
                <asp:BoundField DataField="JournalTypeName" HeaderText="Bank" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Top" />
                <asp:BoundField DataField="JournalDetDescDisplay" HeaderText="Type" ItemStyle-Width="200px" />  
                <asp:BoundField DataField="InsTranAmt" HeaderText="Amount" ItemStyle-Width="100px" DataFormatString="{0:#0.00}" ItemStyle-HorizontalAlign="Right" /> 
            </Columns>
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
      </div>

        <div id="dvGridFooter" style="width:100%;height:25px; font-size: smaller;" class="subFooter">
            <table style="height: 100%; width:100%; font-weight: bold;"
                cellspacing="2" cellpadding="1" rules="all" >
            <tr>
                <td align="left" style="width:40%">
                <table>
                   <tr>
                     <td style="width:2px;">
                     
                     </td>
                     <td>
                     <asp:Label ID="lblTotal" runat="server" Text="Rows: 0 of 0"></asp:Label>
                     <asp:HiddenField ID="hdnRowCount" runat="server" Value="0" />
                     </td>
                  </tr>
                </table>
                 
                 
                
                </td>
                <td align="right" style="width:60%" >
                <div id="dvGridPager" class="dvGridPager">
                 <table>
                    <tr>
                      <td>
                          <asp:Button ID="btnGridPageGoTo" runat="server" Text="Go" 
                              onclick="btnGridPageGoTo_Click" />
                      </td>
                      <td>
                        <asp:Label ID="Label10" runat="server" Text="Page Size:"></asp:Label>
                      </td>
                       <td>
                           <asp:DropDownList ID="ddlGridPageSize" runat="server" CssClass="dropDownList" 
                               Width="50" Height="18" AutoPostBack="True" >
                               <asp:ListItem Value="10">10</asp:ListItem>
                               <asp:ListItem Value="20">20</asp:ListItem>
                               <asp:ListItem Value="30">30</asp:ListItem>
                               <asp:ListItem Value="50" Selected="True">50</asp:ListItem>
                               <asp:ListItem Value="100">100</asp:ListItem>
                               <asp:ListItem Value="200">200</asp:ListItem>
                               <asp:ListItem Value="0" >all</asp:ListItem>
                           </asp:DropDownList>
                       </td>


                      <td>
                          <asp:Label ID="Label11" runat="server" Text="Page:"></asp:Label>
                      </td>
                      <td>
                          <asp:TextBox ID="txtGridPageNo" runat="server"  CssClass="textBox" Width="30" 
                              Height="14" style="text-align:center;">0</asp:TextBox>
                      </td>
                      <td>
                        <asp:Label ID="lblGridPageInfo" runat="server" Text=" of 0"></asp:Label>
                      </td>
                      <td>
                         <asp:Button ID="btnGridPageFirst" runat="server" Text="" CssClass="btnGridPageFirst" ToolTip="First" />
                      </td>
                      <td>
                             <asp:Button ID="btnGridPagePrev" runat="server" Text="" CssClass="btnGridPagePrev" ToolTip="Previous" />
                      </td>
                      <td>
                        <asp:Button ID="btnGridPageNext" runat="server" Text="" CssClass="btnGridPageNext" ToolTip="Next" />
                      </td>
                      <td> 
                          <asp:Button ID="btnGridPageLast" runat="server" Text=""  CssClass="btnGridPageLast"  ToolTip="Last"/>
                      </td>
                      <td style="width:2px;">
                      </td>
                    </tr>
                 </table>
                </div>
                   
                </td>
                
            </tr>
            </table>
        </div> 
    </div>

                               </div>

                            </div>
                        </div>
                        <div id="groupFooter" class="groupFooter">
                            <div style="width: 100%; height: 12px;">
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
                        <asp:Button ID="btnAddNew" runat="server" Text="New" CssClass="buttonNew" OnClick="btnAddNew_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="buttonCancel checkIsDirty"
                            OnClick="btnCancel_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonSave" OnClick="btnSave_Click" />
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="buttonEdit" OnClick="btnEdit_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="buttonDelete" OnClick="btnDelete_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="buttonRefresh checkIsDirty"
                            OnClick="btnRefresh_Click" />
                    </td>
                    <td>
                    </td>
                    <td>
                        <input id="btnClose" type="button" runat="server" class="buttonClose" value="Close"
                            onclick="if (ContentForm) { ContentForm.CloseForm(); }" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
