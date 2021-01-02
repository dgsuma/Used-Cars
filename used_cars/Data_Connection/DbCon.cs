using System;
using MySql.Data.MySqlClient;
using Used_cars.Properties;

namespace Used_cars.Data_Connection
{
    public class DbCon
    {
        public MySqlConnection connection;
        public MySqlDataReader Reader;
		public MySqlCommand sql;
        public MySqlDataReader DBConnection(string ins_sql)
        {
            try
            {
                string db_con_s = Settings.Default.db_string;       // connection string for db; grabs from app settings
                connection = new MySqlConnection(db_con_s);         // create a new db connection
                connection.Open();                                  // db conn. open
                sql = connection.CreateCommand();
                sql.CommandTimeout = 5000;                          // timeout for single connection 5s
                sql.CommandText = ins_sql;                          // set sql query
                Reader = sql.ExecuteReader();                       // execute the sql query on given db
                return Reader;                                     // returns the db result
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Database Access Failed...");
                return null;
            }
        }
        public void connClose()             // method for db con. close
        {
           
            try
            {
                connection.Close();         // close the db connection
            }
            catch (Exception) { }
        }
    }
}
