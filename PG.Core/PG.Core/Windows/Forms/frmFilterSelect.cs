using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PG.Core.DBFilters;

namespace PG.Core.Windows.Forms
{
    public partial class frmFilterSelect : Form
    {
        public bool IsMultiSelect = false;
        public int DisplayColumnCount = 1;
        public List<string> DisplayColumnNames = new List<string>();
        public List<int> DisplayColumnWidths = new List<int>();
        public List<DBFilterPresetValue> ListValues = null;
        public string SelectedValuesText = string.Empty;
        public List<string> SelectedValues = new List<string>();

        public frmFilterSelect()
        {
            InitializeComponent();
        }

        private void frmSelect_Load(object sender, EventArgs e)
        {

        }


        public void SetListColumns()
        {
            this.lvwList.Columns.Clear();
            if (DisplayColumnCount > 0)
            {
                ColumnHeader c1 = new ColumnHeader();
                c1.Name = "Col01";
                c1.Text = this.DisplayColumnNames.Count > 0 ? this.DisplayColumnNames[0] : "Column 1";
                c1.Width = 150;
                if (this.DisplayColumnWidths.Count > 0)
                {
                    c1.Width = DisplayColumnWidths[0];
                }

                this.lvwList.Columns.Add(c1);
            }
            if (DisplayColumnCount > 1)
            {
                ColumnHeader c2 = new ColumnHeader();
                c2.Name = "Col02";
                c2.Text = this.DisplayColumnNames.Count > 1 ? this.DisplayColumnNames[1] : "Column 2";
                c2.Width = 150;
                if (this.DisplayColumnWidths.Count > 1)
                {
                    c2.Width = DisplayColumnWidths[1];
                }
                this.lvwList.Columns.Add(c2);
            }
            if (DisplayColumnCount > 2)
            {
                ColumnHeader c3 = new ColumnHeader();
                c3.Name = "Col03";
                c3.Text = this.DisplayColumnNames.Count > 1 ? this.DisplayColumnNames[2] : "Column 3";
                c3.Width = 150;
                if (this.DisplayColumnWidths.Count > 2)
                {
                    c3.Width = DisplayColumnWidths[2];
                }
                this.lvwList.Columns.Add(c3);
            }

        }

        public void LoadTask()
        {
            SetListColumns();
            this.lvwList.Items.Clear();

            List<string> selValues = this.SelectedValuesText.Split(',').ToList();
            selValues = selValues.ConvertAll(d => d.ToLower().Trim());
            
            if (ListValues != null)
            {
                foreach (DBFilterPresetValue item in ListValues)
                {
                    string disp1 = this.DisplayColumnNames.Count > 0 ? item.Display : item.Value;
                    ListViewItem lsItem = new ListViewItem(disp1);
                    lsItem.Tag = item.Value;

                    if (selValues.Contains(item.Display.ToLower().Trim()))
                    {
                        lsItem.Checked = true;
                    }

                    if (this.DisplayColumnCount > 1)
                    {
                        lsItem.SubItems.Add(item.Display2);
                    }


                    lvwList.Items.Add(lsItem);
                }
            }
        }

        private void chkSelectALL_CheckedChanged(object sender, EventArgs e)
        {
            CheckItems(chkSelectALL.Checked);
        }
        private void CheckItems(bool bCheck)
        {
            if (this.lvwList.CheckBoxes)
            {
                foreach (ListViewItem lsItem in this.lvwList.Items)
                {
                    lsItem.Checked = bCheck;
                }
            }
        }
        private void SetSelectedValues()
        {
            string strComma = string.Empty;
            SelectedValuesText = string.Empty;
            foreach (ListViewItem lstItem in this.lvwList.Items)
            {
                if (lstItem.Checked)
                {
                    if (lstItem.Tag!=null)
                    {
                        SelectedValues.Add(lstItem.Tag.ToString());
                        SelectedValuesText += strComma + lstItem.Text;
                        strComma = ",";
                    }
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SetSelectedValues();
        }

        private void chkSelectALL_CheckedChanged_1(object sender, EventArgs e)
        {

        }



    }
}
