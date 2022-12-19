using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static KENSPOS.MODULES.CommonFunctions;

namespace KENSPOS.FORMS
{
    public partial class frmCashier : Form
    {
        private string SQL, dgItemFilter, dgSalesOrderFilter, dgQuoteFilter, text, docType, OrderNo, ReceiptNo = string.Empty;
        private Int64 OrderSNo = 0;
        private DataTable DT;
        private Int64 RecNo = 0;

        private Double AmountOnLeave, TotalOrderAmount = 0;

        private DataSet dtSet;

        public frmCashier()
        {
            InitializeComponent();
        }

        private void frmCashier_Load(object sender, EventArgs e)
        {
            loadOrders();

        }
        private void loadOrders()
        {
            SQL = "SELECT ORDER_SNO, ORDER_NO FROM WIZ_KENS_Order WHERE doctype='ORDER' AND ORDER_NO LIKE '%" + activeUSERWhse + "%'";
            LoadCombo(SQL, cboOrders, "ORDER_SNO", "ORDER_NO", true, false);
            cboOrders.SelectedIndex = -1;
        }

        private void cboOrders_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cboOrders.SelectedIndex > -1)
            {
                Double TotalPaidAmount, Balance = 0;
                SQL = "select FORMAT(SUM(d.TotalIncl),'N2') Amount, O.CustName from WIZ_KENS_OrderDetails d INNER JOIN WIZ_KENS_Order O ON D.OrderSNo = O.ORDER_SNO where d.docType='ORDER' and d.OrderSNo=" + cboOrders.SelectedValue + " GROUP BY O.CustName";
                DT = new DataTable();
                LoadDatatable(SQL, DT);

                if (DT.Rows.Count > 0)
                {
                    TotalOrderAmount = Convert.ToDouble(DT.Rows[0]["Amount"]);

                    lblOrderAmountValue.Text = TotalOrderAmount.ToString("n2");
                    lblCustNameValue.Text = DT.Rows[0]["CustName"].ToString();
                    txtBalCashier.Text = (Convert.ToDouble(lblOrderAmountValue.Text) - Convert.ToDouble(txtTotalCashier.Text)).ToString("n2");

                    //txtBalCashier.Text = Balance.ToString("n2");
                }
            }
        }

        private void txtCashCashier_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumbersOnly(sender, e);
        }

        private void txtMobileMoneyCashier_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumbersOnly(sender, e);
        }

        private void txtChequeCashier_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumbersOnly(sender, e);
        }

        private void txtCashCashier_Leave(object sender, EventArgs e)
        {
            (sender as TextBox).Text = String.Format("{0:n}", double.Parse((sender as TextBox).Text));
            CalculateReceivedAmounts(sender, e);
        }

        private void CalculateReceivedAmounts(object sender, EventArgs e)
        {

            if (Convert.ToDouble((sender as TextBox).Text) > 0 && (sender as TextBox).Text != "")
            {
                try
                {
                    double sum = GetAll(this, typeof(TextBox))
                              .Where(box => string.Equals("SUM", box.Tag as String))
                              .Select(box => double.TryParse(box.Text, out var v) ? v : 0)
                              .Sum();

                    txtTotalCashier.Text = sum.ToString("n2");
                    txtBalCashier.Text = (Convert.ToDouble(lblOrderAmountValue.Text) - Convert.ToDouble(txtTotalCashier.Text)).ToString("n2");
                }
                catch (Exception ex)
                {

                    frmError err = new frmError();
                    err.txtError.Text = ex.Message.ToString();
                    err.ShowDialog();
                }
            }
        }



        public IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
        }


        //private static IEnumerable<Control> GetAll(Control control)
        //{
        //    var controls = control.Controls.OfType<Control>();

        //    return controls
        //      .SelectMany(ctrl => GetAll(ctrl))
        //      .Concat(controls);
        //}

        private void txtWhtCashier_Leave(object sender, EventArgs e)
        {

            (sender as TextBox).Text = String.Format("{0:n}", double.Parse((sender as TextBox).Text));
            CalculateReceivedAmounts(sender, e);
        }

        private void txtUnreceivedCashier_Leave(object sender, EventArgs e)
        {
            (sender as TextBox).Text = String.Format("{0:n}", double.Parse((sender as TextBox).Text));
            CalculateReceivedAmounts(sender, e);
        }

        private void txtRTGSCashier_Leave(object sender, EventArgs e)
        {
            (sender as TextBox).Text = String.Format("{0:n}", double.Parse((sender as TextBox).Text));
            CalculateReceivedAmounts(sender, e);
        }

        private void txtPesaLinkCashier_Leave(object sender, EventArgs e)
        {
            (sender as TextBox).Text = String.Format("{0:n}", double.Parse((sender as TextBox).Text));
            CalculateReceivedAmounts(sender, e);
        }

        private void txtMpesaBankCashier_Leave(object sender, EventArgs e)
        {
            (sender as TextBox).Text = String.Format("{0:n}", double.Parse((sender as TextBox).Text));
            CalculateReceivedAmounts(sender, e);
        }

        private void txtCardCashier_Leave(object sender, EventArgs e)
        {
            (sender as TextBox).Text = String.Format("{0:n}", double.Parse((sender as TextBox).Text));
            CalculateReceivedAmounts(sender, e);
        }

        private void txtMpesaCashier_Leave(object sender, EventArgs e)
        {
            (sender as TextBox).Text = String.Format("{0:n}", double.Parse((sender as TextBox).Text));
            CalculateReceivedAmounts(sender, e);
        }

        private void txChequeCashier_Leave(object sender, EventArgs e)
        {
            (sender as TextBox).Text = String.Format("{0:n}", double.Parse((sender as TextBox).Text));
            CalculateReceivedAmounts(sender, e);
        }

        private void tbnReceive_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(txtTotalCashier.Text) != TotalOrderAmount)
            {
                MessageBox.Show("Received amount must be equal to order amount", "Form Cashier", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                docType = "RECEIPT";
                saveNewReceipt(docType, sender, e);
            }
        }


        private void txtEFTCashier_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumbersOnly(sender, e);
        }

        private void txtCardCashier_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumbersOnly(sender, e);
        }

        private void lblCash_DoubleClick(object sender, EventArgs e)
        {
            txtCashCashier.Text = txtBalCashier.Text;
        }

        private void lbCheque_DoubleClick(object sender, EventArgs e)
        {
            txtChequeCashier.Text = txtBalCashier.Text;
        }

        private void lbMpesa_DoubleClick(object sender, EventArgs e)
        {
            txtMobileMoneyCashier.Text = txtBalCashier.Text;
        }

        private void lbCCard_DoubleClick(object sender, EventArgs e)
        {
            txtCardCashier.Text = txtBalCashier.Text;
        }

        private void lbMTB_DoubleClick(object sender, EventArgs e)
        {
            txtMpesaBankCashier.Text = txtBalCashier.Text;
        }

        private void lbPesalink_Click(object sender, EventArgs e)
        {
            txtPesaLinkCashier.Text = txtBalCashier.Text;
        }

        private void lbRTGS_DoubleClick(object sender, EventArgs e)
        {
            txtRTGSCashier.Text = txtBalCashier.Text;
        }

        private void LBUnreceived_DoubleClick(object sender, EventArgs e)
        {
            txtUnreceivedCashier.Text = txtBalCashier.Text;
        }

        private void lbWHT_DoubleClick(object sender, EventArgs e)
        {
            txtWhtCashier.Text = txtBalCashier.Text;
        }

        private void txtCRNCashier_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumbersOnly(sender, e);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumbersOnly(sender, e);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumbersOnly(sender, e);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            NumbersOnly(sender, e);
        }

        private void CalculateTotalAmountReceived(object sender, EventArgs e)
        {
            if (Convert.ToDouble((sender as TextBox).Text) > 0)
            {
                AmountOnLeave = AmountOnLeave + Convert.ToDouble((sender as TextBox).Text);
            }

        }


        //Reset fields  
        private void initializeFields()
        {
            cboOrders.SelectedIndex = -1;
            txtChqBank.Text = "";
            txtCardMobile.Text = "";
            cbPesalinkBank.SelectedIndex = -1;
            cbRTGSBank.SelectedIndex = -1;
            lblOrderAmountValue.Text = "0.0";
            lblCustNameValue.Text = "";
            txtCashCashier.Text = "0.0";
            txtChequeCashier.Text = "0.0";
            txtCardCashier.Text = "0.0";
            txtMobileMoneyCashier.Text = "0.0";
            txtCardCashier.Text = "0.0";
            txtPesaLinkCashier.Text = "0.0";
            txtRTGSCashier.Text = "0.0";
            txtUnreceivedCashier.Text = "0.0";
            txtWhtCashier.Text = "0.0";
            txtTotalCashier.Text = "0.0";
            txtBalCashier.Text = "0.0";
            txtMpesaRef.Text = "";
            txtMpesaMobile.Text = "";
            txtCardNumber.Text = "";
            txtMpesaBankCashier.Text = "";
            txtMpesaBankNo.Text = "";
            txtMTBRef.Text = "";
            txtChqNumber.Text = "";
        }

        private string FetchSettingsQuery(String Doc)
        {
            String Query = string.Empty;
            if (Doc == "RECEIPT")
            {                       // code block
                Query = "SELECT ISNULL(s.NextReceiptNo, 1) [RecNO], ISNULL(s.ReceiptNoPrefix, 'RC-') + REPLICATE('0',S.ReceiptNoLen-LEN(RTRIM(NextReceiptNo))) + RTRIM(NextReceiptNo) [REC_NO] FROM WIZ_KENS_SETTINGS S";
            }
            return Query;
        }


        private void saveNewReceipt(String DocType, object sender, EventArgs e)
        {
            if (DocType == "RECEIPT")
            {
                SQL = FetchSettingsQuery(DocType);
            }
            else
            {
                MessageBox.Show("Document Type not specified. Can't be saved. Please contact the adminstartor.", "Error in Cashier", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DT = new DataTable();
            LoadDatatable(SQL, DT);

            if (DT.Rows.Count > 0)
            {
                RecNo = Convert.ToInt64(DT.Rows[0]["RecNO"].ToString());
                ReceiptNo = DT.Rows[0]["REC_NO"].ToString();
                DT = null;


                if (DocType == "RECEIPT")
                {
                     SQL = "Update WIZ_KENS_SETTINGS Set NextReceiptNo = NextReceiptNo + 1";                          
                }

                ExecuteQuery(SQL);
            }
            else
            {
                MessageBox.Show("Error encountered while saving document. Please contact the adminstrator.", "Error in receipt", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (DocType == "RECEIPT")
            {

                SQL = "	INSERT INTO WIZ_KENS_ReceivePayment ([ORDER_NO] ,[CustName] ,[OrderAmount] ,[CashAmt],[OtherCashRef],[ChequeAmt],[ChqBank],[ChqDate],[ChqNo],[OtherChqRef],[MpesaAmt],[MpesaRef],[MpesaMobile],[OtherMpesaRef],[CCardAmt],[CCardDigits],[CCardBank],[OtherCCardRef],[MTBankAmt],[MTRef],[MTMobile],[OtherMTBRef],[PesalinkAmt], [PesalinkBank],[OtherPesalinkRef],[RTGSAmt],[RTGSBank] ,[OtherRTGSRef],[UnreceivedAmt],[OtherUnreceivedRef],[WHTAmt] ,[OtherWHTRef],[TotalPaid],[BalanceLeft] ,[DateCreated] ,[CreatedBy]) VALUES('" + cboOrders.Text + "' + '" + lblCustNameValue + "'+ " + lblOrderAmountValue + "+ '" + txtCashCashier + "' + )";

                if (ExecuteQuery(SQL))
                {
                    MessageBox.Show("Receipt No :" + ReceiptNo.ToString() + " saved sucessfully.", "Receipt", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataGrid(dgPayments, SQL, "", "");
                    //initializeFields();
                }
            }
        }
    }
}


