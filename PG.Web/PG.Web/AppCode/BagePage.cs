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
using PG.Core.Web;
using PG.DBClass.SecurityDC;
using PG.BLLibrary.SecurityBL;
using PG.Common;

namespace PG.Web
{
    public class BagePage : ICoreBagePage
    {
        protected AppObjectEnum AppObjectID;

        protected string DateFormat;
        protected string DateFormatGrid;
        protected string DateFormatReport;
        protected string IntFormat;
        protected string DecimalFormat;
        protected string DecimalFormat2;
        protected string CurrencyFormat;
        protected string CurrencyFormat2;

        protected string ControlID_IconImageName = "dvIconEditMode";
        protected string ControlID_AppID = "hdnAppID";
        protected string ControlID_AppObjectID  = "hdnAppObjectID";
        protected string ControlID_UserID = "hdnUserID";
        protected string ControlID_ObjectPermission = "hdnObjectPermission";

        //protected MMS.BLL.User UserObject = new MMS.BLL.User();
        
        public BagePage()
        {
            LoginUserName = HttpContext.Current.User.Identity.Name;
            LoginUserID = AppSecurity.GetUser(AppGlobals.LoginInfoAppID, LoginUserName).UserID; 
            DateFormat = Globals.GetDateFormat();
            DateFormatGrid = Globals.GetDateFormatGrid();
            DateFormatReport = Globals.GetDateFormatReport();
            IntFormat = Globals.GetIntFormat();
            DecimalFormat = Globals.GetDecimalFormat();
            DecimalFormat2 = Globals.GetDecimalFormat2();
            CurrencyFormat = Globals.GetCurrencyFormat();
            CurrencyFormat2 = Globals.GetCurrencyFormat2();
            //

            

            //this.AppMessageCurrent = AppMessage.GetAppMessageByQueryString(); // base.GetAppMessage();
        }



        protected override void OnLoad(EventArgs e)
        {
            this.AppMessageID = this.GetPageQueryInteger(AppMessage.QueryStingKey);
            this.AppMessageCurrent = AppMessage.GetAppMessageFromSessionByID(this.AppMessageID);
            base.OnLoad(e);
           
            // your code
            //if (this.DbContext != null)
            //    this.DbContext.ReleaseDBContext();
           // SetContentPageOnLoad();
           // this.AppMessageCurrent = AppMessage.GetAppMessageByQueryString(); // base.GetAppMessage();
        }

        protected override void OnPreRender(EventArgs e)
        {
            SetIconImage();
            base.OnPreRender(e);
        }  



        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            // your code
            //if (this.DbContext != null)
            //    this.DbContext.ReleaseDBContext();
            
        }

        public void SetIconImage()
        {
            Control ctl = WebUtility.FindControlRecursive(this.ControlID_IconImageName, this.Page);
            if (ctl != null)
            {
                WebUtility.RemoveCssClass(ctl, "iconNew");
                WebUtility.RemoveCssClass(ctl, "iconEdit");
                WebUtility.RemoveCssClass(ctl, "iconDelete");
                WebUtility.RemoveCssClass(ctl, "iconView");
                WebUtility.RemoveCssClass(ctl, "iconList");

                switch (this.EditMode)
                {
                    case Core.FormDataMode.Read:
                        WebUtility.AddCssClass(ctl, "iconView");
                        break;
                    case Core.FormDataMode.Add:
                        WebUtility.AddCssClass(ctl, "iconNew");
                        break;
                    case Core.FormDataMode.Edit:
                        WebUtility.AddCssClass(ctl, "iconEdit");
                        break;
                    case Core.FormDataMode.Delete:
                        WebUtility.AddCssClass(ctl, "iconDelete");
                        break;
                    case Core.FormDataMode.List:
                        WebUtility.AddCssClass(ctl, "iconList");
                        break;
                    default:
                        WebUtility.AddCssClass(ctl, "iconView");
                        break;
                }
            }
        }


        public void SetUserAppInfo()
        {
            dcUser user = AppSecurity.GetUserInfoFromSession(HttpContext.Current);
            Control ctl = (Control)WebUtility.FindControlRecursive(this.ControlID_AppID, this.Page);
            if (ctl != null)
            {
                ((HtmlInputHidden)ctl).Value = this.AppID.ToString();
            }
                
                
            ctl = (Control)WebUtility.FindControlRecursive(this.ControlID_AppObjectID, this.Page);
            if (ctl != null)
            {
                ((HtmlInputHidden)ctl).Value = this.AppObjectID.ToString();
            }

            ctl = (Control)WebUtility.FindControlRecursive(this.ControlID_UserID, this.Page);
            if (ctl != null)
            {
                ((HtmlInputHidden)ctl).Value = user.UserID.ToString();
            }

            //ctl = (Control)WebUtility.FindControlReqursive(this.ControlID_UserID, this.Page);
            //if (ctl != null)
            //{
            //    ((HtmlInputHidden)ctl).Value = user.UserID.ToString();
            //}

        }


        public void ShowPageMessage(Label lblMessage)
        {
            ShowPageMessage(lblMessage, this.AppMessageCurrent, false);
        }
        public void ShowPageMessage(Label lblMessage, bool showMessageBox)
        {
            ShowPageMessage(lblMessage, this.AppMessageCurrent, showMessageBox);
        }

