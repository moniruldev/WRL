
using PG.Core.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using PG.DBClass.InventoryDC;
using PG.BLLibrary.InventoryBL;

namespace PG.Web.Controls
{
    public partial class ItemGroupTree : System.Web.UI.UserControl
    {
        //private string m_GroupTreeText = string.Empty;

        public string GroupTreeText
        {
            get { return litItemGroup.Text; }
            set
            {
                litItemGroup.Text = value;
            }
        }

        public void SetItemGroupTreeText()
        {
            //SetItemGroupTreeText(INV_ITEM_GROUPBL.GetChildItemCount(INV_ITEM_GROUPBL.GetItemGroupList()));
            SetItemGroupTreeText(INV_ITEM_GROUPBL.GetItemGroupList());
        }

        public void SetItemGroupTreeText(List<dcINV_ITEM_GROUP> itemGroupList)
        {
            if (itemGroupList == null)
            {
                itemGroupList = new List<dcINV_ITEM_GROUP>();
            }
            string divClassName = "itemgroup_div";
            string treeClassName = "itemgroup_tree";
            StringBuilder sb = new StringBuilder();
            //string sTab = "\t";
            //string sNewLine = "\r\n";

            int grpLevel = -1;

            sb.Append("<div class='" + divClassName + "'>");
            sb.AppendLine();
            sb.Append("<ul class='" + treeClassName + "'>");
            sb.AppendLine();


            FillItemGroupStringRecursive(sb, 0, grpLevel, itemGroupList);


            sb.AppendLine();
            sb.Append("</ul>");
            sb.AppendLine();
            sb.Append("</div>");

            litItemGroup.Text = sb.ToString();

        }

        private static int FillItemGroupStringRecursive(StringBuilder pSbTree, int pParentID, int pLevel, List<dcINV_ITEM_GROUP> cList)
        {
            int cnt = 0;
            string sTab = "\t";
            string sNewLine = "\r\n";


            pLevel++;

            List<dcINV_ITEM_GROUP> pList = cList.Where(m => m.ITEM_GROUP_ID_PARENT == pParentID).ToList();
            //List<dcINV_ITEM_GROUP> pList = cList.ToList();//cList.Where(m => m.ITEM_GROUP_ID_PARENT == pParentID).ToList();


            pList = pList.OrderBy(m => m.ITEM_GROUP_NAME).ToList();
            //pList = pList.OrderBy(m => m.GLGroupSLNo).ThenBy(m => m.GLGroupName).ToList();
            //pList = pList.OrderBy(fieldID).ToList();

            foreach (dcINV_ITEM_GROUP grp in pList)
            {
                cnt++;

                int gID = grp.ITEM_GROUP_ID;

                int childCount = cList.Where(m => m.ITEM_GROUP_ID_PARENT == gID).Count();

                string strPrefix = PG.Core.Utility.Helper.RepeatString(sTab, pLevel);
                pSbTree.AppendLine();
                pSbTree.Append(strPrefix);


                string strKey = "grpid" + grp.ITEM_GROUP_ID.ToString();

                string gCode = System.Web.HttpUtility.HtmlEncode(grp.ITEM_GROUP_CODE);
                string gName = System.Web.HttpUtility.HtmlEncode(grp.ITEM_GROUP_NAME);
                //string gNameShort = System.Web.HttpUtility.HtmlEncode(grp.GLGroupNameShort);
                string gNameCode = System.Web.HttpUtility.HtmlEncode(grp.ITEM_GROUP_NAME + " - " + grp.ITEM_GROUP_CODE);
                string gCodeName = System.Web.HttpUtility.HtmlEncode(grp.ITEM_GROUP_CODE + " - " + grp.ITEM_GROUP_NAME);

                //string gNameCodeSN = System.Web.HttpUtility.HtmlEncode(grp.GLGroupName + " (" + grp.GLGroupNameShort + ") - " + grp.GLGroupCode);
                string gNameCodeSN = System.Web.HttpUtility.HtmlEncode(grp.ITEM_GROUP_NAME + " - " + grp.ITEM_GROUP_CODE);

                
                string gNameCodeItemCount = System.Web.HttpUtility.HtmlEncode(grp.ITEM_GROUP_NAME + " - " + grp.ITEM_GROUP_CODE);
                if (grp.CHILD_ITEM_COUNT > 0)
                {
                    gNameCodeItemCount = System.Web.HttpUtility.HtmlEncode(gNameCodeItemCount + ", " + grp.CHILD_ITEM_COUNT + " item(s)");
                }


                string gNameShow = gNameCodeSN;
                //string gNameShow = gNameCodeItemCount;


                StringBuilder sbG = new StringBuilder();
                sbG.Append("{{");
                sbG.Append("itemgroupkey:'{0}'");
                sbG.Append(", itemgroupid:{1}");
                sbG.Append(", itemgroupcode:'{2}'");
                sbG.Append(", itemgroupname:'{3}'");
                sbG.Append(", itemgroupnameshow:'{4}'");
                sbG.Append(", itemgroupidparent:{5}");
                sbG.Append("}}");


                string strItem = string.Format(sbG.ToString()
                                , strKey
                                , grp.ITEM_GROUP_ID
                                , gCode
                                , gName
                                , gNameShow
                                , grp.ITEM_GROUP_ID_PARENT
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
                int totChild = FillItemGroupStringRecursive(pSbTree, gID, pLevel, cList);

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
            cssSource.Href = WebUtility.GetAbsoluteUrl("~/css/pg.ui.itemgrouptree.css", this.Request);
            cssSource.Attributes["rel"] = "stylesheet";
            cssSource.Attributes["type"] = "text/css";
            Page.Header.Controls.Add(cssSource);
        }

    }
}