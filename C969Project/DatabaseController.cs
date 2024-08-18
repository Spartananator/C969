using MySql.Data.MySqlClient;
using Scheduling_Software;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace C969Project
{

    partial class Mainscheduler
    {
        string popcustomer = @"
            select c.customerName, a.address, a.address2, a.postalCode, a.phone, ci.city, co.country 
            from customer as c
            left join address as a
            ON c.addressId = a.addressId
            left join city as ci
            ON a.cityId = ci.cityId
            left join country as co
            ON ci.countryId = co.countryId;";
        string popmeeting = @"
            select c.customerName, u.userName, a.title, a.description, a.location, a.contact, a.type, a.url, a.start, a.end from appointment as a
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
            BindingList<meeting> meetings = new BindingList<meeting>();
            BindingList<Customer> customers = new BindingList<Customer>();
            
            
            sched.setGrids(meetings, customers);
        }
    }


    public class meeting
    {
        Customer Customer;
        string User;
        string Title;
        string Description;
        string Location;
        string Contact;
        string Type;
        string Url;
        DateTime Start;
        DateTime End;

        public meeting(Customer customer, string user, string title, string description, string location, string contact, string type, string url, DateTime start, DateTime end)
        {
            Customer = customer;
            User = user;
            Title = title;
            Description = description;
            Location = location;
            Contact = contact;
            Type = type;
            Url = url;
            Start = start;
            End = end;

        }
    }
    public class Customer
    {
        string CustomerName;
        string Adress;
        string Adress2;
        string City;
        int Zipcode;
        string Country;
        string PhoneNumber;
        
        public Customer(string customerName,string adress, string adress2, string city, int zipcode, string country, string phoneNumber)
        {
            CustomerName = customerName;
            Adress = adress;
            City = city;
            Zipcode = zipcode;
            Country = country;
            PhoneNumber = phoneNumber;

        }
    }
}
