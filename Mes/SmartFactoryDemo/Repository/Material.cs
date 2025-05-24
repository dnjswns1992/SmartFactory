using Guna.UI2.WinForms;
using ScottPlot.TickGenerators.Financial;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartFactoryDemo.InternalClass // 네임스페이스는 대소문자 자유
{
    internal class Material
    {

        public int getAllMaterial()
        {
            string dburl = new RegisterForm().connStr;
            int rows = 0;

            using (SqlConnection conn = new SqlConnection(dburl))
            {
                conn.Open();

                string query = @"SELECT COUNT(*) FROM Materials";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    rows = Convert.ToInt32(cmd.ExecuteScalar());


                }

            }

            return rows;
        }
        public int GetLowStockMaterialCount()
        {
            string dburl = new RegisterForm().connStr;
            int rows = 0;

            using (SqlConnection conn = new SqlConnection(dburl))
            {
                conn.Open();

                string query = @"SELECT COUNT(*)
                                FROM Materials m
                                JOIN MaterialInventory mi
                                ON m.MaterialID = mi.MaterialID
                                WHERE mi.StockQty < m.SafetyStock;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    rows = Convert.ToInt32(cmd.ExecuteScalar());


                }

            }

            return rows;
        }
        public int GetTodayTransactionCount()
        {
            string dburl = new RegisterForm().connStr;
            int rows = 0;

            using (SqlConnection conn = new SqlConnection(dburl))
            {
                conn.Open();

                string query = @"SELECT COUNT(*) 
                                FROM MaterialTransactions
                                WHERE CAST(TransactionDate AS DATE) =
                                CAST(GETDATE() AS DATE);";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    rows = Convert.ToInt32(cmd.ExecuteScalar());


                }

            }

            return rows;
        }
        public DataTable GetRecentTransactions()
        {
            string dburl = new RegisterForm().connStr;
            DataTable dt = new DataTable();
            int rows = 0;

            using (SqlConnection conn = new SqlConnection(dburl))
            {
                conn.Open();

                string query = @"SELECT TOP 3 
                               m.MaterialName AS 자재명,
                               t.TransactionType AS 타입,
                               t.Quantity AS 수량,
                               t.TransactionDate
                               FROM MaterialTransactions t
                               JOIN Materials m ON t.MaterialID = m.MaterialID
                               ORDER BY t.TransactionDate DESC;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }


                }

            }
            return dt;
        }
        public DataTable GetMaterialCategoryStats()
        {
            string dburl = new RegisterForm().connStr;
            DataTable dataTable = new DataTable();

            using (SqlConnection conn = new SqlConnection(dburl))
            {

                conn.Open();

                string query = @"
                 SELECT Category, COUNT(*) AS Count
                 FROM Materials
                 GROUP BY Category;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dataTable);
                    }

                }
            }

            return dataTable;
        }
        public DataTable GetAllMaterialGridView()
        {
            string dburl = new RegisterForm().connStr;
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(dburl))
            {
                conn.Open();

                string query = @"
            SELECT 
                MaterialName AS 자재이름,
                Unit AS 무게,
                Category AS 카테고리,
                SafetyStock AS 안전재고
            FROM Materials";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }

            return dt;
        }
        public DataTable getMaterialTrsaction()
        {
            string dburl = new RegisterForm().connStr;
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(dburl))
            {
                conn.Open();

                string query = @"
                                 SELECT 
                                 MT.MaterialID AS 자재번호,
                                 M.MaterialName AS 자재이름,
                                 MT.TransactionType AS 입출고,
                                 MT.Quantity AS 수량,
                                 MT.TransactionDate AS 입출고시간,
                                 MT.Note AS 비고
                                 FROM 
                                 MaterialTransactions MT
                                 INNER JOIN 
                                 Materials M ON MT.MaterialID = M.MaterialID";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dt);

                    }
                }
            }
            return dt;
        }
        public DataTable getMaterialInventory()
        {
            string dburl = new RegisterForm().connStr;
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(dburl))
            {
                conn.Open();

                string query = @"
                 SELECT 
                MI.MaterialID AS 자재번호,
                M.MaterialName AS 자재이름,
                MI.StockQty AS 재고수량,
                MI.LastUpdate AS 마지막업데이트
                FROM 
                MaterialInventory MI
                INNER JOIN 
                Materials M ON MI.MaterialID = M.MaterialID";

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                }

            }
            return dt;
        }
        /**
         * Material Table에 데이터들을 등록하는 메서드
         * **/
        public void MaterialRegister(string materialName, string unit, string category, string safetyStockText)
        {
            // 유효성 검사
            if (string.IsNullOrEmpty(materialName) || string.IsNullOrEmpty(unit) ||
                string.IsNullOrEmpty(category) || string.IsNullOrEmpty(safetyStockText))
            {
                MessageBox.Show("모든 항목을 입력해 주세요.");
                return;
            }

            // 안전재고 숫자 변환
            if (!int.TryParse(safetyStockText, out int safetyStock))
            {
                MessageBox.Show("안전 재고는 숫자만 입력 가능합니다.");
                return;
            }

            string connStr = new RegisterForm().connStr;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("InsertMaterial", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MaterialName", materialName);
                    cmd.Parameters.AddWithValue("@Unit", unit);
                    cmd.Parameters.AddWithValue("@Category", category);
                    cmd.Parameters.AddWithValue("@SafetyStock", safetyStock);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("자재가 성공적으로 등록되었습니다.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("등록 실패: " + ex.Message);
                    }
                }
            }
        }
        /**
         * MaterialInventory 삽입하는 메서드
         * */
        public void MaterialInventoryRegister(string materialName, int stockQty, DateTime lastupadate)
        {


            string dburl = new RegisterForm().connStr;
            int materialID = -1;
            using (SqlConnection conn = new SqlConnection(dburl))
            {

                conn.Open();

                string query = @"SELECT MaterialID FROM Materials 
                                  WHERE 
                                  MaterialName = @materialName AND
                                  SafetyStock = @safetyStock";



                using (SqlCommand sqlCommand = new SqlCommand(query, conn))
                {
                    sqlCommand.Parameters.AddWithValue("@materialName", materialName);
                    sqlCommand.Parameters.AddWithValue("@safetyStock", stockQty);

                    materialID = Convert.ToInt32(sqlCommand.ExecuteScalar());
                }

                using (SqlCommand cmd = new SqlCommand("InsertMaterialInventory", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@MaterialID", materialID);
                    cmd.Parameters.AddWithValue("@StockQty", stockQty);
                    cmd.Parameters.AddWithValue("@LastUpdate", lastupadate);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("재고 등록이 완료되었습니다.");
                }
            }
        }
        public int GetMaterialID(string materialName)
        {

            string dburl = new RegisterForm().connStr;
            int materialID = -1;

            using(SqlConnection conn = new SqlConnection(dburl))
            {

                conn.Open();

                string query = @"SELECT MaterialID FROM Materials WHERE MaterialName = @materialName";


                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@materialName", materialName);

                    materialID = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
           
            return materialID;
        }
        public void MaterialTrsansactionRegister(int materialID,string 
            inout,string qty,DateTime date,string note)
        {
            DialogResult result = MessageBox.Show("등록하시겠습니까?"
                , "경고", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

            if (result == DialogResult.OK) {

            string dburl = new RegisterForm().connStr;

                using (SqlConnection conn = new SqlConnection(dburl))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("InsertMaterialTransaction", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MaterialID", materialID);
                        cmd.Parameters.AddWithValue("@TransactionType", inout);
                        cmd.Parameters.AddWithValue("@Quantity", int.Parse(qty));
                        cmd.Parameters.AddWithValue("@TransactionDate", date);
                        cmd.Parameters.AddWithValue("@Note", note);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("입출고 등록이 완료되었습니다.");
                    }

                }
            }

        }
    }
}
