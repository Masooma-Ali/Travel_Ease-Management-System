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
    public partial class tripManagementForm : Form
    {
        private int currentOperatorId;
        public tripManagementForm()
        {
            InitializeComponent();
        }

        public tripManagementForm(int optid)
        {
            InitializeComponent();
            currentOperatorId = optid;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        private void tripManagementForm_Load(object sender, EventArgs e)
        {
            LoadCategories();
            LoadDestinations();
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            menuform mf = new menuform(currentOperatorId);
            mf.Show();
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connStr = @"Data Source=DESKTOP-842J4RM\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;Encrypt=False";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    // ===================
                    // 1. Get DestinationID (insert if new)
                    // ===================
                    int destinationId;
                    destinationId = Convert.ToInt32(comboBox2.SelectedValue);

                    // ===================
                    // 2. Get CategoryID (insert if new)
                    // ===================
                    int categoryId;
                    categoryId = Convert.ToInt32(comboBox1.SelectedValue);

                    // ===================
                    // 3. Calculate derived fields
                    // ===================
                    int duration = (dateTimePicker1.Value - dateTimePicker2.Value).Days;
                    decimal pricePerPerson = 5000; // or use logic based on destination/category
                    int totalSeats = Convert.ToInt32(textBox1.Text);
                    decimal totalAmount = pricePerPerson * totalSeats;

                    // ===================
                    // 4. Insert Trip
                    // ===================
                    string insertTripQuery = @"
                INSERT INTO Trip (
                    StartDate, EndDate,
                    TotalSeats, Duration, PricePerPerson,
                    totalamount, description,
                    DestinationID, categoryid, OperatorID
                )
                VALUES (
                     @StartDate, @EndDate,
                    @TotalSeats, @Duration, @PricePerPerson,
                    @TotalAmount, @Description,
                    @DestinationID, @CategoryID, @OperatorID
                );";

                    SqlCommand cmdTrip = new SqlCommand(insertTripQuery, conn);
                    cmdTrip.Parameters.AddWithValue("@StartDate", dateTimePicker2.Value);
                    cmdTrip.Parameters.AddWithValue("@EndDate", dateTimePicker1.Value);
                    cmdTrip.Parameters.AddWithValue("@TotalSeats", totalSeats);
                    cmdTrip.Parameters.AddWithValue("@Duration", duration);
                    cmdTrip.Parameters.AddWithValue("@PricePerPerson", pricePerPerson);
                    cmdTrip.Parameters.AddWithValue("@TotalAmount", totalAmount);
                    cmdTrip.Parameters.AddWithValue("@Description", textBox2.Text);
                    cmdTrip.Parameters.AddWithValue("@DestinationID", destinationId);
                    cmdTrip.Parameters.AddWithValue("@CategoryID", categoryId);
                    cmdTrip.Parameters.AddWithValue("@OperatorID", currentOperatorId); // make sure this is passed in constructor

                    int rowsAffected = cmdTrip.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        MessageBox.Show("No rows were inserted.");
                    }
                    else
                    {
                        MessageBox.Show("Trip created successfully!");
                    }

                    //cmdTrip.ExecuteNonQuery();

                   // MessageBox.Show("Trip created successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void LoadTrips()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-842J4RM\\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;Encrypt=False"))
                {
                    conn.Open();
                    string query = "SELECT * FROM Trip WHERE OperatorID = @OperatorID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@OperatorID", currentOperatorId);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading trips: " + ex.Message);
            }
        }


        private void LoadCategories()
        {
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-842J4RM\\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;Encrypt=False"))
            {
                conn.Open();
                string query = "SELECT CategoryID, CategoryName FROM TripCategory";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "CategoryName";
                comboBox1.ValueMember = "CategoryID";
                comboBox1.SelectedIndex = -1;
            }
        }

        private void LoadDestinations()
        {
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-842J4RM\\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;Encrypt=False"))
            {
                conn.Open();
                string query = "SELECT DestinationID, City FROM Destination";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                comboBox2.DataSource = dt;
                comboBox2.DisplayMember = "City";
                comboBox2.ValueMember = "DestinationID";
                comboBox2.SelectedIndex = -1;
            }
        }

        int selectedTripId = -1;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Columns.Contains("TripID"))
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                if (row.Cells["TripID"].Value != null)
                {
                    selectedTripId = Convert.ToInt32(row.Cells["TripID"].Value);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Columns.Contains("TripID"))
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                if (row.Cells["TripID"].Value != null)
                {
                    selectedTripId = Convert.ToInt32(row.Cells["TripID"].Value);
                    MessageBox.Show("Trip ID selected: " + selectedTripId);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (selectedTripId == -1)
            {
                MessageBox.Show("Please select a trip first.");
                return;
            }

            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-842J4RM\\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;Encrypt=False"))
            {
                conn.Open();

                List<string> updates = new List<string>();
                SqlCommand cmdUpdate = new SqlCommand();
                cmdUpdate.Connection = conn;

                // --- Start building only if fields are filled ---

                if (!string.IsNullOrWhiteSpace(comboBox2.Text)) // Destination
                {
                    string destCheck = "SELECT DestinationID FROM Destination WHERE City = @City";
                    using (SqlCommand cmd = new SqlCommand(destCheck, conn))
                    {
                        cmd.Parameters.AddWithValue("@City", comboBox2.Text);
                        object result = cmd.ExecuteScalar();
                        int destinationId;

                        if (result != null)
                        {
                            destinationId = Convert.ToInt32(result);
                            updates.Add("DestinationID = @DestinationID");
                            cmdUpdate.Parameters.AddWithValue("@DestinationID", destinationId);
                        }
                        else
                        {
                           
                        }

                      
                    }
                }

                if (!string.IsNullOrWhiteSpace(comboBox1.Text)) // Category
                {
                    string catCheck = "SELECT CategoryID FROM TripCategory WHERE CategoryName = @Category";
                    using (SqlCommand cmd = new SqlCommand(catCheck, conn))
                    {
                        cmd.Parameters.AddWithValue("@Category", comboBox1.Text);
                        object result = cmd.ExecuteScalar();
                        int categoryId;

                        if (result != null)
                        {
                            categoryId = Convert.ToInt32(result);
                            updates.Add("CategoryID = @CategoryID");
                            cmdUpdate.Parameters.AddWithValue("@CategoryID", categoryId);
                        }
                        else
                        {
                           
                        }

                        
                    }
                }

                if (!string.IsNullOrWhiteSpace(textBox1.Text) && int.TryParse(textBox1.Text, out int totalSeats))
                {
                    updates.Add("TotalSeats = @TotalSeats");
                    cmdUpdate.Parameters.AddWithValue("@TotalSeats", totalSeats);

                    decimal pricePerPerson = 5000; // Static
                    decimal totalAmount = pricePerPerson * totalSeats;
                    updates.Add("PricePerPerson = @PricePerPerson");
                    updates.Add("TotalAmount = @TotalAmount");

                    cmdUpdate.Parameters.AddWithValue("@PricePerPerson", pricePerPerson);
                    cmdUpdate.Parameters.AddWithValue("@TotalAmount", totalAmount);
                }

                if (!string.IsNullOrWhiteSpace(textBox2.Text)) // Description
                {
                    updates.Add("Description = @Description");
                    cmdUpdate.Parameters.AddWithValue("@Description", textBox2.Text);
                }

                // Handle dates and duration
                int duration = (dateTimePicker1.Value - dateTimePicker2.Value).Days;
                if (duration > 0)
                {
                    updates.Add("StartDate = @StartDate");
                    updates.Add("EndDate = @EndDate");
                    updates.Add("Duration = @Duration");

                    cmdUpdate.Parameters.AddWithValue("@StartDate", dateTimePicker2.Value);
                    cmdUpdate.Parameters.AddWithValue("@EndDate", dateTimePicker1.Value);
                    cmdUpdate.Parameters.AddWithValue("@Duration", duration);
                }

                if (updates.Count == 0)
                {
                    MessageBox.Show("No fields filled for update.");
                    return;
                }

                string updateQuery = $"UPDATE Trip SET {string.Join(", ", updates)} WHERE TripID = @TripID";
                cmdUpdate.CommandText = updateQuery;
                cmdUpdate.Parameters.AddWithValue("@TripID", selectedTripId);

                int rowsAffected = cmdUpdate.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Trip updated successfully!");
                    LoadTrips();
                }
                else
                {
                    MessageBox.Show("Update failed.");
                }
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadTrips();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (selectedTripId == -1)
            {
                MessageBox.Show("Please select a trip to delete.");
                return;
            }

            DialogResult confirm = MessageBox.Show("Are you sure you want to delete this trip?", "Confirm Delete", MessageBoxButtons.YesNo);
            if (confirm != DialogResult.Yes) return;

            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-842J4RM\\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;Encrypt=False"))
            {
                try
                {
                    conn.Open();

                    // 1. Delete from Bookings
                    string deleteBookings = "DELETE FROM Bookings WHERE TripID = @TripID";
                    using (SqlCommand cmd = new SqlCommand(deleteBookings, conn))
                    {
                        cmd.Parameters.AddWithValue("@TripID", selectedTripId);
                        cmd.ExecuteNonQuery();
                    }

                    // 2. Delete from TripHistory
                    string deleteHistory = "DELETE FROM TripHistory WHERE TripID = @TripID";
                    using (SqlCommand cmd = new SqlCommand(deleteHistory, conn))
                    {
                        cmd.Parameters.AddWithValue("@TripID", selectedTripId);
                        cmd.ExecuteNonQuery();
                    }

                    // 3. Delete from Trip
                    string deleteTrip = "DELETE FROM Trip WHERE TripID = @TripID";
                    using (SqlCommand cmd = new SqlCommand(deleteTrip, conn))
                    {
                        cmd.Parameters.AddWithValue("@TripID", selectedTripId);
                        int rows = cmd.ExecuteNonQuery();

                        if (rows > 0)
                        {
                            MessageBox.Show("Trip deleted successfully.");
                            LoadTrips(); // Refresh the grid
                            selectedTripId = -1;
                        }
                        else
                        {
                            MessageBox.Show("Trip deletion failed.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error during deletion: " + ex.Message);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
