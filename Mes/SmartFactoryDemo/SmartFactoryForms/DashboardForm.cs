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

namespace SmartFactoryDemo

{
    public partial class DashboardForm : Form
    {
        public DashboardForm()
        {
            InitializeComponent();
            LoadCategoryChart();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            ProductionOrderService orderService = new ProductionOrderService();
            int todaycount = orderService.GetTodayOrderCount();
            intput_todayCount.Text = todaycount.ToString();
            inputCompletedCount.Text = orderService.GetTodayCompletedOrderCount().ToString();
            guna2HtmlLabel6_avg.Text = orderService.GetAveragequantity().ToString();



            ProductionResults prodResult = new ProductionResults();
            var avgData = prodResult.GetAverageByDepartment();

            guna2HtmlLabel5_defectavg.Text = prodResult.GetAverageDefectRate().ToString("F5") + " %";

            // 차트 초기화
            chart1.Series.Clear();
            chart1.Series.Add("부서별 평균 생산량");
            chart1.Series[0].ChartType = SeriesChartType.Column;
            chart1.Series[0].IsValueShownAsLabel = true; // 수치 표시

            // 데이터 바인딩
            foreach (var (dept, avgQty) in avgData)
            {
                chart1.Series[0].Points.AddXY(dept, avgQty);
            }

            // 축 제목 설정
            chart1.ChartAreas[0].AxisX.Title = "부서";
            chart1.ChartAreas[0].AxisY.Title = "평균 생산량";

            // ✅ chart2 - 최근 30일간 불량률
            var defectRates = prodResult.GetDailyDefectRatesLast30Days();

            chart2.Series.Clear();
            chart2.Series.Add("일별 불량률");
            chart2.Series[0].ChartType = SeriesChartType.Line;
            chart2.Series[0].BorderWidth = 2;
            chart2.Series[0].IsValueShownAsLabel = false;

            foreach (var (date, rate) in defectRates)
            {
                chart2.Series[0].Points.AddXY(date.ToString("MM-dd"), rate);
            }

            chart2.ChartAreas[0].AxisX.Title = "날짜";
            chart2.ChartAreas[0].AxisY.Title = "불량률 (%)";
            chart2.ChartAreas[0].AxisX.Interval = 1; // 하루 단위로 표시
            chart2.ChartAreas[0].AxisX.LabelStyle.Angle = -45; // 라벨 기울이기

        }
        private void LoadCategoryChart()
        {
            string connStr = new RegisterForm().connStr;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = @"
            SELECT Category, COUNT(*) AS Count
            FROM Materials
            GROUP BY Category";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                // 차트 초기화
                chart3.Series.Clear();
                chart3.Titles.Clear();
                chart3.Series.Add("자재 분류");
                chart3.Series["자재 분류"].ChartType = SeriesChartType.Pie;

                foreach (DataRow row in dt.Rows)
                {
                    chart3.Series["자재 분류"].Points.AddXY(row["Category"], row["Count"]);
                }

                chart3.Titles.Add("자재 분류별 구성 비율");
                chart3.Series["자재 분류"]["PieLabelStyle"] = "Outside";
                chart3.Series["자재 분류"].Label = "#VALX: #PERCENT";
            }

        }
    }
}
