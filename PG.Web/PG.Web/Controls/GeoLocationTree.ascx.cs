using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using PG.Core.Extentions;
using PG.Core.Web;
using PG.DBClass.OrganiztionDC;
using PG.BLLibrary.AccountingBL;
using PG.BLLibrary.OrganizationBL;

namespace PG.Web.Controls
{
    public partial class GeoLocationTree : System.Web.UI.UserControl
    {
        //private string m_GroupTreeText = string.Empty;

        public string GroupTreeText
        {
            get { return litGeoLocation.Text; }
            set {
                litGeoLocation.Text = value;
            }
        }

        public void SetGeoLocationTreeText(int pCompanyID)
        {
            SetGeoLocationTreeText(pCompanyID, true);
        }

        public void SetGeoLocationTreeText(int pCompanyID, bool includeGeoLocationType)
        {
            SetGeoLocationTreeText(pCompanyID, GeoLocationBL.GetLocationList(pCompanyID,0), includeGeoLocationType);
        }

        public void SetGeoLocationTreeText(int pComanyID, List<dcGeoLocation> geoLocationList)
        {
            SetGeoLocationTreeText(pComanyID, geoLocationList, true);
        }

        public void SetGeoLocationTreeText(int pComanyID, List<dcGeoLocation> geoLocationList, bool includeGeoLocationType)
        {
            if (geoLocationList == null)
            {
                geoLocationList = new List<dcGeoLocation>();
            }
            string divClassName = "geolocation_div";
            string treeClassName = "geolocation_tree";
            StringBuilder sb = new StringBuilder();
            //string sTab = "\t";
            //string sNewLine = "\r\n";

            int grpLevel = -1;

            sb.Append("<div class='" + divClassName + "'>");
            sb.AppendLine();
             sb.Append("<ul class='" + treeClassName + "'>");
            sb.AppendLine();


            List<dcGeoLocationType> geoLocationTypeList = GeoLocationTypeBL.GetGeoLocationTypeList(1);

            //first geo location type
            List<dcGeoLocationType> geoLocationTypeList_L0 = geoLocationTypeList.Where(c => c.GeoLocationTypeIDParent == 0).ToList();

            foreach (dcGeoLocationType geoLocationType in geoLocationTypeList_L0)
            {
                grpLevel++;
                FillGeoLocationStringRecursive(sb,0, grpLevel,geoLocationList);

            }







            if (includeGeoLocationType)
            {
                //List<dcGeoLocationType> geoLocationTypeList = GeoLocationTypeBL.GetGeoLocationTypeList(1);
                foreach (dcGeoLocationType geoLocationType in geoLocationTypeList)
                {
                    grpLevel++;
                    //" data = "key: 'node5.1'" ";
                    string strKey = "geoloctypeid" + geoLocationType.GeoLocationTypeID.ToString();
                    string gCode = System.Web.HttpUtility.HtmlEncode(geoLocationType.GeoLocationTypeCode);
                    string gName = System.Web.HttpUtility.HtmlEncode(geoLocationType.GeoLocationTypeName);
                    string gNameCode = System.Web.HttpUtility.HtmlEncode(geoLocationType.GeoLocationTypeName + " - " + geoLocationType.GeoLocationTypeCode);
                    string gNameName = System.Web.HttpUtility.HtmlEncode(geoLocationType.GeoLocationTypeCode + " - " + geoLocationType.GeoLocationTypeName);
                    string gNameShow = gName;

                    StringBuilder sbG = new StringBuilder();
                    sbG.Append("{{");
                    sbG.Append("glgroupkey:'{0}'");
                    sbG.Append(", glclassid:{1}");
                    sbG.Append(", glgroupid:{2}");
                    sbG.Append(", glgroupcode:'{3}'");
                    sbG.Append(", glgroupname:'{4}'");
                    sbG.Append(", glgroupnameshow:'{5}'");
                    sbG.Append(", glgroupclassid:{8}");
                    sbG.Append(", glgroupidparent:{9}");
                    sbG.Append("}}");

                    string strItem = string.Format(sbG.ToString()
                                    , strKey
                                    , geoLocationType.GeoLocationTypeID
                                    , 0
                                    , gCode
                                    , gName
                                    , gNameShow
                                    , 0
                                    , 0
                                    );


                    //string strItem = string.Format("{{glgroupkey:'{0}',glclassid:{1},glgroupid:{2}, glgroupcode:'{3}', glgroupname:'{4}', balancetype:{5},  isgrossprofit:{6}, glgroupclassid:{7},glgroupidparent:{8}}}"
                    //                                , strKey, glClass.GLClassID, 0, gCode, gName, balanceType, isGrossProfit, 0, 0);
                    
                    
                    string strData = string.Format(" id=\"{0}\"  data = \"key: '{0}', icon : false, item:{1}\"", strKey, strItem);

                    sb.Append(string.Format("<li {0}>",strData));
                    //sb.Append(glClass.GLClassName);
                    sb.Append(gNameShow);
                    List<dcGeoLocation> geoLocationListByType = geoLocationList.Where(c => c.GeoLocationTypeID == geoLocationType.GeoLocationTypeID).ToList();
                    int grpCount = geoLocationListByType.Count;
                    if (grpCount > 0)
                    {
                        sb.AppendLine();
                        sb.Append("<ul>");
                        sb.AppendLine();
                    }
                    int totChild = FillGeoLocationStringRecursive(sb, 0, grpLevel, geoLocationListByType);
                    if (grpCount > 0)
                    {
                        sb.AppendLine();
                        sb.Append("</ul>");
                    }
                    sb.AppendLine();
                    sb.Append("</li>");
                    sb.AppendLine();
                }
            }
            else
            {
                FillGeoLocationStringRecursive(sb, 0, grpLevel, geoLocationList);
            }
            sb.AppendLine();
            sb.Append("</ul>");
            sb.AppendLine();
            sb.Append("</div>");

            litGeoLocation.Text = sb.ToString();

        }

