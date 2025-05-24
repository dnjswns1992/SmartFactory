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

namespace SmartFactoryDemo.Controller
{
    public partial class Employee_managementControl : UserControl
    {
        public Employee_managementControl()
        {
            InitializeComponent();
            LoadEmployeeData();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        //User 테이블과 Worker테이블을 조인
        
        public void LoadEmployeeData()
        {
            string connStr = new RegisterForm().connStr;

            string query = @"
                SELECT 
                    U.FullName AS 이름,
                    U.EmployeeCode AS 직원코드,
                    W.WorkerCode AS 워크코드,
                    W.Department AS 부서,
                    W.Shift AS 근무조,
                    W.HireDate AS 입사일,
                    W.SkillLevel AS 숙련도,
                    U.AttendanceStatus AS 출근상태,
                    U.LastLogin AS 마지막접속
                FROM 
                    Users U
                INNER JOIN 
                    Workers W ON U.UserID = W.UserID;";

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                guna2DataGridView1.AutoGenerateColumns = true;
                guna2DataGridView1.DataSource = dt;

            }
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
