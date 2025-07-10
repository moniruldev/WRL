using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data.Linq.Mapping;
using Oracle.ManagedDataAccess.Client;

namespace PG.Core.DBBase
{
    public partial class DBMap
    {
        private DatabaseTypeEnum m_DatabaseType = DatabaseTypeEnum.SQLServer;
        private string m_xUpdateNoFieldName = "xUpdateNo";

        public DatabaseTypeEnum DatabaseType
        {
            get { return m_DatabaseType; }
            set { m_DatabaseType = value; }
        }
        public string xUpdateNoFieldName
        {
            get { return m_xUpdateNoFieldName; }
            set { m_xUpdateNoFieldName = value; }
        }

        public static bool IsNullableType(Type theType)
        {
            //use: IsNullableType(property.PropertyType)


            //The NullableConverter Class will allow you to convert a Nullable Type to its underlying type:
            // UnderlyingType will equal System.DateTime
            //NullableConverter nc = new NullableConverter(DateTime?);
            //Type underlyingType = nc.UnderlyingType;


            return (theType.IsGenericType && theType.
              GetGenericTypeDefinition().Equals
              (typeof(Nullable<>)));
        }

        
        public static string GetMapFieldName<T>(string field, out bool oIsKey, out bool oIsDBGenarated) where T : class
        {
            Type cType = typeof(T);
            string fldName = string.Empty;
            //Type fldType = null;
            oIsKey = false;
            oIsDBGenarated = false;
            PropertyInfo prop = cType.GetProperty(field);
            //object[] info = prop.GetCustomAttributes(typeof(ColumnAttribute), true);
            //if (info != null && info.Count() > 0)
            //{
            //    ColumnAttribute ca = (ColumnAttribute)info[0];
            //    fldName = ca.Name;
            //    oIsKey = ca.IsPrimaryKey;
            //    oIsDBGenarated = ca.IsDbGenerated;
            //}

            object[] info = prop.GetCustomAttributes(typeof(DBColumnAttribute), true);
            if (info != null && info.Count() > 0)
            {
                DBColumnAttribute ca = (DBColumnAttribute)info[0];
                fldName = ca.Name;
                oIsKey = ca.IsPrimaryKey;
                oIsDBGenarated = ca.IsDbGenerated;
            }

            ////get type
            ////is NULLABLE
            //if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            //{
            //    // If it is NULLABLE, then get the underlying type. eg if "Nullable<int>" then this will return just "int"
            //    fldType = prop.PropertyType.GetGenericArguments()[0];
            //}
            //else
            //{
            //    fldType = prop.PropertyType;
            //}

            //Nullable.GetUnderlyingType(prop.PropertyType).FullName
            //p.ParameterType.GetGenericArguments()[0]

            //fldName = prop.PropertyType.FullName;
            //oType = fldType;
            return fldName;
        }
        public static List<string> GetMapPropertyNames<T>() where T : class
        {
            List<string> lstProps = new List<string>();
            Type cType = typeof(T);
            PropertyInfo[] props = cType.GetProperties();

            //props.GetEnumerator
            IEnumerator enumurator = props.GetEnumerator();
            while (enumurator.MoveNext())
            {
                PropertyInfo prop = enumurator.Current as PropertyInfo;
                //object[] info = prop.GetCustomAttributes(typeof(ColumnAttribute), true);
                //if (info != null && info.Count() > 0)
                //{
                //    lstProps.Add(prop.Name);
                //}
                object[] info = prop.GetCustomAttributes(typeof(DBColumnAttribute), true);
                if (info != null && info.Count() > 0)
                {
                    lstProps.Add(prop.Name);
                }


            }
            return lstProps;
        }
        public static List<string> GetMapFieldNames<T>() where T : class
        {
            List<string> lstFields = new List<string>();
            List<DBMapField> listMapField = DBMapList.GetDBMapFieldListFromCache<T>();
            foreach (DBMapField cObj in listMapField)
            {
                lstFields.Add(cObj.FieldName);
            }

            //Type cType = typeof(T);
            //PropertyInfo[] props = cType.GetProperties();

            ////props.GetEnumerator
            //IEnumerator enumurator = props.GetEnumerator();
            //while (enumurator.MoveNext())
            //{
            //    PropertyInfo prop = enumurator.Current as PropertyInfo;
            //    object[] info = prop.GetCustomAttributes(typeof(ColumnAttribute), true);
            //    if (info != null && info.Count() > 0)
            //    {
            //        ColumnAttribute ca = (ColumnAttribute)info[0];
            //        lstFields.Add(ca.Name);
            //    }
            //}
            return lstFields;
        }


