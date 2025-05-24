using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartFactoryDemo
{
    public partial class AttendanceForm : Form
    {
        public AttendanceForm()
        {
            InitializeComponent();
        }

        private void AttendanceForm_Load(object sender, EventArgs e)
        {
            getAttendanceData();
        }
        private void getAttendanceData()
        {

            string dburl = new RegisterForm().connStr;

            using (SqlConnection sqlConnection = new SqlConnection(dburl))
            {
                sqlConnection.Open();

                string query = @"SELECT
                u.FullName AS 이름,
                u.Username AS 아이디, 
                u.Department AS 부서,
                a.LoginTime AS 로그인,
                a.LogoutTime AS 로그아웃,
                a.PunctualityStatus AS 근무상태
                FROM [MYDB2].[dbo].[Users] u
                INNER JOIN [MYDB2].[dbo].[AttendanceStatus] a
                ON u.UserID = a.UserID
                ORDER BY a.LoginTime DESC";

                SqlDataAdapter adapter = new SqlDataAdapter(query,sqlConnection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                guna2DataGridView1.DataSource = dt;


                           
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
