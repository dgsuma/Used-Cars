using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Used_cars.Presentation
{
    public partial class Login : Form
    {

        public Login()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e) 
        {
            Application.Exit();   // exit on close button click
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(Valid()){
                Business.User user = new Business.User();     // create user object
                if (user.Login(txt_un.Text, txt_up.Text))     // access Login method inside User class in the Business layer<< returns true on sucess login attempt
                {
                    Business.Globals.user = user;             // set the global user object :: for future access
                    this.Hide();                              // hides login form
                    MainF mainf = new MainF(this);            // create the main window

                    mainf.Show();                             // displays the main window
                }
                else   // if login failed
                {
                    this.lbl_er.Text = "Login Failed... ";
                    lbl_er.Visible = true;
                }

            }
        }

        private bool Valid()    // validation for form inputs
        {
            if (txt_un.Text.Trim().Length < 1)    // check user name length
            {
                lbl_er.Text = "User name missing...!";
                lbl_er.Visible = true;
                return false;
            }

            if (txt_up.Text.Trim().Length < 1)     // check user pass length
            {
                lbl_er.Text = "Password missing...!";
                lbl_er.Visible = true;
                return false;
            }

            return true;            //returns true on sucess validation
        }

        private void Login_Shown(object sender, EventArgs e)  // set textbox to empty on form display
        {
            txt_un.Text = "";
            txt_up.Text = "";
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
