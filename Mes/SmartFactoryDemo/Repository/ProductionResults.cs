using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFactoryDemo
{
    internal class ProductionResults
    {
        public List<(string Department, double AvgQty)> GetAverageByDepartment()
        {
            List<(string, double)> result = new List<(string, double)>();

            RegisterForm register = new RegisterForm();
            string connStr = register.connStr;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = @"
            SELECT Department, AVG(ProducedQty) AS AvgQty
            FROM ProductionResults
            GROUP BY Department;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string dept = reader["Department"].ToString();
                        double avgQty = Convert.ToDouble(reader["AvgQty"]);
                        result.Add((dept, avgQty));
                    }
                }
            }

            return result;
        }
        public double GetAverageDefectRate()
        {
            double rate = 0;

            RegisterForm register = new RegisterForm();
            string connStr = register.connStr;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = @"
            SELECT 
                    CAST(SUM(DefectQty) AS FLOAT) / NULLIF(SUM(ProducedQty), 0) * 100 AS DefectRateToday
                    FROM [MYDB2].[dbo].[ProductionResults]
                    WHERE ResultDate = CAST(GETDATE() AS DATE);";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    object result = cmd.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        rate = Convert.ToDouble(result); 
                    }
                }
            }

            return rate;
        }
        public List<(DateTime Date, double DefectRate)> GetDailyDefectRatesLast30Days()
        {
            List<(DateTime, double)> result = new List<(DateTime, double)>();

            RegisterForm register = new RegisterForm();
            string connStr = register.connStr;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = @"
            SELECT 
                ResultDate,
                CAST(SUM(DefectQty) AS FLOAT) / NULLIF(SUM(ProducedQty), 0) * 100 AS DefectRate
            FROM ProductionResults
            WHERE ResultDate >= DATEADD(DAY, -30, CAST(GETDATE() AS DATE))
            GROUP BY ResultDate
            ORDER BY ResultDate;
        ";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DateTime date = Convert.ToDateTime(reader["ResultDate"]);
                        double rate = Convert.ToDouble(reader["DefectRate"]);
                        result.Add((date, rate));
                    }
                }
            }

            return result;
        }
    }
}
