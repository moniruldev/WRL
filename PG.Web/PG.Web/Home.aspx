<%@ Page Language="C#" MasterPageFile="~/AppMaster.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="PG.Web.Home" Title="Home" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <!-- Moment.js -->
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.29.4/moment.min.js"></script>

<!-- Tempus Dominus Bootstrap 4 -->
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/tempusdominus-bootstrap-4@5.39.0/build/css/tempusdominus-bootstrap-4.min.css" />
<script type="text/javascript" src="https://cdn.jsdelivr.net/npm/tempusdominus-bootstrap-4@5.39.0/build/js/tempusdominus-bootstrap-4.min.js"></script>

    <!-- Daterangepicker.js -->
<script type="text/javascript"  src="https://cdn.jsdelivr.net/npm/daterangepicker/daterangepicker.min.js"></script>
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/daterangepicker/daterangepicker.css" />

  <script type="text/javascript"  src="https://cdn.jsdelivr.net/npm/chart.js"></script>
   
   <style type="text/css">
       
       .card-link-hover:hover {
    background-color: #66b3ff; 
    cursor: pointer;
  }
  .card-link-hover:hover h5,
  .card-link-hover:hover h1,
  .card-link-hover:hover i {
    color:white ; 
    text-decoration: none;
  }

    
        
         
   </style>

        <script type="text/javascript">
            window.onload = function () {
                var ctx = document.getElementById('chartIncomeExpense').getContext('2d');

                // Parse chart data from server literal
                var chartData = JSON.parse(document.getElementById('litChartDataJson').textContent);

                new Chart(ctx, {
                    type: 'bar',
                    data: {
                        labels: chartData.labels,
                        datasets: [
                            {
                                label: 'Income',
                                data: chartData.income,
                                backgroundColor: 'rgba(54, 162, 235, 0.7)'
                            },
                            {
                                label: 'Expense',
                                data: chartData.expense,
                                backgroundColor: 'rgba(255, 99, 132, 0.7)'
                            }
                        ]
                    },
                    options: {
                        responsive: true,
                        maintainAspectRatio: false,
                        plugins: {
                            legend: { position: 'top' },
                            title: { display: true, text: 'Income vs Expense' }
                        }
                    }
                });


                const pieData = JSON.parse(document.getElementById('pieChartData').textContent);

                // Create Chart.js pie chart
                const ctxpie = document.getElementById('chartPieRevenue').getContext('2d');
                const data = {
                    labels: ["Product A", "Product B", "Product C"],
                    datasets: [{
                        data: [300, 150, 100],
                        backgroundColor: ["#007bff", "#28a745", "#ffc107"],
                        hoverOffset: 30
                    }]
                };

                const config = {
                    type: 'pie',
                    data: data,
                    options: {
                        responsive: true,
                        maintainAspectRatio: false
                    }
                };

                new Chart(ctxpie, config);
            };
    </script>

    <script language="javascript" type="text/javascript">
// <!CDATA[

        var isPageResize = true;
     
        $(document).ready(function () {
            $('#datetimepicker12').datetimepicker({
                inline: true,
                sideBySide: true,
                format: 'L LT'
            });
        });

        $(function () {
            $('.date_range_picker').daterangepicker({
                autoUpdateInput: false,
                locale: {
                    format: 'YYYY-MM-DD',
                    cancelLabel: 'Clear'
                }
            });

            $('.date_range_picker').on('apply.daterangepicker', function (ev, picker) {
                $(this).val(picker.startDate.format('YYYY-MM-DD') + ' - ' + picker.endDate.format('YYYY-MM-DD'));
            });

            $('.date_range_picker').on('cancel.daterangepicker', function (ev, picker) {
                $(this).val('');
            });
        });


function tbopen(key)
{
     if(!key)
     {
       key = '';
     }
 
    
    var url = "/Admin/SetPassword.aspx?uid=" + key
    //if (pageInTab == 1)
    if (ZForm.PageMode == Enums.PageMode.InTab)
    {

       var tdata = new xtabdata();
       tdata.linktype = Enums.LinkType.Direct;
       tdata.id = 6320;
       tdata.name = "SetPassword";
       //tdata.label = "User: " + userid;
       tdata.label = "Set Password";
       tdata.type = 0;
       tdata.url = url;
       tdata.tabaction = Enums.TabAction.InTabReuse;
       tdata.selecttab = 1;
       tdata.reload = 0;
       tdata.param = "";
       
                             
       try
       {                                          
        window.parent.OpenMenuByData(tdata);
       }
       catch(err)
       {
           alert("error in page");
       }
   }
   else
   {
      //on new window/tab
       //window.open(url,'_blank');   
   
       window.location = url;
   }
}

