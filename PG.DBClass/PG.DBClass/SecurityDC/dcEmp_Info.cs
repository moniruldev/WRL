using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using PG.Core.DBBase;

namespace PG.DBClass.SecurityDC
{
     [Serializable]
    [DBTable(Name = "EMP_INFO_VW")]
    public partial class dcEmp_Info : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private string m_EMP_ID = string.Empty;
        private string m_FULL_NAME = string.Empty;
        private string m_NICK_NAME = string.Empty;
        private string m_FATHER_NAME = string.Empty;
        private string m_MOTHER_NAME = string.Empty;
        private string m_PA_COUNTRY = string.Empty;
        private string m_PA_DIVISION = string.Empty;
        private string m_PA_DISTRICT = string.Empty;
        private string m_PA_THANA = string.Empty;
        private string m_PA_WARD = string.Empty;
        private string m_PA_VILLAGE = string.Empty;
        private string m_PA_ROAD = string.Empty;
        private string m_PA_HOUSE = string.Empty;
        private string m_PR_COUNTRY = string.Empty;
        private string m_PR_DIVISION = string.Empty;
        private string m_PR_DISTRICT = string.Empty;
        private string m_PR_THANA = string.Empty;
        private string m_PR_WARD = string.Empty;
        private string m_PR_VILLAGE = string.Empty;
        private string m_PR_ROAD = string.Empty;
        private string m_PR_HOUSE = string.Empty;
        private string m_EMAIL_ADDRESS = string.Empty;
        private string m_RES_TELEPHONE = string.Empty;
        private string m_OF_PHONE_EXT = string.Empty;
        private string m_MOBILE = string.Empty;
        private DateTime? m_DOB = null;
        private string m_NATIONALITY = string.Empty;
        private DateTime? m_JOINING_DATE = null;
        private DateTime? m_RT_DATE = null;
        private string m_RT_STATUS = string.Empty;
        private string m_P_F_TIME = string.Empty;
        private string m_DEPARTMENT = string.Empty;
        private string m_DESIGNATION = string.Empty;
        private string m_WORKING_LOCATION = string.Empty;
        private Int16 m_PROVISION_PERIOD = 0;
        private string m_LAST_EMP_STATUS = string.Empty;
        private string m_EMP_STATUS = string.Empty;
        private string m_TIN_NO = string.Empty;
        private string m_NATIONAL_ID = string.Empty;
        private string m_DRIV_LICENSE = string.Empty;
        private string m_DRIV_LICENSE_VALID = string.Empty;
        private string m_BLOOD_GROUP = string.Empty;
        private string m_SEX = string.Empty;
        private string m_MARITAL_STATUS = string.Empty;
        private DateTime? m_MARRIGE_DATE = null;
        private string m_F_WORKER = string.Empty;
        private string m_WORK_PERMIT = string.Empty;
        private DateTime? m_ISSUE_DATE = null;
        private string m_FREEDOM_FIGHTER = string.Empty;
        private string m_SMOKING_STATUS = string.Empty;
        private string m_LEGAL_CASE = string.Empty;
        private string m_LEGAL_CASE_DETAIL = string.Empty;
        private string m_CRONIC_ILLNESS = string.Empty;
        private string m_CRONIC_ILLNESS_DETAIL = string.Empty;
        private string m_OTHER_JOB_BUSI = string.Empty;
        private string m_RELIGION = string.Empty;
        private string m_ATTENDANCE_ID = string.Empty;
        private string m_EMPLOYEE_CATEGORY = string.Empty;
        private string m_PA_COUNTRY_NAME = string.Empty;
        private string m_PR_COUNTRY_NAME = string.Empty;
        private string m_PA_DISTRICT_NAME = string.Empty;
        private string m_PR_DISTRICT_NAME = string.Empty;
        private string m_PA_DIVISION_NAME = string.Empty;
        private string m_PR_DIVISION_NAME = string.Empty;
        private string m_PA_THANA_NAME = string.Empty;
        private string m_PR_THANA_NAME = string.Empty;
        private string m_DEPT_NAME = string.Empty;
        private string m_DESIG_NAME = string.Empty;
        private string m_USE_P_CAR = string.Empty;
        private string m_EMP_PASS = string.Empty;
        private string m_LEAVE_FORWARD = string.Empty;
        private string m_DEPT_INCHARGE = string.Empty;
        private string m_ADDMIN_FORWARD = string.Empty;
        private string m_SHIFT_STATUS = string.Empty;
        private string m_COMPANY_CODE = string.Empty;
        private string m_BRANCH_CODE = string.Empty;
        private string m_LOC_CODE = string.Empty;
        private string m_CONTACT_NAME = string.Empty;
        private string m_CONTACT_ADDRESS = string.Empty;
        private string m_CONTACT_PNONE = string.Empty;
        private string m_PLACEOFBIRTH = string.Empty;
        private string m_RT_REF_NO = string.Empty;
        private DateTime? m_RT_REF_DATE = null;
        private string m_PERMAMENT_FROM_REF = string.Empty;
        private DateTime? m_PERMAMENT_FROM = null;
        private string m_USER_ID = string.Empty;
        private DateTime? m_ENTRY_DATE = null;
        private Single m_WORKING_HOURS = 0;
        private string m_OVERTIME_ELIGIBLE = string.Empty;
        private string m_ENTRY_USER = string.Empty;
        private string m_OT_ALLALLOWANCE = string.Empty;
        private string m_GRADE = string.Empty;
        private string m_FFS_STATUS = string.Empty;
        private string m_DAY_NAME = string.Empty;
        private string m_WORK_SHIFT = string.Empty;
        private string m_S_SHIFT = string.Empty;
        private string m_E_SHIFT = string.Empty;
        private string m_PIN = string.Empty;
        private string m_SALARY_TYPE = string.Empty;
        private string m_UPDATE_USER = string.Empty;
        private DateTime? m_UPDATE_DATE = null;
        private string m_AUTHORIZED_USER = string.Empty;
        private DateTime? m_AUTHORIZED_DATE = null;
        private string m_AUTHORIZED_STATUS = string.Empty;
        private string m_EMP_PWD = string.Empty;