        public static List<string> GetKeyNames(List<DBMapField> listDBMapField)
        {
            List<string> listKeyNames = new List<string>();
            List<DBMapField> listKeys = listDBMapField.FindAll(c => c.IsPrimaryKey == true);
            foreach (DBMapField cObj in listKeys)
            {
                listKeyNames.Add(cObj.PropertyName);
            }
            return listKeyNames;
        }

        public static List<string> GetKeyNames<T>() where T : class
        {
            List<string> lstKeys = new List<string>();

            Type cType = typeof(T);
            PropertyInfo[] props = cType.GetProperties();
            
            //props.GetEnumerator
            IEnumerator enumurator = props.GetEnumerator();
            while (enumurator.MoveNext())
            {
                PropertyInfo prop = enumurator.Current as PropertyInfo;
                //object[] info = prop.GetCustomAttributes(typeof(ColumnAttribute), true);
                //if (info != null && info.Count() > 0)
                //{
                //    ColumnAttribute ca = (ColumnAttribute)info[0];
                //    if (ca.IsPrimaryKey)
                //    {
                //        lstKeys.Add(prop.Name);
                //    }
                //}

                object[] info = prop.GetCustomAttributes(typeof(DBColumnAttribute), true);
                if (info != null && info.Count() > 0)
                {
                    DBColumnAttribute ca = (DBColumnAttribute)info[0];
                    if (ca.IsPrimaryKey)
                    {
                        lstKeys.Add(prop.Name);
                    }
                }
            }
            return lstKeys;
        }

        public static DBMapTable GetDBMapTable(Type pType)
        {
            //Type cType = typeof(T);
            DBMapTable mapTable = null;
            try
            {
                //object[] info = cType.GetCustomAttributes(typeof(TableAttribute), true);
                //TableAttribute ta = (TableAttribute)info[0];
                //tblName = ta.Name;
                mapTable = new DBMapTable();

                string tblName = string.Empty;
                object[] info = pType.GetCustomAttributes(typeof(DBTableAttribute), true);
                DBTableAttribute ta = (DBTableAttribute)info[0];
                mapTable.Name = ta.Name;
                mapTable.TableName = ta.TableName;
                mapTable.SchemaName = ta.Schema;
                mapTable.SequenceName = ta.Schema;
                mapTable.Comments = ta.Comments;

                mapTable.LogChange = ta.LogChange;

            }
            catch { }

            return mapTable;
        }

        public static DBMapTable GetDBMapTable<T>() where T : class
        {
            Type cType = typeof(T);
            return GetDBMapTable(cType);

            //DBMapTable mapTable = null;
            //try
            //{
            //    //object[] info = cType.GetCustomAttributes(typeof(TableAttribute), true);
            //    //TableAttribute ta = (TableAttribute)info[0];
            //    //tblName = ta.Name;
            //    mapTable = new DBMapTable();

            //    string tblName = string.Empty;
            //    object[] info = cType.GetCustomAttributes(typeof(DBTableAttribute), true);
            //    DBTableAttribute ta = (DBTableAttribute)info[0];
            //    mapTable.Name = ta.Name;
            //    mapTable.TableName = ta.TableName;
            //    mapTable.SchemaName = ta.Schema;
            //    mapTable.SequenceName = ta.Schema;
            //    mapTable.Comments = ta.Comments;

            //    mapTable.LogChange = ta.LogChange;

            //}
            //catch { }

            //return mapTable;
        }

        //Type cType = typeof(T);
        //PropertyInfo[] props = cType.GetProperties();

