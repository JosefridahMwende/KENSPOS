using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KENSPOS.FORMS
{
    public partial class frmDashboard : Form
    {
        public frmDashboard()
        {
            InitializeComponent();
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            frmSales frm = new frmSales();
            frm.ShowDialog();
        }

        private void btnCashier_Click(object sender, EventArgs e)
        {
            frmCashier frm = new frmCashier();
            frm.ShowDialog();
        }

        private void btApprovals_Click(object sender, EventArgs e)
        {
            frmAprrovals frm = new frmAprrovals();
            frm.ShowDialog();
        }
    }
}