        #endregion  //private members

        #region public events

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            _UpdateChangedList(info);
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        #endregion //public events

        #region properties


        [DBColumn(Name = "EMP_ID", Storage = "m_EMP_ID", DbType = "126")]
        public string EMP_ID
        {
            get { return this.m_EMP_ID; }
            set
            {
                this.m_EMP_ID = value;
                this.NotifyPropertyChanged("EMP_ID");
            }
        }

        [DBColumn(Name = "FULL_NAME", Storage = "m_FULL_NAME", DbType = "126")]
        public string FULL_NAME
        {
            get { return this.m_FULL_NAME; }
            set
            {
                this.m_FULL_NAME = value;
                this.NotifyPropertyChanged("FULL_NAME");
            }
        }

        [DBColumn(Name = "NICK_NAME", Storage = "m_NICK_NAME", DbType = "126")]
        public string NICK_NAME
        {
            get { return this.m_NICK_NAME; }
            set
            {
                this.m_NICK_NAME = value;
                this.NotifyPropertyChanged("NICK_NAME");
            }
        }

        [DBColumn(Name = "FATHER_NAME", Storage = "m_FATHER_NAME", DbType = "126")]
        public string FATHER_NAME
        {
            get { return this.m_FATHER_NAME; }
            set
            {
                this.m_FATHER_NAME = value;
                this.NotifyPropertyChanged("FATHER_NAME");
            }
        }

        [DBColumn(Name = "MOTHER_NAME", Storage = "m_MOTHER_NAME", DbType = "126")]
        public string MOTHER_NAME
        {
            get { return this.m_MOTHER_NAME; }
            set
            {
                this.m_MOTHER_NAME = value;
                this.NotifyPropertyChanged("MOTHER_NAME");
            }
        }

        [DBColumn(Name = "PA_COUNTRY", Storage = "m_PA_COUNTRY", DbType = "126")]
        public string PA_COUNTRY
        {
            get { return this.m_PA_COUNTRY; }
            set
            {
                this.m_PA_COUNTRY = value;
                this.NotifyPropertyChanged("PA_COUNTRY");
            }
        }

        [DBColumn(Name = "PA_DIVISION", Storage = "m_PA_DIVISION", DbType = "126")]
        public string PA_DIVISION
        {
            get { return this.m_PA_DIVISION; }
            set
            {
                this.m_PA_DIVISION = value;
                this.NotifyPropertyChanged("PA_DIVISION");
            }
        }

        [DBColumn(Name = "PA_DISTRICT", Storage = "m_PA_DISTRICT", DbType = "126")]
        public string PA_DISTRICT
        {
            get { return this.m_PA_DISTRICT; }
            set
            {
                this.m_PA_DISTRICT = value;
                this.NotifyPropertyChanged("PA_DISTRICT");
            }
        }

        [DBColumn(Name = "PA_THANA", Storage = "m_PA_THANA", DbType = "126")]
        public string PA_THANA
        {
            get { return this.m_PA_THANA; }
            set
            {
                this.m_PA_THANA = value;
                this.NotifyPropertyChanged("PA_THANA");
            }
        }

        [DBColumn(Name = "PA_WARD", Storage = "m_PA_WARD", DbType = "126")]
        public string PA_WARD
        {
            get { return this.m_PA_WARD; }
            set
            {
                this.m_PA_WARD = value;
                this.NotifyPropertyChanged("PA_WARD");
            }
        }

        [DBColumn(Name = "PA_VILLAGE", Storage = "m_PA_VILLAGE", DbType = "126")]
        public string PA_VILLAGE
        {
            get { return this.m_PA_VILLAGE; }
            set
            {
                this.m_PA_VILLAGE = value;
                this.NotifyPropertyChanged("PA_VILLAGE");
            }
        }

        [DBColumn(Name = "PA_ROAD", Storage = "m_PA_ROAD", DbType = "126")]
        public string PA_ROAD
        {
            get { return this.m_PA_ROAD; }
            set
            {
                this.m_PA_ROAD = value;
                this.NotifyPropertyChanged("PA_ROAD");
            }
        }

        [DBColumn(Name = "PA_HOUSE", Storage = "m_PA_HOUSE", DbType = "126")]
        public string PA_HOUSE
        {
            get { return this.m_PA_HOUSE; }
            set
            {
                this.m_PA_HOUSE = value;
                this.NotifyPropertyChanged("PA_HOUSE");
            }
        }

        [DBColumn(Name = "PR_COUNTRY", Storage = "m_PR_COUNTRY", DbType = "126")]
        public string PR_COUNTRY
        {
            get { return this.m_PR_COUNTRY; }
            set
            {
                this.m_PR_COUNTRY = value;
                this.NotifyPropertyChanged("PR_COUNTRY");
            }
        }

