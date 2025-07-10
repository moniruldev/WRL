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

namespace PG.Core.Web
{
    [Serializable]
    public class AppMessage
    {
        private string m_MessageString = string.Empty;
        private string m_MessageDesc = string.Empty;
        private MessageTypeEnum m_MessageType = MessageTypeEnum.Information;
        private string m_MessageSource = string.Empty;
        private string m_StackTrace = string.Empty;
        private bool m_ShowMessageBox = false;
        private bool m_ShowBackButton = false;
        private bool m_RemoveMessageOnRead = true;
        private static string m_KeyPrfix = "_msg_";
        private static string m_QueryStingKey = "_msg";

        public string MessageString
        {
            get { return m_MessageString; }
            set { m_MessageString = value; }
        }

        public string MessageDesc
        {
            get { return m_MessageDesc; }
            set { m_MessageDesc = value; }
        }


        public MessageTypeEnum MessageType
        {
            get { return m_MessageType; }
            set { m_MessageType = value; }
        }

        public string MessageSource
        {
            get { return m_MessageSource; }
            set { m_MessageSource = value; }
        }

        public string StackTrace
        {
            get { return m_StackTrace; }
            set { m_StackTrace = value; }
        }

        public bool ShowMessageBox
        {
            get { return m_ShowMessageBox; }
            set { m_ShowMessageBox = value; }
        }

        public bool ShowBackButton
        {
            get { return m_ShowBackButton; }
            set { m_ShowBackButton = value; }
        }

        public bool RemoveMessageOnRead
        {
            get { return m_RemoveMessageOnRead; }
            set { m_RemoveMessageOnRead = value; }
        }

        public static string QueryStingKey
        {
            get { return m_QueryStingKey; }
            set { m_QueryStingKey = value; }
        }

        public static string CreateQueryString(int pMsgID)
        {
            return QueryStingKey + "=" + pMsgID.ToString();
        }

        public static AppMessage GetAppMessageFromSessionByID(int pMsgID)
        {
            return GetAppMessageFromSessionByID(pMsgID, true);
        }

        public static AppMessage GetAppMessageFromSessionByID(int pMsgID, bool pRemoveAfterRead)
        {
            AppMessage oMsg = null;

            string msgKey = m_KeyPrfix + pMsgID;

            if (HttpContext.Current.Session[msgKey] != null)
            {
                if (HttpContext.Current.Session[msgKey] is AppMessage)
                {
                    oMsg = HttpContext.Current.Session[msgKey] as AppMessage;
                    if (pRemoveAfterRead | oMsg.RemoveMessageOnRead)
                    {
                        RemoveAppMessge(pMsgID);
                    }
                }
            }
            
            return oMsg;
        }

        public static int SetAppMessageToSession(AppMessage pApMessage)
        {
            int mID = GetNextMessageKeyID();
            string msgKey = m_KeyPrfix + mID.ToString();
            HttpContext.Current.Session[msgKey] = pApMessage;
            return mID;
        }

        public static int GetNextMessageKeyID()
        {
            bool isExits = true;
            int iNext = 0;
            Random rnd = new Random();
            while (isExits)
            {
                iNext = rnd.Next(1000);
                string msgKey = m_KeyPrfix + iNext.ToString();
                if (HttpContext.Current.Session[msgKey] == null)
                {
                    isExits = false;
                }
            }
            return iNext;
        }

        public static void RemoveAppMessge(int pMsgID)
        {
            string msgKey = m_KeyPrfix + pMsgID;
            HttpContext.Current.Session.Remove(msgKey);
        }


        public static AppMessage GetAppMessageByQueryString()
        {
            return GetAppMessageByQueryString(QueryStingKey);
        }
        public static AppMessage GetAppMessageByQueryString(string pQSKey)
        {
            AppMessage cMessage = null;
            string qsKey = AppMessage.QueryStingKey;
            if (HttpContext.Current.Request.QueryString[pQSKey] != null)
            {
                string sKey = HttpContext.Current.Request.QueryString[pQSKey].Trim();
                int iMsgID;
                if (int.TryParse(sKey, out iMsgID))
                {
                    cMessage = AppMessage.GetAppMessageFromSessionByID(iMsgID);
                }
            }
            return cMessage;
        }

        public static string GetMessageTypeTextByType(MessageTypeEnum msgType)
        {
            string text = "Info";

            switch (msgType)
            {
                case MessageTypeEnum.None:
                    text = string.Empty;
                    break;
                case MessageTypeEnum.Information:
                    text = "Info";
                    break;
                case MessageTypeEnum.Successful:
                    text = "Success";
                    break;
                case MessageTypeEnum.Error:
                    text = "Error";
                    break;
                case MessageTypeEnum.Permission:
                    text = "Permission";
                    break;
                case MessageTypeEnum.InvalidData:
                    text = "Invalid Data";
                    break;
                case MessageTypeEnum.MissingData:
                    text = "Data Missing";
                    break;
                case MessageTypeEnum.Wait:
                    text = "Please Wait";
                    break;
                case MessageTypeEnum.InProgress:
                    text = "Task In Progress";
                    break;
                case MessageTypeEnum.Processing:
                    text = "Processing";
                    break;
                default:
                    text = "Info";
                    break;
            }
            return text;
        }


    }
}