function tbopenSalInfo(key) {
    if (!key) {
        key = '';
    }


    var url = "/Master/EmpSalaryInfo.aspx?eid=" + key
    //if (pageInTab == 1)
    if (ZForm.PageMode == Enums.PageMode.InTab) {

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

function fromParent(val1)
{
    alert('this is called from parent: ' + val1);
}


function showMessage() {
    var msg = 'this is message';
    var newDialog;
    newDialog = $('<div class="popup" title="Save item">' + msg +  '</div>');


    var buttonsConfig = [
    {
        text: "Ok",
        "class": "ok",
        click: function () {
        }
    },
    {
        text: "Annulla",
        "class": "cancel",
        click: function () {
            newDialog.dialog("close");
        }
    }
    ];

    newDialog.dialog({
        resizable: false,
        modal: true,
        show: 'clip',
        buttons: buttonsConfig
    });



}

// ]]>

function Button1_onclick() {
    showMessage();
}

    </script>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="container-fluid dashboard-ecommerce py-4">

    <!-- Filter -->
    <div class="row mb-3">
     <div class="col-md-12 text-right">
      <div class="d-flex justify-content-end align-items-center">
        <asp:TextBox ID="txtFilterDate" runat="server" CssClass="form-control date_range_picker mr-2" Width="300px" placeholder="YYYY-MM-DD" />
        <asp:Button ID="btnFilter" runat="server" Text="Filter" CssClass="btn btn-sm btn-primary" OnClick="btnFilter_Click" />
      </div>
    </div>

    </div>

    <!-- Summary Cards -->
    <div class="row header-summery mb-4">
      <div class="col-md-3 col-lg-3 mb-3">
    
          <div class="card border-top border-top-primary card-link-hover">
            <div class="card-body text-center">
              <i class="fa fa-box-open fa-2x"></i>
              <h5>Total Parcel</h5>
              <h1><asp:Literal ID="litTotalParcel" runat="server" /></h1>
            </div>
          </div>
      
      </div>
      <div class="col-md-3 col-lg-3 mb-3">
    
          <div class="card border-top border-top-primary card-link-hover">
            <div class="card-body text-center">
              <i class="fa fa-users fa-2x"></i>
              <h5>Total User</h5>
              <h1><asp:Literal ID="litTotalUser" runat="server" /></h1>
            </div>
          </div>
      
      </div>
       <div class="col-md-3 col-lg-3 mb-3">
    
          <div class="card border-top border-top-primary card-link-hover">
            <div class="card-body text-center">
              <i class="fa fa-users fa-2x"></i>
              <h5>Total Client</h5>
              <h1><asp:Literal ID="litTotalClient" runat="server" /></h1>
            </div>
          </div>
      
      </div>
       <div class="col-md-3 col-lg-3 mb-3">
    
          <div class="card border-top border-top-primary card-link-hover">
            <div class="card-body text-center">
              <i class="fa fa-users fa-2x"></i>
              <h5>Total Delivery Man</h5>
              <h1><asp:Literal ID="litTotalDeliveryMan" runat="server" /></h1>
            </div>
          </div>
      
      </div>
      <!-- Add other metric cards similarly -->
    </div>

     <div class="row header-summery mb-4">
      <div class="col-md-3 col-lg-3 mb-3">
    
          <div class="card border-top border-top-primary card-link-hover">
            <div class="card-body text-center">
              <i class="fa fa-warehouse fa-2x"></i>
              <h5>Total Hub</h5>
              <h1><asp:Literal ID="litTotalHub" runat="server" /></h1>
            </div>
          </div>
        
      </div>
      <div class="col-md-3 col-lg-3 mb-3">
    
          <div class="card border-top border-top-primary card-link-hover">
            <div class="card-body text-center">
              <i class="fa fa-credit-card fa-2x"></i>
              <h5>Total Accounts</h5>
              <h1><asp:Literal ID="litTotalAccounts" runat="server" /></h1>
            </div>
          </div>
      
      </div>
       <div class="col-md-3 col-lg-3 mb-3">
    
          <div class="card border-top border-top-primary card-link-hover">
            <div class="card-body text-center">
              <i class="fa fa-handshake fa-2x"></i>
              <h5>Agreement</h5>
              <h1><asp:Literal ID="litAgreement" runat="server" /></h1>
            </div>
          </div>
      
      </div>
       <div class="col-md-3 col-lg-3 mb-3">
    
          <div class="card border-top border-top-primary card-link-hover">
            <div class="card-body text-center">
              <i class="fa fa-box-open fa-2x"></i>
              <h5>Total Parcel Delivered</h5>
              <h1><asp:Literal ID="litTotalParcelDelivered" runat="server" /></h1>
            </div>
          </div>
      
      </div>
      <!-- Add other metric cards similarly -->
    </div>

 <!-- Statement Lists -->
<div class="row mb-4">
  <!-- Delivery Man -->
  <div class="col-md-4">
    <ul class="list-group">
      <li class="list-group-item text-center font-weight-bold">Delivery Man Statements</li>
      <li class="list-group-item">Income <span class="float-right"><asp:Literal ID="litDMIncome" runat="server" /></span></li>
      <li class="list-group-item">Expense <span class="float-right"><asp:Literal ID="litDMExpense" runat="server" /></span></li>
      <li class="list-group-item">Balance <span class="float-right"><asp:Literal ID="litDMBalance" runat="server" /></span></li>
    </ul>
  </div>

  <!-- Merchant -->
  <div class="col-md-4">
    <ul class="list-group">
      <li class="list-group-item text-center font-weight-bold">Merchant Statements</li>
      <li class="list-group-item">Income <span class="float-right"><asp:Literal ID="litMerchantIncome" runat="server" /></span></li>
      <li class="list-group-item">Expense <span class="float-right"><asp:Literal ID="litMerchantExpense" runat="server" /></span></li>
      <li class="list-group-item">Balance <span class="float-right"><asp:Literal ID="litMerchantBalance" runat="server" /></span></li>
    </ul>
  </div>

  <!-- Branch -->
  <div class="col-md-4">
    <ul class="list-group">
      <li class="list-group-item text-center font-weight-bold">Branch Statements</li>
      <li class="list-group-item">Income <span class="float-right"><asp:Literal ID="litBranchIncome" runat="server" /></span></li>
      <li class="list-group-item">Expense <span class="float-right"><asp:Literal ID="litBranchExpense" runat="server" /></span></li>
      <li class="list-group-item">Balance <span class="float-right"><asp:Literal ID="litBranchBalance" runat="server" /></span></li>
    </ul>
  </div>
</div>


    <!-- Charts -->
    <div class="row mb-4">
     <div class="col-xl-6 mb-3">
        <div class="card">
            <div class="card-body" style="position:relative;height:350px;">
                <!-- Chart.js needs a canvas element -->
                <canvas id="chartIncomeExpense" width="100%" height="100%"></canvas>
            </div>
            <div class="card-footer">
                <span class="text-primary">৳ <asp:Literal ID="litIncomeTotal" runat="server" /></span>
                <span class="float-right text-secondary">৳ <asp:Literal ID="litExpenseTotal" runat="server" /></span>
            </div>
        </div>

        <!-- This literal should render the JSON data for the chart -->
        <asp:Literal ID="litChartData" runat="server" />
    </div>
     <div class="col-xl-6 mb-3">
      <div class="card">
        <div class="card-body" style="position:relative;height:350px;">
          <canvas id="chartPieRevenue" width="100%" height="100%"></canvas>
          <asp:Literal ID="litPieChartData" runat="server" />
        </div>
        <div class="card-footer">
          <!-- Totals -->
          <span class="text-primary">৳ <asp:Literal ID="litRevenueTotal" runat="server" /></span>
          <span class="float-right text-secondary">৳ <asp:Literal ID="Literal1" runat="server" /></span>
        </div>
      </div>
    </div>
    </div>

    <!-- Date Filter Widget -->
    <div class="row">
      <div class="col-12 mb-5">
          <div class="card">
             <div class="card-body">
               <div id="datetimepicker12"></div>
             </div>

          </div>
     
      </div>
    </div>
  </div>
</asp:Content>



