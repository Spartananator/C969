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
        List<string> hours = new List<string> { "09", "10", "11", "12", "13", "14", "15", "16", "17" };
        List<string> minutes = new List<string> { "00", "15", "30","45"};

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
            
            startDate.MinDate = DateTime.Now.Date;
            endDate.MinDate = DateTime.Now.Date;
            curUser = User;
            sched = s;
            configureGrid();
            if (meet != null)
            {
                updateMeet = meet;
                fillFields(meet);
            }
            else
            {
                endHour.Text = "09";
                endMinute.Text = "15";
                startHour.Text = "09";
                startMinute.Text = "00";
                
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
            startDate.Value = meet.Start; 
            endDate.Value = meet.End;
            endMinute.Text = meet.End.ToString("mm");
            endHour.Text = meet.End.ToString("HH");                       
            startHour.Text = meet.Start.ToString("HH");            
            startMinute.Text = meet.Start.ToString("mm");
            checkEndBox();
            checkStartBox();
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
                        int cusid = selectedcus.getKey();
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
                            comm.Parameters.AddWithValue("@appid", updateMeet.getKey());
                        }



                        bool secure = true;
                        foreach (meeting meet in meetingsList)
                        {
                            if (updateMeet != null)
                            { if (meet.getKey() == updateMeet.getKey()) { continue; } }
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
                var zip = searchBox.Text;

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



        private void checkEndBox()
        {
            if (endHour.Text == startHour.Text && startMinute.Text == "45")
            {
                
                
                endMinute.Items.Clear();
                foreach (string min in minutes)
                {
                    endHour.Items.Add(min);
                }
                
                endMinute.Text = minutes[0];
                
            }
            else if (endHour.Text == startHour.Text)
            {

                var holder = endMinute.Text;
                var sorted = minutes.Where(p => int.Parse(p) > int.Parse(startMinute.Text)).ToList();
                endMinute.Items.Clear();
                foreach (string min in sorted)
                {
                    endMinute.Items.Add(min);
                }
                if (int.Parse(holder) < int.Parse(startMinute.Text))
                {
                    endMinute.Text = sorted[0];
                }
                else
                {
                    endMinute.Text = holder;
                }
            }
            else if (endHour.Text == "17")
            {
                endMinute.Items.Clear();
                endMinute.Items.Add("00");
                endMinute.Text = "00";
            }
            else
            {
                endMinute.Items.Clear();
                foreach (string min in minutes)
                {
                    endMinute.Items.Add(min);
                }
                endMinute.Text = minutes[0];
            }
        }
        private void checkStartBox()
        {

            if(startMinute.Text == "45")
            {
                var holder = endHour.Text;
                var sorted = hours.Where(p => int.Parse(p) > int.Parse(startHour.Text)).ToList();
                endHour.Items.Clear();
                foreach (string hour in sorted)
                {
                    endHour.Items.Add(hour);
                }
                if(int.Parse(holder) < int.Parse(sorted[0]))
                {
                    endHour.Text = sorted[0];
                }
                else
                {
                    endHour.Text = holder;
                }
            }
            else
            { 
                
                var holder = endHour.Text;
                var sorted = hours.Where(p => int.Parse(p) >= int.Parse(startHour.Text)).ToList();
                endHour.Items.Clear();
                foreach (string hour in sorted)
                {
                    endHour.Items.Add(hour);
                }
                if (int.Parse(holder) <= int.Parse(sorted[0]))
                {
                    endHour.Text = sorted[0];
                }
                else
                {
                    endHour.Text = holder;
                }

            }
        }

        private void startHour_SelectionChangeCommitted(object sender, EventArgs e)
        {
            checkStartBox();
            checkEndBox();
        }

        private void startMinute_SelectionChangeCommitted(object sender, EventArgs e)
        {
            checkStartBox();
            checkEndBox();
        }

        private void endHour_SelectionChangeCommitted(object sender, EventArgs e)
        {
            
            checkStartBox();
            checkEndBox();
        }
    }
}
