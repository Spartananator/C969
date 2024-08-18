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
            MySqlConnection conn = scheduler.getdatabase();

            scheduler.InitializeDatabase(this);




        }

        public void setGrids(BindingList<meeting> meetings, BindingList<Customer> customers)
        {
            BindingSource meetingsource = new BindingSource();
            meetingsource.DataSource = meetings;
            BindingSource customersource = new BindingSource();
            customersource.DataSource = customers;
            appointmentGrid.DataSource = meetingsource;
            customerGrid.DataSource = customersource;
        }

        private void dataViewer_Selected(object sender, TabControlEventArgs e)
        {
            var tab = e.TabPage;
            var index = e.TabPageIndex;
            
        }
    }
}
