using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Data;
using Microsoft.Win32;
using System.Diagnostics;
using System.IO;


namespace PG.Core.License
{
    public sealed class AppLicense
    {
        public enum MachineIDType
        {
            CPU_ID = 1,
            HDD_Serial = 2,
            IP_Address = 3,
            MAC_Address = 4
        }


        private static bool IsLicenseValid = false;
       
        private static int CheckCounterCurrent = 0;

        public static string FilePath = string.Empty;
        public static bool IsLicenseReset = false;
        public static int CheckCounterMax = 1000;
        
        private static string FileName = "license.dat";

        private static string LicenseDataSetName = "dsLicense";
        private static string LicenseInfoTableName = "dtLicenseInfo";
        private static string LicenseDataTableName = "dtLicenseData";


        #region private members
        //private bool m_IsWebApplication = false;

        private bool m_IsRegistrationFromFile = false;
        private bool m_IsFileRegistrationUpdated = false;
        private DateTime? m_FileRegistrationUpdateDate = null;

        private string m_FileName = string.Empty;
        private string m_UserID = string.Empty;
        private string m_Password = string.Empty;
        private string m_RegistryKeyPath = string.Empty;

        private string m_ApplicationKey = string.Empty;
        private string m_MachineKey = string.Empty;
        private string m_ProductKey = string.Empty;
        private string m_RegistrationKey = string.Empty;

        private string m_RegisterTo = string.Empty;
        
        private int m_RegistrationDay = 0;
        private int m_RemainDay = 0;
        private bool m_IsRegistrationExpired = false;

        private bool m_IsDemo = false;
        private int m_DemoDay = 0;
        private bool m_IsDemoExpired = false;
        private int m_MaxUsers = 0;

        private DateTime? m_FirstRunDate = null;
        private DateTime? m_LastRunDate = null;

        #endregion //prvate members

        #region public properties

        //private bool IsWebApplication
        //{
        //    get { return m_IsWebApplication; }
        //    set { m_IsWebApplication = value; }
        //}


        private bool IsRegistrationFromFile
        {
            get { return m_IsRegistrationFromFile; }
            set { m_IsRegistrationFromFile = value; }
        }

        private bool IsFileRegistrationUpdated
        {
            get { return m_IsFileRegistrationUpdated; }
            set { m_IsFileRegistrationUpdated = value; }
        }

        private DateTime? FileRegistrationUpdateDate
        {
            get { return m_FileRegistrationUpdateDate; }
            set { m_FileRegistrationUpdateDate = value; }
        }

        private string RegistryKeyPath
        {
            get { return m_RegistryKeyPath; }
            set { m_RegistryKeyPath = value; }
        }

        private string ApplicationKey
        {
            get { return m_ApplicationKey; }
            set { m_ApplicationKey = value; }
        }

        private string MachineKey
        {
            get { return m_MachineKey; }
            set { m_MachineKey = value; }
        }

        private string ProductKey
        {
            get { return m_ProductKey; }
            set { m_ProductKey = value; }
        }
        
        private string RegistrationKey
        {
            get { return m_RegistrationKey; }
            set { m_RegistrationKey = value; }
        }

        private string RegisterTo
        {
            get { return m_RegisterTo; }
            set { m_RegisterTo = value; }
        }

        private int RegistrationDay
        {
            get { return m_RegistrationDay; }
            set { m_RegistrationDay = value; }
        }

        private int RemainDay
        {
            get { return m_RemainDay; }
            set { m_RemainDay = value; }
        }

        private bool IsRegistrationExpired
        {
            get { return m_IsRegistrationExpired; }
            set { m_IsRegistrationExpired = value; }
        }
        
        private bool IsDemo
        {
            get { return m_IsDemo; }
            set { m_IsDemo = value; }
        }

        private int DemoDay
        {
            get { return m_DemoDay; }
            set { m_DemoDay = value; }
        }

        private bool IsDemoExpired
        {
            get { return m_IsDemoExpired; }
            set { m_IsDemoExpired = value; }
        }

        private int MaxUsers
        {
            get { return m_MaxUsers; }
            set { m_MaxUsers = value; }
        }

