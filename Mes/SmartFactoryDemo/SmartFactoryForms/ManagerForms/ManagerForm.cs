using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SmartFactoryDemo.Controller.ManagerController;

namespace SmartFactoryDemo.SmartFactoryForms.ManagerForms
{
    public partial class ManagerForm : Form
    {
        public ManagerForm()
        {
            InitializeComponent();
            LoadControllToTab(new EmployeeManagementControl(), tabPage1);
        }

        private void guna2TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(guna2TabControl1.SelectedIndex)
            {
                case 0:
                    LoadControllToTab(new EmployeeManagementControl(), tabPage1);
                    break;
                    case 1:
                    LoadControllToTab(new MaterialRegisterControll(), tabPage2);
                    break;
                    case 2:
                    LoadControllToTab(new MaterialInventoryControl(), tabPage3);
                    break;
            }
        }
        private void LoadControllToTab(UserControl control, TabPage page)
        {
            page.Controls.Clear();              // 기존 컨트롤 제거
            control.Dock = DockStyle.Fill;      // 꽉 채우기
            page.Controls.Add(control);         // UserControl 추가
        }
    }
}

