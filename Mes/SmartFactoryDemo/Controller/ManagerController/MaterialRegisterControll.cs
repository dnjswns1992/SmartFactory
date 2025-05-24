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
using SmartFactoryDemo.InternalClass;

namespace SmartFactoryDemo.Controller.ManagerController
{
    public partial class MaterialRegisterControll : UserControl
    {
        public MaterialRegisterControll()
        {
            InitializeComponent();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
           SmartFactoryDemo.InternalClass.Material material = 
                new SmartFactoryDemo.InternalClass.Material();

            string mateiralName = guna2ComboBox2.Text.ToString();
            string unit = guna2ComboBox3.Text.ToString();
            string category = guna2ComboBox4.Text.ToString();
            string safetyStock = guna2TextBox1.Text.ToString();


            material.MaterialRegister(mateiralName, unit, category, safetyStock);
            material.MaterialInventoryRegister(mateiralName, Convert.ToInt32(safetyStock), DateTime.Now);
        }
    }
}
