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
using PG.DBClass.AccountingDC.GeneralLedgerDC;
using PG.BLLibrary.AccountingBL;
using PG.BLLibrary.AccountingBL.GeneralLedgerBL;

namespace PG.Web.Controls
{
    public partial class GLGroupTree : System.Web.UI.UserControl
    {
        //private string m_GroupTreeText = string.Empty;

        public string GroupTreeText
        {
            get { return litGLGroup.Text; }
            set { 
                litGLGroup.Text = value;
            }
        }

        public void SetGLGroupTreeText(int pCompanyID)
        {
            SetGLGroupTreeText(pCompanyID,  true);
        }

        public void SetGLGroupTreeText(int pCompanyID, bool includeGLClass)
        {
            SetGLGroupTreeText(GLGroupBL.GetGLGroupList(pCompanyID) , includeGLClass);
        }

        public void SetGLGroupTreeText(List<dcGLGroup> glGroupList)
        {
            SetGLGroupTreeText(glGroupList, true);
        }

        public void SetGLGroupTreeText(List<dcGLGroup> glGroupList, bool includeGLClass)
        {
            if (glGroupList == null)
            {
                glGroupList = new List<dcGLGroup>();
            }
            string divClassName = "glgroup_div";
            string treeClassName = "glgroup_tree";
            StringBuilder sb = new StringBuilder();
            //string sTab = "\t";
            //string sNewLine = "\r\n";

            int grpLevel = -1;

            sb.Append("<div class='" + divClassName + "'>");
            sb.AppendLine();
             sb.Append("<ul class='" + treeClassName + "'>");
            sb.AppendLine();
            if (includeGLClass)
            {
                List<dcGLClass> glClassList = GLClassBL.GetGLClassList();
                foreach (dcGLClass glClass in glClassList)
                {
                    grpLevel++;
                    //" data = "key: 'node5.1'" ";
                    string strKey = "gclid" + glClass.GLClassID.ToString();
                    string gCode = System.Web.HttpUtility.HtmlEncode(glClass.GLClassID);
                    string gName = System.Web.HttpUtility.HtmlEncode(glClass.GLClassName);
                    string gNameCode = System.Web.HttpUtility.HtmlEncode(glClass.GLClassName + " - " + glClass.GLClassCode);
                    string gNameName = System.Web.HttpUtility.HtmlEncode(glClass.GLClassCode + " - " + glClass.GLClassName);
                    string gNameShow = gNameCode;

                    string balanceType = glClass.BalanceType.ToString();
                    string isGrossProfit = "0";
                    string isBank = "0";
                    string isCash = "0";
                    string isInventory = "0";

                    StringBuilder sbG = new StringBuilder();
                    sbG.Append("{{");
                    sbG.Append("glgroupkey:'{0}'");
                    sbG.Append(", glclassid:{1}");
                    sbG.Append(", glgroupid:{2}");
                    sbG.Append(", glgroupcode:'{3}'");
                    sbG.Append(", glgroupname:'{4}'");
                    sbG.Append(", glgroupnameshow:'{5}'");
                    sbG.Append(", balancetype:{6}");
                    sbG.Append(", isgrossprofit:{7}");
                    sbG.Append(", glgroupclassid:{8}");
                    sbG.Append(", glgroupidparent:{9}");
                    sbG.Append(", iscash:{10}");
                    sbG.Append(", isbank:{11}");
                    sbG.Append(", isinventory:{12}");
                    sbG.Append("}}");


                    string strItem = string.Format(sbG.ToString()
                                    , strKey
                                    , glClass.GLClassID
                                    , 0
                                    , gCode
                                    , gName
                                    , gNameShow
                                    , balanceType
                                    , isGrossProfit
                                    , 0
                                    , 0
                                    , isCash
                                    , isBank
                                    , isInventory
                                    );


                    //string strItem = string.Format("{{glgroupkey:'{0}',glclassid:{1},glgroupid:{2}, glgroupcode:'{3}', glgroupname:'{4}', balancetype:{5},  isgrossprofit:{6}, glgroupclassid:{7},glgroupidparent:{8}}}"
                    //                                , strKey, glClass.GLClassID, 0, gCode, gName, balanceType, isGrossProfit, 0, 0);
                    
                    
                    string strData = string.Format(" id=\"{0}\"  data = \"key: '{0}', icon : false, item:{1}\"", strKey, strItem);

                    sb.Append(string.Format("<li {0}>",strData));
                    //sb.Append(glClass.GLClassName);
                    sb.Append(gNameShow);
                    List<dcGLGroup> glGroupsByClass = glGroupList.Where(c => c.GLClassID == glClass.GLClassID).ToList();
                    int grpCount = glGroupsByClass.Count;
                    if (grpCount > 0)
                    {
                        sb.AppendLine();
                        sb.Append("<ul>");
                        sb.AppendLine();
                    }
                    int totChild = FillGLGroupStringRecursive(sb, 0, grpLevel, glGroupsByClass);
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
                FillGLGroupStringRecursive(sb, 0, grpLevel, glGroupList);
            }
            sb.AppendLine();
            sb.Append("</ul>");
            sb.AppendLine();
            sb.Append("</div>");

            litGLGroup.Text = sb.ToString();

        }

        private static int FillGLGroupStringRecursive(StringBuilder pSbTree, int pParentID, int pLevel, List<dcGLGroup> cList)
        {
            int cnt = 0;
            string sTab = "\t";
            string sNewLine = "\r\n";


            pLevel++;

            List<dcGLGroup> pList = cList.Where(m => m.GLGroupIDParent == pParentID).ToList();

            pList = pList.OrderBy(m => m.GLGroupSLNo).ThenBy(m=>m.GLGroupName).ToList();
            //pList = pList.OrderBy(fieldID).ToList();

            foreach (dcGLGroup grp in pList)
            {
                cnt++;

                int gID = grp.GLGroupID;

                int childCount = cList.Where(m => m.GLGroupIDParent == gID).Count();

                string strPrefix = PG.Core.Utility.Helper.RepeatString(sTab, pLevel);
                pSbTree.AppendLine();
                pSbTree.Append(strPrefix);


                string strKey = "grpid" + grp.GLGroupID.ToString();

                string gCode = System.Web.HttpUtility.HtmlEncode(grp.GLGroupCode);
                string gName = System.Web.HttpUtility.HtmlEncode(grp.GLGroupName);
                string gNameShort = System.Web.HttpUtility.HtmlEncode(grp.GLGroupNameShort);
                string gNameCode = System.Web.HttpUtility.HtmlEncode(grp.GLGroupName + " - " + grp.GLGroupCode);
                string gCodeName = System.Web.HttpUtility.HtmlEncode(grp.GLGroupCode + " - " + grp.GLGroupName);

                string gNameCodeSN = System.Web.HttpUtility.HtmlEncode(grp.GLGroupName + " (" + grp.GLGroupNameShort + ") - " + grp.GLGroupCode);

                string gNameShow = gNameCodeSN;

                string balanceType = grp.BalanceType.ToString();
                string isGrossProfit = grp.IsGrossProfit ? "1" : "0";
                string isBank = grp.IsBank ? "1" : "0";
                string isCash = grp.IsCash ? "1" : "0";
                string isInventory = grp.IsInventory ? "1" : "0";

                StringBuilder sbG = new StringBuilder();
                sbG.Append("{{");
                sbG.Append("glgroupkey:'{0}'");
                sbG.Append(", glclassid:{1}");
                sbG.Append(", glgroupid:{2}");
                sbG.Append(", glgroupcode:'{3}'");
                sbG.Append(", glgroupname:'{4}'");
                sbG.Append(", glgroupnameshow:'{5}'");
                sbG.Append(", balancetype:{6}");
                sbG.Append(", isgrossprofit:{7}");
                sbG.Append(", glgroupclassid:{8}");
                sbG.Append(", glgroupidparent:{9}");
                sbG.Append(", iscash:{10}");
                sbG.Append(", isbank:{11}");
                sbG.Append(", isinventory:{12}");
                sbG.Append("}}");


                string strItem = string.Format(sbG.ToString()
                                , strKey
                                , grp.GLClassID
                                , grp.GLGroupID
                                , gCode
                                , gName
                                , gNameShow
                                , balanceType
                                , isGrossProfit
                                , grp.GLGroupClassID
                                , grp.GLGroupIDParent
                                , isCash
                                , isBank
                                , isInventory
                                );



                //string strItem = string.Format("{{glgroupkey:'{0}',glclassid:{1},glgroupid:{2},glgroupcode:'{3}', glgroupname:'{4}',balancetype:{5}, isgrossprofit:{6}, glgroupclassid:{7},glgroupidparent:{8}}}"
                //                                , strKey
                //                                , grp.GLClassID
                //                                , grp.GLGroupID
                //                                , gCode
                //                                , gName
                //                                , balanceType
                //                                , isGrossProfit
                //                                , grp.GLGroupClassID
                //                                , grp.GLGroupIDParent
                //                                );





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
                int totChild = FillGLGroupStringRecursive(pSbTree, gID, pLevel, cList);

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
            cssSource.Href = WebUtility.GetAbsoluteUrl("~/css/pg.ui.glgrouptree.css", this.Request);
            cssSource.Attributes["rel"] = "stylesheet";
            cssSource.Attributes["type"] = "text/css";
            Page.Header.Controls.Add(cssSource);
        }


    }
}