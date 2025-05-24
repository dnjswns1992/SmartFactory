using SmartFactoryDemo.InternalClass;
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
using System.Windows.Forms.DataVisualization.Charting;
using SmartFactoryDemo.InternalClass;

namespace SmartFactoryDemo.Controller
{
    public partial class MaterialDashBoardController : UserControl
    {
        public MaterialDashBoardController()
        {
            InitializeComponent();
            getChartData();
            LoadPieChart();
            SmartFactoryDemo.InternalClass.Material material = new SmartFactoryDemo.InternalClass.Material();
            label1.Text = material.getAllMaterial().ToString();
            label2.Text = material.GetLowStockMaterialCount().ToString();
            label3.Text = material.GetTodayTransactionCount().ToString();
            guna2DataGridView1.DataSource = material.GetRecentTransactions();
            
        }

        public void getChartData()
        {
            string dburl = new RegisterForm().connStr;

            using (SqlConnection conn = new SqlConnection(dburl))
            {
                conn.Open();

                string query = @"
            SELECT 
                m.MaterialName,
                mi.StockQty,
                m.SafetyStock
            FROM Materials m
            JOIN MaterialInventory mi ON m.MaterialID = mi.MaterialID;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // 기존 데이터 제거
                    chart1.Series.Clear();
                    chart1.Titles.Clear();
                    chart1.ChartAreas.Clear();

                    // 차트 영역 생성
                    chart1.ChartAreas.Add("ChartArea1");

                    // 시리즈 생성
                    Series stockSeries = new Series("현재재고");
                    Series safetySeries = new Series("안전재고");

                    stockSeries.ChartType = SeriesChartType.Column;
                    safetySeries.ChartType = SeriesChartType.Column;

                    stockSeries.Color = Color.SteelBlue;
                    safetySeries.Color = Color.OrangeRed;

                    // 자재별로 막대 쌍 생성
                    while (reader.Read())
                    {
                        string materialName = reader.GetString(0);
                        int stockQty = reader.GetInt32(1);
                        int safetyStock = reader.GetInt32(2);

                        stockSeries.Points.AddXY(materialName, stockQty);
                        safetySeries.Points.AddXY(materialName, safetyStock);
                    }

                    chart1.Series.Add(stockSeries);
                    chart1.Series.Add(safetySeries);

                    chart1.Titles.Add("자재별 현재 재고 vs 안전재고");
                }
            }
        
        }

        private void LoadPieChart()
        {
            SmartFactoryDemo.InternalClass.Material material = new SmartFactoryDemo.InternalClass.Material();
            DataTable dt = material.GetMaterialCategoryStats();

            chart2.Series.Clear();
            chart2.Series.Add("자재 분포");
            chart2.Series[0].ChartType = SeriesChartType.Pie;

            foreach (DataRow row in dt.Rows)
            {
                string category = row["Category"].ToString();
                int count = Convert.ToInt32(row["Count"]);
                chart2.Series[0].Points.AddXY(category, count);
            }
        }

        private void MaterialController_Load(object sender, EventArgs e)
        {

        }
    }
}
    