        private DateTime? FirstRunDate
        {
            get { return m_FirstRunDate; }
            set { m_FirstRunDate = value; }
        }

        private DateTime? LastRunDate
        {
            get { return m_LastRunDate; }
            set { m_LastRunDate = value; }
        }

        #endregion //public properties



        internal static bool ValidateLicense()
        {
            return ValidateLicense(true);
        }
        internal static bool ValidateLicense(bool isThrowError)
        {
            bool bStatus = false;

            //try
            {
                //Trace.WriteLine("Validating License...");
                bStatus = CheckLicense();
            }
            //catch(Exception e)
            //{
            //    throw new ApplicationException("Error Processing License");
            //}
            
            
            if (bStatus == false)
            {
                if (isThrowError)
                {
                    throw new ApplicationException("Application Licesne Not Valid Or Expired!");
                }
            }
            return bStatus;
        }


        internal static bool CheckLicense()
        {
            bool bStatus = false;

            //Trace.WriteLine("IsLicenseReset:" + IsLicenseReset.ToString());
            if (IsLicenseReset)
            {
                bStatus = CheckLicenseFromFile();
                CheckCounterCurrent = 1;
                IsLicenseReset = false;
                IsLicenseValid = bStatus;
            }
            else
            {
                //Trace.WriteLine("IsLicenseValid:" + IsLicenseValid.ToString());
                if (IsLicenseValid)
                {
                    CheckCounterCurrent++;
                    if (CheckCounterCurrent > CheckCounterMax)
                    {
                        bStatus = CheckLicenseFromFile();
                        IsLicenseValid = bStatus;
                        CheckCounterCurrent = 1;
                    }
                    else
                    {
                        bStatus = true;
                    }
                }
                else
                {
                    bStatus = CheckLicenseFromFile();
                    CheckCounterCurrent = 1;
                    IsLicenseValid = bStatus;
                }
            }
            return bStatus;
        }

        private static bool CheckLicenseFromFile()
        {
            bool bStatus = false;

            string fileName = FilePath + FileName;

            Trace.WriteLine("Validating License from file.");

            RemoveFileReadOnlyAttribute(fileName);

            PG.Core.Encryption.DataSetXMLEncryptor enc = new PG.Core.Encryption.DataSetXMLEncryptor(LicenseInfo.UserID, LicenseInfo.Password);
            DataSet ds = enc.ReadEncryptedXML(fileName);
            
            Trace.WriteLine("License File Loaded.");

            MachineIDType keyType = MachineIDType.CPU_ID;
            bool isLocalHost = false;
            bool isDataMatched = false;
            bool isDateValid = false;

            if (ds != null)
            {
                if (ds.Tables.Contains(LicenseInfoTableName))
                {
                    //Trace.WriteLine("Checking License Info.");
                    DataTable dt = ds.Tables[LicenseInfoTableName];
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        //keyType = (MachineIDType)Utility.Conversion.DBNullIntToZero(dr["MachineIDType"]);
                        //isLocalHost = Utility.Conversion.DBNullBoolToFalse(dr["IsLocalHost"]);

                        keyType = (MachineIDType)Convert.ToInt32(dr["MachineIDType"]);
                        isLocalHost = Convert.ToBoolean(dr["IsLocalHost"]);


                        //now check dates
                        isDateValid = IsLicenseDateValid(dr);
                        //Trace.WriteLine("Date Valid:" + isDateValid.ToString());


                        if (isDateValid)
                        {
                            if (ds.Tables.Contains(LicenseDataTableName))
                            {
                                DataTable dtData = ds.Tables[LicenseDataTableName];
                                isDataMatched = IsLicenseDataMatched(dtData, keyType, isLocalHost);
                                Trace.WriteLine("License Data Matched: " + isDataMatched.ToString());
                                if (isDataMatched)
                                {
                                    bStatus = true;
                                }
                            }
                            else
                            {
                                Trace.WriteLine("License Data Table Not Found.");
                            }
                        }
                        dr["LastCheckDate"] = DateTime.Today;
                    }

                }
                enc.WriteEncryptedXML(ds, fileName);
            }
            else
            {
                Trace.WriteLine("License File Load Failed.");
            }
            return bStatus;
        }