        private static int FillGeoLocationStringRecursive(StringBuilder pSbTree, int pParentID, int pLevel, List<dcGeoLocation> cList)
        {
            int cnt = 0;
            string sTab = "\t";
            string sNewLine = "\r\n";


            pLevel++;

            List<dcGeoLocation> pList = cList.Where(m => m.GeoLocationIDParent == pParentID).ToList();

            pList = pList.OrderBy(m => m.GeoLocationSLNo).ThenBy(m => m.GeoLocationName).ToList();
            //pList = pList.OrderBy(fieldID).ToList();

            foreach (dcGeoLocation geoLocation in pList)
            {
                cnt++;

                int geolocid = geoLocation.GeoLocationID;

                int childCount = cList.Where(m => m.GeoLocationIDParent == geolocid).Count();

                string strPrefix = PG.Core.Utility.Helper.RepeatString(sTab, pLevel);
                pSbTree.AppendLine();
                pSbTree.Append(strPrefix);


                string strKey = "geolocid" + geoLocation.GeoLocationID.ToString();

                string gCode = System.Web.HttpUtility.HtmlEncode(geoLocation.GeoLocationCode);
                string gName = System.Web.HttpUtility.HtmlEncode(geoLocation.GeoLocationName);
                //string gNameShort = System.Web.HttpUtility.HtmlEncode(grp.GLGroupNameShort);
                string gNameCode = System.Web.HttpUtility.HtmlEncode(geoLocation.GeoLocationName + " - " + geoLocation.GeoLocationCode);
                string gCodeName = System.Web.HttpUtility.HtmlEncode(geoLocation.GeoLocationCode + " - " + geoLocation.GeoLocationName);

                string gNameShow = gNameCode;

                StringBuilder sbG = new StringBuilder();
                sbG.Append("{{");
                sbG.Append("geolockey:'{0}'");
                sbG.Append(", geoloctypeid:{1}");
                sbG.Append(", geolocid:{2}");
                sbG.Append(", geoloccode:'{3}'");
                sbG.Append(", geolocname:'{4}'");
                sbG.Append(", geolocnameshow:'{5}'");
                sbG.Append(", geolocidparent:{6}");
                sbG.Append("}}");


                string strItem = string.Format(sbG.ToString()
                                , strKey
                                , geoLocation.GeoLocationTypeID
                                , geoLocation.GeoLocationID
                                , gCode
                                , gName
                                , gNameShow
                                , geoLocation.GeoLocationIDParent
                                );




                string strData = string.Format(" id=\"{0}\"  data = \"key: '{0}', icon : false, item:{1}\"", strKey, strItem);
                pSbTree.Append(string.Format("<li {0}>", strData));
                //pSbTree.Append(gName);
                pSbTree.Append(gNameShow);
                if (childCount > 0)
                {
                    string ulTag = sNewLine + strPrefix + "<ul>" + sNewLine;
                    pSbTree.Append(ulTag);
                    pSbTree.AppendLine();
                }

                //Recurisive call
                int totChild = FillGeoLocationStringRecursive(pSbTree, geolocid, pLevel, cList);

                if (childCount > 0)
                {
                    pSbTree.Append(strPrefix + "</ul>");
                    pSbTree.AppendLine();
                }

                pSbTree.Append("</li>");

            }
            return cnt;
        }



