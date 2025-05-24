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

namespace SmartFactoryDemo.Controller.ManagerController
{
    public partial class EmployeeManagementControl : UserControl
    {
        public EmployeeManagementControl()
        {
            InitializeComponent();
            ReloadUserList();


        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            SetUserActiveStatus(false, "비활성화");
        }
        private void ReloadUserList()
        {
            Users users = new Users();
            guna2DataGridView1.DataSource = users.getUserInformation();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            SetUserActiveStatus(true,"활성화");
        }
        private void SetUserActiveStatus( bool isActive,string title)
        {
            DialogResult result =
              MessageBox.Show($"정말 계정을 {title} 하시겠습니까?",
              "경고", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (result == DialogResult.OK)
            {

                string employeeCode =
                    guna2DataGridView1.CurrentRow.Cells["employeeCode"].Value.ToString();

                string username =
                    guna2DataGridView1.CurrentRow.Cells["username"].Value.ToString();

                string dburl = new RegisterForm().connStr;

                using (SqlConnection sqlConnection = new SqlConnection(dburl))
                {

                    sqlConnection.Open();

                    string query = @"UPDATE Users 
                                 SET isActive = @isActive
                                 WHERE Username = @username AND
                                 EmployeeCode = @employeeCode";

                    using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@employeeCode", employeeCode);
                        cmd.Parameters.AddWithValue("@isActive",isActive);

                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show($"사용자 계정을 {title} 하였습니다.");
                }
                ReloadUserList();
            }
        }
    }
}
