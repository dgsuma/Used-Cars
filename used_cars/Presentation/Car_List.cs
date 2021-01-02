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
    public partial class Car_List : Form
    {
        //global references for types
        Makes   make;
        Models  model;
        Colours color;
        Status  stt;
        Trans   trns;
        Types   type;

        Business.Car_List cl1 = new Business.Car_List();            // car list business object

        public Car_List()
        {
            InitializeComponent();
        }

        private void Car_List_Load(object sender, EventArgs e)      // on load initialization
        {
            load_base_data();
            fill_grid_data();
        }

        // fill the car list grid with all cars
        public void fill_grid_data()
        {
            
            Business.Car_List cl = new Business.Car_List();     // new car list object
            List<Car> clist = cl.Get_All();     //calling Get_All method from Car_List class inside the Business Layer

            dataGridView1.Rows.Clear();
            for (int i = 0; i < clist.Count;i++ )
            {
                // skips the car if relevent propety is not matched but a option ia selected
                if (cmb_make.Text != make.Get_Value(clist[i].make_id) && cmb_make.SelectedIndex  != -1 )  // chk for makes
                    continue;
                if (cmb_model.Text != model.Get_Value(clist[i].mod_id) && cmb_model.SelectedIndex != -1)  // chk for model
                    continue;
                if (cmb_type.Text != type.Get_Value(clist[i].t_id) && cmb_type.SelectedIndex != -1)       // chk for type
                    continue;
                if (cmb_stt.Text != stt.Get_Value(clist[i].st_id) && cmb_stt.SelectedIndex != -1)         // chk for status
                    continue;

                // add car to grid
                dataGridView1.Rows.Add(clist[i].v_id.ToString(), make.Get_Value(clist[i].make_id), type.Get_Value(clist[i].t_id), model.Get_Value(clist[i].mod_id), color.Get_Value(clist[i].c_id), clist[i].v_year, clist[i].v_milage, clist[i].v_country, clist[i].v_price.ToString(), stt.Get_Value(clist[i].st_id),  Properties.Resources.tick_green_big, Properties.Resources.tick_green_big, Properties.Resources.tick_green_big, Properties.Resources.tick_green_big);
                
             }
        }

        private void load_base_data()
        {
            // creates base info objects
            make = new Makes();
            color = new Colours();
            type = new Types();
            trns = new Trans();
            stt = new Status();
            model = new Models();

            // fill combo boxes
            cmb_make.Items.AddRange(make.Get_List().ToArray()); //uses Get_List method inside D_Types class in Business layer
            cmb_stt.Items.AddRange(stt.Get_List().ToArray());   //uses Get_List method inside D_Types class in Business layer
            cmb_type.Items.AddRange(type.Get_List().ToArray()); //uses Get_List method inside D_Types class in Business layer
            cmb_model.Items.AddRange(model.Get_List().ToArray());   //uses Get_List method inside D_Types class in Business layer
        }

        //on make change
        private void cmb_make_SelectedIndexChanged(object sender, EventArgs e)
        {
            //refil model list(drp down) acording to make
            cmb_model.Items.Clear();
            cmb_model.Text = "";
            model = new Models(make.Get_Id(cmb_make.SelectedIndex));
            cmb_model.Items.AddRange(model.Get_List().ToArray());   //uses Get_List method inside D_Types class in Business layer

            fill_grid_data();                               // fill the grid << with selectin
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)  // on datagrid action button click
        {
            if(e.RowIndex<0)                            // if header : do nothing
                return;

            int v_id=int.Parse((dataGridView1[0, e.RowIndex]).Value.ToString());        // get v_id of the raw

            if (e.ColumnIndex == 10)                        // if edit coloumn
            {

                // loads and show add_car object for edit the item
                Add_cars adc = new Add_cars(v_id);
                adc.MdiParent = this.MdiParent;
                adc.Show();
                this.Hide();
            }

            if(e.ColumnIndex == 11){

                // delete the selected car
                if (MessageBox.Show("Do you want to remove This Vehicle?", "Delete Vehicle ", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    cl1.Set_Delete(v_id);       //calling Set_Delete method from Car_List class inside the Business Layer
                    fill_grid_data();
                }
            }
            if (e.ColumnIndex == 12)
            {
                // reserve the selected car
                if (MessageBox.Show("Do you want to Reserve This Vehicle?", "Reserve Vehicle ", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    cl1.Set_Reservd(v_id);      //calling Set_Reservd method from Car_List class inside the Business Layer
                    fill_grid_data();
                }
              
            }
            if (e.ColumnIndex == 13)
            {
                // mark as Sold the selected car
                if (MessageBox.Show("Do you want to Sold-Out this vehicle?", "Sold Confirmation", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    cl1.Set_Sold(v_id);     //calling Set_Sold method from Car_List class inside the Business Layer
                    fill_grid_data();
                }

            }
            
        }


        // on drop down change, reload the grid with matching cars  :: filtered
        private void cmb_model_SelectedIndexChanged(object sender, EventArgs e)
        {
            fill_grid_data();
        }

        private void cmb_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            fill_grid_data();
        }

        private void cmb_stt_SelectedIndexChanged(object sender, EventArgs e)
        {
            fill_grid_data();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
