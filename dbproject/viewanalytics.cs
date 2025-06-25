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
    public partial class viewanalytics : Form
    {
        private int currentOperatorId;
        public viewanalytics(int optid)
        {
            InitializeComponent();
            currentOperatorId = optid;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            menuform mf = new menuform(currentOperatorId);
            mf.Show();
            this.Hide();
        }

        private void viewanalytics_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-842J4RM\\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;Encrypt=False"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(@"
                SELECT 
                    r.ReviewID,
                    r.TravelerID,
                    r.TargetRole,
                    r.TargetID,
                    r.Rating,
                    r.ReviewText,
                    r.ReviewDate
                   
                FROM 
                    Review r
                JOIN 
                    Traveler t ON r.TravelerID = t.TravelerID
                WHERE 
                    r.TargetRole = 'TourOperator' AND r.TargetID = @OperatorID AND (r.AdminResponnse= 'Accepted') ", con);

                cmd.Parameters.AddWithValue("@OperatorID", currentOperatorId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No reviews found for this tour operator.");
                }
                else
                {
                    dataGridView1.DataSource = dt;
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
