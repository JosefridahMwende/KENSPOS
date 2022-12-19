using KENSPOS.MODULES;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static KENSPOS.MODULES.CommonFunctions;
using Pastel.Evolution;

namespace KENSPOS.FORMS
{
    public partial class frmSales : Form
    {
        public string SessionVariable = string.Empty;
        private string SQL, dgItemFilter, dgSalesOrderFilter, dgQuoteFilter, text, docType, OrderNo, DefaultPriceList, combotext = string.Empty;
        private Boolean QuoteFlag, ConvertedToSOFlag = true;
        private Int64 OrderSNo = 0;
        private DataTable DT;
        private DataSet dtSet;

        // Bind default keywords
        List<string> listOriginal = new List<string>();
        // save new keywords
        List<string> listNew = new List<string>();


        public frmSales()
        {
            InitializeComponent();
        }

        private void LoadStock()
        {
            var criteria = $"StockLink > {"1"}";
            var stock = InventoryItem.List(criteria);
            //foreach (DataRow matchP in stock.Rows)
            //{
            //    InventoryItem inventory = new InventoryItem((Int32)matchP["StockLink"]);
            //}

            //String test = InventoryItem.List(criteria).Rows[0]["StockingUnit"].ToString();
            //Qua
        }

        private void loadDefaultValues()
        {
            lblPriceListValue.Text = "Price A";
            lblQtyAvailableValue.Text = "100";
            lblDefaultPriceValue.Text = "200.00";
            txtUnitPrice.Text = lblDefaultPriceValue.Text;
        }

        private void frmSales_Load(object sender, EventArgs e)
        {
            splitContMain.SplitterDistance = (int)(splitContMain.ClientSize.Height * 0.45);
            splitContTop.SplitterDistance = (int)(splitContTop.ClientSize.Height * 0.75);
            splitContTopDetails.SplitterDistance = (int)(splitContTopDetails.ClientSize.Width * 0.50);
            splitContTopGrid.SplitterDistance = (int)(splitContTopGrid.ClientSize.Width * 0.35);
            splitContTopSO.SplitterDistance = (int)(splitContTopSO.ClientSize.Width * 0.30);
            splitContBottom.SplitterDistance = (int)(splitContBottom.ClientSize.Height * 0.65);

            ToolStrip1.Height = splitContBottom.Panel2.Top + 25;
            dgDetails.Top = ToolStrip1.Top + ToolStrip1.Height + 5;
            dgDetails.Height = splitContBottom.Panel2.Top - (ToolStrip1.Top + ToolStrip1.Height);
            //ToolStrip1

            StatusBarServer.Text = "Server : " + SageServerName;
            StatusBarDB.Text = " |  Database : " + SageDBName;
            StatusActiveUSER.Text = " |  User : " + activeUSER;
            StatusVerion.Text = " |  " + AppVersion;
            LicenceType.Text = " |  Licence Type : " + AppLicenceType;
            DaysRemaining.Text = " |  Remaining Days :  " + AppDaysRemaining.ToString();

            Guid myuuid = Guid.NewGuid();
            SessionVariable = myuuid.ToString();

            loadDataset();
            //LoadStock();
            loadCustomers();
            loadStockItems();
            loadPriceList();
            rdBtnQuoteMine.Checked = true;
            rdBtnSOMine.Checked = true;
            dtpSOFrom.Value = DateTime.Now.AddDays(-7);
            dtpSOTo.Value = DateTime.Now;
            dtpQuoteFrom.Value = DateTime.Now.AddDays(-7);
            dtpQuoteTo.Value = DateTime.Now;

            tbTopGrids.SelectedIndex = 0;
            tbTopGrids_SelectedIndexChanged(sender, e);

            if (QuoteFlag) SoButtons(false);

        }

        private void SoButtons(bool value)
        {
            btnSaveOrder.Enabled = value;
            btnPrintSO.Enabled = value;
            btnCopySO.Enabled = value;
        }


        private void loadDataset()
        {
            try
            {
                //DT = new DataTable("Customers");
                //SQL = "SELECT C.DCLink, C.Name FROM Client c ORDER BY c.Name";
                //LoadDatatable(SQL, DT);

                DataTable DTCust = new DataTable();
                DTCust = getCustomersSdk();
                DTCust.TableName = "Customers";

                // Create a new DataSet
                dtSet = new DataSet();

                // Add cust Table to the DataSet.
                dtSet.Tables.Add(DTCust);

                //DT = new DataTable("Stock");
                //SQL = "SELECT s.StockLink, s.Description_1, s.Description_3 FROM StkItem s";
                //LoadDatatable(SQL, DT);

                DataTable DTStock = new DataTable();
                DTStock = getStockSdk();
                DTStock.TableName = "Stock";
                // Add stock table to the DataSet.
                dtSet.Tables.Add(DTStock);
            }
            catch (Exception ex)
            {
                frmError err = new frmError();
                err.txtError.Text = ex.Message.ToString();
                err.ShowDialog();
            }

        }

        private void loadCustomers()
        {
            cboCustName.DisplayMember = "NAME";
            cboCustName.ValueMember = "DCLink";
            cboCustName.DataSource = dtSet.Tables["Customers"];
            //cboCustName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //cboCustName.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboCustName.SelectedIndex = -1;

        }

        private void comboboxchange(object sender, EventArgs e)
        {
            if (((sender as ComboBox).SelectedIndex == -1 || (sender as ComboBox).SelectedIndex == 0) && text != null)
            {
                for (int i = 0; i < (sender as ComboBox).Items.Count; i++)
                {
                    if (text.Equals((sender as ComboBox).Items[i].ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        (sender as ComboBox).SelectedIndex = i;
                        break;
                    }
                }
                text = null;
            }
        }

        private void cboItemCode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            combotext = "";

            comboboxchange(sender, e);
            cboItemCode_(sender, e);

            //loadDefaultValues();
        }

        private void cboItemCode_(object sender, EventArgs e)
        {
            if (cboItemCode.SelectedIndex > -1)
            {
                //cboItemDesc.SelectedIndex = cboItemCode.SelectedIndex;
                //SQL = "select fInclPrice from dbo._etblPriceListPrices where iStockID=" + cboItemCode.SelectedValue;

                InventoryItem inventory = getStockDetailsSdk((Int32)cboItemCode.SelectedValue);
                txtUnitPrice.Text = inventory.DefaultSellingPrice.ToString("n2");
                lblDefaultPriceValue.Text = txtUnitPrice.Text;
                lblQtyAvailableValue.Text = inventory.QtyOnHand.ToString("n2");
                lblUOMValue.Text = inventory.StockingUnit.Code.ToString();
            }

            if (cboItemCode.SelectedIndex != -1)
            {
                cboItemDesc.SelectedIndex = cboItemCode.SelectedIndex;
            }
        }

