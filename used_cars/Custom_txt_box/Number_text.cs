// custom texbox component

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Used_cars.Custom_txt_box
{
    class Number_text : System.Windows.Forms.TextBox   // extend standard text box class
    {
        public Number_text()
        {
            this.TextChanged += number_text_box_TextChanged;                // on change text
            this.KeyPress += number_text_box_KeyPress;                      // on key press
        }

        void number_text_box_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e) // key press response
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')  // if a valid input (num or '.')
                {
                    e.Handled = true;                                       // if not valid, handles the key; no furthrer processing for the key 
                }

                if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)   // if more than two period '.'
                {
                    e.Handled = true;                                       // if not valid, handles the key; no furthrer processing for the key 
                }
        }

        void number_text_box_TextChanged(object sender, EventArgs e)
        {
            colour_me();                                                    // on txt change call colour method
        }

        // colour method
        private void colour_me()
        {
            double val = 0;
            double.TryParse(this.Text, out val);
            if (val > 10000)                                               // if val > 10,000
                this.ForeColor = System.Drawing.Color.Red;                 // makes text into red
            else
                this.ForeColor = System.Drawing.Color.Black;               // if not ; text color set to black
        }
    }
}
