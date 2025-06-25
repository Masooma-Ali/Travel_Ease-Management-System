using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dbproject;
using Microsoft.Reporting.WinForms;

namespace db_f
{
    public partial class Reportforms : Form
    {
        public Reportforms()
        {
            InitializeComponent();
        }

        private void Reportforms_Load(object sender, EventArgs e)
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

            // === Clear any existing data sources from the ReportViewer ===
            reportViewer1.LocalReport.DataSources.Clear();

            // === Add the datasets to the ReportViewer ===
            // These names MUST MATCH the dataset names defined in your .rdlc file
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Hoteldata", (DataTable)hotelData));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("transportdata", (DataTable)transportData));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("guidedata", (DataTable)guideData));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("serviceutitlize", (DataTable)serviceUtilizeData));



            // === Set the RDLC report file path ===
            // This should be the name of your .rdlc file (set its "Copy to Output Directory" property to "Copy if newer")
            reportViewer1.LocalReport.ReportPath = "serviceproviderefficiencyreport.rdlc";

            this.reportViewer1.RefreshReport();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Admin newForm = new Admin();  // Create instance of Form1
            newForm.Show();               // Show the new form
            this.Hide();
        }
    }
}

