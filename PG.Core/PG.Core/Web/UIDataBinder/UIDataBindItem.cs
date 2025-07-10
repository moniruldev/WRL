using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace PG.Core.Web.UIDataBinder
{

    public enum DataBindMode
    {
        ObjectToFrom = 1,
        FormToObject = 2,
        Both = 3,
    }


    public class UIDataBindItem
    {
        private int m_SLNo = 0;
        private string m_BindingPropertyName = string.Empty;

        private Control m_BindingControl = null;
        private string m_BindingControlPropertyName = string.Empty;
        private string m_FormatString = string.Empty;

        private DataBindMode m_DataBindMode = DataBindMode.Both;


        public UIDataBindItem()
        {

        }

        public UIDataBindItem(string bindingPropertyName, Control bindingControl, string bindingControlPropertyName)
        {
            this.m_BindingPropertyName = bindingPropertyName;
            this.m_BindingControl = bindingControl;
            this.m_BindingControlPropertyName = bindingControlPropertyName;
        }


        public UIDataBindItem(string bindingPropertyName, Control bindingControl, string bindingControlPropertyName, DataBindMode bindMode)
        {
            this.m_BindingPropertyName = bindingPropertyName;
            this.m_BindingControl = bindingControl;
            this.m_BindingControlPropertyName = bindingControlPropertyName;
            this.m_DataBindMode = bindMode;
        }



        public UIDataBindItem(string bindingPropertyName, Control bindingControl, string bindingControlPropertyName, string formatString)
        {
            this.m_BindingPropertyName = bindingPropertyName;
            this.m_BindingControl = bindingControl;
            this.m_BindingControlPropertyName = bindingControlPropertyName;
            this.FormatString = formatString;
        }

 

        public UIDataBindItem(string bindingPropertyName, Control bindingControl, string bindingControlPropertyName, DataBindMode bindMode, string formatString)
        {
            this.m_BindingPropertyName = bindingPropertyName;
            this.m_BindingControl = bindingControl;
            this.m_BindingControlPropertyName = bindingControlPropertyName;
            this.FormatString = formatString;
            this.m_DataBindMode = bindMode;
        }

        public int SLNo
        {
            get { return m_SLNo; }
            set { m_SLNo = value; }
        }

        public string BindingPropertyName
        {
            get { return m_BindingPropertyName; }
            set { m_BindingPropertyName = value; }
        }

        public Control BindingControl
        {
            get { return m_BindingControl; }
            set { m_BindingControl = value; }
        }

        public string BindingControlPropertyName
        {
            get { return m_BindingControlPropertyName; }
            set { m_BindingControlPropertyName = value; }
        }

        public string FormatString
        {
            get { return m_FormatString; }
            set { m_FormatString = value; }
        }

        public DataBindMode DataBindMode
        {
            get { return m_DataBindMode; }
            set { m_DataBindMode = value; }
        }
    }
}
