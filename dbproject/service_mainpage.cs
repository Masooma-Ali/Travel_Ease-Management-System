using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TOUROPERATOR_INTERFACE;

namespace dbproject
{
    public partial class service_mainpage : Form
    {
        string connectionString = "Data Source=DESKTOP-842J4RM\\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;Encrypt=False";
        private int _providerID;

        public service_mainpage(int providerID)
        {
            InitializeComponent();
            _providerID = providerID;

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Transport reg = new Transport(_providerID);
            reg.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Food reg = new Food(_providerID);
            reg.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Hotel reg = new Hotel(_providerID);
            reg.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            guide reg = new guide(_providerID);
            reg.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM AssignedServices WHERE ServiceProviderID = @providerid";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@providerid", _providerID);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int serviceId = -1;
                serviceId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["AssignedServiceID"].Value);

                if (serviceId == -1)
                {
                    MessageBox.Show("Please select a service to accept.");
                    return;
                }


                int providerId1 = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ServiceProviderID"].Value);

                MessageBox.Show($"Selected ProviderID: {providerId1}\nLogged-in ProviderID: {_providerID}");


                if (providerId1 != _providerID)
                {
                    MessageBox.Show("You can not update services provided by other serviceproviders.");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE AssignedServices SET ServiceProviderStatus = 'Available' WHERE AssignedServiceID = @ServiceID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ServiceID", serviceId);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Status updated to Available.");
                }
            }
        }



        private void button10_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {

                int serviceId = -1;
                serviceId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["AssignedServiceID"].Value);

                if (serviceId == -1)
                {
                    MessageBox.Show("Please select a service to accept.");
                    return;
                }

                int providerId2 = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ServiceProviderID"].Value);


                if (providerId2 != _providerID)
                {
                    MessageBox.Show("You can not update services provided by other serviceproviders.");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE AssignedServices SET ServiceProviderStatus = 'NotAvailable' WHERE AssignedServiceID = @ServiceID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ServiceID", serviceId);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Status updated to Available.");
                }
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            home hm = new home();
            hm.Show();
            this.Hide();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            home hm = new home();
            hm.Show();
            this.Hide();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            home hm = new home();
            hm.Show();
            this.Hide();
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            // 1. Validate selections
            if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null)
            {
                MessageBox.Show("Please select both Service Type and Metric.");
                return;
            }

            string serviceType = comboBox1.SelectedItem.ToString();   // e.g. "Hotel"
            string metric = comboBox2.SelectedItem.ToString();   // e.g. "Ratings"

            string sql = null;

            // 2. Choose your SQL based on selection
            switch (serviceType)
            {
                case "Transport":
                    switch (metric)
                    {
                        case "Reviews":
                            sql = @"
             SELECT t.TransportID, ReviewText AS Reviewtext
             FROM Review r
             JOIN Transport t ON r.TargetID = t.TransportID
             WHERE r.ServiceType = 'Transport'
             ";
                            break;
                        case "Ratings":
                            sql = @"
             SELECT t.TransportID,
                    CAST(AVG(r.Rating) AS DECIMAL(5,2)) AS AvgRating
             FROM Review r
             JOIN Transport t ON r.TargetID = t.TransportID
             WHERE r.ServiceType = 'Transport'
             GROUP BY t.TransportID";
                            break;
                        default:
                            MessageBox.Show($"{metric} not supported for Transport.");
                            return;
                    }
                    break;

                case "Hotel":
                    switch (metric)
                    {
                        case "Reviews":
                            sql = @"
             SELECT h.HotelID, ReviewText AS Reviewtext
             FROM Review r
             JOIN Hotel h ON r.TargetID = h.HotelID
             WHERE r.ServiceType = 'Hotel'
            ";
                            break;
                        case "Ratings":
                            sql = @"
             SELECT h.HotelID,
                    CAST(AVG(r.Rating) AS DECIMAL(5,2)) AS AvgRating
             FROM Review r
             JOIN Hotel h ON r.TargetID = h.HotelID
             WHERE r.ServiceType = 'Hotel'
             GROUP BY h.HotelID";
                            break;
                        case "Occupancy Rate":
                            sql = @"
             SELECT h.HotelID,
                    COUNT(a.BookingID) * 100.0 / NULLIF(h.RoomsAvailable + COUNT(a.BookingID),0) 
                      AS OccupancyRate
             FROM Hotel h
             LEFT JOIN AssignedServices a ON a.HotelProviderID = h.HotelID
             GROUP BY h.HotelID, h.RoomsAvailable";
                            break;
                        default:
                            MessageBox.Show($"{metric} not supported for Hotel.");
                            return;
                    }
                    break;

                case "Personal Guide":
                    switch (metric)
                    {
                        case "Reviews":
                            sql = @"
             SELECT g.GuideID, ReviewText AS Reviewtext
             FROM Review r
             JOIN PersonalGuide g ON r.TargetID = g.GuideID
             WHERE r.ServiceType = 'PersonalGuide'
             ";
                            break;
                        case "Ratings":
                            sql = @"
             SELECT g.GuideID,
                    CAST(AVG(r.Rating) AS DECIMAL(5,2)) AS AvgRating
             FROM Review r
             JOIN PersonalGuide g ON r.TargetID = g.GuideID
             WHERE r.ServiceType = 'PersonalGuide'
             GROUP BY g.GuideID";
                            break;
                        default:
                            MessageBox.Show($"{metric} not supported for PersonalGuide.");
                            return;
                    }
                    break;

                case "Food":
                    switch (metric)
                    {
                        case "Reviews":
                            sql = @"
             SELECT f.FoodID, ReviewText AS Reviewtext
             FROM Review r
             JOIN Food f ON r.TargetID = f.FoodID
             WHERE r.ServiceType = 'Food'
             ";
                            break;
                        case "Ratings":
                            sql = @"
             SELECT f.FoodID,
                    CAST(AVG(r.Rating) AS DECIMAL(5,2)) AS AvgRating
             FROM Review r
             JOIN Food f ON r.TargetID = f.FoodID
             WHERE r.ServiceType = 'Food'
             GROUP BY f.FoodID";
                            break;
                        default:
                            MessageBox.Show($"{metric} not supported for Food.");
                            return;
                    }
                    break;

                default:
                    MessageBox.Show("Unknown service type.");
                    return;
            }

            // 3. Run the query and bind to grid
            using (var conn = new SqlConnection(connectionString))
            using (var da = new SqlDataAdapter(sql, conn))
            {
                try
                {
                    var dt = new DataTable();
                    da.Fill(dt);
                    dataGridView2.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message);
                }
            }
        }
    }
}
