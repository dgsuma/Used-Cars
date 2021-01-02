using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Used_cars.Business;

namespace Used_cars.Presentation
{
    public partial class Add_cars : Form
    {
        // global reference
        Makes make;
        Models model;
        Colours color;
        Status stt;
        Trans trns;
        Types type;
        List<Image> piclist = new List<Image>();
        List<Panel> pic_panel = new List<Panel>();
        Custom_txt_box.Number_text txt_price;
        Car carx = new Car();
        Boolean is_update = false;
        int vid;

        public Add_cars()    // construtor for new car
        {
            InitializeComponent();
            Add_Number_Txt_Box();
            Load_Base_Data();
            
        }

        public Add_cars(int v_id)      // constructor for edit car ( car id is passed)
        {
            InitializeComponent();
            Add_Number_Txt_Box();
            vid = v_id;
            is_update = true;
            carx = new Car(v_id);
            Load_Base_Data();
            Set_Values();
            btn_save.Text = "";
            btn_save.Text = "Update";
            label1.Text = "Update View";
            piclist = carx.imgs;                //assigning carx.imgs picture box values to piclist

        }

        private void Set_Values()    // set vaues to ui when a car is passed to edit
        {
            cmb_make.Text = make.Get_Value(carx.make_id);
            cmb_color.Text = color.Get_Value(carx.c_id);
            cmb_model.Text = model.Get_Value(carx.mod_id);
            cmb_trans.Text = trns.Get_Value(carx.tra_id);
            cmb_type.Text = type.Get_Value(carx.t_id);

            txt_contact.Text = carx.v_tp;
            txt_country.Text = carx.v_country;
            txt_em.Text = carx.v_email;
            txt_en_cap.Text = carx.v_eng_cap.ToString();
            txt_milage.Text = carx.v_milage.ToString();
            txt_name.Text = carx.v_owner;
            txt_postcode.Text = carx.v_postcode;
            txt_price.Text = carx.v_price.ToString();
            txt_town.Text = carx.v_owner;
            txt_year.Text = carx.v_year;


            //load images to ui
            for (int i = 0; i < carx.imgs.Count;i++ )
            {
                Panel pa = new Panel();
                pa.Size = new System.Drawing.Size(70, 50);
                pa.BackgroundImage = carx.imgs[i];
                pa.BackgroundImageLayout = ImageLayout.Stretch;

                Button bt = New_Del_Btn();

                pa.Controls.Add(bt);
                pnl_img.Controls.Add(pa); 
            }
            
        }

        private void Add_Cars_Load(object sender, EventArgs e)
        {
        }

        private void Add_Number_Txt_Box()   // add custom text box to ui
        {
            this.txt_price = new Used_cars.Custom_txt_box.Number_text();    //Creating an instance of Number_text_box Custom class

            this.txt_price.BackColor = System.Drawing.Color.White;
            this.txt_price.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_price.Location = new System.Drawing.Point(97, 356);
            this.txt_price.Name = "txt_price";
            this.txt_price.Size = new System.Drawing.Size(203, 26);
            this.txt_price.TabIndex = 50;

            this.panel1.Controls.Add(this.txt_price);
        }

        private void Load_Base_Data()
        {
            //initialize basic objects
            make = new Makes();
            color = new Colours();
            type = new Types();
            trns = new Trans();
            stt = new Status();
            model = new Models();

            // fill drop downs
            cmb_make.Items.AddRange(make.Get_List().ToArray());     //uses Get_List method inside D_Types class in Business layer
            cmb_color.Items.AddRange(color.Get_List().ToArray());   //uses Get_List method inside D_Types class in Business layer
            cmb_trans.Items.AddRange(trns.Get_List().ToArray());    //uses Get_List method inside D_Types class in Business layer
            cmb_type.Items.AddRange(type.Get_List().ToArray());     //uses Get_List method inside D_Types class in Business layer
            cmb_model.Items.AddRange(model.Get_List().ToArray());   //uses Get_List method inside D_Types class in Business layer
        }

