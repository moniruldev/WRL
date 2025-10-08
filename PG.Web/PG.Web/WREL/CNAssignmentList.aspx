<%@ Page Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="CNAssignmentList.aspx.cs" Inherits="PG.Web.WREL.CNAssignmentList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">



    <script src="../javascript/jquery.ui.combogrid.js" type="text/javascript"></script>
    <script src="../javascript/jquery.attributeobserver.js" type="text/javascript"></script>
    <link href="../css/jquery.ui.combogrid.css" rel="stylesheet" type="text/css" />


    <script language="javascript" type="text/javascript">
        // <!CDATA[

        <%--var ItemListServiceLink = '<%=this.ItemListServiceLink%>';--%>

        var btnGridPageGoTo = '<%=btnGridPageGoTo.ClientID %>';
        var txtGridPageNo = '<%=txtGridPageNo.ClientID %>';
     

        var DeliveryManlistServiceLink = '<%=this.DeliveryManlistServiceLink%>';


        var txtDeliveryMan = '<%=txtDeliveryMan.ClientID%>';
        var hdnDeliveryManID = '<%=hdnDeliveryManID.ClientID%>';

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
            $("#dvGridContainer").height(contInnerHeight - 10);
            var gridHeight = $("#dvGridContainer").height();
            var gridHeaderHeight = $("#dvGridHeader").height();
            var gridFooterHeight = $("#dvGridFooter").height();
            $("#dvGrid").height(gridHeight - gridHeaderHeight - gridFooterHeight - 2);
        }


        $(document).ready(function () {
            $('#' + txtGridPageNo).keydown(function (e) {
                if (e.keyCode == 13) {
                    e.preventDefault();
                    $('#' + btnGridPageGoTo).click();
                }
            });

         


        });
 


        function tbopen(key, userid) {
            if (!key) {
                key = '';
            }

            var url = IForm.RootPath + "WREL/CNAssignmentV2.aspx?id=" + key;

            if (IForm.PageMode == Enums.PageMode.InTab) {

                var tdata = new xtabdata();
                tdata.linktype = Enums.LinkType.Direct;
                tdata.id = 0;
                tdata.name = "CN Assignment";
                tdata.label = "CN Assignment";
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
                window.location = url;
            }
        }

        function fromParent(val1) {
            alert('this is called from parent: ' + val1);
        }


        $(document).ready(function () {


            if ($('#' + txtDeliveryMan).is(':visible')) {

                bindDeliveryManList();

            }
        });    
        function bindDeliveryManList() {
            var cgColumns = [
                             { 'columnName': 'delmanname', 'width': '100', 'align': 'left', 'highlight': 4, 'label': 'Name' }
                            , { 'columnName': 'mobile', 'width': '200', 'align': 'left', 'highlight': 4, 'label': 'Mobile' }

            ];
            var serviceURL = DeliveryManlistServiceLink + "?isterm=1&includeempty=0&hasitem=1&iscodename=1&codecomptype=" + Enums.DataCompareType.StartsWith;

            serviceURL += "&ispaging=0";
            var groupIDElem = $('#' + txtDeliveryMan);

            $('#' + txtDeliveryMan).click(function (e) {
                $(groupIDElem).combogrid("dropdownClick");
            });

            $(groupIDElem).combogrid({
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
                width: 350,
                url: serviceURL,
                search: function (event, ui) {

                    var newServiceURL = serviceURL;
                    $(this).combogrid("option", "url", newServiceURL);


                },
                select: function (event, ui) {
                    if (!ui.item) {
                        event.preventDefault();
                        return false;
                    }

                    if (ui.item.dealerid == '') {
                        event.preventDefault();
                        return false;
                    }
                    else {
                        $('#' + hdnDeliveryManID).val(ui.item.delmanid);
                        $('#' + txtDeliveryMan).val(ui.item.delmanname);
                    }
                    return false;
                },

                lc: ''
            });


            $(groupIDElem).blur(function () {
                var self = this;

                var groupID = $(groupIDElem).val();
                if (groupID == '') {
                    $('#' + txtDeliveryMan).val('');
                    $('#' + hdnDeliveryManID).val('0');
                }
            });
        }

        function showLargeImage(img) {
            var src = img.src; // get small image src
            document.getElementById("imgLarge").src = src; // set modal image
            var modal = new bootstrap.Modal(document.getElementById('imgModal'));
            modal.show();
            return false; // prevent postback
        }
     
    </script>

    <style type="text/css">
      
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
    <div class="container-fluid">
      <div class="card">
      <div class="card-header p-0">
       <div class="d-flex align-items-center justify-content-between p-1">
         <h5 class="card-title">CN Delivery</h5>
           <asp:LinkButton runat="server" ID="btnNewAdd" CssClass="btn btn-primary p-1"><i class="fas fa-plus"></i> New Entry</asp:LinkButton>
       </div>
       </div>

        <div class="card-body">
          <div class="row mb-0">

               


                <div class="col-md-4">
                  <div class="form-group row mb-0">
                    <label for="name" class="col-sm-4 col-form-label-sm">Delivery Man :</label>
                    <div class="col-sm-8">
                       
                       <asp:TextBox ID="txtDeliveryMan" runat="server" CssClass="form-control form-control-sm" ></asp:TextBox>
                        <asp:HiddenField runat="server" ID="hdnDeliveryManID" Value="0" /> 

                    </div>
                  </div>
                </div>

           </div>


            <div class="row-mb-0">
              <div class="card-footer m-2 p-1">
              <asp:LinkButton runat="server" ID="btnLoadData" OnClick="btnLoadData_Click"  CssClass="btn btn-primary" Text="<i class='fa fa-list'></i> Show Data"></asp:LinkButton>
             </div>
            </div>

            <div class="row">
             <div class="col-md-12">
                 <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
    CssClass="table table-sm table-striped table-bordered table-responsive-sm"
    DataKeyNames="CN_ASSIGN_ID" AllowPaging="true" PageSize="10"
    OnRowCommand="GridView1_RowCommand" EmptyDataText="There is no record">

    <PagerSettings Mode="NumericFirstLast" />
    <HeaderStyle CssClass="table-info" Font-Size="Smaller" />

    <Columns>

        <asp:TemplateField HeaderText="SL">
            <ItemTemplate>
                <asp:Label ID="lblSL" runat="server"></asp:Label>
            </ItemTemplate>
            <HeaderStyle Width="40px" />
            <ItemStyle Width="40px" CssClass="text-center" />
        </asp:TemplateField>

        <asp:TemplateField HeaderText="CN Number">
            <ItemTemplate>
                <asp:TextBox ID="txtCNName" runat="server" 
                    CssClass="form-control form-control-sm" Style="width: 140px;" 
                    Text='<%# Bind("CN_NUMBER") %>'></asp:TextBox>
                <asp:HiddenField ID="hdnCNID" runat="server" Value='<%# Bind("CN_ID") %>' />
                <asp:HiddenField ID="hdnIsOTP_Service" runat="server" Value='<%# Bind("IS_OTP_SERVICE") %>' />
            </ItemTemplate>
            <HeaderStyle Width="150px" />
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Consignee">
            <ItemTemplate>
                <asp:TextBox ID="txtConsignee" runat="server" CssClass="form-control form-control-sm"
                    Style="width: 130px;" Text='<%# Bind("CONSIGNEE_NAME") %>'></asp:TextBox>
            </ItemTemplate>
            <HeaderStyle Width="140px" />
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Mobile">
            <ItemTemplate>
                <asp:TextBox ID="txtConsigneeMobil" runat="server" CssClass="form-control form-control-sm"
                    Style="width: 90px;" Text='<%# Bind("CONSIGNEE_MOBILE_NO") %>'></asp:TextBox>
            </ItemTemplate>
            <HeaderStyle Width="90px" />
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Status">
            <ItemTemplate>
                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                     
                    <asp:ListItem Selected="True" Text="Delivered" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Return" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </ItemTemplate>
            <HeaderStyle Width="100px" />
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Return Cause">
            <ItemTemplate>
                <asp:DropDownList ID="ddlRetrunCause" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
            </ItemTemplate>
            <HeaderStyle Width="100px" />
        </asp:TemplateField>

        <asp:TemplateField HeaderText="OTP">
            <ItemTemplate>
                <asp:LinkButton ID="btnotp" runat="server" CommandName="OTP"
                    CommandArgument='<%# Eval("CONSIGNEE_MOBILE_NO") %>'
                    CssClass="btn btn-sm btn-primary" Text="Send" />
                <asp:HiddenField ID="hdnOTPCode" runat="server" Value='<%# Bind("OTP_CODE") %>' />
            </ItemTemplate>
            <HeaderStyle Width="60px" CssClass="text-center" />
            <ItemStyle CssClass="text-center" />
        </asp:TemplateField>

        <asp:TemplateField HeaderText="OTP_No">
            <ItemTemplate>
                <asp:TextBox ID="txtgOTP" runat="server" CssClass="form-control form-control-sm text-center"
                    Style="width: 60px;" Text='<%# Bind("CUSTOMER_OTP") %>'></asp:TextBox>
            </ItemTemplate>
            <HeaderStyle Width="60px" />
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Upload Image">
            <ItemTemplate>
                <div class="d-flex align-items-center">
            <table>
                <tr>
                    <td class="p-0 pe-1">
                        <asp:FileUpload ID="POD_Upload" runat="server" CssClass="form-control form-control-sm" />
                    </td>
                    <td class="p-0 pe-1">
                        <asp:Button ID="btnUpload" runat="server" 
                            Text="Upload" CssClass="btn btn-sm btn-primary"
                            CommandName="UploadImage" CommandArgument='<%# Eval("CN_ID") %>' Width="55px" />
                    </td>
                    <td class="p-0">
                      
                        <asp:Image ID="imgPhoto" runat="server" Width="60px" Height="37px"
    CssClass="img-thumbnail"
    ImageUrl='<%# string.IsNullOrEmpty(Convert.ToString(Eval("POD"))) ? "~/images/no-image.png" : Convert.ToString(Eval("POD")) %>'
    onclick="return showLargeImage(this)" Style="cursor:pointer;" />
                    </td>
                </tr>
            </table>
        </div>
            </ItemTemplate>
           <%-- <HeaderStyle Width="500px" />
            <ItemStyle Width="500" />--%>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
                <asp:LinkButton ID="lnkView" runat="server" CommandName="submit"
                    CommandArgument='<%# Eval("CN_ID") %>'
                    CssClass="btn btn-sm btn-success" Text="Submit" Width="65px" />
            </ItemTemplate>
            <HeaderStyle Width="70px" />
            <ItemStyle CssClass="text-center" />
        </asp:TemplateField>

    </Columns>
