using MySql.Data.MySqlClient;
using Scheduling_Software;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace C969Project
{

    partial class Mainscheduler
    {
        string popcustomer = @"
            select c.customerId, c.customerName, a.address, a.address2, ci.city, a.postalCode, co.country, a.phone 
            from customer as c
            left join address as a
            ON c.addressId = a.addressId
            left join city as ci
            ON a.cityId = ci.cityId
            left join country as co
            ON ci.countryId = co.countryId;";
        string popmeeting = @"
            select a.customerId, c.customerName, u.userName, a.title, a.description, a.location, a.contact, a.type, a.url, a.start, a.end, a.appointmentId from appointment as a
            left join customer as c
            ON c.customerId = a.customerId
            left join user as u 
            ON u.userId = a.userId";
        public MySqlConnection getdatabase() 
        { 
            string constr = "Host=localhost;Port=3306;Database=client_schedule;Username=sqlUser;Password=Passw0rd!";
            MySqlConnection conn = new MySqlConnection(constr);

            return conn;

        }
        public MySqlCommand createQuery(MySqlConnection conn, string query)
        {
            
            MySqlCommand comm = conn.CreateCommand();
            comm.CommandText = query;

            return comm;
        }

        public void InitializeDatabase(Schedule sched)
        {
            List<meeting> meetings = new List<meeting>();
            List<Customer> customers = new List<Customer>();

            var conn = getdatabase();
            var comm = createQuery(conn, popcustomer);

            try
            {
                conn.Open();
            }
            catch { MessageBox.Show("Error connecting to database"); }

            MySqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                
                Customer cust = new Customer
                    (
                        reader.GetInt16(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4),
                        reader.GetInt16(5),
                        reader.GetString(6),
                        reader.GetString(7)
                        
                    );
                customers.Add(cust);
            }
            conn.Close();

            conn = getdatabase();
            comm = createQuery(conn, popmeeting);

            try
            {
                conn.Open();
            }
            catch { MessageBox.Show("Error connecting to database"); }

            reader = comm.ExecuteReader();
            while (reader.Read())
            {

                meeting meet = new meeting
                    (
                        reader.GetInt16(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4),
                        reader.GetString(5),
                        reader.GetString(6),
                        reader.GetString(7),
                        reader.GetString(8),
                        reader.GetDateTime(9),
                        reader.GetDateTime(10),
                        reader.GetInt16(11)
                    );
                meetings.Add(meet);
            }
            conn.Close();
            sched.setGrids(meetings, customers);
        }
    }


    public class meeting
    {
        public int MeetingID { get; set; }
        private int CustomerID { get;  set; }
        public string Customer { get; set; }
        public string User { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Contact { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public meeting( int customerID, string customer, string user, string title, string description, string location, string contact, string type, string url, DateTime start, DateTime end, int meetingid)
        {
            MeetingID = meetingid;
            CustomerID = customerID; 
            Customer = customer;
            User = user;
            Title = title;
            Description = description;
            Location = location;
            Contact = contact;
            Type = type;
            Url = url;
            Start = start.ToLocalTime();
            End = end.ToLocalTime();

        }
    }
    public class Customer
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string Adress { get; set; }
        public string Adress2 { get; set; }
        public string City { get; set; }
        public int Zipcode { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        
        public Customer(int customerid, string customerName,string adress, string adress2, string city, int zipcode, string country, string phoneNumber)
        {
            CustomerID = customerid;
            CustomerName = customerName;
            Adress = adress;
            Adress2 = adress2;
            City = city;
            Zipcode = zipcode;
            Country = country;
            PhoneNumber = phoneNumber;

        }
    }
    
    public class user
    {
        public int userID { get; set; }
    public string userName { get; set; }

    public user(string username, int id)
    {
        userID = id;
        userName = username;
    }
}
}
