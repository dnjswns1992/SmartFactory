using SmartFactoryDemo.Controller;
using SmartFactoryDemo.SmartFactoryForm.Material;
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
using SmartFactoryDemo.SmartFactoryForm.Calendar;
using SmartFactoryDemo.SmartFactoryForms.ManagerForms;

namespace SmartFactoryDemo
{
    public partial class Principal : Form
    {
        string fullName = "";
        string employeeCode = "";
        string department = "";

        public Principal(string username, string employeeCode, string department)
        {
            InitializeComponent();
            guna2Button1_overview_Click(null, null);
            this.fullName = username;
            this.employeeCode = employeeCode;
            this.department = department;

        }
        public Principal()
        {
            InitializeComponent();
            
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            guna2ShadowForm1.SetShadowForm(this);
            input_username.Text = fullName;
            input_employeecode.Text = employeeCode;


        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            try
            {
                // 대시보드 텍스트 및 이미지 설정
                label_dashboard.Text = "Employee Management";
                guna2PictureBox3_dashboard.Image = Properties.Resources.인사관리_대쉬보드;

                // TabControl이 Panel에 없으면 추가
                if (!guna2Panel2_container.Controls.Contains(guna2TabControl1))
                {
                    guna2Panel2_container.Controls.Add(guna2TabControl1);
                }


                guna2TabControl1.Dock = DockStyle.Fill;
                guna2TabControl1.BringToFront();

                // 직원관리 탭 선택 및 컨트롤 초기화
                foreach (TabPage tab in guna2TabControl1.TabPages)
                {
                    if (tab.Text == "직원관리" || tab.Name == "직원관리")
                    {
                        guna2TabControl1.SelectedTab = tab;
                        tab.Controls.Clear();

                        Employee_managementControl empControl = new Employee_managementControl();
                        empControl.Dock = DockStyle.Fill;
                        tab.Controls.Add(empControl);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"오류 발생: {ex.Message}");
            }
           
        }


        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel2_container_Paint(object sender, PaintEventArgs e)
        {
            DashboardForm dashboard = new DashboardForm();

            dashboard.TopLevel = false;
            dashboard.FormBorderStyle = FormBorderStyle.None;
            dashboard.Dock = DockStyle.Fill;

            guna2Panel2_container.Controls.Clear();
            guna2Panel2_container.Controls.Add(dashboard);
            dashboard.Show();
        }

        private void guna2Button1_overview_Click(object sender, EventArgs e)
        {
            label_dashboard.Text = "DashBoard Overview";
            guna2PictureBox3_dashboard.Image = Properties.Resources.대쉬보드;

            DashboardForm dashboard = new DashboardForm();

            dashboard.TopLevel = false;
            dashboard.FormBorderStyle = FormBorderStyle.None;
            dashboard.Dock = DockStyle.Fill;

            guna2Panel2_container.Controls.Clear();
            guna2Panel2_container.Controls.Add(dashboard);
            dashboard.Show();

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            label_dashboard.Text = "Manpower management";
            guna2PictureBox3_dashboard.Image = Properties.Resources.인력관리_대쉬보드;
            Production_management production = new Production_management();
            production.TopLevel = false;
            production.FormBorderStyle = FormBorderStyle.None;
            production.Dock = DockStyle.Fill;

            guna2Panel2_container.Controls.Clear();
            guna2Panel2_container.Controls.Add(production);
            production.Show();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            DialogResult result =
            MessageBox.Show("정말 로그아웃 하시겠습니까?",
                "경고", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                Users users = new Users();
                int userpk = users.getUserPK(fullName, employeeCode);


                string query = @"
                             UPDATE AttendanceStatus
                             SET LogoutTime = @logoutTime
                             WHERE AttendanceID = (
                             SELECT TOP 1 AttendanceID 
                             FROM AttendanceStatus 
                             WHERE UserID = @userid AND CAST(LoginTime AS DATE) = CAST(GETDATE() AS DATE)
                             ORDER BY LoginTime DESC
                              )";

                using (SqlConnection con = new SqlConnection(new RegisterForm().connStr))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@userid", userpk);
                        cmd.Parameters.AddWithValue("@logoutTime", DateTime.Now);

                        int affectedRows = cmd.ExecuteNonQuery();

                    }

                    string statQuery = @"update Users SET AttendanceStatus = @stat
                                       WHERE UserID = @userid";

                    using (SqlCommand statCmd = new SqlCommand(statQuery, con))
                    {
                        statCmd.Parameters.AddWithValue("@stat", "퇴근");
                        statCmd.Parameters.AddWithValue("@userid", userpk);

                        statCmd.ExecuteNonQuery();
                    }

                }

                this.Hide();
                ShellForm form1 = new ShellForm();
                form1.Show();
            }
        }

        private void guna2Panel_top_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (guna2TabControl1.SelectedTab == 사용자관리)
            {
                if (사용자관리.Controls.Count == 0)
                {
                    AttendanceForm attendanceForm = new AttendanceForm();
                    attendanceForm.TopLevel = false;
                    attendanceForm.FormBorderStyle = FormBorderStyle.None;
                    attendanceForm.Dock = DockStyle.Fill;

                    사용자관리.Controls.Add(attendanceForm);
                    attendanceForm.Show();
                }
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            label_dashboard.Text = "Material Management";
            guna2PictureBox3_dashboard.Image = Properties.Resources.자재관리;

            // MaterialDashBoard 폼을 컨트롤처럼 패널에 붙이기
            MaterialDashBoard material = new MaterialDashBoard();
            material.TopLevel = false;
            material.FormBorderStyle = FormBorderStyle.None;
            material.Dock = DockStyle.Fill;

            guna2Panel2_container.Controls.Clear();
            guna2Panel2_container.Controls.Add(material);
            material.Show();

        }

        private void 직원관리_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            label_dashboard.Text = "Sechdule Management";
            guna2PictureBox3_dashboard.Image = Properties.Resources.스케쥴관리;

            CalendarForm calendarForm = new CalendarForm();
            calendarForm.TopLevel = false;
            calendarForm.FormBorderStyle = FormBorderStyle.None;
            calendarForm.Dock = DockStyle.Fill;

            guna2Panel2_container.Controls.Clear();
            guna2Panel2_container.Controls.Add(calendarForm);

            calendarForm.Show();
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {

            label_dashboard.Text = "Admin";
            guna2PictureBox3_dashboard.Image = Properties.Resources.관리자;

            string dburl = new RegisterForm().connStr;
         

            using (SqlConnection conn = new SqlConnection(dburl))
            {
                conn.Open();

                string query = @"SELECT Role AS 권한
                               FROM Users
                               WHERE FullName = @fullName AND
                               EmployeeCode = @employeeCode";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@fullName ", fullName);
                    cmd.Parameters.AddWithValue("@employeeCode", employeeCode);
                    object resultobj = cmd.ExecuteScalar();

                    if (resultobj != null)
                    {
                        string roleUser = resultobj.ToString();
                        

                        if (roleUser != "Admin")
                        {
                            MessageBox.Show("사용자 권한이 없습니다!");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("찾을 수 없는 사용자입니다");
                        return;
                    }
                }
                

                ManagerForm managerForm = new ManagerForm();
                managerForm.TopLevel = false;
                managerForm.FormBorderStyle = FormBorderStyle.None;
                managerForm.Dock = DockStyle.Fill;
                guna2Panel2_container.Controls.Clear();
                guna2Panel2_container.Controls.Add(managerForm);

                managerForm.Show();
            }
        }
    }
}
