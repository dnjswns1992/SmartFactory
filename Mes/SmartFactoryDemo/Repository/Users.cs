using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartFactoryDemo
{
    internal class Users
    {
        public Users()
        {

        }

        public int getUserPK(string fullName, string employeeCode)
        {
            int userpk = 0;

            string dburl = new RegisterForm().connStr;

            using (SqlConnection conn = new SqlConnection(dburl))
            {

                conn.Open();
                MessageBox.Show(fullName.ToString(), employeeCode.ToString());

                string query = @"SELECT UserID FROM Users WHERE FullName = @FullName
                                and EmployeeCode = @employeeCode";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FullName", fullName);
                    cmd.Parameters.AddWithValue("@employeeCode", employeeCode);

                    userpk = Convert.ToInt32(cmd.ExecuteScalar());

                }
            }

            return userpk;
        }
        public DataTable getUserInformation()
        {
            string dburl = new RegisterForm().connStr;
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(dburl))
            {
                conn.Open();

                string query = @"SELECT 
                                Username AS 사용자이름,
                                Role AS 권한,
                                Department AS 부서,
                                IsActive AS 활성화여부,
                                CreatedAt AS 가입날짜,
                                FullName AS 성명,
                                Position AS 직책,
                                EmployeeCode AS 직원코드
                                FROM Users";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }
    }
}
