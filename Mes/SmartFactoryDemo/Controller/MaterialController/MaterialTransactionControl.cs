using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SmartFactoryDemo.InternalClass;

namespace SmartFactoryDemo.Controller.MaterialController
{
    public partial class MaterialTransactionControl : UserControl
    {
        public MaterialTransactionControl()
        {
            InitializeComponent();
            SmartFactoryDemo.InternalClass.Material material = new SmartFactoryDemo.InternalClass.Material();
           guna2DataGridView1.DataSource = material.getMaterialTrsaction();
        }
    }
}
