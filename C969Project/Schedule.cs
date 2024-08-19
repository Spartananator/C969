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
        bool success = false;
        string user = string.Empty;
        int userID;
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
            user = username;
            userID = ID;
            success = true;
            loadApp();
        }

        public void loadApp()
        {
            userLabel.Text = "Welcome " + user +$",\nYour user ID is {userID}";
            Mainscheduler scheduler = new Mainscheduler();
            //MySqlConnection conn = scheduler.getdatabase();

            scheduler.InitializeDatabase(this);




        }
        BindingSource customersource = new BindingSource();
        BindingSource meetingsource = new BindingSource();
        List<meeting> meetingsList;
        List<Customer> customersList;
        public void setGrids(List<meeting> meetings, List<Customer> customers)
        {
            meetingsList = meetings;
            customersList = customers;
            meetingsource.DataSource = meetingsList;
            customersource.DataSource = customersList;
            customerGrid.AutoGenerateColumns = true;
            MessageBox.Show(meetings[0].Start.ToString());
            appointmentGrid.DataSource = meetingsource;
            customerGrid.DataSource = customersource;

            foreach (DataGridViewColumn column in appointmentGrid.Columns)
            {
                if (column.DataPropertyName == "Start" || column.DataPropertyName == "End")
                {
                    //MessageBox.Show(column.DataPropertyName +" "+ column.HeaderText);
                    column.DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                }
            }
        }

        private void dataViewer_Selected(object sender, TabControlEventArgs e)
        {
            var tab = e.TabPage;
            var index = e.TabPageIndex;
            
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            DateTime selected = monthCalendar1.SelectionStart;
            var filteredList = meetingsList.Where(p => p.Start >= selected && p.End <= monthCalendar1.SelectionEnd).ToList();
            meetingsource.DataSource = filteredList;
        }
        //private void
    }
}
