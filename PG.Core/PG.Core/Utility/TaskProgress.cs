using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;


///Task progress - Descpription, uses...
///Generates events for long tasks in BL Library to UI
///how to use:
///task progress is tracked by a TaskID;
///so in BL Library you need to take taskid from UI by a parameter. for this in long task function
///create overloaded function to receive this task id;
///
///Example Long Running function:
/// public bool DoLongTask(string taskID)
/// {
///     //set task progess
///     TaskProgress tProgress = null;
///     if (taskID != string.Empty)
///     {
///       tProgress = TaskProgress.GetTaskProgressByTaskID(taskID);
///       TaskProgress.NotifyTaskMinMaxChanged(tProgress, 0, lstItem.Count);
///     }
///     
///     //actual long task 
///     for (int i = 0; i < 1000; i++)
///     {
///       //check task cancel clicked
///       if (TaskProgress.CheckAndNotifyTaskCancel(tProgress))
///       {
///           ///on cancel
///           break;
///       }
///       /// 
///        //fire notify events with step value
///        TaskProgress.NotifyTaskValueChanged(tProgress, i);
///        
///        //....do you task
///     }
/// }
/// 
/// use in Client side function:
/// public bool CallLongTask()
/// {
///    string taskID = TaskProgress.CreateNewTask();
///    TaskProgress tProgress = TaskProgress.GetTaskProgressByTaskID(taskID);
///    TaskProgress.TaskReset(tProgress);
///    tProgress.OnTaskMinMaxChanged += new TaskMinMaxChangedEventHandler(ChangeMinMax);
///    tProgress.OnTaskValueChanged += new TaskValueChangedEventHandler(ChangeProgressValue);
///    
///    ///call the task
///     DoLongTask(taskID);
///     
///    return true;
/// }
/// public void ChangeProgressValue(int val)
/// {
///     this.progressBar1.Value = val;
///     Application.DoEvents();
/// }
/// /// public void ChangeMinMax(int pMin, int pMax)
/// {
///     this.progressBar1.Minimum = pMin;
///     this.progressBar1.Maximum = pMax
///     Application.DoEvents();
/// }
/// 
/// use for BuiltinProgressBar win form:
/// public bool CallLongTaskWithProgressBarForm()
/// {
///    UILibrary.ProgressBarUI.Reset();
///    string taskID = UILibrary.ProgressBarUI.ShowProgressBar("Long Task Running...");
///    ///call the task
///    DoLongTask(taskID);
///    //task completed close progress bar
///    UILibrary.ProgressBarUI.CloseProgressBar(taskID);
/// }
///
namespace PG.Core.Utility
{
    public delegate void TaskMinMaxChangedEventHandler(int pMin, int pMax);
    public delegate void TaskValueChangedEventHandler(int pValue);
    public delegate void TaskCancelEventHandler();
    public delegate void TaskCompleteEventHandler();
    public delegate void TaskErrorEventHandler(string taskID, Exception e);
       

    public class TaskProgress
    {
        public static Dictionary<string, TaskProgress> TaskProgressList = new Dictionary<string, TaskProgress>();

        private bool m_IsCancelTask = false;
        private string m_TaskID = string.Empty;
        private string m_TaskDesc = string.Empty;
        private string m_SessionID = string.Empty;
        private string m_RequestID = string.Empty;
        private bool m_IsErrorRethrow = false;

        public bool IsCancelTask
        {
            get { return m_IsCancelTask; }
            set { m_IsCancelTask = value; }
        }
        public bool IsErrorRethrow
        {
            get { return m_IsErrorRethrow; }
            set { m_IsErrorRethrow = value; }
        }
        public string TaskID
        {
            get { return m_TaskID; }
            set { m_TaskID = value; }
        }
        public string TaskDescription
        {
            get { return m_TaskDesc; }
            set { m_TaskDesc = value; }
        }
        public string SessionID
        {
            get { return m_SessionID; }
            set { m_SessionID = value; }
        }
        public string RequestID
        {
            get { return m_RequestID; }
            set { m_RequestID = value; }
        }

        public event TaskMinMaxChangedEventHandler OnTaskMinMaxChanged;
        private void NotifyTaskMinMaxChanged(int pMin, int pMax)
        {
            if (OnTaskMinMaxChanged != null)
            {
                OnTaskMinMaxChanged(pMin, pMax);
            }
        }
        public static void NotifyTaskMinMaxChanged(TaskProgress taskProgress, int pMin, int pMax)
        {
            if (taskProgress != null)
            {
                taskProgress.NotifyTaskMinMaxChanged(pMin, pMax);
            }
        }

