using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PG.Core.Windows.Forms
{
    public partial class frmProgressBar : Form
    {
        private bool m_ConfirmCancel = false;
        private string m_TaskID = string.Empty;
        public frmProgressBar()
        {
            InitializeComponent();
        }
        public void SetMinMax(int min, int max)
        {
            this.progressBar1.Minimum = min;
            this.progressBar1.Maximum = max;
            Application.DoEvents();
        }
        public string TaskID
        {
            get { return m_TaskID; }
            set { m_TaskID = value; }
        }

        public void UpdateProgress(int work)
        {

            int max = this.progressBar1.Maximum;
            if (max <= 0)
            {
                return;
            }
            int percent = 0;

            work = work < 0 ? 0 : work;
            work = work > max ? max : work;

            if (work > 0)
            {
                //percent = (work / max) * 100;
                percent = (work * 100) / max;
            }

            this.lblCompleted.Text = "Completed: " + percent + "%";

            this.progressBar1.Value = work;
            Application.DoEvents();
        }
        public bool EnableCancel
        {
            get { return this.btnCancel.Visible; }
            set { this.btnCancel.Visible = value; }
        }

        public bool ConfirmCancel
        {
            get { return m_ConfirmCancel; }
            set { m_ConfirmCancel = value; }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (m_ConfirmCancel)
            {
                string taskDesc = ProgressBarUI.GetTaskDescription(this.TaskID);
                string msg = "Cancel Current Task?";
                if (taskDesc != string.Empty)
                {
                    msg = "Task: " + taskDesc + "\r\n\r\n" + msg;
                }
                if (DialogResult.Yes == MessageBox.Show(msg, "Cancel Task?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                {
                    ProgressBarUI.CancelTask(m_TaskID);
                }
            }
            else
            {
                ProgressBarUI.CancelTask(m_TaskID);
            }
        }

        private void frmProgressBar_Load(object sender, EventArgs e)
        {

        }
    }
}
