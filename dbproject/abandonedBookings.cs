using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;


namespace allinterfaces
{
    public partial class abandonedBookings : Form
    {
        public abandonedBookings()
        {
            InitializeComponent();
        }

        private void abandonedBookings_Load(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection("Data Source=DESKTOP-842J4RM\\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;Encrypt=False");

            SqlCommand cmd = new SqlCommand(@"
                            SELECT 
            'Summary' AS DataType,
            COUNT(*) AS TotalBookings,
            SUM(CASE 
                WHEN b.PaymentStatus = 'Unpaid' AND DATEDIFF(DAY, b.BookingDate, GETDATE()) > 3 THEN 1
                ELSE 0
            END) AS AbandonedBookings,
            SUM(CASE 
                WHEN b.PaymentStatus = 'Paid' AND DATEDIFF(DAY, b.BookingDate, GETDATE()) > 3 THEN 1
                ELSE 0
            END) AS RecoveredBookings,
            NULL AS BookingID,
            NULL AS RevenueLoss

        FROM Bookings b
        JOIN Trip t ON b.TripID = t.TripID

        UNION ALL

        -- Individual abandoned bookings (for bar chart)
        SELECT 
            'Detail' AS DataType,
            NULL AS TotalBookings,
            NULL AS AbandonedBookings,
            NULL AS RecoveredBookings,
            b.BookingID,
            t.totalamount AS RevenueLoss
        FROM Bookings b
        JOIN Trip t ON b.TripID = t.TripID
        WHERE b.PaymentStatus = 'Unpaid' 
          AND DATEDIFF(DAY, b.BookingDate, GETDATE()) > 3
            ", con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            dbproject.abandonedBookingData ds = new dbproject.abandonedBookingData(); // Your .xsd
            da.Fill(ds, "AbandonedBookings");

            ReportDataSource rds = new ReportDataSource("AbandonedBookingDataset", ds.Tables["AbandonedBookings"]);
            reportViewer1.LocalReport.ReportPath = "abandonedBookingReport.rdlc";
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();
        }
    }
}
