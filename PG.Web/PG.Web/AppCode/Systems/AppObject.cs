using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace PG.Web
{

    public enum AppObjectTypeEnum
    {
        Undefined = 0,
        From = 1,
        Procedure = 2,
        Report = 3,
        Task = 4,
        Menu = 5,
        Option = 6,
        FormOption = 7,
        
    }

    public enum AppObjectEnum
    {
        Frm_User = 1001,
        Frm_Role = 1002,
        Frm_RolePermission = 1003,
        Frm_SetPassword = 1004,

        FrmOpt_BlockedEmployee = 5001,
        Frm_Employee = 5002,
        Frm_EmpSalaryInfo = 5003,
        Frm_EmpSalaryData = 5004,
        Frm_Journal=6110,



        Frm_PFMember = 5110,


        Frm_LocationJournalType = 3205,



        Tsk_SalaryProcess = 5501,
        Tsk_PayslipEmail = 5521,
        Frm_ITC = 6112,
        Frm_IRR_DEPT=6113,
    }


}
