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

namespace dbproject
{
    public partial class guide : Form
    {
        string connectionString = "Data Source=DESKTOP-842J4RM\\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;Encrypt=False";
        private int _providerID;

        public guide(int providerID)
        {
            InitializeComponent();
            _providerID = providerID;

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            service_mainpage reg = new service_mainpage(_providerID);
            reg.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
          /*  // Validate input
            if (string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }*/

            // Determine licensed value (1 = Yes, 0 = No)
           /* int isLicensed = -1;
            if (radioButton1.Checked)
                isLicensed = 1;
            else if (radioButton2.Checked)
                isLicensed = 0;
            else
            {
                MessageBox.Show("Please select whether the guide is licensed.");
                return;
            }

            try
            {
               

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO PersonalGuide (ExperienceYears, Language, Licensed) VALUES (@Experience, @Language, @IsLicensed)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {

                        cmd.Parameters.AddWithValue("@Experience", textBox2.Text);
                        cmd.Parameters.AddWithValue("@Language", textBox3.Text);
                        cmd.Parameters.AddWithValue("@IsLicensed", isLicensed);

                        conn.Open();
                        int rows = cmd.ExecuteNonQuery();
                        conn.Close();

                        if (rows > 0)
                        {
                            MessageBox.Show("Personal guide added successfully.");
                            ClearForm();
                        }
                        else
                        {
                            MessageBox.Show("Insert failed.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }*/
        }

        private void ClearForm()
        {
            textBox3.Clear();
            textBox2.Clear();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        private void guide_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

       
        private void AddServiceTypeForProvider()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string checkQuery = @"SELECT COUNT(*) FROM ProviderServiceTypes 
                              WHERE ProviderID = @ProviderID AND ServiceType = @ServiceType";

                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@ProviderID", _providerID);
                checkCmd.Parameters.AddWithValue("@ServiceType", "PersonalGuide");

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
                        insertCmd.Parameters.AddWithValue("@ServiceType", "Personal Guide");

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


        private void button11_Click_1(object sender, EventArgs e)
        {
            int licensed = radioButton1.Checked ? 1 : radioButton2.Checked ? 0 : -1;

    if (licensed == -1)
    {
        MessageBox.Show("Please select license status.");
        return;
    }

    using (SqlConnection conn = new SqlConnection(connectionString))
    {
        string query = @"INSERT INTO PersonalGuide (ProviderID, Language, ExperienceYears, Licensed)
                         VALUES (@ProviderID, @Language, @Experience, @Licensed)";
        SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ProviderID", _providerID);
                cmd.Parameters.AddWithValue("@Language", textBox3.Text);
        cmd.Parameters.AddWithValue("@Experience", textBox4.Text);
        cmd.Parameters.AddWithValue("@Licensed", licensed);

        conn.Open();
        int rows=cmd.ExecuteNonQuery();
        conn.Close();

                if (rows > 0)
                {
                    MessageBox.Show("Guide added successfully.");

                    AddServiceTypeForProvider();

                    using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM PersonalGuide WHERE ProviderID = @ProviderID", connectionString))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@ProviderID", _providerID);

                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        dataGridView1.DataSource = dt;
                    }
                }
                else
                {
                    MessageBox.Show("Failed to add Guide.");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder query = new StringBuilder("SELECT * FROM PersonalGuide WHERE 1=1");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;


                if (!string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    query.Append(" AND Language = @Language");
                    cmd.Parameters.AddWithValue("@Language", textBox3.Text);
                }

                if (int.TryParse(textBox2.Text, out int experience))
                {
                    query.Append(" AND ExperienceYears = @Experience");
                    cmd.Parameters.AddWithValue("@Experience", experience);
                }

                if (radioButton1.Checked)
                {
                    query.Append(" AND Licensed = 1");
                }
                else if (radioButton2.Checked)
                {
                    query.Append(" AND Licensed = 0");
                }

                if (int.TryParse(textBox4.Text, out int guideID))
                {
                    query.Append(" AND GuideID = @GuideID");
                    cmd.Parameters.AddWithValue("@GuideID", guideID);
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
                    MessageBox.Show("No matching guide found.");
                    ClearForm();
                }

            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Please enter the Guide ID.");
                return;
            }

            if (!int.TryParse(textBox4.Text, out int guideId))
            {
                MessageBox.Show("Invalid Guide ID.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Step 1: Verify GuideID belongs to the current provider
                string checkQuery = @"SELECT GuideID FROM PersonalGuide 
                   WHERE GuideID = @GuideID AND ProviderID = @ProviderID";

                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@GuideID", guideId);
                checkCmd.Parameters.AddWithValue("@ProviderID", _providerID);

                conn.Open();
                object result = checkCmd.ExecuteScalar();
                conn.Close();

                if (result == null)
                {
                    MessageBox.Show("No matching guide found for your provider account.");
                    return;
                }

                // Step 2: Nullify foreign key in AssignedServices
                string nullifyQuery = @"UPDATE AssignedServices 
                     SET GuideProviderID = NULL 
                     WHERE GuideProviderID = @GID";

                SqlCommand nullifyCmd = new SqlCommand(nullifyQuery, conn);
                nullifyCmd.Parameters.AddWithValue("@GID", guideId);

                conn.Open();
                nullifyCmd.ExecuteNonQuery();
                conn.Close();

                // Step 3: Delete from PersonalGuide
                string deleteQuery = @"DELETE FROM PersonalGuide WHERE GuideID = @GID";
                SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn);
                deleteCmd.Parameters.AddWithValue("@GID", guideId);

                conn.Open();
                int rows = deleteCmd.ExecuteNonQuery();
                conn.Close();

                if (rows > 0)
                {
                    MessageBox.Show("Guide deleted successfully.");
                    LoadGuideData();  // Refresh the grid
                    ClearGuideForm(); // Optional form clearing method
                }
                else
                {
                    MessageBox.Show("Failed to delete guide.");
                }
            }
        }

        private void LoadGuideData()
        {
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM PersonalGuide WHERE ProviderID = @ProviderID", connectionString))
            {
                adapter.SelectCommand.Parameters.AddWithValue("@ProviderID", _providerID);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void ClearGuideForm()
        {
            textBox3.Clear();                    // Clear Language
            textBox4.Clear();                    // Clear ExperienceYears

            radioButton1.Checked = false;        // Licensed Yes
            radioButton2.Checked = false;        // Licensed No
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM PersonalGuide";

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

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            service_mainpage reg = new service_mainpage(_providerID);
            reg.Show();
            this.Hide();
        }
    }
}
