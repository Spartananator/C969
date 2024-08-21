using C969Project;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.BC;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scheduling_Software
{

    public partial class Schedule : Form
    {
        Mainscheduler scheduler = new Mainscheduler();
        user curUser;
        bool success = false;
        int index;

        public Schedule()
        {
            InitializeComponent();
            do
            {
                attemptLogin();
            }
            while
                (success == false);
        }
        private void attemptLogin()
        {
            using (var login = new Login(this))
            {
                login.ShowDialog();
            }

        }
        public void successLogin(string username, int ID)
        {
            curUser = new user(username, ID);
            success = true;
            loadApp();
        }

        public void loadApp()
        {
            //runs once application receives succesful sign in and loads the database data
            userLabel.Text = $"Welcome {curUser.userName},\nYour user ID is {curUser.userID}";

            //MySqlConnection conn = scheduler.getdatabase();

            scheduler.InitializeDatabase(this);


            //set filter to show meetings that have already ended today
            pastBox_CheckedChanged(null, null);
        }
        BindingSource customersource = new BindingSource();
        BindingSource meetingsource = new BindingSource();
        List<meeting> meetingsList;
        List<Customer> customersList;

        public void setGrids(List<meeting> meetings, List<Customer> customers)
        {
            //attach lists to bindingsources, attach binding sources to grids
            meetingsList = meetings;
            customersList = customers;
            meetingsource.DataSource = meetingsList;
            customersource.DataSource = customersList;
            customerGrid.AutoGenerateColumns = true;
            appointmentGrid.DataSource = meetingsource;
            customerGrid.DataSource = customersource;


            // set visible meetings to today
            DateTime selected = monthCalendar1.SelectionStart;
            var filteredList = meetingsList.Where(p => p.End >= selected).ToList();
            meetingsource.DataSource = filteredList;

            foreach (DataGridViewColumn column in appointmentGrid.Columns)
            {
                if (column.DataPropertyName == "Start" || column.DataPropertyName == "End")
                {
                    //MessageBox.Show(column.DataPropertyName +" "+ column.HeaderText);
                    column.DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                }
            }
            checkAlert();
        }
        private void checkAlert()
        {
            foreach (meeting meet in meetingsList)
            {
                if (meet.User == curUser.userName)
                {

                    TimeSpan difference = meet.Start - DateTime.Now;
                    if (difference <= TimeSpan.FromMinutes(15) && difference > TimeSpan.FromSeconds(0))
                    {
                        MessageBox.Show($"You have a meeting with {meet.Customer} in {((int)difference.TotalMinutes)} minutes");
                    }
                }
            }
        }
        private void dataViewer_Selected(object sender, TabControlEventArgs e)
        {
            //sets context depending on selected tab

            index = e.TabPageIndex;


        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            //change visible meetings based on selected day
            DateTime selected = monthCalendar1.SelectionStart;
            if (monthBox.Checked == true)
            {
                var filteredList = meetingsList.Where(p => p.Start.Month == selected.Month).ToList();
                meetingsource.DataSource = filteredList;
            }
            else
            {
                if (pastBox.Checked == true)
                {
                    var filteredList = meetingsList.Where(p => p.Start.Day == selected.Day).ToList();
                    meetingsource.DataSource = filteredList;
                }
                else
                {
                    var filteredList = meetingsList.Where(p => p.Start.Day == selected.Day && p.End >= DateTime.Now).ToList();
                    meetingsource.DataSource = filteredList;
                }

            }

        }

        private void add_Click(object sender, EventArgs e)
        {
            if (index == 0)
            {
                meeting meets = null;
                MeetingForm meet = new MeetingForm(meets, meetingsList, customersList, curUser, this);
                meet.ShowDialog();
            }
            else if (index == 1)
            {
                
                Customer customer = null;
                customerForm cust = new customerForm(customer, curUser, this);
                cust.ShowDialog();
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            if (index == 0)
            {
                meeting meet = (appointmentGrid.CurrentRow.DataBoundItem) as meeting;
                string deletecom = "delete from appointment where appointmentId = @appid;";
                var conn = scheduler.getdatabase();
                var comm = scheduler.createQuery(conn, deletecom);

                comm.Parameters.AddWithValue("@appid", meet.getKey());

                try
                {
                    conn.Open();
                    comm.ExecuteNonQuery();
                    conn.Close();
                    scheduler.InitializeDatabase(this);
                }
                catch
                {
                    MessageBox.Show("Could not connect to database!");
                }
            }
            else if (index == 1)
            {
                Customer cust = (customerGrid.CurrentRow.DataBoundItem) as Customer;
                string deletecom = "delete from customer where customerId = @cusid; delete from address where addressId = @addressid; delete from city where cityId = @cityid; delete from country where countryId = @countryId;";
                var conn = scheduler.getdatabase();
                var comm = scheduler.createQuery(conn, deletecom);
                comm.Parameters.AddWithValue("@cusid", (cust.getKeys())[0]);
                comm.Parameters.AddWithValue("@addressid", (cust.getKeys())[1]);
                comm.Parameters.AddWithValue("@cityid", (cust.getKeys())[2]);
                comm.Parameters.AddWithValue("@countryid", (cust.getKeys())[3]);
                try
                {
                    conn.Open();
                    comm.ExecuteNonQuery();
                    conn.Close();
                    scheduler.InitializeDatabase(this);
                }
                catch
                {
                    MessageBox.Show("Could not connect to database!");
                }
            }
        }

        private void modify_Click(object sender, EventArgs e)
        {
            if (index == 0)
            {
               // try
                {
                    //if you have been brought here due to an exception please press continue.
                    //VS while running in debugging decides to throw this error even though it is handled.
                    //This error would not happen when running the application normally.
                    meeting meets = (appointmentGrid.CurrentRow.DataBoundItem) as meeting;
                    MeetingForm meet = new MeetingForm(meets, meetingsList, customersList, curUser, this);
                    meet.ShowDialog();
                }
                //catch
                {
                    //MessageBox.Show("No Meeting is selected / available to modify.");
                }
            }
            else if (index == 1)
            {
                //DataGridViewRow row = new DataGridViewRow();
                Customer customer = (customerGrid.CurrentRow.DataBoundItem) as Customer;
                customerForm cust = new customerForm(customer, curUser, this);
                cust.ShowDialog();
            }
        }

            private void monthBox_CheckedChanged(object sender, EventArgs e)
            {
                monthCalendar1_DateChanged(null, null);
            }

            public void pastBox_CheckedChanged(object sender, EventArgs e)
            {
                monthCalendar1_DateChanged(null, null);
            }

            private void generateReports_Click(object sender, EventArgs e)
            {

                Func<string> AbT = () =>
                {
                    List<string> report1 = new List<string>() { $"All appointments counted by type:\n" };
                    string allbytype = "select type, COUNT(*) as Amount from appointment group by type;";
                    var conn = scheduler.getdatabase();
                    var comm = scheduler.createQuery(conn, allbytype);

                    conn.Open();
                    var reader = comm.ExecuteReader();
                    while (reader.Read())
                    {

                        report1.Add($"{reader.GetString(0)}: {reader.GetInt64(1).ToString()}\n");
                    }
                    string report2 = string.Empty;
                    foreach (string report in report1)
                    {
                        report2 = report2 + report;
                    }
                    return report2;
                };

                MessageBox.Show(AbT());

                Func<string> SbU = () =>
                {
                    string getuser = "select userName from user;";
                    string schedulebyuser = "The schedule for each user:\n";
                    List<string> userNames = new List<string>();
                    var conn = scheduler.getdatabase();
                    var comm = scheduler.createQuery(conn, getuser);
                    conn.Open();
                    var reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        userNames.Add(reader.GetString(0));
                    }
                    conn.Close();
                    conn = scheduler.getdatabase();

                    foreach (string user in userNames)
                    {
                        comm = scheduler.createQuery(conn, $@"select a.start, a.end from appointment as a
                                                            left join user as u on a.userId = u.userId
                                                            where u.userName = '{user}';");
                        conn.Open();
                        reader = comm.ExecuteReader(0);
                        schedulebyuser += $"Schedule for {user}:\n";
                        while (reader.Read())
                        {
                            schedulebyuser += $"Busy: {reader.GetDateTime(0).ToLocalTime().ToString()} through {reader.GetDateTime(0).ToLocalTime().ToString()}\n";

                        }

                    }
                    return schedulebyuser;
                };
                MessageBox.Show(SbU());

                Func<string> AbC = () =>
                {
                    string customers = "select c.customerName, COUNT(*) from appointment as a left join customer as c on c.customerId = a.customerId group by c.customerName;";
                    string report = "Total count of meetings per customer:\n";

                    var conn = scheduler.getdatabase();
                    var comm = scheduler.createQuery(conn, customers);

                    conn.Open();
                    var reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        report += $"{reader.GetString(0)} has {reader.GetInt32(1).ToString()} total meetings.\n";
                    }

                    return report;
                };
                MessageBox.Show(AbC());
            }
        }
    
    }

