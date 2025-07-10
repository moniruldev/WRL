using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Data.SqlTypes;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using System.Data;

using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace PG.Web
{
    public partial class XMLTest : System.Web.UI.Page
    {
        public class XMLClass
        {
            public int TextID {get;set;} 
            public string TextCode {get;set;}
            public decimal TextAmount {get;set;}
            public DateTime? TextDate {get;set;}

            public string TextCol2 { get; set; }

            public int TextID2 { get; set; }

            public DateTime? TextDate2 { get; set; }

            public DateTime TextDate3 { get; set; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
             
        }

        private SqlXml ConvertObjectToSqlXml(XMLClass xmlClass)
        {

            MemoryStream memoryStream = new MemoryStream();
            XmlSerializer xs = new XmlSerializer(typeof(XMLClass));
            XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
            xs.Serialize(xmlTextWriter, xmlClass);
            memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
            memoryStream.Position = 0;
            System.Data.SqlTypes.SqlXml sXml = new System.Data.SqlTypes.SqlXml(memoryStream);
            return sXml;
        }

        private SqlXml ConvertObjectListToSqlXml(List<XMLClass> xmlClassList)
        {

            MemoryStream memoryStream = new MemoryStream();
            XmlSerializer xs = new XmlSerializer(typeof(List<XMLClass>));
            XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
            xs.Serialize(xmlTextWriter, xmlClassList);
            memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
            memoryStream.Position = 0;
            System.Data.SqlTypes.SqlXml sXml = new System.Data.SqlTypes.SqlXml(memoryStream);
            return sXml;
        }

        private XMLClass ConvertSQLXmlToObject(SqlXml sXml)
        {
            XMLClass xmlClass = new XMLClass();
            XmlSerializer xs = new XmlSerializer(typeof(XMLClass));
            xmlClass = xs.Deserialize(sXml.CreateReader()) as XMLClass;
            return xmlClass;
        }


        private List<XMLClass> ConvertSQLXmlToObjectList(SqlXml sXml)
        {
            List<XMLClass> xmlClassList = new List<XMLClass>();
            XmlSerializer xs = new XmlSerializer(typeof(List<XMLClass>));
            xmlClassList = xs.Deserialize(sXml.CreateReader()) as List<XMLClass>;
            return xmlClassList;
        }

        private void InsertSQL()
        {
            string conString = ConfigurationManager.ConnectionStrings["Accounting_SQLServer"].ToString();
            SqlConnection sCon = new SqlConnection();

            sCon.ConnectionString = conString;

            sCon.Open();


            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sCon;

            StringBuilder sb = new StringBuilder();

            sb.Append("INSERT INTO tblXMLData ");
            sb.Append(" (XMLDataCode, XMLData) ");
            sb.Append(" VALUES (@xmlDataCode, @xmlData) ");
            sb.Append("");

            cmd.Parameters.AddWithValue("@xmlDataCode", "001");

            XmlDocument xDoc = new XmlDocument();


            List<XMLClass> xmlClassList = new List<XMLClass>();

            XMLClass xmlClass = new XMLClass();
            xmlClass.TextID = 45;
            xmlClass.TextCode = "ID0934";
            xmlClass.TextAmount = 45.24M;
            xmlClass.TextDate = new DateTime(2015, 4, 20);

            xmlClassList.Add(xmlClass);

            XMLClass xmlClass2 = new XMLClass();
            xmlClass2.TextID = 67;
            xmlClass2.TextCode = "ID0976";
            xmlClass2.TextAmount = 548.32M;
            xmlClass2.TextDate = new DateTime(2015, 4, 20);

            xmlClassList.Add(xmlClass2);


            //SqlXml sXml = ConvertObjectToSqlXml(xmlClass);
            SqlXml sXml = ConvertObjectListToSqlXml(xmlClassList);


            cmd.Parameters.AddWithValue("@xmlData", sXml);

        
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = sb.ToString();


            int i = cmd.ExecuteNonQuery();

            sCon.Close();

            Label1.Text = i > 0 ? "insert success" : "insert failed";
        }


        private void InsertOracle()
        {
            string conString = ConfigurationManager.ConnectionStrings["Accounting_Oracle"].ToString();
            OracleConnection oCon = new OracleConnection();
            oCon.ConnectionString = conString;

            oCon.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = oCon;

            StringBuilder sb = new StringBuilder();

            sb.Append("INSERT INTO tblXMLData ");
            sb.Append(" (XMLDataCode, XMLData) ");
            sb.Append(" VALUES (:xmlDataCode, :xmlData) ");
            sb.Append("");

            cmd.Parameters.Add(":xmlDataCode", "001");




            List<XMLClass> xmlClassList = new List<XMLClass>();

            XMLClass xmlClass = new XMLClass();
            xmlClass.TextID = 45;
            xmlClass.TextCode = "ID0934";
            xmlClass.TextAmount = 45.24M;
            xmlClass.TextDate = new DateTime(2015, 4, 20);

            xmlClassList.Add(xmlClass);

            XMLClass xmlClass2 = new XMLClass();
            xmlClass2.TextID = 67;
            xmlClass2.TextCode = "ID0976";
            xmlClass2.TextAmount = 548.32M;
            xmlClass2.TextDate = new DateTime(2015, 4, 20);

            xmlClassList.Add(xmlClass2);

            //OracleDbType.NClob

            SqlXml sXml = ConvertObjectListToSqlXml(xmlClassList);

            //OracleClob clob = new OracleClob(oCon, false, false);
            //clob.Write(sXml.Value.ToCharArray(), 0, sXml.Value.Length);

            //OracleDbType.NClob

            //OracleClob clob = new OracleClob(oCon, false, false);
            //clob.Write(sXml.Value.ToCharArray(), 0, sXml.Value.Length);

            //cmd.Parameters.Add(":xmlData", sXml);
            //OracleParameter oParam = new OracleParameter(":xmlData", sXml.Value);
            //oParam.OracleDbType = OracleDbType.Clob;
            //oParam.OracleDbType = OracleDbType.NClob;

            cmd.Parameters.Add(":xmlData", sXml.Value);

            //cmd.Parameters.Add(":xmlData",
            //          OracleDbType.Clob,
            //          clob,
            //          ParameterDirection.Input);



            //Oracle.ManagedDataAccess.Types.OracleTypeException


            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = sb.ToString();


            int i = cmd.ExecuteNonQuery();

            oCon.Close();

            Label1.Text = i > 0 ? "insert success" : "insert failed";
        }


        private void ReadSQL()
        {
            string conString = ConfigurationManager.ConnectionStrings["Accounting_SQLServer"].ToString();
            SqlConnection sCon = new SqlConnection();

            sCon.ConnectionString = conString;
            sCon.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sCon;

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT tblXMLData.* ");
            sb.Append(" FROM tblXMLData ");
            
            sb.Append(" WHERE (1=1) ");
            sb.Append(" AND tblXMLData.XMLDataID = 1002 ");

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = sb.ToString();

            DataTable dtData = new DataTable();
            dtData.Load(cmd.ExecuteReader());
            sCon.Close();


//            String xmlString =
//                        @"<bookstore>
//                            <book genre='autobiography' publicationdate='1981-03-22' ISBN='1-861003-11-0'>
//                                <title>The Autobiography of Benjamin Franklin</title>
//                                <author>
//                                    <first-name>Benjamin</first-name>
//                                    <last-name>Franklin</last-name>
//                                </author>
//                                <price>8.99</price>
//                            </book>
//                        </bookstore>";

//            XmlReader reader = XmlReader.Create(new StringReader(xmlString));


            if (dtData.Rows.Count > 0)
            {
                DataRow dRow = dtData.Rows[0];
                string xmlCode = Convert.ToString(dRow["XMLDataCode"]);
                string xmlData = Convert.ToString(dRow["XMLData"]);
                StringReader sr = new StringReader(xmlData);

                //XmlReader.Create(new StringReader(xmlString)))

                XmlReader xR = XmlReader.Create(sr);

                //SqlXml sXML =  dRow["XMLData"] as SqlXml;

                SqlXml sXML = new SqlXml(xR);
                //XMLClass xObj = ConvertSQLXmlToObject(sXML);

                //Label1.Text = xObj.TextDate.ToString() + ", " + xObj.TextAmount.ToString();

                List<XMLClass> xObjList = ConvertSQLXmlToObjectList(sXML);
                Label1.Text = xObjList[1].TextDate.ToString() + ", " + xObjList[1].TextAmount.ToString();

            }
            else
            {
                Label1.Text = "No Record!";
            }

        }

        private void ReadOracle()
        {
            string conString = ConfigurationManager.ConnectionStrings["Accounting_Oracle"].ToString();
            OracleConnection oCon = new OracleConnection();

            oCon.ConnectionString = conString;
            oCon.Open();

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = oCon;

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT tblXMLData.* ");
            sb.Append(" FROM tblXMLData ");

            sb.Append(" WHERE (1=1) ");
            sb.Append(" AND tblXMLData.XMLDataID = 3 ");



//            PROCEDURE clobToXMLType(myClob IN CLOB)

//IS
//    l_xmlType XMLTYPE;
//    -- do something
//BEGIN
//    l_xmltype := XMLTYPE.createXML(myClob);
  
//EXCEPTION

//    WHEN OTHERS THEN
//        RAISE;

//END clobToXMLType;

//Select ACC.tblXMLData.*, XMLTYPE.createXML(tblXMLDATA.XMLData) SDF From ACC.tblXMLDATA;


            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = sb.ToString();

            DataTable dtData = new DataTable();
            dtData.Load(cmd.ExecuteReader());
            oCon.Close();


            //            String xmlString =
            //                        @"<bookstore>
            //                            <book genre='autobiography' publicationdate='1981-03-22' ISBN='1-861003-11-0'>
            //                                <title>The Autobiography of Benjamin Franklin</title>
            //                                <author>
            //                                    <first-name>Benjamin</first-name>
            //                                    <last-name>Franklin</last-name>
            //                                </author>
            //                                <price>8.99</price>
            //                            </book>
            //                        </bookstore>";

            //            XmlReader reader = XmlReader.Create(new StringReader(xmlString));


            if (dtData.Rows.Count > 0)
            {
                DataRow dRow = dtData.Rows[0];
                string xmlCode = Convert.ToString(dRow["XMLDataCode"]);
                string xmlData = Convert.ToString(dRow["XMLData"]);
                StringReader sr = new StringReader(xmlData);

                //XmlReader.Create(new StringReader(xmlString)))

                XmlReader xR = XmlReader.Create(sr);

                //SqlXml sXML =  dRow["XMLData"] as SqlXml;

                SqlXml sXML = new SqlXml(xR);
                //XMLClass xObj = ConvertSQLXmlToObject(sXML);

                List<XMLClass> xObjList = ConvertSQLXmlToObjectList(sXML);

                Label1.Text = xObjList[1].TextDate.ToString() + ", " + xObjList[1].TextAmount.ToString();

            }
            else
            {
                Label1.Text = "No Record!";
            }

        }



        protected void Button1_Click(object sender, EventArgs e)
        {
            InsertOracle();
            //InsertSQL();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            //ReadSQL();
            ReadOracle();
        }
    }
}