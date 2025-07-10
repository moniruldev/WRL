using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace PG.Core.Web
{
    public class WebUtility
    {
        /// <summary>
        /// Restarts the Web Application
        /// Requires either Full Trust (HttpRuntime.UnloadAppDomain) 
        /// or Write access to web.config.
        /// </summary>
        public static bool RestartWebApplication()
        {
            bool Error = false;

            ////First try killing your worker process

            //try
            //{

            //    //Get the current process

            //    Process process = Process.GetCurrentProcess();

            //    // Kill the current process

            //    process.Kill();

            //    // if your application have no rights issue then it will restart your app pool

            //    return true;

            //}

            //catch (Exception ex)
            //{

            //    //if exception occoured then log exception

            //    //Logger.Log("Restart Request Failed. Exception details :-" + ex);

            //}


            try
            {
                // *** This requires full trust so this will fail
                // *** in many scenarios
                HttpRuntime.UnloadAppDomain();
            }
            catch
            {
                Error = true;
            }

            if (!Error)
                return true;

            // *** Couldn't unload with Runtime - let's try modifying web.config
            string ConfigPath = HttpContext.Current.Request.PhysicalApplicationPath + "\\web.config";

            try
            {
                 System.IO.File.SetLastWriteTimeUtc(ConfigPath, DateTime.UtcNow);
            }
            catch
            {
                return false;
            }
            return true;
        }


        public static Control FindControlRecursive(string controlId)
        {
            Page page =  HttpContext.Current.Handler as Page;
            if (page != null)
            {

                return FindControlRecursive(controlId, page);
            }
            else
            {
                return null;
            }

        }

        public static Control FindControlRecursive(string Id, Control Root)
        {
            if (Root.ID == Id)
                return Root;

            foreach (Control Ctl in Root.Controls)
            {
                Control FoundCtl = FindControlRecursive(Id, Ctl);
                if (FoundCtl != null)
                    return FoundCtl;
            }
            return null;
        }


        public static void AddCssClass(Control control, string className)
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
                string curClass = hCtl.Attributes["class"].ToString();
                hCtl.Attributes["class"] = WebUtility.AddCssClassString(className, curClass);
            }
        }

        public static void RemoveCssClass(Control control, string className)
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
                string curClass = hCtl.Attributes["class"].ToString();
                hCtl.Attributes["class"] = WebUtility.RemoveCssClassString(className, curClass);
            }
        }

        public static void ReplaceCssClass(Control control, string classNameOld, string classNameNew)
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
                string curClass = hCtl.Attributes["class"].ToString();
                hCtl.Attributes["class"] = WebUtility.ReplaceCssClassString(classNameOld, classNameNew, curClass);
            }
        }


        public static void SetPageCacheOff(HttpResponse Response)
        {
            Response.CacheControl = "no-cache";
            Response.AddHeader("Pragma", "no-cache");
            Response.Expires = -1;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            // Stop Caching in Firefox
            Response.Cache.SetNoStore();
        }


        public static string AddCssClassString(string pClassName, string pCurrentClassString)
        {
            string cssClass = RemoveCssClassString(pClassName, pCurrentClassString);
            cssClass += ' ' + pClassName;
            return cssClass;
        }

        public static string RemoveCssClassString(string pClassName, string pCurrentClassString)
        {
            string cssClass = string.Empty;
            Regex regEx = new Regex(pClassName,
                    RegexOptions.IgnoreCase | RegexOptions.Multiline);

            cssClass = regEx.Replace(pCurrentClassString, "");
            //pCurrentClassString.Replace

            return cssClass;
        }

        public static string ReplaceCssClassString(string pOldClassName, string pNewClassName, string pCurrentClassString)
        {
            string cssClass = string.Empty;
            Regex regEx = new Regex(pOldClassName,
                    RegexOptions.IgnoreCase | RegexOptions.Multiline);

            cssClass = regEx.Replace(pCurrentClassString, pNewClassName);
            //pCurrentClassString.Replace

            return cssClass;
        }

        public static string GetQueryString(string pKey)
        {
            return GetQueryString(pKey, string.Empty, HttpContext.Current);
        }
        public static string GetQueryString(string pKey, string pDefaultValue)
        {
            return GetQueryString(pKey, pDefaultValue, HttpContext.Current);
        }
        public static string GetQueryString(string pKey, HttpContext pContext)
        {
            return GetQueryString(pKey, string.Empty, pContext);
        }
        public static string GetQueryString(string pKey, string pDefaultValue,  HttpContext pContext)
        {
            string pVal = pDefaultValue;

            if (pContext.Request.QueryString[pKey] != null)
            {
                pVal = pContext.Request.QueryString[pKey].Trim();
            }


            return pVal;
        }

        public static int GetQueryStringInteger(string pKey)
        {
            return GetQueryStringInteger(pKey, 0, HttpContext.Current);
        }
        public static int GetQueryStringInteger(string pKey, int pDefaultValue)
        {
            return GetQueryStringInteger(pKey, pDefaultValue, HttpContext.Current);
        }
        public static int GetQueryStringInteger(string pKey, HttpContext pContext)
        {
            return GetQueryStringInteger(pKey, 0, pContext);
        }
        public static int GetQueryStringInteger(string pKey, int pDefaultValue, HttpContext pContext)
        {
            string pVal = pDefaultValue.ToString();
            if (pContext.Request.QueryString[pKey] != null)
            {
                pVal = pContext.Request.QueryString[pKey].Trim();
            }

            int iVal = pDefaultValue;
            Int32.TryParse(pVal, out iVal);
            return iVal;
        }

        public static decimal GetQueryStringDecimal(string pKey)
        {
            return GetQueryStringDecimal(pKey, 0, HttpContext.Current);
        }
        public static decimal GetQueryStringDecimal(string pKey, decimal pDefaultValue)
        {
            return GetQueryStringDecimal(pKey, pDefaultValue,HttpContext.Current);
        }
        public static decimal GetQueryStringDecimal(string pKey, HttpContext pContext)
        {
            return GetQueryStringDecimal(pKey, 0, pContext);
        }
        public static decimal GetQueryStringDecimal(string pKey, decimal pDefaultValue, HttpContext pContext)
        {
            string pVal = pDefaultValue.ToString();

            if (pContext.Request.QueryString[pKey] != null)
            {
                pVal = pContext.Request.QueryString[pKey].Trim();
            }

            decimal dVal = 0;
            decimal.TryParse(pVal, out dVal);
            return dVal;
        }

        public static DateTime? GetQueryStringDate(string pKey)
        {
            return GetQueryStringDate(pKey, HttpContext.Current);
        }
        public static DateTime? GetQueryStringDate(string pKey, HttpContext pContext)
        {
            string pVal = string.Empty;

            if (pContext.Request.QueryString[pKey] != null)
            {
                pVal = pContext.Request.QueryString[pKey].Trim();
            }
            DateTime? dtR = null;
            DateTime dt;


            if (DateTime.TryParse(pVal, out dt))
            {
                dtR = dt;
            }

            return dtR;
        }

        public static List<string> GetQueryStringList(string pKey)
        {
            return GetQueryStringList(pKey, HttpContext.Current);
        }
        public static List<string> GetQueryStringList(string pKey, HttpContext pContext)
        {
            return GetQueryStringList(pKey, ',', pContext);
        }
        public static List<string> GetQueryStringList(string pKey, char pSeperator)
        {
            return GetQueryStringList(pKey, pSeperator, HttpContext.Current);
        }
        public static List<string> GetQueryStringList(string pKey, char pSeperator, HttpContext pContext)
        {
            List<string> strList = new List<string>();

            string pVal = string.Empty;
            if (pContext.Request.QueryString[pKey] != null)
            {
                pVal = pContext.Request.QueryString[pKey].Trim();
            }

            if (pVal != string.Empty)
            {
                string[] strArray = pVal.Split(new char[]{pSeperator});
                strList = strArray.ToList();
            }
            return strList;
        }

        public static List<int> GetQueryStringIntList(string pKey)
        {
            return GetQueryStringIntList(pKey, ',', 0, HttpContext.Current);
        }
        public static List<int> GetQueryStringIntList(string pKey, HttpContext pContext)
        {
            return GetQueryStringIntList(pKey, ',', 0, pContext);
        }
        public static List<int> GetQueryStringIntList(string pKey, char pSeperator)
        {
            return GetQueryStringIntList(pKey, pSeperator, 0, HttpContext.Current);
        }
        public static List<int> GetQueryStringIntList(string pKey, char pSeperator, HttpContext pContext)
        {
            return GetQueryStringIntList(pKey, pSeperator, 0, pContext);
        }
        public static List<int> GetQueryStringIntList(string pKey, char pSeperator, int pDefaultValue)
        {
            return GetQueryStringIntList(pKey, pSeperator, 0, HttpContext.Current);
        }
        public static List<int> GetQueryStringIntList(string pKey, char pSeperator, int pDefaultValue, HttpContext pContext)
        {
            List<int> intList = new List<int>();

            List<string> strList = GetQueryStringList(pKey, pSeperator, pContext);
            foreach(string strData in strList)
            {
                intList.Add(Utility.Conversion.StringToInt(strData, pDefaultValue));
            }
            return intList;
        }

        public static List<decimal> GetQueryStringDecimalList(string pKey)
        {
            return GetQueryStringDecimalList(pKey, ',', 0, HttpContext.Current);
        }
        public static List<decimal> GetQueryStringDecimalList(string pKey, HttpContext pContext)
        {
            return GetQueryStringDecimalList(pKey, ',', 0, pContext);
        }
        public static List<decimal> GetQueryStringDecimalList(string pKey, char pSeperator)
        {
            return GetQueryStringDecimalList(pKey, pSeperator, 0, HttpContext.Current);
        }
        public static List<decimal> GetQueryStringDecimalList(string pKey, char pSeperator, HttpContext pContext)
        {
            return GetQueryStringDecimalList(pKey, pSeperator, 0, pContext);
        }
        public static List<decimal> GetQueryStringDecimalList(string pKey, char pSeperator, decimal pDefaultValue)
        {
            return GetQueryStringDecimalList(pKey, pSeperator, 0, HttpContext.Current);
        }
        public static List<decimal> GetQueryStringDecimalList(string pKey, char pSeperator, decimal pDefaultValue, HttpContext pContext)
        {
            List<decimal> decimalList = new List<decimal>();

            List<string> strList = GetQueryStringList(pKey, pSeperator, pContext);
            foreach (string strData in strList)
            {
                decimalList.Add(Utility.Conversion.StringToDecimal(strData, pDefaultValue));
            }
            return decimalList;
        }

        public static List<DateTime> GetQueryStringDateList(string pKey)
        {
            return GetQueryStringDateList(pKey, ',', HttpContext.Current);
        }
        public static List<DateTime> GetQueryStringDateList(string pKey, HttpContext pContext)
        {
            return GetQueryStringDateList(pKey, ',', pContext);
        }
        public static List<DateTime> GetQueryStringDateList(string pKey, char pSeperator)
        {
            return GetQueryStringDateList(pKey, pSeperator, HttpContext.Current);
        }
        public static List<DateTime> GetQueryStringDateList(string pKey, char pSeperator, HttpContext pContext)
        {
            List<DateTime> datelList = new List<DateTime>();

            List<string> strList = GetQueryStringList(pKey, pSeperator, pContext);
            foreach (string strData in strList)
            {
                DateTime? cDate = Utility.Conversion.StringToDateORNull(strData);
                if (cDate != null)
                {
                    datelList.Add(cDate.Value);
                }
            }
            return datelList;
        }

        public static string AddReplaceQueryString(string fullUrl, string paramName, string paramVal)
        {
            string strR = "";
            string sParam = paramName + "=" + paramVal;
            string strQs = "";
            string leftUrl = "";

            int iQ = fullUrl.IndexOf("?");
            leftUrl = fullUrl;
            if (iQ > 0)
            {
                leftUrl = fullUrl.Substring(0, iQ);
                strQs = fullUrl.Substring(iQ);
            }

            if (strQs == string.Empty)
            {
                strR = "?" + sParam;
            }
            else
            {
                //find
                //(?<=[?|&])Page=.*?(?=&|$)
                System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex("(?<=[?|&])" + paramName + "=.*?(?=&|$)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                if (re.IsMatch(strQs))
                {

                    strR = re.Replace(strQs, sParam);
                }
                else
                {
                    strR = strQs + "&" + sParam;
                }
            }
            strR = leftUrl + strR;
            return strR;
        }

        public static ListItem FindByValue(ListBox lstBox, string value)
        {
            ListItem lItem = null;
            foreach (ListItem li in lstBox.Items)
            {
                if (li.Value.ToLower() == value.ToLower())
                {
                    lItem = li;
                    break;
                }
            }
            return lItem;
        }

        public static ListItem FindByValue(DropDownList lstBox, string value)
        {
            ListItem lItem = null;
            foreach (ListItem li in lstBox.Items)
            {
                if (li.Value.ToLower() == value.ToLower())
                {
                    lItem = li;
                    break;
                }
            }
            return lItem;
        }
        public static int FindIndexByValue(DropDownList lstBox, string value)
        {
            int i = 0;

            foreach (ListItem li in lstBox.Items)
            {
                if (li.Value == value)
                    return i;
                i++;
            }
            return -1;
        }
        public static int FindIndexByValue(ListBox lstBox, string value)
        {
            int i = 0;

            foreach (ListItem li in lstBox.Items)
            {
                if (li.Value == value)
                    return i;
                i++;
            }
            return -1;
        }
        public static ListItem FindByText(ListBox lstBox, string value)
        {
            ListItem lItem = null;
            foreach (ListItem li in lstBox.Items)
            {
                if (li.Text.ToLower() == value.ToLower())
                {
                    lItem = li;
                    break;
                }
            }
            return lItem;
        }
        public static ListItem FindByText(DropDownList lstBox, string value)
        {
            ListItem lItem = null;
            foreach (ListItem li in lstBox.Items)
            {
                if (li.Text.ToLower() == value.ToLower())
                {
                    lItem = li;
                    break;
                }
            }
            return lItem;
        }


        private static bool IsAbsolutePath(string originalUrl)
        {
            // *** Absolute path - just return
            int IndexOfSlashes = originalUrl.IndexOf("://");
            int IndexOfQuestionMarks = originalUrl.IndexOf("?");

            if (IndexOfSlashes > -1 &&
                 (IndexOfQuestionMarks < 0 ||
                  (IndexOfQuestionMarks > -1 && IndexOfQuestionMarks > IndexOfSlashes)
                  )
                )
                return true;

            return false;
        }

        /// <summary>
        /// Returns a site relative HTTP path from a partial path starting out with a ~.
        /// Same syntax that ASP.Net internally supports but this method can be used
        /// outside of the Page framework.
        /// 
        /// Works like Control.ResolveUrl including support for ~ syntax
        /// but returns an absolute URL.
        /// </summary>
        /// <param name="originalUrl">Any Url including those starting with ~</param>
        /// <returns>relative url</returns>

        public static string ResolveUrl(string originalUrl)
        {
            return ResolveUrl(originalUrl, HttpContext.Current.Request);
        }
        public static string ResolveUrl(string originalUrl, HttpRequest request)
        {
            if (string.IsNullOrEmpty(originalUrl))
                return originalUrl;

            // *** Absolute path - just return
            if (IsAbsolutePath(originalUrl))
                return originalUrl;

            // *** We don't start with the '~' -> we don't process the Url

            string workUrl = originalUrl;

            //if (!originalUrl.StartsWith("~"))
            //    return originalUrl;


            string curPath = VirtualPathUtility.GetDirectory(request.Path);
            if (!originalUrl.StartsWith("~"))
            {
                //workUrl = originalUrl.Substring(1);
                //if not starts with ~/ then add current request path to url  
                if (!originalUrl.StartsWith("/"))
                {
                    //string curPath = VirtualPathUtility.GetDirectory(request.Path);
                    workUrl = originalUrl.Insert(0, curPath);
                }
                workUrl = workUrl.Insert(0, "~");
            }
            else
            {
                workUrl = originalUrl.Substring(1);
                if (workUrl.Length > 0)
                {
                    if (!workUrl.StartsWith("/"))
                    {
                        workUrl = originalUrl.Insert(0, curPath);
                    }
                }
                workUrl = workUrl.Insert(0, "~");
            }
                


            // *** Fix up path for ~ root app dir directory
            // VirtualPathUtility blows up if there is a 
            // query string, so we have to account for this.
            int queryStringStartIndex = originalUrl.IndexOf('?');
            if (queryStringStartIndex != -1)
            {
                string queryString = workUrl.Substring(queryStringStartIndex);
                string baseUrl = workUrl.Substring(0, queryStringStartIndex);

                return string.Concat(
                    VirtualPathUtility.ToAbsolute(baseUrl),
                    queryString);
            }
            else
            {
                return VirtualPathUtility.ToAbsolute(workUrl);
            }

        }

        public static string ResolveUrl2(string relativeUrl)
        {
            if (relativeUrl == null) throw new ArgumentNullException("relativeUrl");

            if (relativeUrl.Length == 0 || relativeUrl[0] == '/' || relativeUrl[0] == '\\')
                return relativeUrl;

            int idxOfScheme = relativeUrl.IndexOf(@"://", StringComparison.Ordinal);
            if (idxOfScheme != -1)
            {
                int idxOfQM = relativeUrl.IndexOf('?');
                if (idxOfQM == -1 || idxOfQM > idxOfScheme) return relativeUrl;
            }

            StringBuilder sbUrl = new StringBuilder();
            sbUrl.Append(HttpRuntime.AppDomainAppVirtualPath);
            if (sbUrl.Length == 0 || sbUrl[sbUrl.Length - 1] != '/') sbUrl.Append('/');

            // found question mark already? query string, do not touch!
            bool foundQM = false;
            bool foundSlash; // the latest char was a slash?
            if (relativeUrl.Length > 1
                && relativeUrl[0] == '~'
                && (relativeUrl[1] == '/' || relativeUrl[1] == '\\'))
            {
                relativeUrl = relativeUrl.Substring(2);
                foundSlash = true;
            }
            else foundSlash = false;
            foreach (char c in relativeUrl)
            {
                if (!foundQM)
                {
                    if (c == '?') foundQM = true;
                    else
                    {
                        if (c == '/' || c == '\\')
                        {
                            if (foundSlash) continue;
                            else
                            {
                                sbUrl.Append('/');
                                foundSlash = true;
                                continue;
                            }
                        }
                        else if (foundSlash) foundSlash = false;
                    }
                }
                sbUrl.Append(c);
            }

            return sbUrl.ToString();
        }


        public static string GetAbsoluteUrl(string relativeUrl)
        {
            return GetAbsoluteUrl(relativeUrl, HttpContext.Current.Request);
        }

        /// <summary>
        /// This method returns a fully qualified absolute server Url which includes
        /// the protocol, server, port in addition to the server relative Url.
        /// 
        /// Works like Control.ResolveUrl including support for ~ syntax
        /// but returns an absolute URL.
        /// </summary>
        /// <param name="retalive Url">Any Url, either App relative or fully qualified</param>
        /// <param name="request">HttpRequest, default: HttpContext.Current.Request</param>
        /// <returns></returns>
        public static string GetAbsoluteUrl(string relativeUrl, HttpRequest request)
        {
            if (string.IsNullOrEmpty(relativeUrl))
                return string.Empty;

            string absUrl = string.Empty;

            try
            {
                string root = request.Url.GetLeftPart(UriPartial.Authority);
                string requestPath = VirtualPathUtility.GetDirectory(request.Path);

                string url = ResolveUrl(relativeUrl, request);
                if (!url.StartsWith("/"))
                {
                    url = "/" + url;
                }
                absUrl = string.Format("{0}{1}", root, url);
            }
            catch {}
            return absUrl;
        }

        /// <summary>
        /// Returns a relative path string from a full path based on a base path
        /// provided.
        /// </summary>
        /// <param name="fullPath">The path to convert. Can be either a file or a directory</param>
        /// <param name="basePath">The base path on which relative processing is based. Should be a directory.</param>
        /// <returns>
        /// String of the relative path.
        /// 
        /// Examples of returned values:
        ///  test.txt, ..\test.txt, ..\..\..\test.txt, ., .., subdir\test.txt
        /// </returns>
        public static string GetRelativePath(string fullPath, string basePath)
        {
            // Require trailing backslash for path
            if (!basePath.EndsWith("\\"))
                basePath += "\\";

            Uri baseUri = new Uri(basePath);
            Uri fullUri = new Uri(fullPath);

            string a= baseUri.AbsolutePath;

            Uri relativeUri = baseUri.MakeRelativeUri(fullUri);

            // Uri's use forward slashes so convert back to backward slashes
            return relativeUri.ToString().Replace("/", "\\");


            //uses
            //string relPath = FileUtils.GetRelativePath("c:\temp\templates\subdir\test.txt","c:\temp\templates")
        }


        public static string ReplaceQueryString(string url, string key, string value)
        {
            //System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex("([?|&])" + param + "=.*?(&|$)", "i");
            string finalURL = string.Empty;
            int pos = url.IndexOf(key + "=", StringComparison.OrdinalIgnoreCase);
            if (pos != -1)
            {
                finalURL = System.Text.RegularExpressions.Regex.Replace(url, @"([?&]" + key + ")=[^?&]+", "$1=" + value);

                //int posAnd = url.IndexOf('&', pos);
                //string preVal = string.Empty;
                //if (posAnd == -1)
                //{
                //    preVal = url.Substring(pos + key.Length + 1);
                //}
                //else
                //{
                //    int valLegnth = posAnd - pos + key.Length + 1;
                //    preVal = url.Substring(key.Length + 1, valLegnth);
                //}
                //finalURL = url.Replace(key + "=" + preVal, key + "=" + value);

                //finalURL = System.Text.RegularExpressions.Regex.Replace(url, @"([?&]" + key + ")=[^?&]+", "$1=" + value);
            }
            else if (url.IndexOf('?') == -1)
            {
                finalURL = url + '?' + key + "=" + value;
            }
            else
            {
                finalURL = url + '&' + key + "=" + value;
            }
            return finalURL;
        }

    }
}
