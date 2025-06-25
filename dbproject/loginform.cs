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
    public partial class loginform : Form
    {
        public loginform()
        {
            InitializeComponent();
        }

        private void LoadUsername()
        {
            using (SqlConnection conn = new SqlConnection("Data Source=DESKTOP-9F33E8U\\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;Encrypt=False"))
            {
                conn.Open();
                string query = "SELECT UserID, Email from USERS WHERE Role = 'TourOperator'";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "Email"; // Shows this in dropdown
                comboBox1.ValueMember = "UserID"; // Stores this as SelectedValue
            }
        }
        private void loginform_Load(object sender, EventArgs e)
        {
            LoadUsername();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(comboBox1.SelectedValue);
            int operatorId = -1;

            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-9F33E8U\\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;Encrypt=False"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT OperatorID FROM TourOperator WHERE UserID = @UserID", con);
                cmd.Parameters.AddWithValue("@UserID", userId);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    operatorId = Convert.ToInt32(reader["OperatorID"]);
                }
                reader.Close();
            }

            if (operatorId != -1)
            {
                MessageBox.Show("Operator ID: " + operatorId);
                menuform mf = new menuform(operatorId);
                mf.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Error: This user is not registered as a Tour Operator.");
            }
        }
    }
}
