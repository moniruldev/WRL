using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PG.Core.Utility;

namespace PG.Core.Windows
{
    public class ProgressBarUI
    {
        public static bool IsProgressBar = false;
        public static bool IsCancelled = false;
        public static bool IsError = false;
        public static bool IsHandleError = false;
        public static bool IsErrorRethrow = false;
        
        private static Forms.frmProgressBar frmProgress = null;
        public static FormStartPosition ProgressBarPosition = FormStartPosition.CenterScreen;

        public static void Reset()
        {
            IsProgressBar = false;
            IsCancelled = false;
            IsError = false;
            IsHandleError = false;
            IsErrorRethrow = false;
            ProgressBarPosition = FormStartPosition.CenterScreen;
        }
        public static void Reset(string taskID)
        {
            CloseProgressBar(taskID);
            Reset();
        }
        public static string ShowProgressBar()
        {
            return ShowProgressBar(string.Empty, string.Empty, string.Empty, 0, 0, false, false, null);
        }
        public static string ShowProgressBar(string msg)
        {
            return ShowProgressBar(string.Empty, msg, string.Empty, 0, 0, false, false, null);
        }
        public static string ShowProgressBar(string msg, Form parentForm)
        {
            return ShowProgressBar(string.Empty, msg, string.Empty, 0, 0, false, false, parentForm);
        }
        public static string ShowProgressBar(bool showCancel, bool confirmCancel)
        {
            return ShowProgressBar(string.Empty, string.Empty, string.Empty, 0, 0, showCancel, confirmCancel,null);
        }
        public static string ShowProgressBar(string msg, bool showCancel, bool confirmCancel)
        {
            return ShowProgressBar(string.Empty, msg, string.Empty, 0, 0, showCancel, confirmCancel, null);
        }
        public static string ShowProgressBar(string msg, bool showCancel, bool confirmCancel, Form parentForm)
        {
            return ShowProgressBar(string.Empty, msg, string.Empty, 0, 0, showCancel, confirmCancel, parentForm);
        }
        public static string ShowProgressBar(string taskID, string msg, bool showCancel, bool confirmCancel)
        {
            return ShowProgressBar(taskID, msg, string.Empty,0, 0, showCancel, confirmCancel, null);
        }
        public static string ShowProgressBar(string msg, int min, int max, bool showCancel, bool confirmCancel)
        {
            return ShowProgressBar(string.Empty, msg, string.Empty, min, max, showCancel, confirmCancel, null);
        }
        public static string ShowProgressBar(string msg, string taskDesc,int min, int max, bool showCancel, bool confirmCancel)
        {
            return ShowProgressBar(string.Empty, msg, taskDesc, min, max, showCancel, confirmCancel, null);
        }
        public static string ShowProgressBar(string taskID, string msg, string taskDesc, int min, int max, bool showCancel, bool confirmCancel, Form parentForm)
        {
            if (frmProgress == null)
            {
                frmProgress = new PG.Core.Windows.Forms.frmProgressBar();
            }

            string mTaskID = taskID;
            if (taskID == string.Empty)
            {
                mTaskID = TaskProgress.AddNewTaskProgressToList(taskDesc);
            }
            TaskProgress tProgress = TaskProgress.GetTaskProgressByTaskID(mTaskID);
            TaskProgress.TaskReset(tProgress);
            tProgress.OnTaskMinMaxChanged += new TaskMinMaxChangedEventHandler(ProgressBarUI.SetMinMax);
            tProgress.OnTaskValueChanged += new TaskValueChangedEventHandler(ProgressBarUI.ChangeValue);
            if (IsHandleError)
            {
                tProgress.IsErrorRethrow = IsErrorRethrow;
                tProgress.OnTaskError += new TaskErrorEventHandler(ProgressBarUI.Error);
            }

            frmProgress.TaskID = mTaskID;
            if (msg != string.Empty)
            {
                frmProgress.lblMessage.Text = msg;
            }
            frmProgress.SetMinMax(min, max);
            frmProgress.EnableCancel = showCancel;
            frmProgress.ConfirmCancel = confirmCancel;
            
            if (!frmProgress.Visible)
            {
                frmProgress.StartPosition = ProgressBarPosition;
                frmProgress.Show(parentForm);
                frmProgress.TopMost = true ;
            }
            IsProgressBar = true;
            IsCancelled = false;

            return mTaskID;
        }

        public static void SetMinMax(int pMin, int pMax)
        {
            if (frmProgress != null)
            {
                frmProgress.SetMinMax(pMin, pMax);
            }
        }

        public static void ChangeValue(int val)
        {
            if (frmProgress != null)
            {
                frmProgress.UpdateProgress(val);
                //frm.Close();
               // frm.Dispose();
            }
        }
        public static void ChangeMessage(string msg)
        {

            if (frmProgress != null)
            {
                frmProgress.lblMessage.Text = msg;
                Application.DoEvents();
                //frm.Close();
                // frm.Dispose();
            }
        }

        public static void ChangeDescripton(string msg)
        {

            if (frmProgress != null)
            {
                frmProgress.Text = msg;
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

        public static string AddTask()
        {
            return AddTask(string.Empty);
        }
        public static string AddTask(string taskDesc)
        {
           return TaskProgress.AddNewTaskProgressToList(taskDesc);
        }

        public static void Error(string taskID, Exception e)
        {
            IsError = true;
            MessageBox.Show(e.Message);
            //CloseProgressBar(taskID);
        }
        public static void CloseProgressBar(string taskID)
        {
            if (frmProgress != null)
            {
                frmProgress.Close();
                // frm.Dispose();
            }
            frmProgress = null;
            TaskProgress.RemoveTaskProgressFromList(taskID);
        }
        public static string GetTaskDescription(string taskID)
        {
            string taskDesc = string.Empty;
            TaskProgress tProgress = TaskProgress.GetTaskProgressByTaskID(taskID);
            if (tProgress != null)
            {
                taskDesc = tProgress.TaskDescription;
            }
            return taskDesc;
        }
    }
}
