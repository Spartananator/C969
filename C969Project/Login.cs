using MySql.Data.MySqlClient;
using Scheduling_Software;
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C969Project
{
    public partial class Login : Form
    {
        string failedLogin = "Provided username or password was incorrect.";
        string failedConnection = "Failed to connect to the database";
        Schedule sched;
        public Login(Schedule sched1)
        {
            sched = sched1;
            InitializeComponent();
            var regionInfo = RegionInfo.CurrentRegion;
            CultureInfo name = CultureInfo.CurrentCulture;
            
            setlocalizaton(name);
            
        }

        private void setlocalizaton(CultureInfo locale)
        {
            ResourceManager rm = new ResourceManager("C969Project.Properties.Resource", typeof(Program).Assembly);


            label1.Text = rm.GetString("label1", locale);
            label2.Text = rm.GetString("label2", locale);
            loginButton.Text = rm.GetString("loginButton", locale);
            failedConnection = rm.GetString("connectionFail", locale);
            failedLogin = rm.GetString("incorrectLogin", locale);

        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            var controller = new Mainscheduler();
            string username = usernameBox.Text;
            var conn = controller.getdatabase();
            var comm = controller.createQuery(conn, "SELECT password, userId FROM user WHERE userName = ?name");
            comm.Parameters.AddWithValue("name", username);

            try {
                conn.Open();
            } catch 
            {
                MessageBox.Show(failedConnection);
            }
            MySqlDataReader reader = comm.ExecuteReader();


            while (reader.Read())
            {
                string password = reader.GetString(0);
                int userID = reader.GetInt16(1);
                string passbox = passwordBox.Text;
                
                if (passbox == password)
                {
                    
                    sched.successLogin(username, userID);
                    DateTime now = DateTime.UtcNow;
                    string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    string filepath = System.IO.Path.Combine(documentsPath, "userlog.txt");
                    string append = username +" "+ now;

                    try
                    {
                        File.AppendAllText(filepath, append + Environment.NewLine);
                    }
                    catch
                    {
                        File.WriteAllText(filepath, append);
                    }

                    this.Close();
                    
                }
                else
                {
                    MessageBox.Show(failedLogin);

                }

            }
            conn.Close();


        }
    }
}
