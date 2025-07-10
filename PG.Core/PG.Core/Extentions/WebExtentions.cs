using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using PG.Core.Web;

namespace PG.Core.Extentions
{
    public static class WebExtentions
    {

        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request["isAjax"] != null)
            {
                return true;
            }
            var page = HttpContext.Current.Handler as Page;
            if (request.HttpMethod.Equals("post", StringComparison.InvariantCultureIgnoreCase) && !page.IsPostBack)
            {
                return true;
            }
            if (request != null)
            {
                return (request["X-Requested-With"] == "XMLHttpRequest") || ((request.Headers != null) && (request.Headers["X-Requested-With"] == "XMLHttpRequest"));
            }
            return false;
        }  
          


        public static Control FindAnyControl(this Page page, string controlId)
        {
            return WebUtility.FindControlRecursive(controlId, page.Form);
        }

        public static Control FindAnyControl(this UserControl control, string controlId)
        {
            return WebUtility.FindControlRecursive(controlId, control);
        }

        public static void AddCssClass(this Control control, string className)
        {
            if (control is WebControl)
            {
                WebControl wCtl = control as WebControl;
                string curClass = wCtl.CssClass;
                wCtl.CssClass = WebUtility.AddCssClassString(className, curClass);
            }

            if (control is HtmlControl)
            {
                HtmlControl hCtl = control as HtmlControl;
                string curClass = string.Empty;
                if (hCtl.Attributes["class"] != null)
                {
                    curClass = hCtl.Attributes["class"];
                }
                hCtl.Attributes["class"] = WebUtility.AddCssClassString(className, curClass);
            }
        }

        public static void RemoveCssClass(this Control control, string className)
        {
            if (control is WebControl)
            {
                WebControl wCtl = control as WebControl;
                string curClass = wCtl.CssClass;
                wCtl.CssClass = WebUtility.RemoveCssClassString(className, curClass);
            }

            if (control is HtmlControl)
            {
                HtmlControl hCtl = control as HtmlControl;
                string curClass = string.Empty;
                if (hCtl.Attributes["class"] != null)
                {
                    curClass = hCtl.Attributes["class"];
                }
                hCtl.Attributes["class"] = WebUtility.RemoveCssClassString(className, curClass);
            }
        }

        public static void ReplaceCssClass(this Control control, string classNameOld, string classNameNew)
        {
            if (control is WebControl)
            {
                WebControl wCtl = control as WebControl;
                string curClass = wCtl.CssClass;
                wCtl.CssClass = WebUtility.ReplaceCssClassString(classNameOld, classNameNew, curClass);
            }

            if (control is HtmlControl)
            {
                HtmlControl hCtl = control as HtmlControl;
                string curClass = string.Empty;
                if (hCtl.Attributes["class"] != null)
                {
                    curClass = hCtl.Attributes["class"];
                }
                hCtl.Attributes["class"] = WebUtility.ReplaceCssClassString(classNameOld, classNameNew, curClass);
            }
        }
    }
}
