using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using PG.Core.Extentions;

namespace PG.Web.Controls
{
    public partial class GroupTree : System.Web.UI.UserControl
    {
        //private string m_GroupTreeText = string.Empty;

        public string GroupTreeText
        {
            get { return litGLGroup.Text; }
            set { 
                litGLGroup.Text = value;
            }
        }

        public void SetGroupTreeText<T>(List<T> ObjList, string fieldID, string fieldName, string fieldParent, bool pCheckBox) where T : class
        {
            SetGroupTreeText<T>(ObjList, fieldID, fieldName, fieldParent, pCheckBox, "group_tree");
        }

        public void SetGroupTreeText<T>(List<T> ObjList, string fieldID, string fieldName, string fieldParent, bool pCheckBox, string treeClassName) where T : class
        {
            //List<T> pList = ObjList.Where(fieldParent + "=0");
                                     //where c.AccGLGroupIDParent == pParentID
                                     //                      orderby c.AccGLGroupSLNo, c.AccGLGroupName
                                     //                      select c).ToList();

            List<T> pList = ObjList.Where(m => Convert.ToInt32(typeof(T).GetProperty(fieldParent).GetValue(m, null)) == 0).ToList();

            StringBuilder sb = new StringBuilder();
            //sb.Append("<ul id='" + treeID + "' class='glgroup_tree'>");

            //string attrTabIndex = "tabindex=\"0\"";


            sb.Append("<ul class='" + treeClassName + "'>");
            FillGroupStringRecursive<T>(sb, 0, -1, pCheckBox, ObjList, fieldID, fieldName, fieldParent);
            sb.Append("</ul>");

            litGLGroup.Text = sb.ToString();

        }

        private static int FillGroupStringRecursive<T>(StringBuilder pSbTree, int pParentID, int pLevel, bool pCheckBox, List<T> cList, string fieldID, string fieldName, string fieldParent) where T : class
        {
            int cnt = 0;
            string sTab = "\t";
            string sNewLine = "\r\n";


            pLevel++;

            //List<DBClass.Accounting.dcAccGLGroup> pList = (from c in cList
            //                                               where c.AccGLGroupIDParent == pParentID
            //                                               orderby c.AccGLGroupSLNo, c.AccGLGroupName
            //                                               select c).ToList();

            List<T> pList = cList.Where(m => Convert.ToInt32(typeof(T).GetProperty(fieldParent).GetValue(m, null)) == pParentID).ToList();

            //pList = pList.OrderBy(m => Convert.ToString(typeof(T).GetProperty(fieldName).GetValue(m, null))).ToList();
            //pList = pList.OrderBy(m => Convert.ToString(typeof(T).GetProperty(fieldName).GetValue(m, null))).ToList();

            pList = pList.OrderBy(m => typeof(T).GetProperty(fieldName)).ToList();
            //pList = pList.OrderBy(fieldID).ToList();

            foreach (T grp in pList)
            {
                cnt++;
                int gID = Convert.ToInt32(typeof(T).GetProperty(fieldID).GetValue(grp, null));
                string gName = Convert.ToString(typeof(T).GetProperty(fieldName).GetValue(grp, null));

                //int childCount = (from c in cList
                //                  where c.AccGLGroupIDParent == grp.AccGLGroupID
                //                  orderby c.AccGLGroupSLNo, c.AccGLGroupName
                //                  select c).Count();

                int childCount = cList.Where(m => Convert.ToInt32(typeof(T).GetProperty(fieldID).GetValue(grp, null)) == gID).Count();


                //string jsScript = "javascript:onGLGroupSelect(" + grp.AccGLGroupID.ToString() + ");";
                string strPrefix = PG.Core.Utility.Helper.RepeatString(sTab, pLevel);
                pSbTree.AppendLine();
                pSbTree.Append(strPrefix);

                //string jsClick = "onGLGroupSelect(" + grp.AccGLGroupID.ToString() + ");";
                string attrGID = "gid=\"" + gID.ToString() + "\"";
                string attrGName = "gname=\"" + System.Web.HttpUtility.HtmlEncode(gName) + "\"";

                string attrChildCount = "childcount=\"" + childCount.ToString() + "\"";


                string attrNodeClass = "class=\"group_node\"";
                string attrNodeSpanClass = "class=\"group_nodeSpan\"";

                //data="key: '3', isFolder: true"
                //with checkbox
                //pSbTree.Append("<li " + attrLiID + " " + attrData + "><div style=\"height:20px;width:200px\"> <input type=\"checkbox\" /><span class=\"tree_Node\" " + attrID + " " + attrChildCount + " onclick= " + jsClick + " >").Append(grp.AccGLGroupName).Append("</span></div>");

                if (pCheckBox)
                {
                    pSbTree.Append("<li " + attrGID + " " + attrGName + " " + attrChildCount + " " + attrNodeClass + ">");
                    pSbTree.Append("<div>");
                    pSbTree.Append("<input type=\"checkbox\" class=\"group_nodeCheckBox\"/>");
                    pSbTree.Append("<span " + attrNodeSpanClass + ">").Append(gName).Append("</span>");
                    pSbTree.Append("</div>");
                }
                else
                {
                    pSbTree.Append("<li " + attrGID + " " + attrGName + " " + attrChildCount + " " + attrNodeClass + ">");
                    pSbTree.Append("<span " + attrNodeSpanClass + ">").Append(gName).Append("</span>");
                }


                //pSbTree.Append("<span " + attrGID + " " + attrNodeSpanClass + ">").Append(grp.AccGLGroupName).Append("</span>");

                //remember the positon for insert <ul> tab if it has child node
                int ulPos = pSbTree.Length;

                //Recurisive call
                int totChild = FillGroupStringRecursive<T>(pSbTree, gID, pLevel, pCheckBox, cList, fieldID, fieldName, fieldParent);
                if (totChild > 0)
                {
                    string ulTag = sNewLine + strPrefix + "<ul>" + sNewLine;
                    pSbTree.Insert(ulPos, ulTag);
                    pSbTree.AppendLine();
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
                //List<DBClass.Accounting.dcAccGLGroup> cList = BLLibrary.AccountingBL.AccGLGroupBL.GetAccGLGroupList(true, false, DBClass.AccOption.AccOrderByEnum.SLNo, "");
                //litGLGroup.Text = BLLibrary.AccountingBL.AccGLGroupBL.GetAccGLGroupULTreeText(0, cList, false);
                //litGLGroup.Text = m_GroupTreeText; 
            }


        }

    }
}