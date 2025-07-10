using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PG.Web
{
    public partial class BageMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ///ResolveClientUrl: this function needed for dynamic url binding for child page in different folder

            //for this an error will occur if any script code block has any code in header section
            //error:The Controls collection cannot be modified because the control contains code blocks (i.e. <% ... %>).
            //solution1:  user %# insteed of %= , i.e: src="<%# ResolveClientUrl("~/javascript/jquery-latest.min.js") %>"
            //           and in  page load event add: Page.Header.DataBind();  -- better in master page
            //solution2: move any script code from head tag to body tag
            //solution3: remover runat=server form haad tag.
            this.form1.DefaultButton = btnSubmit.UniqueID;

            Page.Header.DataBind(); 
        }
    }
}