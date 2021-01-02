using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Used_cars.Business
{
    public class User
    {
        public int u_id{ get;set;}
        public string u_full_name{ get;set;}
        public string u_name{ get;set;}
        public string u_email{ get;set;}
        public string u_contact{ get;set;}
        public string u_nic{ get;set;}
        public int u_type { get; set; }
        private bool is_loged=false;

        public User(int uid)
        {

        }

        public User()
        {
            // TODO: Complete member initialization
        }

        public bool IsLogged()
        {
            return is_loged;
        }


        public bool Login(string uname, string pass)                    // login method
        {
            Data_Connection.DbCon db = new Used_cars.Data_Connection.DbCon();
            string sql = "select * from users where u_pass=md5('" + pass + "') and u_uname='" + uname + "'";
            MySqlDataReader rr = db.DBConnection(sql);
            if (rr != null && rr.HasRows)                               // if login sucess
            {
                rr.Read();
                this.u_name = uname;                                    // set user name
                this.u_id = rr.GetInt16("u_id");                        // set user id
                this.u_type = rr.GetInt16("u_type");
                db.connClose();
                return true;
            }
            else
            {
                db.connClose();
                return false;
            }
        }


        public bool Chng_Pass(string old_p,string new_p)                // change passwoord method
        {
            bool old_mtch = false;
            Data_Connection.DbCon db = new Used_cars.Data_Connection.DbCon();
            string sql = "select * from users where u_pass=md5('" + old_p + "') and u_uname='" + this.u_name + "'";
            MySqlDataReader rr = db.DBConnection(sql);                  
            if (rr != null && rr.HasRows)                               // chk for null or empty result set
                old_mtch = true;                                        // chk for existing password 
                
            db.connClose();

            if (old_mtch)
            {
                db = new Used_cars.Data_Connection.DbCon();               
                sql = "Update users set u_pass=md5('" + new_p + "') where u_id='" + this.u_id + "'";           // change the pass
                rr = db.DBConnection(sql);
                db.connClose();
                return true;
            }
            return false;
        }


        public bool Create_User(string u_full_name,string u_email,string u_contact,string u_nic,string u_uname,string u_pass )
        {
            Data_Connection.DbCon db = new Used_cars.Data_Connection.DbCon();
            string sql = "insert into users(u_full_name, u_email, u_contact, u_nic, u_uname, u_pass) values('"+u_full_name+"','"+u_email+"','"+u_contact+"','"+u_nic+"','"+u_uname+"',md5('"+u_pass+"'))";
            MySqlDataReader rr = db.DBConnection(sql);
            db.connClose();
            return true;
        }
            
    }
}
