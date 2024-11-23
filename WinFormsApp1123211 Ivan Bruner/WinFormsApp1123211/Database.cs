using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace WinFormsApp1123211
{
    public class Database
    {
        private readonly string connectionString;

       
        public Database(string connectionString = "server=localhost;user=root;database=BendrijuPaslaugos;password=1997")
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Connection string cannot be null or empty.");

            this.connectionString = connectionString;
        }

      
        public DataTable ExecuteQuery(string query, MySqlParameter[]? parameters = null)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (parameters != null)
                            cmd.Parameters.AddRange(parameters);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error executing query: {ex.Message}", ex);
            }
        }

       
        public int ExecuteNonQuery(string query, MySqlParameter[]? parameters = null)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (parameters != null)
                            cmd.Parameters.AddRange(parameters);

                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error executing non-query: {ex.Message}", ex);
            }
        }

        
        public object ExecuteScalar(string query, MySqlParameter[]? parameters = null)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (parameters != null)
                            cmd.Parameters.AddRange(parameters);

                        return cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error executing scalar query: {ex.Message}", ex);
            }
        }
    }
}
