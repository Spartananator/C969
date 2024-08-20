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
using System.Linq.Expressions;
using System.Reflection;
using System.Transactions;
using Google.Protobuf.WellKnownTypes;
using Scheduling_Software;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities;

namespace C969Project
{
    public partial class MeetingForm : Form
    {
        DayOfWeek[] InvalidDay = { DayOfWeek.Saturday, DayOfWeek.Sunday };
        List<meeting> meetingsList;
        List<Customer> customersList;
        BindingSource customers = new BindingSource();
        Mainscheduler scheduler = new Mainscheduler();
        user curUser;
        Schedule sched;
        meeting updateMeet;
        public MeetingForm(meeting meet, List<meeting>meets, List<Customer> custs, user User, Schedule s)
        {
            meetingsList = meets;
            customersList = custs;
            InitializeComponent();
            startHour.Text = "09";
            startMinute.Text = "00";
            endHour.Text = "09";
            endMinute.Text = "00";
            startDate.MinDate = DateTime.Now;
            endDate.MinDate = DateTime.Now;
            curUser = User;
            sched = s;
            configureGrid();
            if (meet != null)
            {
                updateMeet = meet;
                fillFields(meet);
            }
        }

        private void fillFields(meeting meet)
        {
            titleBox.Text = meet.Title;
            locationBox.Text = meet.Location;
            contactBox.Text = meet.Contact;
            typeBox.Text = meet.Type;
            urlBox.Text = meet.Url;
            descriptionBox.Text = meet.Description;
            searchBox.Text = meet.Customer;
        }
        private void submitButton_Click_1(object sender, EventArgs e)
        {

            List<TextBox> boxes = new List<TextBox> { titleBox, typeBox };
            List<string> fields = new List<string>() { "Meeting Title", "Meeting Type"};
            bool required = true;
            string err = "The following fields require input:\n";
            foreach (TextBox box in boxes)
            {
                if (box.Text == "")
                {
                    required = false;
                    err += $" {fields[boxes.IndexOf(box)]}\n";
                }
            }
            if (required == false)
            {

                MessageBox.Show(err);

            }
            else 
            {DateTime start;
            DateTime end;
            string starttime = $"{startDate.Value.Year:D4}-{startDate.Value.Month:D2}-{startDate.Value.Day:D2} {startHour.Text:D2}:{startMinute.Text:D2}:00";
            string endtime = $"{endDate.Value.Year:D4}-{endDate.Value.Month:D2}-{endDate.Value.Day:D2} {endHour.Text:D2}:{endMinute.Text:D2}:00";
            
            // Parse the string into a DateTime object

            bool success = DateTime.TryParseExact(starttime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out start);

                if (success)
                {


                    success = DateTime.TryParseExact(endtime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out end);

                    if (success)
                    {
                        DateTime timestamp = DateTime.UtcNow;
                        DataGridViewRow row = new DataGridViewRow();
                        Customer selectedcus = (row = this.customerGridShort.CurrentRow).DataBoundItem as Customer;
                        int cusid = selectedcus.CustomerID;
                        int userid = curUser.userID;
                        string title = titleBox.Text;
                        string desc = descriptionBox.Text;
                        string loc = locationBox.Text;
                        string cont = contactBox.Text;
                        string type = typeBox.Text;
                        string url = urlBox.Text;
                        string startd = start.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss");
                        string endd = end.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss");
                        string create = timestamp.ToString("yyyy-MM-dd HH:mm:ss");
                        string createby = curUser.userName;
                        string update = timestamp.ToString("yyyy-MM-dd HH:mm:ss");
                        string updateby = curUser.userName;




                        string insertMeeting = @"
                            insert into appointment (customerId, userId, title, description, location, contact, type, url, start, end, createDate, createdBy, lastUpdate, lastUpdateBy)
                            values (@cusid, @userid, @title, @desc, @loc, @cont, @type, @url, @start, @end, @create, @createby, @update, @updateby)
                        ";
                        string updateMeeting = @"
                            update appointment set customerId = @cusid, userId = @userid, title = @title, 
                    description = @desc, location = @loc, contact = @cont, type = @type, url = @url, start = @start,
                    end = @end, lastUpdate = @update, lastUpdateBy = @updateby where appointmentId = @appid;";





                        MySqlConnection conn = scheduler.getdatabase();
                        MySqlCommand comm;
                        if (updateMeet == null)
                        {
                            comm = scheduler.createQuery(conn, insertMeeting);

                            comm.Parameters.AddWithValue("@cusid", cusid);
                            comm.Parameters.AddWithValue("@userid", userid);
                            comm.Parameters.AddWithValue("@title", title);
                            comm.Parameters.AddWithValue("@desc", desc);
                            comm.Parameters.AddWithValue("@loc", loc);
                            comm.Parameters.AddWithValue("@cont", cont);
                            comm.Parameters.AddWithValue("type", type);
                            comm.Parameters.AddWithValue("@url", url);
                            comm.Parameters.AddWithValue("@start", startd);
                            comm.Parameters.AddWithValue("@end", endd);
                            comm.Parameters.AddWithValue("@create", create);
                            comm.Parameters.AddWithValue("@createby", createby);
                            comm.Parameters.AddWithValue("@update", update);
                            comm.Parameters.AddWithValue("@updateby", updateby);
                        }
                        else
                        {
                            comm = scheduler.createQuery(conn, updateMeeting);

                            comm.Parameters.AddWithValue("@cusid", cusid);
                            comm.Parameters.AddWithValue("@userid", userid);
                            comm.Parameters.AddWithValue("@title", title);
                            comm.Parameters.AddWithValue("@desc", desc);
                            comm.Parameters.AddWithValue("@loc", loc);
                            comm.Parameters.AddWithValue("@cont", cont);
                            comm.Parameters.AddWithValue("type", type);
                            comm.Parameters.AddWithValue("@url", url);
                            comm.Parameters.AddWithValue("@start", startd);
                            comm.Parameters.AddWithValue("@end", endd);

                            comm.Parameters.AddWithValue("@update", update);
                            comm.Parameters.AddWithValue("@updateby", updateby);
                            comm.Parameters.AddWithValue("@appid", updateMeet.MeetingID);
                        }



                        bool secure = true;
                        foreach (meeting meet in meetingsList)
                        {
                            if (updateMeet != null)
                            { if (meet.MeetingID == updateMeet.MeetingID) { continue; } }
                            if (start < meet.Start || start > meet.End)
                            {
                                if (end < meet.Start || end > meet.End)
                                {
                                    if (meet.Start > start && meet.Start < end)
                                    {
                                        secure = false;
                                        MessageBox.Show($"Unable to schedule this meeting at the same time as the meeting with {meet.Customer} \n This meeting is from {meet.Start} to {meet.End}");
                                    }
                                    else { if (end == start) { secure = false; MessageBox.Show("Cannot have a meeting with the same start and end time."); } }

                                }
                                else
                                {
                                    secure = false;
                                    MessageBox.Show($"Unable to schedule this meeting at the same time as the meeting with {meet.Customer} \n This meeting is from {meet.Start} to {meet.End}");
                                }
                            }
                            else
                            {
                                secure = false;
                                MessageBox.Show($"Unable to schedule this meeting at the same time as the meeting with {meet.Customer} \n This meeting is from {meet.Start} to {meet.End}");
                            }

                        }

                        {
                            try
                            {
                                conn.Open();
                            }
                            catch { MessageBox.Show("Error connecting to database"); }
                            if (secure == true)
                            {
                                comm.ExecuteNonQuery();



                                scheduler.InitializeDatabase(sched);
                                this.Close();
                            }
                            conn.Close();
                        }
                        //MessageBox.Show(start.ToString() + end.ToString());

                    }
                    else
                    {
                        MessageBox.Show("Please confirm end date and time is set.");
                    }
                }
                else
                {
                    MessageBox.Show("Please confirm start date and time is set.");
                }
            }
        }