        private void loadStockItems()
        {
            //Load item desc combobox
            cboItemDesc.ValueMember = "StockLink";
            cboItemDesc.DisplayMember = "Description_1";
            cboItemDesc.DataSource = dtSet.Tables["Stock"];
            //cboItemDesc.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //cboItemDesc.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboItemDesc.SelectedIndex = -1;

            //Load item code combobox
            cboItemCode.ValueMember = "StockLink";
            cboItemCode.DisplayMember = "Description_3";
            cboItemCode.DataSource = dtSet.Tables["Stock"];
            //cboItemCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //cboItemCode.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboItemCode.SelectedIndex = -1;
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            //if (txtQty.Text.Length > 0) calcTotalAmount();
        }

        private void txtUnitPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cboCustName.SelectedIndex == -1)
            {
                MessageBox.Show("Kindly choose customer.", "Form Sales", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboCustName.Select();
                return;

            }

            if (txtQty.Text.Length == 0 || txtQty.Text == "0")
            {
                MessageBox.Show("Quantity is required and cannot be zero", "Form Sales", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQty.Select();
                return;
            }

            if (txtUnitPrice.Text.Length == 0 || txtUnitPrice.Text == "0.00")
            {
                MessageBox.Show("Unit price is required and cannot be zero", "Form Sales", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUnitPrice.Select();
                return;
            }

            if (lblTotalAmountValue.Text.Length == 0 || lblTotalAmountValue.Text == "0.00")
            {
                MessageBox.Show("Total amount is required and cannot be zero", "Form Sales", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            saveOrderDetails();

            initializeFields();

        }

        private void saveOrderDetails()
        {
            //Check if it's a new order/quote
            if (lblDocNo.Text.Length == 0)
            {
                OrderSNo = -1;
            }

            SQL = "select fInclPrice from dbo._etblPriceListPrices where iStockID=" + cboItemCode.SelectedValue;
            Double UnitExclPrice, VAT, UnitInclPrice, QTY, Discount, UnitDiscount, DefaultPrice = 0;
            UnitInclPrice = FetchDoubleValue(SQL, true, false);

            //Simulation
            UnitInclPrice = Convert.ToDouble(txtUnitPrice.Text);
            DefaultPrice = Convert.ToDouble(lblDefaultPriceValue.Text);

            Discount = Convert.ToDouble(txtDiscount.Text);
            UnitDiscount = DefaultPrice - UnitInclPrice;

            VAT = Math.Round(UnitInclPrice - (UnitInclPrice * 100) / 116, 1);

            UnitExclPrice = UnitInclPrice - VAT;

            QTY = Convert.ToDouble(txtQty.Text);


            SQL = "INSERT INTO WIZ_KENS_OrderDetails(DefaultPriceList, OrderSNo, StockLink, DefaultPrice, Qty, QtyConfirmed, VAT, UnitExcl, UnitVAT, DiscPercent,UnitDiscount , TotaDiscount, UnitIncl, TotalExcl, TotalVAT, TotalIncl,SessionVar,EntryBy, Approved ) VALUES('" + lblPriceListValue.Text + "'," + OrderSNo + "," + cboItemCode.SelectedValue + "," + DefaultPrice + "," + QTY + "," + QTY + ",16," + UnitExclPrice + "," + VAT + "," + Discount + ", " + UnitDiscount + "," + UnitDiscount * QTY + "," + UnitInclPrice + ", " + UnitExclPrice * QTY + ", " + VAT * QTY + "," + UnitInclPrice * QTY + ",'" + SessionVariable + "','" + activeUSER + "','TRUE')";

            if (!ExecuteQuery(SQL))
            {
                MessageBox.Show("Error occured while saving data. Contact Administrator.", "Form Sales", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (lblDocNo.Text.Length == 0)
            {
                dgItemFilter = "  WHERE d.OrderSNo = -1 AND d.EntryBy = '" + activeUSER + "' AND SessionVar='" + SessionVariable + "'";
            }
            else
            {
                dgItemFilter = "  WHERE d.OrderSNo = " + OrderSNo + " AND d.EntryBy = '" + activeUSER + "' AND SessionVar='" + SessionVariable + "' ";
            }
            Recalculate();
            loadItemDetails();
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmCashier frm = new frmCashier();
            frm.ShowDialog();
        }

        private void cboItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && cboItemCode.SelectedIndex > -1) cboItemCode_SelectionChangeCommitted(sender, e);
        }

        private void cboCustName_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter && cboCustName.SelectedIndex > -1) cboCustName_SelectionChangeCommitted(sender, e);

            if (e.KeyCode == Keys.Escape && !cboCustName.DroppedDown)
            {

            }
        }

        private void cboCustName_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //if (e.KeyCode == Keys.Return)
            //{
            //    text = this.cboCustName.Text;
            //}
        }

        private void cboItemDesc_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void cboItemDesc_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                text = this.cboItemDesc.Text;
            }
        }

        private void cboItemDesc_SelectionChangeCommitted(object sender, EventArgs e)
        {
            combotext = "";
            comboboxchange(sender, e);
            //if (this.cboItemDesc.SelectedIndex == -1 && text != null)
            //{
            //    for (int i = 0; i < this.cboItemDesc.Items.Count; i++)
            //    {
            //        if (text.Equals(this.cboItemDesc.Items[i].ToString(), StringComparison.OrdinalIgnoreCase))
            //        {
            //            this.cboItemDesc.SelectedIndex = i;
            //            break;
            //        }
            //    }
            //    text = null;
            //}

            if (cboItemDesc.SelectedIndex != -1)
            {
                cboItemCode.SelectedIndex = cboItemDesc.SelectedIndex;
            }

            if (cboItemDesc.SelectedIndex > -1)
            {
                //cboItemDesc.SelectedIndex = cboItemCode.SelectedIndex;
                //SQL = "select fExclPrice from dbo._etblPriceListPrices where iStockID=" + cboItemDesc.SelectedValue;
                //txtUnitPrice.Text = FetchDoubleValue(SQL, true, false).ToString("n2");
                //lblDefaultPriceValue.Text = txtUnitPrice.Text;

                InventoryItem inventory = getStockDetailsSdk((Int32)cboItemDesc.SelectedValue);
                txtUnitPrice.Text = inventory.DefaultSellingPrice.ToString("n2");
                lblDefaultPriceValue.Text = txtUnitPrice.Text;
                lblQtyAvailableValue.Text = inventory.QtyOnHand.ToString("n2");
                lblUOMValue.Text = inventory.StockingUnit.Code.ToString();
            }

            // loadDefaultValues();
        }

        private void txtUnitPrice_Leave(object sender, EventArgs e)
        {
            if (txtUnitPrice.Text.Length > 0 && lblDefaultPriceValue.Text.Length > 0)
            {
                txtUnitPrice.Text = CalculateCeilingNew(Convert.ToDouble(txtUnitPrice.Text)).ToString("n2");

                Double QTY, UnitPrice, Discount, DefaultPrice, totalAmount = 0;

                DefaultPrice = Convert.ToDouble(lblDefaultPriceValue.Text);
                QTY = Convert.ToDouble(txtQty.Text);
                UnitPrice = Convert.ToDouble(txtUnitPrice.Text);
                Discount = CalculateCeilingNew(100 * (DefaultPrice - UnitPrice) / DefaultPrice);//Math.Round((100*(DefaultPrice- UnitPrice)/ DefaultPrice),1);
                totalAmount = UnitPrice * QTY;
                txtDiscount.Text = Discount.ToString("n2");
                lblTotalAmountValue.Text = totalAmount.ToString("n2");
            }
        }

        private double CalculateCeiling(Double UnitPrice)
        {
            double multipler = Math.Pow(10, Convert.ToDouble(1));
            return Math.Ceiling(UnitPrice * multipler) / multipler;
        }

        private double CalculateCeilingNew(Double Price)
        {
            return Math.Ceiling(Price * 20) / 20;
            //return Math.Ceiling((Math.Round(Price * 20, MidpointRounding.AwayFromZero)/20));            
        }

        private void frmSales_FormClosing(object sender, FormClosingEventArgs e)
        {
            SQL = "DELETE FROM WIZ_KENS_OrderDetails WHERE SessionVar='" + SessionVariable + "' and EntryBy='" + activeUSER + "' and OrderSNo=-1";
            ExecuteQuery(SQL);
        }

        private void btnSaveOrder_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are your sure you want to save the sales order", "Save sales order Confirmation", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (lblDocNo.Text.Length > 0)
                {
                    docType = "ORDER";
                    saveNewOrder(docType, sender, e, GetDocNo(lblDocNo.Text));
                }
                else
                {
                    MessageBox.Show("Invalid document type. Cannont be converted to sales order. Please contact the adminstartor.", "Error in sales", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private int GetDocNo(String DocTypeValue)
        {
            string a = DocTypeValue;
            string b = string.Empty;
            int val = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (Char.IsDigit(a[i]))
                    b += a[i];
            }

            if (b.Length > 0)
                val = int.Parse(b);

            return val;
        }

        private string FetchSettingsQuery(String whse, String Doc)
        {
            String Query = string.Empty;
            if (Doc == "ORDER")
            {
                switch (whse)
                {
                    case "ATH":
                        // code block
                        Query = "SELECT ISNULL(s.ATHNextSalesOrderNo, 1) [SNO], ISNULL(s.SalesOrderPrefix, 'SO-') + REPLICATE('0',S.[SalesOrderNoLen]-LEN(RTRIM(ATHNextSalesOrderNo))) + RTRIM(ATHNextSalesOrderNo) [ORDER_NO] FROM WIZ_KENS_SETTINGS S";
                        break;
                    case "MSA":
                        // code block
                        Query = "SELECT ISNULL(s.MSANextSalesOrderNo, 1) [SNO], ISNULL(s.SalesOrderPrefix, 'SO-') + REPLICATE('0',S.[SalesOrderNoLen]-LEN(RTRIM(MSANextSalesOrderNo))) + RTRIM(MSANextSalesOrderNo) [ORDER_NO] FROM WIZ_KENS_SETTINGS S";
                        break;
                    case "LSK":
                        // code block
                        Query = "SELECT ISNULL(s.LSKNextSalesOrderNo, 1) [SNO], ISNULL(s.SalesOrderPrefix, 'SO-') + REPLICATE('0',S.[SalesOrderNoLen]-LEN(RTRIM(LSKNextSalesOrderNo))) + RTRIM(LSKNextSalesOrderNo) [ORDER_NO] FROM WIZ_KENS_SETTINGS S";
                        break;
                }
            }
            else if (Doc == "QUOTE")
            {
                switch (whse)
                {
                    case "ATH":
                        // code block
                        Query = "SELECT ISNULL(s.ATHNextQuoteNo, 1) [SNO], ISNULL(s.QuoteNoPrefix, 'SQ-') + REPLICATE('0',S.[QuoteNoLen]-LEN(RTRIM(ATHNextQuoteNo))) + RTRIM(ATHNextQuoteNo) [ORDER_NO] FROM WIZ_KENS_SETTINGS S";
                        break;
                    case "MSA":
                        // code block
                        Query = "SELECT ISNULL(s.MSANextQuoteNo, 1) [SNO], ISNULL(s.QuoteNoPrefix, 'SQ-') + REPLICATE('0',S.[QuoteNoLen]-LEN(RTRIM(MSANextQuoteNo))) + RTRIM(MSANextQuoteNo) [ORDER_NO] FROM WIZ_KENS_SETTINGS S";
                        break;
                    case "LSK":
                        // code block
                        Query = "SELECT ISNULL(s.LSKNextQuoteNo, 1) [SNO], ISNULL(s.QuoteNoPrefix, 'SQ-') + REPLICATE('0',S.[QuoteNoLen]-LEN(RTRIM(LSKNextQuoteNo))) + RTRIM(LSKNextQuoteNo) [ORDER_NO] FROM WIZ_KENS_SETTINGS S";
                        break;
                }
            }

            return Query;
        }

        private void saveNewOrder(String DocType, object sender, EventArgs e, Int64 QuoteNo = 0)
        {
            if (DocType == "ORDER")
            {
                SQL = FetchSettingsQuery(activeUSERWhse, DocType);
            }
            else if (DocType == "QUOTE")
            {
                SQL = FetchSettingsQuery(activeUSERWhse, DocType);
            }
            else
            {
                MessageBox.Show("Document Type not specified. Can't be saved. Please contact the adminstartor.", "Error in sales", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DT = new DataTable();
            LoadDatatable(SQL, DT);

            if (DT.Rows.Count > 0)
            {
                OrderSNo = Convert.ToInt64(DT.Rows[0]["SNO"].ToString());
                OrderNo = DT.Rows[0]["ORDER_NO"].ToString();
                DT = null;

                //Increment Next Doc number
                if (DocType == "ORDER")
                {
                    switch (activeUSERWhse)
                    {
                        case "ATH":
                            SQL = "Update WIZ_KENS_SETTINGS Set ATHNextSalesOrderNo = ATHNextSalesOrderNo + 1";
                            break;
                        case "MSA":
                            SQL = "Update WIZ_KENS_SETTINGS Set MSANextSalesOrderNo = MSANextSalesOrderNo + 1";
                            break;
                        case "LSK":
                            SQL = "Update WIZ_KENS_SETTINGS Set LSKNextSalesOrderNo = LSKNextSalesOrderNo + 1";
                            break;
                    }

                }
                else if (DocType == "QUOTE")
                {
                    switch (activeUSERWhse)
                    {
                        case "ATH":
                            SQL = "Update WIZ_KENS_SETTINGS Set ATHNextQuoteNo = ATHNextQuoteNo + 1";
                            break;
                        case "MSA":
                            SQL = "Update WIZ_KENS_SETTINGS Set MSANextQuoteNo = MSANextQuoteNo + 1";
                            break;
                        case "LSK":
                            SQL = "Update WIZ_KENS_SETTINGS Set LSKNextQuoteNo = LSKNextQuoteNo + 1";
                            break;
                    }
                }

                ExecuteQuery(SQL);
            }
            else
            {
                MessageBox.Show("Error encountered while saving document. Please contact the adminstartor.", "Error in sales", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DateTime date = DateTime.Now;
            //String OrderDate = date.ToString("yyyy-MM-dd h:mm:ss tt");


            dgItemFilter = "  WHERE d.DocType='" + DocType + "' AND d.OrderSNo = " + OrderSNo + " AND d.EntryBy = '" + activeUSER + "' AND SessionVar='" + SessionVariable + "' ";


            //Allow for updating/editing of an order without creating another order
            if (DocType == "QUOTE")
            {
                lblDocNo.Text = String.Format("{0}-{1}", activeUSERWhse, OrderNo);
                SQL = "INSERT INTO [dbo].[WIZ_KENS_Order]([IsCashCustomer],[SageCustID],[CustName],[Mobile],[Address1],[Address2] ,[Address3],[ORDER_SNO],[ORDER_NO],[ORDER_DATE],[STATUS],[docType],[EntryBY],[APPROVAL],[ConvertedOrderSNo],[Comments]) VALUES ('TRUE'," + cboCustName.SelectedValue + ",'" + cboCustName.Text + "','','" + lblAddress1Value.Text + "','" + lblAddress2Value.Text + "', '" + lblAddress3Value.Text + "'," + OrderSNo + ",'" + lblDocNo.Text.ToString() + "','" + date.ToString("yyyy-MM-dd h:mm:ss tt") + "','NEW', '" + DocType + "', '" + activeUSER + "',1,'','')";

                if (ExecuteQuery(SQL))
                {
                    SQL = "UPDATE WIZ_KENS_OrderDetails Set OrderSNo = " + OrderSNo + ", docType='" + docType + "' WHERE OrderSNo = -1 And  EntryBy='" + activeUSER + "' and SessionVar='" + SessionVariable + "'";

                    if (ExecuteQuery(SQL))
                    {

                        MessageBox.Show("Quote No :" + lblDocNo.Text.ToString() + " saved sucessfully.", "Sales", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tbTopGrids.SelectedIndex = 0;
                        tbTopGrids_SelectedIndexChanged(sender, e);
                        initializeForm();
                        dgDetails.DataSource = null;
                        cboCustName.SelectedIndex = -1;
                        //SoButtons(true);
                        //loadItemDetails();

                    }

                }
                else
                {
                    MessageBox.Show("Error encountered while saving document. Please contact the adminstrator.", "Error in sales", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (DocType == "ORDER")
            {
                SQL = "INSERT INTO WIZ_KENS_OrderDetails(DefaultPriceList, OrderSNo, StockLink, DefaultPrice, Qty, QtyConfirmed, VAT, UnitExcl, UnitVAT, DiscPercent,UnitDiscount , TotaDiscount, UnitIncl, TotalExcl, TotalVAT, TotalIncl,SessionVar,EntryBy, Approved, DocType ) SELECT DefaultPriceList, " + OrderSNo + ", StockLink, DefaultPrice, Qty, QtyConfirmed, VAT, UnitExcl, UnitVAT, DiscPercent, UnitDiscount, TotaDiscount, UnitIncl, TotalExcl, TotalVAT, TotalIncl, '" + SessionVariable + "', '" + activeUSER + "', Approved, 'ORDER' FROM WIZ_KENS_OrderDetails WHERE docType = 'QUOTE' AND SessionVar='" + SessionVariable + "' AND OrderSNo = " + QuoteNo;

                if (ExecuteQuery(SQL))
                {
                    lblDocNo.Text = String.Format("{0}-{1}", activeUSERWhse, OrderNo);
                    SQL = "INSERT INTO [dbo].[WIZ_KENS_Order]([IsCashCustomer],[SageCustID],[CustName],[Mobile],[Address1],[Address2] ,[Address3],[ORDER_SNO],[ORDER_NO],[ORDER_DATE],[STATUS],[docType],[EntryBY],[APPROVAL],[ConvertedOrderSNo],[Comments]) VALUES ('TRUE'," + cboCustName.SelectedValue + ",'" + cboCustName.Text + "','','" + lblAddress1Value.Text + "','" + lblAddress2Value.Text + "', '" + lblAddress3Value.Text + "'," + OrderSNo + ",'" + lblDocNo.Text.ToString() + "','" + date.ToString("yyyy-MM-dd h:mm:ss tt") + "','NEW', '" + DocType + "', '" + activeUSER + "',1," + QuoteNo + ",'')";

                    if (ExecuteQuery(SQL))
                    {
                        UpdatedConvertedQuote(QuoteNo);
                        MessageBox.Show("Order No :" + lblDocNo.Text.ToString() + " saved sucessfully.", "Sales", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        tbTopGrids.SelectedIndex = 0;
                        tbTopGrids_SelectedIndexChanged(sender, e);
                        initializeForm();
                        dgDetails.DataSource = null;
                        cboCustName.SelectedIndex = -1;
                    }
                }
            }

        }

        private void UpdatedConvertedQuote(Int64 QuoteNo)
        {
            SQL = "UPDATE wiz_kens_order SET converted='TRUE' WHERE docType='QUOTE' AND ORDER_SNO=" + QuoteNo;
            ExecuteQuery(SQL);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            initializeFields();
        }

        private void btnLoadQuotes_Click(object sender, EventArgs e)
        {
            docType = "QUOTE";
            dgQuoteFilter = string.Empty;

            dgQuoteFilter = " WHERE docType='QUOTE' AND convert(varchar(10), ORDER_DATE, 112) BETWEEN '" + dtpQuoteFrom.Value.ToString("yyyyMMdd", CultureInfo.InvariantCulture) + "' AND '" + dtpQuoteTo.Value.ToString("yyyyMMdd", CultureInfo.InvariantCulture) + "'";

            if (cboCustName.SelectedIndex > -1)
            {
                dgQuoteFilter = dgQuoteFilter + " AND SageCustID =" + cboCustName.SelectedValue;
            }

            if (rdBtnQuoteMine.Checked)
            {
                dgQuoteFilter = dgQuoteFilter + " AND ENTRYBY='" + activeUSER + "'";
            }

            if (chkQuoteApproved.Checked)
            {
                dgQuoteFilter = dgQuoteFilter + " AND ISNULL(APPROVAL, FALSE) ='TRUE'";
            }

            if (chkQuoteRejected.Checked)
            {
                dgQuoteFilter = dgQuoteFilter + " AND ISNULL(APPROVAL, FALSE) ='FALSE'";
            }

            loadDocuments(dgQuoteFilter, docType);
        }

        private void btnLoadSO_Click(object sender, EventArgs e)
        {
            docType = "ORDER";
            dgSalesOrderFilter = string.Empty;

            dgSalesOrderFilter = " WHERE docType='ORDER' AND convert(varchar(10), ORDER_DATE, 112) BETWEEN '" + dtpSOFrom.Value.ToString("yyyyMMdd", CultureInfo.InvariantCulture) + "' AND '" + dtpSOTo.Value.ToString("yyyyMMdd", CultureInfo.InvariantCulture) + "'";

            if (cboCustName.SelectedIndex > -1)
            {
                dgSalesOrderFilter = dgSalesOrderFilter + " AND SageCustID =" + cboCustName.SelectedValue;
            }

            if (rdBtnSOMine.Checked)
            {
                dgSalesOrderFilter = dgSalesOrderFilter + " AND ENTRYBY='" + activeUSER + "'";
            }

            if (chkSOApproved.Checked)
            {
                dgSalesOrderFilter = dgSalesOrderFilter + " AND ISNULL(APPROVAL, FALSE) ='TRUE'";
            }

            if (chkSORejected.Checked)
            {
                dgSalesOrderFilter = dgSalesOrderFilter + " AND ISNULL(APPROVAL, FALSE) ='FALSE'";
            }

            loadDocuments(dgSalesOrderFilter, docType);
        }

        private void tbTopGrids_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tbTopGrids.SelectedIndex)
            {
                case 0:  // Pending Quotes
                    {
                        btnLoadQuotes_Click(sender, e);
                        break;
                    }

                case 1:  // Pending Orders
                    {
                        btnLoadSO_Click(sender, e);
                        break;
                    }
            }
        }

        public void dgQuotes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || dgQuotes.RowCount == 0) return;

            loadDocumentDetails("QUOTE", dgQuotes, sender, e);
        }

        private void dgSalesOrders_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || dgSalesOrders.RowCount == 0) return;
            loadDocumentDetails("ORDER", dgSalesOrders, sender, e);
        }

        private void loadDocumentDetails(String DocType, DataGridView dg, object sender, DataGridViewCellEventArgs e)
        {
            int selectedrowindex = dg.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dg.Rows[selectedrowindex];
            OrderSNo = Convert.ToInt64(selectedRow.Cells["ORDER_SNO"].Value);
            ConvertedToSOFlag = Convert.ToBoolean(selectedRow.Cells["converted"].Value);
            cboCustName.SelectedValue = Convert.ToInt64(selectedRow.Cells["SageCustID"].Value);
            //cboCustName_SelectionChangeCommitted(sender, e);

            lblDocNo.Text = String.Format("{0}", selectedRow.Cells["Order No"].Value.ToString());

            dgItemFilter = "WHERE OrderSNo = " + OrderSNo + " AND docType='" + DocType + "'";
            loadItemDetails();
            Recalculate();
            SoButtons(true);

            if (ConvertedToSOFlag)
            {
                btnSaveQuote.Enabled = false;
                btnAdd.Enabled = false;
            }
            else
            {
                btnSaveQuote.Enabled = true;
                btnAdd.Enabled = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void dgDetails_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || dgDetails.RowCount == 0) return;

            int selectedrowindex = dgDetails.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dgDetails.Rows[selectedrowindex];
            Int64 RowId = Convert.ToInt64(selectedRow.Cells["sno"].Value);

            cboItemDesc.SelectedValue = Convert.ToInt64(selectedRow.Cells["StockLink"].Value);
            cboItemCode.SelectedIndex = cboItemDesc.SelectedIndex;
            lblDefaultPriceValue.Text = selectedRow.Cells["Default Price"].Value.ToString();
            lblQtyAvailableValue.Text = selectedRow.Cells["Qty Avail."].Value.ToString();
            txtQty.Text = selectedRow.Cells["Qty"].Value.ToString();
            txtUnitPrice.Text = selectedRow.Cells["Unit Incl"].Value.ToString();
            txtDiscount.Text = selectedRow.Cells["DiscPercent"].Value.ToString();
            lblTotalAmountValue.Text = selectedRow.Cells["Total Incl"].Value.ToString();

            OrderSNo = Convert.ToInt64(selectedRow.Cells["OrderSNo"].Value);

            SQL = "DELETE FROM WIZ_KENS_OrderDetails WHERE SNo=" + RowId;
            ExecuteQuery(SQL);

            if (lblDocNo.Text.Length == 0)
            {
                dgItemFilter = "  WHERE d.OrderSNo = -1 AND d.EntryBy = '" + activeUSER + "' AND SessionVar='" + SessionVariable + "'";
            }
            else
            {
                dgItemFilter = "  WHERE d.OrderSNo = " + OrderSNo + " AND d.EntryBy = '" + activeUSER + "' AND SessionVar='" + SessionVariable + "' ";
            }
            Recalculate();
            loadItemDetails();

        }

        private void toolStripbtnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are your sure you want to delete item(s)", "Delete Confirmation", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                int RowCount = 0;
                if (dgDetails.Rows.Count > 0)
                {
                    foreach (DataGridViewRow rw in dgDetails.Rows)
                    {
                        if (Convert.ToBoolean(rw.Cells["chk"].Value))
                        {
                            OrderSNo = Convert.ToInt64(rw.Cells["OrderSNo"].Value);
                            SQL = "DELETE FROM WIZ_KENS_OrderDetails WHERE SNo=" + Convert.ToInt64(rw.Cells["sno"].Value);
                            ExecuteQuery(SQL);
                            RowCount++;
                        }
                    }

                    if (RowCount > 0)
                    {
                        MessageBox.Show("Deleted : " + RowCount + " out of " + dgDetails.Rows.Count + " rows successfully.", "Sales", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (lblDocNo.Text.Length == 0)
                        {
                            dgItemFilter = "  WHERE d.OrderSNo = -1 AND d.EntryBy = '" + activeUSER + "' AND SessionVar='" + SessionVariable + "'";
                        }
                        else
                        {
                            dgItemFilter = "  WHERE d.OrderSNo = " + OrderSNo + " AND d.EntryBy = '" + activeUSER + "' AND SessionVar='" + SessionVariable + "' ";
                        }
                        Recalculate();
                        loadItemDetails();
                    }

                }
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }

        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            if (dgDetails.Rows.Count > 0)
            {
                foreach (DataGridViewRow rw in dgDetails.Rows)
                {
                    rw.Cells["chk"].Value = true;
                }
            }
        }

        private void btnUnselectAll_Click(object sender, EventArgs e)
        {
            if (dgDetails.Rows.Count > 0)
            {
                foreach (DataGridViewRow rw in dgDetails.Rows)
                {
                    rw.Cells["chk"].Value = false;
                }
            }
        }

        private void dgDetails_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgDetails.IsCurrentCellDirty)
            {
                dgDetails.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void btnSaveQuote_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are your sure you want to save the quote", "Save quote Confirmation", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                docType = "QUOTE";
                saveNewOrder(docType, sender, e);
            }

        }

        private void loadDocuments(String Filter, String DocType)
        {
            SQL = "select CustName [Cust Name], ORDER_NO [Order No], ORDER_SNO,SageCustID, EntryBY [Sale Agent], ISNULL(converted, 'FALSE') converted from WIZ_KENS_Order " + Filter;

            if (DocType == "ORDER")
            {
                LoadDataGrid(dgSalesOrders, SQL, "ORDER_SNO,SageCustID,converted", "");
                dgSalesOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

            if (DocType == "QUOTE")
            {
                LoadDataGrid(dgQuotes, SQL, "ORDER_SNO,SageCustID", "");
                dgQuotes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }


        }


        private void initializeForm()
        {
            lblDocNo.Text = string.Empty;
            //cboCustName.SelectedIndex = -1;
            lblAddress1Value.Text = string.Empty;
            lblAddress2Value.Text = string.Empty;
            lblAddress3Value.Text = string.Empty;
            lblPriceListValue.Text = string.Empty;
            lblCustTypeValue.Text = string.Empty;
            lblOsBalValue.Text = string.Empty;
            initializeFields();
            tbLPONo.Text = string.Empty;
            cbDeliveryMethod.SelectedIndex = -1;
            tbRegNo.Text = string.Empty;
            tbDeliveryLocation.Text = string.Empty;
            //dgDetails.DataSource = null;

        }

        private void initializeQuoteSelect()
        {
            lblDocNo.Text = string.Empty;
            cboCustName.SelectedIndex = -1;
            lblAddress1Value.Text = string.Empty;
            lblAddress2Value.Text = string.Empty;
            lblAddress3Value.Text = string.Empty;
            lblPriceListValue.Text = string.Empty;
            lblCustTypeValue.Text = string.Empty;
            lblOsBalValue.Text = string.Empty;
            initializeFields();
            btnAdd.Enabled = true;
            btnSaveQuote.Enabled = true;
            //dgDetails.DataSource = null;

        }

        private void cboItemDesc_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                this.cboItemDesc.DataSource = null;

                String Test = this.cboItemDesc.Text;

                combotext = this.cboItemDesc.Text;

                DataTable orders = dtSet.Tables["Stock"];

                EnumerableRowCollection<DataRow> query = from order in orders.AsEnumerable()
                                                         where order.Field<String>("Description_1").ToUpper().Contains(this.cboItemDesc.Text.ToUpper())
                                                         select order;

                DataView view = query.AsDataView();

                this.cboItemDesc.DisplayMember = "Description_1";
                this.cboItemDesc.ValueMember = "StockLink";
                this.cboItemDesc.DataSource = view;

                this.cboItemDesc.SelectedIndex = -1;
                this.cboItemDesc.Text = combotext;
                this.cboItemDesc.SelectionStart = this.cboItemDesc.Text.Length;
                Cursor = Cursors.Default;
                // Automatically pop up drop-down
                this.cboItemDesc.DroppedDown = true;
            }
            catch (Exception ex)
            {
                frmError err = new frmError();
                err.txtError.Text = ex.Message.ToString();
                err.ShowDialog();
            }
        }

        private void btnCopyQuote_Click(object sender, EventArgs e)
        {
            if (cboCustName.SelectedIndex == -1)
            {
                MessageBox.Show("Kindly choose customer.", "Form Sales", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboCustName.Select();
                return;

            }

            if (lblDocNo.Text.Length > 0)
            {
                if (lblDocNo.Text.Contains("SQ"))
                {
                    initializeQuoteSelect();
                    SaveCopiedQuoteItems(OrderSNo);
                    if (lblDocNo.Text.Length == 0)
                    {
                        dgItemFilter = "  WHERE d.OrderSNo = -1 AND d.EntryBy = '" + activeUSER + "' AND SessionVar='" + SessionVariable + "'";
                    }
                    else
                    {
                        dgItemFilter = "  WHERE d.OrderSNo = " + OrderSNo + " AND d.EntryBy = '" + activeUSER + "' AND SessionVar='" + SessionVariable + "' ";
                    }
                    Recalculate();
                    loadItemDetails();
                    SoButtons(false);
                    //saveNewOrder("QUOTE", sender, e, OrderSNo);
                }
            }
            else
            {
                MessageBox.Show("Cannot perform the action. Invalid document.", "Form Sales", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void SaveCopiedQuoteItems(Int64 OrderSNo)
        {

            SQL = "INSERT INTO WIZ_KENS_OrderDetails(DefaultPriceList, OrderSNo, StockLink, DefaultPrice, Qty, QtyConfirmed, VAT, UnitExcl, UnitVAT, DiscPercent,UnitDiscount , TotaDiscount, UnitIncl, TotalExcl, TotalVAT, TotalIncl,SessionVar,EntryBy, Approved, DocType ) SELECT DefaultPriceList,-1, StockLink, DefaultPrice, Qty, QtyConfirmed, VAT, UnitExcl, UnitVAT, DiscPercent, UnitDiscount, TotaDiscount, UnitIncl, TotalExcl, TotalVAT, TotalIncl, '" + SessionVariable + "', '" + activeUSER + "', Approved, 'ORDER' FROM WIZ_KENS_OrderDetails WHERE docType = 'QUOTE' AND OrderSNo = " + OrderSNo;

            if (ExecuteQuery(SQL))
            {
                MessageBox.Show("Quote copied successfully.", "Form Sales", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnInitializeForm_Click(object sender, EventArgs e)
        {
            initializeForm();
            cboCustName.SelectedIndex = -1;
            dgDetails.Columns["Select"].Visible = false;
            dgDetails.DataSource = null;
            tbTopGrids.SelectedIndex = 0;
            tbTopGrids_SelectedIndexChanged(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Generating Preview
            rptMaster.PrintingRequired = true;
            rptMaster.PreviewRequired = false;
            rptMaster.RPT_NAME = @"\Quote.rpt";
            rptMaster.OrderNum = lblDocNo.Text;
            //CommonFunctions.Default_Printer = cboPrinter.Text
            rptMaster.PrepareReport();
        }

        private void btnPrintSO_Click(object sender, EventArgs e)
        {
            //Generating Preview
            rptMaster.PrintingRequired = true;
            rptMaster.PreviewRequired = false;
            rptMaster.RPT_NAME = @"\OrderConfirmation.rpt";
            rptMaster.OrderNum = lblDocNo.Text;
            //CommonFunctions.Default_Printer = cboPrinter.Text
            rptMaster.PrepareReport();
        }

        private void cboItemCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                this.cboItemCode.DataSource = null;

                combotext = this.cboItemCode.Text;

                DataTable orders = dtSet.Tables["Stock"];

                EnumerableRowCollection<DataRow> query = from order in orders.AsEnumerable()
                                                         where order.Field<String>("Description_3").ToUpper().Contains(this.cboItemCode.Text.ToUpper())
                                                         select order;

                DataView view = query.AsDataView();

                this.cboItemCode.DisplayMember = "Description_3";
                this.cboItemCode.ValueMember = "StockLink";
                this.cboItemCode.DataSource = view;

                this.cboItemCode.SelectedIndex = -1;
                this.cboItemCode.Text = combotext;
                this.cboItemCode.SelectionStart = this.cboItemCode.Text.Length;
                Cursor = Cursors.Default;
                // Automatically pop up drop-down
                this.cboItemCode.DroppedDown = true;
            }
            catch (Exception ex)
            {
                frmError err = new frmError();
                err.txtError.Text = ex.Message.ToString();
                err.ShowDialog();
            }
        }

        private void cboCustName_KeyPress(object sender, KeyPressEventArgs e)
        {

            try
            {
                if (e.KeyChar != (char)13)
                {
                    this.cboCustName.DataSource = null;

                    String Test = this.cboCustName.Text;

                    combotext = this.cboCustName.Text;

                    DataTable orders = dtSet.Tables["Customers"];

                    EnumerableRowCollection<DataRow> query = from order in orders.AsEnumerable()
                                                             where order.Field<String>("NAME").ToUpper().Contains(this.cboCustName.Text.ToUpper())
                                                             select order;

                    DataView view = query.AsDataView();

                    this.cboCustName.DisplayMember = "NAME";
                    this.cboCustName.ValueMember = "DCLink";
                    this.cboCustName.DataSource = view;

                    this.cboCustName.SelectedIndex = -1;
                    this.cboCustName.Text = combotext;
                    this.cboCustName.SelectionStart = this.cboCustName.Text.Length;
                    Cursor = Cursors.Default;
                    // Automatically pop up drop-down
                    this.cboCustName.DroppedDown = true;
                }

            }
            catch (Exception ex)
            {
                frmError err = new frmError();
                err.txtError.Text = ex.Message.ToString();
                err.ShowDialog();
            }

        }

        private void cboCustName_Enter(object sender, EventArgs e)
        {
            if (cboCustName.SelectedIndex > -1)
            {

            }
        }

        private void cboCustName_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                //SelectNextControl(tbLPONo, true, false, false, true);
                //cboCustName_SelectionChangeCommitted(sender, e);
            }
        }

        private void cboCustName_PreviewKeyDown_1(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                text = this.cboCustName.Text;
            }
        }

        private void cboItemCode_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                text = this.cboItemCode.Text;
            }
        }

        private void splitContBottom_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void grpCustDetails_Enter(object sender, EventArgs e)
        {

        }

        private void splitContMain_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void splitContTop_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void splitContTopDetails_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void lblDocNo_Click(object sender, EventArgs e)
        {

        }

        private void lblAddress4Value_Click(object sender, EventArgs e)
        {

        }

        private void lblAddress3Value_Click(object sender, EventArgs e)
        {

        }

        private void lblAddress2Value_Click(object sender, EventArgs e)
        {

        }

        private void lblAddress1Value_Click(object sender, EventArgs e)
        {

        }

        private void labltbCreditTermValue_Click(object sender, EventArgs e)
        {

        }

        private void tbDeliveryLocation_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbRegNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void cbDeliveryMethod_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void tbLPONo_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void lblOsBalValue_Click(object sender, EventArgs e)
        {

        }

        private void lblCustTypeValue_Click(object sender, EventArgs e)
        {

        }

        private void lblPriceListValue_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void lblOsBal_Click(object sender, EventArgs e)
        {

        }

        private void lblCustType_Click(object sender, EventArgs e)
        {

        }

        private void lblPriceList_Click(object sender, EventArgs e)
        {

        }

        private void lblAddress1_Click(object sender, EventArgs e)
        {

        }

        private void lblCustName_Click(object sender, EventArgs e)
        {

        }

        private void cboCustName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tbQuotes_Click(object sender, EventArgs e)
        {

        }

        private void splitContTopGrid_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void grpQuotesFilter_Enter(object sender, EventArgs e)
        {

        }

        private void btnClearQuotes_Click(object sender, EventArgs e)
        {

        }

        private void lblQuoteTo_Click(object sender, EventArgs e)
        {

        }

        private void lblQuoteFrom_Click(object sender, EventArgs e)
        {

        }

        private void dtpQuoteTo_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpQuoteFrom_ValueChanged(object sender, EventArgs e)
        {

        }

        private void chkQuoteApproved_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkQuoteRejected_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdBtnQuoteAll_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdBtnQuoteMine_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dgQuotes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tbSalesOrders_Click(object sender, EventArgs e)
        {

        }

        private void splitContTopSO_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnSOClear_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dtpSOTo_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpSOFrom_ValueChanged(object sender, EventArgs e)
        {

        }

        private void chkSOApproved_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkSORejected_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdBtnSOAll_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdBtnSOMine_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dgSalesOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lblUOMValue_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void cboPriceList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblTotalAmountValue_Click(object sender, EventArgs e)
        {

        }

        private void lblQtyAvailableValue_Click(object sender, EventArgs e)
        {

        }

        private void lblDefaultPriceValue_Click(object sender, EventArgs e)
        {

        }

        private void lblUnitPrice_Click(object sender, EventArgs e)
        {

        }

        private void btnSearchPriceList_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void lblTotalAmount_Click(object sender, EventArgs e)
        {

        }

        private void lblQtyAvail_Click(object sender, EventArgs e)
        {

        }

        private void lblDiscount_Click(object sender, EventArgs e)
        {

        }

        private void lblQty_Click(object sender, EventArgs e)
        {

        }

        private void lblItemDesc_Click(object sender, EventArgs e)
        {

        }

        private void lblItemCode_Click(object sender, EventArgs e)
        {

        }

        private void cboItemDesc_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void splitContBottom_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void ToolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void ToolStripSeparator1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripSeparator2_Click(object sender, EventArgs e)
        {

        }

        private void dgDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblTotalWtValue_Click(object sender, EventArgs e)
        {

        }

        private void lblTotalItemsValue_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lbWTHTotal_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void lblTotalInclValue_Click(object sender, EventArgs e)
        {

        }

        private void lblTotalVatValue_Click(object sender, EventArgs e)
        {

        }

        private void lblTotalExclValue_Click(object sender, EventArgs e)
        {

        }

        private void lblTotalIncl_Click(object sender, EventArgs e)
        {

        }

        private void lblTotalVAT_Click(object sender, EventArgs e)
        {

        }

        private void lblTotalExcl_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCopySO_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void StatusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void StatusBarServer_Click(object sender, EventArgs e)
        {

        }

        private void StatusBarDB_Click(object sender, EventArgs e)
        {

        }

        private void StatusActiveUSER_Click(object sender, EventArgs e)
        {

        }

        private void StatusVerion_Click(object sender, EventArgs e)
        {

        }

        private void LicenceType_Click(object sender, EventArgs e)
        {

        }

        private void DaysRemaining_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void search()
        {
            DT = dtSet.Tables["Customers"];
            var newTable = DT.AsEnumerable()
                .Where(x => x.Field<string>("NAME").ToUpper().Contains(cboCustName.Text.ToUpper()))
                .CopyToDataTable();

            cboCustName.DataSource = newTable;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void initializeFields()
        {
            cboItemCode.SelectedIndex = -1;
            cboItemDesc.SelectedIndex = -1;
            lblDefaultPriceValue.Text = "0";
            lblQtyAvailableValue.Text = "0";
            txtQty.Text = "0";
            txtUnitPrice.Text = "0";
            txtDiscount.Text = "0";
            lblTotalAmountValue.Text = "0";
        }

        private void txtDiscount_Leave(object sender, EventArgs e)
        {
            if (txtQty.Text.Length > 0 && txtDiscount.Text.Length > 0)
            {
                double UnitPrice, totalAmount, Discount, QTY, DefaultPrice = 0;

                DefaultPrice = Convert.ToDouble(lblDefaultPriceValue.Text);
                QTY = Convert.ToDouble(txtQty.Text);
                Discount = Convert.ToDouble(txtDiscount.Text);
                if (Discount > 0)
                {
                    UnitPrice = CalculateCeilingNew(DefaultPrice - (DefaultPrice * (Discount / 100)));//  Math.Round(DefaultPrice - (DefaultPrice * (Discount / 100)), 1);
                    totalAmount = UnitPrice * QTY;
                    txtUnitPrice.Text = UnitPrice.ToString("n2");
                    lblTotalAmountValue.Text = totalAmount.ToString("n2");
                }
            }
        }

        public void cboCustName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            initializeForm();
            combotext = "";
            comboboxchange(sender, e);
            cboCustName_(sender, e);


        }

        public void cboCustName_(object sender, EventArgs e){
            if (cboCustName.SelectedIndex > -1 && cboCustName.SelectedIndex >0)
            {
                loadCustomerDetails((int) cboCustName.SelectedValue);

        btnLoadQuotes_Click(sender, e);
        btnLoadSO_Click(sender, e);
        SoButtons(false);
    }
}

        private void loadCustomerDetails(int DCLink)
        {

            Customer customer = new Customer(DCLink);

            lblAddress1Value.Text = customer.PostalAddress.Line1.ToString();
            lblAddress2Value.Text = customer.PostalAddress.Line2.ToString();
            lblAddress3Value.Text = customer.PostalAddress.Line3.ToString();
            lblAddress4Value.Text = customer.PostalAddress.Line4.ToString();

        }

        private void loadPriceList()
        {
            SQL = "select IDPriceListName, cDescription from _etblPriceListName";
            LoadCombo(SQL, cboPriceList, "IDPriceListName", "cDescription", true, false);
            cboPriceList.SelectedIndex = -1;
        }



        private void loadItemDetails()
        {
            SQL = "SELECT ROW_NUMBER() OVER (ORDER BY d.sno)  LNO, D.sno,D.OrderSNo, D.StockLink, D.DiscPercent, s.Description_3 [Item Code], s.Description_1 [Description],'' [UOM],D.Qty,''[Price List], D.DefaultPrice [Default Price], D.UnitIncl [Price Incl.] ,  d.DiscPercent [Disc %],D.UnitDiscount [Unit Disc.],D.UnitVAT [Unit VAT],D.TotaDiscount [Total Disc.], D.TotalVAT [Total VAT], D.TotalIncl [Total Incl.], '' [Item Wt.], '' [Total Wt.] FROM WIZ_KENS_OrderDetails d INNER JOIN StkItem s ON D.StockLink = s.StockLink " + dgItemFilter + " ORDER BY D.SNO ASC";
            LoadDataGrid(dgDetails, SQL, "sno,StockLink,DiscPercent,OrderSNo,Unit VAT, Unit Disc,Total Disc, Total VAT", "");

            if (dgDetails.Rows.Count > 0)
            {
                //DataGridViewColumn AutoNumberColumn = new DataGridViewColumn();
                //AutoNumberColumn.Name = "S.No.";
                //AutoNumberColumn.ValueType = typeof(int);
                //dgDetails.Columns.Insert(1, AutoNumberColumn);

                DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                chk.HeaderText = "Select";
                chk.Name = "chk";
                chk.ThreeState = false;
                chk.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                chk.FlatStyle = FlatStyle.Standard;
                dgDetails.Columns.Insert(0, chk);

                // Set your desired AutoSize Mode:
                dgDetails.Columns["Item Code"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgDetails.Columns["Description"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgDetails.Columns["UOM"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgDetails.Columns["Qty"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgDetails.Columns["Default Price"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgDetails.Columns["Price List"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgDetails.Columns["Price Incl."].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgDetails.Columns["Total VAT"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgDetails.Columns["Total Incl."].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgDetails.Columns["Item Wt."].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgDetails.Columns["Total Wt."].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                lblTotalItemsValue.Text = dgDetails.Rows.Count.ToString();
            }
            else
            {
                if (dgDetails.Columns.Contains("chk"))
                {
                    dgDetails.Columns["chk"].Visible = false;

                }
            }

        }
        private void Recalculate()
        {
            SQL = "select FORMAT(SUM(D.QTY), 'N2') QTY,FORMAT(SUM(D.UnitIncl), 'N2') UnitExcl,FORMAT(SUM(D.UnitVAT), 'N2') UnitVAT, FORMAT(SUM(D.TotalExcl),'N2') TotalExcl, FORMAT(SUM(D.UnitIncl),'N2') UnitIncl, FORMAT(SUM(D.TotalIncl),'N2') TotalIncl, FORMAT(SUM(TotalVAT), 'N2') TotalVAT FROM WIZ_KENS_OrderDetails d INNER JOIN StkItem s ON D.StockLink = s.StockLink " + dgItemFilter + " GROUP BY D.SessionVar";
            DT = new DataTable();

            LoadDatatable(SQL, DT);

            if (DT.Rows.Count > 0)
            {
                lblTotalExclValue.Text = (Convert.ToDouble(DT.Rows[0]["TotalIncl"]) - Convert.ToDouble(DT.Rows[0]["TotalVAT"])).ToString("n2");
                lblTotalVatValue.Text = DT.Rows[0]["TotalVAT"].ToString();
                lblTotalInclValue.Text = DT.Rows[0]["TotalIncl"].ToString();
                //lblTotalItemsValue.Text = DT.Rows.Count.ToString();
            }
            else
            {
                lblTotalExclValue.Text = string.Empty;
                lblTotalVatValue.Text = string.Empty;
                lblTotalInclValue.Text = string.Empty;
                //lblTotalItemsValue.Text = string.Empty;
            }
        }


    }
}
