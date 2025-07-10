using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Core.DBFilters
{
    public class DBFilterField
    {
        public int SLNo { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FieldName { get; set; }
        public string TableName { get; set; }
        public DBFilterDataTypeEnum FieldDataType { get; set; }
        public bool IsPreValue { get; set; }
        public List<DBFilterPresetValue> DBFilterPresetValues { get; set; }
        public int PresetValueDisplayColumnCount { get; set; }
        public List<string> PresetValueDispalyColumnNames { get; set; }
        public List<int> PresetValueDisplayColumnWidths { get; set; }
        public int PresetValueDisplayWindowWidth { get; set; }
        public int PresetValueDisplayWindowHeight { get; set; }

        public string FormatString { get; set; }

        public DBFilterField()
        {
            this.SLNo = 0;
            this.Name = string.Empty;
            this.FieldName = string.Empty;
            this.Description = string.Empty;
            this.TableName = string.Empty;
            this.FieldDataType = DBFilterDataTypeEnum.String;
            this.IsPreValue = false;
            this.DBFilterPresetValues = new List<DBFilterPresetValue>();
            this.PresetValueDisplayColumnCount = 1;
            this.PresetValueDispalyColumnNames = new List<string>();
            this.PresetValueDisplayColumnWidths = new List<int>();
            this.PresetValueDisplayWindowHeight = 400;
            this.PresetValueDisplayWindowWidth = 300;
            this.FormatString = string.Empty;
        }
    }
}
