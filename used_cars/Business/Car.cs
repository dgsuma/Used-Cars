using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Used_cars.Properties;

namespace Used_cars.Business
{
    class Car
    {
        // public propeties of vehicle object

        public int v_id { get; set; }
        public int id { get; set; } 
        public int t_id { get; set; }
        public int make_id { get; set; }
        public int mod_id { get; set; }
        public int c_id { get; set; }
        public int tra_id { get; set; }
        public int st_id { get; set; }
        public string  v_year { get; set; }
        public double v_milage { get; set; }
        public double v_eng_cap { get; set; }
        public string v_country { get; set; }
        public string v_town { get; set; }
        public string v_postcode { get; set; } 
        public string v_owner { get; set; } 
        public string v_tp { get; set; } 
        public string v_email { get; set; }
        public double v_price { get; set; }
        public int img_count { get; set;}
        public List<Image> imgs { get; set; }

        public Car()
        {

        }

        public Car(int vid) // contructor >> initialise(loads) a car
        {
            Data_Connection.DbCon db = new Used_cars.Data_Connection.DbCon();
            string sql = "Select v.*,m.make_id from vehicle v,models m where v.mod_id =m.mod_id and v_id ='" + vid + "'";
            MySqlDataReader rr = db.DBConnection(sql);
            if (rr != null && rr.HasRows)       //
            {
                rr.Read();
                v_id = rr.GetInt32("v_id");
                t_id = rr.GetInt32("t_id");
                make_id = rr.GetInt32("make_id");
                mod_id = rr.GetInt32("mod_id");
                c_id = rr.GetInt32("c_id");
                tra_id = rr.GetInt32("tra_id");
                st_id = rr.GetInt32("st_id");
                v_year = rr.GetString("v_year");
                v_milage = rr.GetDouble("v_milage");
                v_eng_cap = rr.GetDouble("v_eng_cap");
                v_country = rr.GetString("v_country");
                v_town = rr.GetString("v_town");
                v_postcode = rr.GetString("v_postcode");
                v_tp = rr.GetString("v_tp");
                v_email = rr.GetString("v_email");
                v_price = rr.GetDouble("v_price");
                v_owner = rr.GetString("v_owner");
                img_count = rr.GetInt32("v_no_img");
                imgs =new List<Image>();
                Load_Img();
            }
            db.connClose();
        }

        public void Add_Car()  // insert a new car entry
        {
            Data_Connection.DbCon db = new Used_cars.Data_Connection.DbCon();
            string sql = "insert into vehicle (t_id, mod_id, c_id, tra_id, st_id, v_year, v_milage, v_eng_cap, v_country, v_town, v_postcode, v_owner, v_tp, v_email, v_price,v_no_img)" +
            " values("+t_id+","+mod_id+","+c_id+","+tra_id+","+st_id+",'"+v_year+"',"+v_milage+","+v_eng_cap+",'"+v_country+"','"+v_town+"','"+v_postcode+"','"+v_owner+"','"+v_tp+"','"+v_email+"','"+v_price+"','"+imgs.Count+"');";
            MySqlDataReader rr = db.DBConnection(sql);
            this.id= int.Parse(db.sql.LastInsertedId.ToString());  // gets the inserted cars ref. no  << primery key
            this.Add_Img();                                        // call to add image method
            db.connClose();
        }

        public void Update_Car(int v_id)                           // updating existing car info                    
        {
            Data_Connection.DbCon db = new Used_cars.Data_Connection.DbCon();
            string sql = "update vehicle set t_id=" + t_id + ", mod_id=" + mod_id + ", c_id=" + c_id + ", tra_id=" + tra_id + ", st_id=" + st_id + ", v_year='" + v_year + "', v_milage=" + v_milage + ", v_eng_cap=" + v_eng_cap + ", v_country='" + v_country + "', v_town='" + v_town + "', v_postcode='" + v_postcode + "', v_owner='" + v_owner + "', v_tp='" + v_tp + "', v_email='" + v_email + "', v_price=" + v_price + ",v_no_img="+imgs.Count.ToString()+" where v_id='" + v_id + "'  ";
            MySqlDataReader rr = db.DBConnection(sql);
            this.id = v_id;
            Del_Img();                                             // call >> deletes existing images of the car
            Add_Img();                                             // save final image set
            db.connClose();
        }

        public void Del_Img()     // delete imgage method                                  
        {
            string path = Settings.Default.img_path + id;            // get img folder path from app settings

            for (int i = 0; i < img_count;i++ )                      // iterate for all images
            {
                string xpath = path + "_" + i + ".jpg";              // crerate img path
                 try
                 {
                    File.Delete(xpath);                              // delete the current img
                 }
                catch (Exception)
                {
                    MessageBox.Show("Image deletion failed..  No acces to image location");
                }
             }
        }

        public void Add_Img()       //add image method
        {
            string path = Settings.Default.img_path + id;            // path for saving veh. images
            int cv=0;
            foreach (Image im in imgs)                                     //iterate through img list
            {
                    string xpath=path+"_"+cv+".jpg";                            // set file name 
                    try
                {
                    im.Save(xpath, System.Drawing.Imaging.ImageFormat.Jpeg);     // save the image
                    
                }catch(Exception){

                    MessageBox.Show("Failed to save image..  No acces to image location");
                }   
                cv++;
            }
        }

        public void Load_Img()          // method for get saved images
        {   try{
            for (int i = 0; i < img_count;i++ )   
            {
                Image im = Image.FromStream(new MemoryStream(File.ReadAllBytes(Settings.Default.img_path  + v_id + "_" + i + ".jpg")));    // read image from saved location
                imgs.Add(im);           // add the image to the list
            }
        }catch(Exception){

                    MessageBox.Show("Failed to load image..  No acces to image location");
                }   
        }
    }
}