        private void SaveButton_Click(object sender, EventArgs e)    // save button click
        {

            if (Valid())    // if valid
               {
                // set car object info (for save or update process)
                carx.make_id = make.Get_Id(cmb_make.SelectedIndex);
                carx.c_id = color.Get_Id(cmb_color.SelectedIndex);
                carx.tra_id = trns.Get_Id(cmb_trans.SelectedIndex);
                carx.t_id = type.Get_Id(cmb_type.SelectedIndex);
                carx.mod_id = model.Get_Id(cmb_model.SelectedIndex);
                carx.st_id = 1;

                carx.v_year = txt_year.Text;
                carx.v_milage =double.Parse(txt_milage.Text);
                carx.v_eng_cap = double.Parse(txt_en_cap.Text);
                carx.v_country = txt_country.Text;
                carx.v_town = txt_town.Text;
                carx.v_postcode = txt_postcode.Text;
                carx.v_owner = txt_name.Text;
                carx.v_tp = txt_contact.Text;
                carx.v_email = txt_em.Text;
                carx.v_price = double.Parse(txt_price.Text);
                carx.img_count = piclist.Count;


                carx.imgs = Get_Pics();   // get images to list
                Save_Values();            // save method call
            }
        }

        private List<Image> Get_Pics()   // grab pics from the interface
        {
            List<Image> img = new List<Image>();

            foreach (Control pnl in pnl_img.Controls) // for all images
            {
                img.Add(((Panel)pnl).BackgroundImage);  // add to list
            }
            return img;                 // returns all pics in a Image list
        }


        public void Save_Values()                   // on save button click        
        {
            if (is_update==true)
            {
                // if an upadate request
                carx.Update_Car(vid);           //call Update_Car method in Car class in Business layer
                MessageBox.Show("vehicle Updated succesfully ... ");
                this.Close();
            }else
            {
                // if new car save
                carx.Add_Car();                 //call Add_Car method in Car class in Business layer
                MessageBox.Show("vehicle Saved succesfully ... ");
                this.Close();
            }
        }

        // input controll validation 
        private bool Valid()
        {

                if (cmb_make.SelectedIndex == -1)
                {
                    MessageBox.Show("Select Vehicle Make !");
                    return false;
                }

                if (cmb_type.SelectedIndex == -1)
                {
                    MessageBox.Show("Select Vehicle Type !");
                    return false;
                }

                if (cmb_model.SelectedIndex == -1)
                {
                    MessageBox.Show("Select Vehicle Model !");
                    return false;
                }

                if (cmb_color.SelectedIndex == -1)
                {
                    MessageBox.Show("Select Vehicle Color !");
                    return false;
                }
              
                if (cmb_trans.SelectedIndex == -1)
                {
                    MessageBox.Show("Select Vehicle Transmission !");
                    return false;
                }
                
                try
                { 
                    int.Parse(txt_year.Text); Double.Parse(txt_milage.Text); Double.Parse(txt_price.Text); 
                }
                catch (Exception)
                {
                        MessageBox.Show("Pleace Check Numaric Valuies !");
                        return false;
                }

           return true;
        }

        //on make change >> change the model list
        private void Cmb_Make_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmb_model.Items.Clear();                                    // clear current list
            cmb_model.Text = "";
            model = new Models(make.Get_Id(cmb_make.SelectedIndex));
            cmb_model.Items.AddRange(model.Get_List().ToArray());       // add new set
        }

        private void Btn_Add_Img_Click(object sender, EventArgs e)      //  on add img button click
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)        // if a file is selected
            {
                Panel pa = new Panel();                                 //new panel
                pa.Size = new System.Drawing.Size(70, 50);
                pa.BackgroundImage = new Bitmap(openFileDialog1.FileName);      // set image as bg_image
                pa.BackgroundImageLayout = ImageLayout.Stretch;
                Button bt = New_Del_Btn();                                      // add del button
                pa.Controls.Add(bt);
                pnl_img.Controls.Add(pa);                                       // add the img panel to main img holder(flow panel)
            }
        }

        // create a delete button for each img dynamically
        private Button New_Del_Btn()
        {
            Button bt = new Button();
            bt.Text = "x";
            bt.BackColor = System.Drawing.Color.Chocolate;
            bt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            bt.Location = new Point(2, 2);
            bt.Size = new System.Drawing.Size(20, 20);
            bt.Click += Bt_Click;
            return bt;
        }
        
        // delete image
        void Bt_Click(object sender, EventArgs e)
        {
            pnl_img.Controls.Remove(((Button)sender).Parent);  
        }

    }
}
