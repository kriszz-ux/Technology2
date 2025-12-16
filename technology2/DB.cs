using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace DB
{
    internal class DBConnection
    {
        private MySqlConnection myconnect;

        public DBConnection()
        {
            string connectionString =
                "server=localhost;database=health_center_db2;port=3306;uid=root;pwd=;";

            myconnect = new MySqlConnection(connectionString);
        }

        public MySqlConnection Open()
        {
            if (myconnect.State != ConnectionState.Open)
                myconnect.Open();

            return myconnect;
        }

        public void Close()
        {
            if (myconnect.State == ConnectionState.Open)
                myconnect.Close();
        }
    }
}

namespace Technology_Project
{
    internal class Class2
    {
        DBConnection db = new DBConnection();

        // ADD PATIENT
        public void AddPatient(
    string name,
    string age,
    string gender,
    string contact,
    string diagnosis,
    DateTime consultationDate,
    string address)
        {
            string query = @"INSERT INTO records
    (name, age, gender, contact, diagnosis, consultation_date, address)
    VALUES (@name, @age, @gender, @contact, @diagnosis, @date, @address)";

            using (MySqlCommand cmd = new MySqlCommand(query, db.Open()))
            {
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@age", age);
                cmd.Parameters.AddWithValue("@gender", gender);
                cmd.Parameters.AddWithValue("@contact", contact);
                cmd.Parameters.AddWithValue("@diagnosis", diagnosis);
                cmd.Parameters.AddWithValue("@date", consultationDate);
                cmd.Parameters.AddWithValue("@address", address);

                cmd.ExecuteNonQuery();
            }
            db.Close();   
        }

        // UPDATE PATIENT
        public void UpdatePatient(int id, string name, string age, string gender,
                                   string contact, string diagnosis, System.DateTime consultationDate, string address)
        {
            string query = @"UPDATE records SET
                            name=@name, age=@age, gender=@gender,
                            contact=@contact, diagnosis=@diagnosis,
                            consultation_date=@date, address=@address
                            WHERE patient_id=@id";

            using (MySqlCommand cmd = new MySqlCommand(query, db.Open()))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@age", age);
                cmd.Parameters.AddWithValue("@gender", gender);
                cmd.Parameters.AddWithValue("@contact", contact);
                cmd.Parameters.AddWithValue("@diagnosis", diagnosis);
                cmd.Parameters.AddWithValue("@date", consultationDate);
                cmd.Parameters.AddWithValue("address", address);

                cmd.ExecuteNonQuery();
            }
            db.Close();
        }
    }
}
        
    

 