        [DBColumn(Name = "PR_DIVISION", Storage = "m_PR_DIVISION", DbType = "126")]
        public string PR_DIVISION
        {
            get { return this.m_PR_DIVISION; }
            set
            {
                this.m_PR_DIVISION = value;
                this.NotifyPropertyChanged("PR_DIVISION");
            }
        }

        [DBColumn(Name = "PR_DISTRICT", Storage = "m_PR_DISTRICT", DbType = "126")]
        public string PR_DISTRICT
        {
            get { return this.m_PR_DISTRICT; }
            set
            {
                this.m_PR_DISTRICT = value;
                this.NotifyPropertyChanged("PR_DISTRICT");
            }
        }

        [DBColumn(Name = "PR_THANA", Storage = "m_PR_THANA", DbType = "126")]
        public string PR_THANA
        {
            get { return this.m_PR_THANA; }
            set
            {
                this.m_PR_THANA = value;
                this.NotifyPropertyChanged("PR_THANA");
            }
        }

        [DBColumn(Name = "PR_WARD", Storage = "m_PR_WARD", DbType = "126")]
        public string PR_WARD
        {
            get { return this.m_PR_WARD; }
            set
            {
                this.m_PR_WARD = value;
                this.NotifyPropertyChanged("PR_WARD");
            }
        }

        [DBColumn(Name = "PR_VILLAGE", Storage = "m_PR_VILLAGE", DbType = "126")]
        public string PR_VILLAGE
        {
            get { return this.m_PR_VILLAGE; }
            set
            {
                this.m_PR_VILLAGE = value;
                this.NotifyPropertyChanged("PR_VILLAGE");
            }
        }

        [DBColumn(Name = "PR_ROAD", Storage = "m_PR_ROAD", DbType = "126")]
        public string PR_ROAD
        {
            get { return this.m_PR_ROAD; }
            set
            {
                this.m_PR_ROAD = value;
                this.NotifyPropertyChanged("PR_ROAD");
            }
        }

        [DBColumn(Name = "PR_HOUSE", Storage = "m_PR_HOUSE", DbType = "126")]
        public string PR_HOUSE
        {
            get { return this.m_PR_HOUSE; }
            set
            {
                this.m_PR_HOUSE = value;
                this.NotifyPropertyChanged("PR_HOUSE");
            }
        }

        [DBColumn(Name = "EMAIL_ADDRESS", Storage = "m_EMAIL_ADDRESS", DbType = "126")]
        public string EMAIL_ADDRESS
        {
            get { return this.m_EMAIL_ADDRESS; }
            set
            {
                this.m_EMAIL_ADDRESS = value;
                this.NotifyPropertyChanged("EMAIL_ADDRESS");
            }
        }

        [DBColumn(Name = "RES_TELEPHONE", Storage = "m_RES_TELEPHONE", DbType = "126")]
        public string RES_TELEPHONE
        {
            get { return this.m_RES_TELEPHONE; }
            set
            {
                this.m_RES_TELEPHONE = value;
                this.NotifyPropertyChanged("RES_TELEPHONE");
            }
        }

        [DBColumn(Name = "OF_PHONE_EXT", Storage = "m_OF_PHONE_EXT", DbType = "126")]
        public string OF_PHONE_EXT
        {
            get { return this.m_OF_PHONE_EXT; }
            set
            {
                this.m_OF_PHONE_EXT = value;
                this.NotifyPropertyChanged("OF_PHONE_EXT");
            }
        }

        [DBColumn(Name = "MOBILE", Storage = "m_MOBILE", DbType = "126")]
        public string MOBILE
        {
            get { return this.m_MOBILE; }
            set
            {
                this.m_MOBILE = value;
                this.NotifyPropertyChanged("MOBILE");
            }
        }

        [DBColumn(Name = "DOB", Storage = "m_DOB", DbType = "106")]
        public DateTime? DOB
        {
            get { return this.m_DOB; }
            set
            {
                this.m_DOB = value;
                this.NotifyPropertyChanged("DOB");
            }
        }

        [DBColumn(Name = "NATIONALITY", Storage = "m_NATIONALITY", DbType = "126")]
        public string NATIONALITY
        {
            get { return this.m_NATIONALITY; }
            set
            {
                this.m_NATIONALITY = value;
                this.NotifyPropertyChanged("NATIONALITY");
            }
        }

        [DBColumn(Name = "JOINING_DATE", Storage = "m_JOINING_DATE", DbType = "106")]
        public DateTime? JOINING_DATE
        {
            get { return this.m_JOINING_DATE; }
            set
            {
                this.m_JOINING_DATE = value;
                this.NotifyPropertyChanged("JOINING_DATE");
            }
        }

        [DBColumn(Name = "RT_DATE", Storage = "m_RT_DATE", DbType = "106")]
        public DateTime? RT_DATE
        {
            get { return this.m_RT_DATE; }
            set
            {
                this.m_RT_DATE = value;
                this.NotifyPropertyChanged("RT_DATE");
            }
        }

        [DBColumn(Name = "RT_STATUS", Storage = "m_RT_STATUS", DbType = "126")]
        public string RT_STATUS
        {
            get { return this.m_RT_STATUS; }
            set
            {
                this.m_RT_STATUS = value;
                this.NotifyPropertyChanged("RT_STATUS");
            }
        }

