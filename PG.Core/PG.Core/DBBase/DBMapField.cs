using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace PG.Core.DBBase
{
    public partial class DBMapField
    {
        public string PropertyName { get; set; }
        public string FieldName { get; set; }
        public string DataTypeName { get; set; }
        public string DataTypeFullName { get; set; }

        public string DBFieldType { get; set; }
        public string DBFieldTypeGeneric { get; set; }

        public string DBFieldTypeSQL { get; set; }
        public string DBFieldTypeOracle { get; set; }

        public string Schema { get; set; }
        
        public bool IsPrimaryKey { get; set; }
        public bool IsDBGenerated { get; set; }

        public bool IsIdentity { get; set; }

        public bool RowGuid { get; set; }

        public int Length { get; set; }
        public int Precision { get; set; }
        public int Scale { get; set; }

        public bool Unicode { get; set; }
        public bool Nullable { get; set; }
        public bool SyncOnInsert { get; set; }
        public bool SyncOnUpdate { get; set; }

        public string SequenceName { get; set; }

        public bool LogChange { get; set; }

        public int SLNo { get; set; }

        public string Comments { get; set; }

        public string DefaultValue { get; set; }

        //public object DefaultValue { get; set; }

        public DBMapField()
        {
            //this.ClassName = string.Empty;
            //this.ClassNameFull = string.Empty;
            //this.TableName = string.Empty;
            //this.TableNameFull = string.Empty;
            this.PropertyName = string.Empty;
            this.FieldName = string.Empty;
            this.DataTypeName = string.Empty;
            this.DataTypeFullName = string.Empty;


            this.DBFieldType = string.Empty;
            this.DBFieldTypeGeneric = string.Empty;
            this.DBFieldTypeSQL = string.Empty;
            this.DBFieldTypeOracle = string.Empty;

            this.Schema = string.Empty;

            this.IsPrimaryKey = false;
            this.IsDBGenerated = false;
            this.IsIdentity = false;

            this.RowGuid = false;

            this.Unicode = false;
            
            this.Length = 0;
            this.Precision = 0;
            this.Scale = 0;
            
            this.SyncOnInsert = false;
            this.SyncOnUpdate = false;
            this.Nullable = false;

            this.SequenceName = string.Empty;

            this.LogChange = false;

            this.SLNo = 0;
            this.Comments = string.Empty;

            this.DefaultValue = string.Empty;

        }

        
    }
}