        public static string GetMapFieldName(PropertyInfo[] props, string field, out bool oIsKey, out bool oIsDBGenarated)
        {
            string fldName = string.Empty;
            oIsKey = false;
            oIsDBGenarated = false;
            foreach (PropertyInfo prop in props)
            {
                if (prop.Name.ToLower() == field.Trim().ToLower())
                {
                    object[] info = prop.GetCustomAttributes(typeof(DBColumnAttribute), true);
                    if (info != null && info.Count() > 0)
                    {
                        DBColumnAttribute ca = (DBColumnAttribute)info[0];
                        fldName = ca.Name;
                        oIsKey = ca.IsPrimaryKey;
                        oIsDBGenarated = ca.IsDbGenerated;
                    }
                }
            }
            return fldName;
        }

        public static List<DBMapField> GetDBMapFieldList(Type pType)
        {
            List<DBMapField> listMapField = new List<DBMapField>();
            PropertyInfo[] props = pType.GetProperties();
            foreach (PropertyInfo prop in props)
            {
                object[] info = prop.GetCustomAttributes(typeof(DBColumnAttribute), true);
                if (info != null && info.Count() > 0)
                {
                    DBColumnAttribute ca = (DBColumnAttribute)info[0];
                    DBMapField cObj = new DBMapField();
                    cObj.PropertyName = prop.Name;
                    cObj.DataTypeName = prop.PropertyType.Name;
                    cObj.DataTypeFullName = prop.PropertyType.FullName;

                    cObj.FieldName = ca.Name;
                    cObj.IsPrimaryKey = ca.IsPrimaryKey;
                    cObj.IsDBGenerated = ca.IsDbGenerated;
                    cObj.IsIdentity = ca.IsIdentity;
                    cObj.RowGuid = ca.RowGuid;


                    cObj.DBFieldType = ca.DbType;
                    cObj.DBFieldTypeGeneric = ca.DbTypeGeneric;
                    cObj.DBFieldTypeSQL = ca.DbTypeSQL;
                    cObj.DBFieldTypeOracle = ca.DbTypeOracle;

                    cObj.Schema = ca.Schema;

                    cObj.Length = ca.Length;
                    cObj.Precision = ca.Precision;
                    cObj.Scale = ca.Scale;

                    cObj.SequenceName = ca.SequenceName;

                    cObj.Nullable = ca.Nullable;
                    cObj.Unicode = ca.Unicode;

                    cObj.DefaultValue = ca.DefaultValue;
                    cObj.Comments = ca.Comments;
                    cObj.SLNo = ca.SLNo;

                    cObj.SyncOnInsert = ca.SyncOnInsert;
                    cObj.SyncOnUpdate = ca.SyncOnUpdate;

                    cObj.LogChange = ca.LogChange;

                    listMapField.Add(cObj);

                }
            }
            return listMapField;
        }

        public static List<DBMapField> GetDBMapFieldList<T>() where T : class
        {
            return GetDBMapFieldList(typeof(T));

            //List<DBMapField> listMapField = new List<DBMapField>();
            //PropertyInfo[] props = typeof(T).GetProperties();
            //foreach (PropertyInfo prop in props)
            //{
            //    object[] info = prop.GetCustomAttributes(typeof(DBColumnAttribute), true);
            //    if (info != null && info.Count() > 0)
            //    {
            //        DBColumnAttribute ca = (DBColumnAttribute)info[0];
            //        DBMapField cObj = new DBMapField();
            //        cObj.PropertyName = prop.Name;
            //        cObj.DataTypeName = prop.PropertyType.Name;
            //        cObj.DataTypeFullName = prop.PropertyType.FullName;

            //        cObj.FieldName = ca.Name;
            //        cObj.IsPrimaryKey = ca.IsPrimaryKey;
            //        cObj.IsDBGenerated = ca.IsDbGenerated;
            //        cObj.IsIdentity = ca.IsIdentity;
            //        cObj.RowGuid = ca.RowGuid;


            //        cObj.DBFieldType = ca.DbType;
            //        cObj.DBFieldTypeGeneric = ca.DbTypeGeneric;
            //        cObj.DBFieldTypeSQL = ca.DbTypeSQL;
            //        cObj.DBFieldTypeOracle = ca.DbTypeOracle;

            //        cObj.Schema = ca.Schema;

            //        cObj.Length = ca.Length;
            //        cObj.Precision = ca.Precision;
            //        cObj.Scale = ca.Scale;

            //        cObj.SequenceName = ca.SequenceName;

            //        cObj.Nullable = ca.Nullable;
            //        cObj.Unicode = ca.Unicode;

            //        cObj.DefaultValue = ca.DefaultValue;
            //        cObj.Comments =  ca.Comments;
            //        cObj.SLNo = ca.SLNo;

            //        cObj.SyncOnInsert = ca.SyncOnInsert;
            //        cObj.SyncOnUpdate = ca.SyncOnUpdate;

            //        cObj.LogChange = ca.LogChange;

            //        listMapField.Add(cObj);

            //    }
            //}
            //return listMapField;
        }

