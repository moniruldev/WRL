using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Core.DBFilters
{
    public class DBFilterPresetValue
    {
        private string m_Value = string.Empty;
        private string m_Display = string.Empty;
        private string m_Display2 = string.Empty;
        private string m_Display3 = string.Empty;

        public string Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }

        public string Display
        {
            get { return m_Display; }
            set { m_Display = value; }
        }
        public string Display2
        {
            get { return m_Display2; }
            set { m_Display2 = value; }
        }
        public string Display3
        {
            get { return m_Display3; }
            set { m_Display3 = value; }
        }

        public DBFilterPresetValue()
        {
            m_Value = string.Empty;
            m_Display = string.Empty;
            m_Display2 = string.Empty;
            m_Display3 = string.Empty;
        }
        public DBFilterPresetValue(string pValue, string pDisplay)
        {
            m_Value = pValue;
            m_Display = pDisplay;
        }
        public DBFilterPresetValue(string pValue, string pDisplay, string pDisplay2)
        {
            m_Value = pValue;
            m_Display = pDisplay;
            m_Display2 = pDisplay2;
        }

        public DBFilterPresetValue(string pValue, string pDisplay, string pDisplay2, string pDisplay3)
        {
            m_Value = pValue;
            m_Display = pDisplay;
            m_Display2 = pDisplay2;
            m_Display3 = pDisplay3;
        }

        public DBFilterPresetValue(params string[] values)
        {
            if (values.Length == 0)
            {
                throw new ArgumentException("data not supplied");
            }
            m_Value = values[0];
            if (values.Length > 1)
            {
                m_Display = values[1];
            }
            if (values.Length > 2)
            {
                m_Display2 = values[2];
            }
            if (values.Length > 3)
            {
                m_Display3 = values[3];
            }
        }
    }
}
