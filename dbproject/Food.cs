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
    public partial class Food : Form
    {
        string connectionString = "Data Source=DESKTOP-842J4RM\\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;Encrypt=False";
        private int _providerID;

        public Food(int providerID)
        {
            InitializeComponent();
            _providerID = providerID;
            LoadCuisineTypes();
            LoadFoodData();

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            service_mainpage reg = new service_mainpage(_providerID);
            reg.Show();
            this.Hide();
        }

        private void AddServiceTypeForProvider()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string checkQuery = @"SELECT COUNT(*) FROM ProviderServiceTypes 
                              WHERE ProviderID = @ProviderID AND ServiceType = @ServiceType";

                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@ProviderID", _providerID);
                checkCmd.Parameters.AddWithValue("@ServiceType", "Food");

                try
                {
                    conn.Open();
                    int count = (int)checkCmd.ExecuteScalar();

                    if (count == 0) // Only insert if not already present
                    {
                        string insertQuery = @"INSERT INTO ProviderServiceTypes (ProviderID, ServiceType)
                                       VALUES (@ProviderID, @ServiceType)";
                        SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                        insertCmd.Parameters.AddWithValue("@ProviderID", _providerID);
                        insertCmd.Parameters.AddWithValue("@ServiceType", "Food");

                        insertCmd.ExecuteNonQuery();
                    }

                    conn.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
          
        // Get halal option
        int isHalal = -1;
              if (radioButton4.Checked)
                  isHalal = 1;
              else if (radioButton3.Checked)
                  isHalal = 0;
              else
              {
                  MessageBox.Show("Please select whether the food is Halal.");
                  return;
              }

              // Get Veg option
              int isveg = -1;
              if (radioButton1.Checked)
                  isveg = 1;
              else if (radioButton2.Checked)
                  isveg = 0;
              else
              {
                  MessageBox.Show("Please select whether the Vegeterian food is available or not.");
                  return;
              }

              // Get Del option
              int isdel = -1;
              if (radioButton6.Checked)
                  isdel = 1;
              else if (radioButton5.Checked)
                  isdel = 0;
              else
              {
                  MessageBox.Show("Please select whether Food Delivery is available or not.");
                  return;
              }

              // Insert into DB
              try
              {

                  using (SqlConnection conn = new SqlConnection(connectionString))
                  {
                      string query = "INSERT INTO Food (ProviderID,CuisineType, HalalAvailable,VegAvailable,DeliveryAvailable) " +
                        "VALUES (@pid,@CuisineType, @IsHalal,@Isveg,@Isdel)";

                      using (SqlCommand cmd = new SqlCommand(query, conn))
                      {
                        cmd.Parameters.AddWithValue("@pid", _providerID);
                        cmd.Parameters.AddWithValue("@CuisineType", comboBox1.SelectedItem);
                          cmd.Parameters.AddWithValue("@IsHalal", isHalal);
                          cmd.Parameters.AddWithValue("@Isveg", isveg);
                          cmd.Parameters.AddWithValue("@Isdel", isdel);


                        conn.Open();
                          int rows = cmd.ExecuteNonQuery();
                          conn.Close();

                          if (rows > 0)
                          {
                              MessageBox.Show("Food record added successfully.");
                            AddServiceTypeForProvider();
                            LoadFoodData();
                            ClearForm();
                          }
                          else
                          {
                              MessageBox.Show("Failed to insert data.");
                          }
                      }
                  }
              }
              catch (Exception ex)
              {
                  MessageBox.Show("Error: " + ex.Message);
              }
        }
     
        private void Food_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder query = new StringBuilder("SELECT * FROM Food WHERE 1=1");
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;


                // Filter by CuisineType
                if (comboBox1.SelectedItem != null)
                {
                    query.Append(" AND CuisineType = @CuisineType");
                    cmd.Parameters.AddWithValue("@CuisineType", comboBox1.SelectedItem.ToString());
                }

                if (radioButton4.Checked)
                {
                    query.Append(" AND HalalAvailable = 1");
                }
                else if (radioButton3.Checked)
                {
                    query.Append(" AND HalalAvailable = 0");
                }

                // Filter by VegAvailable
                if (radioButton2.Checked)
                {
                    query.Append(" AND VegAvailable = 1");
                }
                else if (radioButton1.Checked)
                {
                    query.Append(" AND VegAvailable = 0");
                }

                // Filter by DeliveryAvailable
                if (radioButton6.Checked)
                {
                    query.Append(" AND DeliveryAvailable = 1");
                }
                else if (radioButton5.Checked)
                {
                    query.Append(" AND DeliveryAvailable = 0");
                }


                // Filter by FoodID
                if (!string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    query.Append(" AND FoodID = @FoodID");
                    cmd.Parameters.AddWithValue("@FoodID", textBox3.Text.Trim());
                }
                cmd.CommandText = query.ToString();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;

                if (dt.Rows.Count > 0)
                {
                   
                }
                else
                {
                    MessageBox.Show("No matching food found.");
                    ClearForm();
                }
            }
        }

        private void LoadCuisineTypes()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT DISTINCT CuisineType FROM Food";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                comboBox1.Items.Clear();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader["CuisineType"].ToString());
                }

                conn.Close();
            }
        }

       

        private void button12_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Please enter the Food ID.");
                return;
            }

            if (!int.TryParse(textBox3.Text, out int foodId))
            {
                MessageBox.Show("Invalid Food ID.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Step 1: Check if this food record belongs to the current provider
                string checkQuery = @"SELECT FoodID FROM Food 
                   WHERE FoodID = @FoodID AND ProviderID = @ProviderID";

                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@FoodID", foodId);
                checkCmd.Parameters.AddWithValue("@ProviderID", _providerID);

                conn.Open();
                object result = checkCmd.ExecuteScalar();
                conn.Close();

                if (result == null)
                {
                    MessageBox.Show("Food record not found or doesn't belong to you.");
                    return;
                }

                // Step 2: Nullify in AssignedServices
                string nullifyQuery = @"UPDATE AssignedServices 
                     SET FoodProviderID = NULL 
                     WHERE FoodProviderID = @FID";

                SqlCommand nullifyCmd = new SqlCommand(nullifyQuery, conn);
                nullifyCmd.Parameters.AddWithValue("@FID", foodId);

                conn.Open();
                nullifyCmd.ExecuteNonQuery();
                conn.Close();

                // Step 3: Delete from Food table
                string deleteQuery = @"DELETE FROM Food WHERE FoodID = @FID";
                SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn);
                deleteCmd.Parameters.AddWithValue("@FID", foodId);

                conn.Open();
                int rows = deleteCmd.ExecuteNonQuery();
                conn.Close();

                if (rows > 0)
                {
                    MessageBox.Show("Food service deleted successfully.");
                    LoadFoodData(); // Refresh grid
                    ClearForm();    // Optional: reset form fields
                }
                else
                {
                    MessageBox.Show("Failed to delete food record.");
                }
            }
        }

        private void LoadFoodData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Food WHERE ProviderID = @ProviderID";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@ProviderID", _providerID);

                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void ClearForm()
        {
            comboBox1.SelectedIndex = -1;
            radioButton3.Checked = radioButton4.Checked = false;
            radioButton1.Checked = radioButton2.Checked = false;
            radioButton6.Checked = radioButton5.Checked = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Food";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dataGridView1.DataSource = dt;
                    }
                }
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