</asp:GridView>
                 <%--  <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" ShowHeader="true" CssClass="table table-sm table-striped table-bordered table-responsive-sm"  
                DataKeyNames="CN_ASSIGN_ID" EnableModelValidation="True" ClientIDMode="AutoID" OnRowDataBound="GridView1_RowDataBound" AllowPaging="true" EmptyDataText="There is no record" PageSize="10" 
                 OnPageIndexChanging="GridView1_PageIndexChanging" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowCommand="GridView1_RowCommand">
                  <PagerSettings Mode="NumericFirstLast" />
                <HeaderStyle CssClass="table-info" Font-Size="Smaller" />                                      
              <Columns>
                 
                  <asp:TemplateField HeaderText="SL">
    <ItemTemplate>
        <asp:Label ID="lblSL" runat="server"></asp:Label>
    </ItemTemplate>
</asp:TemplateField>
              
                    <asp:TemplateField HeaderText="CN Number">
    <ItemTemplate>
        <div class="d-flex align-items-center">
            <table>
                <tr>
                    <td class="p-0">
                        <asp:TextBox ID="txtCNName" runat="server" CssClass="form-control form-control-sm" Style="width: 140px;" Text='<%# Bind("CN_NUMBER") %>'></asp:TextBox>

                        <asp:HiddenField ID="hdnCNID" runat="server" Value='<%# Bind("CN_ID") %>' />

                         <asp:HiddenField ID="hdnIsOTP_Service" runat="server" Value='<%# Bind("IS_OTP_SERVICE") %>' />
                        
                    </td>
                </tr>
            </table>
        </div>
    </ItemTemplate>
