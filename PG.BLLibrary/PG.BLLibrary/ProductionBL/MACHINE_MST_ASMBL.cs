using PG.Core.DBBase;
using PG.DBClass.ProductionDC;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG.BLLibrary.ProductionBL
{
    public class MACHINE_MST_ASMBL
    {

        public static string GetMachineAssemblyListString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" Select *  ");
            sb.Append(" FROM MACHINE_MST_ASM mst ");
            sb.Append(" Where 1=1 ");
            return sb.ToString();
        }
        public static DataLoadOptions MACHINE_MST_ASMLoadOptions()
        {
            DataLoadOptions dlo = new DataLoadOptions();
            //dlo.LoadWith<DBClass.dcMACHINE_MST_ASM>(obj => obj.relatedclassname);
            return dlo;
        }
        public static List<dcMACHINE_MST_ASM> GetMACHINE_MST_ASMList()
        {
            return GetMACHINE_MST_ASMList(null, null);
        }
        public static List<dcMACHINE_MST_ASM> GetMACHINE_MST_ASMList(DBContext dc)
        {
            return GetMACHINE_MST_ASMList(null, dc);
        }
        public static List<dcMACHINE_MST_ASM> GetMACHINE_MST_ASMList(DBQuery dbq, DBContext dc)
        {
            List<dcMACHINE_MST_ASM> cObjList = new List<dcMACHINE_MST_ASM>();
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    if (dbq == null)
                    {
                        dbq = new DBQuery();
                        //dbq.OrderBy = "YearStartDate Desc";
                    }
                    cObjList = DBQuery.ExecuteDBQuery<dcMACHINE_MST_ASM>(dbq, dc);
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObjList;
        }
        public static dcMACHINE_MST_ASM GetMACHINE_MST_ASMByID(int pMACHINE_MST_ASMID)
        {
            return GetMACHINE_MST_ASMByID(pMACHINE_MST_ASMID, null);
        }
        public static dcMACHINE_MST_ASM GetMACHINE_MST_ASMByID(int pMACHINE_MST_ASMID, DBContext dc)
        {
            dcMACHINE_MST_ASM cObj = null;
            bool isDCInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                using (DataContext dataContext = dc.NewDataContext())
                {
                    var result = (from c in dataContext.GetTable<dcMACHINE_MST_ASM>()
                                  //where c.MACHINE_MST_ASMID == pMACHINE_MST_ASMID
                                  select c).ToList();
                    if (result.Count() > 0)
                    {
                        cObj = result.First();
                    }
                }
            }
            catch { throw; }
            finally { DBContextManager.ReleaseDBContext(ref dc, isDCInit); }
            return cObj;
        }

        public static int Insert(dcMACHINE_MST_ASM cObj)
        {
            return Insert(cObj, null);
        }

        public static int Insert(dcMACHINE_MST_ASM cObj, DBContext dc)
        {
            bool isDCInit = false;
            int id = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                id = dc.DoInsert<dcMACHINE_MST_ASM>(cObj, true);
                //if (id > 0) { cObj.MACHINE_MST_ASMID = id; }
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return id;
        }

        public static bool Update(dcMACHINE_MST_ASM cObj)
        {
            return Update(cObj, null);
        }

        public static bool Update(dcMACHINE_MST_ASM cObj, DBContext dc)
        {
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoUpdate<dcMACHINE_MST_ASM>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static bool Delete(int pMACHINE_MST_ASMID)
        {
            return Delete(pMACHINE_MST_ASMID, null);
        }
        public static bool Delete(int pMACHINE_MST_ASMID, DBContext dc)
        {
            dcMACHINE_MST_ASM cObj = new dcMACHINE_MST_ASM();
            //cObj.MACHINE_MST_ASMID = pMACHINE_MST_ASMID;
            bool isDCInit = false;
            int cnt = 0;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            using (DataContext dataContext = dc.NewDataContext())
            {
                cnt = dc.DoDelete<dcMACHINE_MST_ASM>(cObj);
            }
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            return cnt > 0;
        }

        public static int Save(dcMACHINE_MST_ASM cObj, bool isAdd)
        {
            return Save(cObj, isAdd, null);
        }

        public static int Save(dcMACHINE_MST_ASM cObj, bool isAdd, DBContext dc)
        {
            //cObj._RecordState = isAdd ? Interwave.Core.DBClass.RecordStateEnum.Added : Interwave.Core.DBClass.RecordStateEnum.Edited;
            return Save(cObj, dc);
        }

        public static int Save(dcMACHINE_MST_ASM cObj)
        {
            return Save(cObj, null);
        }

        public static int Save(dcMACHINE_MST_ASM cObj, DBContext dc)
        {
            int newID = 0;
            bool isDCInit = false;
            bool isTransInit = false;
            try
            {
                isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
                isTransInit = dc.StartTransaction();
                using (DataContext dataContext = dc.NewDataContext())
                {

                    switch (cObj._RecordState)
                    {
                        //case Interwave.Core.DBClass.RecordStateEnum.Added:
                        //    newID = Insert(cObj, dc);
                        //    break;
                        //case Interwave.Core.DBClass.RecordStateEnum.Edited:
                        //    if (Update(cObj, dc))
                        //    {
                        //        newID = cObj.MACHINE_MST_ASMID;
                        //    }
                        //    break;
                        //case Interwave.Core.DBClass.RecordStateEnum.Deleted:
                        //    if (Delete(cObj.MACHINE_MST_ASMID, dc))
                        //    {
                        //        newID = 1;
                        //    }
                        //    break;
                        default:
                            break;
                    }

                    if (newID > 0)
                    {
                        bool bStatus = false;

                        ///code list save logic here

                        bStatus = true;
                        if (bStatus)
                        {
                            dc.CommitTransaction(isTransInit);
                        }
                    }
                }
            }
            catch
            {
                dc.RollbackTransaction(isTransInit);
                throw;
            }
            finally
            {
                DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            }
            return newID;
        }

        public static bool SaveList(List<dcMACHINE_MST_ASM> detList)
        {
            return SaveList(detList, null);
        }

        public static bool SaveList(List<dcMACHINE_MST_ASM> detList, DBContext dc)
        {
            bool bStatus = false;
            bool isDCInit = false;
            bool isTransInit = false;
            isDCInit = DBContextManager.CheckAndInitDBContext(ref dc);
            isTransInit = dc.StartTransaction();
            foreach (dcMACHINE_MST_ASM oDet in detList)
            {
                switch (oDet._RecordState)
                {
                    //case Interwave.Core.DBClass.RecordStateEnum.Added:
                    //    int a = Insert(oDet, dc);
                    //    break;
                    //case Interwave.Core.DBClass.RecordStateEnum.Edited:
                    //    bool e = Update(oDet, dc);
                    //    break;
                    //case Interwave.Core.DBClass.RecordStateEnum.Deleted:
                    //    bool d = Delete(oDet.MACHINE_MST_ASMID, dc);
                    //    break;
                    default:
                        break;
                }
            }
            dc.CommitTransaction(isTransInit);
            DBContextManager.ReleaseDBContext(ref dc, isDCInit);
            bStatus = true;
            return bStatus;
        }
    }
}
