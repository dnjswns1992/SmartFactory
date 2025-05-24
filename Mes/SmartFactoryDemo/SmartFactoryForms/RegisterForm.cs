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
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace SmartFactoryDemo
{
    public partial class RegisterForm : Form
    {
        public string connStr = "Server=DESKTOP-JS4L9VV;Database=MYDB2;UID=sa;Password=dkdlzks!153;";

        SqlConnection sqlcon = null;
        private bool isPasswordVisible = true;
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("정말 종료하시겠습니까?", "경고", MessageBoxButtons.YesNoCancel
                , MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                this.Hide();
                ShellForm form = new ShellForm();
                form.Show();
            }

        }
        private void Register_Load(object sender, EventArgs e)
        {
            guna2ShadowForm1.SetShadowForm(this);
        }

        private void userPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void userPassword_IconRightClick(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;

            // 반대로 설정해야 맞음
            userPassword.UseSystemPasswordChar = !isPasswordVisible;

            userPassword.IconRight = isPasswordVisible
                ? Properties.Resources.open_eye
                : Properties.Resources.close_eye;
        }

        private void repeat_userPassword_IconRightClick(object sender, EventArgs e)
        {
            isPasswordVisible = !isPasswordVisible;

            // 반대로 설정해야 맞음
            userPassword.UseSystemPasswordChar = !isPasswordVisible;

            userPassword.IconRight = isPasswordVisible
                ? Properties.Resources.open_eye
                : Properties.Resources.close_eye;
        }

        private void employeeCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button2_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(userID.Text.Trim()) ||
               string.IsNullOrEmpty(userDepartment.Text.Trim()) ||
               string.IsNullOrEmpty(userName.Text.Trim()) ||
               string.IsNullOrEmpty(userPassword.Text.Trim()) ||
               string.IsNullOrEmpty(repeat_userPassword.Text.Trim()) ||
               string.IsNullOrEmpty(employeeCode.Text.Trim())
               )
            {
                MessageBox.Show("모든 필드를 입력 해주세요!", "경고");
            }
            else
            {
                int length = employeeCode.Text.Length;
                if (length != 4)
                {
                    MessageBox.Show("직원코드는 반드시 4자리여야 합니다.");
                    return;
                }
                string input = userName.Text.Trim();
                bool haskorean = Regex.IsMatch(input, @"[가-힣]");

                if (haskorean == false)
                {
                    MessageBox.Show("이름은 반드시 한글로 입력해야 합니다.");
                    return;
                }


                if (userPassword.Text == repeat_userPassword.Text)
                {


                    try
                    {



                        using (SqlConnection conn = new SqlConnection(connStr))
                        {
                            conn.Open();

                            // 🔍 직원코드 중복 체크 추가
                            string codeCheckQuery = "SELECT COUNT(*) FROM Users WHERE EmployeeCode = @EmployeeCode";
                            using (SqlCommand codeCheckCmd = new SqlCommand(codeCheckQuery, conn))
                            {
                                codeCheckCmd.Parameters.AddWithValue("@EmployeeCode", employeeCode.Text.Trim());
                                int codeCount = (int)codeCheckCmd.ExecuteScalar();

                                if (codeCount > 0)
                                {
                                    MessageBox.Show("직원코드가 이미 사용 중입니다.");
                                    return;
                                }


                                string checkQuery = "SELECT COUNT(*) FROM Users " +
                          "WHERE Username = @Username AND EmployeeCode = @EmployeeCode";

                                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                                {
                                    checkCmd.Parameters.AddWithValue("@Username", userID.Text.Trim());
                                    checkCmd.Parameters.AddWithValue("@EmployeeCode", employeeCode.Text.Trim());

                                    int count = (int)checkCmd.ExecuteScalar();

                                    if (count > 0)
                                    {
                                        MessageBox.Show("이미 존재하는 사용자 입니다");
                                        return;


                                    }



                                }

                                using (SqlCommand cmd = new SqlCommand("InsertUser", conn))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.AddWithValue("@Username", userID.Text.Trim());
                                    cmd.Parameters.AddWithValue("@Password", userPassword.Text.Trim());
                                    cmd.Parameters.AddWithValue("@Department", userDepartment.Text.Trim());
                                    cmd.Parameters.AddWithValue("@FullName", userName.Text.Trim());
                                    cmd.Parameters.AddWithValue("@IsActive", true);
                                    cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                                    cmd.Parameters.AddWithValue("@LastLogin", DateTime.Now);
                                    cmd.Parameters.AddWithValue("@Position", "employee");
                                    cmd.Parameters.AddWithValue("@Role", "User");
                                    cmd.Parameters.AddWithValue("@EmployeeCode", employeeCode.Text.Trim());
                                    cmd.Parameters.AddWithValue("@AttendanceStatus", "출근");

                                    cmd.ExecuteNonQuery();


                                }
                            }
                            MessageBox.Show("회원가입이 완료 되었습니다");
                            this.Hide();
                            ShellForm form = new ShellForm();
                            form.Show();

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }


                }
                else
                {
                    MessageBox.Show("비밀번호가 일치하지 않습니다");
                }
            }
        }
    }
}
