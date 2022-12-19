using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static KENSPOS.MODULES.CommonFunctions;

namespace KENSPOS.FORMS
{
    public partial class frmLogin : Form
    {
        private string SQL = string.Empty;

        private void btLogin_Click(object sender, EventArgs e)
        {
            if (cboUserName.Text == "")
            {
                MessageBox.Show("User name not selected. Please try again.", "Login Form", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (attemptLogin())
            {
                activeUSER = cboUserName.Text;
                UserSNo = Convert.ToInt16(cboUserName.SelectedValue);
                SQL = "SELECT Warehouse FROM WIZ_KENS_USERS WHERE AgentID=" + cboUserName.SelectedValue;
                activeUSERWhse = FetchSingleStringValue(SQL, true, false);
                var frm = new frmDashboard();
                frm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Login failed check your credentials and try again", "Login Form", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String encrytPassword = EncryptText(txtPassword.Text, "ABC123");
            SQL = "IF NOT EXISTS (SELECT 1 FROM WIZ_KENS_USERS WHERE AgentID =" + cboUserName.SelectedValue + ") " +
        "INSERT INTO [dbo].[WIZ_KENS_USERS]  ([AgentID], [Name], [Password])  VALUES (" +
        cboUserName.SelectedValue + ",'" +
        cboUserName.Text + "','" +
        encrytPassword + "') " +
        "ELSE UPDATE WIZ_KENS_USERS SET Password = '" + encrytPassword + "' WHERE AgentID = " + cboUserName.SelectedValue;
            ExecuteQuery(SQL);
        }

        private DataTable DT;

        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            loadUsers();
            cboUserName.SelectedIndex = -1;
        }

        private void loadUsers()
        {
            SQL = "SELECT AgentID, Name FROM WIZ_KENS_USERS";
            LoadCombo(SQL,cboUserName, "AgentID", "Name", false, false);
        }

        private bool attemptLogin()
        {
            bool resp = false;
            String encrytPassword = EncryptText(txtPassword.Text, "ABC123");

            SQL = "SELECT 1 FROM WIZ_KENS_USERS WHERE AgentID =" + cboUserName.SelectedValue + " AND PASSWORD ='" + encrytPassword + "'";
            resp = FetchSingleBooleanValue(SQL);

            return resp;
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
