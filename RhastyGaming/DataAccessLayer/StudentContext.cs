using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using MySql.Data.MySqlClient;

namespace DataAccessLayer
{
    public class StudentContext : DataAccessHelper
    {
        #region -- Private Members --
        private static string _pkField = "studentID";
        private static string _tableName = "tblstudent";
        #endregion

        #region -- Properties --
        // maps fields in the database to be inserted or updated.
        private List<string> TargetFields
        {
            get
            {
                    return new List<string> {
                    "firstName", "userPassword",
                    "lastName", "middleName",
                    "schoolYear", "term",
                    "year", "userStatus"  };
            }
        }

        public IEnumerable<Student> GetAllStudent
        {
            get
            {
                List<Student> students = new List<Student>();
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
                        Student student = new Student();

                        student.ID = Convert.ToInt32(rd[_pkField]);                    
                        student.Firstname = rd["firstName"].ToString();
                        student.Lastname = rd["lastName"].ToString();
                        student.Middlename = rd["middleName"].ToString();
                        student.Program = rd["program"].ToString();
                        student.SchoolYear = rd["schoolYear"].ToString();
                        student.StudentNumber = rd["studentNumber"].ToString();
                        student.Term = Convert.ToInt32(rd["term"]);
                        student.Year = Convert.ToInt32(rd["year"]);
                        student.Status = Convert.ToBoolean(rd["userStatus"]);
                        student.Username = rd["userName"].ToString();
                        student.Password = rd["userPassword"].ToString();
                        students.Add(student);
                    }

                    myConn.Close();
                }
                return students;
            }
        }
        #endregion

        #region -- Methods --
        public void Insert(Student student)
        {
            ExecuteNonQuery(QueryBuilder.Insert(_tableName, TargetFields), SetParams(student));
        }
        public void Update(Student student, int id)
        {
            ExecuteNonQuery(QueryBuilder.Update(_tableName, TargetFields, id, _pkField), SetParams(student));
        }

        public void MassUpload(string fileName) 
        {
            string query = "DROP TABLE IF EXISTS tbltemporary;" +
                           "CREATE  TABLE IF NOT EXISTS tbltemporary LIKE tblstudent;" +
                           "set names utf8;" +
                           "LOAD DATA LOCAL INFILE 'C://Users//i-jcadiao//Documents//Babawokie//Casio//RhastyGaming//RhastyGaming//uploads//student//" + fileName + "'" +
                           "into table tbltemporary FIELDS TERMINATED BY ',' OPTIONALLY ENCLOSED BY '\"' " +
                           "ESCAPED BY '' LINES TERMINATED BY '\r\n' IGNORE 1 LINES " +
                           "(studentNumber, lastName, firstName, middleName, program, year, term, schoolYear);" +
                           "SHOW COLUMNS FROM tblstudent;" +
                           "set names utf8;" +
                           "INSERT INTO tblstudent SELECT * FROM tbltemporary ON DUPLICATE KEY UPDATE" +
                           " studentNumber = VALUES(studentNumber), lastName = VALUES(lastName)," +
                           "firstName = VALUES(firstName), middleName = VALUES(middleName)," +
                           "program = VALUES(program), year = VALUES(year),term = VALUES(term),schoolYear = VALUES(schoolYear);" +                         
                           "DROP TABLE IF EXISTS tbltemporary";

            using (MySqlConnection strConn = MySqlConn)
            {
                strConn.Open();
                MySqlCommand cmd = new MySqlCommand(query, strConn);
                cmd.CommandTimeout = 900;
                cmd.ExecuteNonQuery();
                strConn.Close();
            }                 
        }


        // sets parameters for insert/update

        private Dictionary<string, object> SetParams(Student student)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            result.Add("@firstName", student.Firstname);
            result.Add("@userPassword", student.Password);
            result.Add("@lastName", student.Lastname);
            result.Add("@middleName", student.Middlename);
            result.Add("@schoolYear", student.SchoolYear);
            result.Add("@term", student.Term);
            result.Add("@year", student.Year);
            result.Add("@userStatus", student.Status);    
    
            return result;
        }
        #endregion



    }
}
