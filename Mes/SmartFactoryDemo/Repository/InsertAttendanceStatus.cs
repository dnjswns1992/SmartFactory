using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFactoryDemo
{

    internal class InsertAttendanceStatus
    {

        public int getUserID(string username, string EmployeeCode)
        {
            RegisterForm register = new RegisterForm();
            string dburl = register.connStr;
            int userId = -1;

            using (SqlConnection conn = new SqlConnection(dburl))
            {

                conn.Open();

                string query = @"SELECT UserID FROM Users WHERE Username = @fullName AND
                                EmployeeCode = @employeeCode";

                using (SqlCommand cmd = new SqlCommand(query,conn))
                {
                    cmd.Parameters.AddWithValue("@fullName", username);
                    cmd.Parameters.AddWithValue("@employeeCode", EmployeeCode);

                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        userId = Convert.ToInt32(result);
                    }

                }

                return userId;
            }
        }
        public void RecordAttendance(int userID)
        {
            RegisterForm register = new RegisterForm();
            string dburl = register.connStr;

            using (SqlConnection conn = new SqlConnection(dburl))
            {

                conn.Open();

                using (SqlCommand cmd = new SqlCommand("InsertAttendanceStatus", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserID", userID);
                    


                    try
                    {
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("출근기록 저장완료");

                    }catch(Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
            }
        }
    }
}

