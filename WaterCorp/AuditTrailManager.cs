using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WaterCorp
{
    public class AuditTrailManager
    {
        private string connectionString;

        public AuditTrailManager()
        {
            // You can store your connection string in the web.config or another configuration file
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public void LogAuditTrail(string userId, string tableName, long recordId, string action)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO AuditTrail (UserId, TableName, RecordId, Action, ActionDate) VALUES (@UserId, @TableName, @RecordId, @Action, @ActionDate)", connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@TableName", tableName);
                    command.Parameters.AddWithValue("@RecordId", recordId);
                    command.Parameters.AddWithValue("@Action", action);
                    command.Parameters.AddWithValue("@ActionDate", DateTime.Now);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}