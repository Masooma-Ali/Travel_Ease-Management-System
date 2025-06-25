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

namespace TOUROPERATOR_INTERFACE
{
    public partial class assignservices : Form
    {

        private int currentOperatorId;
        private int providerID;
        public assignservices(int optid)
        {
            InitializeComponent();
            currentOperatorId = optid;
            this.comboBox6.SelectedIndexChanged += new System.EventHandler(this.comboBox6_SelectedIndexChanged);
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            LoadTripIDs();
            LoadProviderIDs();
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int tripId = Convert.ToInt32(comboBox1.SelectedValue);
            LoadBookingIDs(tripId);
        }


        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox6.SelectedValue != null && int.TryParse(comboBox6.SelectedValue.ToString(), out int selectedProviderID))
            {
                providerID = selectedProviderID;
                LoadFoodOptions(providerID);
                LoadTransportOptions(providerID);
                LoadHotelOptions(providerID);
                LoadGuideOptions(providerID);
            }
        }


        private void LoadProviderIDs()
        {
            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-842J4RM\\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;Encrypt=False"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ProviderID FROM ServiceProvider", con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comboBox6.DisplayMember = "ProviderID";
                comboBox6.ValueMember = "ProviderID";
                comboBox6.DataSource = dt;
            }
        }


        private void LoadTripIDs()
        {
            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-842J4RM\\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;Encrypt=False"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT TripID FROM TRIP WHERE OperatorID = @optId", con);
                cmd.Parameters.AddWithValue("@optId", currentOperatorId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comboBox1.DisplayMember = "TripID";
                comboBox1.ValueMember = "TripID";
                comboBox1.DataSource = dt;
            }
        }

        private void LoadBookingIDs(int tripId)
        {
            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-842J4RM\\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;Encrypt=False"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(@"
            SELECT BookingID 
            FROM Bookings 
            WHERE TripID = @tripId 
              AND BookingID NOT IN (SELECT BookingID FROM AssignedServices)", con);

                cmd.Parameters.AddWithValue("@tripId", tripId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Trip not booked or all bookings already assigned.");
                    comboBox7.DataSource = null;
                }
                else
                {
                    comboBox7.DisplayMember = "BookingID";
                    comboBox7.ValueMember = "BookingID";
                    comboBox7.DataSource = dt;
                }
            }
        }




        private void LoadTransportOptions(int providerId)
        {
            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-842J4RM\\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;Encrypt=False"))
            {
                con.Open();
                providerID = Convert.ToInt32(comboBox6.SelectedValue);
                SqlCommand cmd = new SqlCommand("SELECT TransportID  FROM Transport WHERE ProviderID = @pid", con);
                cmd.Parameters.AddWithValue("@pid", providerId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                comboBox3.DisplayMember = "TransportID";
                comboBox3.ValueMember = "TransportID";
                comboBox3.DataSource = dt;
            }
        }

        private void LoadFoodOptions(int providerId)
        {
            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-842J4RM\\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;Encrypt=False"))
            {
                con.Open();
                providerID = Convert.ToInt32(comboBox6.SelectedValue);
                SqlCommand cmd = new SqlCommand("SELECT FoodID FROM Food WHERE ProviderID = @pid", con);
                cmd.Parameters.AddWithValue("@pid", providerId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                comboBox2.DisplayMember = "FoodID";
                comboBox2.ValueMember = "FoodID";
                comboBox2.DataSource = dt;
            }
        }

        private void LoadHotelOptions(int providerId)
        {
            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-842J4RM\\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;Encrypt=False"))
            {
                providerID = Convert.ToInt32(comboBox6.SelectedValue);
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT HotelID FROM Hotel WHERE ProviderID = @pid", con);
                cmd.Parameters.AddWithValue("@pid", providerId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                comboBox4.DisplayMember = "HotelID";
                comboBox4.ValueMember = "HotelID";
                comboBox4.DataSource = dt;
            }
        }

        private void LoadGuideOptions(int providerId)
        {
            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-842J4RM\\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;Encrypt=False"))
            {
                providerID = Convert.ToInt32(comboBox6.SelectedValue);
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT GuideID FROM PersonalGuide WHERE ProviderID = @pid", con);
                cmd.Parameters.AddWithValue("@pid", providerId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                comboBox5.DisplayMember = "GuideID";
                comboBox5.ValueMember = "GuideID";
                comboBox5.DataSource = dt;
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            menuform mf = new menuform(currentOperatorId);
            mf.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void assignservices_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int providerId = Convert.ToInt32(comboBox6.SelectedValue);
            int foodId = Convert.ToInt32(comboBox2.SelectedValue);
            int transportId = Convert.ToInt32(comboBox3.SelectedValue);
            int hotelId = Convert.ToInt32(comboBox4.SelectedValue);
            int guideId = Convert.ToInt32(comboBox5.SelectedValue);
            int bookingId = Convert.ToInt32(comboBox7.SelectedValue); // Make sure this comboBox holds BookingID!

            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-842J4RM\\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;Encrypt=False"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(@"
            INSERT INTO AssignedServices (
                ServiceProviderID, BookingID, HotelProviderID, TransportProviderID, 
                FoodProviderID, GuideProviderID, ServiceProviderStatus
            )
            VALUES (
                @ServiceProviderID, @BookingID, @HotelProviderID, @TransportProviderID,
                @FoodProviderID, @GuideProviderID, 'RequestedtoServiceProvider'
            )", con);

                cmd.Parameters.AddWithValue("@ServiceProviderID", providerId);
                cmd.Parameters.AddWithValue("@BookingID", bookingId);
                cmd.Parameters.AddWithValue("@HotelProviderID", hotelId);
                cmd.Parameters.AddWithValue("@TransportProviderID", transportId);
                cmd.Parameters.AddWithValue("@FoodProviderID", foodId);
                cmd.Parameters.AddWithValue("@GuideProviderID", guideId);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Service assigned successfully!");
                }
                else
                {
                    MessageBox.Show("Failed to assign service.");
                }
            }
        }
    }
}
