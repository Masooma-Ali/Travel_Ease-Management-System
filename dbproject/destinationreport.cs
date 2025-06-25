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
using Microsoft.Reporting.WinForms;

namespace allinterfaces
{
    public partial class destinationreport : Form
    {
        public destinationreport()
        {
            InitializeComponent();
        }

        private void destinationreport_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-842J4RM\\SQLEXPRESS;Initial Catalog=travelease;Integrated Security=True;Encrypt=False");

            SqlCommand cmd = new SqlCommand(@"
        SELECT 
            d.City + ', ' + d.Country AS DestinationName,
            COUNT(b.BookingID) AS TotalBookings,
            DATENAME(MONTH, b.BookingDate) AS BookingMonth,
            MONTH(b.BookingDate) AS MonthNumber,
            AVG(CASE 
                WHEN r.TargetRole = 'Trip' AND r.TargetID = t.TripID THEN r.Rating 
                ELSE NULL
            END) AS AvgRating
        FROM 
            Destination d
        JOIN Trip t ON t.DestinationID = d.DestinationID
        LEFT JOIN Bookings b ON b.TripID = t.TripID
        LEFT JOIN Review r ON r.TargetRole = 'Trip' AND r.TargetID = t.TripID
        GROUP BY 
            d.City, d.Country, DATENAME(MONTH, b.BookingDate), MONTH(b.BookingDate)
        ORDER BY 
            d.City, MONTH(b.BookingDate)
    ", con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            destinationData ds = new destinationData();
            da.Fill(ds, "DestinationReport");
            ReportDataSource rds = new ReportDataSource("DestinationReport", ds.Tables["DestinationReport"]);
            reportViewer1.LocalReport.ReportPath = "destinationReport.rdlc";
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }
    }
}