        public static string GetDynamicFieldSql(string pFields)
        {
            string sql = string.Empty;
            pFields = pFields.Trim();
            if (pFields == string.Empty | pFields == "*" | pFields == "it")
            {
                sql = "it";
            }
            else
            {
                string[] fields = pFields.Split(',');
                string strComma = string.Empty;
                foreach (string fld in fields)
                {
                    string fData = fld.Trim();
                    if (fData == "*" | fData.ToLower() == "it")
                    {
                        sql += strComma + "it";
                    }
                    else
                    {
                        sql += strComma + "it." + fData;
                    }
                    strComma = ",";
                }
                sql = "new(" + sql + ")";
            }
            return sql;
        }

        public static string GetDynamicFieldSql<T>(string pFields) where T : class
        {
            string sql = string.Empty;
            pFields = pFields.Trim();
            if (pFields == string.Empty | pFields == "*" | pFields == "it")
            {
                sql = "it";
            }
            else
            {
                string[] fields = pFields.Split(',');
                string strComma = string.Empty;
                foreach (string fld in fields)
                {
                    string fData = fld.Trim();
                    if (fData == "*" | fData.ToLower() == "it")
                    {
                        List<string> sFields = GetMapFieldNames<T>();
                        //List<string> sFields = GetDBMapFieldListFromCache<T>();
                        string tFields = string.Join(",", sFields.ToArray());
                        string sql1 = string.Empty;
                        string strComma1 = string.Empty;
                        foreach (string fld1 in sFields)
                        {
                            sql1 += strComma1 + "it." + fld1;
                            strComma1 = ",";
                        }
                        sql += strComma + sql1;
                    }
                    else
                    {
                        sql += strComma + "it." + fData;
                    }
                    strComma = ",";
                }
                sql = "new(" + sql + ")";
            }
            return sql;
        }

        public static string GetDynamicOrderBySql(string pOrderBy)
        {
            return GetDynamicOrderBySql(pOrderBy, string.Empty);
        }
        public static string GetDynamicOrderBySql(string pOrderBy, string pDefault)
        {
            string sql = string.Empty;

            if (pOrderBy == string.Empty)
            {
                if (pDefault != string.Empty)
                {
                    sql = "it." + pDefault;
                }
            }
            else
            {
                string[] fields = pOrderBy.Split(',');
                string strComma = string.Empty;
                foreach (string fld in fields)
                {
                    sql += strComma + "it." + fld;
                    strComma = ",";
                }
            }
            return sql;
        }


        #region conversion

        public static bool ConvertDBBoolToBool(object fData)
        {
            bool bData = false;
            if (Convert.IsDBNull(fData))
            {
                return false;
            }
            string strData = Convert.ToString(fData).Trim().ToUpper();
            if (strData == "1" | strData == "Y" | strData == "YES" | strData == "TRUE" )
            {
                bData = true;
            }
            return bData;
        }


        public static object ConvertDataToType(Type pType, object fData)
        {
            return ConvertDataToType(pType, fData, false);
        }

        public static object ConvertDataToType(Type pType, object fData, bool nullToDefault)
        {
            object cData = null;

            bool isDBNull = Convert.IsDBNull(fData);

