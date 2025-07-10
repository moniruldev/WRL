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
    public partial class frmFilter : Form
    {
        public List<DBFilterField> DBFilterFields = new List<DBFilterField>();
        public List<DBFilter> DBFilters = new List<DBFilter>();

        public frmFilter()
        {
            InitializeComponent();
        }

        private void frmFilter_Load(object sender, EventArgs e)
        {
            this.Width = 430;

        }

        private void LoadList()
        {
            //FillOperator();
            //this.cboOperator.SelectedIndex = 0; //equal to
            FillPreset();
            this.cboPreset.SelectedValue = 3;  //this month
        }

        public void LoadData()
        {
            LoadList();
            FillFilterFields();
            FillFilter();
        }

        private void FillFilterFields()
        {
            //this.lvwFilter.Items.Clear();
            this.listField.DisplayMember = "Name";
            this.listField.DataSource = this.DBFilterFields;

            if (this.listField.Items.Count > 0)
            {
                this.listField.SelectedIndex = 0;
                SetFieldOptions();
            }

        }
        public void FillFilter()
        {
            this.lvwFilter.Items.Clear();
            foreach (DBFilter filter in this.DBFilters)
            {
                AddFilterToList(filter);
            }
        }
        private void AddFilterToList(DBFilter filter)
        {
            if (this.DBFilters == null)
            {
                this.DBFilters = new List<DBFilter>();
            }

            string name = filter.Name;
            string not = filter.NegateExpression ? "NOT " : string.Empty;
            if (this.lvwFilter.Items.Count != 0)
            {
                string combine = filter.CombineType == DBFilterCombineTypeEnum.AND ? "AND " : "OR ";
                name = combine + not + filter.Name;
            }
            else
            {
                name = not + filter.Name;
            }
            ListViewItem lsItem = new ListViewItem(name);
            lsItem.Tag = this.DBFilters.IndexOf(filter);
            lsItem.SubItems.Add(filter.FilterText);
            lvwFilter.Items.Add(lsItem);
        }
        private void UpdateFilterToList(int index, DBFilter filter)
        {
            string name = filter.Name;
            string not = filter.NegateExpression ? "NOT " : string.Empty;
            if (index > 0)
            {
                string combine = filter.CombineType == DBFilterCombineTypeEnum.AND ? "AND " : "OR ";
                name = combine + not + filter.Name;
            }
            else
            {
                name = not + filter.Name;
            }
            ListViewItem lsItem = this.lvwFilter.Items[index];
            lsItem.Text = name;
            lsItem.Tag = this.DBFilters.IndexOf(filter);
            lsItem.SubItems[1].Text = filter.FilterText;
        }


        public void FillOperator(DBFilterField filterfield)
        {
            Dictionary<int, string> lstOperator = new Dictionary<int, string>();

            if (filterfield.IsPreValue)
            {
                lstOperator.Add(8, "IN (Multi Value-Comma Separated)");
            }
            else
            {
                switch (filterfield.FieldDataType)
                {
                    case DBFilterDataTypeEnum.String:
                        lstOperator.Add(1, "EqualTo (=)");
                        lstOperator.Add(2, "Not EqualTo (<>)");
                        lstOperator.Add(8, "IN (Multi Value-Comma Separated)");
                        lstOperator.Add(9, "Contains (*Value*)");
                        lstOperator.Add(10, "StartWith (Value*)");
                        lstOperator.Add(11, "EndsWith (*Value)");
                        break;
                    case DBFilterDataTypeEnum.Integer:
                    case DBFilterDataTypeEnum.Decimal:
                        lstOperator.Add(1, "EqualTo (=)");
                        lstOperator.Add(2, "Not EqualTo (<>)");
                        lstOperator.Add(3, "Greater Than (>)");
                        lstOperator.Add(4, "Less Than (<)");
                        lstOperator.Add(5, "Greater Than or Equal To (>=)");
                        lstOperator.Add(6, "Less Than or Equal To (<=)");
                        lstOperator.Add(7, "Range (Between Value1 And Value2)");
                        lstOperator.Add(8, "IN (Multi Value-Comma Separated)");
                        break;
                    case DBFilterDataTypeEnum.Boolean:
                        lstOperator.Add(1, "EqualTo (=)");
                        break;
                    case DBFilterDataTypeEnum.Date:
                    case DBFilterDataTypeEnum.DateTime:
                    case DBFilterDataTypeEnum.Time:
                        lstOperator.Add(1, "EqualTo (=)");
                        lstOperator.Add(2, "Not EqualTo (<>)");
                        lstOperator.Add(3, "Greater Than (>)");
                        lstOperator.Add(4, "Less Than (<)");
                        lstOperator.Add(5, "Greater Than or Equal To (>=)");
                        lstOperator.Add(6, "Less Than or Equal To (<=)");
                        lstOperator.Add(7, "Range (Between Value1 And Value2)");
                        break;
                }
            }


            //lstOperator.Add(1,"EqualTo (=)");
            //lstOperator.Add(2, "Not EqualTo (<>)");
            //lstOperator.Add(3, "Greater Than (>)");
            //lstOperator.Add(4, "Less Than (<)");
            //lstOperator.Add(5, "Greater Than or Equal To (>=)");
            //lstOperator.Add(6, "Less Than or Equal To (<=)");
            //lstOperator.Add(7, "Range (Between Value1 And Value2)");
            //lstOperator.Add(8, "IN (Multi Value-Comma Separated)");
            //lstOperator.Add(9, "Contains (*Value*)");
            //lstOperator.Add(10, "StartWith (Value*)");
            //lstOperator.Add(11, "EndsWith (*Value)");

            this.cboOperator.ValueMember = "Key";
            this.cboOperator.DisplayMember = "Value";
            this.cboOperator.DataSource = lstOperator.ToList();
        }

        public void FillPreset()
        {
            Dictionary<int, string> lstPreset = new Dictionary<int, string>();

            lstPreset.Add(1, "Today");
            lstPreset.Add(2, "Yesterday");
            lstPreset.Add(3, "This Month");
            lstPreset.Add(4, "Month To Date");
            lstPreset.Add(5, "Last Month");
            lstPreset.Add(6, "Next Month");
            lstPreset.Add(7, "This Year");
            lstPreset.Add(8, "Year To Date");
            lstPreset.Add(9, "Last Year");
            lstPreset.Add(10, "Custom");


            this.cboPreset.ValueMember = "Key";
            this.cboPreset.DisplayMember = "Value";
            this.cboPreset.DataSource = lstPreset.ToList();
        }

        private void SetFieldOptions()
        {
            DBFilterField ff = (DBFilterField)this.listField.SelectedItem;
            FillOperator(ff);
            if (ff.FieldDataType == DBFilterDataTypeEnum.Date |
                ff.FieldDataType == DBFilterDataTypeEnum.DateTime |
                ff.FieldDataType == DBFilterDataTypeEnum.Time)
            {
                cboOperator.SelectedValue = 7;
            }
            else
            {
                cboOperator.SelectedIndex = 0;
            }
        }


        private void SetOptions()
        {
            if (this.listField.SelectedItems.Count == 0)
            {
                return;
            }

            int pLeft = 174;
            int pTop = 79;

            pnlValue.Left = pLeft;
            pnlValue.Top = pTop;

            pnlList.Left = pLeft;
            pnlList.Top = pTop;

            pnlDate.Left = pLeft;
            pnlDate.Top = pTop;

            pnlBoolean.Left = pLeft;
            pnlBoolean.Top = pTop;

            int cOperator = 0;
            if (this.cboOperator.SelectedIndex != -1)
            {
                cOperator = (int)this.cboOperator.SelectedValue;
            }
            DBFilterField ff = (DBFilterField)this.listField.SelectedItem;

            //cboOperator.Enabled = ff.DataType == DbFilterField.DataTypeEnum.Boolean ? false : true;
            this.pnlBoolean.Visible = false;
            this.pnlDate.Visible = false;
            this.pnlValue.Visible = false;
            this.lblPreset.Visible = false;
            this.cboPreset.Visible = false;
            this.pnlList.Visible = false;

            this.txtValues.Text = string.Empty;
            this.txtValue1.Text = string.Empty;
            this.txtValue2.Text = string.Empty;


            switch (ff.FieldDataType)
            {
                case DBFilterDataTypeEnum.Boolean:
                    this.pnlBoolean.Visible = true;
                    break;
                case DBFilterDataTypeEnum.String:
                case DBFilterDataTypeEnum.Integer:
                case DBFilterDataTypeEnum.Decimal:
                    if (ff.IsPreValue)
                    {

                        this.pnlList.Visible = true;
                    }
                    else
                    {
                        if (cOperator == 7) // range
                        {
                            this.pnlValue.Visible = true;
                            this.lblValue1.Text = "Value1";
                            this.lblValue2.Text = "Value2";
                            this.lblValue2.Visible = true;
                            this.txtValue2.Visible = true;
                        }
                        else if (cOperator == 8) //IN
                        {
                            this.pnlValue.Visible = true;
                            this.lblValue1.Text = "Values";
                            this.lblValue2.Visible = false;
                            this.txtValue2.Visible = false;
                        }
                        else
                        {
                            this.pnlValue.Visible = true;
                            this.lblValue1.Text = "Value";
                            this.lblValue2.Visible = false;
                            this.txtValue2.Visible = false;
                        }
                    }
                    break;
                case DBFilterDataTypeEnum.DateTime:
                case DBFilterDataTypeEnum.Date:
                case DBFilterDataTypeEnum.Time:
                    if (cOperator == 7) // range
                    {
                        this.pnlDate.Visible = true;
                        this.lblDate1.Text = "Date1";
                        this.lblDate2.Text = "Date2";
                        this.lblDate2.Visible = true;
                        this.dtpDate2.Visible = true;
                        this.lblPreset.Visible = true;
                        this.cboPreset.Visible = true;
                    }
                    else
                    {
                        this.pnlDate.Visible = true;
                        this.lblDate1.Text = "Date1";
                        this.lblDate2.Text = "Date2";
                        this.lblDate2.Visible = false;
                        this.dtpDate2.Visible = false;
                    }

                    break;
            }
        }

        private DBFilterCompareTypeEnum GetCompareType(int pOperator)
        {
            DBFilterCompareTypeEnum comType = DBFilterCompareTypeEnum.EqualTo;
            switch (pOperator)
            {
                case 1:
                    comType = DBFilterCompareTypeEnum.EqualTo;
                    break;
                case 2:
                    comType = DBFilterCompareTypeEnum.NotEqualTo;
                    break;
                case 3:
                    comType = DBFilterCompareTypeEnum.GreaterThan;
                    break;
                case 4:
                    comType = DBFilterCompareTypeEnum.LessThan;
                    break;
                case 5:
                    comType = DBFilterCompareTypeEnum.GreaterThanEqualTo;
                    break;
                case 6:
                    comType = DBFilterCompareTypeEnum.LessThanEqualTo;
                    break;
                case 7:
                    comType = DBFilterCompareTypeEnum.Range;
                    break;
                case 8:
                    comType = DBFilterCompareTypeEnum.IN;
                    break;
                case 9:
                    comType = DBFilterCompareTypeEnum.StartsWith;
                    break;
                case 10:
                    comType = DBFilterCompareTypeEnum.EndsWith;
                    break;
                case 11:
                    comType = DBFilterCompareTypeEnum.Contains;
                    break;
                default:
                    comType = DBFilterCompareTypeEnum.EqualTo;
                    break;
            }
            return comType;
        }

        private void lblVal1_Click(object sender, EventArgs e)
        {

        }

        private void txtValue2_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboOperator_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetOptions();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void listField_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetFieldOptions();
        }

        private void grpValue_Enter(object sender, EventArgs e)
        {

        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.listField.SelectedItems.Count == 0)
            {
                return;
            }
            DBFilter filter = new DBFilter();

            UpdateFilter(filter);

            this.DBFilters.Add(filter);
            AddFilterToList(filter);
            this.lvwFilter.Items[lvwFilter.Items.Count - 1].Selected = true;
        }
        private void UpdateFilter(DBFilter filter)
        {
            int cOperator = (int)this.cboOperator.SelectedValue;
            DBFilterField ff = (DBFilterField)this.listField.SelectedItem;

            filter.Name = ff.Name;
            filter.FieldName = ff.FieldName;
            filter.TableName = ff.TableName;
            filter.CombineType = rbtnAnd.Checked ? DBFilterCombineTypeEnum.AND : DBFilterCombineTypeEnum.OR;
            filter.NegateExpression = chkNOT.Checked;
            filter.FormatString = ff.FormatString;
            filter.IsPreValue = ff.IsPreValue;
            filter.CompareType = GetCompareType(cOperator);
            filter.DataType = (DBFilterDataTypeEnum)ff.FieldDataType;
            if (ff.FieldDataType == DBFilterDataTypeEnum.Boolean)
            {
                filter.Values.Add(rbtnTure.Checked);
                filter.CompareType = DBFilterCompareTypeEnum.EqualTo;
            }
            else
            {
                filter.Values = CreateValue(ff);
            }
            if (filter.IsPreValue)
            {
                string strText = string.Empty;
                string strComma = string.Empty;
                foreach (object obj in filter.Values)
                {
                    var query = ff.DBFilterPresetValues.Find(c => c.Value.ToLower() == obj.ToString().ToLower());
                    if (query != null)
                    {
                        strText += strComma + query.Display;
                        strComma = ", ";
                    }
                }
                filter.FilterTextPreValue = strText;
            }
            filter.FilterText = DBFilterManager.CreateFilterText(filter);
        }


        private List<object> CreateValue(DBFilterField filterfield)
        {
            List<object> values = new List<object>();
            int cOperator = (int)this.cboOperator.SelectedValue;
            DBFilterCompareTypeEnum compType = GetCompareType(cOperator);
            DBFilterDataTypeEnum type = filterfield.FieldDataType;

            string[] val1 = txtValue1.Text.Split(',');
            string[] val2 = txtValue2.Text.Split(',');

            if (compType == DBFilterCompareTypeEnum.Range)
            {
                if (type == DBFilterDataTypeEnum.DateTime |
                    type == DBFilterDataTypeEnum.Date |
                    type == DBFilterDataTypeEnum.Time)
                {
                    values.Add(dtpDate1.Value);
                    values.Add(dtpDate2.Value);
                }
                else
                {
                    values.Add(ConvertTextToTypeObject(val1[0], type));
                    values.Add(ConvertTextToTypeObject(val2[0], type));
                }
            }
            else
            {
                if (type == DBFilterDataTypeEnum.DateTime |
                     type == DBFilterDataTypeEnum.Date |
                     type == DBFilterDataTypeEnum.Time)
                {
                    values.Add(dtpDate1.Value);
                }
                else
                {
                    if (compType == DBFilterCompareTypeEnum.IN)
                    {
                        if (filterfield.IsPreValue)
                        {
                            //TODO
                            string[] vals = txtValues.Text.Split(',');
                            foreach (string val in vals)
                            {
                                var query = filterfield.DBFilterPresetValues.Find(c => c.Display.ToLower() == val.ToLower());
                                if (query != null)
                                {
                                    values.Add(ConvertTextToTypeObject(query.Value, type));
                                }
                            }
                        }
                        else
                        {
                            foreach (string val in val1)
                            {
                                values.Add(ConvertTextToTypeObject(val, type));
                            }
                        }
                    }
                    else
                    {
                        values.Add(ConvertTextToTypeObject(val1[0], type));
                    }


                }


            }
            return values;
        }
        private object ConvertTextToTypeObject(string data, DBFilterDataTypeEnum type)
        {
            object obj = null;
            switch (type)
            {
                case DBFilterDataTypeEnum.String:
                    obj = Convert.ToString(data);
                    break;
                case DBFilterDataTypeEnum.Integer:
                case DBFilterDataTypeEnum.Decimal:
                    if (data == string.Empty)
                    {
                        obj = 0;
                    }
                    else
                    {
                        decimal i;
                        if (decimal.TryParse(data, out i))
                        {
                            if (type == DBFilterDataTypeEnum.Integer)
                            {
                                obj = i;
                            }
                            else
                            {
                                obj = Convert.ToInt32(i);
                            }
                        }
                        else
                        {
                            obj = 0;
                        }
                    }
                    break;
                case DBFilterDataTypeEnum.Boolean:
                    obj = Convert.ToBoolean(data);
                    break;
                case DBFilterDataTypeEnum.DateTime:
                case DBFilterDataTypeEnum.Date:
                case DBFilterDataTypeEnum.Time:
                    if (data == string.Empty)
                    {
                        obj = null;
                    }
                    else
                    {
                        DateTime dt;
                        if (DateTime.TryParse(data, out dt))
                        {
                            obj = dt;
                        }
                        else
                        {
                            obj = null;
                        }

                    }
                    break;
                default:
                    obj = data;
                    break;
            }
            return obj;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.DBFilters.Clear();
            this.lvwFilter.Items.Clear();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            if (this.listField.SelectedItems.Count == 0)
            {
                return;
            }
            if (this.lvwFilter.SelectedItems.Count == 0)
            {
                return;
            }
            ListViewItem lstItem = this.lvwFilter.SelectedItems[0];
            DBFilter filter = this.DBFilters[Convert.ToInt32(lstItem.Tag)];

            UpdateFilter(filter);
            UpdateFilterToList(lstItem.Index, filter);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (this.lvwFilter.SelectedItems.Count == 0)
            {
                return;
            }
            ListViewItem lstItem = this.lvwFilter.SelectedItems[0];
            int index = lstItem.Index;
            DBFilter filter = this.DBFilters[Convert.ToInt32(lstItem.Tag)];
            this.DBFilters.Remove(filter);
            this.lvwFilter.Items.Remove(lstItem);

            FillFilter();

            if (index > this.lvwFilter.Items.Count - 1)
            {
                index = index - 1;
            }
            if (index >= 0)
            {
                this.lvwFilter.Items[index].Selected = true;
            }
        }

        private void btnValues_Click(object sender, EventArgs e)
        {
            if (this.listField.SelectedItems.Count == 0)
            {
                return;
            }

            frmFilterSelect form1 = new frmFilterSelect();
            DBFilterField ff = (DBFilterField)this.listField.SelectedItem;
            form1.Text = "Select " + ff.Name;
            form1.DisplayColumnCount = ff.PresetValueDisplayColumnCount;
            form1.DisplayColumnNames = ff.PresetValueDispalyColumnNames;
            form1.DisplayColumnWidths = ff.PresetValueDisplayColumnWidths;
            form1.Height = ff.PresetValueDisplayWindowHeight;
            form1.Width = ff.PresetValueDisplayWindowWidth;

            form1.ListValues = ff.DBFilterPresetValues;
            form1.SelectedValuesText = this.txtValues.Text;
            form1.LoadTask();
            if (form1.ShowDialog(this) == DialogResult.OK)
            {
                this.txtValues.Text = form1.SelectedValuesText;
            }
            form1.Close();
            form1 = null;
        }

        private void cboPreset_SelectedIndexChanged(object sender, EventArgs e)
        {
            int val = (int)cboPreset.SelectedValue;
            switch (val)
            {
                case 1: //today
                    this.dtpDate1.Value = DateTime.Today;
                    this.dtpDate2.Value = DateTime.Today;
                    break;
                case 2: //yesterday
                    this.dtpDate1.Value = DateTime.Today.AddDays(-1);
                    this.dtpDate2.Value = DateTime.Today.AddDays(-1);
                    break;
                case 3: //This month
                    this.dtpDate1.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                    this.dtpDate2.Value = this.dtpDate1.Value.AddMonths(1).AddDays(-1);
                    break;
            }

        }

        private void lvwFilter_Click(object sender, EventArgs e)
        {
            if (this.lvwFilter.SelectedItems.Count > 0)
            {
                int idx = Convert.ToInt32(lvwFilter.SelectedItems[0].Tag);
                DBFilter filter = this.DBFilters[idx];

                switch (filter.CompareType)
                {
                    case DBFilterCompareTypeEnum.IN:
                        this.txtValues.Text = filter.FilterTextPreValue;
                        break;
                }
            }
        }

        private void lvwFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
