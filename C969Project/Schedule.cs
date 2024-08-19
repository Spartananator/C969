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
            var tab = e.TabPage;
            var index = e.TabPageIndex;
            
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            //change visible meetings based on selected day
            DateTime selected = monthCalendar1.SelectionStart;
            var filteredList = meetingsList.Where(p => p.End >= selected).ToList();
            meetingsource.DataSource = filteredList;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MeetingForm meet = new MeetingForm(meetingsList, customersList, curUser, this);
            meet.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            meeting selectedmeet = (row = this.appointmentGrid.CurrentRow).DataBoundItem as meeting;
            string deleteapt = "delete from appointment where appointmentId = @id";
            MySqlConnection conn = scheduler.getdatabase();
            MySqlCommand comm = scheduler.createQuery(conn, deleteapt);

            comm.Parameters.AddWithValue("@id",selectedmeet.MeetingID);

            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show("Confirm Deletion", "Alert", buttons);
            if (result == DialogResult.Yes)
            {
                conn.Open();
                comm.ExecuteNonQuery();
                conn.Close();
                scheduler.InitializeDatabase(this);
            }
        }
        //private void
    }
}
