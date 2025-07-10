using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG.Report.ReportClass.InventoryRC
{
     [Serializable]
    public class rcINV_ITEM_GROUP
    {
        #region private members

        private int m_ITEM_GROUP_ID = 0;
        private string m_ITEM_GROUP_CODE = string.Empty;
        private string m_ITEM_GROUP_NAME = string.Empty;
        private string m_ITEM_GROUP_DESC = string.Empty;
        private int m_ITEM_GROUP_ID_PARENT = 0;
        private int m_ITEM_GROUP_LEVEL = 0;
        private int m_ITEM_GROUP_SLNO = 0;
        #endregion  

        #region properties
        public int ITEM_GROUP_ID
        {
            get { return this.m_ITEM_GROUP_ID; }
            set
            {
                this.m_ITEM_GROUP_ID = value;

            }
        }
        public string ITEM_GROUP_CODE
        {
            get { return this.m_ITEM_GROUP_CODE; }
            set
            {
                this.m_ITEM_GROUP_CODE = value;

            }
        }

        public string ITEM_GROUP_NAME
        {
            get { return this.m_ITEM_GROUP_NAME; }
            set
            {
                this.m_ITEM_GROUP_NAME = value;

            }
        }


        public string ITEM_GROUP_DESC
        {
            get { return this.m_ITEM_GROUP_DESC; }
            set
            {
                this.m_ITEM_GROUP_DESC = value;

            }
        }


        public int ITEM_GROUP_ID_PARENT
        {
            get { return this.m_ITEM_GROUP_ID_PARENT; }
            set
            {
                this.m_ITEM_GROUP_ID_PARENT = value;

            }
        }



        public int ITEM_GROUP_LEVEL
        {
            get { return this.m_ITEM_GROUP_LEVEL; }
            set
            {
                this.m_ITEM_GROUP_LEVEL = value;

            }
        }



        public int ITEM_GROUP_SLNO
        {
            get { return this.m_ITEM_GROUP_SLNO; }
            set
            {
                this.m_ITEM_GROUP_SLNO = value;

            }
        }

        #endregion //properties

        #region partialProperty
        private int m_ITEM_GROUP_LEVEL_CURRENT = 0;
        public int ITEM_GROUP_LEVEL_CURRENT
        {

            get { return m_ITEM_GROUP_LEVEL_CURRENT; }
            set { this.m_ITEM_GROUP_LEVEL_CURRENT = value; }
        }

        private bool m_HasParent = false;
        public bool HasParent
        {

            get { return m_HasParent; }
            set { this.m_HasParent = value; }
        }
        private string m_ITEM_GROUP_NAME_PARENT = string.Empty;
        public string ITEM_GROUP_NAME_PARENT
        {

            get { return m_ITEM_GROUP_NAME_PARENT; }
            set { this.m_ITEM_GROUP_NAME_PARENT = value; }
        }

        private string m_ITEM_GROUP_CODE_PARENT = string.Empty;
        public string ITEM_GROUP_CODE_PARENT
        {
            get { return m_ITEM_GROUP_CODE_PARENT; }
            set { this.m_ITEM_GROUP_CODE_PARENT = value; }
        }

        private string m_ITEM_GROUP_NAME_INDENT = string.Empty;
        public string ITEM_GROUP_NAME_INDENT
        {

            get { return m_ITEM_GROUP_NAME_INDENT; }
            set { this.m_ITEM_GROUP_NAME_INDENT = value; }
        }

        private int m_ChildGroupCount = 0;
        public int ChildGroupCount
        {
            get { return m_ChildGroupCount; }
            set { m_ChildGroupCount = value; }
        }
        public string ITEM_GROUP_PARENT_KEY
        {
            get
            {
                string pKey = string.Empty;
                pKey = "grpid" + m_ITEM_GROUP_ID_PARENT.ToString();
                return pKey;
            }
        }
        #endregion

    }
}
