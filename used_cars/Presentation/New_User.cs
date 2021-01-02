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
    public partial class New_User : Form
    {
        Business.User user = Business.Globals.user;
        public New_User()
        {
            InitializeComponent();
        }

        private void New_User_Load(object sender, EventArgs e)
        {
        }

        // method to save new user
        private void btn_save_Click(object sender, EventArgs e)
        {
            if (vlaid())
            {
                Business.User user = new Business.User();               // new user object
                if (user.Create_User(txt_name.Text, txt_em.Text, txt_pno.Text, txt_nic.Text, txt_un.Text, txt_pass.Text))   //calling Create_User method from User class in the Business Layer
                {
                    MessageBox.Show("User Created ....");
                    this.Close();
                }                
            }
        }

        // validation method
        private bool vlaid()
        {
            if (txt_name.Text.Length < 1)
            {
                MessageBox.Show("Name missing...!");
                return false;
            }
            if (txt_un.Text.Length < 1)
            {
                MessageBox.Show("User name missing...!");
                return false;
            }
            if (txt_pass.Text.Length < 1)
            {
                MessageBox.Show("Password missing...!");
                return false;
            }
            if (txt_pass.Text != txt_pass2.Text)
            {
                MessageBox.Show("New & confirmation password dose not match...!");
                return false;
            }

            return true;
        }
    }
}
