using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Used_cars.Business
{
    class Types:D_Types
     {

         // fill the two list (id, val) with database values :: lists are derived from parent class - d_types
        public Types()        
        {
            Data_Connection.DbCon db = new Used_cars.Data_Connection.DbCon();
            string sql = "select * from TYPES";
            MySqlDataReader rr = db.DBConnection(sql);
            if (rr != null && rr.HasRows)
            {
                while( rr.Read()){
                    val.Add(rr.GetString("t_name"));                        // add data to list : val( type names)
                    id.Add(rr.GetInt32("t_id"));                            // add data to list : id( index )
                }
            }
            db.connClose();
        }

      
    }
}
