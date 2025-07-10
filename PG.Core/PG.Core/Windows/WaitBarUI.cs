using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PG.Core.Utility;

namespace PG.Core.Windows
{
    public class WaitBarUI
    {
        public static bool IsWaitBar = false;
        public static bool IsCancelled = false;
        public static bool IsError = false;
        public static bool IsHandleError = false;
        public static bool IsErrorRethrow = false;

        private static Forms.frmWait frmWait = null;
        public static FormStartPosition ProgressBarPosition = FormStartPosition.CenterScreen;

        public static void Reset()
        {
            IsWaitBar = false;
            IsCancelled = false;
            IsError = false;
            IsHandleError = false;
            IsErrorRethrow = false;
            ProgressBarPosition = FormStartPosition.CenterScreen;
        }

        public static void Reset(string taskID)
        {
            CloseWaitBar(taskID);
            Reset();
        }



        public static string ShowWaitBar()
        {
            return ShowWaitBar(string.Empty, string.Empty, string.Empty, false, false, null);
        }

        public static string ShowWaitBar(string msg)
        {
            return ShowWaitBar(string.Empty, msg, string.Empty, false, false, null);
        }


        public static string ShowWaitBar(string msg, Form parentForm)
        {
            return ShowWaitBar(string.Empty, msg, string.Empty, false, false, parentForm);
        }


        public static string ShowWaitBar(string taskID, string msg, string taskDesc, bool showCancel, bool confirmCancel, Form parentForm)
        {
            if (frmWait == null)
            {
                frmWait = new PG.Core.Windows.Forms.frmWait();
            }

            string mTaskID = taskID;
            if (taskID == string.Empty)
            {
                mTaskID = TaskProgress.AddNewTaskProgressToList(taskDesc);
            }
            TaskProgress tProgress = TaskProgress.GetTaskProgressByTaskID(mTaskID);
            TaskProgress.TaskReset(tProgress);

            if (IsHandleError)
            {
                tProgress.IsErrorRethrow = IsErrorRethrow;
                tProgress.OnTaskError += new TaskErrorEventHandler(ProgressBarUI.Error);
            }

            frmWait.TaskID = mTaskID;
            if (msg != string.Empty)
            {
                frmWait.lblMessage.Text = msg;
            }
            frmWait.EnableCancel = showCancel;
            frmWait.ConfirmCancel = confirmCancel;

            if (!frmWait.Visible)
            {
                frmWait.StartPosition = ProgressBarPosition;
                frmWait.Show(parentForm);
                frmWait.TopMost = true;

            }
            IsWaitBar = true;
            IsCancelled = false;
            Application.DoEvents();
            return mTaskID;
        }

        public static void ChangeMessage(string msg)
        {

            if (frmWait != null)
            {
                frmWait.Text = msg;
                Application.DoEvents();
                //frm.Close();
                // frm.Dispose();
            }
        }

        public static void ChangeDescripton(string msg)
        {

            if (frmWait != null)
            {
                frmWait.lblMessage.Text = msg;
                Application.DoEvents();
                //frm.Close();
                // frm.Dispose();
            }
        }



        public static void CancelTask(string taskID)
        {
            TaskProgress tProgress = TaskProgress.GetTaskProgressByTaskID(taskID);
            if (tProgress != null)
            {
                tProgress.IsCancelTask = true;
            }
            IsCancelled = true;
            //CloseProgressBar(taskID);
            Application.DoEvents();
        }


        public static void CloseWaitBar(string taskID)
        {
            if (frmWait != null)
            {
                frmWait.Close();
                // frm.Dispose();
            }
            frmWait = null;
            TaskProgress.RemoveTaskProgressFromList(taskID);
        }
    }
}
