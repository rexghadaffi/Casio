using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Excel;
using System.Data;
using MySql.Data.MySqlClient;

namespace DataAccessLayer
{
    public class AccountabilitiesContext : DataAccessHelper
    {
        private string _tableName = "tblaccountability";
        private string _pkField = "accountabilityID";

        public List<string> TargetFields
        {
            get
            {
                return new List<string> {
                     "description",
                     "studentNumber",
                     "departmentID"
                     };
            }
        }

        public IEnumerable<Accountability> Fetch
        {
            get
            {
                List<Accountability> accountabilities = new List<Accountability>();
                using (MySqlConnection myConn = MySqlConn)
                {
                    MySqlCommand cmd = new MySqlCommand
                    {
                        CommandText = QueryBuilder.SelectAll(_tableName),
                        Connection = myConn
                    };
                    myConn.Open();
                    MySqlDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        Accountability accountability = new Accountability();

                        accountability.ID = Convert.ToInt32(rd[_pkField]);
                        accountability.DepartmentID = Convert.ToInt32(rd["departmentID"]);
                        accountability.Description = rd["description"].ToString();
                        accountability.StudentNumber = rd["studentNumber"].ToString();
                        accountability.Status = Convert.ToBoolean(rd["status"]);
                        accountabilities.Add(accountability);
                    }

                    myConn.Close();
                }

                return accountabilities;
            }
        }

        public void AddAccountability(Accountability data)
        {
            ExecuteNonQuery(QueryBuilder.Insert(_tableName, TargetFields), SetParams(data));
        }

        public string MassUpload(HttpPostedFileBase excelFile, int departmentID)
        {
            try
            {
                foreach (KeyValuePair<string, string> value in ReadExcel(excelFile))
                {
                    Accountability accountability = new Accountability
                    {
                        DepartmentID = departmentID,
                        Description = value.Value,
                        StudentNumber = value.Key
                    };
                    AddAccountability(accountability);
                }
                return "Mass upload was successful!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private Dictionary<string, object> SetParams(Accountability data)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            result.Add("@description", data.Description);
            result.Add("@studentNumber", data.StudentNumber);
            result.Add("@departmentID", data.DepartmentID);

            return result;
        }

        public Dictionary<string, string> ReadExcel(HttpPostedFileBase excelFile)
        {
            IExcelDataReader reader = CheckExcelVersion(Path.GetExtension(excelFile.FileName),
                                                        GetFileStream(excelFile));


            return FetchData(reader);
        }

        private MemoryStream GetFileStream(HttpPostedFileBase excelFile)
        {
            BinaryReader b = new BinaryReader(excelFile.InputStream);

            byte[] binData = b.ReadBytes(Convert.ToInt32(excelFile.InputStream.Length));
            return new MemoryStream(binData);
        }

        private IExcelDataReader CheckExcelVersion(string extension, MemoryStream stream)
        {

            IExcelDataReader dataReader = null;

            switch (extension)
            {
                case ".xls":
                    dataReader = ExcelReaderFactory.CreateBinaryReader(stream);
                    break;
                case ".xlsx":
                    dataReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    break;
                default:
                    dataReader = null;
                    break;
            }

            return dataReader;
        }

        private DataSet ConvertExcelToDataSet(IExcelDataReader parsedExcel)
        {
            DataSet result = parsedExcel.AsDataSet();

            return result;
        }
        private Dictionary<string, string> FetchData(IExcelDataReader excelData)
        {

            DataTable excelDataTable = excelData.AsDataSet().Tables[0];

            int columnCount = excelDataTable.Columns.Count;

            var query = from a in excelDataTable.AsEnumerable()
                        select a;

            Dictionary<string, string> ColumnValues = new Dictionary<string, string>();

            foreach (var item in query)
            {
                ColumnValues.Add(ValidateStudentNumber(item.Field<double>(0)),
                                 item.Field<string>(1));
            }

            return ColumnValues;
        }

        private string ValidateStudentNumber(double data)
        {
            string result = data.ToString();

            if (result.Length < 11)
            {
                result = AddZeros(result);
                return result;
            }
            return result;
        }

        public string AddZeros(string data)
        {
            string zeros = "";
            for (int i = data.Length; i < 11; i++)
            {
                zeros += string.Format("{0}", "0");
            }
            return zeros + data;
        }

        //--Commented for re use purposes
        //private List<string> GetPreceedingColumns(IExcelDataReader excelData)
        //{
        //    System.Data.DataTable excelDataTable = excelData.AsDataSet().Tables[0];

        //    excelDataTable.Columns.Remove("Column1");

        //    int columnCount = excelDataTable.Columns.Count;

        //    var query = from a in excelDataTable.AsEnumerable()
        //                select a;

        //    List<string> remainingColumnValues = new List<string>();

        //    foreach (var item in query)
        //    {
        //        for (int i = 0; i < columnCount; i++)
        //        {
        //            remainingColumnValues.Add(item.Field<string>(i));
        //        }
        //    }

        //    return remainingColumnValues;
        //}


    }
}