        [DBColumn(Name = "P_F_TIME", Storage = "m_P_F_TIME", DbType = "126")]
        public string P_F_TIME
        {
            get { return this.m_P_F_TIME; }
            set
            {
                this.m_P_F_TIME = value;
                this.NotifyPropertyChanged("P_F_TIME");
            }
        }

        [DBColumn(Name = "DEPARTMENT", Storage = "m_DEPARTMENT", DbType = "126")]
        public string DEPARTMENT
        {
            get { return this.m_DEPARTMENT; }
            set
            {
                this.m_DEPARTMENT = value;
                this.NotifyPropertyChanged("DEPARTMENT");
            }
        }

        [DBColumn(Name = "DESIGNATION", Storage = "m_DESIGNATION", DbType = "126")]
        public string DESIGNATION
        {
            get { return this.m_DESIGNATION; }
            set
            {
                this.m_DESIGNATION = value;
                this.NotifyPropertyChanged("DESIGNATION");
            }
        }

        [DBColumn(Name = "WORKING_LOCATION", Storage = "m_WORKING_LOCATION", DbType = "126")]
        public string WORKING_LOCATION
        {
            get { return this.m_WORKING_LOCATION; }
            set
            {
                this.m_WORKING_LOCATION = value;
                this.NotifyPropertyChanged("WORKING_LOCATION");
            }
        }

        [DBColumn(Name = "PROVISION_PERIOD", Storage = "m_PROVISION_PERIOD", DbType = "111")]
        public Int16 PROVISION_PERIOD
        {
            get { return this.m_PROVISION_PERIOD; }
            set
            {
                this.m_PROVISION_PERIOD = value;
                this.NotifyPropertyChanged("PROVISION_PERIOD");
            }
        }

        [DBColumn(Name = "LAST_EMP_STATUS", Storage = "m_LAST_EMP_STATUS", DbType = "126")]
        public string LAST_EMP_STATUS
        {
            get { return this.m_LAST_EMP_STATUS; }
            set
            {
                this.m_LAST_EMP_STATUS = value;
                this.NotifyPropertyChanged("LAST_EMP_STATUS");
            }
        }

        [DBColumn(Name = "EMP_STATUS", Storage = "m_EMP_STATUS", DbType = "126")]
        public string EMP_STATUS
        {
            get { return this.m_EMP_STATUS; }
            set
            {
                this.m_EMP_STATUS = value;
                this.NotifyPropertyChanged("EMP_STATUS");
            }
        }

        [DBColumn(Name = "TIN_NO", Storage = "m_TIN_NO", DbType = "126")]
        public string TIN_NO
        {
            get { return this.m_TIN_NO; }
            set
            {
                this.m_TIN_NO = value;
                this.NotifyPropertyChanged("TIN_NO");
            }
        }

        [DBColumn(Name = "NATIONAL_ID", Storage = "m_NATIONAL_ID", DbType = "126")]
        public string NATIONAL_ID
        {
            get { return this.m_NATIONAL_ID; }
            set
            {
                this.m_NATIONAL_ID = value;
                this.NotifyPropertyChanged("NATIONAL_ID");
            }
        }

        [DBColumn(Name = "DRIV_LICENSE", Storage = "m_DRIV_LICENSE", DbType = "126")]
        public string DRIV_LICENSE
        {
            get { return this.m_DRIV_LICENSE; }
            set
            {
                this.m_DRIV_LICENSE = value;
                this.NotifyPropertyChanged("DRIV_LICENSE");
            }
        }

        [DBColumn(Name = "DRIV_LICENSE_VALID", Storage = "m_DRIV_LICENSE_VALID", DbType = "126")]
        public string DRIV_LICENSE_VALID
        {
            get { return this.m_DRIV_LICENSE_VALID; }
            set
            {
                this.m_DRIV_LICENSE_VALID = value;
                this.NotifyPropertyChanged("DRIV_LICENSE_VALID");
            }
        }

        [DBColumn(Name = "BLOOD_GROUP", Storage = "m_BLOOD_GROUP", DbType = "126")]
        public string BLOOD_GROUP
        {
            get { return this.m_BLOOD_GROUP; }
            set
            {
                this.m_BLOOD_GROUP = value;
                this.NotifyPropertyChanged("BLOOD_GROUP");
            }
        }

        [DBColumn(Name = "SEX", Storage = "m_SEX", DbType = "126")]
        public string SEX
        {
            get { return this.m_SEX; }
            set
            {
                this.m_SEX = value;
                this.NotifyPropertyChanged("SEX");
            }
        }

        [DBColumn(Name = "MARITAL_STATUS", Storage = "m_MARITAL_STATUS", DbType = "126")]
        public string MARITAL_STATUS
        {
            get { return this.m_MARITAL_STATUS; }
            set
            {
                this.m_MARITAL_STATUS = value;
                this.NotifyPropertyChanged("MARITAL_STATUS");
            }
        }

        [DBColumn(Name = "MARRIGE_DATE", Storage = "m_MARRIGE_DATE", DbType = "106")]
        public DateTime? MARRIGE_DATE
        {
            get { return this.m_MARRIGE_DATE; }
            set
            {
                this.m_MARRIGE_DATE = value;
                this.NotifyPropertyChanged("MARRIGE_DATE");
            }
        }

        [DBColumn(Name = "F_WORKER", Storage = "m_F_WORKER", DbType = "126")]
        public string F_WORKER
        {
            get { return this.m_F_WORKER; }
            set
            {
                this.m_F_WORKER = value;
                this.NotifyPropertyChanged("F_WORKER");
            }
        }

