using MySql.Data.MySqlClient;

namespace Technology_Project
{
    public class DBConnection
    {
        private MySqlConnection conn;

        public DBConnection()
        {
            conn = new MySqlConnection(
                "server=localhost;database=health_center_db2;uid=root;pwd=;"
            );
        }

        public MySqlConnection Open()
        {
            if (conn.State == System.Data.ConnectionState.Closed)
                conn.Open();
            return conn;
        }

        public void Close()
        {
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
        }
    }
}
