using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace PG.Core.Web
{
    public class ICoreBagePage : System.Web.UI.Page
    {
        protected DBBase.DBContext DbContext = null;

        protected int LoginUserID = 0;
        protected string LoginUserName = string.Empty;
        protected int LoginRoleID = 0;
        protected string LoginRoleName = string.Empty;

        protected int AppID = 0;
        protected string RandomString = string.Empty;

        protected AppMessage AppMessageCurrent = new AppMessage();
        protected int AppMessageID = 0;

        protected string EditModeViewStateKey = "_EditMode";
        protected string EditModeQueryStringKey = "_em";


        protected bool IsPageResize = true;
        protected bool InitDefaultFeature = true;

        protected string HiddenIsDirtyID = "hdnIsDirty";
        private string HiddenIsDirtyClientID = "ctl00_hdnIsDirty";


        protected FormDataMode m_EditMode = FormDataMode.None;
        protected FormDataMode EditMode
        {
            get { return m_EditMode; }
            set { m_EditMode = value; }
        }

        private PageModeEnum m_PageMode = PageModeEnum.None;
        protected PageModeEnum PageMode
        {
            get { return m_PageMode; }
            set { m_PageMode = value; }
        }

        private int m_PageInTabNo = 0;
        protected int PageInTabNo
        {
            get { return m_PageInTabNo; }
            set { m_PageInTabNo = value; }
        }

        private bool m_IsDirty = false;
        protected bool IsDirty
        {
            get { return m_IsDirty; }
            set { m_IsDirty = value; }
        }

        private string m_ContentOnLoadScriptName = "_ContentOnLoad";
        protected string ContentOnLoadScriptName
        {
            get { return m_ContentOnLoadScriptName; }
            set { m_ContentOnLoadScriptName = value; }
        }


        //constructor
        public ICoreBagePage()
        {
            if (Internals.IsLicenseValidate)
            {
                License.AppLicense.ValidateLicense();
            }

            SetPageInTab();
            SetLinkRandomString();
            LoginUserName = HttpContext.Current.User.Identity.Name;

        }

        protected override void OnLoad(EventArgs e)
        {
            SetIsDirty();

            base.OnLoad(e);
            // your code
            //if (this.DbContext != null)
            //    this.DbContext.ReleaseDBContext();
            SetContentPageOnLoad();
            //if (!this.IsPostBack) //firsttime
            //{
            //    int editmode = this.GetPageQueryInteger(this.EditModeQueryStringKey);
            //    this.EditMode 
            //}
            //else
            //{

           // }

            

        }

        protected override void OnPreRender(EventArgs e)
        {
            SetPageFormJSVer();
            this.ViewState[EditModeViewStateKey] = (int)this.EditMode;
            base.OnPreRender(e);
        }      
        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            // your code
            if (this.DbContext != null)
                this.DbContext.ReleaseDBContext();
        }

        private void SetContentPageOnLoad()
        {
            string key = "contentOnLoad";
            //"alert('Hello World');
            string script = "";
            if (!ClientScript.IsStartupScriptRegistered(key))
            {
                ClientScript.RegisterStartupScript(this.Page.GetType(), key, script, true);
            }

        }
        protected void InitDBContext()
        {
            InitDBContext(PG.Core.DBBase.DBContextManager.GetDBContextSettings());
        }
        protected void InitDBContext(PG.Core.DBBase.DBContextSettings pDBCSettings)
        {
            this.DbContext = new PG.Core.DBBase.DBContext(pDBCSettings);
            this.DbContext.InitDBContext();
        }
        protected void SetPageCacheOff()
        {
            //Response.CacheControl = "no-cache";
            //Response.AddHeader("Pragma", "no-cache");
            //Response.Expires = -1;
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);

            WebUtility.SetPageCacheOff(this.Response);
        }
        private void SetIsDirty()
        {
            bool vIsDirty = false;
            HtmlInputHidden fld1 = null;
            Control ctl1 = WebUtility.FindControlRecursive(this.HiddenIsDirtyID, this.Page);
            if (ctl1 != null)
            {
                fld1 = (HtmlInputHidden)ctl1;
                if (IsPostBack)
                {
                    int val = PG.Core.Utility.Conversion.StringToInt(fld1.Value);
                    vIsDirty = val == 1 ? true : false;
                }
                this.HiddenIsDirtyClientID = fld1.ClientID;
            }
           
            this.IsDirty = vIsDirty;
           
        }

        private void SetPageInTab()
        {
            string pMode = string.Empty;
            
            if (HttpContext.Current.Request.QueryString["_t"] != null)
            {
                pMode = HttpContext.Current.Request.QueryString["_t"].Trim();
            }
            int iMode = 0;
            Int32.TryParse(pMode, out iMode);
            this.PageMode = (PageModeEnum)iMode;

            //this.m_PageInTab = inTab == 1 ? 1 : 0;

          

            string pTabNo = string.Empty;
            if (HttpContext.Current.Request.QueryString["_n"] != null)
            {
                pTabNo = HttpContext.Current.Request.QueryString["_n"].Trim();
            }
            int inTabNo = 0;
            if (Int32.TryParse(pTabNo, out inTabNo))
            {
                this.m_PageInTabNo = inTabNo;
            }
        }


        private void SetPageFormJSVer()
        {
            string rootPath = WebUtility.GetAbsoluteUrl("~/");

            string TabVar = "var TabVar = {" +
                        " RootPath : '" + rootPath + "', " +
                        " PageMode : " + Convert.ToInt32(this.PageMode).ToString() + ", " +
                        " TabNo : " + this.m_PageInTabNo.ToString() + " " +
                        "};";

            string strIsDirty = this.IsDirty ? "true" : "false";
            string strCheckIsDirty = "false";
            string strIsPageResize = this.IsPageResize ? "true" : "false";
            string strInitDefaultFeature = this.InitDefaultFeature ? "true" : "false";
            string strIsPostBack = this.IsPostBack ? "true" : "false";



            if (this.EditMode == FormDataMode.Add | this.EditMode == FormDataMode.Edit)
            {
                strCheckIsDirty = "true";
            }

            
            string ZForm = "var ZForm = {" +
                    " RootPath : '" + rootPath + "', " +
                    " PageMode : " + Convert.ToInt32(this.PageMode).ToString() + ", " +
                    " EditMode : " + Convert.ToInt32(this.EditMode).ToString() + ", " +
                    " IsPostBack : " + strIsPostBack + ", " +
                    " IsDirtyEnabled : " + strCheckIsDirty + ", " +
                    " HiddenIsDirtyID : '" + this.HiddenIsDirtyClientID + "', " +
                    " IsDirty : "+  strIsDirty + ", " +
                    " IsFromUser : true, " +
                    " TabNo : " + this.m_PageInTabNo.ToString() + ", " +
                    " AppID : " + this.AppID.ToString() + ", " +
                    " LoginUserID : " + this.LoginUserID.ToString() + ", " +
                    " LoginUserName : '" + this.LoginUserName.ToString() + "', " +
                    " LoginRoleID : " + this.LoginRoleID.ToString() + ", " +
                    " LoginRoleName : '" + this.LoginRoleName.ToString() + "', " +
                    " ls : 0 " +
                    "};";


            string IForm = "var IForm = {" +
                " RootPath : '" + rootPath + "', " +
                " PageMode : " + Convert.ToInt32(this.PageMode).ToString() + ", " +
                " EditMode : " + Convert.ToInt32(this.EditMode).ToString() + ", " +
                " IsPostBack : " + strIsPostBack + ", " +
                " IsDirtyEnabled : " + strCheckIsDirty + ", " +
                " HiddenIsDirtyID : '" + this.HiddenIsDirtyClientID + "', " +
                " IsDirty : " + strIsDirty + ", " +
                " IsFromUser : true, " +
                " TabNo : " + this.m_PageInTabNo.ToString() + ", " +
                " AppID : " + this.AppID.ToString() + ", " +
                " LoginUserID : " + this.LoginUserID.ToString() + ", " +
                " LoginUserName : '" + this.LoginUserName.ToString() + "', " +
                " LoginRoleID : " + this.LoginRoleID.ToString() + ", " +
                " LoginRoleName : '" + this.LoginRoleName.ToString() + "', " +
                " IsPageResize : " + strIsPageResize + ", " +
                " InitDefaultFeature : " + strInitDefaultFeature + ", " +
                " ls : 0 " +
                "};";



            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "TabVar", TabVar, true);
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "ZForm", ZForm, true);
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "IForm", IForm, true);
        }


        protected string SetPageTabQueryString(string pURL)
        {
            string qString = pURL;
            if (this.PageMode == PageModeEnum.InTab)
            {
                qString = WebUtility.AddReplaceQueryString(pURL, "_t", ((int)PageModeEnum.InTab).ToString());
                qString = WebUtility.AddReplaceQueryString(qString, "_n", this.PageInTabNo.ToString());
                //redirectURL += "&_t=" + ((int)PageModeEnum.InTab).ToString();
                //redirectURL += "&_n=" + base.PageInTabNo.ToString();
            }
            return qString;
        }

        protected string SetPageMessageQueryString(string pURL)
        {
            return SetPageMessageQueryString(pURL, this.AppMessageID);
        }
        protected string SetPageMessageQueryString(string pURL, int pAppMessageID)
        {
            string qString = string.Empty;
            qString = WebUtility.AddReplaceQueryString(pURL, AppMessage.QueryStingKey, pAppMessageID.ToString());
            return qString;
        }



        protected void SetLinkRandomString()
        {
           // if (Globals.LinkIncludeRandom)
            {
                Random rnd = new Random();
                int i = rnd.Next(1000);
                //this.RandomString = "?" + i.ToString();
                this.RandomString = "?" + i.ToString();
            }
        }

        protected void RestrictByPageInTab()
        {
            if (this.PageMode != PageModeEnum.InTab)
            {
                Response.Write("Page must be opened in Tab");
                Response.End();
            }
        }

        protected void RestrictByPageInDialog()
        {
            if (this.PageMode != PageModeEnum.InDialog)
            {
                Response.Write("Page must be opened in Dialog");
                Response.End();
            }
        }

        protected string GetPageQueryString(string pKey)
        {
            return WebUtility.GetQueryString(pKey, string.Empty, HttpContext.Current);
        }

        protected string GetPageQueryString(string pKey, string pDefautValue)
        {
            return WebUtility.GetQueryString(pKey, pDefautValue, HttpContext.Current);
        }
        protected int GetPageQueryInteger(string pKey)
        {
            return WebUtility.GetQueryStringInteger(pKey, 0, HttpContext.Current);
        }

        protected int GetPageQueryInteger(string pKey, int pDefautValue)
        {
            return WebUtility.GetQueryStringInteger(pKey, pDefautValue, HttpContext.Current);
        }

         protected decimal GetPageQueryDecimal(string pKey)
        {
            return WebUtility.GetQueryStringDecimal(pKey, 0,HttpContext.Current);
        }

        protected decimal GetPageQueryDecimal(string pKey, decimal pDefautValue)
        {
            return WebUtility.GetQueryStringDecimal(pKey, pDefautValue,HttpContext.Current);
        }

        protected DateTime? GetPageQueryDate(string pKey)
        {
            return WebUtility.GetQueryStringDate(pKey, HttpContext.Current);
        }

        protected FormDataMode GetEditModeFromQueryString()
        {
            return GetEditModeFromQueryString(EditModeQueryStringKey);
        }

        protected FormDataMode GetEditModeFromQueryString(string pKey)
        {
            FormDataMode mode = FormDataMode.None;

            int emInt = WebUtility.GetQueryStringInteger(pKey, 0,  HttpContext.Current);

            try
            {
                mode = (FormDataMode)emInt;
            }
            finally { }


            return mode;
        }
                


        protected FormDataMode GetEditModeFromViewState()
        {
            return GetEditModeFromViewState(EditModeViewStateKey);
        }
        protected FormDataMode GetEditModeFromViewState(string key)
        {
            FormDataMode mode = FormDataMode.None;
            if (this.ViewState[key] != null)
            {
                string smode = this.ViewState[key].ToString();
                int i;
                if (int.TryParse(smode, out i))
                {
                    mode = (FormDataMode)i;
                }

            }
            return mode;
        }

        protected int GetViewStateInt(string key)
        {
            int i = 0;

            if (this.ViewState[key] != null)
            {
                string vs = this.ViewState[key].ToString();
                if (int.TryParse(vs, out i))
                {

                }
            }
            return i;
        }

        protected decimal GetViewStateDecimal(string key)
        {
            decimal i = 0;

            if (this.ViewState[key] != null)
            {
                string vs = this.ViewState[key].ToString();
                if (decimal.TryParse(vs, out i))
                {

                }
            }
            return i;
        }

        protected string GetViewStateString(string key)
        {
            string data = string.Empty;

            if (this.ViewState[key] != null)
            {
                data = this.ViewState[key].ToString();
            }
            return data;
        }

        protected DateTime? GetViewStateDateTime(string key)
        {
            DateTime? date = null;

            if (this.ViewState[key] != null)
            {
                string vs = this.ViewState[key].ToString();

                DateTime dt;
                if (DateTime.TryParse(vs, out dt))
                {
                    date = dt;
                }
            }
            return date;
        }

        protected string GetPostBackControlID()
        {
            string controlID = this.Request.Params["__EVENTTARGET"];
            Control postbackControl = null;

            if (controlID != null && controlID != String.Empty)
            {
                postbackControl = Page.FindControl(controlID);
            }
            else
            {
                foreach (string ctrl in Page.Request.Form)
                {
                    //Check if Image Button
                    if (ctrl.EndsWith(".x") || ctrl.EndsWith(".y"))
                    {
                        postbackControl = Page.FindControl(ctrl.Substring(0, ctrl.Length - 2));
                        break;
                    }
                    else
                    {
                        postbackControl = Page.FindControl(ctrl);

                        //Check if Button control      
                        if (postbackControl is Button)
                        {
                            break;
                        }

                    }

                }
            }

            string id = string.Empty;
            if (postbackControl != null)
            {
                id = postbackControl.ID;
            }

            return id;
        }
        protected Control GetPostBackControl()
        {
            Control postbackControlInstance = null;
            string postbackControlName = this.Request.Params.Get("__EVENTTARGET");

            if (postbackControlName != null && postbackControlName != string.Empty)
            {
                postbackControlInstance = this.FindControl(postbackControlName);
            }
            else
            {
                // handle the Button control postbacks
                for (int i = 0; i < this.Request.Form.Keys.Count; i++)
                {
                    postbackControlInstance = this.FindControl(this.Request.Form.Keys[i]);
                    if (postbackControlInstance is System.Web.UI.WebControls.Button)
                    {
                        return postbackControlInstance;
                    }
                }
            }

            // handle the ImageButton postbacks
            if (postbackControlInstance == null)
            {
                for (int i = 0; i < this.Request.Form.Count; i++)
                {
                    if ((this.Request.Form.Keys[i].EndsWith(".x")) || (this.Request.Form.Keys[i].EndsWith(".y")))
                    {
                        postbackControlInstance = this.FindControl(this.Request.Form.Keys[i].Substring(0, this.Request.Form.Keys[i].Length - 2));
                        return postbackControlInstance;
                    }
                }
            }
            return postbackControlInstance;
        }
        protected bool IsButtonClicked(string buttonName)
        {
            bool isClicked = false;
            foreach (string ctl in this.Request.Form)
            {
                if (ctl.EndsWith(buttonName))
                {
                    isClicked = true;
                    break;
                }
            }
            return isClicked;
        }


        protected void DisableButtonOnPostBack(Button btn)
        {
            DisableButtonOnPostBack(btn, string.Empty);
        }
        protected void DisableButtonOnPostBack(Button btn, string argument)
        {
            //Button2.Attributes.Add("onclick", "javascript:" +
            //Button2.ClientID + ".disabled=true;" +
            //ClientScript.GetPostBackEventReference(Button2, string.Empty));

            btn.Attributes.Add("onclick", "javascript:" +
            btn.ClientID + ".disabled=true;" +
            ClientScript.GetPostBackEventReference(btn, argument));
        }

        protected AppMessage GetAppMessage()
        {
            return AppMessage.GetAppMessageByQueryString();
        }


        protected void SetPageMessage(string pMsg, MessageTypeEnum pMessageType)
        {
            SetPageMessage(pMsg, pMessageType, string.Empty);
        }

        protected void SetPageMessage(string pMsg, MessageTypeEnum pMessageType, string pMessageSource)
        {
            SetPageMessage(pMsg, pMessageType, pMessageSource, false);
        }

        protected void SetPageMessage(string pMsg, MessageTypeEnum pMessageType, bool pShowMeesageBox)
        {
            SetPageMessage(pMsg, pMessageType, string.Empty, pShowMeesageBox);
        }

        protected void SetPageMessage(string pMsg, MessageTypeEnum pMessageType, string pMessageSource, bool pShowMeesageBox)
        {
            AppMessage cMessage = new AppMessage();
            cMessage.MessageString = pMsg;
            cMessage.MessageType = pMessageType;
            cMessage.MessageSource = pMessageSource;
            cMessage.ShowMessageBox = pShowMeesageBox;
            this.AppMessageCurrent = cMessage;
        }

        protected int SetPageMessageToSession()
        {
            return SetPageMessageToSession(this.AppMessageCurrent);
        }
        protected int SetPageMessageToSession(AppMessage pMessage)
        {
            this.AppMessageID = AppMessage.SetAppMessageToSession(pMessage);
            return AppMessageID;
        }

        protected void SetStartupMessageBox(string msg)
        {
            SetStartupMessageBox(msg, MessageTypeEnum.Information);
        }
        protected void SetStartupMessageBox(string msg, MessageTypeEnum msgType)
        {
            if (msg != string.Empty)
            {
                string key = "_msg_" + AppMessage.GetMessageTypeTextByType(msgType);

                if (!ClientScript.IsStartupScriptRegistered(key))
                {
                    string jsScript = "$(function(){alert('" + msg + "');});";
                    ClientScript.RegisterStartupScript(this.Page.GetType(), key, jsScript, true);
                }
            }
        }

    }
}
