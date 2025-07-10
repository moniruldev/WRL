using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace PG.Core.DBBase
{
    public class DataTypeConverter
    {

        public static OracleDbType ConvertTypeToOracleDbType(Type pType)
        {
            OracleDbType oType = OracleDbType.NVarchar2;

            switch(Type.GetTypeCode(pType))
            {
                case TypeCode.String:
                    oType = OracleDbType.NVarchar2;
                    break;
                case TypeCode.Int16:
                    oType = OracleDbType.Int16;
                    break;
                case TypeCode.Int32:
                    oType = OracleDbType.Int32;
                    break;
                case TypeCode.Int64:
                    oType = OracleDbType.Int64;
                    break;
                case TypeCode.Double:
                    oType = OracleDbType.Double;
                    break;
                case TypeCode.Decimal:
                    oType = OracleDbType.Decimal;
                    break;
                case TypeCode.Byte:
                    oType = OracleDbType.Byte;
                    break;
                case TypeCode.Char:
                    oType = OracleDbType.NVarchar2;
                    break;
                case TypeCode.Single:
                    oType = OracleDbType.Single;
                    break;
                case TypeCode.Boolean:
                    oType = OracleDbType.NVarchar2;
                    break;
                case TypeCode.DateTime:
                    oType = OracleDbType.Date;
                    break;

            }
            return oType;
        }

        public static OracleDbType ConvertDbTypeToOracleDbType(DbType pType)
        {
            OracleDbType oType = OracleDbType.NVarchar2;

            switch (pType)
            {
                case DbType.String:
                    oType = OracleDbType.NVarchar2;
                    break;
                case DbType.Int16:
                    oType = OracleDbType.Int16;
                    break;
                case DbType.Int32:
                    oType = OracleDbType.Int32;
                    break;
                case DbType.Int64:
                    oType = OracleDbType.Int64;
                    break;
                case DbType.Double:
                    oType = OracleDbType.Double;
                    break;
                case DbType.Decimal:
                    oType = OracleDbType.Decimal;
                    break;
                case DbType.Byte:
                    oType = OracleDbType.Byte;
                    break;
                case DbType.StringFixedLength:
                    oType = OracleDbType.NVarchar2;
                    break;
                case DbType.Single:
                    oType = OracleDbType.Single;
                    break;
                case DbType.Boolean:
                    oType = OracleDbType.NVarchar2;
                    break;
                case DbType.Date:
                case DbType.DateTime:
                    oType = OracleDbType.Date;
                    break;

            }
            return oType;
        }

        public static DbType CheckAndConvertDbTypeForOracle(DbType pType)
        {
            DbType dType = pType;

            if (pType == DbType.Boolean)
            {
                dType = DbType.String;
            }
            return dType;
        }

        public static SqlDbType ConvertTypeToSqlDbType(Type pType)
        {
            SqlDbType sType = SqlDbType.NVarChar;

            switch (Type.GetTypeCode(pType))
            {
                case TypeCode.String:
                    sType = SqlDbType.NVarChar;
                    break;
                case TypeCode.Int16:
                    sType = SqlDbType.SmallInt;
                    break;
                case TypeCode.Int32:
                    sType = SqlDbType.Int;
                    break;
                case TypeCode.Int64:
                    sType = SqlDbType.BigInt;
                    break;
                case TypeCode.Double:
                    sType = SqlDbType.Float;
                    break;
                case TypeCode.Decimal:
                    sType = SqlDbType.Decimal;
                    break;
                case TypeCode.Byte:
                    sType = SqlDbType.VarBinary;
                    break;
                case TypeCode.Char:
                    sType = SqlDbType.NChar;
                    break;
                case TypeCode.Single:
                    sType = SqlDbType.Real;
                    break;
                case TypeCode.Boolean:
                    sType = SqlDbType.Bit;
                    break;
                case TypeCode.DateTime:
                    sType = SqlDbType.DateTime;
                    break;
            }
            return sType;
        }

        public static object ConvertBoolToOracleString(object pValue)
        {
            object val = pValue;
            //Type valType = pValue.GetType();

            if (pValue == DBNull.Value)
            {
                val = "N";
            }
            else
            {
                val = Convert.ToBoolean(pValue) ? "Y" : "N";
            }
            return val;
        }

        public static object ConvertBoolToOracleInt(object pValue)
        {
            object val = pValue;
            //Type valType = pValue.GetType();

            if (pValue == DBNull.Value)
            {
                val = 0;
            }
            else
            {
                val = Convert.ToBoolean(pValue) ? 0 : 1;
            }
            return val;
        }

        public static object CheckAndConvertBoolValue(object pValue, DBContextSettings pDBContextSettings)
        {
            object nVal = pValue;
            if (pValue != null)
            {
                TypeCode tc = Type.GetTypeCode(pValue.GetType());
                if (tc == TypeCode.Boolean)
                {
                    bool bVal = Convert.ToBoolean(pValue);
                    switch (pDBContextSettings.DatabaseType)
                    {
                        case DatabaseTypeEnum.SQLServer:
                            nVal = bVal;
                            break;
                        case DatabaseTypeEnum.MSAccess:
                            nVal = bVal;
                            break;
                        case DatabaseTypeEnum.Oracle:
                            if (pDBContextSettings.ConvertBoolData)
                            {
                                string bDataType = pDBContextSettings.BoolDataType.Trim().ToLower();
                                switch(bDataType)
                                {
                                    case "string":
                                    case "char":
                                    case "nchar":
                                    case "varchar":
                                    case "varchar2":
                                    case "nvarchar2":
                                        nVal = bVal ? pDBContextSettings.BoolTrueValue : pDBContextSettings.BoolFalseValue;
                                        break;
                                    case "number":
                                    case "int":
                                    case "integer":
                                    case "bit":
                                        nVal = bVal ? Convert.ToInt32(pDBContextSettings.BoolTrueValue) : Convert.ToInt32(pDBContextSettings.BoolFalseValue);
                                        break;
                                    default:
                                        nVal = bVal;
                                        break;
                                }
                            }
                            else
                            {
                                nVal = bVal;
                            }
                            break;
                    }
                }
            }
            return nVal;
        }




        public static void CheckAndAlterSQLString_ISNULL(StringBuilder sb, DatabaseTypeEnum pDatabaseType)
        {
            switch (pDatabaseType)
            {
                case DatabaseTypeEnum.SQLServer:
                    //sb.Replace("NVL")
                    break;
                case DatabaseTypeEnum.MSAccess:
                    //nVal = bVal;
                    break;
                case DatabaseTypeEnum.Oracle:
                    //nVal = bVal ? "Y" : "N";
                    sb.Replace("ISNULL", "NVL");
                    break;
            }
        }
    }
}
