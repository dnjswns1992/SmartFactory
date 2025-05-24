using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartFactoryDemo
{
    public partial class LoadingScreen : Form
    {
        private string username = "";
        private string employeeCode = "";
        private string department = "";
        public LoadingScreen()
        {
            InitializeComponent();
        }

        public LoadingScreen(string username, string employeeCode,string department)
        {
            InitializeComponent();
            this.username = username;
            this.employeeCode = employeeCode;
            this.department = department;

        }

        private void Loading_Load(object sender, EventArgs e)
        {
            guna2ShadowForm1.SetShadowForm(this);
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(guna2CircleProgressBar1.Value == 100)
            {
                timer1.Stop();
                this.Hide();
                Principal principal = new Principal(username,employeeCode,department);
                principal.Show();
                return;
            }

            guna2CircleProgressBar1.Value += 1;
            label_val.Text = (Convert.ToInt32(label_val.Text)+1).ToString();
        }

        private void guna2CircleProgressBar1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
