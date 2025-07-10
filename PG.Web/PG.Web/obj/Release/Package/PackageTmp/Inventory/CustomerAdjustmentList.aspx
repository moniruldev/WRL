<%@ Page Title="" Language="C#" ViewStateMode="Disabled" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="CustomerAdjustmentList.aspx.cs" Inherits="PG.Web.Inventory.CustomerAdjustmentList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />



    <script language="javascript" type="text/javascript">
        // <!CDATA[

        var isPageResize = true;
        ContentForm.CalendarImageURL = "../image/calendar.png";

    
        var hdnLocationID = '<%=hdnLocationID.ClientID%>';
     
        function ShowProgress() {
            $('#' + updateProgressID).show();
        }

        function UserConfirmation() {
            return confirm("Are you sure you want to Stock Issue and Print DC,GP ?");
        }


        function resizeContentInner(cntMain) {
            var contHeight = $("#dvContentMain").height();
            var contHead = $("#dvControlsHead").height();
            var contFooter = $("#dvControlsFooter").height();

            var contInnerHeight = contHeight - contHead - contFooter - 5;
            $("#dvControls").height(contInnerHeight);

            $("#dvControlsInner").height(contInnerHeight - 10);
            $("#dvGridContainer").height(contInnerHeight - 10);
            var gridHeight = $("#dvGridContainer").height();
            var gridHeaderHeight = $("#dvGridHeader").height();
            var gridFooterHeight = $("#dvGridFooter").height();
            $("#dvGrid").height(gridHeight - gridHeaderHeight - gridFooterHeight - 2);
        }


    

        function tbopen(key, userid) {
            key = key || '';
            // alert(key);
            var url = IForm.RootPath + "Inventory/CustomerMoneyAdjustment.aspx?id=" + key;
            //var url = IForm.RootPath + "Accounting/GeneralLedger/JournalFull.aspx?id=" + key;


            if (IForm.PageMode == Enums.PageMode.InTab) {

                var tdata = new xtabdata();
                tdata.linktype = Enums.LinkType.Direct;
                tdata.id = 0;
                tdata.name = "Customer Adjustment";
                //tdata.label = "User: " + userid;
                tdata.label = "Customer Adjustment";
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
       
      








        // ]]>
    </script>


    <style type="text/css">
        /*.FixedHeader { POSITION: relative; TOP: expression(this.offsetParent.scrollTop); BACKGROUND-COLOR: white } */

        .FixedHeader {
            position: relative;
            background-color: white;
        }

        #dvMessage {
            height: 20px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hdnSEGMIENT_ID" runat="server" Value="6" />
    <div id="dvPageContent" style="width: 100%; height: auto;">
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader">
                <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text=" Customer Adjustment List"></asp:Label>
            </div>
            <!--Message Div -->
            <div id="dvMsg" runat="server" class="dvMessage" style="">
                <asp:Label ID="lblMessage" runat="server" Font-Size="Small" Width="100%"> </asp:Label>
            </div>
            <div id="dvHeaderControl" class="dvHeaderControl">
            </div>
        </div>
        <div id="dvContentMain" class="dvContentMain">
            <div id="dvControlsHead" style="height: auto; width: 100%;">
                <table>

                    <tr>
                        <td></td>
                        <td align="right">
                            <asp:Label ID="lblLoccode" runat="server" Text="Loc Code :" Style="height: 19px; width: 43px;" Font-Bold="True" Visible="false"></asp:Label>
                        </td>
                        <td align="left" colspan="4">
                            <asp:TextBox ID="txtLocCode" runat="server" CssClass="textBox textAutoSelect" Style="text-align: left;" Width="100px" ViewStateMode="Enabled" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="txtLocDP" runat="server" CssClass="textBox textAutoSelect" Style="text-align: left;" Width="200px" ViewStateMode="Enabled" Visible="false"></asp:TextBox>&nbsp;
                        </td>






                    </tr>

                    <tr>
                        <td></td>
                        <td>
                            <asp:Label ID="lblFromDate" runat="server" Text="Adjust From Date:" Style="height: 19px; width: 43px;" Font-Bold="True" Visible="true"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="100px"></asp:TextBox>
                            <%-- <asp:RegularExpressionValidator runat="server" ControlToValidate="txtFromDate" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$" ErrorMessage="Invalid date format." ValidationGroup="Group1" ForeColor="Red" />--%>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblInvoiceToDate" runat="server" Text="To Date:" Style="height: 19px; width: 43px;" Font-Bold="True" Visible="true"></asp:Label>

                        </td>

                        <td>
                            <asp:TextBox ID="txtTodate" runat="server" CssClass="textBox textDate dateParse" Style="text-align: left;" Width="100px"></asp:TextBox>&nbsp;
                   
                 <asp:RegularExpressionValidator runat="server" ControlToValidate="txtTodate" ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$" ErrorMessage="Invalid date format." ValidationGroup="Group1" ForeColor="Red" />
                        </td>



                    </tr>


                    <tr class="rowParam">
                        <td></td>
                        <td align="right" class="auto-style1">
                            <asp:Label ID="lblisActive" runat="server" Text="IsAuthorized:" Font-Bold="True"></asp:Label>
                        </td>
                        <td class="auto-style1">
                            <asp:DropDownList ID="ddlIsAuthorized" runat="server" CssClass="dropDownList ">
                                <asp:ListItem Text="All" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Authorized" Value="A"></asp:ListItem>
                                <asp:ListItem Selected="True" Text="UnAuthorized" Value="U"></asp:ListItem>

                            </asp:DropDownList>

                        </td>
                        <td class="auto-style1"></td>
                        <td align="right" class="auto-style1"></td>

                    </tr>

                    <tr>
                        <td></td>
                        <td></td>
                        <td>
                            <asp:Button ID="btnFind" runat="server" Text="Show Data" CssClass="buttonRefresh checkIsDirty" OnClick="btnFind_Click" />
                        </td>
                        <td>
                            <input id="btnAddNew" type="button" runat="server" value="New Adjustment" class="buttonNew" style=" width: 120px;" />
                        </td>


                        <td>
                            <asp:HiddenField ID="hdnCustomerID" runat="server" Value="0" />
                            <asp:HiddenField ID="hdnLocationID" runat="server" />
                        </td>

                    </tr>



                </table>
            </div>
            <br />




            <div id="dvControls" style="width: 100%;">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="width: 1124px">
                    <dx:ASPxGridView ID="grdInvoiceMstList" runat="server" AutoGenerateColumns="False" Width="98%" ClientInstanceName="grdInvoiceMstList" >
                        <Columns>

                            <dx:GridViewDataTextColumn Caption="Action" UnboundType="String" Width="55px" VisibleIndex="1">
                                <DataItemTemplate>
                                    <dx:ASPxHyperLink ID="hyperLink" runat="server" OnInit="hyperLink_Init">
                                    </dx:ASPxHyperLink>
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Adjust NO" Name="lblAdj_NO" VisibleIndex="2" Width="150px" FieldName="ADJUSTMENT_NO">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataDateColumn Caption="Adjust Date" Name="lblADJ_DATE" VisibleIndex="3" Width="120px" FieldName="ADJ_DATE">
                                <PropertiesDateEdit DisplayFormatString="dd-MMM-yyyy"></PropertiesDateEdit>
                            </dx:GridViewDataDateColumn>
                             <dx:GridViewDataTextColumn Caption="Cust Code" Name="lblCust_Code" VisibleIndex="4" Width="100px" FieldName="CUST_CODE">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Cust Name" Name="lblCust_NAME" VisibleIndex="5" Width="120px" FieldName="CUST_NAME">
                            </dx:GridViewDataTextColumn>
                             <dx:GridViewDataTextColumn Caption="Adj Type" Name="lblADJUST_TYPE_DESC" VisibleIndex="6" Width="100px" FieldName="ADJUST_TYPE_DESC">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Adj Amount" Name="lblADJUSTADJ_AMT" VisibleIndex="7" Width="100px" FieldName="ADJ_AMT">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Adj Reason" Name="lblADJ_REASON" VisibleIndex="8" Width="150px" FieldName="ADJ_REASON">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataDateColumn Caption="Create Date" Name="lblCREATE_DATE" VisibleIndex="10" Width="120px" FieldName="CREATE_DATE">
                                <PropertiesDateEdit DisplayFormatString="dd-MMM-yyyy"></PropertiesDateEdit>
                            </dx:GridViewDataDateColumn>
                         
                            <dx:GridViewDataTextColumn Caption="Status" Name="lblAUTO_STATUS" VisibleIndex="9" Width="80px" FieldName="AUTHO_STATUS">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                        <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                        <SettingsSearchPanel Visible="True" />
                        <SettingsPager AlwaysShowPager="True" PageSize="20">
                        </SettingsPager>
                        <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="380" ShowFilterBar="Visible" ShowFilterRowMenu="True" />
                        <Styles>
                            <Header CssClass="headerRow_Prod" HorizontalAlign="Center" Font-Bold="True" ForeColor="White" Font-Size="9pt" BackColor="#80ADE5">
                            </Header>
                            <AlternatingRow BackColor="#FFFFCC">
                            </AlternatingRow>
                            <HeaderPanel BackColor="#669999">
                            </HeaderPanel>
                        </Styles>
                    </dx:ASPxGridView>
                       <dx:ASPxButton ID="btnXlsExport" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                OnClick="btnXlsExport_Click" />

                            <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grdInvoiceMstList"></dx:ASPxGridViewExporter>
                </div>
            </div>
        </div>
        <div id="dvContentFooter" class="dvContentFooter">
        </div>
    </div>
</asp:Content>