            switch (pType.Name.ToLower())
            {
                case "bool":
                case "boolean":
                    //bit
                    //dType = "bool";

                    if (nullToDefault)
                    {
                        //cData = isDBNull ? false : Convert.ToBoolean(fData);
                        cData = isDBNull ? false : ConvertDBBoolToBool(fData);
                    }
                    else
                    {
                        //cData = Convert.ToBoolean(fData);
                        cData = ConvertDBBoolToBool(fData);
                    }

                    break;
                case "byte":
                    //tinyint
                    //dType = "byte";

                    if (nullToDefault)
                    {
                        cData = isDBNull ? 0 : Convert.ToByte(fData);
                    }
                    else
                    {

                        cData = Convert.ToByte(fData);
                    }
                    break;

                case "image":
                case "byte[]":
                    //binary, varbinary,image,timestamp   
                    //dType = "byte[]";
                    //fieldType = typeof(byte[]);
                    cData = (byte[])fData;
                    break;


                case "guid":
                    //uniqeidentifier
                    //dType = "Guid";
                    //fieldType = typeof(Guid);
                    cData = (Guid)fData;
                    break;

                case "int16":
                    //dType = "int16";
                    //fieldType = typeof(Int16);
                    if (nullToDefault)
                    {
                        cData = isDBNull ? 0 : Convert.ToInt16(fData);
                    }
                    else
                    {
                        cData = Convert.ToInt16(fData);
                    }
                    break;
                case "int":
                case "int32":
                    //int
                    //dType = "int";
                    //fieldType = typeof(Int32);
                    if (nullToDefault)
                    {
                        cData = isDBNull ? 0 : Convert.ToInt32(fData);
                    }
                    else
                    {
                        cData = Convert.ToInt32(fData);
                    }
                    break;
                case "int64":
                    //bigint
                    //dType = "int64";
                    //fieldType = typeof(Int64);
                    if (nullToDefault)
                    {
                        cData = isDBNull ? 0 : Convert.ToInt64(fData);
                    }
                    else
                    {
                        cData = Convert.ToInt64(fData);
                    }
                    break;
                case "single":
                    //real
                    //dType = "single";
                    //fieldType = typeof(Single);
                    if (nullToDefault)
                    {
                        cData = isDBNull ? 0 : Convert.ToInt64(fData);
                    }
                    else
                    {
                        cData = Convert.ToSingle(fData);
                    }
                    break;
                case "double":
                    //dType = "double";
                    //fieldType = typeof(Double);
                    if (nullToDefault)
                    {
                        cData = isDBNull ? 0 : Convert.ToDouble(fData);
                    }
                    else
                    {
                        cData = Convert.ToDouble(fData);
                    }
                    break;
                case "decimal":
                    //money, smallmoney
                    //dType = "decimal";
                    //fieldType = typeof(Decimal);
                    if (nullToDefault)
                    {
                        cData = isDBNull ? 0 : Convert.ToDecimal(fData);
                    }
                    else
                    {
                        cData = Convert.ToDecimal(fData);
                    }
                    break;
                case "datetime":
                    //datetime, smalldatetime
                    //dType = "DateTime";
                    //fieldType = typeof(DateTime);
                    cData = Convert.ToDateTime(fData);
                    break;
                case "char":
                case "string":
                    //also char,varchar,nvarchar,nchar,text,ntext
                    //dType = "string";
                    //fieldType = typeof(string);
                    cData = Convert.ToString(fData);
                    break;
                case "object":
                    //sqlvariant
                    //dType = "object";
                    //fieldType = typeof(string);
                    cData = Convert.ToString(fData);
                    break;

                default:
                    //dType = "string";
                    //fieldType = typeof(string);
                    cData = Convert.ToString(fData);
                    break;

            }

            return cData;
        }



        public static T ConvertDataRowToObject<T>(DataRow pDataRow) where T : class
        {
            return ConvertDataRowToObject<T>(pDataRow, typeof(T).GetProperties(), false, false);
        }
        public static T ConvertDataRowToObject<T>(DataRow pDataRow, PropertyInfo[] pTargetProperties) where T : class
        {
            return ConvertDataRowToObject<T>(pDataRow, pTargetProperties, false,  false);
        }

