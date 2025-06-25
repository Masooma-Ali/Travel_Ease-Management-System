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
    public partial class manageBooking : Form
    {
        private int currentOperatorId;
        public manageBooking(int optid)
        {
            InitializeComponent();
            currentOperatorId = optid;
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

        private void button4_Click(object sender, EventArgs e)
        {
            menuform m = new menuform(currentOperatorId);
            m.Show();
            this.Hide();
        }

        private void manageBooking_Load(object sender, EventArgs e)
        {
            LoadTripIDs();
            dataGridView1.CellClick += dataGridView1_CellClick;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int tripId = Convert.ToInt32(comboBox1.SelectedValue);

            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-842J4RM\\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;Encrypt=False"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Bookings WHERE TripID = @TripID AND TourOperatorResponse = 'Pending' ", con);
                cmd.Parameters.AddWithValue("@TripID", tripId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No bookings found for this trip.");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (selectedBookingId == -1)
            {
                MessageBox.Show("Please select a Booking first.");
                return;
            }


                using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-842J4RM\\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;Encrypt=False"))
                {
                con.Open();
                SqlCommand cmd = new SqlCommand(@"
                UPDATE Bookings
                SET 
                    TourOperatorResponse = 'Cancelled',
                    PaymentStatus = CASE 
                        WHEN PaymentStatus = 'Paid' THEN 'refund'
                        ELSE PaymentStatus
                    END,
                    OperatorResponseDate = GETDATE()
                WHERE BookingID = @BookingID", con);

                cmd.Parameters.AddWithValue("@BookingID", selectedBookingId);
                cmd.ExecuteNonQuery();

            }

            MessageBox.Show("Booking has been cancelled and marked as refunded.");
                // Refresh the grid
                button1.PerformClick();
        }

        int selectedBookingId = -1;

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                if (row.Cells["BookingID"].Value != null)
                {
                    selectedBookingId = Convert.ToInt32(row.Cells["BookingID"].Value);
                    MessageBox.Show("Trip ID selected: " + selectedBookingId);
                }
            }
        }
    }
}
