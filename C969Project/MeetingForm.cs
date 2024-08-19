using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C969Project
{
    public partial class MeetingForm : Form
    {
        public MeetingForm()
        {
            InitializeComponent();
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            
            //meeting meet = new meeting(
                
                
                
                //);
        }

        private void endHour_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void endMinute_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void startHour_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void startMinute_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void submitButton_Click_1(object sender, EventArgs e)
        {
            DateTime start;
            DateTime end;
            string starttime = startDate.Value.Year +"-"+startDate.Value.Month +"-"+startDate.Value.Day + " " + startHour.Text + ":" + startMinute.Text + ":00";
            start = DateTime.ParseExact(starttime, "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture);
            MessageBox.Show( start.ToString());
        }
    }
}