</asp:TemplateField>
                  <asp:TemplateField HeaderText="Assign Date">
    <ItemTemplate>
        <div class="d-flex align-items-center">
            <table>
                <tr>
                    <td class="p-0">
                       
                        <asp:TextBox ID="txtAssignDate" runat="server" CssClass="form-control form-control-sm" Style="width: 80px;" Text='<%# Eval("ASSIGN_DATE", "{0:dd-MMM-yy}") %>' ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
    </ItemTemplate>
</asp:TemplateField>

  <asp:TemplateField HeaderText="Consignee">
    <ItemTemplate>
        <div class="d-flex align-items-center">
            <table>
                <tr>
                    <td class="p-0">
                        <asp:TextBox ID="txtConsignee" runat="server" CssClass="form-control form-control-sm" Style="width: 120px;" Text='<%# Bind("CONSIGNEE_NAME") %>'></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
    </ItemTemplate>
</asp:TemplateField>
                 
  <asp:TemplateField HeaderText="Consignee Mobile">
    <ItemTemplate>
        <div class="d-flex align-items-center">
            <table>
                <tr>
                    <td class="p-0">
                        <asp:TextBox ID="txtConsigneeMobil" runat="server" CssClass="form-control form-control-sm" Style="width: 100px;" Text='<%# Bind("CONSIGNEE_MOBILE_NO") %>'></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
    </ItemTemplate>
