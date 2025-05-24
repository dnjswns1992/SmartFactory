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
    public partial class MaterialInventoryControl : UserControl
    {
        public MaterialInventoryControl()
        {
            InitializeComponent();
            LoadMaterialIDs();
        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {
            
        }

        //Material Table에 있는 MaterialID를 콤보박스에 넣는 로직
        private void LoadMaterialIDs()
        {
            string connStr = new RegisterForm().connStr;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = "SELECT MaterialID, MaterialName FROM Materials";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                guna2ComboBox1.DataSource = dt;
                guna2ComboBox1.DisplayMember = "MaterialName"; // 사용자에게 보여줄 값
                guna2ComboBox1.ValueMember = "MaterialID";     // 실제 사용할 값
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            SmartFactoryDemo.InternalClass.Material material =
                new SmartFactoryDemo.InternalClass.Material();

            int materialID = -1;

            if (guna2ComboBox1.SelectedValue == null)
            {
                MessageBox.Show("자재를 선택해주세요.");
                return;
            }
            if (guna2ComboBox1.SelectedValue != null && int.TryParse(guna2ComboBox1.SelectedValue.ToString(), out materialID))
            {
                // materialID는 여기서 안전하게 int로 사용 가능
                Console.WriteLine("선택된 MaterialID: " + materialID);
            }
            string materialType = guna2ComboBox2.SelectedItem.ToString();
            string qty = guna2TextBox1.Text.ToString();
            DateTime selectedDate = guna2DateTimePicker1.Value;
            string note = guna2TextBox2.Text.ToString();


            material.MaterialTrsansactionRegister(materialID, materialType, qty, selectedDate, note); ;
        }
    }
}
