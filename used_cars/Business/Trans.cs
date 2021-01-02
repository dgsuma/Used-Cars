using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Used_cars.Business
{
    class Trans:D_Types  
    {
        public Trans()    
        {
            // fill the two list (id, val) with database values :: lists are derived from parent class - d_types
            Data_Connection.DbCon db = new Used_cars.Data_Connection.DbCon();
            string sql = "select * from trans";
            MySqlDataReader rr = db.DBConnection(sql);
            if (rr != null && rr.HasRows)
            {
                while( rr.Read()){
                    val.Add(rr.GetString("tr_name"));                       // fill val list
                    id.Add(rr.GetInt32("tr_id"));                           // fill id list
                }
            }
            db.connClose();
        }

       
    }
}