        private static int FillGeoLocationStringRecursive_Old(StringBuilder pSbTree, int pParentID, int pLevel, List<dcGeoLocation> cList)
        {
            int cnt = 0;
            string sTab = "\t";
            string sNewLine = "\r\n";


            pLevel++;

            List<dcGeoLocation> pList = cList.Where(m => m.GeoLocationIDParent == pParentID).ToList();

            pList = pList.OrderBy(m => m.GeoLocationSLNo).ThenBy(m=>m.GeoLocationName).ToList();
            //pList = pList.OrderBy(fieldID).ToList();

            foreach (dcGeoLocation geoLocation in pList)
            {
                cnt++;

                int geolocid = geoLocation.GeoLocationID;

                int childCount = cList.Where(m => m.GeoLocationIDParent == geolocid).Count();

                string strPrefix = PG.Core.Utility.Helper.RepeatString(sTab, pLevel);
                pSbTree.AppendLine();
                pSbTree.Append(strPrefix);


                string strKey = "geolocid" + geoLocation.GeoLocationID.ToString();

                string gCode = System.Web.HttpUtility.HtmlEncode(geoLocation.GeoLocationCode);
                string gName = System.Web.HttpUtility.HtmlEncode(geoLocation.GeoLocationName);
                //string gNameShort = System.Web.HttpUtility.HtmlEncode(grp.GLGroupNameShort);
                string gNameCode = System.Web.HttpUtility.HtmlEncode(geoLocation.GeoLocationName + " - " + geoLocation.GeoLocationCode);
                string gCodeName = System.Web.HttpUtility.HtmlEncode(geoLocation.GeoLocationCode + " - " + geoLocation.GeoLocationName);

                string gNameShow = gNameCode;

                StringBuilder sbG = new StringBuilder();
                sbG.Append("{{");
                sbG.Append("geolockey:'{0}'");
                sbG.Append(", geoloctypeid:{1}");
                sbG.Append(", geolocid:{2}");
                sbG.Append(", geoloccode:'{3}'");
                sbG.Append(", geolocname:'{4}'");
                sbG.Append(", geolocnameshow:'{5}'");
                sbG.Append(", geolocidparent:{6}");
                sbG.Append("}}");


                string strItem = string.Format(sbG.ToString()
                                , strKey
                                , geoLocation.GeoLocationTypeID
                                , geoLocation.GeoLocationID
                                , gCode
                                , gName
                                , gNameShow
                                , geoLocation.GeoLocationIDParent
                                );



   
                string strData = string.Format(" id=\"{0}\"  data = \"key: '{0}', icon : false, item:{1}\"", strKey, strItem);
                pSbTree.Append(string.Format("<li {0}>",strData));
                //pSbTree.Append(gName);
                pSbTree.Append(gNameShow);
                if (childCount > 0)
                {
                    string ulTag = sNewLine + strPrefix + "<ul>" + sNewLine;
                    pSbTree.Append(ulTag);
                    pSbTree.AppendLine();
                }

                //Recurisive call
                int totChild = FillGeoLocationStringRecursive(pSbTree, geolocid, pLevel, cList);

                if (childCount > 0)
                {
                    pSbTree.Append(strPrefix + "</ul>");
                    pSbTree.AppendLine();
                }

                pSbTree.Append("</li>");

            }
            return cnt;
        }





        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {
                IncludeResources();
                //List<DBClass.Accounting.dcAccGLGroup> cList = BLLibrary.AccountingBL.AccGLGroupBL.GetAccGLGroupList(true, false, DBClass.AccOption.AccOrderByEnum.SLNo, "");
                //litGLGroup.Text = BLLibrary.AccountingBL.AccGLGroupBL.GetAccGLGroupULTreeText(0, cList, false);
                //litGLGroup.Text = m_GroupTreeText; 
            }


        }

        protected void IncludeResources()
        {
            HtmlLink cssSource = new HtmlLink();
            
            //Page.ResolveUrl("")

            //cssSource.Href = Page.ClientScript.GetWebResourceUrl(this.GetType(), "styles.css");
            cssSource.Href = WebUtility.GetAbsoluteUrl("~/css/pg.ui.geolocationtree.css", this.Request);
            cssSource.Attributes["rel"] = "stylesheet";
            cssSource.Attributes["type"] = "text/css";
            Page.Header.Controls.Add(cssSource);
        }


    }
}