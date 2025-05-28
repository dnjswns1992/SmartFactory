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
    public partial class Production_management : Form
    {
        public Principal Principal;

        public Production_management()
        {
            InitializeComponent();
        }
        public Production_management(Principal principal)
        {
            InitializeComponent();
            this.Principal = principal;
            principal.guna2TextBox1.Visible = false;
        }
        


        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Production_management_Load(object sender, EventArgs e)
        {
             LoadProuctionData();
        }
        private void LoadProuctionData()
        {
            string dburl = new RegisterForm().connStr;

            string query = @"SELECT 
                       O.OrderID AS 지시번호,
                       O.ProductCode AS 제품코드,
                       O.Quantity AS 지시수량,
                       ISNULL(R.ProducedQty, 0) AS 생산수량,
                       ISNULL(R.DefectQty, 0) AS 불량수량,
                       O.DueDate AS 납기일,
                       R.ResultDate AS 실적일,
                       O.Status AS 지시상태,
                       CAST(
                             CASE 
                               WHEN O.Quantity = 0 THEN 0
                               WHEN R.ProducedQty IS NULL THEN 0
                               ELSE ROUND(CAST(R.ProducedQty AS FLOAT) / O.Quantity * 100, 1)
                             END AS FLOAT
                       ) AS 달성률
                     FROM 
                       ProductionOrders O
                     LEFT JOIN 
                       ProductionResults R ON O.OrderID = R.OrderID;";

            using (SqlConnection conn = new SqlConnection(dburl))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                guna2DataGridView1.AutoGenerateColumns = true;
                guna2DataGridView1.DataSource = dt;

            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string keyword = guna2TextBox1.Text;
            string dburl = new RegisterForm().connStr;

            using (SqlConnection conn = new SqlConnection(dburl))

            {

                conn.Open();



                string query = @"
                    SELECT 
                    O.OrderID AS 지시번호,
                    O.ProductCode AS 제품코드,
                    O.Quantity AS 지시수량,
                    ISNULL(R.ProducedQty, 0) AS 생산수량,
                    ISNULL(R.DefectQty, 0) AS 불량수량,
                    O.DueDate AS 납기일,
                    R.ResultDate AS 실적일,
                    O.Status AS 지시상태,
                    CAST(
                    CASE 
                    WHEN O.Quantity = 0 THEN 0
                    WHEN R.ProducedQty IS NULL THEN 0
                    ELSE ROUND(CAST(R.ProducedQty AS FLOAT) / O.Quantity * 100, 1)
                     END AS FLOAT
                    ) AS 달성률
                    FROM 
                     ProductionOrders O
                    LEFT JOIN 
                    ProductionResults R ON O.OrderID = R.OrderID
                     WHERE 
                    O.ProductCode LIKE @keyword OR
                     O.OrderID LIKE @keyword OR
                      O.Status LIKE @keyword OR
                    CONVERT(VARCHAR, O.DueDate, 23) LIKE @keyword OR
                   CONVERT(VARCHAR, R.ResultDate, 23) LIKE @keyword OR
                  CAST(O.Quantity AS VARCHAR) LIKE @keyword OR
                  CAST(R.ProducedQty AS VARCHAR) LIKE @keyword OR
                  CAST(R.DefectQty AS VARCHAR) LIKE @keyword OR
                   CAST(
                     CASE 
                      WHEN O.Quantity = 0 THEN 0
                      WHEN R.ProducedQty IS NULL THEN 0
                       ELSE ROUND(CAST(R.ProducedQty AS FLOAT) / O.Quantity * 100, 1)
                    END AS VARCHAR
                     ) LIKE @keyword;
                       ";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                guna2DataGridView1.DataSource = dt;
            }
        }
    }
}
