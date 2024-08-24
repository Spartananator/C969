using System;
using System.Runtime;
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
using System.Security.Cryptography;

namespace C969Project
{
    public partial class MeetingForm : Form
    {
        int offset = (DateTime.UtcNow - TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"))).Hours;
        List<string> hours = new List<string> {"01","02","03","04","05","06","07","08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21","22","23","00" };
        List<string> minutes = new List<string> { "00", "15", "30","45"};
        List<string> localhours = new List<string>();
        DateTime BusinessStart = new DateTime(2024,8,22,8,0,0, DateTimeKind.Unspecified) ; //(2001,01,01,09,0,0);
        DateTime BusinessEnd = new DateTime(2024, 8, 22, 8, 0, 0, DateTimeKind.Unspecified);  //(2001, 01, 01, 17, 0, 0);
        DateTime localbusend;
        DateTime localbusstart;

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
            BusinessStart = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")); BusinessStart = BusinessStart.Date.AddHours(09);
            BusinessEnd = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")); BusinessEnd = BusinessEnd.Date.AddHours(17);
            
            BusinessStart = TimeZoneInfo.ConvertTimeToUtc(BusinessStart, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
            BusinessEnd = TimeZoneInfo.ConvertTimeToUtc(BusinessEnd, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
            localbusend = TimeZoneInfo.ConvertTimeFromUtc(BusinessEnd, TimeZoneInfo.FindSystemTimeZoneById(TimeZoneInfo.Local.Id));
            localbusstart = TimeZoneInfo.ConvertTimeFromUtc(BusinessStart, TimeZoneInfo.FindSystemTimeZoneById(TimeZoneInfo.Local.Id));
            meetingsList = meets;
            customersList = custs;
            InitializeComponent();
            
            
            //startDate.MinDate = DateTime.Now.Date;
            //endDate.MinDate = DateTime.Now.Date;
            curUser = User;
            sched = s;
            initDates(localbusstart, localbusend);
            configureGrid();
            if (meet != null)
            {
                updateMeet = meet;
                fillFields(meet);
            }
            else
            {
                

            }
        }
        private void initDates(DateTime start, DateTime end)
        {
            startDate.MinDate = start;
            startDate.MaxDate = end.AddMinutes(-15);
            endDate.MinDate = start.AddMinutes(15);
            endDate.MaxDate = end;
            datepicker.MinDate = start.Date;
            datepicker.Value = start;

            

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
            

            
            
            //initDates();

            checkDates();
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
            {
            
            

            
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
                        string startd = startDate.Value.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss");
                        string endd = endDate.Value.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss");
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
                            if (startDate.Value < meet.Start || startDate.Value > meet.End)
                            {
                                if (endDate.Value < meet.Start || endDate.Value > meet.End)
                                {
                                    if (meet.Start > startDate.Value && meet.Start < endDate.Value)
                                    {
                                        secure = false;
                                        MessageBox.Show($"Unable to schedule this meeting at the same time as the meeting with {meet.Customer} \n This meeting is from {meet.Start} to {meet.End}");
                                    }
                                    else { if (endDate == startDate) { secure = false; MessageBox.Show("Cannot have a meeting with the same start and end time."); } }

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
            }
        }

        private void startDate_ValueChanged(object sender, EventArgs e)
        {
            
                try
                {
                if (startDate.Value.Minute == 46)
                {

                    if(startDate.Value == startDate.MaxDate.AddMinutes(-1))
                    {
                        startDate.Value = startDate.Value.AddMinutes(-1);
                    }
                    else 
                    {
                    var diff = 60 - startDate.Value.Minute;
                    startDate.Value = startDate.Value.AddMinutes(diff);
                    endDate.MinDate = startDate.Value;
                        endDate.Value = startDate.Value.AddMinutes(15);
                    }
                    

                }else if (startDate.Value.Minute == 47)
                {
                    startDate.Value = startDate.MaxDate.AddHours(-1).AddMinutes(-2);
                    startDate.MaxDate = localbusend.AddMinutes(-15);
                }
                else if (startDate.Value.Minute == 59)
                {
                    if (startDate.MinDate.AddHours(1) == startDate.Value.AddMinutes(1))
                    {
                        
                        startDate.Value = startDate.MinDate;
                    }else
                    {
                        
                        var diff = 45 - startDate.Value.Minute;
                        startDate.Value = startDate.Value.AddMinutes(diff);
                        startDate.Value = startDate.Value.AddHours(-1);
                        endDate.MinDate = startDate.Value.AddMinutes(15);
                        endDate.Value = startDate.Value.AddMinutes(15);
                    }
                    
                }
                else if (startDate.Value.Minute == 14)
                {
                    var diff = (00 - startDate.Value.Minute);
                    startDate.Value = startDate.Value.AddMinutes(-14);
                    endDate.MinDate = startDate.Value.AddMinutes(15);
                    endDate.Value = startDate.Value.AddMinutes(15);
                    
                }
                else if (startDate.Value.Minute == 31)
                {
                    
                    var diff = 45 - startDate.Value.Minute;
                    startDate.Value = startDate.Value.AddMinutes(diff);
                    
                    endDate.Value = startDate.Value.AddMinutes(15);
                    endDate.MinDate = startDate.Value.AddMinutes(15);
                }
                else if (startDate.Value.Minute == 16 || startDate.Value.Minute == 44)
                {
                    var diff = 30 - startDate.Value.Minute;
                    if (startDate.Value.Minute == 44)
                    {
                        startDate.Value = startDate.Value.AddMinutes(diff);
                        endDate.MinDate = startDate.Value.AddMinutes(15);
                        endDate.Value = startDate.Value.AddMinutes(15);
                        
                    }
                    else
                    {
                        startDate.Value = startDate.Value.AddMinutes(diff);

                        endDate.Value = startDate.Value.AddMinutes(15);
                        endDate.MinDate = startDate.Value.AddMinutes(15);
                    };
                    
                }
                else if (startDate.Value.Minute == 01 || startDate.Value.Minute == 29)
                {
                    var diff = 15 - startDate.Value.Minute;
                    if (startDate.Value.Minute == 29)
                    {
                        startDate.Value = startDate.Value.AddMinutes(diff);
                        endDate.MinDate = startDate.Value.AddMinutes(15);
                        endDate.Value = startDate.Value.AddMinutes(15);
                        
                    }
                    else
                    {
                        startDate.Value = startDate.Value.AddMinutes(diff);
                        
                        endDate.Value = startDate.Value.AddMinutes(15);
                        endDate.MinDate = startDate.Value.AddMinutes(15);
                    }
                    

                }
                else if (startDate.Value == startDate.MaxDate)
                {
                    startDate.MaxDate = startDate.MaxDate.AddMinutes(2);
                    //startDate.Value = startDate.MaxDate.AddHours(-1);
                }
                else
                {
                    endDate.MinDate = startDate.Value.AddMinutes(15);
                    endDate.Value = startDate.Value.AddMinutes(15);
                }
                //endDate.Value = startDate.Value.AddMinutes(15);
                //endDate.MinDate = startDate.Value;

            }
                catch(Exception ex)
                {
                MessageBox.Show("Do not set the end date before the start date, \nnor as the same time as the start date.");
                
                //endDate.MinDate = startDate.Value;
                    //endDate.MaxDate = startDate.Value.AddHours(8);
                    
                }
                
           
            
            
        }
        private bool dateIsValid (DateTimePicker picker)
        {
            foreach (DayOfWeek Day in InvalidDay) {
                if (picker.Value.ToUniversalTime().DayOfWeek == Day)
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
            try
            {
                if (endDate.Value.Minute == 46)
                {
                    var diff = 60 - endDate.Value.Minute;
                    endDate.Value = endDate.Value.AddMinutes(diff);
                    endDate.MinDate = startDate.Value;
                    //endDate.Value = startDate.Value.AddMinutes(15);

                }else if (endDate.Value.Minute == 59)
                {
                    var diff = 45 - endDate.Value.Minute;
                    endDate.Value = endDate.Value.AddMinutes(diff);
                    endDate.Value =  endDate.Value.AddHours(-1);
                    endDate.MinDate = startDate.Value;
                    //endDate.Value = startDate.Value.AddMinutes(15);
                }else if (endDate.Value.Minute == 14)
                {                    
                    var diff = (00 - endDate.Value.Minute);
                    endDate.Value = endDate.Value.AddMinutes(-14);
                    endDate.MinDate = startDate.Value;
                }
                else if (endDate.Value.Minute == 31)
                {
                    var diff = 45 - endDate.Value.Minute;
                    endDate.Value = endDate.Value.AddMinutes(diff);
                    endDate.MinDate = startDate.Value;
                    //endDate.Value = startDate.Value.AddMinutes(15);
                }
                else if (endDate.Value.Minute == 16 || endDate.Value.Minute == 44)
                {
                    var diff = 30 - endDate.Value.Minute;
                    endDate.Value = endDate.Value.AddMinutes(diff);
                    endDate.MinDate = startDate.Value;
                    //endDate.Value = startDate.Value.AddMinutes(15);
                }
                else if (endDate.Value.Minute == 01 || endDate.Value.Minute == 29)
                {
                    var diff = 15 - endDate.Value.Minute;
                    endDate.Value = endDate.Value.AddMinutes(diff);
                    endDate.MinDate = startDate.Value;
                    //endDate.Value = startDate.Value.AddMinutes(15);
                }
                
            }
            catch { }
            }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (searchBox.Text == "")
                {
                    customers.DataSource = customersList;
                }
                else if (searchBox.Text.Any(c => char.IsDigit(c) || c == '-'))
                {
                    
                    string zip = searchBox.Text;

                    var filteredList = customersList.Where(p => p.Zipcode.Contains(zip) ).ToList();
                    customers.DataSource = filteredList;


                }
                else
                {
                    string name = searchBox.Text;
                    
                    var filteredList = customersList.Where(p => p.CustomerName.ToLower().Contains(name.ToLower())).ToList();

                    customers.DataSource = filteredList;
                }

            }
            catch
            {
                

            }
            
        }



        
        private void checkDates()
        {
            endDate.MinDate = startDate.Value;
            
        }

        

        private void searchBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == '-')
            {
                
            }
            else
            {
                
            }
            

        }
        private void setTime(DateTime time)
        {
            BusinessStart = TimeZoneInfo.ConvertTimeFromUtc(time, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")); BusinessStart = BusinessStart.Date.AddHours(09);
            BusinessEnd = TimeZoneInfo.ConvertTimeFromUtc(time, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")); BusinessEnd = BusinessEnd.Date.AddHours(17);

            BusinessStart = TimeZoneInfo.ConvertTimeToUtc(BusinessStart, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
            BusinessEnd = TimeZoneInfo.ConvertTimeToUtc(BusinessEnd, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
            localbusend = TimeZoneInfo.ConvertTimeFromUtc(BusinessEnd, TimeZoneInfo.FindSystemTimeZoneById(TimeZoneInfo.Local.Id));
            localbusstart = TimeZoneInfo.ConvertTimeFromUtc(BusinessStart, TimeZoneInfo.FindSystemTimeZoneById(TimeZoneInfo.Local.Id));
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            setTime(datepicker.Value.ToUniversalTime());
            var diff = (datepicker.Value - localbusstart);
            try
            {
                
                if(BusinessStart.Add(diff).DayOfWeek != DayOfWeek.Sunday && BusinessStart.Add(diff).DayOfWeek != DayOfWeek.Saturday)
                {
                    endDate.MaxDate = localbusend.Add(diff);
                    startDate.MaxDate = localbusend.Add(diff).AddMinutes(-15);
                    startDate.Value = localbusstart.Add(diff);
                    endDate.Value = localbusstart.Add(diff).AddMinutes(15);
                    endDate.MinDate = localbusstart.Add(diff).AddMinutes(15);
                    startDate.MinDate = localbusstart.Add(diff);
                }

                //endDate.MinDate = datepicker.Value;
               // startDate.MinDate = datepicker.Value;
            }
            catch
            {
                //startDate.MinDate = datepicker.Value;
                endDate.MinDate = localbusstart.Add(diff).AddMinutes(15);
                startDate.MinDate = localbusstart.Add(diff);
                endDate.Value = localbusstart.Add(diff).AddMinutes(15);
                startDate.Value = localbusstart.Add(diff);
                endDate.MaxDate = localbusend.Add(diff);
                startDate.MaxDate = localbusend.Add(diff).AddMinutes(-15);
            }
            
        }

        
    }
}