        private void startDate_ValueChanged(object sender, EventArgs e)
        {
            bool valid = dateIsValid(startDate);
            if (valid)
            {
                endDate.MinDate = startDate.Value;
            }
            else
            {
                if ( DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                {
                    startDate.Value.AddDays(1);
                }else if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
                {
                    startDate.Value.AddDays(2);
                }else
                {
                    startDate.Value = DateTime.Now;
                }
                
                MessageBox.Show("Scheduling is only possible Monday-Friday");
            }
            
            
        }
        private bool dateIsValid (DateTimePicker picker)
        {
            foreach (DayOfWeek Day in InvalidDay) {
                if (picker.Value.DayOfWeek == Day)
                {
                    return false;
                }
            }
            return true;

        }

        private void configureGrid()
        {
            customerGridShort.AutoGenerateColumns = false;
            customerGridShort.RowHeadersVisible = false;
            customerGridShort.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            
            var column = new DataGridViewTextBoxColumn
            {
                HeaderText = "Customer Name",
                DataPropertyName = "CustomerName",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

            };
            
            var column2 = new DataGridViewTextBoxColumn
            {
                HeaderText = "Customer Zip",
                DataPropertyName = "Zipcode",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

            };
            customerGridShort.Columns.Add(column);
            customerGridShort.Columns.Add(column2);
            customers.DataSource = customersList;
            customerGridShort.DataSource = customers;

        }

        private void endDate_ValueChanged(object sender, EventArgs e)
        {
            bool valid = dateIsValid(endDate);
            if (valid)
            {
                
            }
            else
            {
                endDate.Value = startDate.Value;
                MessageBox.Show("Scheduling is only possible Monday-Friday");
            }
        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int zip = int.Parse(searchBox.Text);

                var filteredList = customersList.Where(p => p.Zipcode == zip).ToList();
                customers.DataSource = filteredList;

            }
            catch
            {
                string name = searchBox.Text;

                var filteredList = customersList.Where(p => p.CustomerName.ToLower().Contains(name.ToLower()) ).ToList();
                customers.DataSource = filteredList;

            }
            if (searchBox.Text == "")
            {
                customers.DataSource = customersList;
            }
        }

        private void endHour_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(endHour.Text == "09")
            {
                endMinute.Items.Clear();
                List<string> minuteOptionsSmall = new List<string>() {"15", "30", "45" };
                foreach (string option in minuteOptionsSmall)
                {
                    endMinute.Items.Add(option);
                }
            }
            else if(endHour.Text == "17")
            {
                endMinute.Items.Clear();
                List<string> minuteOptionsSmall = new List<string>() { "00"};
                foreach (string option in minuteOptionsSmall)
                {
                    endMinute.Items.Add(option);
                }
            }else
            {
                endMinute.Items.Clear();
                List<string> minuteOptionsSmall = new List<string>() {"00", "15", "30", "45" };
                foreach (string option in minuteOptionsSmall)
                {
                    endMinute.Items.Add(option);
                }
               
            }
        }
    }
}
