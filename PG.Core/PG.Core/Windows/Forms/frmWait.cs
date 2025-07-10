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
    public partial class frmWait : Form
    {
        private bool m_ConfirmCancel = false;
        private string m_TaskID = string.Empty;


        public string TaskID
        {
            get { return m_TaskID; }
            set { m_TaskID = value; }
        }

        public bool EnableCancel
        {
            get { 
                return this.btnCancel.Visible; 
            }
            set { 

                this.btnCancel.Visible = value;
                if (btnCancel.Visible)
                {
                    this.Height = 100;
                }
                else
                {
                    this.Height = 70;
                }
                Application.DoEvents();
            }
        }

        public bool ConfirmCancel
        {
            get { return m_ConfirmCancel; }
            set { m_ConfirmCancel = value; }
        }

        public frmWait()
        {
            InitializeComponent();
        }

        private void frmWait_Load(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (m_ConfirmCancel)
            {
                string taskDesc = string.Empty;
                if (this.TaskID != string.Empty)
                {
                   taskDesc = ProgressBarUI.GetTaskDescription(this.TaskID);
                }

                string msg = "Cancel Current Task?";
                if (taskDesc != string.Empty)
                {
                    msg = "Task: " + taskDesc + "\r\n\r\n" + msg;
                }

                if (DialogResult.Yes == MessageBox.Show(msg, "Cancel Task?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
                {
                    if (this.TaskID != string.Empty)
                    {
                        ProgressBarUI.CancelTask(m_TaskID);
                    }
                }
            }
            else
            {
                if (this.TaskID != string.Empty)
                {
                    ProgressBarUI.CancelTask(m_TaskID);
                }
            }
        }
    }
}
