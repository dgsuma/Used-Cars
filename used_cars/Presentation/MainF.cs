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
    public partial class MainF : Form
    {
        
        public MainF(Login log_form)
        {
            InitializeComponent();
            Globals.l_form = log_form;       // reference of login form  :: for future use
        }

        private void addNewCarToolStripMenuItem_Click(object sender, EventArgs e)   //Add New Car form call
        {
            Add_cars addcar = new Add_cars();
            addcar.MdiParent = this;
            addcar.Show();                  //showing Add New Car form
        }

        private void viewCarListToolStripMenuItem_Click(object sender, EventArgs e)  //View Car List form call
        {
            Car_List clist = new Car_List();
            clist.MdiParent = this;
            clist.Show();                   //showing View Car List form
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)            //Exit menu form call
        {
            if(MessageBox.Show("Are you sure toExit the Application ?","Application exit..",MessageBoxButtons.YesNo,MessageBoxIcon.Question)== DialogResult.Yes)
            Application.Exit();     //exit application
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)      // Add New User form call
        {
            if (Globals.user.u_type == 1)           // chk current user type for admin   (admin :1)
            {
                // display add new user :: admin only
                New_User newuser = new New_User();
                newuser.MdiParent = this;
                newuser.Show();                 //showing Add New User form
            }
            else
            {
                MessageBox.Show("Please log-in as admin to used this feature ..");
            }
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)  // Change Password form call  (for currnt user)
        {
            Change_Password pass = new Change_Password();
            pass.MdiParent = this;
            pass.Show();            //showing Change Password form
        }

        private void main_Load(object sender, EventArgs e)
        {

        }

        private void mainF_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Globals.user != null)           // if user is logged out
                Application.Exit();             // exit on form close
        }

        private void logOffToolStripMenuItem_Click(object sender, EventArgs e)      // Log-Off form call
        {
            log_off();      
        }

        private void log_off()  // log off method
        {
            Globals.user = null;            // clear curent user object
            Globals.l_form.Show();          // showing Login form
            this.Close();
        }

    }
}
