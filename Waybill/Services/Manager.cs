using System;
using System.Data.SqlClient;

namespace WpfApp2.Services
{
    public class Manager
    {
         /// <summary>
         /// Opens connection to database
         /// </summary>
         /// <returns></returns>
        protected static SqlConnection Open()
        {
           String connectionString = "Integrated Security=SSPI;"
                                          + "Initial Catalog=Formatka;"
                                          + "Data Source=.\\SQLExpress";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}