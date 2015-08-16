using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using MySql.Data.MySqlClient;

namespace DAL
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
                    "studentNumber", "firstName",
                    "lastName", "middleName",
                    "program", "schoolYear", "term"};
            }
        }

        public IEnumerable<Student> GetAllDepartment
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
                        student.StudentNumber = rd["studentNumber"].ToString();
                        student.Firstname = rd["firstName"].ToString();
                        student.Lastname = rd["lastName"].ToString();
                        student.Middlename = rd["middleName"].ToString();
                        student.Program = rd["program"].ToString();
                        student.SchoolYear = rd["schoolYear"].ToString();
                        student.Term = Convert.ToInt32(rd["term"]);

                        students.Add(student);
                    }

                    myConn.Close();
                }
                return students;
            }
        }
        #endregion

        #region -- Methods --
        public void Insert(Student department)
        {
            ExecuteNonQuery(QueryBuilder.Insert(_tableName, TargetFields), SetParams(department));
        }

        public void Update(Student department, int id)
        {
            ExecuteNonQuery(QueryBuilder.Update(_tableName, TargetFields, id, _pkField), SetParams(department));
        }
        // sets parameters for insert/update

        private Dictionary<string, object> SetParams(Student department)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            result.Add("@userName", department.Username);
            result.Add("@userPassword", department.Password);
            result.Add("@userStatus", department.Status);
            result.Add("@userTypeID", department.RoleID);
            return result;
        }
        #endregion



    }
}