</asp:TemplateField>
                  
    <asp:TemplateField HeaderText="Status">
    <ItemTemplate>
        <div class="d-flex align-items-center">
            <table>
                <tr>
                    <td class="p-0">
                       <asp:DropDownList ID="ddlgStatus" runat="server" CssClass="dropDownList">
                           <asp:ListItem Selected="True" Text="Delivered" Value="1"></asp:ListItem>
                            <asp:ListItem  Text="Return" Value="2"></asp:ListItem>
                       </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
    </ItemTemplate>
</asp:TemplateField>
                   <asp:TemplateField HeaderText="Return Cause">
    <ItemTemplate>
        <div class="d-flex align-items-center">
            <table>
                <tr>
                    <td class="p-0">
                       <asp:DropDownList ID="ddlRetrunCause" runat="server" CssClass="dropDownList">
                          
                       </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
    </ItemTemplate>
</asp:TemplateField>

                   <asp:TemplateField HeaderText="OTP">
                       <ItemTemplate>
                            <asp:LinkButton ID="btnotp" runat="server"
                                CommandName="OTP"
                                CommandArgument='<%# Eval("CONSIGNEE_MOBILE_NO") %>'
                                CssClass="btn btn-sm btn-primary"
                                Text="Send" />
                           <asp:HiddenField ID="hdnOTPCode" runat="server" Value='<%# Bind("OTP_CODE") %>' />

                        </ItemTemplate>
                        <ItemStyle Width="60px" />
                 </asp:TemplateField>

   <asp:TemplateField HeaderText="Otp Code">
    <ItemTemplate>
        <div class="d-flex align-items-center">
            <table>
                <tr>
                    <td class="p-0">
                        <asp:TextBox ID="txtgOTP" runat="server" CssClass="form-control form-control-sm" Style="width: 60px;" Text='<%# Bind("CUSTOMER_OTP") %>'></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
    </ItemTemplate>
</asp:TemplateField>

   <asp:TemplateField HeaderText="Upload Image">
    <ItemTemplate>
        <div class="d-flex align-items-center">
            <table>
                <tr>
                    <td class="p-0 pe-1">
                        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control form-control-sm" />
                    </td>
                    <td class="p-0 pe-1">
                        <asp:Button ID="btnUpload" runat="server" 
                            Text="Upload" CssClass="btn btn-sm btn-primary"
                            CommandName="UploadImage" CommandArgument='<%# Eval("CN_ID") %>' />
                    </td>
                    <td class="p-0">
                      
                        <asp:Image ID="imgPhoto" runat="server" Width="60px" Height="35px"
    CssClass="img-thumbnail"
    ImageUrl='<%# string.IsNullOrEmpty(Convert.ToString(Eval("POD"))) ? "~/images/no-image.png" : Convert.ToString(Eval("POD")) %>'
    onclick="return showLargeImage(this)" Style="cursor:pointer;" />
                    </td>
                </tr>
            </table>
        </div>
    </ItemTemplate>
