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
    public partial class Audit : Form
    {
        public Audit()
        {
            InitializeComponent();
        }

        private void Audit_Load(object sender, EventArgs e)
        {

            try
            {
                // Create the typed DataTable from auditdataset
                var auditTable = new auditdataset.AuditLogDataTable();

                // Create and fill TableAdapter
                var auditAdapter = new dbproject.auditdatasetTableAdapters.AuditLogTableAdapter();
                auditAdapter.Fill(auditTable);

                // Set the RDLC report path (make sure the file is set to "Copy Always")
                reportViewer1.LocalReport.ReportPath = "auditreport.rdlc";

                // Clear existing data sources
                reportViewer1.LocalReport.DataSources.Clear();

                // Bind to ReportViewer
                reportViewer1.LocalReport.DataSources.Add(
                    new ReportDataSource("audittable", (DataTable)auditTable)
                );

                // Refresh the report
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading audit report: " + ex.Message);
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