        [DBColumn(Name = "WORK_PERMIT", Storage = "m_WORK_PERMIT", DbType = "126")]
        public string WORK_PERMIT
        {
            get { return this.m_WORK_PERMIT; }
            set
            {
                this.m_WORK_PERMIT = value;
                this.NotifyPropertyChanged("WORK_PERMIT");
            }
        }

        [DBColumn(Name = "ISSUE_DATE", Storage = "m_ISSUE_DATE", DbType = "106")]
        public DateTime? ISSUE_DATE
        {
            get { return this.m_ISSUE_DATE; }
            set
            {
                this.m_ISSUE_DATE = value;
                this.NotifyPropertyChanged("ISSUE_DATE");
            }
        }

        [DBColumn(Name = "FREEDOM_FIGHTER", Storage = "m_FREEDOM_FIGHTER", DbType = "126")]
        public string FREEDOM_FIGHTER
        {
            get { return this.m_FREEDOM_FIGHTER; }
            set
            {
                this.m_FREEDOM_FIGHTER = value;
                this.NotifyPropertyChanged("FREEDOM_FIGHTER");
            }
        }

        [DBColumn(Name = "SMOKING_STATUS", Storage = "m_SMOKING_STATUS", DbType = "126")]
        public string SMOKING_STATUS
        {
            get { return this.m_SMOKING_STATUS; }
            set
            {
                this.m_SMOKING_STATUS = value;
                this.NotifyPropertyChanged("SMOKING_STATUS");
            }
        }

        [DBColumn(Name = "LEGAL_CASE", Storage = "m_LEGAL_CASE", DbType = "126")]
        public string LEGAL_CASE
        {
            get { return this.m_LEGAL_CASE; }
            set
            {
                this.m_LEGAL_CASE = value;
                this.NotifyPropertyChanged("LEGAL_CASE");
            }
        }

        [DBColumn(Name = "LEGAL_CASE_DETAIL", Storage = "m_LEGAL_CASE_DETAIL", DbType = "126")]
        public string LEGAL_CASE_DETAIL
        {
            get { return this.m_LEGAL_CASE_DETAIL; }
            set
            {
                this.m_LEGAL_CASE_DETAIL = value;
                this.NotifyPropertyChanged("LEGAL_CASE_DETAIL");
            }
        }

        [DBColumn(Name = "CRONIC_ILLNESS", Storage = "m_CRONIC_ILLNESS", DbType = "126")]
        public string CRONIC_ILLNESS
        {
            get { return this.m_CRONIC_ILLNESS; }
            set
            {
                this.m_CRONIC_ILLNESS = value;
                this.NotifyPropertyChanged("CRONIC_ILLNESS");
            }
        }

        [DBColumn(Name = "CRONIC_ILLNESS_DETAIL", Storage = "m_CRONIC_ILLNESS_DETAIL", DbType = "126")]
        public string CRONIC_ILLNESS_DETAIL
        {
            get { return this.m_CRONIC_ILLNESS_DETAIL; }
            set
            {
                this.m_CRONIC_ILLNESS_DETAIL = value;
                this.NotifyPropertyChanged("CRONIC_ILLNESS_DETAIL");
            }
        }

        [DBColumn(Name = "OTHER_JOB_BUSI", Storage = "m_OTHER_JOB_BUSI", DbType = "126")]
        public string OTHER_JOB_BUSI
        {
            get { return this.m_OTHER_JOB_BUSI; }
            set
            {
                this.m_OTHER_JOB_BUSI = value;
                this.NotifyPropertyChanged("OTHER_JOB_BUSI");
            }
        }

        [DBColumn(Name = "RELIGION", Storage = "m_RELIGION", DbType = "126")]
        public string RELIGION
        {
            get { return this.m_RELIGION; }
            set
            {
                this.m_RELIGION = value;
                this.NotifyPropertyChanged("RELIGION");
            }
        }

        [DBColumn(Name = "ATTENDANCE_ID", Storage = "m_ATTENDANCE_ID", DbType = "126")]
        public string ATTENDANCE_ID
        {
            get { return this.m_ATTENDANCE_ID; }
            set
            {
                this.m_ATTENDANCE_ID = value;
                this.NotifyPropertyChanged("ATTENDANCE_ID");
            }
        }

        [DBColumn(Name = "EMPLOYEE_CATEGORY", Storage = "m_EMPLOYEE_CATEGORY", DbType = "126")]
        public string EMPLOYEE_CATEGORY
        {
            get { return this.m_EMPLOYEE_CATEGORY; }
            set
            {
                this.m_EMPLOYEE_CATEGORY = value;
                this.NotifyPropertyChanged("EMPLOYEE_CATEGORY");
            }
        }

        [DBColumn(Name = "PA_COUNTRY_NAME", Storage = "m_PA_COUNTRY_NAME", DbType = "126")]
        public string PA_COUNTRY_NAME
        {
            get { return this.m_PA_COUNTRY_NAME; }
            set
            {
                this.m_PA_COUNTRY_NAME = value;
                this.NotifyPropertyChanged("PA_COUNTRY_NAME");
            }
        }

        [DBColumn(Name = "PR_COUNTRY_NAME", Storage = "m_PR_COUNTRY_NAME", DbType = "126")]
        public string PR_COUNTRY_NAME
        {
            get { return this.m_PR_COUNTRY_NAME; }
            set
            {
                this.m_PR_COUNTRY_NAME = value;
                this.NotifyPropertyChanged("PR_COUNTRY_NAME");
            }
        }

