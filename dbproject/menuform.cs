using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TOUROPERATOR_INTERFACE
{
    public partial class menuform : Form
    {
        private int operatorid;
        public menuform(int opid)
        {
            InitializeComponent();
            this.operatorid = opid;
        }

        public menuform()
        {
            InitializeComponent();
        }

        private void menuform_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            home hm = new home();
            hm.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tripManagementForm tmf = new tripManagementForm(operatorid);
            tmf.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            manageBooking mb = new manageBooking(operatorid); 
            mb.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            assignservices ass = new assignservices(operatorid);
            ass.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            viewanalytics va = new viewanalytics(operatorid);
            va.Show();
            this.Hide();
        }
    }
}
