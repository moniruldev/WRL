using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PG.Web.Controls
{
   [ToolboxData("<{0}:PageableGridView runat=server></{0}:PageableGridView>")]
   public class PageableGridView : GridView, IPageableItemContainer
    {
        public PageableGridView() : base()
        {
            PagerSettings.Visible = false;
        }

        public event EventHandler<PageEventArgs> TotalRowCountAvailable;

        public int MaximumRows
        {
            get { return this.PageSize; }
        }

        public int StartRowIndex
        {
            get { return (this.PageSize * this.PageIndex); }
        }

        protected virtual void OnTotalRowCountAvailable(PageEventArgs e)
        {
            if (TotalRowCountAvailable != null)
                TotalRowCountAvailable(this, e);
        }

        public virtual void SetPageProperties(int startRowIndex, int maximumRows, bool databind)
        {
            if (databind)
            {
                PageSize = maximumRows;

                int newPageIndex = (startRowIndex / PageSize);

                if (PageIndex != newPageIndex)
                {
                    OnPageIndexChanging(new GridViewPageEventArgs(newPageIndex));

                    PageIndex = newPageIndex;

                    OnPageIndexChanged(EventArgs.Empty);
                }
            }

            RequiresDataBinding = databind;
        }

        //Gets row count from SqlDataSource and the like...
        private int _GetTotalRowsFromDataSource(IEnumerable dataSource)
        {
            DataSourceView view = this.GetData(); if (AllowPaging && view.CanPage && view.CanRetrieveTotalRowCount)
                return base.SelectArguments.TotalRowCount;
            else
                return (PageIndex * PageSize) + _GetSourceCount(dataSource);
        }

        //Gets the row count from a manually bound source or from a source in viewstate
        private int _GetSourceCount(IEnumerable dataSource)
        {
            ICollection source = dataSource as ICollection;

            return source != null ?
                 source.Count :
                 (from x in dataSource.OfType<object>() select 1).Sum();
        }

        protected override int CreateChildControls(IEnumerable dataSource, bool dataBinding)
        {
            int baseResult = base.CreateChildControls(dataSource, dataBinding);

            if (dataSource != null)
            {
                int dataSourceCount = (IsBoundUsingDataSourceID && dataBinding) ?
                    _GetTotalRowsFromDataSource(dataSource) :
                    _GetSourceCount(dataSource);
                OnTotalRowCountAvailable(new PageEventArgs(StartRowIndex, MaximumRows, dataSourceCount));
            }

            return baseResult;
        }
    }


   [ToolboxData("<{0}:MyTextBox runat=server></{0}:MyTextBox>")]
   public class MyTextBox : TextBox
   {
   }
}