        [DBColumn(Name = "PA_DISTRICT_NAME", Storage = "m_PA_DISTRICT_NAME", DbType = "126")]
        public string PA_DISTRICT_NAME
        {
            get { return this.m_PA_DISTRICT_NAME; }
            set
            {
                this.m_PA_DISTRICT_NAME = value;
                this.NotifyPropertyChanged("PA_DISTRICT_NAME");
            }
        }

        [DBColumn(Name = "PR_DISTRICT_NAME", Storage = "m_PR_DISTRICT_NAME", DbType = "126")]
        public string PR_DISTRICT_NAME
        {
            get { return this.m_PR_DISTRICT_NAME; }
            set
            {
                this.m_PR_DISTRICT_NAME = value;
                this.NotifyPropertyChanged("PR_DISTRICT_NAME");
            }
        }

        [DBColumn(Name = "PA_DIVISION_NAME", Storage = "m_PA_DIVISION_NAME", DbType = "126")]
        public string PA_DIVISION_NAME
        {
            get { return this.m_PA_DIVISION_NAME; }
            set
            {
                this.m_PA_DIVISION_NAME = value;
                this.NotifyPropertyChanged("PA_DIVISION_NAME");
            }
        }

        [DBColumn(Name = "PR_DIVISION_NAME", Storage = "m_PR_DIVISION_NAME", DbType = "126")]
        public string PR_DIVISION_NAME
        {
            get { return this.m_PR_DIVISION_NAME; }
            set
            {
                this.m_PR_DIVISION_NAME = value;
                this.NotifyPropertyChanged("PR_DIVISION_NAME");
            }
        }

        [DBColumn(Name = "PA_THANA_NAME", Storage = "m_PA_THANA_NAME", DbType = "126")]
        public string PA_THANA_NAME
        {
            get { return this.m_PA_THANA_NAME; }
            set
            {
                this.m_PA_THANA_NAME = value;
                this.NotifyPropertyChanged("PA_THANA_NAME");
            }
        }

        [DBColumn(Name = "PR_THANA_NAME", Storage = "m_PR_THANA_NAME", DbType = "126")]
        public string PR_THANA_NAME
        {
            get { return this.m_PR_THANA_NAME; }
            set
            {
                this.m_PR_THANA_NAME = value;
                this.NotifyPropertyChanged("PR_THANA_NAME");
            }
        }

        [DBColumn(Name = "DEPT_NAME", Storage = "m_DEPT_NAME", DbType = "126")]
        public string DEPT_NAME
        {
            get { return this.m_DEPT_NAME; }
            set
            {
                this.m_DEPT_NAME = value;
                this.NotifyPropertyChanged("DEPT_NAME");
            }
        }

        [DBColumn(Name = "DESIG_NAME", Storage = "m_DESIG_NAME", DbType = "126")]
        public string DESIG_NAME
        {
            get { return this.m_DESIG_NAME; }
            set
            {
                this.m_DESIG_NAME = value;
                this.NotifyPropertyChanged("DESIG_NAME");
            }
        }

        [DBColumn(Name = "USE_P_CAR", Storage = "m_USE_P_CAR", DbType = "126")]
        public string USE_P_CAR
        {
            get { return this.m_USE_P_CAR; }
            set
            {
                this.m_USE_P_CAR = value;
                this.NotifyPropertyChanged("USE_P_CAR");
            }
        }

        [DBColumn(Name = "EMP_PASS", Storage = "m_EMP_PASS", DbType = "126")]
        public string EMP_PASS
        {
            get { return this.m_EMP_PASS; }
            set
            {
                this.m_EMP_PASS = value;
                this.NotifyPropertyChanged("EMP_PASS");
            }
        }

        [DBColumn(Name = "LEAVE_FORWARD", Storage = "m_LEAVE_FORWARD", DbType = "104")]
        public string LEAVE_FORWARD
        {
            get { return this.m_LEAVE_FORWARD; }
            set
            {
                this.m_LEAVE_FORWARD = value;
                this.NotifyPropertyChanged("LEAVE_FORWARD");
            }
        }

        [DBColumn(Name = "DEPT_INCHARGE", Storage = "m_DEPT_INCHARGE", DbType = "126")]
        public string DEPT_INCHARGE
        {
            get { return this.m_DEPT_INCHARGE; }
            set
            {
                this.m_DEPT_INCHARGE = value;
                this.NotifyPropertyChanged("DEPT_INCHARGE");
            }
        }

        [DBColumn(Name = "ADDMIN_FORWARD", Storage = "m_ADDMIN_FORWARD", DbType = "126")]
        public string ADDMIN_FORWARD
        {
            get { return this.m_ADDMIN_FORWARD; }
            set
            {
                this.m_ADDMIN_FORWARD = value;
                this.NotifyPropertyChanged("ADDMIN_FORWARD");
            }
        }

        [DBColumn(Name = "SHIFT_STATUS", Storage = "m_SHIFT_STATUS", DbType = "126")]
        public string SHIFT_STATUS
        {
            get { return this.m_SHIFT_STATUS; }
            set
            {
                this.m_SHIFT_STATUS = value;
                this.NotifyPropertyChanged("SHIFT_STATUS");
            }
        }

