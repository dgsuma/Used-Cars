using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Used_cars.Business
{
    class Models:D_Types
    {

        // fill the two list (id, val) using Modoels matching given manufature id
        public Models(int mk_id)
        {
            val.Clear();
            id.Clear();

            Data_Connection.DbCon db = new Used_cars.Data_Connection.DbCon();           
            string sql = "select * from models where make_id="+mk_id.ToString();
            MySqlDataReader rr = db.DBConnection(sql);
            if (rr != null && rr.HasRows)
            {
                while( rr.Read()){
                    val.Add(rr.GetString("mod_name"));                  // fill val list
                    id.Add(rr.GetInt32("mod_id"));                      // fill id list
                }
            }
            db.connClose();
        }

        // fill the two list (id, val) with database values :: lists are derived from parent class - d_types
        public Models()
        {
            val.Clear();                                                // clear current info from lists
            id.Clear();

            Data_Connection.DbCon db = new Used_cars.Data_Connection.DbCon();       
            string sql = "select * from models";
            MySqlDataReader rr = db.DBConnection(sql);
            if (rr != null && rr.HasRows)
            {
                while (rr.Read())
                {
                    val.Add(rr.GetString("mod_name"));                  // fill val list
                    id.Add(rr.GetInt32("mod_id"));                      // fill id list
                }
            }
            db.connClose();
        }

       
    }
}