        public void ShowPageMessage(Label lblMessage, AppMessage pMessage, bool showMessageBox)
        {
            
            if (pMessage == null)
            {
                lblMessage.Text = string.Empty;
                lblMessage.Visible = false;  
            }
            else
            {
                System.Drawing.Color backColor = System.Drawing.Color.Transparent;
                System.Drawing.Color foreColor = System.Drawing.Color.Black;    
                switch (pMessage.MessageType)
                {
                    case MessageTypeEnum.None:
                        backColor = System.Drawing.Color.Transparent;
                        break;
                    case MessageTypeEnum.Information:
                        backColor = System.Drawing.Color.Transparent;
                        break;
                    case MessageTypeEnum.Successful:
                        //color = System.Drawing.Color.LightGreen;
                        //color = "#20b2aa";
                        backColor = System.Drawing.Color.MediumAquamarine;
                        break;
                    case MessageTypeEnum.Error:
                        //color = System.Drawing.Color.Red;
                        backColor = System.Drawing.Color.Pink;
                        break;
                    case MessageTypeEnum.Permission:
                        backColor = System.Drawing.Color.Red;
                        break;
                    case MessageTypeEnum.InvalidData:
                        //color = System.Drawing.Color.Red;
                        backColor = System.Drawing.Color.Pink;
                        break;

                    case MessageTypeEnum.MissingData:
                        //color = System.Drawing.Color.Red;
                        backColor = System.Drawing.Color.Pink;
                        break;

                    default:
                        backColor = System.Drawing.Color.Transparent;
                        break;
                }
                lblMessage.Text = pMessage.MessageString;
                ///lblStatus.ForeColor = color;
                lblMessage.BackColor = backColor;

                lblMessage.Visible = pMessage.MessageString.Trim() == string.Empty ? false : true;
                if (showMessageBox | pMessage.ShowMessageBox)
                {
                    this.SetStartupMessageBox(pMessage.MessageString, pMessage.MessageType);
                }
            }
        }
        public void ClearPageMessage(Label lblStatus)
        {
            lblStatus.Text = string.Empty;
            lblStatus.Visible = false;
        }

        protected bool CheckObjectPermission(AppObjectEnum objID, PermissionEnum seekPerm)
        {
            bool pRet = AppSecurity.CheckObjectPermissionByUserID(this.LoginUserID, objID, seekPerm);

            if (!pRet)
            {
                AppMessage appMsg = new AppMessage();
                appMsg.MessageType = MessageTypeEnum.Permission;
                appMsg.RemoveMessageOnRead = true;
                appMsg.MessageString = "You don't have permission for this object";
                Globals.ShowMessagePage(appMsg);
                //MMS.Globals.RemoveStatusMessage();
                //MMS.Globals.ShowMessagePage(MMS.MessageTypeEnum.Permission, "Permission Denied", "");
            }
            
            return pRet;
        }

        protected bool CheckPermissionRead()
        {
            bool pRet = AppSecurity.CheckObjectPermissionByUserID(this.LoginUserID, this.AppObjectID, PermissionEnum.Read);

            if (!pRet)
            {
                AppMessage appMsg = new AppMessage();
                appMsg.MessageType = MessageTypeEnum.Permission;
                appMsg.RemoveMessageOnRead = true;
                appMsg.MessageString = "You don't have read permission for this object";
                appMsg.ShowBackButton = true;
                Globals.ShowMessagePage(appMsg);
                //MMS.Globals.RemoveStatusMessage();
                //MMS.Globals.ShowMessagePage(MMS.MessageTypeEnum.Permission, "You don't have read permission for this object", "");
            }
            return pRet;
        }
        protected bool CheckPermissionAdd()
        {
            bool pRet = AppSecurity.CheckObjectPermissionByUserID(this.LoginUserID, this.AppObjectID, PermissionEnum.Add);
            if (!pRet)
            {
                AppMessage appMsg = new AppMessage();
                appMsg.MessageType = MessageTypeEnum.Permission;
                appMsg.RemoveMessageOnRead = true;
                appMsg.ShowBackButton = true;
                appMsg.MessageString = "You don't have add permission for this object";
                Globals.ShowMessagePage(appMsg);

                //MMS.Globals.RemoveStatusMessage();
                //MMS.Globals.ShowMessagePage(MMS.MessageTypeEnum.Permission, "You don't have add permission for this object", "");
            }
            return pRet;
        }
        protected bool CheckPermissionEdit()
        {
            bool pRet = AppSecurity.CheckObjectPermissionByUserID(this.LoginUserID, this.AppObjectID, PermissionEnum.Edit);

            if (!pRet)
            {
                AppMessage appMsg = new AppMessage();
                appMsg.MessageType = MessageTypeEnum.Permission;
                appMsg.RemoveMessageOnRead = true;
                appMsg.MessageString = "You don't have edit permission for this object";
                appMsg.ShowBackButton = true;
                Globals.ShowMessagePage(appMsg);


                //MMS.Globals.RemoveStatusMessage();
                //MMS.Globals.ShowMessagePage(MMS.MessageTypeEnum.Permission, "You don't have edit permission for this object", "");
            }
            return pRet;
        }
        protected bool CheckPermissionDelete()
        {
            bool pRet = AppSecurity.CheckObjectPermissionByUserID(this.LoginUserID, this.AppObjectID, PermissionEnum.Delete);
            if (!pRet)
            {
                AppMessage appMsg = new AppMessage();
                appMsg.MessageType = MessageTypeEnum.Permission;
                appMsg.RemoveMessageOnRead = true;
                appMsg.MessageString = "You don't have delete permission for this object";
                appMsg.ShowBackButton = true;
                Globals.ShowMessagePage(appMsg);
                //MMS.Globals.RemoveStatusMessage();
                //MMS.Globals.ShowMessagePage(MMS.MessageTypeEnum.Permission, "You don't have delete permission for this object", "");
            }
            return pRet;
        }


    }


}
