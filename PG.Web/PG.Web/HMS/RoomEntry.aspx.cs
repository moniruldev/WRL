using PG.BLLibrary.InventoryBL;
using PG.Core;
using PG.Core.DBBase;
using PG.Core.Utility;
using PG.Core.Web;
using PG.DBClass.InventoryDC;
using PG.DBClass.SecurityDC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PG.BLLibrary.OrganizationBL;
using PG.Report.ReportRBL.InventoryRBL;
using PG.Report.ReportEnums;
using PG.Report;
using PG.Report.ReportGen.InventoryRGN;
using PG.DBClass.HMSDC;
using PG.BLLibrary.HMSBL;
using System.IO;
using System.Text;
using System.Data;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace PG.Web.HMS
{
    public partial class RoomEntry : BagePage
    {
        //this 
        string ViewStateKey = "ROOM_ID";
        string ViewStateKeyPrev = "ROOM_ID_PREV";
        ReportOpenTypeEnum ReportOpenType = ReportOpenTypeEnum.Preview;
        // int CompanyID = 0;
       
        int ROOM_ID = 0;
        string saveMsg = string.Empty;
        string errMsg = string.Empty;

        private  dcUser loggedinUser = null;
        public string ReportViewPageLink = PageLinks.ReportLinks.GetLink_ReportView;
        public string ReportViewPDFPageLink = PageLinks.ReportLinks.GetLink_ReportViewPDF;
        public string ReportPrintPageLink = PageLinks.ReportLinks.GetLink_ReportPrint;
        public string ReportPDFPageLink = PageLinks.ReportLinks.GetLink_ReportPDF;



        public string CountryListServiceLink = PageLinks.InventoryLink.GetLink_CountryList;

        protected override void OnPreInit(EventArgs e)
        {

            if (Globals.AppMasterPage != string.Empty)
            {
                this.MasterPageFile = Globals.AppMasterPage;
            }
            base.OnPreInit(e);
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            loggedinUser = AppSecurity.GetUserInfoFromSession();

            this.ROOM_ID = base.GetPageQueryInteger("id");

            if (!IsPostBack) //first Time
            {

              
                hdnLoggedInUser.Value = loggedinUser.UserID.ToString();
                //FillCombo();


                if (this.ROOM_ID == 0) //not query string
                {
                    SetDate();
                    AddTask();
                    this.EditMode = FormDataMode.Add;
                }
                else
                {
                    FormDataMode fdMode = base.GetEditModeFromQueryString(this.EditModeQueryStringKey);
                    if (fdMode == FormDataMode.Edit)
                    {

                        EditTask();
                    }
                    else
                    {
                        ReadTask();
                    }
                 
                }

            }
            else
            {
                this.EditMode = base.GetEditModeFromViewState(base.EditModeViewStateKey);
                this.ROOM_ID = int.Parse(ViewState[ViewStateKey].ToString());
            }

            SetHyperLink();

          
            //this.ShowPageMessage(this.lblMessage);
            // Response.Write("UserID : " + this.UserID.ToString());

        }
     
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            EditTask();
        }

        public void FillCombo()
        {
            ddlRoomType.Items.Clear();
            ddlRoomType.AppendDataBoundItems = true;
            ddlRoomType.DataTextField = "TITLE";
            ddlRoomType.DataValueField = "ROOM_TYPE_ID";
            ddlRoomType.DataSource = HMROOM_TYPEBL.GetRoomTypeList();
            ddlRoomType.DataBind();
            ddlRoomType.SelectedIndex = 0;

            ddlRoomStatus.Items.Clear();
            ddlRoomStatus.AppendDataBoundItems = true;
            ddlRoomStatus.DataTextField = "ROOM_STATUS";
            ddlRoomStatus.DataValueField = "ROOM_STATUS_ID";
            ddlRoomStatus.DataSource = HMROOMBL.GetRoomStatusList();
            ddlRoomStatus.DataBind();
            ddlRoomStatus.SelectedIndex = 0;


        }

        protected override void Render(HtmlTextWriter writer)
        {

            //Page.ClientScript.RegisterForEventValidation(btnPopupTrigger.UniqueID);
            //Page.ClientScript.RegisterForEventValidation(btnPopupTrigger.UniqueID, "");
            //Page.ClientScript.RegisterForEventValidation(btnPopupTrigger.UniqueID, "fillcombo");
            //Page.ClientScript.RegisterForEventValidation(btnPopupTrigger.UniqueID, "getbalance");

            base.Render(writer);
        }

        private void SetDate()
        {


        }

        private void ReadTask()
        {
            this.EditMode = FormDataMode.Read;
            ReadData(this.ROOM_ID);
            ViewState[ViewStateKey] = this.ROOM_ID.ToString();

            SetControl(FormDataMode.Read);

        }
        private void AddTask()
        {
            ViewState[ViewStateKeyPrev] = this.ROOM_ID.ToString();

            this.EditMode = FormDataMode.Add;
            this.IsDirty = false;
            this.ROOM_ID = 0;
            ViewState[ViewStateKey] = "0";
            SetControl(FormDataMode.Add);
        }
        private void EditTask()
        {
            this.EditMode = FormDataMode.Edit;
            ReadData(this.ROOM_ID);
            this.EditMode = FormDataMode.Edit;
            ViewState[ViewStateKey] = this.ROOM_ID.ToString();
            SetControl(FormDataMode.Edit);
        }

        private bool ReadData(int id)
        {
            bool bStatus = false;
            byte[] bytes = null;
            dcHMROOM cObj = HMROOMBL.GetRoomInfoById(id);
            if (cObj != null)
            {
                
                hdnROOM_ID.Value = cObj.ROOM_ID.ToString();
                txtRoomNo.Text = cObj.ROOM_NO;
                txtFloor.Text = cObj.FLOOR_ID.ToString();
                txtOrder.Text = cObj.ORDER_NO.ToString();
                txtResponsible.Text = cObj.RESPONSIBLE.ToString();
                ddlRoomType.SelectedValue = cObj.ROOM_TYPE_ID.ToString();
                ddlStatus.SelectedValue = cObj.IS_ACTIVE.ToString();
                ddlRoomStatus.SelectedValue = cObj.ROOM_STATUS_ID.ToString();
                if(cObj.THUMBNAILSIMAGE_NAME != null)
                {
                    bytes = (byte[])cObj.THUMBNAILSIMAGE_NAME;
                    string strBase64 = Convert.ToBase64String(bytes);
                    Image1.ImageUrl = "data:Image/png;base64," + strBase64;
                  
                }
            

                bStatus = true;
            }
            return bStatus;

        }

        private void SetControl(FormDataMode dataMode)
        {
            bool isEnabled = false;

            if (dataMode == FormDataMode.Add | dataMode == FormDataMode.Edit)
            {
                isEnabled = true;
            }


            txtRoomNo.Enabled = isEnabled;
            txtFloor.Enabled = isEnabled;
            txtOrder.Enabled = isEnabled;
            txtResponsible.Enabled = isEnabled;
            FileUpload1.Enabled = isEnabled;

            ddlRoomStatus.Enabled = isEnabled;
            ddlRoomType.Enabled = isEnabled;
            ddlStatus.Enabled = isEnabled;
            
            //buttons
            btnAddNew.Visible = !isEnabled;
            btnEdit.Visible = !isEnabled;
            btnSave.Visible = isEnabled;
            //btnUpdate.Visible = !isEnabled;


        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            SaveTask();

        }

        private bool SaveTask()
        {

            if (!Page.IsValid)
            { return false; }


            if (CheckData())
            {

                bool bStatus = SaveData();

                if (bStatus)
                {
                    ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Data Saved Successfully');", true);
                   
                }
                else
                {
                    
                    ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Error !! Data not Saved');", true);
                }

            }
            else
            {

                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Error !! Data not Saved');", true);
                this.SetPageMessage(errMsg, MessageTypeEnum.InvalidData);
            }

            return true;

        }


        private bool CheckData()
        {
            bool status = true;
            errMsg = string.Empty;

            if (txtRoomNo.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Please Enter Room No !!');", true);
                txtRoomNo.Focus();
                return false;

            }

            if (ddlRoomType.SelectedValue == "0")
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Please Select Room Type !!');", true);
                ddlRoomType.Focus();
                return false;

            }

            string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string contentType = FileUpload1.PostedFile.ContentType;

            if (contentType != "image/jpeg")
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Only jpg file is Allowed !!');", true);
                return false;
            }

            double fileSize = FileUpload1.PostedFile.ContentLength;
            if (fileSize > 5145728.00) //5145728.00
            {
                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('You can only upload files of size less than 5 MB, but you are uploading a file of " + Math.Round((fileSize / 5145728.00), 2) + " MB ');", true);

                return false;
            }


            return status;
        

        }
        private void SetHyperLink()
        {

            //new button
            string hLink = "javascript:tbopen(" + this.ROOM_ID.ToString() + ")";
            if (base.PageMode == PG.Core.Web.PageModeEnum.InTab)
            {
                hLink = "javascript:tbopenSalInfo(" + this.ROOM_ID.ToString() + ")";
                this.btnAddNew.Attributes.Add("onclick", hLink);
            }
            else
            {
                hLink = "~/HMS/RoomEntry.aspx?id=" + this.ROOM_ID.ToString();
                this.btnAddNew.Attributes.Add("onclick", hLink);
            }

        }

        //newly added comment
        private bool SaveData()
        {

            bool bStatus = false;

            bool isAdd = false;
            int newROOM_ID = 0;
            dcHMROOM cObj = new dcHMROOM();
            if (this.ROOM_ID > 0)
            {
                cObj.ROOM_ID = this.ROOM_ID;
                cObj._RecordState = RecordStateEnum.Edited;
            }
            else
            {
                cObj._RecordState = RecordStateEnum.Added;
                isAdd = true;
            }

          
       
            cObj.IS_ACTIVE = ddlStatus.SelectedValue;
            cObj.ROOM_NO = txtRoomNo.Text;
            cObj.ROOM_TYPE_ID =Conversion.StringToInt(ddlRoomType.SelectedValue);
            cObj.ROOM_STATUS_ID = Conversion.StringToInt(ddlRoomStatus.SelectedValue);
            cObj.RESPONSIBLE = txtResponsible.Text;
            cObj.ORDER_NO = Convert.ToInt32(txtOrder.Text);
            cObj.FLOOR_ID =Conversion.StringToInt(txtFloor.Text);

            //Stream stream = FileUpload1.PostedFile.InputStream;
            //BinaryReader binaryReader = new BinaryReader(stream);
            //byte[] byteImage = binaryReader.ReadBytes((Int32)stream.Length);
            //string str = Encoding.UTF8.GetString(byteImage);
            //string hexString = BitConverter.ToString(byteImage).Replace("-", "");
            //cObj.THUMBNAILS_IMAGE = byteImage;

            if (isAdd)
            {
                cObj.CREATE_BY = loggedinUser.UserName;
                cObj.CREATE_DATE = DateTime.Now;

            }
            else
            {
                cObj.UPDATE_DATE = DateTime.Now;

            }

            newROOM_ID = HMROOMBL.Save(cObj);
            if (newROOM_ID > 0)
            {
                if (FileUpload1.HasFile)
                {
                    try
                    {
                        using (Stream fs = FileUpload1.PostedFile.InputStream)
                        {
                            using (BinaryReader br = new BinaryReader(fs))
                            {
                                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                                byte[] bytes = br.ReadBytes((Int32)fs.Length);
                                string constr = ConfigurationManager.ConnectionStrings["SND_Oracle"].ConnectionString;
                                using (OracleConnection con = new OracleConnection(constr))
                                {
                                    //string query = " INSERT INTO HMROOM_TYPE (THUMBNAILS_IMAGE) VALUES(:SIGN_PHOTO) ";
                                    string query = " UPDATE HMROOM SET THUMBNAILSIMAGE_NAME =:SIGN_PHOTO,FULL_IMAGE_NAME=:filename  WHERE ROOM_ID=:newROOM_ID ";
                                    using (OracleCommand cmd = new OracleCommand(query))
                                    {
                                        cmd.Connection = con;

                                        cmd.Parameters.Add("SIGN_PHOTO", bytes);
                                        cmd.Parameters.Add("filename", filename);
                                        cmd.Parameters.Add("newROOM_ID", newROOM_ID);

                                        con.Open();
                                        try
                                        {
                                            cmd.ExecuteNonQuery();
                                        }
                                        catch
                                        { throw; }
                                        finally
                                        {
                                            con.Close();

                                        }

                                    }
                                }
                            }
                        }
                    }
                    catch
                    {
                        throw;
                    }
                }
                this.ROOM_ID = newROOM_ID;
                ReadTask();
                bStatus = true;
                ScriptManager.RegisterClientScriptBlock(btnSave, GetType(), "", "alert('Data saved successfully !!');", true);
            }

            return bStatus;
        }



        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            AddTask();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                int fileLen;
                // Get the length of the file.
                fileLen = FileUpload1.PostedFile.ContentLength;
                byte[] input = new byte[fileLen - 1];
                input = FileUpload1.FileBytes;

                byte[] fileBytes = new byte[FileUpload1.PostedFile.ContentLength];

                Image1.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(input.ToArray(), 0, input.ToArray().Length);

            }
        }


    }
}