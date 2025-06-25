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
    public partial class paymentreportview : Form
    {
        public paymentreportview()
        {
            InitializeComponent();
        }

        private void paymentreportview_Load(object sender, EventArgs e)
        {


            try
            {
                // 1. Create typed DataTables
                var paySuccessTable = new paymentdataset.paymentsuccessDataTable();
                var chargebackTable = new paymentdataset.chargebackDataTable();
                var payTable = new paymentdataset.paycountDataTable();

                // 2. Create and use TableAdapters to fill the tables
                var paySuccessAdapter = new dbproject.paymentdatasetTableAdapters.paymentsuccessTableAdapter();
                var chargebackAdapter = new dbproject.paymentdatasetTableAdapters.chargebackTableAdapter();
                var payAdapter = new dbproject.paymentdatasetTableAdapters.paycountTableAdapter();

                paySuccessAdapter.Fill(paySuccessTable);
                chargebackAdapter.Fill(chargebackTable);
                payAdapter.Fill(payTable);
                // 3. Set RDLC path
                reportViewer1.LocalReport.ReportPath = "paymentreport.rdlc"; // Ensure 'Copy to Output Directory = Copy Always'

                // 4. Clear and bind report data sources
                reportViewer1.LocalReport.DataSources.Clear();

                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("paysuccess", (DataTable)paySuccessTable));
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("chargebacktable", (DataTable)chargebackTable));
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("paystatus", (DataTable)payTable));

                // 5. Refresh the report
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading payment report: " + ex.Message);
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