        public event TaskValueChangedEventHandler OnTaskValueChanged;
        private void NotifyTaskValueChanged(int pValue)
        {
            if (OnTaskValueChanged != null)
            {
                OnTaskValueChanged(pValue);
            }
        }
        public static void NotifyTaskValueChanged(TaskProgress taskProgress, int pValue)
        {
            if (taskProgress != null)
            {
                taskProgress.NotifyTaskValueChanged(pValue);
            }
        }
        public event TaskCancelEventHandler OnTaskCancel;
        private void NotifyTaskCancel()
        {
            if (OnTaskCancel != null)
            {
                OnTaskCancel();
            }
        }
        public static void NotifyTaskCancel(TaskProgress taskProgress)
        {
            if (taskProgress != null)
            {
                taskProgress.NotifyTaskCancel();
            }
        }
        public static bool CheckAndNotifyTaskCancel(TaskProgress taskProgress)
        {
            bool bResult = false;
            if (taskProgress != null)
            {
                if (taskProgress.IsCancelTask)
                {
                    taskProgress.NotifyTaskCancel();
                    bResult = true;
                }
            }
            return bResult;
        }

        public event TaskCompleteEventHandler OnTaskComplete;
        private void NotifyTaskComplete()
        {
            if (OnTaskComplete != null)
            {
                OnTaskComplete();
            }
        }
        public static void NotifyTaskComplete(TaskProgress taskProgress)
        {
            if (taskProgress != null)
            {
                taskProgress.NotifyTaskComplete();
            }
        }
        public event TaskErrorEventHandler OnTaskError;
        private void NotifyOnError(Exception e)
        {
            if (OnTaskError != null)
            {
                OnTaskError(this.TaskID, e);
            }
        }
        public static void NotifyOnError(TaskProgress taskProgress, Exception e)
        {
            if (taskProgress != null)
            {
                taskProgress.NotifyOnError(e);
            }
        }

        private void TaskReset()
        {
            IsCancelTask = false;
            OnTaskMinMaxChanged = null;
            OnTaskValueChanged = null;
            OnTaskCancel = null;
            OnTaskComplete = null;
            OnTaskError = null;
        }
        public static void TaskReset(TaskProgress taskProgress)
        {
            taskProgress.TaskReset();
        }

        public static TaskProgress GetTaskProgressByTaskID(string taskID)
        {
            TaskProgress cTProgess = null;
            if (TaskProgressList != null)
            {
                if (TaskProgressList.ContainsKey(taskID))
                {
                    cTProgess = TaskProgressList[taskID];
                }
            }
            return cTProgess;
        }
        public static void AddTaskProgressToList(string taskID, TaskProgress taskProgess)
        {
            TaskProgressList.Add(taskID, taskProgess);
        }
        public static void RemoveTaskProgressFromList(string taskID)
        {
            TaskProgressList.Remove(taskID);
        }
        public static void ClearTaskProgressList()
        {
            TaskProgressList.Clear();
        }
        public static string CreateTaskID()
        {
            Random rnd = new Random();
            //return rnd.Next(Int32.MaxValue).ToString();
            string key = rnd.Next(10000).ToString();
            while (TaskProgressList.ContainsKey(key))
            {
                key = rnd.Next(10000).ToString(); 
            }
            //TaskProgressList.ContainsKey(key);

            return key;
        }

        public static string CreateNewTask()
        {
           return AddNewTaskProgressToList(string.Empty);
        }
        public static string CreateNewTask(string taskDesc)
        {
           return AddNewTaskProgressToList(taskDesc);
        }
        public static string AddNewTaskProgressToList()
        {
            return AddNewTaskProgressToList(string.Empty);
        }
        public static string AddNewTaskProgressToList(string taskDesc)
        {
            string taskID = CreateTaskID();
            TaskProgress tProgress = new TaskProgress();
            tProgress.TaskID = taskID;
            tProgress.TaskDescription = taskDesc;
            AddTaskProgressToList(taskID, tProgress);
            return taskID;
        }
        public static void EndTask(string taskID)
        {
            RemoveTaskProgressFromList(string.Empty);
        }
    }
}