</asp:TemplateField>
        

                     <asp:TemplateField HeaderText="Action">
                       <ItemTemplate>
                            <asp:LinkButton ID="lnkView" runat="server"
                                CommandName="submit"
                                CommandArgument='<%# Eval("CN_ID") %>'
                                CssClass="btn btn-sm btn-primary"
                                Text="Submit" />
                        </ItemTemplate>
                        <ItemStyle Width="130px" />
                 </asp:TemplateField>
               
                 
                  
                  
                 

               </Columns>
                                                     
          </asp:GridView>--%>

   
             </div>
            </div>
            <!-- Bootstrap Modal for Large Image -->
<div class="modal fade" id="imgModal" tabindex="-1" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered modal-lg">
    <div class="modal-content">
      <div class="modal-body text-center">
        <img id="imgLarge" src="" class="img-fluid" />
      </div>
    </div>
  </div>
</div>
            <div class="row">
                <div class="col-md-12">
                     <div id="dvGridFooter" style="width: 100%; height: 25px; font-size: smaller;" class="subFooter">
                            <table style="height: 100%; width: 100%;"
                                cellspacing="2" cellpadding="1" rules="all">
                                <tr>
                                    <td align="left" style="width: 40%">
                                        <table>
                                            <tr>
                                                <td style="width: 2px;"></td>
                                                <td>
                                                    <asp:Label ID="lblTotal" CssClass="col-form-label-sm" runat="server" Text="Rows: 0 of 0"></asp:Label>
                                                    <asp:HiddenField ID="hdnRowCount" runat="server" Value="0" />
                                                </td>
                                            </tr>
                                        </table>



                                    </td>
                                    <td align="right" style="width: 60%">
                                        <div id="dvGridPager" class="dvGridPager">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnGridPageGoTo" runat="server" Text="Go"
                                                            OnClick="btnGridPageGoTo_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label2" CssClass="col-form-label-sm p-2" runat="server" Text="Page Size:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlGridPageSize" runat="server" CssClass="form-control form-control-sm m-1 p-0" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddlGridPageSize_SelectedIndexChanged">
                                                            <asp:ListItem Value="10" Selected="True">10</asp:ListItem>
                                                            <asp:ListItem Value="20">20</asp:ListItem>
                                                            <asp:ListItem Value="30">30</asp:ListItem>
                                                            <asp:ListItem Value="50">50</asp:ListItem>
                                                            <asp:ListItem Value="100">100</asp:ListItem>
                                                            <asp:ListItem Value="200">200</asp:ListItem>
                                                            <asp:ListItem Value="0" >all</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>


                                                    <td>
                                                        <asp:Label ID="Label1" runat="server" CssClass="col-form-label-sm p-2" Text="Page:"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtGridPageNo" runat="server" CssClass="form-control form-control-sm m-1 p-0" Width="50px">0</asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblGridPageInfo" runat="server" CssClass="col-form-label-sm p-2"  Text=" of 0"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnGridPageFirst" runat="server" Text="" CssClass="btnGridPageFirst"
                                                            OnClick="btnGridPageFirst_Click" ToolTip="First" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnGridPagePrev" runat="server" Text="" CssClass="btnGridPagePrev"
                                                            OnClick="btnGridPagePrev_Click" ToolTip="Previous" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnGridPageNext" runat="server" Text="" CssClass="btnGridPageNext"
                                                            OnClick="btnGridPageNext_Click" ToolTip="Next" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnGridPageLast" runat="server" Text="" CssClass="btnGridPageLast"
                                                            OnClick="btnGridPageLast_Click" ToolTip="Last" />
                                                    </td>
                                                    <td style="width: 2px;"></td>
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

    </div>
    </div>
</asp:Content>