        [DBColumn(Name = "COMPANY_CODE", Storage = "m_COMPANY_CODE", DbType = "126")]
        public string COMPANY_CODE
        {
            get { return this.m_COMPANY_CODE; }
            set
            {
                this.m_COMPANY_CODE = value;
                this.NotifyPropertyChanged("COMPANY_CODE");
            }
        }

        [DBColumn(Name = "BRANCH_CODE", Storage = "m_BRANCH_CODE", DbType = "126")]
        public string BRANCH_CODE
        {
            get { return this.m_BRANCH_CODE; }
            set
            {
                this.m_BRANCH_CODE = value;
                this.NotifyPropertyChanged("BRANCH_CODE");
            }
        }

        [DBColumn(Name = "LOC_CODE", Storage = "m_LOC_CODE", DbType = "126")]
        public string LOC_CODE
        {
            get { return this.m_LOC_CODE; }
            set
            {
                this.m_LOC_CODE = value;
                this.NotifyPropertyChanged("LOC_CODE");
            }
        }

        [DBColumn(Name = "CONTACT_NAME", Storage = "m_CONTACT_NAME", DbType = "126")]
        public string CONTACT_NAME
        {
            get { return this.m_CONTACT_NAME; }
            set
            {
                this.m_CONTACT_NAME = value;
                this.NotifyPropertyChanged("CONTACT_NAME");
            }
        }

        [DBColumn(Name = "CONTACT_ADDRESS", Storage = "m_CONTACT_ADDRESS", DbType = "126")]
        public string CONTACT_ADDRESS
        {
            get { return this.m_CONTACT_ADDRESS; }
            set
            {
                this.m_CONTACT_ADDRESS = value;
                this.NotifyPropertyChanged("CONTACT_ADDRESS");
            }
        }

        [DBColumn(Name = "CONTACT_PNONE", Storage = "m_CONTACT_PNONE", DbType = "126")]
        public string CONTACT_PNONE
        {
            get { return this.m_CONTACT_PNONE; }
            set
            {
                this.m_CONTACT_PNONE = value;
                this.NotifyPropertyChanged("CONTACT_PNONE");
            }
        }

        [DBColumn(Name = "PLACEOFBIRTH", Storage = "m_PLACEOFBIRTH", DbType = "126")]
        public string PLACEOFBIRTH
        {
            get { return this.m_PLACEOFBIRTH; }
            set
            {
                this.m_PLACEOFBIRTH = value;
                this.NotifyPropertyChanged("PLACEOFBIRTH");
            }
        }

        [DBColumn(Name = "RT_REF_NO", Storage = "m_RT_REF_NO", DbType = "126")]
        public string RT_REF_NO
        {
            get { return this.m_RT_REF_NO; }
            set
            {
                this.m_RT_REF_NO = value;
                this.NotifyPropertyChanged("RT_REF_NO");
            }
        }

        [DBColumn(Name = "RT_REF_DATE", Storage = "m_RT_REF_DATE", DbType = "106")]
        public DateTime? RT_REF_DATE
        {
            get { return this.m_RT_REF_DATE; }
            set
            {
                this.m_RT_REF_DATE = value;
                this.NotifyPropertyChanged("RT_REF_DATE");
            }
        }

        [DBColumn(Name = "PERMAMENT_FROM_REF", Storage = "m_PERMAMENT_FROM_REF", DbType = "126")]
        public string PERMAMENT_FROM_REF
        {
            get { return this.m_PERMAMENT_FROM_REF; }
            set
            {
                this.m_PERMAMENT_FROM_REF = value;
                this.NotifyPropertyChanged("PERMAMENT_FROM_REF");
            }
        }

        [DBColumn(Name = "PERMAMENT_FROM", Storage = "m_PERMAMENT_FROM", DbType = "106")]
        public DateTime? PERMAMENT_FROM
        {
            get { return this.m_PERMAMENT_FROM; }
            set
            {
                this.m_PERMAMENT_FROM = value;
                this.NotifyPropertyChanged("PERMAMENT_FROM");
            }
        }

        [DBColumn(Name = "USER_ID", Storage = "m_USER_ID", DbType = "126")]
        public string USER_ID
        {
            get { return this.m_USER_ID; }
            set
            {
                this.m_USER_ID = value;
                this.NotifyPropertyChanged("USER_ID");
            }
        }

        [DBColumn(Name = "ENTRY_DATE", Storage = "m_ENTRY_DATE", DbType = "106")]
        public DateTime? ENTRY_DATE
        {
            get { return this.m_ENTRY_DATE; }
            set
            {
                this.m_ENTRY_DATE = value;
                this.NotifyPropertyChanged("ENTRY_DATE");
            }
        }

        [DBColumn(Name = "WORKING_HOURS", Storage = "m_WORKING_HOURS", DbType = "122")]
        public Single WORKING_HOURS
        {
            get { return this.m_WORKING_HOURS; }
            set
            {
                this.m_WORKING_HOURS = value;
                this.NotifyPropertyChanged("WORKING_HOURS");
            }
        }

        [DBColumn(Name = "OVERTIME_ELIGIBLE", Storage = "m_OVERTIME_ELIGIBLE", DbType = "126")]
        public string OVERTIME_ELIGIBLE
        {
            get { return this.m_OVERTIME_ELIGIBLE; }
            set
            {
                this.m_OVERTIME_ELIGIBLE = value;
                this.NotifyPropertyChanged("OVERTIME_ELIGIBLE");
            }
        }

