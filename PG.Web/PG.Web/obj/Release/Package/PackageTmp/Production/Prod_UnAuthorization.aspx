<%@ Page Title="" Language="C#" ViewStateMode="Disabled" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="Prod_UnAuthorization.aspx.cs" Inherits="PG.Web.Report.INV.Prod_UnAuthorization" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        // <!CDATA[

        var isPageResize = true;
        ContentForm.CalendarImageURL = "../image/calendar.png";




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


        $(document).ready(function () {
           
        });


        function tbopen(key, userid) {
            key = key || '';
            var url = IForm.RootPath + "Production/INVNewLP_MRR_Authorize.aspx?id=" + key;

            if (IForm.PageMode == Enums.PageMode.InTab) {

                var tdata = new xtabdata();
                tdata.linktype = Enums.LinkType.Direct;
                tdata.id = 0;
                tdata.name = "Production UnAuthorization";

                tdata.label = "Production UnAuthorization";
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



        function SaveReplaceData() {
            var isselect = false;
            var cObjList = new Array();
            $(".cbIgr:checked").each(function () {
                if ($(this).is(':checked')) {
                    var id = $(this).parent().parent().find('#hdnReqId').val();
                    cObjList.push(id);
                    isselect = true;
                }
            });

            if (isselect) {
                var y = confirm('Are you sure?');
                if (y) {
                    $.ajax({
                        type: "POST",
                        url: "../Production/Prod_UnAuthorization.aspx/UnAuthorize_Production",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: false,
                        data: JSON.stringify({ reqIdList: cObjList }),
                        success: function (result) {
                            var returnObj = JSON.parse(result.d);
                            if (returnObj.Status == "Success") {
                                alert('Production unauthorized successfully.');
                                $("[id*=btnShow]").click();
                            } else if (returnObj.Status == "Failed") {
                                alert(returnObj.ErrorMessage);
                            }

                        },
                        error: function (result) {
                            alert(result.responseText);
                        }

                    });
                }
            } else {
                alert('Please select MRR first to authorize.');
            }


        }





        // ]]>

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="dvPageContent" style="width: 100%; height: auto;">
        <div id="dvContentHeader" class="dvContentHeader">
            <div id="dvHeader" class="dvHeader">
                <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Production Un_Authorization"></asp:Label>
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
                        <td align="right">
                            <asp:Label ID="Label1" runat="server" Text="Dept:" Font-Bold="true"></asp:Label>&nbsp;
                        </td>
                        <td>
                           <asp:DropDownList ID="ddlDEPT_ID" runat="server" CssClass="dropDownList required" Width="200px" ViewStateMode="Enabled" ></asp:DropDownList>
                        </td>
                        <td style="width: 4px;"></td>
                        <td align="right">
                          
                        </td>

                        <td>
                            
                        </td>
                        <td>
                        </td>
                    </tr>
                    
                    
                    
                    
                     <tr>
                        <td align="right">
                            <asp:Label ID="lblDateFrom" runat="server" Text="Date From:" Font-Bold="true"></asp:Label>&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="txtFromDate" runat="server" Width="80px" CssClass="textBox textDate dateParse"></asp:TextBox>
                        </td>
                        <td style="width: 4px;"></td>
                        <td align="right">
                            <asp:Label ID="lblToDate" runat="server" Text="Date To:" Font-Bold="true"></asp:Label>&nbsp;
                        </td>

                        <td>
                            <asp:TextBox ID="txtToDate" runat="server" Width="80px" CssClass="textBox textDate dateParse"></asp:TextBox>
                        </td>
                        <td>&nbsp;&nbsp;<asp:Button ID="btnShow" runat="server" CssClass="buttonSearch" Style="padding-left: 22px;"
                            Text="Show Data" OnClick="btnShow_Click" />
                        </td>
                    </tr>


                </table>
            </div>
            <div id="dvControls" style="width: 100%;">
                <div id="dvControlsInner" class="groupBoxContainer boxShadow" style="width: 1150px">
                    <div id="dvGridContainer" style="width: 100%; height: auto; text-align: left;">

                        <div id="dvGrid" style="width: 100%; height: 500px; overflow: auto;">
                            <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="ASPxGridView1" runat="server" Width="100%" AutoGenerateColumns="False" KeyFieldName="MRR_NO">
                                <Settings VerticalScrollableHeight="250" ShowFooter="True" ShowGroupFooter="VisibleAlways" />
                                <Columns>                                   
                                      <dx:GridViewDataTextColumn Caption="Select" FieldName="IS_SELECTED" VisibleIndex="1" Width="40px">
                                        <DataItemTemplate>
                                            <input type="hidden" id="hdnReqId" value="<%#Eval("PROD_ID")%>" />
                                            <input type="checkbox" name="cbIsSelected" class="cbIgr" />
                                        </DataItemTemplate>
                                    </dx:GridViewDataTextColumn>

                                     <dx:GridViewDataTextColumn Caption="Prod No" FieldName="PROD_NO" VisibleIndex="1" Width="150px" />                                  
                                   
                                    <dx:GridViewDataTextColumn Caption="Entry By" Name="lblEDIT_BY_ID" Width="120px" VisibleIndex="4" FieldName="ENTRY_BY">
                                    </dx:GridViewDataTextColumn>

                                    <dx:GridViewDataTextColumn Caption="Dept" Name="lblEDIT_Dept_ID" Width="120px" VisibleIndex="4" FieldName="DEPARTMENT_NAME">
                                    </dx:GridViewDataTextColumn>

                                    
                                    <dx:GridViewDataTextColumn Caption="Dept. ID" Name="lblDEPT_ID" VisibleIndex="5" Visible="false" FieldName="DEPT_ID">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Shift" Name="lblSHIFT_NAME" VisibleIndex="6" Width="100px" FieldName="SHIFT_NAME">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="Supervisor" Name="lblSUPERVISOR_NAME" VisibleIndex="7" Width="200px" FieldName="FULL_NAME">
                                    </dx:GridViewDataTextColumn>
                                   
                               
                                    <dx:GridViewDataDateColumn Caption="Pro. Date" FieldName="PRODUCTION_DATE" Width="100px"   Name="lblPRODUCTION_DATE" VisibleIndex="3">
                                         <PropertiesDateEdit DisplayFormatString="dd-MMM-yyyy" ></PropertiesDateEdit>
                                    </dx:GridViewDataDateColumn>
                                   
                                      <dx:GridViewDataTextColumn Caption="Batch" FieldName="PROD_BATCH_NO" Width="50px" Name="txtPROD_BATCH_NO" VisibleIndex="8">
                                      </dx:GridViewDataTextColumn>
                                </Columns>
                                <SettingsBehavior AllowFixedGroups="True" AutoExpandAllGroups="True" SortMode="Value" />
                                <SettingsPager NumericButtonCount="20">
                                    <PageSizeItemSettings Visible="true" Items="50,100" />
                                </SettingsPager>

                               
                                 <SettingsSearchPanel Visible="True" />
                                <SettingsDataSecurity AllowInsert="false" AllowEdit="false" AllowDelete="false" />
                                <Styles>
                                    <Header BackColor="#0033CC" ForeColor="White">
                                    </Header>
                                    <AlternatingRow BackColor="#FFFFCC">
                                    </AlternatingRow>
                                    <GroupFooter BackColor="#CCCCFF">
                                    </GroupFooter>
                                    <GroupPanel BackColor="#9999FF">
                                    </GroupPanel>
                                </Styles>
                            </dx:ASPxGridView>

                            <dx:ASPxButton ID="btnXlsExport" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                                OnClick="btnXlsExport_Click" />

                            <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="ASPxGridView1"></dx:ASPxGridViewExporter>
                        </div>

                    </div>
                </div>
            </div>
        </div>


        <div id="dvContentFooter" class="dvContentFooter">
            <table>
                <tr>
                    <td></td>
                    <td></td>
                    <td>
                        <input type="button" id="btnCloseIGR" value="UnAuthorize" class="buttonSave" onclick="SaveReplaceData()" style="width: 110px !important; background-repeat: no-repeat; text-align: right" />
                    </td>
                    <td>
                        <input id="btnClose" type="button" runat="server" class="buttonClose" value="Close" onclick="if (ContentForm) { ContentForm.CloseForm(); }" />
                    </td>

                    <td></td>
                    <td></td>

                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
        </div>


    </div>
</asp:Content>