        public static T ConvertDataRowToObject<T>(DataRow pDataRow, PropertyInfo[] pTargetProperties, bool useColumnMap) where T : class
        {
            return ConvertDataRowToObject<T>(pDataRow, pTargetProperties, useColumnMap, false);
        }
        public static T ConvertDataRowToObject<T>(DataRow pDataRow, PropertyInfo[] pTargetProperties, bool useColumnMap, bool nullToDefault) where T : class
        {
            //	If either the source, or destination is null, return
            if (pDataRow == null)
                return null;

            //Type targetType = destObject.GetType();
            Type targetType = typeof(T);
            T destObject = Activator.CreateInstance<T>();


            DataTable dtSchema = pDataRow.Table;

            //	Loop through the target properties
            //foreach (PropertyInfo targetProp in targetType.GetProperties())
            foreach (PropertyInfo targetProp in pTargetProperties)
            {
                //	Get the matching property in the destination object
                //PropertyInfo targetObj = targetType.GetProperty(p.Name);
                string fieldName = string.Empty;

                if (useColumnMap)
                {
                    //List<DBMapField> listDBMap = DBMapList.GetDBMapFieldListFromCache<T>();
                    fieldName = DBMapList.GetDBMapFieldListFromCache<T>().Find(c => c.PropertyName == targetProp.Name).FieldName;
                    if (fieldName == string.Empty)
                    {
                        //no map field
                        continue;
                    }
                }
                else
                {
                    fieldName = targetProp.Name;
                }


                if (dtSchema.Columns.Contains(fieldName))
                {
                    object val = null;
                    if (IsNullableType(targetProp.PropertyType))
                    {
                        if (Convert.IsDBNull(pDataRow[fieldName]))
                        {
                            val = null;
                        }
                        else
                        {
                            val = ConvertDataToType(Nullable.GetUnderlyingType(targetProp.PropertyType), pDataRow[fieldName]);
                        }
                    }
                    else
                    {
                        if (Convert.IsDBNull(pDataRow[fieldName]))
                        {
                            //if target type is string then convert the data to string
                            if (targetProp.PropertyType == typeof(string))
                            {
                                val = ConvertDataToType(targetProp.PropertyType, pDataRow[fieldName]);
                            }
                            else
                            {
                                if (!nullToDefault)
                                {
                                    throw new NoNullAllowedException("Cannot assign null value to Non-null field : " + fieldName);
                                }
                            }
                            //if (!nullToDefault)
                            //{
                            //    throw new NoNullAllowedException("Cannot assign null value to Non-null field : " + fieldName);
                            //}

                            //no need to asign value
                        }
                        else
                        {
                            val = ConvertDataToType(targetProp.PropertyType, pDataRow[fieldName]);
                        }
                    }

                    //	Set the value in the destination
                    if (targetProp.CanWrite)
                    {
                        targetProp.SetValue(destObject, val, null);
                    }
                }
            }  //property loop
            return destObject;
        }

        public static T ConvertDbReaderRowToObject<T>(DbDataReader pDbReader) where T : class
        {
            return ConvertDbReaderRowToObject<T>(pDbReader, typeof(T).GetProperties());
        }

        public static T ConvertDbReaderRowToObject<T>(DbDataReader pDbReader, PropertyInfo[] pTargetProperties) where T : class
        {
            //	If either the source, or destination is null, return
            if (pDbReader == null)
                return null;

            //Type targetType = destObject.GetType();
            Type targetType = typeof(T);
            T destObject = Activator.CreateInstance<T>();


            DataTable dtSchema = pDbReader.GetSchemaTable();

            //	Loop through the target properties
            //foreach (PropertyInfo targetProp in targetType.GetProperties())
            foreach (PropertyInfo targetProp in pTargetProperties)
            {
                //	Get the matching property in the destination object
                //PropertyInfo targetObj = targetType.GetProperty(p.Name);
                string fieldName = targetProp.Name;

                if (dtSchema.Columns.Contains(fieldName))
                {
                    object val = null;

                    if (IsNullableType(targetProp.PropertyType))
                    {
                        if (Convert.IsDBNull(pDbReader[fieldName]))
                        {
                            val = null;
                        }
                        else
                        {
                            val = ConvertDataToType(Nullable.GetUnderlyingType(targetProp.PropertyType), pDbReader[fieldName]);
                        }
                    }
                    else
                    {
                        if (Convert.IsDBNull(pDbReader[fieldName]))
                        {
                            throw new NoNullAllowedException("Cannot assign null value to Non-null field!");
                        }
                        val = ConvertDataToType(targetProp.PropertyType, pDbReader[fieldName]);

                    }

                    //	Set the value in the destination
                    if (targetProp.CanWrite)
                    {
                        targetProp.SetValue(destObject, val, null);
                    }
                }
            }
            return destObject;
        }