        [DBColumn(Name = "ENTRY_USER", Storage = "m_ENTRY_USER", DbType = "126")]
        public string ENTRY_USER
        {
            get { return this.m_ENTRY_USER; }
            set
            {
                this.m_ENTRY_USER = value;
                this.NotifyPropertyChanged("ENTRY_USER");
            }
        }

        [DBColumn(Name = "OT_ALLALLOWANCE", Storage = "m_OT_ALLALLOWANCE", DbType = "126")]
        public string OT_ALLALLOWANCE
        {
            get { return this.m_OT_ALLALLOWANCE; }
            set
            {
                this.m_OT_ALLALLOWANCE = value;
                this.NotifyPropertyChanged("OT_ALLALLOWANCE");
            }
        }

        [DBColumn(Name = "GRADE", Storage = "m_GRADE", DbType = "126")]
        public string GRADE
        {
            get { return this.m_GRADE; }
            set
            {
                this.m_GRADE = value;
                this.NotifyPropertyChanged("GRADE");
            }
        }

        [DBColumn(Name = "FFS_STATUS", Storage = "m_FFS_STATUS", DbType = "126")]
        public string FFS_STATUS
        {
            get { return this.m_FFS_STATUS; }
            set
            {
                this.m_FFS_STATUS = value;
                this.NotifyPropertyChanged("FFS_STATUS");
            }
        }

        [DBColumn(Name = "DAY_NAME", Storage = "m_DAY_NAME", DbType = "126")]
        public string DAY_NAME
        {
            get { return this.m_DAY_NAME; }
            set
            {
                this.m_DAY_NAME = value;
                this.NotifyPropertyChanged("DAY_NAME");
            }
        }

        [DBColumn(Name = "WORK_SHIFT", Storage = "m_WORK_SHIFT", DbType = "126")]
        public string WORK_SHIFT
        {
            get { return this.m_WORK_SHIFT; }
            set
            {
                this.m_WORK_SHIFT = value;
                this.NotifyPropertyChanged("WORK_SHIFT");
            }
        }

        [DBColumn(Name = "S_SHIFT", Storage = "m_S_SHIFT", DbType = "126")]
        public string S_SHIFT
        {
            get { return this.m_S_SHIFT; }
            set
            {
                this.m_S_SHIFT = value;
                this.NotifyPropertyChanged("S_SHIFT");
            }
        }

        [DBColumn(Name = "E_SHIFT", Storage = "m_E_SHIFT", DbType = "126")]
        public string E_SHIFT
        {
            get { return this.m_E_SHIFT; }
            set
            {
                this.m_E_SHIFT = value;
                this.NotifyPropertyChanged("E_SHIFT");
            }
        }

        [DBColumn(Name = "PIN", Storage = "m_PIN", DbType = "126")]
        public string PIN
        {
            get { return this.m_PIN; }
            set
            {
                this.m_PIN = value;
                this.NotifyPropertyChanged("PIN");
            }
        }

        [DBColumn(Name = "SALARY_TYPE", Storage = "m_SALARY_TYPE", DbType = "126")]
        public string SALARY_TYPE
        {
            get { return this.m_SALARY_TYPE; }
            set
            {
                this.m_SALARY_TYPE = value;
                this.NotifyPropertyChanged("SALARY_TYPE");
            }
        }

        [DBColumn(Name = "UPDATE_USER", Storage = "m_UPDATE_USER", DbType = "126")]
        public string UPDATE_USER
        {
            get { return this.m_UPDATE_USER; }
            set
            {
                this.m_UPDATE_USER = value;
                this.NotifyPropertyChanged("UPDATE_USER");
            }
        }

        [DBColumn(Name = "UPDATE_DATE", Storage = "m_UPDATE_DATE", DbType = "106")]
        public DateTime? UPDATE_DATE
        {
            get { return this.m_UPDATE_DATE; }
            set
            {
                this.m_UPDATE_DATE = value;
                this.NotifyPropertyChanged("UPDATE_DATE");
            }
        }

        [DBColumn(Name = "AUTHORIZED_USER", Storage = "m_AUTHORIZED_USER", DbType = "126")]
        public string AUTHORIZED_USER
        {
            get { return this.m_AUTHORIZED_USER; }
            set
            {
                this.m_AUTHORIZED_USER = value;
                this.NotifyPropertyChanged("AUTHORIZED_USER");
            }
        }

        [DBColumn(Name = "AUTHORIZED_DATE", Storage = "m_AUTHORIZED_DATE", DbType = "106")]
        public DateTime? AUTHORIZED_DATE
        {
            get { return this.m_AUTHORIZED_DATE; }
            set
            {
                this.m_AUTHORIZED_DATE = value;
                this.NotifyPropertyChanged("AUTHORIZED_DATE");
            }
        }

        [DBColumn(Name = "AUTHORIZED_STATUS", Storage = "m_AUTHORIZED_STATUS", DbType = "126")]
        public string AUTHORIZED_STATUS
        {
            get { return this.m_AUTHORIZED_STATUS; }
            set
            {
                this.m_AUTHORIZED_STATUS = value;
                this.NotifyPropertyChanged("AUTHORIZED_STATUS");
            }
        }

        [DBColumn(Name = "EMP_PWD", Storage = "m_EMP_PWD", DbType = "126")]
        public string EMP_PWD
        {
            get { return this.m_EMP_PWD; }
            set
            {
                this.m_EMP_PWD = value;
                this.NotifyPropertyChanged("EMP_PWD");
            }
        }

        #endregion //properties
    }
}
