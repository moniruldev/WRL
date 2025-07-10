<%@ Page Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Supplier.aspx.cs" Inherits="PG.Web.Inventory.Supplier" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        // <!CDATA[

        var isPageResize = true;

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

        function fromParent(val1) {
            alert('this is called from parent: ' + val1);
        }

        function Button1_onclick() {
            //document.getElementById("btnSave").click();
            ///__doPostBack("<%= this.btnSave.UniqueID %>", "");
            __doPostBack("btnSave", "");
        }

        // ]]>
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvPageContent" style="width:100%; height:100%;">
   
    <div id="dvContentHeader" class="dvContentHeader">  
        <div id="dvHeader" class="dvHeader" >
            <asp:Label ID="lblHeader" CssClass="lblHeader" runat="server" Text="Supplier"></asp:Label>
        </div>
        <div id="dvMessage" class="dvMessage" >
            <asp:Label ID="lblMessage" runat="server" Text="" Font-Size="Small" Width="100%"></asp:Label>
        </div>
   </div>

   <div id="dvContentMain" class="dvContentMain"> 
    <div id = "dvControls" style="height:auto; width:100%">
         <div id="dvControlsInner" class="groupBoxContainer boxShadow">    
           <div id="groupBox">
             <div id="groupHeader" class="groupHeader">
                      <div style="width:100%;height:20px;">
                         <table>
                            <tr>
                             <td>
                                <div id="dvIconEditMode" class="iconView" runat="server" ></div>
                             </td>
                             <td>
                                <span>Supplier</span> 
                             </td>
                            </tr>
                         </table>
                         
                      </div>
                      
                  </div>
             
             <div id="groupContent" class="groupContent" style="width:100%;height:500px; overflow:auto;">
               <div id="groupContenInner" style="width:100%;height:auto; text-align:left;">
                <table cellpadding="2" cellspacing="4">
               <tr>
                 <td style="width:20px;">
                 </td>
                 <td>
                 
                 </td>
                 <td>
                 
                 </td>
               </tr>
            
             <tr>
                 <td>
                 </td>
                 <td>
                 <asp:Label id="Label8" runat="server" Text="Supplier Code" ></asp:Label><span style="color: red">*</span>
                 </td>
                 <td>
                    <asp:TextBox id="txtsupCode" runat="server"  CssClass="textBox" Width="300px"  Height="25px"></asp:TextBox> 
                    
                 </td>
                 <td>
                 </td>
               </tr>
              <tr>
                 <td>
                 </td>
                 <td>
                  <asp:Label id="lblSupplierName" runat="server" Text="Supplier Name" ></asp:Label><span style="color: red">*</span>
                 </td>
                 <td>
                  <asp:TextBox id="txtSupplierName" runat="server" CssClass="textBox" Width="300px" Height="25px"></asp:TextBox>
                 </td>
               </tr>

                <tr>
                 <td>
                 </td>
                 <td>
                   <asp:Label  id="lblSupplierAddress" runat="server" Text="Supplier Address" ></asp:Label><span style="color: red">*</span>
                 </td>
                 <td>
                    <asp:TextBox id="txtSupplierAddress" TextMode="multiline" Columns="50" Rows="5" runat="server" Width="300px" Height="50px" CssClass="textBox"></asp:TextBox>
                 </td>
               </tr>

                <tr>
                 <td>
                 </td>
                 <td>
                  <asp:Label ID="lblPhone" runat="server" Text="Phone" ></asp:Label>
                 </td>
                 <td>
                 <asp:TextBox id="txtPhone" runat="server" CssClass="textBox" Width="300px" Height="25px"></asp:TextBox>
                 </td>
               </tr>
                <tr>
                 <td>
                 </td>
                 <td>
                  <asp:Label id="Label1" runat="server" Text="Another Phone(if any)" ></asp:Label>
                 </td>
                 <td>
                  <asp:TextBox id="txtPhoneAnother" runat="server" CssClass="textBox" Width="300px" Height="25px"></asp:TextBox> 
                 </td>
               </tr>
                <tr>
                 <td>
                 </td>
                 <td>
                    <asp:Label id="lblEmail" runat="server" Text="Email" ></asp:Label> 
                 </td>
                 <td>
                    <asp:TextBox id="txtEmail" runat="server" CssClass="textBox" Width="300px" Height="25px"></asp:TextBox> 
                     
                 </td>
                 <td>
                 
                 </td>
               </tr>
              
                    

               <tr>
              <td>
              </td>
              <td>
               <asp:Label ID="lblCountry" runat="server" Text="Country"></asp:Label>
             </td>
            <td>
              <asp:DropDownList ID="ddlCountry" runat="server" Width="305" Height="25px" CssClass="dropDownList enableIsDirty"> </asp:DropDownList>
            </td>
         </tr>
           <tr>
                 <td>
                 </td>
                 <td>
                  <asp:Label ID="lblCompanyID" runat="server" Text="Company Name " ></asp:Label>
                 </td>
                 <td>
                     <asp:DropDownList ID="ddlCompanyID" runat="server" CssClass="dropDownList enableIsDirty"  Width="305" Height="25px" ></asp:DropDownList>
                 <%--<asp:TextBox id="txtCompanyName" runat="server" CssClass="textBox" Width="300px" Height="25px"></asp:TextBox>--%>
                 </td>
            </tr>
                    <tr>
                 <td>
                 </td>
                 <td>
                    <asp:Label id="Label7" runat="server" Text="VAT Reg. No" ></asp:Label> 
                 </td>
                 <td>
                    <asp:TextBox id="txtVatRegNo" runat="server" CssClass="textBox" Width="300px" Height="25px"></asp:TextBox> 
                     
                 </td>
                 <td>
                 
                 </td>
               </tr>
                     <tr>
                 <td>
                 </td>
                 <td>
                    <asp:Label id="Label4" runat="server" Text="Advise Bank" ></asp:Label> 
                 </td>
                 <td>
                    <asp:TextBox id="txtBank" runat="server" CssClass="textBox" Width="300px" Height="25px"></asp:TextBox> 
                     
                 </td>
                 <td>
                 
                 </td>
               </tr>
                    <tr>
                 <td>
                 </td>
                 <td>
                    <asp:Label id="Label2" runat="server" Text="Contact Person" ></asp:Label> 
                 </td>
                 <td>
                    <asp:TextBox id="txtContactPerson" runat="server" CssClass="textBox" Width="300px" Height="25px"></asp:TextBox> 
                     
                 </td>
                 <td>
                 
                 </td>
               </tr>
                    <tr>
                 <td>
                 </td>
                 <td>
                    <asp:Label id="Label3" runat="server" Text="Contact Person Phone" ></asp:Label> 
                 </td>
                 <td>
                    <asp:TextBox id="txtContactPersonPhone" runat="server" CssClass="textBox" Width="300px" Height="25px"></asp:TextBox> 
                     
                 </td>
                 <td>
                 
                 </td>
               </tr>
                    <tr>
                 <td>
                 </td>
                 <td>
                    <asp:Label id="Label9" runat="server" Text="Cont. Per. Another Phone" ></asp:Label> 
                 </td>
                 <td>
                    <asp:TextBox id="txtContactPersonPhoneAnother" runat="server" CssClass="textBox" Width="300px" Height="25px"></asp:TextBox> 
                     
                 </td>
                 <td>
                 
                 </td>
               </tr>
                <tr>
                 <td>
                   </td>
                 <td style="" align="left">
                   <asp:Label id="lblSupplierType" runat="server" Text="Supplier Type" ></asp:Label><span style="color: red">*</span>
                 </td>
                 <td style="" align="left">
                     <asp:DropDownList ID="ddlSupplierType" runat="server" Width="305" Height="25px" CssClass="dropDownList enableIsDirty">
                         <asp:ListItem Text="Local" Value="L" Selected="True"></asp:ListItem>
                         <asp:ListItem Text="Foreign" Value="F"></asp:ListItem>
                         <asp:ListItem Text="Common" Value="C"></asp:ListItem>
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
                 <td style="" align="left">
                   <asp:Label id="lblManufacture" runat="server" Text="Manufacture?" ></asp:Label>
                 </td>
                 <td style="" align="left">
                     <asp:DropDownList ID="ddlManufacture" runat="server" Width="305" Height="25px" CssClass="dropDownList enableIsDirty">
                         <asp:ListItem Text="No" Value="N"></asp:ListItem>
                         <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
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
                 <td style="" align="left">
                   <asp:Label id="Label6" runat="server" Text="Enlisted" ></asp:Label>
                 </td>
                 <td style="" align="left">
                     <asp:DropDownList ID="ddlEnlisted" runat="server" Width="305" Height="25px" CssClass="dropDownList enableIsDirty">
                         <asp:ListItem Text="No" Value="N"></asp:ListItem>
                         <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                    </asp:DropDownList>
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
             </div>

             <div id="groupFooter" class="groupFooter">
                      <div style="width:100%;height:12px;">
                      
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
                 <asp:Button ID="btnAddNew" runat="server" Text="Reset" CssClass="buttonNew" width="90px" Height="26px"
                onclick="btnReset_Click" />
                </td>
                <td>
                 <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="buttonSave" 
                onclick="btnSave_Click" />
                </td>
                </tr> 
            </table>
        </div>
    </div> 
</asp:Content>
