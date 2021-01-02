using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Used_cars.Presentation
{
    public partial class Change_Password : Form
    {
        public Change_Password()
        {
            InitializeComponent();
        }

        private void Change_Password_Load(object sender, EventArgs e)
        {
            string txt="Change Password of the user   :   "+Used_cars.Business.Globals.user.u_full_name + "  ["+Used_cars.Business.Globals.user.u_name+"]";
            lbl_note.Text = txt;
        }

        // password change method
        private void button1_Click(object sender, EventArgs e)      
        {
            if (valid())
                if (Business.Globals.user.Chng_Pass(txt_cp.Text, txt_new1.Text))  //calling Chng_Pass method from User class in the Business Layer
                {
                    MessageBox.Show("Password changed");
                    this.Close();
                }
                else
                    MessageBox.Show("Password change Failed");

        }

        // validate input controls
        private bool valid()
        {
            if (txt_cp.Text.Length < 1)
            {
               MessageBox.Show("Current Password missing...!");
                return false;
            }
            if (txt_new1.Text.Length < 1)
            {
                MessageBox.Show("New Password missing...!");
                return false;
            }
            if (txt_new1.Text!=txt_new2.Text)
            {
                MessageBox.Show("New & confirmation password dose not match...!");
                return false;
            }

            return true;
        }
    }
}
