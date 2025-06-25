using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace dbproject
{
    public partial class ReviewForm : Form
    {
        Form previousForm;
        private int userID;
        string connectionString = "Data Source=DESKTOP-842J4RM\\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;Encrypt=False";

        public ReviewForm(Form callingForm, int userID)
        {
            InitializeComponent();
            previousForm = callingForm;
            this.userID = userID;
            this.Load += ReviewForm_Load;
        }
        private void ReviewForm_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Load all non-traveler user IDs
                //string query = "SELECT DISTINCT u.UserID\r\nFROM Users u\r\nWHERE u.UserID IN (\r\n    -- Tour Operators involved\r\n    SELECT topr.UserID\r\n    FROM BookingList b\r\n    JOIN Traveler t ON b.TravelerID = t.TravelerID\r\n    JOIN TourOperator topr ON b.OperatorID = topr.OperatorID\r\n    JOIN Trip tr ON b.TripID = tr.TripID\r\n    WHERE t.UserID = @UserID AND b.PaymentID IS NOT NULL AND tr.EndDate < GETDATE()\r\n\r\n   UNION\r\n\r\n    -- Hotel Providers\r\n    SELECT sp.UserID\r\n    FROM BookingList b\r\n    JOIN Traveler t ON b.TravelerID = t.TravelerID\r\n    JOIN Hotel h ON b.HotelProviderID = h.HotelID\r\n    JOIN ServiceProvider sp ON h.ProviderID = sp.ProviderID\r\n    JOIN Trip tr ON b.TripID = tr.TripID\r\n    WHERE t.UserID = @UserID AND b.PaymentID IS NOT NULL AND tr.EndDate < GETDATE()\r\n\r\n    UNION\r\n\r\n    -- Transport Providers\r\n    SELECT sp.UserID\r\n    FROM BookingList b\r\n    JOIN Traveler t ON b.TravelerID = t.TravelerID\r\n    JOIN Transport tpt ON b.TransportProviderID = tpt.TransportID\r\n    JOIN ServiceProvider sp ON tpt.ProviderID = sp.ProviderID\r\n    JOIN Trip tr ON b.TripID = tr.TripID\r\n    WHERE t.UserID = @UserID AND b.PaymentID IS NOT NULL AND tr.EndDate < GETDATE()\r\n\r\n    UNION\r\n\r\n    -- Food Providers\r\n    SELECT sp.UserID\r\n    FROM BookingList b\r\n    JOIN Traveler t ON b.TravelerID = t.TravelerID\r\n    JOIN Food f ON b.FoodProviderID = f.FoodID\r\n    JOIN ServiceProvider sp ON f.ProviderID = sp.ProviderID\r\n    JOIN Trip tr ON b.TripID = tr.TripID\r\n    WHERE t.UserID = @UserID AND b.PaymentID IS NOT NULL AND tr.EndDate < GETDATE()\r\n\r\n    UNION\r\n\r\n    -- Guide Providers\r\n    SELECT sp.UserID\r\n    FROM BookingList b\r\n    JOIN Traveler t ON b.TravelerID = t.TravelerID\r\n    JOIN PersonalGuide g ON b.GuideProviderID = g.GuideID\r\n    JOIN ServiceProvider sp ON g.ProviderID = sp.ProviderID\r\n    JOIN Trip tr ON b.TripID = tr.TripID\r\n    WHERE t.UserID = @UserID AND b.PaymentID IS NOT NULL AND tr.EndDate < GETDATE()\r\n)\r\n";
                string query = "SELECT DISTINCT u.UserID\r\nFROM Users u\r\nWHERE u.UserID IN (\r\n    -- Tour Operators involved\r\n    SELECT topr.UserID\r\n    FROM BookingList b\r\n    JOIN Traveler t ON b.TravelerID = t.TravelerID\r\n    JOIN TourOperator topr ON b.OperatorID = topr.OperatorID\r\n    JOIN Trip tr ON b.TripID = tr.TripID\r\n    WHERE t.UserID = @UserID AND b.PaymentID IS NOT NULL AND tr.EndDate < GETDATE()\r\n)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", userID); 
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    comboBox4.Items.Add(reader["UserID"].ToString());
                }
                reader.Close();
                
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            int authorID = userID;  // Passed from TravellerMain
            int targetID = Convert.ToInt32(comboBox4.SelectedItem);
            int rating = Convert.ToInt32(comboBox1.SelectedItem);
            int targetTypeID = Convert.ToInt32(comboBox2.SelectedItem);
            string reviewText = textBox4.Text.Trim();
            DateTime reviewDate = dateTimePicker1.Value;
            int statusID = 1; // Pending by default

            if (string.IsNullOrWhiteSpace(reviewText))
            {
                MessageBox.Show("Please enter review text.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string insertQuery = @"INSERT INTO Review 
            (AuthorUserID, TargetID, TargetTypeID, Rating, ReviewText, ReviewDate, StatusID)
            VALUES (@AuthorUserID, @TargetID, @TargetTypeID, @Rating, @ReviewText, @ReviewDate, @StatusID)";

                SqlCommand cmd = new SqlCommand(insertQuery, conn);
                cmd.Parameters.AddWithValue("@AuthorUserID", authorID);
                cmd.Parameters.AddWithValue("@TargetID", targetID);
                cmd.Parameters.AddWithValue("@TargetTypeID", targetTypeID);
                cmd.Parameters.AddWithValue("@Rating", rating);
                cmd.Parameters.AddWithValue("@ReviewText", reviewText);
                cmd.Parameters.AddWithValue("@ReviewDate", reviewDate);
                cmd.Parameters.AddWithValue("@StatusID", statusID);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Review submitted successfully (pending approval).");
               
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            previousForm.Show(); // Show TravellerMain again
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
