using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using PG.Core.Web;

namespace PG.Web.PageLinks
{
    public class AccountingLinks
    {
        public static string GetLink_GLAccount
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Accounting/GetGLAccount.ashx"); }
        }

        public static string GetLink_LocationGLAccount
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Accounting/GetLocationGLAccount.ashx"); }
        }


        public static string GetLink_GLGroup
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Accounting/GetGLGroup.ashx"); }
        }


        public static string GetLink_AccRefSettings
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Accounting/GetAccRefSettings.ashx"); }
        }


        public static string GetLink_AccRef
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Accounting/GetAccRef.ashx"); }
        }

        public static string GetLink_LocationAccRef
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Accounting/GetLocationAccRef.ashx"); }
        }

        public static string GetLink_AccRefCategory
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Accounting/GetAccRefCategory.ashx"); }
        }


        public static string GetLink_InstrumentGet
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Accounting/GetInstrument.ashx"); }
        }

        public static string GetLink_InstrumentUpdate
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Accounting/UpdateInstrument.ashx"); }
        }

        public static string GetLink_GetJournalList
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Accounting/GetJournalList.ashx"); }
        }

        public static string GetLink_GetJournal
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Accounting/GetJournal.ashx"); }
        }

        public static string GetLink_UpdateJournal
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Accounting/UpdateJournal.ashx"); }
        }

        public static string GetLink_ValidateJournal
        {
            get { return WebUtility.GetAbsoluteUrl("~/Service/Accounting/ValidateJournal.ashx"); }
        }

    }
}