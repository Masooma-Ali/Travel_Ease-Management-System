using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace allinterfaces
{
    public partial class operatorreview : Form
    {
        public operatorreview()
        {
            InitializeComponent();
        }

        private void operatorreview_Load(object sender, EventArgs e)
        {
            // Replace with your actual database connection string
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-842J4RM\\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;Encrypt=False");

            // SQL query with Operator Full Name from USERS table
            SqlCommand cmd = new SqlCommand(@"
                SELECT 
                    TOpr.OperatorID,
                    U.FirstName + ' ' + ISNULL(U.MiddleName + ' ', '') + U.LastName AS OperatorName,

                    -- Average Rating for Tour Operator
                    AVG(CASE 
                        WHEN r.TargetRole = 'TourOperator' THEN r.Rating 
                        ELSE NULL 
                    END) AS AvgRating,

                    -- Total Revenue from Paid Bookings
                    SUM(CASE 
                        WHEN b.PaymentStatus = 'Paid' THEN t.totalamount 
                        ELSE 0 
                    END) AS TotalRevenue,

                    -- Average Response Time (in minutes)
                    AVG(CASE 
                        WHEN b.OperatorResponseDate IS NOT NULL THEN DATEDIFF(MINUTE, b.OperatorResponseDate, b.BookingDate)
                        ELSE NULL 
                    END) AS AvgResponseTime

                FROM 
                    TourOperator TOpr
                INNER JOIN USERS U ON TOpr.UserID = U.UserID
                LEFT JOIN Trip t ON t.OperatorID = TOpr.OperatorID
                LEFT JOIN Bookings b ON b.TripID = t.TripID
                LEFT JOIN Review r ON r.TargetRole = 'TourOperator' AND r.TargetID = TOpr.OperatorID

                GROUP BY 
                    TOpr.OperatorID, U.FirstName, U.MiddleName, U.LastName
            ", con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            // Create and fill the typed DataSet
            operatorPerformanceDataset ds = new operatorPerformanceDataset();
            da.Fill(ds, "OperatorPerformance"); // Match the DataTable name in your .xsd exactly

            // Set up ReportViewer
            ReportDataSource rds = new ReportDataSource("OperatorPerformance", ds.Tables["OperatorPerformance"]);
            reportViewer1.LocalReport.ReportPath = "operatorPerformance.rdlc"; // Make sure path is correct
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
