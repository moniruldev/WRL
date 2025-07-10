namespace PG.Core.Windows.Forms
{
    partial class frmFilter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listField = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblOperator = new System.Windows.Forms.Label();
            this.cboOperator = new System.Windows.Forms.ComboBox();
            this.lvwFilter = new System.Windows.Forms.ListView();
            this.Field = new System.Windows.Forms.ColumnHeader();
            this.Filter = new System.Windows.Forms.ColumnHeader();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnChange = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.cboPreset = new System.Windows.Forms.ComboBox();
            this.pnlValue = new System.Windows.Forms.Panel();
            this.txtValue2 = new System.Windows.Forms.TextBox();
            this.lblValue2 = new System.Windows.Forms.Label();
            this.txtValue1 = new System.Windows.Forms.TextBox();
            this.lblValue1 = new System.Windows.Forms.Label();
            this.pnlDate = new System.Windows.Forms.Panel();
            this.dtpDate2 = new System.Windows.Forms.DateTimePicker();
            this.dtpDate1 = new System.Windows.Forms.DateTimePicker();
            this.lblDate2 = new System.Windows.Forms.Label();
            this.lblDate1 = new System.Windows.Forms.Label();
            this.pnlBoolean = new System.Windows.Forms.Panel();
            this.rbtnFalse = new System.Windows.Forms.RadioButton();
            this.rbtnTure = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkNOT = new System.Windows.Forms.CheckBox();
            this.rbtnOR = new System.Windows.Forms.RadioButton();
            this.rbtnAnd = new System.Windows.Forms.RadioButton();
            this.lblPreset = new System.Windows.Forms.Label();
            this.pnlList = new System.Windows.Forms.Panel();
            this.btnValues = new System.Windows.Forms.Button();
            this.txtValues = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pnlValue.SuspendLayout();
            this.pnlDate.SuspendLayout();
            this.pnlBoolean.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnlList.SuspendLayout();
            this.SuspendLayout();
            // 
            // listField
            // 
            this.listField.FormattingEnabled = true;
            this.listField.Location = new System.Drawing.Point(15, 25);
            this.listField.Name = "listField";
            this.listField.Size = new System.Drawing.Size(153, 238);
            this.listField.TabIndex = 0;
            this.listField.SelectedIndexChanged += new System.EventHandler(this.listField_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Fields:";
            // 
            // lblOperator
            // 
            this.lblOperator.AutoSize = true;
            this.lblOperator.Location = new System.Drawing.Point(171, 9);
            this.lblOperator.Name = "lblOperator";
            this.lblOperator.Size = new System.Drawing.Size(48, 13);
            this.lblOperator.TabIndex = 2;
            this.lblOperator.Text = "Operator";
            // 
            // cboOperator
            // 
            this.cboOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOperator.FormattingEnabled = true;
            this.cboOperator.Location = new System.Drawing.Point(174, 25);
            this.cboOperator.MaxDropDownItems = 12;
            this.cboOperator.Name = "cboOperator";
            this.cboOperator.Size = new System.Drawing.Size(241, 21);
            this.cboOperator.TabIndex = 3;
            this.cboOperator.SelectedIndexChanged += new System.EventHandler(this.cboOperator_SelectedIndexChanged);
            // 
            // lvwFilter
            // 
            this.lvwFilter.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Field,
            this.Filter});
            this.lvwFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvwFilter.FullRowSelect = true;
            this.lvwFilter.GridLines = true;
            this.lvwFilter.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwFilter.HideSelection = false;
            this.lvwFilter.Location = new System.Drawing.Point(15, 282);
            this.lvwFilter.MultiSelect = false;
            this.lvwFilter.Name = "lvwFilter";
            this.lvwFilter.ShowItemToolTips = true;
            this.lvwFilter.Size = new System.Drawing.Size(403, 128);
            this.lvwFilter.TabIndex = 4;
            this.lvwFilter.UseCompatibleStateImageBehavior = false;
            this.lvwFilter.View = System.Windows.Forms.View.Details;
            this.lvwFilter.SelectedIndexChanged += new System.EventHandler(this.lvwFilter_SelectedIndexChanged);
            this.lvwFilter.Click += new System.EventHandler(this.lvwFilter_Click);
            // 
            // Field
            // 
            this.Field.Text = "Field";
            this.Field.Width = 160;
            // 
            // Filter
            // 
            this.Filter.Text = "Filter";
            this.Filter.Width = 200;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 266);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Current Filters:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(259, 240);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnChange
            // 
            this.btnChange.Location = new System.Drawing.Point(340, 240);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(75, 23);
            this.btnChange.TabIndex = 7;
            this.btnChange.Text = "Change";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(259, 416);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(340, 416);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(12, 416);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 10;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(93, 416);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 11;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // cboPreset
            // 
            this.cboPreset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPreset.FormattingEnabled = true;
            this.cboPreset.Location = new System.Drawing.Point(223, 52);
            this.cboPreset.MaxDropDownItems = 12;
            this.cboPreset.Name = "cboPreset";
            this.cboPreset.Size = new System.Drawing.Size(192, 21);
            this.cboPreset.TabIndex = 16;
            this.cboPreset.SelectedIndexChanged += new System.EventHandler(this.cboPreset_SelectedIndexChanged);
            // 
            // pnlValue
            // 
            this.pnlValue.Controls.Add(this.txtValue2);
            this.pnlValue.Controls.Add(this.lblValue2);
            this.pnlValue.Controls.Add(this.txtValue1);
            this.pnlValue.Controls.Add(this.lblValue1);
            this.pnlValue.Location = new System.Drawing.Point(174, 79);
            this.pnlValue.Name = "pnlValue";
            this.pnlValue.Size = new System.Drawing.Size(241, 64);
            this.pnlValue.TabIndex = 19;
            // 
            // txtValue2
            // 
            this.txtValue2.Location = new System.Drawing.Point(49, 32);
            this.txtValue2.Name = "txtValue2";
            this.txtValue2.Size = new System.Drawing.Size(192, 20);
            this.txtValue2.TabIndex = 19;
            // 
            // lblValue2
            // 
            this.lblValue2.AutoSize = true;
            this.lblValue2.Location = new System.Drawing.Point(3, 35);
            this.lblValue2.Name = "lblValue2";
            this.lblValue2.Size = new System.Drawing.Size(40, 13);
            this.lblValue2.TabIndex = 18;
            this.lblValue2.Text = "Value2";
            // 
            // txtValue1
            // 
            this.txtValue1.Location = new System.Drawing.Point(49, 6);
            this.txtValue1.Name = "txtValue1";
            this.txtValue1.Size = new System.Drawing.Size(192, 20);
            this.txtValue1.TabIndex = 17;
            // 
            // lblValue1
            // 
            this.lblValue1.AutoSize = true;
            this.lblValue1.Location = new System.Drawing.Point(3, 11);
            this.lblValue1.Name = "lblValue1";
            this.lblValue1.Size = new System.Drawing.Size(34, 13);
            this.lblValue1.TabIndex = 16;
            this.lblValue1.Text = "Value";
            // 
            // pnlDate
            // 
            this.pnlDate.Controls.Add(this.dtpDate2);
            this.pnlDate.Controls.Add(this.dtpDate1);
            this.pnlDate.Controls.Add(this.lblDate2);
            this.pnlDate.Controls.Add(this.lblDate1);
            this.pnlDate.Location = new System.Drawing.Point(432, 149);
            this.pnlDate.Name = "pnlDate";
            this.pnlDate.Size = new System.Drawing.Size(241, 64);
            this.pnlDate.TabIndex = 20;
            // 
            // dtpDate2
            // 
            this.dtpDate2.CustomFormat = "dd-MMM-yyyy";
            this.dtpDate2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate2.Location = new System.Drawing.Point(49, 32);
            this.dtpDate2.Name = "dtpDate2";
            this.dtpDate2.Size = new System.Drawing.Size(192, 20);
            this.dtpDate2.TabIndex = 20;
            // 
            // dtpDate1
            // 
            this.dtpDate1.CustomFormat = "dd-MMM-yyyy";
            this.dtpDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate1.Location = new System.Drawing.Point(49, 6);
            this.dtpDate1.Name = "dtpDate1";
            this.dtpDate1.Size = new System.Drawing.Size(192, 20);
            this.dtpDate1.TabIndex = 19;
            // 
            // lblDate2
            // 
            this.lblDate2.AutoSize = true;
            this.lblDate2.Location = new System.Drawing.Point(3, 35);
            this.lblDate2.Name = "lblDate2";
            this.lblDate2.Size = new System.Drawing.Size(36, 13);
            this.lblDate2.TabIndex = 18;
            this.lblDate2.Text = "Date2";
            // 
            // lblDate1
            // 
            this.lblDate1.AutoSize = true;
            this.lblDate1.Location = new System.Drawing.Point(3, 11);
            this.lblDate1.Name = "lblDate1";
            this.lblDate1.Size = new System.Drawing.Size(30, 13);
            this.lblDate1.TabIndex = 17;
            this.lblDate1.Text = "Date";
            // 
            // pnlBoolean
            // 
            this.pnlBoolean.Controls.Add(this.rbtnFalse);
            this.pnlBoolean.Controls.Add(this.rbtnTure);
            this.pnlBoolean.Controls.Add(this.label5);
            this.pnlBoolean.Location = new System.Drawing.Point(432, 221);
            this.pnlBoolean.Name = "pnlBoolean";
            this.pnlBoolean.Size = new System.Drawing.Size(241, 42);
            this.pnlBoolean.TabIndex = 21;
            // 
            // rbtnFalse
            // 
            this.rbtnFalse.AutoSize = true;
            this.rbtnFalse.Location = new System.Drawing.Point(103, 11);
            this.rbtnFalse.Name = "rbtnFalse";
            this.rbtnFalse.Size = new System.Drawing.Size(50, 17);
            this.rbtnFalse.TabIndex = 18;
            this.rbtnFalse.TabStop = true;
            this.rbtnFalse.Text = "Fales";
            this.rbtnFalse.UseVisualStyleBackColor = true;
            // 
            // rbtnTure
            // 
            this.rbtnTure.AutoSize = true;
            this.rbtnTure.Checked = true;
            this.rbtnTure.Location = new System.Drawing.Point(54, 11);
            this.rbtnTure.Name = "rbtnTure";
            this.rbtnTure.Size = new System.Drawing.Size(47, 17);
            this.rbtnTure.TabIndex = 17;
            this.rbtnTure.TabStop = true;
            this.rbtnTure.Text = "True";
            this.rbtnTure.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Value";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.chkNOT);
            this.groupBox1.Controls.Add(this.rbtnOR);
            this.groupBox1.Controls.Add(this.rbtnAnd);
            this.groupBox1.Location = new System.Drawing.Point(174, 166);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(241, 68);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Join By:";
            // 
            // chkNOT
            // 
            this.chkNOT.AutoSize = true;
            this.chkNOT.Location = new System.Drawing.Point(10, 42);
            this.chkNOT.Name = "chkNOT";
            this.chkNOT.Size = new System.Drawing.Size(150, 17);
            this.chkNOT.TabIndex = 2;
            this.chkNOT.Text = "NOT (Negate Expresstion)";
            this.chkNOT.UseVisualStyleBackColor = true;
            // 
            // rbtnOR
            // 
            this.rbtnOR.AutoSize = true;
            this.rbtnOR.Location = new System.Drawing.Point(113, 20);
            this.rbtnOR.Name = "rbtnOR";
            this.rbtnOR.Size = new System.Drawing.Size(41, 17);
            this.rbtnOR.TabIndex = 1;
            this.rbtnOR.Text = "OR";
            this.rbtnOR.UseVisualStyleBackColor = true;
            // 
            // rbtnAnd
            // 
            this.rbtnAnd.AutoSize = true;
            this.rbtnAnd.Checked = true;
            this.rbtnAnd.Location = new System.Drawing.Point(59, 20);
            this.rbtnAnd.Name = "rbtnAnd";
            this.rbtnAnd.Size = new System.Drawing.Size(48, 17);
            this.rbtnAnd.TabIndex = 0;
            this.rbtnAnd.TabStop = true;
            this.rbtnAnd.Text = "AND";
            this.rbtnAnd.UseVisualStyleBackColor = true;
            // 
            // lblPreset
            // 
            this.lblPreset.AutoSize = true;
            this.lblPreset.Location = new System.Drawing.Point(177, 55);
            this.lblPreset.Name = "lblPreset";
            this.lblPreset.Size = new System.Drawing.Size(37, 13);
            this.lblPreset.TabIndex = 23;
            this.lblPreset.Text = "Preset";
            // 
            // pnlList
            // 
            this.pnlList.Controls.Add(this.btnValues);
            this.pnlList.Controls.Add(this.txtValues);
            this.pnlList.Controls.Add(this.label6);
            this.pnlList.Location = new System.Drawing.Point(432, 79);
            this.pnlList.Name = "pnlList";
            this.pnlList.Size = new System.Drawing.Size(241, 64);
            this.pnlList.TabIndex = 24;
            // 
            // btnValues
            // 
            this.btnValues.Location = new System.Drawing.Point(163, 30);
            this.btnValues.Name = "btnValues";
            this.btnValues.Size = new System.Drawing.Size(75, 23);
            this.btnValues.TabIndex = 18;
            this.btnValues.Text = "Select...";
            this.btnValues.UseVisualStyleBackColor = true;
            this.btnValues.Click += new System.EventHandler(this.btnValues_Click);
            // 
            // txtValues
            // 
            this.txtValues.Location = new System.Drawing.Point(49, 6);
            this.txtValues.Name = "txtValues";
            this.txtValues.Size = new System.Drawing.Size(192, 20);
            this.txtValues.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Values";
            // 
            // frmFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 449);
            this.Controls.Add(this.pnlList);
            this.Controls.Add(this.lblPreset);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pnlBoolean);
            this.Controls.Add(this.pnlDate);
            this.Controls.Add(this.pnlValue);
            this.Controls.Add(this.cboPreset);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnChange);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lvwFilter);
            this.Controls.Add(this.cboOperator);
            this.Controls.Add(this.lblOperator);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listField);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmFilter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Filter";
            this.Load += new System.EventHandler(this.frmFilter_Load);
            this.pnlValue.ResumeLayout(false);
            this.pnlValue.PerformLayout();
            this.pnlDate.ResumeLayout(false);
            this.pnlDate.PerformLayout();
            this.pnlBoolean.ResumeLayout(false);
            this.pnlBoolean.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlList.ResumeLayout(false);
            this.pnlList.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listField;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblOperator;
        private System.Windows.Forms.ComboBox cboOperator;
        private System.Windows.Forms.ListView lvwFilter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ColumnHeader Field;
        private System.Windows.Forms.ColumnHeader Filter;
        private System.Windows.Forms.ComboBox cboPreset;
        private System.Windows.Forms.Panel pnlValue;
        private System.Windows.Forms.TextBox txtValue1;
        private System.Windows.Forms.Label lblValue1;
        private System.Windows.Forms.TextBox txtValue2;
        private System.Windows.Forms.Label lblValue2;
        private System.Windows.Forms.Panel pnlDate;
        private System.Windows.Forms.DateTimePicker dtpDate2;
        private System.Windows.Forms.DateTimePicker dtpDate1;
        private System.Windows.Forms.Label lblDate2;
        private System.Windows.Forms.Label lblDate1;
        private System.Windows.Forms.Panel pnlBoolean;
        private System.Windows.Forms.RadioButton rbtnTure;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rbtnFalse;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtnOR;
        private System.Windows.Forms.RadioButton rbtnAnd;
        private System.Windows.Forms.CheckBox chkNOT;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPreset;
        private System.Windows.Forms.Panel pnlList;
        private System.Windows.Forms.TextBox txtValues;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnValues;
    }
}