using MySql.Data.MySqlClient;
using Scheduling_Software;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace C969Project
{
    public partial class customerForm : Form
    {
        user curUser;
        Schedule sched;
        Customer custom;
        public customerForm(Customer customer, user User, Schedule schede)
        {
            InitializeComponent();
            curUser = User;
            sched = schede;
            if (customer != null)
            {
                custom = customer;
                fillFields(customer);
            }
        }

        private void fillFields(Customer cust)
        {
            customernameBox.Text = cust.CustomerName;
            addressBox.Text = cust.Adress;
            address2Box.Text = cust.Adress2;
            cityBox.Text = cust.City;
            zipcodeBox.Text = cust.Zipcode.ToString();
            phoneBox.Text = cust.PhoneNumber;
            countryBox.Text = cust.Country;


        }
        List<TextBox> boxes = new List<TextBox>();
        
        private void submitButton_Click(object sender, EventArgs e)
        {
            boxes = new List<TextBox> { cityBox, phoneBox, addressBox, countryBox, customernameBox, zipcodeBox };
            List<string> fields = new List<string>() {"City","Phone","Address","Country","Customer Name","Zip / Postal Code" };
            bool required = true;
            string err = "The following fields require input:\n";
            foreach (TextBox box in boxes)
            {
                if (box.Text == "") {
                    required = false;
                    err += $" {fields[boxes.IndexOf(box)]}\n";
                }
            }
            if (required == false)
            {

                MessageBox.Show(err);
                
            }
            else { 




            Func<bool, int> active = term =>
            {
                if (term)
                {
                    return (1);
                }
                else
                {
                    return (0);
                }
            };
             string insertCountryStmt = @"
    INSERT INTO country (country, createDate, createdBy, lastUpdate, lastUpdateBy)
    VALUES (@country, @update, @updateby, @update, @updateby);
    SELECT LAST_INSERT_ID();";

            string insertCityStmt = @"
    INSERT INTO city (city, countryid, createDate, createdBy, lastUpdate, lastUpdateBy)
    VALUES (@city, @countryid, @update, @updateby, @update, @updateby);
    SELECT LAST_INSERT_ID();";

            string insertAddressStmt = @"
    INSERT INTO address (address, address2, cityId, postalCode, phone, createDate, createdBy, lastUpdate, lastUpdateBy)
    VALUES (@address, @address2, @cityid, @postal, @phone, @update, @updateby, @update, @updateby);
    SELECT LAST_INSERT_ID();";
            Mainscheduler scheduler = new Mainscheduler();
            var conn = scheduler.getdatabase();
            if (custom == null)
            {
                string insertCustomerStmt = @"
    INSERT INTO customer (customerName, addressId, active, createDate, createdBy, lastUpdate, lastUpdateBy)
    VALUES (@customer, @addressid, @active, @update, @updateby, @update, @updateby);";



                try
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {
                        int countryId;
                        using (var comm = new MySqlCommand(insertCountryStmt, conn, transaction))
                        {
                            comm.Parameters.AddWithValue("@update", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
                            comm.Parameters.AddWithValue("@updateby", curUser.userName);
                            comm.Parameters.AddWithValue("@country", countryBox.Text.Trim());

                            countryId = Convert.ToInt32(comm.ExecuteScalar());
                        }

                        int cityId;
                        using (var comm = new MySqlCommand(insertCityStmt, conn, transaction))
                        {
                            comm.Parameters.AddWithValue("@update", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
                            comm.Parameters.AddWithValue("@updateby", curUser.userName);
                            comm.Parameters.AddWithValue("@city", cityBox.Text.Trim());
                            comm.Parameters.AddWithValue("@countryid", countryId);

                            cityId = Convert.ToInt32(comm.ExecuteScalar());
                        }

                        int addressId;
                        using (var comm = new MySqlCommand(insertAddressStmt, conn, transaction))
                        {
                            comm.Parameters.AddWithValue("@update", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
                            comm.Parameters.AddWithValue("@updateby", curUser.userName);
                            comm.Parameters.AddWithValue("@address", addressBox.Text.Trim());
                            comm.Parameters.AddWithValue("@address2", address2Box.Text.Trim());
                            comm.Parameters.AddWithValue("@cityid", cityId);
                            comm.Parameters.AddWithValue("@postal", zipcodeBox.Text.Trim());
                            comm.Parameters.AddWithValue("@phone", phoneBox.Text.Trim());

                            addressId = Convert.ToInt32(comm.ExecuteScalar());
                        }

                        using (var comm = new MySqlCommand(insertCustomerStmt, conn, transaction))
                        {
                            comm.Parameters.AddWithValue("@update", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
                            comm.Parameters.AddWithValue("@updateby", curUser.userName);
                            comm.Parameters.AddWithValue("@customer", customernameBox.Text.Trim());
                            comm.Parameters.AddWithValue("@addressid", addressId);
                            comm.Parameters.AddWithValue("@active", active(activeBox.Checked));

                            comm.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    // Consider logging the exception and/or handling it appropriately
                }
                finally
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }

                scheduler.InitializeDatabase(sched);
                this.Close();
            }
            else
            {
                string updatecust =
                @"
update country set country = @country, lastUpdate = @update, lastUpdateBy = @user where countryId = @id4;
update city set city = @city, lastUpdate = @update, lastUpdateBy = @user where cityId = @id3;
update address set address = @address, address2 = @address2, postalCode = @zip, phone = @phone, lastUpdate = @update, lastUpdateBy = @user where addressId = @id2;
update customer set customerName = @name, active = @active, lastUpdate = @update, lastUpdateBy = @user where customerId = @id;
              ";
                List<int> keys = custom.getKeys();
                var conn2 = scheduler.getdatabase();
                var comm2 = scheduler.createQuery(conn2, updatecust);
                comm2.Parameters.AddWithValue("@update", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
                comm2.Parameters.AddWithValue("@user", curUser.userName);
                comm2.Parameters.AddWithValue("@address", addressBox.Text);
                comm2.Parameters.AddWithValue("@address2", address2Box.Text);
                comm2.Parameters.AddWithValue("@id3", keys[2]);
                comm2.Parameters.AddWithValue("@zip", zipcodeBox.Text);
                comm2.Parameters.AddWithValue("@phone", phoneBox.Text);
                comm2.Parameters.AddWithValue("@name", customernameBox.Text);
                comm2.Parameters.AddWithValue("@id2", keys[1]);
                comm2.Parameters.AddWithValue("@active", active(activeBox.Checked));
                comm2.Parameters.AddWithValue("@city", cityBox.Text);
                comm2.Parameters.AddWithValue("@id4", keys[3]);
                comm2.Parameters.AddWithValue("@country", countryBox.Text);
                comm2.Parameters.AddWithValue("@id", keys[0]);
                    try
                    {
                        conn2.Open();
                        comm2.ExecuteNonQuery();
                        conn2.Close();

                        scheduler.InitializeDatabase(sched);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        conn2.Close();
                    }
                }
            }
        }

        private void phoneBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            
            if (char.IsDigit(e.KeyChar) || e.KeyChar == '-')
            {
                return;
            }

            
            e.Handled = true;
        }
    }
}
