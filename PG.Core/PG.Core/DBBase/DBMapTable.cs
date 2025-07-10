using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace PG.Core.DBBase
{
    public partial class DBMapTable
    {
        public string Name { get; set; }
        public string TableName { get; set; }
        public string SchemaName { get; set; }
        public string SequenceName { get; set; }

        public bool LogChange { get; set; }

        public string Comments { get; set; }

        public bool IsDBTableLink { get; set; }

        //public object DefaultValue { get; set; }

        public DBMapTable()
        {
            //this.ClassName = string.Empty;
            //this.ClassNameFull = string.Empty;
            //this.TableName = string.Empty;
            //this.TableNameFull = string.Empty;
            this.Name = string.Empty;
            this.TableName = string.Empty;
            this.SchemaName = string.Empty;
            this.Comments = string.Empty;
            this.LogChange = false;
            this.SequenceName = string.Empty;
            this.IsDBTableLink = false;
        }

        
    }
}
