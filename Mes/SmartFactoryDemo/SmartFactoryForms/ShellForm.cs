using Guna.UI2.WinForms;
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
    public partial class ShellForm : Form
    {
        public ShellForm()
        {
            InitializeComponent();
        }

        private void guna2CustomGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            guna2ShadowForm1.SetShadowForm(this);
        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {

        }

        private void guna2ShadowPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegisterForm register = new RegisterForm();
            register.Show();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            RegisterForm register = new RegisterForm();
            string con = register.connStr;

            using (SqlConnection sqlConnection = new SqlConnection(con))
            {
                sqlConnection.Open();

                string query = @"SELECT FullName, Department, EmployeeCode, IsActive
                 FROM Users 
                 WHERE EmployeeCode = @EmployeeCode 
                   AND Username = @Username 
                   AND Password = @Password";

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@EmployeeCode", inputEmployeeCode.Text.Trim());
                    sqlCommand.Parameters.AddWithValue("@Username", inputID.Text.Trim());
                    sqlCommand.Parameters.AddWithValue("@Password", inputPassword.Text.Trim());
                    

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            bool isActive = Convert.ToBoolean(reader["IsActive"]);

                            if (!isActive)
                            {
                                MessageBox.Show("접속이 제한된 계정입니다");
                                return;
                            }

                            string FullName = reader["FullName"].ToString();
                            string department = reader["Department"].ToString();
                            string EmployeeCode = reader["EmployeeCode"].ToString();

                            InsertAttendanceStatus status = new InsertAttendanceStatus();
                            int userPK = status.getUserID(inputID.Text.Trim(), EmployeeCode);
                           
                            status.RecordAttendance(userPK);

                            reader.Close();

                            string updateQuery = @"UPDATE Users SET 
                                AttendanceStatus ='출근', LastLogin = @lastLogin
                             WHERE Username = @Username AND EmployeeCode = @EmployeeCode";

                            try
                            {


                                using (SqlCommand updateCmd = new SqlCommand(updateQuery, sqlConnection))
                                {
                                    updateCmd.Parameters.
                                        AddWithValue("@Username", inputID.Text.Trim());

                                    updateCmd.Parameters.
                                        AddWithValue("@EmployeeCode", inputEmployeeCode.Text.Trim());
                                    

                                    updateCmd.Parameters.AddWithValue("@lastLogin", DateTime.Now);

                                    updateCmd.ExecuteNonQuery();

                                }

                                
                                MessageBox.Show("로그인 성공");

                                this.Hide();
                                LoadingScreen loading = new LoadingScreen(FullName, EmployeeCode, department);

                                loading.Show();

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            }
                        else
                        {
                            MessageBox.Show("입력하신 정보가 일치하지 않습니다");
                        }
                    }
                }

            }
        }
    }
}

