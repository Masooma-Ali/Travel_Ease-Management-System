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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TOUROPERATOR_INTERFACE;


namespace dbproject
{
    public partial class TravellerMaincs : Form
    {
        private int userID;
        private int selectedTripID = -1;
        string connectionString = "Data Source=DESKTOP-842J4RM\\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;Encrypt=False";

        public TravellerMaincs(int id)
        {
            InitializeComponent();
            this.Load += TravellerMaincs_Load;
            userID = id;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);

        }
        private void TravellerMaincs_Load(object sender, EventArgs e)
        {
            button7.Enabled = false;
            LoadTripHistory(); // Call your method
            LoadFilters(); // <-- Load dropdown values here
            LoadBookingsGrid();
            LoadCancelableBookings(userID);
            LoadUnpaidBookings(userID);

        }
        private void LoadBookingsGrid()
        {
            string query = @"
        SELECT 
            asv.AssignedServiceID, 
            b.BookingID, 
            asv.ServiceProviderID,
            b.TourOperatorResponse, 
            b.PaymentStatus, 
            b.TicketType
            
        FROM AssignedServices asv
        JOIN Bookings b ON asv.BookingID = b.BookingID
        JOIN Trip t ON b.TripID = t.TripID
        WHERE 
            b.TravelerID = @travelerID 
            AND t.StartDate >= CAST(GETDATE() AS DATE)";  // Only ongoing or future trips

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@travelerID", userID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView5.DataSource = dt;
            }
        }

        private void LoadFilters()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Load Destinations
                SqlDataAdapter da1 = new SqlDataAdapter("SELECT DestinationID, City FROM Destination", conn);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                comboBox1.DataSource = dt1;
                comboBox1.DisplayMember = "City";
                comboBox1.ValueMember = "DestinationID";
                comboBox1.SelectedIndex = -1;

                // Load Categories
                SqlDataAdapter da2 = new SqlDataAdapter("SELECT CategoryID, CategoryName FROM TripCategory", conn);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                comboBox2.DataSource = dt2;
                comboBox2.DisplayMember = "CategoryName";
                comboBox2.ValueMember = "CategoryID";
                comboBox2.SelectedIndex = -1;

                // Load available Group Sizes from Trip table
                SqlDataAdapter da3 = new SqlDataAdapter("SELECT DISTINCT TotalSeats AS GroupSize FROM Trip ORDER BY TotalSeats", conn);
                DataTable dt3 = new DataTable();
                da3.Fill(dt3);
                comboBox3.DataSource = dt3;
                comboBox3.DisplayMember = "GroupSize";
                comboBox3.ValueMember = "GroupSize";
                comboBox3.SelectedIndex = -1;

            }
        }

        private void LoadTripHistory()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT 
                B.BookingID,
                B.TripID,
                T.OperatorID AS TourOperatorID,
                B.TourOperatorResponse,
                ASV.AssignedServiceID,
                ASV.ServiceProviderID,
                ASV.HotelProviderID,
                ASV.TransportProviderID,
                ASV.FoodProviderID,
                ASV.GuideProviderID,
                ASV.ServiceProviderStatus,
                T.StartDate,
                T.EndDate
            FROM 
                Bookings B
            JOIN Trip T ON B.TripID = T.TripID
            JOIN AssignedServices ASV ON B.BookingID = ASV.BookingID
            WHERE 
                B.PaymentStatus = 'Paid'
                AND T.EndDate < GETDATE()
                AND B.TravelerID = @UserID
            ORDER BY 
                B.BookingID, ASV.AssignedServiceID;
        ";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", userID);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dataGridView2.DataSource = dt;

                comboBox6.Items.Clear();

                // Add unique IDs by category
                foreach (DataRow row in dt.Rows)
                {
                    AddItemIfNotNull(comboBox6, "Transport", row["TransportProviderID"]);
                    AddItemIfNotNull(comboBox6, "Guide", row["GuideProviderID"]);
                    AddItemIfNotNull(comboBox6, "Hotel", row["HotelProviderID"]);
                    AddItemIfNotNull(comboBox6, "Food", row["FoodProviderID"]);
                    AddItemIfNotNull(comboBox6, "Operator", row["TourOperatorID"]);
                    AddItemIfNotNull(comboBox6, "Provider", row["ServiceProviderID"]);
                    AddItemIfNotNull(comboBox6, "Trip", row["TripID"]);
                }
            }
        }

        // Helper method
        private void AddItemIfNotNull(System.Windows.Forms.ComboBox comboBox, string label, object value)
        {
            if (value != DBNull.Value)
            {
                string item = $"{label}-{value}";
                if (!comboBox.Items.Contains(item)) // Avoid duplicates
                    comboBox.Items.Add(item);
            }
        }
        






        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
           if (comboBox4.SelectedItem == null || comboBox6.SelectedItem == null || comboBox7.SelectedItem == null)
            {
                MessageBox.Show("Please select Role, Target ID, and Rating before submitting the review.");
                return;
            }

            string role = comboBox4.SelectedItem.ToString(); // 'TourOperator' or 'ServiceProvider'
            string selected = comboBox6.SelectedItem.ToString();
            int targetID = int.Parse(selected.Split('-')[1]);

            string serviceType = null;
            string transportPerformance = null;

            if (role == "ServiceProvider")
            {
                if (comboBox8.SelectedItem == null)
                {
                    MessageBox.Show("Please select the Service Type.");
                    return;
                }

               
                serviceType = comboBox8.SelectedItem.ToString();

                if (serviceType == "Transport")
                {
                    var result = MessageBox.Show("Was the transport on time?", "Transport Performance",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    transportPerformance = (result == DialogResult.Yes) ? "On-Time" : "Delayed";
                }
            }
            

                int rating = Convert.ToInt32(comboBox7.SelectedItem);
            string reviewText = string.IsNullOrWhiteSpace(textBox3.Text) ? null : textBox3.Text;
            int travelerID = userID;

            string query = @"
        INSERT INTO Review (TravelerID, TargetRole, TargetID, ServiceType, Transportperformance, Rating, ReviewText)
        VALUES (@TravelerID, @TargetRole, @TargetID, @ServiceType, @Transportperformance, @Rating, @ReviewText);";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TravelerID", travelerID);
                cmd.Parameters.AddWithValue("@TargetRole", role);
                cmd.Parameters.AddWithValue("@TargetID", targetID);
                cmd.Parameters.AddWithValue("@ServiceType", (object)serviceType ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Transportperformance", (object)transportPerformance ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Rating", rating);
                cmd.Parameters.AddWithValue("@ReviewText", (object)reviewText ?? DBNull.Value);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Review submitted successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error submitting review: " + ex.Message);
                }
            }
        

        }


        private void button5_Click(object sender, EventArgs e)
        {
            home hm = new home();
            hm.Show();
            this.Hide();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder query = new StringBuilder(@"
        SELECT Trip.TripID, Destination.City, Destination.Country, TripCategory.CategoryName,
               Trip.StartDate, Trip.EndDate, Trip.PricePerPerson, Trip.TotalSeats, Trip.duration, Trip.description
        FROM Trip
        JOIN Destination ON Trip.DestinationID = Destination.DestinationID
        JOIN TripCategory ON Trip.CategoryID = TripCategory.CategoryID
        WHERE 1 = 1");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if (checkBox1.Checked && comboBox1.SelectedValue != null)
                {
                    query.Append(" AND Trip.DestinationID = @DestinationID");
                    cmd.Parameters.AddWithValue("@DestinationID", comboBox1.SelectedValue);
                }

                if (checkBox2.Checked && comboBox2.SelectedValue != null)
                {
                    query.Append(" AND Trip.CategoryID = @CategoryID");
                    cmd.Parameters.AddWithValue("@CategoryID", comboBox2.SelectedValue);
                }

                if (checkBox3.Checked && checkBox4.Checked)
                {
                    query.Append(" AND Trip.StartDate >= @StartDate AND Trip.EndDate <= @EndDate");
                    cmd.Parameters.AddWithValue("@StartDate", dateTimePicker1.Value.Date);
                    cmd.Parameters.AddWithValue("@EndDate", dateTimePicker2.Value.Date);
                }
                else if (checkBox3.Checked)
                {
                    query.Append(" AND Trip.StartDate = @StartDate");
                    cmd.Parameters.AddWithValue("@StartDate", dateTimePicker1.Value.Date);
                }
                else if (checkBox4.Checked)
                {
                    query.Append(" AND Trip.EndDate = @EndDate");
                    cmd.Parameters.AddWithValue("@EndDate", dateTimePicker2.Value.Date);
                }
                

                if (checkBox5.Checked && comboBox3.SelectedValue != null)
                {
                    query.Append(" AND Trip.TotalSeats = @GroupSize");
                    cmd.Parameters.AddWithValue("@GroupSize", comboBox3.SelectedValue);
                   // MessageBox.Show("GroupSize Selected: " + comboBox3.SelectedValue.ToString());

                }

                if (checkBox6.Checked)
                {
                    decimal minPrice, maxPrice;

                    if (decimal.TryParse(numericUpDown1.Text, out minPrice))
                    {
                        query.Append(" AND Trip.PricePerPerson >= @MinPrice");
                        cmd.Parameters.AddWithValue("@MinPrice", minPrice);
                    }

                    if (decimal.TryParse(numericUpDown2.Text, out maxPrice))
                    {
                        query.Append(" AND Trip.PricePerPerson <= @MaxPrice");
                        cmd.Parameters.AddWithValue("@MaxPrice", maxPrice);
                    }
                }

                cmd.CommandText = query.ToString();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;

            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;

            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.Value = DateTime.Today;

            numericUpDown1.Text = "";
            numericUpDown2.Text = "";

            dataGridView1.DataSource = null;

        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
           // MessageBox.Show("GroupSize Selected: " + comboBox3.SelectedValue.ToString());
        }
        private void LoadCancelableBookings(int travelerID)
        {
           /* comboBox10.Items.Clear();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT a.AssignedServiceID
            FROM AssignedServices a
            JOIN Bookings b ON a.BookingID = b.BookingID
            WHERE b.TravelerID = @TravelerID
              AND b.TourOperatorResponse != 'Cancelled'";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TravelerID", travelerID);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBox10.Items.Add(reader["AssignedServiceID"].ToString());
                    }
                }
            }
           */
        }

        private void LoadUnpaidBookings(int travelerID)
        {
            
            /*comboBox11.Items.Clear();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT a.AssignedServiceID
            FROM AssignedServices a
            JOIN Bookings b ON a.BookingID = b.BookingID
            WHERE b.TravelerID = @TravelerID
              AND b.PaymentStatus = 'Unpaid'
              AND b.TourOperatorResponse != 'Cancelled'";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TravelerID", travelerID);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                        comboBox11.Items.Add(reader["AssignedServiceID"].ToString());
                }
            }
            */
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (selectedTripID == -1)
            {
                MessageBox.Show("Please select a trip from the list first.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Step 1: Get TravelerID from userID
                int travelerID = -1;
                string getTravelerIdQuery = "SELECT TravelerID FROM Traveler WHERE UserID = @UserID";
                using (SqlCommand cmd = new SqlCommand(getTravelerIdQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        travelerID = Convert.ToInt32(result);
                    }
                    else
                    {
                        MessageBox.Show("Traveler profile not found.");
                        return;
                    }
                }

                // Step 2: Insert into Bookings table
                string insertQuery = @"
            INSERT INTO Bookings 
            (TripID, TravelerID, BookingDate, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType)
            VALUES 
            (@TripID, @TravelerID, GETDATE(), NULL, @Response, @PaymentStatus, @TicketType)";

                using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                {
                    insertCmd.Parameters.AddWithValue("@TripID", selectedTripID);
                    insertCmd.Parameters.AddWithValue("@TravelerID", travelerID);
                    insertCmd.Parameters.AddWithValue("@Response", "Pending");
                    insertCmd.Parameters.AddWithValue("@PaymentStatus", "Unpaid");
                    insertCmd.Parameters.AddWithValue("@TicketType", "not paid"); // default status before payment

                    try
                    {
                        int rowsAffected = insertCmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Trip successfully added to your bookings!");
                            LoadBookingsGrid();
                            LoadCancelableBookings(userID);
                            LoadUnpaidBookings(userID);
                            selectedTripID = -1;
                        }
                        else
                        {
                            MessageBox.Show("Failed to add trip to bookings.");
                        }
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 2627) // Unique constraint violation
                            MessageBox.Show("This trip is already in your bookings.");
                        else
                            MessageBox.Show("Database error: " + ex.Message);
                    }
                }
               
            }
        }




        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                selectedTripID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["TripID"].Value);
                MessageBox.Show("Trip added to cart: " + selectedTripID);
            }
            else
            {
                MessageBox.Show("Please select a trip first.");
            }
        }


        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                selectedTripID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["TripID"].Value);
                // Optional: Display selected ID
               // MessageBox.Show("Trip selected: " + selectedTripID);
            }
        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /*
        private void button6_Click(object sender, EventArgs e)
        {
            if (comboBox10.SelectedItem == null)
            {
                MessageBox.Show("Please select a service to cancel its booking.");
                return;
            }

            int assignedServiceID = Convert.ToInt32(comboBox10.SelectedItem);
            int bookingID = -1;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Get BookingID from AssignedServiceID
                string lookupQuery = "SELECT BookingID FROM AssignedServices WHERE AssignedServiceID = @AssignedServiceID";
                using (SqlCommand lookupCmd = new SqlCommand(lookupQuery, conn))
                {
                    lookupCmd.Parameters.AddWithValue("@AssignedServiceID", assignedServiceID);
                    var result = lookupCmd.ExecuteScalar();
                    if (result != null)
                        bookingID = Convert.ToInt32(result);
                    else
                    {
                        MessageBox.Show("Invalid AssignedServiceID.");
                        return;
                    }
                }

                // Check if already cancelled
                string checkQuery = "SELECT TourOperatorResponse FROM Bookings WHERE BookingID = @BookingID";
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@BookingID", bookingID);
                    string status = checkCmd.ExecuteScalar()?.ToString();

                    if (status == "Cancelled")
                    {
                        MessageBox.Show("This booking is already cancelled.");
                        return;
                    }
                }

                // Cancel the booking
                string updateQuery = @"
            UPDATE Bookings 
            SET TourOperatorResponse = 'Cancelled', OperatorResponseDate = GETDATE()
            WHERE BookingID = @BookingID";

                using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@BookingID", bookingID);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Booking cancelled successfully.");

                // Reload updated data
                LoadBookingsGrid();
                LoadCancelableBookings(userID);
                LoadUnpaidBookings(userID);
            }
        }
        */
        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView5.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a service to cancel.");
                return;
            }

            int bookingID = Convert.ToInt32(dataGridView5.SelectedRows[0].Cells["BookingID"].Value);
            int serviceID = Convert.ToInt32(dataGridView5.SelectedRows[0].Cells["AssignedServiceID"].Value);
            string currentStatus = dataGridView5.SelectedRows[0].Cells["TourOperatorResponse"].Value.ToString();
           // int serviceProviderID = Convert.ToInt32(dataGridView5.SelectedRows[0].Cells["ServiceProviderID"].Value);
            if (currentStatus == "Cancelled")
            {
                MessageBox.Show("This booking is already cancelled.");
                return;
            }

            string query = @"
            UPDATE Bookings
            SET TourOperatorResponse = 'Cancelled', OperatorResponseDate = GETDATE()
            WHERE BookingID = @bookingID AND TravelerID = @userID
              AND EXISTS (
                  SELECT 1 FROM AssignedServices
                  WHERE AssignedServiceID = @serviceID
                    AND BookingID = Bookings.BookingID
                    
                    
              );";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@bookingID", bookingID);
                cmd.Parameters.AddWithValue("@serviceID", serviceID);
                cmd.Parameters.AddWithValue("@userID", userID);
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            LoadBookingsGrid();
        }


        /*private void button7_Click(object sender, EventArgs e)
        {
            if (comboBox11.SelectedItem == null)
            {
                MessageBox.Show("Please select a service (AssignedServiceID) to proceed with payment.");
                return;
            }

            if (comboBox5.SelectedItem == null)
            {
                MessageBox.Show("Please select a ticket type (Digital or Physical).");
                return;
            }

            int assignedServiceID;
            if (!int.TryParse(comboBox11.SelectedItem.ToString(), out assignedServiceID))
            {
                MessageBox.Show("Invalid service selection.");
                return;
            }

            string ticketType = comboBox5.SelectedItem.ToString();
            int bookingID = -1;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Lookup BookingID
                    string lookupQuery = "SELECT BookingID FROM AssignedServices WHERE AssignedServiceID = @AssignedServiceID";
                    using (SqlCommand lookupCmd = new SqlCommand(lookupQuery, conn))
                    {
                        lookupCmd.Parameters.AddWithValue("@AssignedServiceID", assignedServiceID);
                        object result = lookupCmd.ExecuteScalar();

                        if (result == null)
                        {
                            MessageBox.Show("Assigned service not found. Please verify the selection.");
                            return;
                        }

                        bookingID = Convert.ToInt32(result);
                    }

                    // Validate current Booking status
                    string statusQuery = @"SELECT PaymentStatus, TourOperatorResponse FROM Bookings WHERE BookingID = @BookingID";
                    using (SqlCommand statusCmd = new SqlCommand(statusQuery, conn))
                    {
                        statusCmd.Parameters.AddWithValue("@BookingID", bookingID);
                        using (SqlDataReader reader = statusCmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string payment = reader["PaymentStatus"].ToString();
                                string response = reader["TourOperatorResponse"].ToString();

                                if (response == "Cancelled")
                                {
                                    MessageBox.Show("Cannot process payment for a cancelled booking.");
                                    return;
                                }

                                if (payment == "Paid")
                                {
                                    MessageBox.Show("Booking already paid. No further action needed.");
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Booking not found for the selected service.");
                                return;
                            }
                        }
                    }

                    // Mark as Paid
                    string payQuery = @"UPDATE Bookings SET PaymentStatus = 'Paid' WHERE BookingID = @BookingID";
                    using (SqlCommand payCmd = new SqlCommand(payQuery, conn))
                    {
                        payCmd.Parameters.AddWithValue("@BookingID", bookingID);
                        payCmd.ExecuteNonQuery();
                    }

                    // Update TicketType
                    string ticketQuery = @"UPDATE Bookings 
                                   SET TicketType = @TicketType 
                                   WHERE BookingID = @BookingID AND PaymentStatus = 'Paid'";
                    using (SqlCommand ticketCmd = new SqlCommand(ticketQuery, conn))
                    {
                        ticketCmd.Parameters.AddWithValue("@BookingID", bookingID);
                        ticketCmd.Parameters.AddWithValue("@TicketType", ticketType);
                        ticketCmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("✔️ Payment successful and ticket type updated.");
                    LoadBookingsGrid();
                    LoadCancelableBookings(userID);
                    LoadUnpaidBookings(userID);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database error occurred:\n" + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error:\n" + ex.Message);
            }
        }
        */
        private void button7_Click(object sender, EventArgs e)
        {
            if (dataGridView5.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a service to pay.");
                return;
            }

            if (comboBox5.SelectedItem == null)
            {
                MessageBox.Show("Please select a ticket type.");
                return;
            }

            int bookingID = Convert.ToInt32(dataGridView5.SelectedRows[0].Cells["BookingID"].Value);
            int serviceID = Convert.ToInt32(dataGridView5.SelectedRows[0].Cells["AssignedServiceID"].Value);
            string status = dataGridView5.SelectedRows[0].Cells["TourOperatorResponse"].Value.ToString();
            string paymentStatus = dataGridView5.SelectedRows[0].Cells["PaymentStatus"].Value.ToString();
            int serviceProviderID = Convert.ToInt32(dataGridView5.SelectedRows[0].Cells["ServiceProviderID"].Value);

            if (status == "Cancelled")
            {
                MessageBox.Show("This booking is cancelled and cannot be paid.");
                return;
            }

            if (status != "Reserved" || paymentStatus != "Unpaid")
            {
                MessageBox.Show("Only reserved and unpaid bookings can be paid.");
                return;
            }

            string ticketType = comboBox5.SelectedItem.ToString();

            string query = @"
    UPDATE Bookings
    SET PaymentStatus = 'Paid', TicketType = @ticketType
    WHERE BookingID = @bookingID
      AND TravelerID = @userID
      AND EXISTS (
          SELECT 1
          FROM AssignedServices AS A
          INNER JOIN Trip T ON A.TripID = T.TripID
          WHERE A.AssignedServiceID = @serviceID
            AND A.BookingID = Bookings.BookingID
            AND A.ServiceProviderID = @serviceProviderID
            AND T.StartDate > GETDATE()
      );";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@bookingID", bookingID);
                cmd.Parameters.AddWithValue("@serviceID", serviceID);
                cmd.Parameters.AddWithValue("@ticketType", ticketType);
                cmd.Parameters.AddWithValue("@userID", userID);
                cmd.Parameters.AddWithValue("@serviceProviderID", serviceProviderID);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Payment successful!");
                }
                else
                {
                    MessageBox.Show("Trip has already started or other condition failed. Payment not allowed.");
                }
            }

            LoadBookingsGrid();
        }







        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void UpdateButton7State()
        {
            button7.Enabled = comboBox5.SelectedItem != null;
        }

        private void comboBox11_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButton7State();
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TravellerMaincs_Load_1(object sender, EventArgs e)
        {

        }
    }
}
