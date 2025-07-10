using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace PG.Core.DBBase
{
    public partial class DBMapList
    {
        public static readonly Dictionary<string, List<DBMapField>> DBMapFieldList = new Dictionary<string, List<DBMapField>>();
        public static readonly Dictionary<string, DBMapTable> DBMapTableList = new Dictionary<string, DBMapTable>();

        private static void AddDBMapTableToCache(string keyName, DBMapTable mapTable)
        {
            DBMapTableList.Add(keyName, mapTable);
        }
        public static void AddDBMapTableToCache<T>() where T : class
        {
            string keyName = typeof(T).Name;
            DBMapTableList.Add(keyName, DBMap.GetDBMapTable<T>());
        }

        public static void RemoveDBMapTableFromCache<T>() where T : class
        {
            DBMapTableList.Remove(typeof(T).Name);
        }

        public static void ClearDBMapTableList()
        {
            DBMapTableList.Clear();
        }

        public static DBMapTable GetDBMapTableNameFromCache<T>() where T : class
        {
            DBMapTable mapTable = null;
            string keyName = typeof(T).Name;
            if (DBMapTableList != null)
            {
                if (DBMapTableList.ContainsKey(keyName))
                {
                    mapTable = DBMapTableList[keyName];
                }
                else
                {
                    mapTable = DBMap.GetDBMapTable<T>();
                    AddDBMapTableToCache(keyName, mapTable);
                }
            }
            return mapTable;
        }

        private static void AddDBMapFieldListToCache(string keyName, List<DBMapField> listMapFieldList)
        {
            DBMapFieldList.Add(keyName, listMapFieldList);
        }

        public static void AddDBMapFieldListToCache<T>() where T : class
        {
            string keyName = typeof(T).Name;
            DBMapFieldList.Add(keyName, DBMap.GetDBMapFieldList<T>());
        }


        public static void RemoveDBMapFieldListFromCache<T>() where T : class
        {
            DBMapFieldList.Remove(typeof(T).Name);
        }

        public static void ClearDBMapFieldList()
        {
            DBMapFieldList.Clear();
        }

        public static List<DBMapField> GetDBMapFieldListFromCache<T>() where T : class
        {
            List<DBMapField> listMapFieldList = null;
            string keyName = typeof(T).Name;

            if (DBMapFieldList != null)
            {
                lock (DBMapFieldList)  //locked for thread safe
                {
                    if (DBMapFieldList.ContainsKey(keyName))
                    {
                        listMapFieldList = DBMapFieldList[keyName];
                    }
                    else
                    {
                        listMapFieldList = DBMap.GetDBMapFieldList<T>();
                        AddDBMapFieldListToCache(keyName, listMapFieldList);
                    }
                }
            }

            return listMapFieldList;
        }

        public static DBMapTable GetDBMapTableFromCache<T>() where T : class
        {
            DBMapTable mapTable = null;
            string keyName = typeof(T).Name;

            if (DBMapTableList != null)
            {
                lock (DBMapTableList)  //locked for thread safe
                {
                    if (DBMapTableList.ContainsKey(keyName))
                    {
                        mapTable = DBMapTableList[keyName];
                    }
                    else
                    {
                        mapTable = DBMap.GetDBMapTable<T>();
                        AddDBMapTableToCache(keyName, mapTable);
                    }
                }
            }

            return mapTable;
        }
    }
}
