using PetPamper.Models;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace PetPamper.Lib.SQL
{
    public static class MSSQL
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        private static SqlConnection sqlCon;
        private static object connectionState;

        public static SqlConnection getSQLConnection()
        {
            if (sqlCon == null)
                sqlCon = new SqlConnection(connectionString);

            if (sqlCon.State != ConnectionState.Open)
                sqlCon.Open();
            
            return sqlCon;
        }

        public static DataTable GetData(string sqlQuery, string[] keys = null, object[] values = null)
        {

            SqlCommand sqlCommand = new SqlCommand(sqlQuery, getSQLConnection());
            sqlCommand.CommandType = CommandType.Text;

            if (keys != null && values != null)
            {
                int maxIndex = Math.Min(keys.Length, values.Length);
                for (int i = 0; i < maxIndex; i++)
                {
                    sqlCommand.Parameters.AddWithValue(keys[i], values[i]);
                }
            }

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            return dataTable;

        }


        public static DataRow GetRow(string sqlQuery, string[] keys = null, object[] values = null)
        {
            var dataTable = GetData(sqlQuery, keys, values);
            if (dataTable == null)
                return null;

            return dataTable.Rows.Count > 0 ? dataTable.Rows[0] : null;
        }

        public static bool Execute(string sqlQuery, string[] keys = null, object[] values = null)
        {

            SqlCommand sqlCommand = new SqlCommand(sqlQuery, getSQLConnection());
            sqlCommand.CommandType = CommandType.Text;

            if (keys != null && values != null)
            {
                int maxIndex = Math.Min(keys.Length, values.Length);
                for (int i = 0; i < maxIndex; i++)
                {
                    sqlCommand.Parameters.AddWithValue(keys[i], values[i]);
                }
            }
            string sql = sqlCommand.CommandText;
            int efftectRow = sqlCommand.ExecuteNonQuery();
            return efftectRow > 0;

        }


        public static T GetItemByQuery<T>(string sqlQuery)
        {
            T obj = default(T);


            SqlCommand sqlCommand = new SqlCommand(sqlQuery, getSQLConnection());
            sqlCommand.CommandType = CommandType.Text;

            var dataReader = sqlCommand.ExecuteReader();
            var loginData = new LoginModel();

            while (dataReader.Read())
            {
                //loginData.Tendangnhap = dataReader.GetValue(0).ToString();
                //loginData.Matkhau = dataReader.GetValue(1).ToString();
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!object.Equals(dataReader[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dataReader[prop.Name], null);
                    }
                }
            }
            
            return obj;

        }
    
    }
}