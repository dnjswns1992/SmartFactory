using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartFactoryDemo.Controller.Material
{
    public partial class MaterialListController : UserControl
    {
        public MaterialListController()
        {
            InitializeComponent();
            SmartFactoryDemo.InternalClass.Material material = new SmartFactoryDemo.InternalClass.Material();
            guna2DataGridView1.DataSource = material.GetAllMaterialGridView();
        }
    }
}
