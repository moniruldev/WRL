using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace PG.Core.DBBase
{
    public class DBContextManagerConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("defaultDBContext", IsRequired = true)]
        public string DefaultDBContext
        {
            get { return (string)base["defaultDBContext"]; }
            set { base["defaultDBContext"] = value; }
        }

        [ConfigurationProperty("securityDBContext")]
        public string SecurityDBContext
        {
            get { return (string)base["securityDBContext"]; }
            set { base["securityDBContext"] = value; }
        }

        [ConfigurationProperty("hrmDBContext")]
        public string HrmDBContext
        {
            get { return (string)base["hrmDBContext"]; }
            set { base["hrmDBContext"] = value; }
        }


        [ConfigurationProperty("dbContext", IsRequired = true)]
        public DBContextCollectionConfiguration DBContextCollection
        {
            get { return (DBContextCollectionConfiguration)base["dbContext"]; }
            set { base["dbContext"] = value; }
        }

        public class DBContextElement : ConfigurationElement
        {
            [ConfigurationProperty("name", IsRequired = true, IsKey= true)]
            public string Name
            {
                get { return (string)base["name"]; }
                set { base["name"] = value; }
            }

            [ConfigurationProperty("dataBaseType", IsRequired = true)]
            public DatabaseTypeEnum DatabaseType
            {
                get { return (DatabaseTypeEnum)base["dataBaseType"]; }
                set { base["dataBaseType"] = value; }
            }

            [ConfigurationProperty("dbVersion")]
            public string DatabaseVersion
            {
                get { return (string)base["dbVersion"]; }
                set { base["dbVersion"] = value; }
            }

            [ConfigurationProperty("connectionStringName", IsRequired = true)]
            public string ConnectionStringName
            {
                get { return (string)base["connectionStringName"]; }
                set { base["connectionStringName"] = value; }
            }

            [ConfigurationProperty("dbSchemaName")]
            public string DBSchemaName
            {
                get { return (string)base["dbSchemaName"]; }
                set { base["dbSchemaName"] = value; }
            }

            [ConfigurationProperty("alterDBSchema")]
            public bool AlterDBSchema
            {
                get { return (bool)base["alterDBSchema"]; }
                set { base["alterDBSchema"] = value; }
            }

            [ConfigurationProperty("textCaseInsensitive")]
            public bool TextCaseInsensitive
            {
                get { return (bool)base["textCaseInsensitive"]; }
                set { base["textCaseInsensitive"] = value; }
            }

            [ConfigurationProperty("useDBColumnMap")]
            public bool UseDBColumnMap
            {
                get { return (bool)base["useDBColumnMap"]; }
                set { base["useDBColumnMap"] = value; }
            }

            [ConfigurationProperty("convertNullToDefault")]
            public bool ConvertNullToDefault
            {
                get { return (bool)base["convertNullToDefault"]; }
                set { base["convertNullToDefault"] = value; }
            }

            [ConfigurationProperty("convertBoolData")]
            public bool ConvertBoolData
            {
                get { return (bool)base["convertBoolData"]; }
                set { base["convertBoolData"] = value; }
            }

            [ConfigurationProperty("boolDataType")]
            public string BoolDataType
            {
                get { return (string)base["boolDataType"]; }
                set { base["boolDataType"] = value; }
            }
            [ConfigurationProperty("boolTrueValue")]
            public string BoolTrueValue
            {
                get { return (string)base["boolTrueValue"]; }
                set { base["boolTrueValue"] = value; }
            }
            [ConfigurationProperty("boolFalseValue")]
            public string BoolFalseValue
            {
                get { return (string)base["boolFalseValue"]; }
                set { base["boolFalseValue"] = value; }
            }

            [ConfigurationProperty("nullToDefault")]
            public bool NullToDefault
            {
                get { return (bool)base["nullToDefault"]; }
                set { base["nullToDefault"] = value; }
            }

        }

        public class DBContextCollectionConfiguration : ConfigurationElementCollection
        {
            public DBContextElement this[int index]
            {
                get
                {
                    return base.BaseGet(index) as DBContextElement;
                }
                set
                {
                    if (base.BaseGet(index) != null)
                    {
                        base.BaseRemoveAt(index);
                    }
                    this.BaseAdd(index, value);
                }
            }

            public new DBContextElement this[string name]
            {
                get
                {
                    return base.BaseGet(name) as DBContextElement;
                }
                set
                {
                    int index = -1;
                    if (base.BaseGet(name) != null)
                    {
                        index = base.BaseIndexOf(base.BaseGet(name));
                        base.BaseRemove(name);
                    }

                    if (index == -1)
                    {
                        this.BaseAdd(value);
                    }
                    else
                    {
                        this.BaseAdd(index, value);
                    }
                }
            }

            public void Add(ConfigurationElement dBContextConfig)
            {
                BaseAdd(dBContextConfig);
            }


            public void Clear()
            {
                BaseClear();
            }


            protected override ConfigurationElement CreateNewElement()
            {
                return new DBContextElement();
            }

            protected override object GetElementKey(ConfigurationElement element)
            {
                return ((DBContextElement)element).Name;
            }
        }

    }
}
