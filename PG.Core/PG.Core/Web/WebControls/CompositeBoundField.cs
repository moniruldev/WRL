using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace PG.Core.Web.WebControls
{
    public class CompositeBoundField : BoundField
    {
        //uses
        //<%@ Register assembly="ZCore" namespace="ZCore.Web.WebControls" tagprefix="cc1" %>
        //<cc1:CompositeBoundField HeaderText="City" DataField="Child.ChildName" />

        protected override object GetValue(Control controlContainer)
        {
            object item = DataBinder.GetDataItem(controlContainer);
            return DataBinder.Eval(item, this.DataField);
        }
    }
}
