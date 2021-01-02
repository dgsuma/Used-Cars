using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Used_cars.Business
{
    class Car_List
    {

        private List<Car> clist = new List<Car>();           // car list to store all cars

        public Car_List()
        {
            clist.Clear();                                  // emty the car list

            // get cars from local db   : db operations
            Data_Connection.DbCon db = new Used_cars.Data_Connection.DbCon();
            string sql = "Select v.*,m.make_id from vehicle v,models m where v.mod_id =m.mod_id and v.is_del=0 ";
            MySqlDataReader rr = db.DBConnection(sql);
            if (rr != null && rr.HasRows)                   //  if db returns some data
            {
                while(rr.Read()){                           // for each car in db 

                    // create a new car obect and populate info in to car object
                    Car carx = new Car();

                    carx.v_id = rr.GetInt32("v_id");
                    carx.t_id = rr.GetInt32("t_id");
                    carx.make_id = rr.GetInt32("make_id");
                    carx.mod_id = rr.GetInt32("mod_id");
                    carx.c_id = rr.GetInt32("c_id");
                    carx.tra_id = rr.GetInt32("tra_id");
                    carx.st_id = rr.GetInt32("st_id");
                    carx.v_year = rr.GetString("v_year");
                    carx.v_milage = rr.GetDouble("v_milage");
                    carx.v_eng_cap = rr.GetDouble("v_eng_cap");
                    carx.v_country = rr.GetString("v_country");
                    carx.v_town = rr.GetString("v_town");
                    carx.v_postcode = rr.GetString("v_postcode");
                    carx.v_tp = rr.GetString("v_tp");
                    carx.v_email = rr.GetString("v_email");
                    carx.v_price = rr.GetDouble("v_price");
                    carx.v_owner = rr.GetString("v_owner");

                    clist.Add(carx);                            // add car oject to the list
                }
            }
            db.connClose();
        }

        public List<Car> Get_All(){                             // method for return Car List :: all cars

            return clist;
        }

        public void Set_Delete(int v_id)                        // method to delete a single car
        {
            Data_Connection.DbCon db = new Used_cars.Data_Connection.DbCon();       
            string sql = "update vehicle set is_del=1 where v_id='"+v_id+"'";       // set is_del propety to 1 :: (1 = deleted)
            MySqlDataReader rr = db.DBConnection(sql);
            MessageBox.Show("Vehicle Deleted !");
            db.connClose();
        }


        public void Set_Reservd(int v_id)                       // method to reserve a single car
        {
            Data_Connection.DbCon db = new Used_cars.Data_Connection.DbCon();
            string sql = "update vehicle set st_id=2 where v_id='" + v_id + "'";        // set v_stt propety to 2 :: (2 = reserved)
            MySqlDataReader rr = db.DBConnection(sql);
            MessageBox.Show("Car is marked as 'Reserved' !");
            db.connClose();
        }


        public void Set_Sold(int v_id)                          // method to sold a single car
        {
            Data_Connection.DbCon db = new Used_cars.Data_Connection.DbCon();
            string sql = "update vehicle set st_id=3 where v_id='" + v_id + "'";       // set v_stt propety to 3 :: (3 = Sold)
            MySqlDataReader rr = db.DBConnection(sql);
            MessageBox.Show("Vehicle Sold !");
            db.connClose();
        }


    }
}