        public static void ConvertObjectToDataRow(object pObj, DataRow pDataRow)
        {
            ConvertObjectToDataRow(pObj, pDataRow, pObj.GetType().GetProperties());
        }

        public static void ConvertObjectToDataRow(object pObj, DataRow pDataRow, PropertyInfo[] pObjProperties)
        {
            if (pObj == null)
            {
                throw new ArgumentNullException("Object cannot be null");
            }
            if (pDataRow == null)
            {
                throw new ArgumentNullException("Data row cannot be null");
            }

            DataTable dtSchema = pDataRow.Table;
            foreach (PropertyInfo objProp in pObjProperties)
            {
                string fieldName = objProp.Name;
                if (dtSchema.Columns.Contains(fieldName))
                {
                    object val = objProp.GetValue(objProp, null);
                    if (val == null)
                    {
                        pDataRow[fieldName] = DBNull.Value;
                    }
                    else
                    {
                        pDataRow[fieldName] = val;
                    }
                }
            }
        }

        public static DataTable ConvertObjectListToDataTable<T>(List<T> pObjList) where T : class
        {
            DataTable dtTable = CreateTable<T>();
            ConvertObjectListToDataTable<T>(pObjList, dtTable);
            return dtTable;
        }

        public static void ConvertObjectListToDataTable<T>(List<T> pObjList, DataTable pDataTable) where T : class
        {
            if (pObjList == null)
            {
                throw new ArgumentNullException("Object List cannot be null");
            }
            if (pDataTable == null)
            {
                throw new ArgumentNullException("Datatable cannot be null");
            }

            PropertyInfo[] objProperties = typeof(T).GetProperties();
            foreach (object obj in pObjList)
            {
                DataRow dRow = pDataTable.NewRow();
                ConvertObjectToDataRow(obj, dRow, objProperties);
                pDataTable.Rows.Add(dRow);
            }
        }

        public static List<T> ConvertDataTableToObjectList<T>(DataTable pData) where T : class
        {
            return ConvertDataTableToObjectList<T>(pData, false, false);
        }
        public static List<T> ConvertDataTableToObjectList<T>(DataTable pData, bool useDBColumnMap, bool nullToDefault) where T : class
        {
            List<T> cList = new List<T>();
            if (pData.Rows.Count > 0)
            {
                PropertyInfo[] targetProperties = typeof(T).GetProperties();
                foreach(DataRow dRow in pData.Rows)
                {
                    cList.Add(ConvertDataRowToObject<T>(dRow, targetProperties, useDBColumnMap , nullToDefault));
                }
            }
            return cList;
        }

        public static T ConvertObjectToType<T>(object obj) where T : class
        {
            //	If source is null, return
            if (obj == null)
                return null;

            //	Get the type of each object
            Type sourceType = obj.GetType();

            Type targetType = typeof(T);
            //T destObject = new T();

            T destObject = Activator.CreateInstance<T>();

            //	Loop through the source properties
            foreach (PropertyInfo p in sourceType.GetProperties())
            {
                //	Get the matching property in the destination object
                PropertyInfo targetObj = targetType.GetProperty(p.Name);
                //	If there is none, skip
                if (targetObj == null)
                    continue;
                if (p.GetType() != targetObj.GetType())
                    throw new InvalidCastException("Property type not matched");

                //	Set the value in the destination
                if (targetObj.CanWrite)
                {
                    if (p.GetType() != targetObj.GetType())
                        throw new InvalidCastException("Property type not matched");
                    targetObj.SetValue(destObject, p.GetValue(obj, null), null);
                }
            }
            return destObject;
        }

        public static DataTable CreateTable<T>()
        {
            Type entityType = typeof(T);
            DataTable table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, prop.PropertyType);
            }

            return table;
        }


        #endregion

    }


}
