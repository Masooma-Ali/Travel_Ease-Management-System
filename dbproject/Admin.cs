using allinterfaces;
using db_f;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace dbproject
{
    public partial class Admin : Form
    {
        string connectionString = "Data Source=DESKTOP-842J4RM\\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;Encrypt=False";

        public Admin()
        {
            InitializeComponent();
        }
            private void button12_Click(object sender, EventArgs e)
            {
                string categoryName = textBox1.Text.Trim();

                if (string.IsNullOrEmpty(categoryName))
                {
                    MessageBox.Show("Please enter a category name.");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Check if the category already exists
                    string checkQuery = "SELECT COUNT(*) FROM TripCategory WHERE CategoryName = @Name";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@Name", categoryName);

                    int exists = (int)checkCmd.ExecuteScalar();

                    if (exists > 0)
                    {
                        MessageBox.Show("This category already exists.");
                        return;
                    }

                    // Insert the new category
                    string insertQuery = "INSERT INTO TripCategory (CategoryName) VALUES (@Name)";
                    SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                    insertCmd.Parameters.AddWithValue("@Name", categoryName);

                    int rows = insertCmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Category added successfully.");
                        textBox1.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Failed to add category.");
                    }
                }
            }

            private void button3_Click(object sender, EventArgs e)
            {

            }

            private void label3_Click(object sender, EventArgs e)
            {

            }

            private void textBox3_TextChanged(object sender, EventArgs e)
            {

            }

            private void button2_Click(object sender, EventArgs e)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM TripCategory";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }

            private void button5_Click(object sender, EventArgs e)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Review";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView2.DataSource = dt;
                }
            }


            private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
            {

            }


            private void button6_Click(object sender, EventArgs e)
            {
                if (dataGridView2.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a review to update.");
                    return;
                }

                if (comboBox1.SelectedItem == null)
                {
                    MessageBox.Show("Please select a status to update.");
                    return;
                }

                string selectedStatus = comboBox1.SelectedItem.ToString();
                int reviewId = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells["ReviewID"].Value);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE Review SET AdminResponnse = @Status WHERE ReviewID = @ReviewID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Status", selectedStatus);
                    cmd.Parameters.AddWithValue("@ReviewID", reviewId);
                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Review status updated successfully.");
                        // Refresh grid
                        button5_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Failed to update review status.");
                    }
                }
            }

            private void button3_Click_1(object sender, EventArgs e)
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

            private void button1_Click(object sender, EventArgs e)
            {
                string catIdText = textBox3.Text.Trim();
                string catNameText = textBox1.Text.Trim();

                if (!int.TryParse(catIdText, out int categoryId) || string.IsNullOrEmpty(catNameText))
                {
                    MessageBox.Show("Please enter a valid Category ID and Category Name.");
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Step 0: Check if Category exists
                    string checkQuery = "SELECT COUNT(*) FROM TripCategory WHERE CategoryID = @CatID AND CategoryName = @CatName";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@CatID", categoryId);
                        checkCmd.Parameters.AddWithValue("@CatName", catNameText);

                        int exists = (int)checkCmd.ExecuteScalar();

                        if (exists == 0)
                        {
                            MessageBox.Show("Category does not exist with the provided ID and Name.");
                            return;
                        }
                    }

                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        // Step 1: Get all TripIDs under this Category
                        string getTripsQuery = "SELECT TripID FROM Trip WHERE CategoryID = @CatID";
                        SqlCommand getTripsCmd = new SqlCommand(getTripsQuery, conn, transaction);
                        getTripsCmd.Parameters.AddWithValue("@CatID", categoryId);

                        List<int> tripIds = new List<int>();
                        using (SqlDataReader reader = getTripsCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                tripIds.Add(reader.GetInt32(0));
                            }
                        }

                        foreach (int tripId in tripIds)
                        {
                            // Step 2: Get BookingIDs for each Trip
                            string getBookingsQuery = "SELECT BookingID FROM Bookings WHERE TripID = @TripID";
                            SqlCommand getBookingsCmd = new SqlCommand(getBookingsQuery, conn, transaction);
                            getBookingsCmd.Parameters.AddWithValue("@TripID", tripId);

                            List<int> bookingIds = new List<int>();
                            using (SqlDataReader reader = getBookingsCmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    bookingIds.Add(reader.GetInt32(0));
                                }
                            }

                            // Step 3: Delete from AssignedServices
                            foreach (int bookingId in bookingIds)
                            {
                                string deleteAssigned = "DELETE FROM AssignedServices WHERE BookingID = @BID";
                                SqlCommand deleteAssignedCmd = new SqlCommand(deleteAssigned, conn, transaction);
                                deleteAssignedCmd.Parameters.AddWithValue("@BID", bookingId);
                                deleteAssignedCmd.ExecuteNonQuery();
                            }

                            // Step 4: Delete Bookings
                            string deleteBookings = "DELETE FROM Bookings WHERE TripID = @TripID";
                            SqlCommand deleteBookingsCmd = new SqlCommand(deleteBookings, conn, transaction);
                            deleteBookingsCmd.Parameters.AddWithValue("@TripID", tripId);
                            deleteBookingsCmd.ExecuteNonQuery();

                            // Step 5: Delete TripHistory
                            string deleteTripHistory = "DELETE FROM TripHistory WHERE TripID = @TripID";
                            SqlCommand deleteTripHistoryCmd = new SqlCommand(deleteTripHistory, conn, transaction);
                            deleteTripHistoryCmd.Parameters.AddWithValue("@TripID", tripId);
                            deleteTripHistoryCmd.ExecuteNonQuery();

                            // Step 6: Delete Trip
                            string deleteTrip = "DELETE FROM Trip WHERE TripID = @TripID";
                            SqlCommand deleteTripCmd = new SqlCommand(deleteTrip, conn, transaction);
                            deleteTripCmd.Parameters.AddWithValue("@TripID", tripId);
                            deleteTripCmd.ExecuteNonQuery();
                        }

                        // Step 7: Delete Category
                        string deleteCategory = "DELETE FROM TripCategory WHERE CategoryID = @CatID";
                        SqlCommand deleteCatCmd = new SqlCommand(deleteCategory, conn, transaction);
                        deleteCatCmd.Parameters.AddWithValue("@CatID", categoryId);
                        int rowsAffected = deleteCatCmd.ExecuteNonQuery();

                        transaction.Commit();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Category and all related records deleted successfully.");
                        }
                        else
                        {
                            MessageBox.Show("Category not found.");
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Error during deletion: " + ex.Message);
                    }
                }
            }

            private void tabPage2_Click(object sender, EventArgs e)
            {

            }

            private void Admin_Load(object sender, EventArgs e)
            {

                //  this.reportViewer1.RefreshReport();
                // this.reportViewer1.RefreshReport();
            }

            private void tabPage3_Click(object sender, EventArgs e)
            {

            }

            private void tabPage1_Click(object sender, EventArgs e)
            {
                if (tabControl1.SelectedTab == tabPage1) // Replace with your actual tab name
                {
                    // LoadEfficiencyReport(); // Load when that tab is selected
                }
            }
        /*
                private void LoadEfficiencyReport()
                {
                    // === Create DataTables (from servicereportdataset.xsd) ===
                    // This refers to the dataset defined in your .xsd file
                    var hotelData = new servicereportdataset.HotelDataTable();             // Table for hotel data
                    var transportData = new servicereportdataset.ReviewDataTable();     // Table for transport data
                    var guideData = new servicereportdataset.ServiceProviderDataTable();             // Table for guide data
                    var serviceUtilizeData = new servicereportdataset.AssignedServicesDataTable(); // Table for service utilization

                    // === Create TableAdapters ===
                    // These connect to your database and fill the tables
                    var hotelAdapter = new db_f.servicereportdatasetTableAdapters.HotelTableAdapter();
                    var transportAdapter = new db_f.servicereportdatasetTableAdapters.ReviewTableAdapter();
                    var guideAdapter = new db_f.servicereportdatasetTableAdapters.ServiceProviderTableAdapter();
                    var serviceUtilizeAdapter = new db_f.servicereportdatasetTableAdapters.AssignedServicesTableAdapter();

                    // === Fill DataTables with data from the database ===
                    hotelAdapter.Fill(hotelData);
                    transportAdapter.Fill(transportData);
                    guideAdapter.Fill(guideData);
                    serviceUtilizeAdapter.Fill(serviceUtilizeData);


                    // === Set RDLC path (must be in bin\Debug)
                    string path = "serviceproviderefficiencyreport.rdlc";
                    if (!System.IO.File.Exists(path))
                    {
                        MessageBox.Show("RDLC file not found.");
                        return;
                    }

                    // === Clear any existing data sources from the ReportViewer ===
                    reportViewer1.LocalReport.DataSources.Clear();

                    // === Add the datasets to the ReportViewer ===
                    // These names MUST MATCH the dataset names defined in your .rdlc file
                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Hoteldata", hotelData.AsEnumerable()));
                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("transportdata", transportData.AsEnumerable()));
                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("guidedata", guideData.AsEnumerable()));
                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("serviceutilize", serviceUtilizeData.AsEnumerable()));

                    // === Set the RDLC report file path ===
                    // This should be the name of your .rdlc file (set its "Copy to Output Directory" property to "Copy if newer")
                    reportViewer1.LocalReport.ReportPath = "serviceproviderefficiencyreport.rdlc";

                    // === Refresh the report to load everything ===
                    reportViewer1.RefreshReport();

                }*/

        /*  private void button4_Click(object sender, EventArgs e)
          {
              Reportforms hm = new Reportforms();
              hm.Show();
              this.Hide();
          }

          private void button7_Click(object sender, EventArgs e)
          {
              platformreportview hm = new platformreportview();
              hm.Show();
              this.Hide();
          }

          private void button8_Click(object sender, EventArgs e)
          {
              paymentreportview hm = new paymentreportview();
              hm.Show();
              this.Hide();
          }

          private void button9_Click(object sender, EventArgs e)
          {
              Audit hm = new Audit();
              hm.Show();
              this.Hide();
          }*/

        private void button11_Click(object sender, EventArgs e)
        {
           abandonedBookings ab = new abandonedBookings();
            ab.Show();
            this.Hide();
        }
        private void button9_Click(object sender, EventArgs e)
        {
            TravellerReportViewer hm = new TravellerReportViewer();
            hm.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            TripReportViewer hm = new TripReportViewer();
            hm.Show();
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {
            Audit hm = new Audit();
            hm.Show();
            this.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
           paymentreportview paymentreportview = new paymentreportview();
            paymentreportview.Show();
            this.Hide();
          
        }

        private void button13_Click(object sender, EventArgs e)
        {
            platformreportview hm = new platformreportview();
            hm.Show();
            this.Hide();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Reportforms reportforms = new Reportforms();  
            reportforms.Show();
            this.Hide();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            destinationreport dr = new destinationreport(); 
            dr.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            operatorreview operatorreview = new operatorreview();
            operatorreview.Show();
            this.Hide();
        }
    }
}







/*private void button9_Click(object sender, EventArgs e)
        {
            TravellerReportViewer hm = new TravellerReportViewer();
            hm.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            TripReportViewer hm = new TripReportViewer();
            hm.Show();
            this.Hide();
        }

 
 
        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

          private void Admin_Load(object sender, EventArgs e)
        {

            //this.reportViewer1.RefreshReport();
        }

        private void button11_Click(object sender, EventArgs e)
        {

        }*/