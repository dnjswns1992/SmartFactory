using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SmartFactoryDemo.Controller;
using SmartFactoryDemo.Controller.Material;
using SmartFactoryDemo.Controller.MaterialController;

namespace SmartFactoryDemo.SmartFactoryForm.Material
{
    public partial class MaterialDashBoard : Form
    {
        public MaterialDashBoard()
        {
            InitializeComponent();
            LoadControlToTab(new MaterialDashBoardController(), tabPage1);
        }

        private void guna2TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(guna2TabControl1.SelectedIndex)
            {
                case 0:
                    LoadControlToTab(new MaterialDashBoardController(), tabPage1);
                    break;
                case 1:
                    LoadControlToTab(new MaterialListController(),tabPage2);
                    break;
                case 2:
                    LoadControlToTab(new MaterialTransactionControl(), tabPage3);
                    break;
                case 3:
                    LoadControlToTab(new MaterialInventoryControll(), tabPage4);
                    break;
            }
        }
        private void LoadControlToTab(UserControl control, TabPage page)
        {
            page.Controls.Clear();              // 기존 컨트롤 제거
            control.Dock = DockStyle.Fill;      // 꽉 채우기
            page.Controls.Add(control);         // UserControl 추가
        }
    }
}