        private static bool IsLicenseDateValid(DataRow dr)
        {
            bool bStatus = false;

            DateTime? lastCheckDate = Utility.Conversion.DBNullDateToNull(dr["LastCheckDate"]);
            if (lastCheckDate.HasValue)
            {
                //Trace.WriteLine("Last Check Date: " + lastCheckDate.Value.ToString("dd-MMM-yyy"));
                if (DateTime.Today >= lastCheckDate.Value.Date)
                {
                    //Trace.WriteLine("DateTime.Today >= lastCheckDate.Value");
                    DateTime? expireDate = Utility.Conversion.DBNullDateToNull(dr["ExpireDate"]);
                    if (expireDate.HasValue)
                    {
                        //Trace.WriteLine("Expire Date: " + expireDate.Value.ToString("dd-MMM-yyy"));
                        if (DateTime.Today <= expireDate.Value)
                        {
                            bStatus = true;
                        }
                    }
                }
            }
            return bStatus;
        }


        private static bool IsLicenseDataMatched(DataTable dtData, MachineIDType keyType, bool isLocalHost)
        {
            bool bStatus = false;


            if (dtData != null)
            {
                    DataRow[] dRow = null;
                    switch (keyType)
                    {
                        case MachineIDType.CPU_ID:
                            string cpuID = GetSystemCPUID();
                            dRow = dtData.Select("MachineID = '" + cpuID + "'");
                            System.Diagnostics.Debug.Print("CPU ID:  " + cpuID + ", row: " + dRow.Length);
                            if (dRow.Length > 0)
                            {
                                bStatus = true;
                            }
                            break;
                        case MachineIDType.HDD_Serial:
                            string hddSerial = GetSystemHDSerial();
                            dRow = dtData.Select("MachineID = '" + hddSerial + "'");
                            if (dRow.Length > 0)
                            {
                                bStatus = true;
                            }
                            break;
                        case MachineIDType.IP_Address:
                            string ipAddress = GetSystemIPAddress();
                            dRow = dtData.Select("IPAddress1 = '" + ipAddress + "' OR IPAddress2='" + ipAddress + "'");
                            if (dRow.Length > 0)
                            {
                                bStatus = true;
                            }
                            else
                            {
                                if (isLocalHost)
                                {
                                    if (ipAddress == "127.0.0.1")
                                    {
                                        bStatus = true;
                                    }
                                }
                            }
                            break;
                        case MachineIDType.MAC_Address:
                            break;
                    }
            }


            return bStatus;
        }

        public static string GetSystemCPUID()
        {
            string cpuInfo = string.Empty;
            try
            {
                ManagementClass mc = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if (cpuInfo == String.Empty)
                    {// only return cpuInfo from first CPU
                        cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
                    }
                }
            }
            finally { }
            
