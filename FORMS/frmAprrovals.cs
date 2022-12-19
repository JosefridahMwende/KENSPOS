using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static KENSPOS.MODULES.CommonFunctions;
using Pastel.Evolution;

namespace KENSPOS.FORMS
{
    public partial class frmAprrovals : Form
    {
        private string SQL, dgItemFilter, dgSalesOrderFilter, dgQuotesFilter, text, docType, OrderNo,  combotext = string.Empty;

        private void dgApproveQuotes_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmSales frm = new frmSales();

            frm.lblDocNo.Text = this.dgApproveQuotes.CurrentRow.Cells[2].Value.ToString();
            frm.button2.Enabled = false;
            frm.btnCopyQuote.Enabled = false;
            frm.btnSaveQuote.Enabled = false;

            frm.btnSaveOrder.Enabled = false;
            frm.btnPrintSO.Enabled = false;
            frm.btnCopySO.Enabled = false;

            frm.cboItemCode.Enabled = false;
            frm.cboItemDesc.Enabled = false;
            frm.txtQty.Enabled = false;
            frm.txtUnitPrice.Enabled = false;
            frm.txtDiscount.Enabled = false;
            frm.cboPriceList.Enabled = false;

            frm.btnAdd.Enabled = false;
            frm.btnClear.Enabled = false;
            frm.btnInitializeForm.Enabled = false;

            frm.tbTopGrids.Enabled = false;

            int selectedrowindex = dgApproveQuotes.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dgApproveQuotes.Rows[selectedrowindex];

            frm.cboCustName.SelectedValue = 28;//selectedRow.Cells["SageCustID"].Value.ToString();      

            frm.cboCustName_(sender, e);

            if (frm.dgQuotes.Rows.Count > 0)
            {
                String searchingFor = selectedRow.Cells["ORDER_NO"].Value.ToString();
                int rowIndex = 0;

                foreach (DataGridViewRow RW in frm.dgQuotes.Rows)
                {
                    foreach(DataGridViewCell DC in RW.Cells)
                    {
                        if(DC.Value.ToString() == searchingFor)
                        {
                            rowIndex = RW.Index;
                        }
                    }
                }

                frm.dgQuotes.CurrentCell = frm.dgQuotes.Rows[rowIndex].Cells[2];

                frm.dgQuotes.Rows[rowIndex].Selected = true;
                frm.dgQuotes_CellDoubleClick(sender, new DataGridViewCellEventArgs(3, rowIndex));
            }      
            frm.ShowDialog();
        }


        public frmAprrovals()
        {
            InitializeComponent();
        }
      
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e) 
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:  // Pending Quotes
                    {
                        btnLoadUAQuotes_Click(sender, e);
                        break;
                    }

                case 1:  // Pending Orders
                    {
                        btnLoadUASO_Click(sender, e);
                        break;
                    }
            }

        }

        private void btnLoadUAQuotes_Click(object sender, EventArgs e)
        {
            docType = "QUOTE";
            dgQuotesFilter = string.Empty;

            dgQuotesFilter = "AND convert(varchar(10), OD.ORDER_DATE, 112) BETWEEN '" + dtpFromSO.Value.ToString("yyyyMMdd", CultureInfo.InvariantCulture) + "' AND '" + dtpToSO.Value.ToString("yyyyMMdd", CultureInfo.InvariantCulture) + "'";

            //if (cboCustName.SelectedIndex > -1)
            //{
            //    dgSalesOrderFilter = dgSalesOrderFilter + " AND SageCustID =" + cboCustName.SelectedValue;
            //}

            //if (rdBtnSOMine.Checked)
            //{
            //    dgSalesOrderFilter = dgSalesOrderFilter + " AND ENTRYBY='" + activeUSER + "'";
            //}

            //if (chkSOApproved.Checked)
            //{
            //    dgSalesOrderFilter = dgSalesOrderFilter + " AND ISNULL(APPROVAL, FALSE) ='TRUE'";
            //}

            //if (chkSORejected.Checked)
            //{
            //    dgSalesOrderFilter = dgSalesOrderFilter + " AND ISNULL(APPROVAL, FALSE) ='FALSE'";
            //}

            loadDocuments(dgQuotesFilter, docType);
        }

        private void btnLoadUASO_Click(object sender, EventArgs e)
        {
            docType = "SALESORDER";
            dgSalesOrderFilter = string.Empty;

            dgSalesOrderFilter = "AND convert(varchar(10), OD.ORDER_DATE, 112) BETWEEN '" + dtpFromSO.Value.ToString("yyyyMMdd", CultureInfo.InvariantCulture) + "' AND '" + dtpToSO.Value.ToString("yyyyMMdd", CultureInfo.InvariantCulture) + "'";

            //if (cboCustName.SelectedIndex > -1)
            //{
            //    dgSalesOrderFilter = dgSalesOrderFilter + " AND SageCustID =" + cboCustName.SelectedValue;
            //}

            //if (rdBtnSOMine.Checked)
            //{
            //    dgSalesOrderFilter = dgSalesOrderFilter + " AND ENTRYBY='" + activeUSER + "'";
            //}

            //if (chkSOApproved.Checked)
            //{
            //    dgSalesOrderFilter = dgSalesOrderFilter + " AND ISNULL(APPROVAL, FALSE) ='TRUE'";
            //}

            //if (chkSORejected.Checked)
            //{
            //    dgSalesOrderFilter = dgSalesOrderFilter + " AND ISNULL(APPROVAL, FALSE) ='FALSE'";
            //}

            loadDocuments(dgSalesOrderFilter, docType);
        }


        private void loadDocuments(String Filter, String DocType)
        {
            if (DocType == "SALESORDER") //ORDER
            {
                SQL = "	select od.CustName [Cust Name], od.ORDER_SNO,od.ORDER_NO, od.SageCustID, o.EntryBY [Sale Agent], ISNULL(converted, 'FALSE') converted,o.LIneNotes, Approved from WIZ_KENS_OrderDetails o INNER JOIN WIZ_KENS_Order od on od.SNo = O.SNo where o.Approved = 0 and od.ORDER_NO like '%SO%'"; //+ Filter;
                LoadDataGrid(dgUASalesOrders, SQL, "od.ORDER_SNO,converted", "");
                dgUASalesOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            if (DocType == "QUOTE")
            {
                SQL = "select od.CustName [Cust Name], od.ORDER_SNO,od.ORDER_NO, od.SageCustID, o.EntryBY [Sale Agent], ISNULL(converted, 'FALSE') converted,o.LIneNotes, Approved from WIZ_KENS_OrderDetails o INNER JOIN WIZ_KENS_Order od on od.SNo = O.SNo where o.Approved = 0 and od.ORDER_NO like '%SQ%'"; //+ Filter;
                LoadDataGrid(dgApproveQuotes, SQL, "od.ORDER_SNO,converted", "");
                dgApproveQuotes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }
    }
}
