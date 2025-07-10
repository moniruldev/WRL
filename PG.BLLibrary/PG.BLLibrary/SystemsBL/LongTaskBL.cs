using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PG.DBClass.SystemDC;

namespace PG.BLLibrary.SystemsBL
{
    public class LongTaskBL
    {
        private static readonly List<dcLongTask> LongTaskList = new List<dcLongTask>();

        public static List<dcLongTask> GetLongTaskList()
        {
            return LongTaskList;
        }


        public static string CreateTaskID()
        {
            Random rnd = new Random();
            //return rnd.Next(Int32.MaxValue).ToString();
            string key = rnd.Next(10000).ToString();
            while (LongTaskList.Exists(c=> c.TaskID == key))
            {
                key = rnd.Next(10000).ToString();
            }
            //TaskProgressList.ContainsKey(key);

            return key;
        }

        public static dcLongTask GetOrCreateLongTask(LongTaskEnum pLongTask)
        {
            dcLongTask longTask = null;
            lock (LongTaskList)
            {
                longTask = LongTaskList.SingleOrDefault(c => c.Task == pLongTask);
                if (longTask == null)
                {
                    longTask = new dcLongTask();
                    longTask.TaskID = CreateTaskID();
                    longTask.Task = pLongTask;

                    LongTaskList.Add(longTask);
                }
            }
            return longTask;
        }

        public static dcLongTask GetOrCreateLongTask(string pTaskID)
        {
            dcLongTask longTask = LongTaskList.SingleOrDefault(c => c.TaskID == pTaskID);
            if (longTask == null)
            {
                longTask = new dcLongTask();
                longTask.Task = LongTaskEnum.TaskByID;
                lock (LongTaskList)
                {
                    LongTaskList.Add(longTask);
                }
            }
            return longTask;
        }

        public static dcLongTask GetLongTask(LongTaskEnum pLongTask)
        {
            dcLongTask longTask = LongTaskList.SingleOrDefault(c => c.Task == pLongTask);
            return longTask;
        }

        public static dcLongTask GetLongTask(string pTaskID)
        {
            dcLongTask longTask = LongTaskList.SingleOrDefault(c => c.TaskID == pTaskID);
            return longTask;
        }



        public static string GetTaskIDByType(LongTaskEnum pLongTask)
        {
            string taskID = string.Empty;
            dcLongTask longTask = LongTaskList.SingleOrDefault(c => c.Task == pLongTask);

            if (longTask != null)
            {
                taskID = longTask.TaskID;
            }
            return taskID;
        }


        public static bool IsTaskInProgress(LongTaskEnum pLongTask)
        {
            bool bStatus = false;
            dcLongTask longTask = LongTaskList.SingleOrDefault(c => c.Task == pLongTask);

            if (longTask != null)
            {
                if (longTask.TaskState == LongTaskStateEnum.InProgress)
                {
                    bStatus = true;
                }
            }
            return bStatus;
        }

        public static bool CancelTask(LongTaskEnum pLongTask)
        {
            bool bStatus = false;
            dcLongTask longTask = LongTaskList.SingleOrDefault(c => c.Task == pLongTask);

            if (longTask != null)
            {
                if (longTask.TaskState == LongTaskStateEnum.InProgress)
                {
                    longTask.IsCancelled = true;
                    bStatus = true;
                }
            }
            return bStatus;
        }

        public static bool CancelTask(string pTaskID)
        {
            bool bStatus = false;
            dcLongTask longTask = LongTaskList.SingleOrDefault(c => c.TaskID == pTaskID);

            if (longTask != null)
            {
                if (longTask.TaskState == LongTaskStateEnum.InProgress)
                {
                    longTask.IsCancelled = true;
                    bStatus = true;
                }
            }

            return bStatus;
        }

     }
}