            return cpuInfo;
        }
        public static string GetSystemHDSerial()
        {  
            string hdd = string.Empty;

            try
            {
                ManagementClass partionsClass = new ManagementClass("Win32_LogicalDisk");
                ManagementObjectCollection partions = partionsClass.GetInstances();
                foreach (ManagementObject partion in partions)
                {
                    hdd = Convert.ToString(partion["VolumeSerialNumber"]);

                    if (hdd != string.Empty)
                        break;
                }
            }
            finally { }
            return hdd;
        }

        public static string GetSystemIPAddress()
        {
            string ipAddress = string.Empty;
            try
            {
               ipAddress =  System.Web.HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"];
            }
            finally { }
            return ipAddress;
        }

        private static void RemoveFileReadOnlyAttribute(string fileName)
        {
            Trace.WriteLine("Validating License: remove readonly");
            if (File.Exists(fileName))
            {
                FileAttributes attributes = File.GetAttributes(fileName);

                if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    attributes = attributes & ~FileAttributes.ReadOnly;
                }
                File.SetAttributes(fileName, attributes);
            }
        }


        //public static DataSet CreateLicenseDataSet()
        //{
        //    DataSet ds = new DataSet(LicenseDataSetName);

        //    DataTable dtInfo = new DataTable(LicenseInfoTableName);

        //    dtInfo.Columns.Add(new DataColumn("IsDemo", Type.GetType("System.Boolean")));
        //    dtInfo.Columns.Add(new DataColumn("MachineKeyType", Type.GetType("System.Int32")));
        //    dtInfo.Columns.Add(new DataColumn("IsLocalHost", Type.GetType("System.Boolean")));
            
        //    dtInfo.Columns.Add(new DataColumn("MaxUserCount", Type.GetType("System.Int32")));
        //    dtInfo.Columns.Add(new DataColumn("MaxUserCount2", Type.GetType("System.Int32")));

        //    dtInfo.Columns.Add(new DataColumn("MaxDataCount", Type.GetType("System.Int32")));
        //    dtInfo.Columns.Add(new DataColumn("MaxDataCount2", Type.GetType("System.Int32")));

        //    dtInfo.Columns.Add(new DataColumn("IsExpired", Type.GetType("System.Boolean")));
        //    dtInfo.Columns.Add(new DataColumn("ExpiredDate", Type.GetType("System.DateTime")));

        //    dtInfo.Columns.Add(new DataColumn("ExpireDate", Type.GetType("System.DateTime")));
        //    dtInfo.Columns.Add(new DataColumn("LastCheckDate", Type.GetType("System.DateTime")));
        //    dtInfo.Columns.Add(new DataColumn("FirstCheckDate", Type.GetType("System.DateTime")));

        //    dtInfo.Columns.Add(new DataColumn("LicenseNo", Type.GetType("System.String")));
        //    dtInfo.Columns.Add(new DataColumn("LicenseTo", Type.GetType("System.String")));
        //    dtInfo.Columns.Add(new DataColumn("LicenseDate", Type.GetType("System.DateTime")));

        //    ds.Tables.Add(dtInfo);

        //    ///////////////////

        //    DataTable dtData = new DataTable(LicenseDataTableName);
        //    dtData.Columns.Add(new DataColumn("CPUID", Type.GetType("System.String")));
        //    dtData.Columns.Add(new DataColumn("HDDSerial", Type.GetType("System.String")));
        //    dtData.Columns.Add(new DataColumn("IPAddress1", Type.GetType("System.String")));
        //    dtData.Columns.Add(new DataColumn("IPAddress2", Type.GetType("System.String")));
        //    dtData.Columns.Add(new DataColumn("MacAddress1", Type.GetType("System.String")));
        //    dtData.Columns.Add(new DataColumn("MacAddress2", Type.GetType("System.String")));

        //    ds.Tables.Add(dtData);

        //    return ds;
        //}


        //protected RegistryKey GetSubKey(string name, string section, bool create, bool writable)
        //{
        //    //RegistryKey rootKey = Microsoft.Win32.Registry.CurrentUser;
        //    RegistryKey rootKey = Microsoft.Win32.Registry.CurrentUser;


        //    string keyName = name + "\\" + section;

        //    if (create)
        //        return rootKey.CreateSubKey(keyName);
        //    return rootKey.OpenSubKey(keyName, writable);
        //}


        //public object GetRegistryValue(string name, string section, string entry)
        //{
        //    RegistryKey subKey = GetSubKey(name, section, false, false);

        //    return (subKey == null ? null : subKey.GetValue(entry));
        //}

        //public void SetRegistryValue(string name, string section, string entry, object value)
        //{
        //    RegistryKey subKey = GetSubKey(name, section, true, true);

        //    if (value == null)
        //    {
        //        subKey.DeleteValue(entry, false);
        //    }
        //    else
        //    {
        //        subKey.SetValue(entry, value);
        //    }
        //}

        //protected void RemoveSection(string name, string section)
        //{
        //    RegistryKey rootKey = Microsoft.Win32.Registry.CurrentUser;
        //    RegistryKey key = rootKey.OpenSubKey(name, true);
        //    //if (key != null && HasSection(section))
        //    //{
        //    //    key.DeleteSubKeyTree(section);
        //    //}
        //}


        //public void CreateKey()
        //{
        //    Microsoft.Win32.RegistryKey key;
        //    key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Names");
        //    key.SetValue("Name", "Isabella");
        //    key.Close();
        //}

   

    }
}
