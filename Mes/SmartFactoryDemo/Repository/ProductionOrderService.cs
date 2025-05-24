using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFactoryDemo
{
    public class ProductionOrderService
    {


        public int GetTodayOrderCount()
        {
            int count = 0;

            RegisterForm register = new RegisterForm();
            string dburl = register.connStr;

            using (SqlConnection connection = new SqlConnection(dburl))
            {
                connection.Open();

                string query = @"SELECT COUNT(*) 
                                 FROM ProductionOrders 
                                 WHERE CAST(OrderDate AS DATE) = CAST(GETDATE() AS DATE)";

                using (SqlCommand cmd = new SqlCommand(query,connection))
                {
                    count = (int)cmd.ExecuteScalar();
                }


                return count;
            }

        }
        public int GetTodayCompletedOrderCount()
        {
            int count = 0;
            RegisterForm register = new RegisterForm();
            string dburl = register.connStr;

            using (SqlConnection connection = new SqlConnection(dburl))
            {
                connection.Open ();

                string query = @"SELECT COUNT(*) FROM ProductionOrders 
                                WHERE Status = '완료' AND CAST(OrderDate AS DATE) 
                                = CAST(GETDATE() AS DATE)";

                using(SqlCommand cmd = new SqlCommand(query, connection))
                {
                    count = (int)cmd.ExecuteScalar();

                }
                

            }

            return count;
        }
        public double GetAveragequantity()
        {
            double avg = 0;
            RegisterForm register = new RegisterForm();
            string dburl = register.connStr;
            using (SqlConnection connection = new SqlConnection(dburl))
            {
                connection.Open();

                string query = @"
                        SELECT AVG(Quantity) 
                        FROM ProductionOrders 
                        WHERE Status = '완료' 
                        AND OrderDate = CAST(GETDATE() AS DATE)";

                using (SqlCommand cmd = new SqlCommand(query, connection)) {

                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value && result != null)
                    {
                        avg = Convert.ToDouble(result); // ← 안전하게 변환
                    }
                }
                
            }
            return avg;
        }
    }
}