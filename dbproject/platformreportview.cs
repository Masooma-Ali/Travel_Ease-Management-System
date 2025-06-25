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
    public partial class platformreportview : Form
    {
        public platformreportview()
        {
            InitializeComponent();
        }

        private void platformreportview_Load(object sender, EventArgs e)
        {
            try
            {
                // Create typed dataset instances
                var registrationTable = new platformdataset.registartionDataTable();
                var activeUserTable = new platformdataset.activeuserDataTable();
                var partnershipTable = new platformdataset.partnershipDataTable();
                var regionExpTable = new platformdataset.regionexpDataTable();
                var cityExpTable = new platformdataset.usercityDataTable();


                // Create TableAdapters
                var registrationAdapter = new dbproject.platformdatasetTableAdapters.registartionTableAdapter();
                var activeUserAdapter = new dbproject.platformdatasetTableAdapters.activeuserTableAdapter();
                var partnershipAdapter = new dbproject.platformdatasetTableAdapters.partnershipTableAdapter();
                var regionExpAdapter = new dbproject.platformdatasetTableAdapters.regionexpTableAdapter();
                var cityExpAdapter = new dbproject.platformdatasetTableAdapters.usercityTableAdapter();

                // Fill the tables with data from the DB
                registrationAdapter.Fill(registrationTable);
                activeUserAdapter.Fill(activeUserTable);
                partnershipAdapter.Fill(partnershipTable);
                regionExpAdapter.Fill(regionExpTable);
                cityExpAdapter.Fill(cityExpTable);
                // Set the RDLC file path (make sure it is copied to output directory)
                reportViewer1.LocalReport.ReportPath = "platformreport.rdlc";

                // Clear previous data sources
                reportViewer1.LocalReport.DataSources.Clear();

                // Bind each DataTable to its RDLC dataset name
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("Registration", (DataTable)registrationTable));
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("activeu", (DataTable)activeUserTable));
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("partner", (DataTable)partnershipTable));
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("regional", (DataTable)regionExpTable));
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("city", (DataTable)cityExpTable));

                // Refresh the report
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Platform Report: " + ex.Message);
            }

         
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Admin newForm = new Admin();  // Create instance of Form1
            newForm.Show();               // Show the new form
            this.Hide();
        }
    }
}
