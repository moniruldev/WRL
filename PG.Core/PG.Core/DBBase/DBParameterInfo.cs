using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PG.Core.Utility;

namespace PG.Core.DBBase
{
    public class DBParameterInfo
    {
        DbType m_DataType = DbType.String;
        ParameterDirection m_Direction = ParameterDirection.Input;
        string m_ParameterName = string.Empty;
        object m_Value = null;

        DbType m_DataType_Changed = DbType.String;
        string m_ParameterName_Changed = string.Empty;
        object m_Value_Changed = null;

        bool m_IsNameChanged = false;
        bool m_IsValueChanged = false;
        bool m_IsDataTypeChanged = false;



        public DBParameterInfo()
        {
        }

        public DBParameterInfo(string parameterName, object value)
        {
            m_ParameterName = parameterName;
            m_Value = value;

            if (value != null)
            {
                m_DataType = Conversion.ConvertTypeToDbType(value.GetType());
            }
            
            m_ParameterName_Changed = parameterName;
            m_Value_Changed = value;
            m_DataType_Changed = m_DataType;
        }

        public DBParameterInfo(string parameterName, object value, DbType dbType)
        {
            m_ParameterName = parameterName;
            m_Value = value;
            m_DataType = dbType;
            m_ParameterName_Changed = parameterName;
            m_Value_Changed = value;
            m_DataType_Changed = m_DataType;
        }

        public DBParameterInfo(string parameterName, object value, ParameterDirection direction)
        {
            m_ParameterName = parameterName;
            m_Value = value;
            if (value != null)
            {
                m_DataType = Conversion.ConvertTypeToDbType(value.GetType());
            }
            m_Direction = direction;

            m_ParameterName_Changed = parameterName;
            m_Value_Changed = value;
            m_DataType_Changed = m_DataType;
        }

        public DBParameterInfo(string parameterName, object value, DbType dbType, ParameterDirection direction)
        {
            m_ParameterName = parameterName;
            m_Value = value;
            m_DataType = dbType;
            m_Direction = direction;
            m_ParameterName_Changed = parameterName;
            m_Value_Changed = value;
            m_DataType_Changed = m_DataType;
        }



        public ParameterDirection Direction
        {
            get { return m_Direction; }
            set { m_Direction = value; }
        }

        public DbType DataType
        {
            get { return m_DataType; }
            set { 
                m_DataType = value;
                m_DataType = value;
            }
        }


        public string ParameterName
        {
            get { return m_ParameterName; }
            set { 
                m_ParameterName = value;
                m_ParameterName_Changed = value;
            }
        }

        public object Value
        {
            get { return m_Value; }
            set { 
                m_Value = value;
                m_Value_Changed = value;
                m_DataType = Conversion.ConvertTypeToDbType(value.GetType());
                m_DataType_Changed = m_DataType;
            }
        }

        public string ParameterName_Changed
        {
            get { return m_ParameterName_Changed; }
            set { m_ParameterName_Changed = value; }
        }

        public object Value_Changed
        {
            get { return m_Value_Changed; }
            set
            {
                m_Value_Changed = value;
            }
        }

        public DbType DataType_Changed
        {
            get { return m_DataType_Changed; }
            set { m_DataType_Changed = value; }
        }


        public bool IsNameChanged
        {
            get { return m_IsNameChanged; }
            set { m_IsNameChanged = value; }
        }


        public bool IsValueChanged
        {
            get { return m_IsValueChanged; }
            set { m_IsValueChanged = value; }
        }

        public bool IsDataTypeChanged
        {
            get { return m_IsDataTypeChanged; }
            set { m_IsDataTypeChanged = value; }
        }


        public void Validate(DBContextSettings pDBContextSettings)
        {
            string newParamName = DBContext.RectifyDBParamName(m_ParameterName, pDBContextSettings.DatabaseType);
            m_IsNameChanged = newParamName != m_ParameterName;
            if (m_IsNameChanged)
            {
                m_ParameterName_Changed = newParamName;
            }

            if (pDBContextSettings.DatabaseType == DatabaseTypeEnum.Oracle)
            {
                object newValue = DataTypeConverter.CheckAndConvertBoolValue(m_Value, pDBContextSettings);
                m_IsValueChanged = newValue != m_Value;
                if (m_IsValueChanged)
                {
                    m_Value_Changed = newValue;
                }

                DbType dataTypeNew = DataTypeConverter.CheckAndConvertDbTypeForOracle(m_DataType);
                m_IsDataTypeChanged = dataTypeNew != m_DataType;
                if (m_IsDataTypeChanged)
                {
                    m_DataType_Changed = dataTypeNew;
                }
                
            }


        }
    }

    public class DBParameterInfoCollection : System.Collections.CollectionBase
    {
        public void Add(DBParameterInfo dbParameterInfo)
        {
            List.Add(dbParameterInfo);
        }

        public void Add(string parameterName, object value)
        {
            List.Add(new DBParameterInfo(parameterName,value));
        }

        public void AddRange(DBParameterInfoCollection DBParameters)
        {
            if(DBParameters == null)
            {
                return;
            }

             foreach(DBParameterInfo paramInfo in DBParameters)
             {
                 List.Add(paramInfo);
             }
        }


        public void Remove(int index)
        {
            // Check to see if there is a widget at the supplied index.
            if (index > Count - 1 || index < 0)
            // If no widget exists, a messagebox is shown and the operation 
            // is cancelled.
            {
                System.Windows.Forms.MessageBox.Show("Index not valid!");
            }
            else
            {
                List.RemoveAt(index);
            }
        }

        public DBParameterInfo Item(int Index)
        {
            // The appropriate item is retrieved from the List object and
            // explicitly cast to the Widget type, then returned to the 
            // caller.
            return (DBParameterInfo)List[Index];
        }


        public void Validate(DBContextSettings pDBContextSettings)
        {
            foreach(DBParameterInfo dbParam in List)
            {
                dbParam.Validate(pDBContextSettings);
            }
        }
    }

}
