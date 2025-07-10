using PG.Core.DBBase;
using PG.DBClass.InventoryDC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PG.DBClass.ProductionDC
{
    [DBTable(Name = "PRODUCTION_DTL")]
    public partial class dcPRODUCTION_DTL : DBBaseClass, INotifyPropertyChanged
    {
        #region private members

        private int m_PROD_DTL_ID = 0;
        private int m_PROD_MST_ID = 0;
        private int m_ITEM_GROUP_ID = 0;
        private int m_ITEM_ID = 0;
        private decimal m_ITEM_QTY = 0;
        private int m_UOM_ID = 0;
        private decimal m_ITEM_WEIGHT = 0;
        private int m_WEIGHT_UOM_ID = 0;
        private int m_BOM_ID = 0;
        private int m_MACHINE_ID = 0;
        private int m_PANEL_UOM_ID = 0;
        private decimal m_ITEM_PANEL_QTY = 0;
        private int m_SLNO = 0;
        private string m_REMARKS = "";
        private int m_PANEL_PC = 0;
        private string m_GRID_BATCH = "";

        // Formation Required
        private int m_AMPERE = 0;
        private int m_CYCLETIME = 0;
        private decimal m_SULFURIC_GRAVITY = 0;
        private int m_TEMPARATURE = 0;
        private int m_UNFORMED_QTY = 0;
        private int m_FORMED_QTY = 0;
        private string m_PASTING_BATCH = "";
        private string m_FORMATION_STARTTIME = "";
        private DateTime? m_FORMATION_OFFDATE = null;
        private string m_FORMATION_OFFTIME = "";
        private decimal m_REJECT_QTY = 0;
        // Formation End
        private string m_OPERATOR_ID = "";
        private int m_USED_BAR_PC = 0;
        private int m_BAR_TYPE = 0;
        private decimal m_USED_QTY_KG = 0;
        private decimal m_BAR_WEIGHT = 0;

        //--Packing---//
        private string m_IS_PACKING = String.Empty;
        private string m_PACK_FINISHED_BATCH = String.Empty;

        //--------Sulphation-------------------//
        private string m_FILLING_BATCH = "";
        private string m_SULPHATION_STARTTIME = "";
        private DateTime? m_SULPHATION_OFFDATE = null;
        private string m_SULPHATION_OFFTIME = "";

        //--------------------Used Item Name----------------------------//
        private string m_USED_ITEM_NAME = "";
        private string m_USED_ITEM_ID = "";


        private string m_IS_UNFORMED = "N";
        private string m_PROCESSTYPE = String.Empty;

        private decimal m_ITEM_STANDARD_WEIGHT_KG = 0;

        private decimal m_SML_ITEM_PC = 0;

        private int m_ITEM_SPECIFICATION_ID = 0;
        private decimal? m_CHARGED_QTY = 0;
        private decimal? m_PACKING_QUANTITY = 0;
        
        private decimal m_ITEM_WEIGHT_PASTE_KG = 0;
        private decimal m_ITEM_STD_PASTE_KG=0;

        private int m_MRB_PLATE_ID_N = 0;
        private decimal m_MRB_PLATE_QTY_N = 0;
        private decimal m_MRB_PLATE_WEIGHT_N = 0;

        private int m_MRB_PLATE_ID= 0;
        private decimal m_MRB_PLATE_QTY= 0;
        private decimal m_MRB_PLATE_WEIGHT= 0;
        private decimal m_SCRAP_BATTERY_WEIGHT = 0;
        private string m_PROD_BATCH_NO_DTL = "";
        private int m_STLM_ID = 0;
        private int m_RM_SUP_ID = 0;
        private int m_FALSE_LUG_ITEM_ID = 0;
        private decimal m_MIXER_BATCH_QTY = 0;
        private string m_MANUAL_SLNO = "";
        
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


        [DBColumn(Name = "PROD_DTL_ID", Storage = "m_PROD_DTL_ID", DbType = "Int NOT NULL IDENTITY", IsDbGenerated = true, SyncOnInsert = true, IsPrimaryKey = true, IsIdentity = true)]
        public int PROD_DTL_ID
        {
            get { return this.m_PROD_DTL_ID; }
            set
            {
                this.m_PROD_DTL_ID = value;
                this.NotifyPropertyChanged("PROD_DTL_ID");
            }
        }

        [DBColumn(Name = "PROD_MST_ID", Storage = "m_PROD_MST_ID", DbType = "107")]
        public int PROD_MST_ID
        {
            get { return this.m_PROD_MST_ID; }
            set
            {
                this.m_PROD_MST_ID = value;
                this.NotifyPropertyChanged("PROD_MST_ID");
            }
        }

        [DBColumn(Name = "ITEM_ID", Storage = "m_ITEM_ID", DbType = "107")]
        public int ITEM_ID
        {
            get { return this.m_ITEM_ID; }
            set
            {
                this.m_ITEM_ID = value;
                this.NotifyPropertyChanged("ITEM_ID");
            }
        }

        [DBColumn(Name = "ITEM_QTY", Storage = "m_ITEM_QTY", DbType = "107")]
        public decimal ITEM_QTY
        {
            get { return this.m_ITEM_QTY; }
            set
            {
                this.m_ITEM_QTY = value;
                this.NotifyPropertyChanged("ITEM_QTY");
            }
        }
    
        [DBColumn(Name = "ITEM_WEIGHT", Storage = "m_ITEM_WEIGHT", DbType = "107")]
        public decimal ITEM_WEIGHT
        {
            get { return this.m_ITEM_WEIGHT; }
            set
            {
                this.m_ITEM_WEIGHT = value;
                this.NotifyPropertyChanged("ITEM_WEIGHT");
            }
        }
        [DBColumn(Name = "WEIGHT_UOM_ID", Storage = "m_WEIGHT_UOM_ID", DbType = "107")]
        public int WEIGHT_UOM_ID
        {
            get { return this.m_WEIGHT_UOM_ID; }
            set
            {
                this.m_WEIGHT_UOM_ID = value;
                this.NotifyPropertyChanged("WEIGHT_UOM_ID");
            }
        }



        [DBColumn(Name = "BOM_ID", Storage = "m_BOM_ID", DbType = "107")]
        public int BOM_ID
        {
            get { return this.m_BOM_ID; }
            set
            {
                this.m_BOM_ID = value;
                this.NotifyPropertyChanged("BOM_ID");
            }
        }


        [DBColumn(Name = "UOM_ID", Storage = "m_UOM_ID", DbType = "107")]
        public int UOM_ID
        {
            get { return this.m_UOM_ID; }
            set
            {
                this.m_UOM_ID = value;
                this.NotifyPropertyChanged("UOM_ID");
            }
        }

        [DBColumn(Name = "MACHINE_ID", Storage = "m_MACHINE_ID", DbType = "107")]
        public int MACHINE_ID
        {
            get { return this.m_MACHINE_ID; }
            set
            {
                this.m_MACHINE_ID = value;
                this.NotifyPropertyChanged("MACHINE_ID");
            }
        }

        [DBColumn(Name = "PANEL_UOM_ID", Storage = "m_PANEL_UOM_ID", DbType = "107")]
        public int PANEL_UOM_ID
        {
            get { return this.m_PANEL_UOM_ID; }
            set
            {
                this.m_PANEL_UOM_ID = value;
                this.NotifyPropertyChanged("PANEL_UOM_ID");
            }
        }

        [DBColumn(Name = "ITEM_PANEL_QTY", Storage = "m_ITEM_PANEL_QTY", DbType = "107")]
        public decimal ITEM_PANEL_QTY
        {
            get { return this.m_ITEM_PANEL_QTY; }
            set
            {
                this.m_ITEM_PANEL_QTY = value;
                this.NotifyPropertyChanged("ITEM_PANEL_QTY");
            }
        }

        [DBColumn(Name = "SLNO", Storage = "m_SLNO", DbType = "107")]
        public int SLNO
        {
            get { return this.m_SLNO; }
            set
            {
                this.m_SLNO = value;
                this.NotifyPropertyChanged("SLNO");
            }
        }



        [DBColumn(Name = "ITEM_GROUP_ID", Storage = "m_ITEM_GROUP_ID", DbType = "107")]
        public int ITEM_GROUP_ID
        {
            get { return this.m_ITEM_GROUP_ID; }
            set
            {
                this.m_ITEM_GROUP_ID = value;
                this.NotifyPropertyChanged("ITEM_GROUP_ID");
            }
        }

        [DBColumn(Name = "REMARKS", Storage = "m_REMARKS", DbType = "107")]
        public string REMARKS
        {
            get { return this.m_REMARKS; }
            set
            {
                this.m_REMARKS = value;
                this.NotifyPropertyChanged("REMARKS");
            }
        }



        [DBColumn(Name = "PANEL_PC", Storage = "m_PANEL_PC", DbType = "107")]
        public int PANEL_PC
        {
            get { return this.m_PANEL_PC; }
            set
            {
                this.m_PANEL_PC = value;
                this.NotifyPropertyChanged("PANEL_PC");
            }
        }

        [DBColumn(Name = "OPERATOR_ID", Storage = "m_OPERATOR_ID", DbType = "107")]
        public string OPERATOR_ID
        {
            get { return this.m_OPERATOR_ID; }
            set
            {
                this.m_OPERATOR_ID = value;
                this.NotifyPropertyChanged("OPERATOR_ID");
            }
        }

        [DBColumn(Name = "USED_BAR_PC", Storage = "m_USED_BAR_PC", DbType = "107")]
        public int USED_BAR_PC
        {
            get { return this.m_USED_BAR_PC; }
            set
            {
                this.m_USED_BAR_PC = value;
                this.NotifyPropertyChanged("USED_BAR_PC");
            }
        }

        [DBColumn(Name = "BAR_TYPE", Storage = "m_BAR_TYPE", DbType = "107")]
        public int BAR_TYPE
        {
            get { return this.m_BAR_TYPE; }
            set
            {
                this.m_BAR_TYPE = value;
                this.NotifyPropertyChanged("BAR_TYPE");
            }
        }

        [DBColumn(Name = "USED_QTY_KG", Storage = "m_USED_QTY_KG", DbType = "107")]
        public decimal USED_QTY_KG
        {
            get { return this.m_USED_QTY_KG; }
            set
            {
                this.m_USED_QTY_KG = value;
                this.NotifyPropertyChanged("USED_QTY_KG");
            }
        }



        [DBColumn(Name = "BAR_WEIGHT", Storage = "m_BAR_WEIGHT", DbType = "107")]
        public decimal BAR_WEIGHT
        {
            get { return this.m_BAR_WEIGHT; }
            set
            {
                this.m_BAR_WEIGHT = value;
                this.NotifyPropertyChanged("BAR_WEIGHT");
            }
        }


        [DBColumn(Name = "GRID_BATCH", Storage = "m_GRID_BATCH", DbType = "107")]
        public string GRID_BATCH
        {
            get { return this.m_GRID_BATCH; }
            set
            {
                this.m_GRID_BATCH = value;
                this.NotifyPropertyChanged("GRID_BATCH");
            }
        }


        // Formation Required Start //
        [DBColumn(Name = "AMPERE", Storage = "m_AMPERE", DbType = "107")]
        public int AMPERE
        {
            get { return this.m_AMPERE; }
            set
            {
                this.m_AMPERE = value;
                this.NotifyPropertyChanged("AMPERE");
            }
        }

        [DBColumn(Name = "CYCLETIME", Storage = "m_CYCLETIME", DbType = "107")]
        public int CYCLETIME
        {
            get { return this.m_CYCLETIME; }
            set
            {
                this.m_CYCLETIME = value;
                this.NotifyPropertyChanged("CYCLETIME");
            }
        }

        [DBColumn(Name = "SULFURIC_GRAVITY", Storage = "m_SULFURIC_GRAVITY", DbType = "107")]
        public decimal SULFURIC_GRAVITY
        {
            get { return this.m_SULFURIC_GRAVITY; }
            set
            {
                this.m_SULFURIC_GRAVITY = value;
                this.NotifyPropertyChanged("SULFURIC_GRAVITY");
            }
        }

        [DBColumn(Name = "TEMPARATURE", Storage = "m_TEMPARATURE", DbType = "107")]
        public int TEMPARATURE
        {
            get { return this.m_TEMPARATURE; }
            set
            {
                this.m_TEMPARATURE = value;
                this.NotifyPropertyChanged("TEMPARATURE");
            }
        }

        [DBColumn(Name = "UNFORMED_QTY", Storage = "m_UNFORMED_QTY", DbType = "107")]
        public int UNFORMED_QTY
        {
            get { return this.m_UNFORMED_QTY; }
            set
            {
                this.m_UNFORMED_QTY = value;
                this.NotifyPropertyChanged("UNFORMED_QTY");
            }
        }

        [DBColumn(Name = "FORMED_QTY", Storage = "m_FORMED_QTY", DbType = "107")]
        public int FORMED_QTY
        {
            get { return this.m_FORMED_QTY; }
            set
            {
                this.m_FORMED_QTY = value;
                this.NotifyPropertyChanged("FORMED_QTY");
            }
        }

        [DBColumn(Name = "PASTING_BATCH", Storage = "m_PASTING_BATCH", DbType = "107")]
        public string PASTING_BATCH
        {
            get { return this.m_PASTING_BATCH; }
            set
            {
                this.m_PASTING_BATCH = value;
                this.NotifyPropertyChanged("PASTING_BATCH");
            }
        }



        [DBColumn(Name = "FORMATION_STARTTIME", Storage = "m_FORMATION_STARTTIME", DbType = "107")]
        public string FORMATION_STARTTIME
        {
            get { return this.m_FORMATION_STARTTIME; }
            set
            {
                this.m_FORMATION_STARTTIME = value;
                this.NotifyPropertyChanged("FORMATION_STARTTIME");
            }
        }

        [DBColumn(Name = "FORMATION_OFFDATE", Storage = "m_FORMATION_OFFDATE", DbType = "107")]
        public DateTime? FORMATION_OFFDATE
        {
            get { return this.m_FORMATION_OFFDATE; }
            set
            {
                this.m_FORMATION_OFFDATE = value;
                this.NotifyPropertyChanged("FORMATION_OFFDATE");
            }
        }

        [DBColumn(Name = "FORMATION_OFFTIME", Storage = "m_FORMATION_OFFTIME", DbType = "107")]
        public string FORMATION_OFFTIME
        {
            get { return this.m_FORMATION_OFFTIME; }
            set
            {
                this.m_FORMATION_OFFTIME = value;
                this.NotifyPropertyChanged("FORMATION_OFFTIME");
            }
        }


        [DBColumn(Name = "REJECT_QTY", Storage = "m_REJECT_QTY", DbType = "107")]
        public decimal REJECT_QTY
        {
            get { return this.m_REJECT_QTY; }
            set
            {
                this.m_REJECT_QTY = value;
                this.NotifyPropertyChanged("REJECT_QTY");
            }
        }




        // Formation Required End //


        [DBColumn(Name = "IS_PACKING", Storage = "m_IS_PACKING", DbType = "107")]
        public string IS_PACKING
        {
            get { return this.m_IS_PACKING; }
            set
            {
                this.m_IS_PACKING = value;
                this.NotifyPropertyChanged("IS_PACKING");
            }
        }



        [DBColumn(Name = "PACK_FINISHED_BATCH", Storage = "m_PACK_FINISHED_BATCH", DbType = "107")]
        public string PACK_FINISHED_BATCH
        {
            get { return this.m_PACK_FINISHED_BATCH; }
            set
            {
                this.m_PACK_FINISHED_BATCH = value;
                this.NotifyPropertyChanged("PACK_FINISHED_BATCH");
            }
        }


        //----------------Sulphation----------------------//




        [DBColumn(Name = "FILLING_BATCH", Storage = "m_FILLING_BATCH", DbType = "107")]
        public string FILLING_BATCH
        {
            get { return this.m_FILLING_BATCH; }
            set
            {
                this.m_FILLING_BATCH = value;
                this.NotifyPropertyChanged("FILLING_BATCH");
            }
        }



        [DBColumn(Name = "SULPHATION_STARTTIME", Storage = "m_SULPHATION_STARTTIME", DbType = "107")]
        public string SULPHATION_STARTTIME
        {
            get { return this.m_SULPHATION_STARTTIME; }
            set
            {
                this.m_SULPHATION_STARTTIME = value;
                this.NotifyPropertyChanged("SULPHATION_STARTTIME");
            }
        }

        [DBColumn(Name = "SULPHATION_OFFDATE", Storage = "m_SULPHATION_OFFDATE", DbType = "107")]
        public DateTime? SULPHATION_OFFDATE
        {
            get { return this.m_SULPHATION_OFFDATE; }
            set
            {
                this.m_SULPHATION_OFFDATE = value;
                this.NotifyPropertyChanged("SULPHATION_OFFDATE");
            }
        }

        [DBColumn(Name = "SULPHATION_OFFTIME", Storage = "m_SULPHATION_OFFTIME", DbType = "107")]
        public string SULPHATION_OFFTIME
        {
            get { return this.m_SULPHATION_OFFTIME; }
            set
            {
                this.m_SULPHATION_OFFTIME = value;
                this.NotifyPropertyChanged("SULPHATION_OFFTIME");
            }
        }


        [DBColumn(Name = "USED_ITEM_ID", Storage = "m_USED_ITEM_ID", DbType = "107")]
        public string USED_ITEM_ID
        {
            get { return this.m_USED_ITEM_ID; }
            set
            {
                this.m_USED_ITEM_ID = value;
                this.NotifyPropertyChanged("USED_ITEM_ID");
            }
        }

        [DBColumn(Name = "IS_UNFORMED", Storage = "m_IS_UNFORMED", DbType = "107")]
        public string IS_UNFORMED
        {
            get { return this.m_IS_UNFORMED; }
            set
            {
                this.m_IS_UNFORMED = value;
                this.NotifyPropertyChanged("IS_UNFORMED");
            }
        }

        [DBColumn(Name = "PROCESSTYPE", Storage = "m_PROCESSTYPE", DbType = "107")]
        public string PROCESSTYPE
        {
            get { return this.m_PROCESSTYPE; }
            set
            {
                this.m_PROCESSTYPE = value;
                this.NotifyPropertyChanged("PROCESSTYPE");
            }
        }


      [DBColumn(Name = "ITEM_STANDARD_WEIGHT_KG", Storage = "m_ITEM_STANDARD_WEIGHT_KG", DbType = "107")]
        public decimal ITEM_STANDARD_WEIGHT_KG
        {
            get { return this.m_ITEM_STANDARD_WEIGHT_KG; }
            set
            {
                this.m_ITEM_STANDARD_WEIGHT_KG = value;
                this.NotifyPropertyChanged("ITEM_STANDARD_WEIGHT_KG");
            }
        }


      [DBColumn(Name = "SML_ITEM_PC", Storage = "m_SML_ITEM_PC", DbType = "107")]
      public decimal SML_ITEM_PC
      {
          get { return this.m_SML_ITEM_PC; }
          set
          {
              this.m_SML_ITEM_PC = value;
              this.NotifyPropertyChanged("SML_ITEM_PC");
          }
      }

      [DBColumn(Name = "ITEM_SPECIFICATION_ID", Storage = "m_ITEM_SPECIFICATION_ID", DbType = "107")]
      public int ITEM_SPECIFICATION_ID
      {
          get { return this.m_ITEM_SPECIFICATION_ID; }
          set
          {
              this.m_ITEM_SPECIFICATION_ID = value;
              this.NotifyPropertyChanged("ITEM_SPECIFICATION_ID");
          }
      }





      [DBColumn(Name = "ITEM_STD_PASTE_KG", Storage = "m_ITEM_STD_PASTE_KG", DbType = "107")]
      public decimal ITEM_STD_PASTE_KG
      {
          get { return this.m_ITEM_STD_PASTE_KG; }
          set
          {
              this.m_ITEM_STD_PASTE_KG = value;
              this.NotifyPropertyChanged("ITEM_STD_PASTE_KG");
          }
      }


      [DBColumn(Name = "ITEM_WEIGHT_PASTE_KG", Storage = "m_ITEM_WEIGHT_PASTE_KG", DbType = "107")]
      public decimal ITEM_WEIGHT_PASTE_KG
      {
          get { return this.m_ITEM_WEIGHT_PASTE_KG; }
          set
          {
              this.m_ITEM_WEIGHT_PASTE_KG = value;
              this.NotifyPropertyChanged("ITEM_WEIGHT_PASTE_KG");
          }
      }

        [DBColumn(Name = "CHARGED_QTY", Storage = "m_CHARGED_QTY", DbType = "107")]
        public decimal? CHARGED_QTY
        {
            get { return this.m_CHARGED_QTY; }
            set
            {
                this.m_CHARGED_QTY = value;
                this.NotifyPropertyChanged("CHARGED_QTY");
            }
        }

        [DBColumn(Name = "PACKING_QUANTITY", Storage = "m_PACKING_QUANTITY", DbType = "107")]
        public decimal? PACKING_QUANTITY
        {
            get { return this.m_PACKING_QUANTITY; }
            set
            {
                this.m_PACKING_QUANTITY = value;
                this.NotifyPropertyChanged("PACKING_QUANTITY");
            }
        }
        //---------------------------------------------------

        [DBColumn(Name = "MRB_PLATE_ID", Storage = "m_MRB_PLATE_ID", DbType = "107")]
        public int MRB_PLATE_ID
        {
            get { return this.m_MRB_PLATE_ID; }
            set
            {
                this.m_MRB_PLATE_ID = value;
                this.NotifyPropertyChanged("MRB_PLATE_ID");
            }
        }


        [DBColumn(Name = "MRB_PLATE_QTY", Storage = "m_MRB_PLATE_QTY", DbType = "107")]
        public decimal  MRB_PLATE_QTY
        {
            get { return this.m_MRB_PLATE_QTY; }
            set
            {
                this.m_MRB_PLATE_QTY = value;
                this.NotifyPropertyChanged("MRB_PLATE_QTY");
            }
        }


        [DBColumn(Name = "MRB_PLATE_WEIGHT", Storage = "m_MRB_PLATE_WEIGHT", DbType = "107")]
        public decimal MRB_PLATE_WEIGHT
        {
            get { return this.m_MRB_PLATE_WEIGHT; }
            set
            {
                this.m_MRB_PLATE_WEIGHT = value;
                this.NotifyPropertyChanged("MRB_PLATE_WEIGHT");
            }
        }










        [DBColumn(Name = "MRB_PLATE_ID_N", Storage = "m_MRB_PLATE_ID_N", DbType = "107")]
        public int MRB_PLATE_ID_N
        {
            get { return this.m_MRB_PLATE_ID_N; }
            set
            {
                this.m_MRB_PLATE_ID_N = value;
                this.NotifyPropertyChanged("MRB_PLATE_ID_N");
            }
        }


        [DBColumn(Name = "MRB_PLATE_QTY_N", Storage = "m_MRB_PLATE_QTY_N", DbType = "107")]
        public decimal MRB_PLATE_QTY_N
        {
            get { return this.m_MRB_PLATE_QTY_N; }
            set
            {
                this.m_MRB_PLATE_QTY_N = value;
                this.NotifyPropertyChanged("MRB_PLATE_QTY_N");
            }
        }


        [DBColumn(Name = "MRB_PLATE_WEIGHT_N", Storage = "m_MRB_PLATE_WEIGHT_N", DbType = "107")]
        public decimal MRB_PLATE_WEIGHT_N
        {
            get { return this.m_MRB_PLATE_WEIGHT_N; }
            set
            {
                this.m_MRB_PLATE_WEIGHT_N = value;
                this.NotifyPropertyChanged("MRB_PLATE_WEIGHT_N");
            }
        }





        [DBColumn(Name = "SCRAP_BATTERY_WEIGHT", Storage = "m_SCRAP_BATTERY_WEIGHT", DbType = "107")]
        public decimal SCRAP_BATTERY_WEIGHT
        {
            get { return this.m_SCRAP_BATTERY_WEIGHT; }
            set
            {
                this.m_SCRAP_BATTERY_WEIGHT = value;
                this.NotifyPropertyChanged("SCRAP_BATTERY_WEIGHT");
            }
        }


        [DBColumn(Name = "PROD_BATCH_NO_DTL", Storage = "m_PROD_BATCH_NO_DTL", DbType = "107")]
        public string PROD_BATCH_NO_DTL
        {
            get { return this.m_PROD_BATCH_NO_DTL; }
            set
            {
                this.m_PROD_BATCH_NO_DTL = value;
                this.NotifyPropertyChanged("PROD_BATCH_NO_DTL");
            }
        }

        [DBColumn(Name = "STLM_ID", Storage = "m_STLM_ID", DbType = "107")]
        public int STLM_ID
        {
            get { return this.m_STLM_ID; }
            set
            {
                this.m_STLM_ID = value;
                this.NotifyPropertyChanged("STLM_ID");
            }
        }

        [DBColumn(Name = "RM_SUP_ID", Storage = "m_RM_SUP_ID", DbType = "107")]
        public int RM_SUP_ID
        {
            get { return this.m_RM_SUP_ID; }
            set
            {
                this.m_RM_SUP_ID = value;
                this.NotifyPropertyChanged("RM_SUP_ID");
            }
        }

         [DBColumn(Name = "FALSE_LUG_ITEM_ID", Storage = "m_FALSE_LUG_ITEM_ID", DbType = "107")]
        public int FALSE_LUG_ITEM_ID
        {
            get { return this.m_FALSE_LUG_ITEM_ID; }
            set
            {
                this.m_FALSE_LUG_ITEM_ID = value;
                this.NotifyPropertyChanged("FALSE_LUG_ITEM_ID");
            }
        }

         [DBColumn(Name = "MIXER_BATCH_QTY", Storage = "m_MIXER_BATCH_QTY", DbType = "107")]
         public decimal MIXER_BATCH_QTY
         {
             get { return this.m_MIXER_BATCH_QTY; }
             set
             {
                 this.m_MIXER_BATCH_QTY = value;
                 this.NotifyPropertyChanged("MIXER_BATCH_QTY");
             }
         }

        [DBColumn(Name = "MANUAL_SLNO", Storage = "m_MANUAL_SLNO", DbType = "107")]
         public string MANUAL_SLNO
         {
             get { return this.m_MANUAL_SLNO; }
             set
             {
                 this.m_MANUAL_SLNO = value;
                 this.NotifyPropertyChanged("MANUAL_SLNO");
             }
         }
        
        
        #endregion //properties

       
    }

    public partial class dcPRODUCTION_DTL
    {
        private string m_ITEM_GROUP_DESC = "";
        private string m_ITEM_CODE = "";
        
        private string m_ITEM_NAME = "";
        private string m_UOM_NAME = "";
        private string m_BOM_NAME = "";
        private string m_PANEL_UOM_NAME = "";
        private string m_WEIGHT_UOM_NAME = "";
        private string m_MACHINE_NAME = "";
        private int m_BALPACKINGQTY = 0;
        


        private List<dcITEM_STOCK_DETAILS> m_ProductionStockDetailsList = null;
        public List<dcITEM_STOCK_DETAILS> ProductionStockDetailsList
        {
            get { return m_ProductionStockDetailsList; }
            set { m_ProductionStockDetailsList = value; }
        }

        public string ITEM_GROUP_DESC
        {
            get { return this.m_ITEM_GROUP_DESC; }
            set
            {
                this.m_ITEM_GROUP_DESC = value;
            }
        }

        public string ITEM_CODE
        {
            get { return this.m_ITEM_CODE; }
            set
            {
                this.m_ITEM_CODE = value;
            }
        }

        public string ITEM_NAME
        {
            get { return this.m_ITEM_NAME; }
            set
            {
                this.m_ITEM_NAME = value;
            }
        }

        public string USED_ITEM_NAME
        {
            get { return this.m_USED_ITEM_NAME; }
            set
            {
                this.m_USED_ITEM_NAME = value;
            }
        }

        public string UOM_NAME
        {
            get { return this.m_UOM_NAME; }
            set
            {
                this.m_UOM_NAME = value;
            }
        }


        public string BOM_NAME
        {
            get { return this.m_BOM_NAME; }
            set
            {
                this.m_BOM_NAME = value;
            }
        }



        public string PANEL_UOM_NAME
        {
            get { return this.m_PANEL_UOM_NAME; }
            set
            {
                this.m_PANEL_UOM_NAME = value;
            }
        }


        public string WEIGHT_UOM_NAME
        {
            get { return this.m_WEIGHT_UOM_NAME; }
            set
            {
                this.m_WEIGHT_UOM_NAME = value;
            }
        }


        public string MACHINE_NAME
        {
            get { return this.m_MACHINE_NAME; }
            set
            {
                this.m_MACHINE_NAME = value;
            }
        }

        private string m_BAR_TYPE_NAME = "";
        public string BAR_TYPE_NAME
        {
            get { return this.m_BAR_TYPE_NAME; }
            set { this.m_BAR_TYPE_NAME = value; }
        }

        private string m_OPERATOR_NAME = "";

        public string OPERATOR_NAME
        {
            get { return this.m_OPERATOR_NAME; }
            set { this.m_OPERATOR_NAME = value; }
        }

        public int BALPACKINGQTY
        {
            get { return this.m_BALPACKINGQTY; }
            set { this.m_BALPACKINGQTY = value; }
        }





        // Production Mst 
        private int m_PROD_ID = 0;
        private string m_PROD_NO = "";

        private string m_SUPERVISOR_NAME = "";
        private string m_SUPERVISOR_ID = "";
        private int m_DEPT_ID = 0;
        private string m_DEPARTMENT_NAME = "";
        private string m_REF_NO_MANUAL = "";
        private int m_FORECUSTMONTH = 0;
        private int m_FORECUSTYEAR = 0;
        private string m_RM_FC_DESC = "";

        private int m_FORECUST_ID = 0;
        private string m_SHIFT_ID = "";
        private string m_SHIFT_NAME = "";
        private DateTime? m_BATCH_STARTTIME = null;
        private DateTime? m_BATCH_ENDTIME = null;
        private string m_STARTTIME = "";
        private string m_ENDTIME = "";
        private int m_PROCESS_CODE = 0;
        private DateTime? m_PRODUCTION_DATE = null;
        private int m_REJECTED_QTY = 0;
        private string m_BATCH_ID = "";
        private string m_PROD_BATCH_NO = "";



        public int PROD_ID
        {
            get { return this.m_PROD_ID; }
            set { this.m_PROD_ID = value; }
        }

        public string PROD_NO
        {
            get { return this.m_PROD_NO; }
            set { this.m_PROD_NO = value; }
        }

        public string SUPERVISOR_NAME
        {
            get { return this.m_SUPERVISOR_NAME; }
            set { this.m_SUPERVISOR_NAME = value; }
        }

        public string SUPERVISOR_ID
        {
            get { return this.m_SUPERVISOR_ID; }
            set { this.m_SUPERVISOR_ID = value; }
        }

        public int DEPT_ID
        {
            get { return this.m_DEPT_ID; }
            set { this.m_DEPT_ID = value; }
        }

        public string DEPARTMENT_NAME
        {
            get { return this.m_DEPARTMENT_NAME; }
            set { this.m_DEPARTMENT_NAME = value; }
        }

        public string REF_NO_MANUAL
        {
            get { return this.m_REF_NO_MANUAL; }
            set { this.m_REF_NO_MANUAL = value; }
        }
        public int FORECUSTMONTH
        {
            get { return this.m_FORECUSTMONTH; }
            set { this.m_FORECUSTMONTH = value; }
        }

        public int FORECUSTYEAR
        {
            get { return this.m_FORECUSTYEAR; }
            set { this.m_FORECUSTYEAR = value; }
        }

        public string RM_FC_DESC
        {
            get { return this.m_RM_FC_DESC; }
            set { this.m_RM_FC_DESC = value; }
        }

        public int FORECUST_ID
        {
            get { return this.m_FORECUST_ID; }
            set { this.m_FORECUST_ID = value; }
        }

        public string SHIFT_ID
        {
            get { return this.m_SHIFT_ID; }
            set { this.m_SHIFT_ID = value; }
        }

        public string SHIFT_NAME
        {
            get { return this.m_SHIFT_NAME; }
            set { this.m_SHIFT_NAME = value; }
        }

        public DateTime? BATCH_STARTTIME
        {
            get { return this.m_BATCH_STARTTIME; }
            set { this.m_BATCH_STARTTIME = value; }
        }

        public DateTime? BATCH_ENDTIME
        {
            get { return this.m_BATCH_ENDTIME; }
            set { this.m_BATCH_ENDTIME = value; }
        }

        public string STARTTIME
        {
            get { return this.m_STARTTIME; }
            set { this.m_STARTTIME = value; }
        }

        public string ENDTIME
        {
            get { return this.m_ENDTIME; }
            set { this.m_ENDTIME = value; }
        }


        public int PROCESS_CODE
        {
            get { return this.m_PROCESS_CODE; }
            set { this.m_PROCESS_CODE = value; }
        }


        public DateTime? PRODUCTION_DATE
        {
            get { return this.m_PRODUCTION_DATE; }
            set { this.m_PRODUCTION_DATE = value; }
        }


        public int REJECTED_QTY
        {
            get { return this.m_REJECTED_QTY; }
            set { this.m_REJECTED_QTY = value; }
        }


        public string BATCH_ID
        {
            get { return this.m_BATCH_ID; }
            set { this.m_BATCH_ID = value; }
        }

        public string PROD_BATCH_NO
        {
            get { return this.m_PROD_BATCH_NO; }
            set { this.m_PROD_BATCH_NO = value; }
        }

        private string m_CREATE_BY = "";
        public string CREATE_BY
        {
            get { return this.m_CREATE_BY; }
            set { this.m_CREATE_BY = value; }
        }

        private DateTime? m_ENTRY_DATE = null;
        public DateTime? ENTRY_DATE
        {
            get { return this.m_ENTRY_DATE; }
            set { this.m_ENTRY_DATE = value; }
        }

      

        private decimal m_PASTE_PC_KG = 0;
        public decimal PASTE_PC_KG
        {
            get { return this.m_PASTE_PC_KG; }
            set { this.m_PASTE_PC_KG = value; }
        }


        private decimal m_PASTE_PANEL_KG = 0;
        public decimal PASTE_PANEL_KG
        {
            get { return this.m_PASTE_PANEL_KG; }
            set { this.m_PASTE_PANEL_KG = value; }
        }


        private int m_USED_GRID_ID = 0;
        public int USED_GRID_ID
        {
            get { return this.m_USED_GRID_ID; }
            set { this.m_USED_GRID_ID = value; }
        }


        private string m_USED_GRID_NAME = "";
        public string USED_GRID_NAME
        {
            get { return this.m_USED_GRID_NAME; }
            set { this.m_USED_GRID_NAME = value; }
        }



        public decimal TOTAL_UNLOAD_QTY
        {
            get
            {
                return this.FORMED_QTY + this.UNFORMED_QTY + this.REJECT_QTY;

            }

        }



        private int m_CLOSING_ITEM_ID = 0;
        public int CLOSING_ITEM_ID
        {
            get { return this.m_CLOSING_ITEM_ID; }
            set { this.m_CLOSING_ITEM_ID = value; }
        }

        private int m_CLOSING_UOM_ID = 0;
        public int CLOSING_UOM_ID
        {
            get { return this.m_CLOSING_UOM_ID; }
            set { this.m_CLOSING_UOM_ID = value; }
        }

          private int m_FINISHED_ITEM_ID = 0;
        public int FINISHED_ITEM_ID
        {
            get { return this.m_FINISHED_ITEM_ID; }
            set { this.m_FINISHED_ITEM_ID = value; }
        }

         private string m_CLOSINGITEM_NAME = "";
        public string CLOSINGITEM_NAME
        {
            get { return this.m_CLOSINGITEM_NAME; }
            set { this.m_CLOSINGITEM_NAME = value; }
        }

         private string m_CLOSING_UOM_NAME = "";
        public string CLOSING_UOM_NAME
        {
            get { return this.m_CLOSING_UOM_NAME; }
            set { this.m_CLOSING_UOM_NAME = value; }
        }
        
         private string m_FINISH_ITEM_NAME = "";
        public string FINISH_ITEM_NAME
        {
            get { return this.m_FINISH_ITEM_NAME; }
            set { this.m_FINISH_ITEM_NAME = value; }
        }
        
        private decimal m_SYSTEM_OPENING_STOCK = 0;
        public decimal SYSTEM_OPENING_STOCK
        {
            get { return this.m_SYSTEM_OPENING_STOCK; }
            set { this.m_SYSTEM_OPENING_STOCK = value; }
        }

        private decimal m_ISSUE_STOCK = 0;
        public decimal ISSUE_STOCK
        {
            get { return this.m_ISSUE_STOCK; }
            set { this.m_ISSUE_STOCK = value; }
        }

        private decimal m_CLOSING_QTY = 0;
        public decimal CLOSING_QTY
        {
            get { return this.m_CLOSING_QTY; }
            set { this.m_CLOSING_QTY = value; }
        }


        private string m_PROD_BATTERY_TYPE_NAME = "";
        public string PROD_BATTERY_TYPE_NAME
        {
            get { return this.m_PROD_BATTERY_TYPE_NAME; }
            set { this.m_PROD_BATTERY_TYPE_NAME = value; }
        }
            
            
        private decimal? m_AVAILABLE_PACKING_QUANTITY =0;
        public decimal? AVAILABLE_PACKING_QUANTITY
        {
            get { return this.m_AVAILABLE_PACKING_QUANTITY; }
            set { this.m_AVAILABLE_PACKING_QUANTITY = value; }
        }
        
        public decimal? AVAILABLE_CHARGING_QUANTITY { get; set; }
        public string MRB_PLATE_NAME { get; set; }

        public string MRB_PLATE_NAME_N { get; set; }
        public string SUP_NAME { get; set; }
        public int SUP_ID { get; set; }
        public string FALSE_LUG_NAME { get; set; }
        public string IS_BATCH { get; set; }
        
    }
}
