using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.VisualBasic; // Install-Package Microsoft.VisualBasic
using Microsoft.VisualBasic.CompilerServices; // Install-Package Microsoft.VisualBasic
using CrystalDecisions.CrystalReports.Engine;
using KENSPOS.FORMS;
using TIMS.MODULES;
using Pastel.Evolution;

namespace KENSPOS.MODULES
{
    public static class CommonFunctions
    {
        public static bool EmailSettingsRetrieved;
        public static string SageServerName = "";
        public static string CommServerName = "";
        public static string SageDBName = "";
        public static string CommonDBName = "";
        public static string UserName = "";
        public static string Password = "";
        public static string ConnSAGE = "";
        public static string ConnCOMM = "";
        public static string ApplicationPath = "";
        public static NameValueCollection TableValues = new NameValueCollection();
        public static int UserSNo = 0;
        public static int Till_No = 0;
        public static string Default_Printer = "";
        public static ReportDocument cryRpt = new ReportDocument();
        public static string EmailServerName = "";
        public static string EmailUserName = "";
        public static string EmailPassword = "";
        public static int MailServerPort = 25;
        public static bool UseSSL = false;
        public static bool UseDefaultSettings = false;
        public static string EmailSubject = "This is your email";
        public static string LoginUserName;
        public static string LoginUserType;
        public static bool flagFormIsReady;
        public static short userMaxDiscount;
        public static string AppLicenceType=string.Empty;
        public static double AppDaysRemaining = 0;
        public static string AppVersion = "Version 1.0.7";

        public static string activeUSER = "";
        public static string activeUSERWhse = "";
        public static bool activeUSERisCashier;

        private static SqlConnection CON = new SqlConnection();
        private static SqlCommand CMD = new SqlCommand();
        private static DataTable DT = null;
        private static SqlDataAdapter DA;
        private static DataSet DS;
        private static SqlDataReader RS;

        private static OleDbConnection oleConn = null;
        private static OleDbCommand oldCmd = null;
        private static OleDbDataAdapter oleDA = null;
        private static string oleConnString = "";
        private static string SQL = "";

        private static frmError FormError = new frmError();
        //frmFileWatcher.ShowDialog(); 


        // Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress

        // '97 - 122 = Ascii codes for simple letters
        // '65 - 90  = Ascii codes for capital letters
        // '48 - 57  = Ascii codes for numbers

        // If Asc(e.KeyChar) <> 8 Then
        // If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
        // e.Handled = True
        // End If
        // End If

        // End Sub

        public static void SendToPrinter(string file)
        {
            cryRpt.Load(@file);
            cryRpt.PrintOptions.PrinterName = CommonFunctions.Default_Printer;
            cryRpt.PrintToPrinter(1, false, 0, 0);
            
        }

        public static DataTable getCustomersSdk()
        {
            SageSDKConnection();

            DataTable DTCust = new DataTable();
            var criteria_ = $"DCLink > {"0"}";
            return DTCust = Customer.List(criteria_);
        }

        public static DataTable getStockSdk()
        {
            SageSDKConnection();

            DataTable DTStock = new DataTable();
            var criteria = $"StockLink > {"1"}";
            return DTStock = InventoryItem.List(criteria);
        }

        public static InventoryItem getStockDetailsSdk(Int32 StockLink)
        {
            SageSDKConnection(); 
            InventoryItem inventory = new InventoryItem(StockLink);

            return inventory;
        }

        public static void SageSDKConnection()
        {
            DatabaseContext.CreateCommonDBConnection(ConnCOMM);
            DatabaseContext.SetLicense("DE12111058", "2523370");
            DatabaseContext.CreateConnection(ConnSAGE);
        }


        public static double ConvertToMtr(string FromUnit, double Qty)
        {
            double ConvertToMtrRet = 0;
            ConvertToMtrRet = 0d;
            switch (FromUnit ?? "")
            {
                case "mtr":
                    {
                        ConvertToMtrRet = Qty * 1d;
                        break;
                    }
                case "cm":
                    {
                        ConvertToMtrRet = Qty / 100d;
                        break;
                    }
                case "mm":
                    {
                        ConvertToMtrRet = Qty / 1000d;
                        break;
                    }
                case "ft":
                    {
                        ConvertToMtrRet = Qty / 3.28084d;
                        break;
                    }
                case "inch":
                    {
                        ConvertToMtrRet = Qty / 39.3701d;
                        break;
                    }
                case "PC":
                    {
                        ConvertToMtrRet = 1d;
                        break;
                    }
            }

            return ConvertToMtrRet;
        }
        //public static bool ExportDatagridViewToExcel(string ExcelFileName, ref DataGridView dataGridViewXl)
        //{
        //    Microsoft.Office.Interop.Excel.Application xlApp;
        //    Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
        //    Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
        //    object misValue = Missing.Value;

        //    ExcelFileName = @"C:\Users\Public\Documents\" + ExcelFileName;

        //    short i;
        //    short j;

        //    xlApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
        //    xlWorkBook = xlApp.Workbooks.Add(misValue);
        //    xlWorkSheet = xlWorkBook.Sheets("sheet1");

        //    for (int k = 0, loopTo = dataGridViewXl.ColumnCount - 1; k <= loopTo; k++)
        //    {
        //        xlWorkSheet.Cells(1, Operators.AddObject(k, 1)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
        //        xlWorkSheet.Cells(1, Operators.AddObject(k, 1)) = dataGridViewXl.Columns(k).Name;
        //    }
        //    var loopTo1 = dataGridViewXl.RowCount - 1;
        //    for (i = 0; i <= loopTo1; i++)
        //    {
        //        var loopTo2 = dataGridViewXl.ColumnCount - 1;
        //        for (j = 0; j <= loopTo2; j++)
        //        {
        //            xlWorkSheet.Cells(i + 2, j + 1).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
        //            xlWorkSheet.Cells(i + 2, j + 1) = dataGridViewXl(j, i).Value.ToString();
        //        }
        //    }

        //    xlApp.Columns.AutoFit();
        //    xlApp.Rows("1:1").Font.FontStyle = "Bold";
        //    xlApp.Rows("1:1").Font.Size = 11;

        //    xlWorkBook.SaveAs(ExcelFileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
        //    xlWorkBook.Close(true, misValue, misValue);
        //    xlApp.Quit();

        //    ReleaseObject(xlWorkSheet);
        //    ReleaseObject(xlWorkBook);
        //    ReleaseObject(xlApp);

        //    return true;
        //}

        private static void ReleaseObject(object obj)
        {
            try
            {
                Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        public static bool IsValidEmailFormat(string s)
        {
            return Regex.IsMatch(s, @"^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$");
        }
        public static bool IsValidMobileNumberFormat(string m)
        {
            return Regex.IsMatch(m, @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");
        }
        public static bool IsValidNumber(string n)
        {
            return Regex.IsMatch(n, "[0-9]+(,[0-9]+)*");
        }

        //public static bool GetFileListing(ref List<string> FileNameList, ref List<string> FullFileNameList, string FileLocation, string FileExtension)
        //{
        //    bool GetFileListingRet = default;

        //    var files = Directory.GetFiles(FileLocation, "*." + FileExtension);
        //    string FileName = "";

        //    // lstFiles.Items.Clear()
        //    FileNameList.Clear();
        //    FullFileNameList.Clear();

        //    foreach (string FullFilename in files)
        //    {
        //        // Do work, example
        //        FileName = Path.GetFileName(FullFilename);
        //        FileName = Strings.Mid(FileName, 1, Strings.InStr(FileName.ToUpper(), "." + FileExtension.ToUpper()) - 1);
        //        FileNameList.Add(FileName);
        //        FullFileNameList.Add(FullFilename);
        //    }
        //    if (FileNameList.Count > 0)
        //    {
        //        GetFileListingRet = true;
        //    }
        //    else
        //    {
        //        GetFileListingRet = false;
        //    }

        //    return GetFileListingRet;
        //}



        public static void wait(int seconds)
        {
            for (int i = 0, loopTo = seconds * 100; i <= loopTo; i++)
            {
                System.Threading.Thread.Sleep(10);
                Application.DoEvents();
            }
        }

        public static void LoadPrinters(ComboBox cboPrinterList)
        {
            cboPrinterList.Items.Clear();
            foreach (var Printer in PrinterSettings.InstalledPrinters)
                cboPrinterList.Items.Add(Printer);
        }
        public static bool SearchNameValue(ref List<string> ListName, string SearchValue, bool AddIfNotFound)
        {
            bool SearchNameValueRet = false;
            // Searches for a values in given list
            // If not found then adds the value to the list

            short I;
            bool Found = false;

            SearchNameValueRet = false;
            if (ListName.Count == 0)
            {
                if (AddIfNotFound == true)
                {
                    ListName.Add(SearchValue);
                    SearchNameValueRet = false;
                    return SearchNameValueRet;
                }
            }

            var loopTo = (short)(ListName.Count - 1);
            for (I = 0; I <= loopTo; I++)
            {
                if ((ListName[I] ?? "") == (SearchValue ?? ""))
                {
                    Found = true;
                }
            }

            if (Found == false & AddIfNotFound == true)
            {
                ListName.Add(SearchValue);
            }
            SearchNameValueRet = Found;
            return SearchNameValueRet;

        }


        public static void CopyColumnsBetweenDatagrids(ref DataGridView dgSource, ref DataGridView dgDestination)
        {
            if (dgDestination.Columns.Count == 0)
            {
                foreach (DataGridViewColumn dgvc in dgSource.Columns)
                    dgDestination.Columns.Add(dgvc.Clone() as DataGridViewColumn);
            }
        }
        

        public static string FetchLoggedAgentName()
        {
            string FetchLoggedAgentNameRet = "";
            SQL = "Select AgentName from _tSessions Where MachineName = '" + System.Windows.Forms.SystemInformation.ComputerName + "'";
            FetchLoggedAgentNameRet = FetchSingleStringValue(SQL, false, true);
            return FetchLoggedAgentNameRet;
        }



        public static bool GET_EMAIL_SETTINGS()
        {
            bool GET_EMAIL_SETTINGSRet = false;
            if (EmailSettingsRetrieved == true)
            {
                GET_EMAIL_SETTINGSRet = true;
                return GET_EMAIL_SETTINGSRet;
            }

            string PwdKey = "65f02fab40f56c8e409b121965fa1d657a80d237fabacab95f9c59f8f967becb";
            var wrapper = new Simple3Des(PwdKey);
            int I = 1;
            int J = 0;
            string FPath;


            //FPath = global::Application.Info.DirectoryPath + @"\Server.Log";
            FPath = Application.ExecutablePath; // & "\AppInfo.Log"
            J = Strings.InStrRev(FPath, @"\");
            FPath = Strings.Mid(FPath, 1, J);
            FPath = FPath + "Server.Log";
            I = 1;

            if (File.Exists(FPath) == false)
            {
                // Debug.Print(FPath)
                MessageBox.Show("Setting file not found. Please enter the settings and save");
                GET_EMAIL_SETTINGSRet = false;
                return GET_EMAIL_SETTINGSRet;
            }

            try
            {
                string line;
                TextReader readFile = new StreamReader(FPath);
                while (true)
                {
                    line = readFile.ReadLine();
                    if (line is null)
                    {
                        break;
                    }
                    else
                    {
                        switch (I)
                        {
                            case 1:
                                {
                                    EmailServerName = wrapper.DecryptData(line);
                                    break;
                                }
                            case 2:
                                {
                                    EmailUserName = wrapper.DecryptData(line);
                                    break;
                                }
                            case 3:
                                {
                                    EmailPassword = wrapper.DecryptData(line);
                                    break;
                                }
                            case 4:
                                {
                                    MailServerPort = Convert.ToInt32(wrapper.DecryptData(line));
                                    break;
                                }
                            case 5:
                                {
                                    UseSSL = Convert.ToBoolean(Convert.ToInt32(wrapper.DecryptData(line)));
                                    break;
                                }
                            case 6:
                                {
                                    UseDefaultSettings = Convert.ToBoolean(Convert.ToInt32(wrapper.DecryptData(line)));
                                    break;
                                }
                        }
                        I += 1;
                    }
                }
                readFile.Close();
                readFile = null;

                GET_EMAIL_SETTINGSRet = true;
                EmailSettingsRetrieved = true;
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString());
                GET_EMAIL_SETTINGSRet = false;
                EmailSettingsRetrieved = false;
            }

            return GET_EMAIL_SETTINGSRet;

        }

        public static bool SendEmail(string RecipientAddress, string RecipientName, string SenderAddress, string SenderName, string EmailMessage, bool isAttachment, string Attachment_Path, string Subject)
        {
            bool SendEmailRet = false;

            var msg = new MailMessage();
            var client = new SmtpClient();

            msg.To.Add(new MailAddress(RecipientAddress, RecipientName));
            // msg.CC.Add(New MailAddress("prabhu.ram@securex.co.ke", "Prabhu"))
            msg.From = new MailAddress(SenderAddress, SenderName);
            msg.Subject = Subject;
            msg.Body = EmailMessage;
            msg.IsBodyHtml = false;

            // client.UseDefaultCredentials = False
            client.UseDefaultCredentials = UseDefaultSettings;
            client.Credentials = new System.Net.NetworkCredential(EmailUserName, EmailPassword);
            client.Port = MailServerPort;          // 587 for Office365
            client.Host = EmailServerName;         // "smtp.office365.com"
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = UseSSL;              // True for office365

            if (isAttachment == true)
            {
                var Attachment = new System.Net.Mail.Attachment(Attachment_Path);
                msg.Attachments.Add(Attachment);
            }

            try
            {
                client.Send(msg);
                SendEmailRet = true;
            }
            catch (Exception ex)
            {
                SendEmailRet = false;
            }

            return SendEmailRet;
        }


        public static string EncryptText(string PlainText, string PwdKey)
        {
            string EncryptTextRet = "";
            if (string.IsNullOrEmpty(PlainText) | string.IsNullOrEmpty(PwdKey))
            {
                EncryptTextRet = "";
                return EncryptTextRet;
            }
            var wrapper = new Simple3Des(PwdKey);
            string cipherText = wrapper.EncryptData(PlainText);
            EncryptTextRet = cipherText;
            return EncryptTextRet;
        }

        public static string DecryptText(string CipherText, string PwdKey)
        {
            string DecryptTextRet = "";
            if (string.IsNullOrEmpty(CipherText) | string.IsNullOrEmpty(PwdKey))
            {
                DecryptTextRet = "";
                return DecryptTextRet;
            }
            var wrapper = new Simple3Des(PwdKey);
            string plainText = wrapper.DecryptData(CipherText);
            DecryptTextRet = plainText;
            return DecryptTextRet;
        }

        public static void LoadUserNames(string Query, ComboBox comboitem)
        {
            if (string.IsNullOrEmpty(Query))
            {
                return;
            }
            if (CON.State == ConnectionState.Open)
                CON.Close();
            CON.ConnectionString = ConnSAGE;
            CON.Open();
            CMD = new SqlCommand(Query, CON);
            RS = CMD.ExecuteReader();
            if (RS.HasRows)
            {
                while (RS.Read())
                    comboitem.Items.Add(RS.GetValue(0));
            }

        }

        //public static void Warning(string message)
        //{
        //    MessageBox.Show(message, (MsgBoxStyle)((int)MsgBoxStyle.OkOnly + (int)Constants.vbExclamation), "Warning");
        //}
        //public static object Confirm(string message)
        //{
        //    return MessageBox.Show(message, (MsgBoxStyle)((int)Constants.vbYesNo + (int)Constants.vbInformation), "Confirm");
        //}
        //public static void Inform(string message)
        //{
        //    MessageBox.Show(message, (MsgBoxStyle)((int)Constants.vbOKOnly + (int)Constants.vbInformation), "Information");
        //}
        public static void ManageAccess(Form FormName)
        {
            var dvAccess = new DataView();
            string Query = "";
            var allControls = new List<Control>();

            Query = "SELECT COUNT(*) FROM  WIZ_USER_RIGHTS AS R INNER JOIN WIZ_USER_RIGHTS_LISTING AS L ON R.UserRightID = L.SNo  WHERE (R.UserID = " + UserSNo + ") AND (L.FormName = '" + FormName.Name + "')";
            if (FetchSingleIntValue(Query, true, false) > 0)
            {
                Query = "SELECT L.ControlName, R.Access FROM  WIZ_USER_RIGHTS AS R INNER JOIN WIZ_USER_RIGHTS_LISTING AS L ON R.UserRightID = L.SNo  WHERE (R.UserID = " + UserSNo + ") AND (L.FormName = '" + FormName.Name + "')";
                FillViewAndReturn(ref dvAccess, Query);

                foreach (Control ctr in FormName.Controls)
                    allControls = getAllControls(ctr, allControls);
                foreach (Control Ctrl in allControls)
                {
                    dvAccess.RowFilter = "ControlName = '" + Ctrl.Name + "'";

                    if (dvAccess.Count > 0)
                    {
                        if (dvAccess[0]["Access"].ToString() == "TRUE")
                        {
                            Ctrl.Enabled = true;
                        }
                        else
                        {
                            Ctrl.Enabled = false;
                        }
                    }
                }

                allControls = null;
            }
        }

        private static List<Control> getAllControls(Control mainControl, List<Control> allControls)
        {

            if (!allControls.Contains(mainControl))
                allControls.Add(mainControl);
            if (mainControl.HasChildren)
            {
                foreach (var child in mainControl.Controls)
                {
                    if (!allControls.Contains((Control)child))
                        allControls.Add((Control)child);
                    if (((Control)child).HasChildren)
                        getAllControls((Control)child, allControls);
                }
            }

            return allControls;

        }

        public static void EnableControls(Form FormName, bool Action, bool Textbox, bool Combobox, bool Listbox, bool CheckBox, bool DataGrid, bool Buttons)
        {
            foreach (Control CTRL in FormName.Controls)
            {
                if (Textbox == true)
                {
                    if (CTRL is TextBox)
                    {
                        CTRL.Enabled = Action;
                    }
                }
                else if (Combobox == true)
                {
                    if (CTRL is ComboBox)
                    {
                        CTRL.Enabled = Action;
                    }
                }
                else if (Listbox == true)
                {
                    if (CTRL is ListBox)
                    {
                        CTRL.Enabled = Action;
                    }
                }
                else if (CheckBox == true)
                {
                    if (CTRL is CheckBox)
                    {
                        CTRL.Enabled = Action;
                    }
                }
                else if (DataGrid == true)
                {
                    if (CTRL is DataGridView)
                    {
                        CTRL.Enabled = Action;
                    }
                }
                else if (Buttons == true)
                {
                    if (CTRL is Button)
                    {
                        CTRL.Enabled = Action;
                    }
                }
            }
        }


        public static void FillViewAndReturn(ref DataView dView, string Query)
        {
            if (string.IsNullOrEmpty(Query))
                return;

            if (CON.State == ConnectionState.Closed)
                CON.Open();
            DA = new SqlDataAdapter(Query, CON);
            DS = new DataSet();
            DA.Fill(DS);
            CON.Close();
            dView = new DataView(DS.Tables[0]);

        }

        public static string FetchNextCode(string Entity)
        {
            string FetchNextCodeRet = "";
            if (string.IsNullOrEmpty(Entity) == true)
            {
                FetchNextCodeRet = "";
                return FetchNextCodeRet;
            }
            string PREFIX = "";
            short LENGTH = 0;
            short NEXT_SERIAL = 0;
            string NEXT_CODE = "";
            if (Entity == "STOCK")
            {
                SQL = "SELECT STK_CODE_PREFIX, STK_CODE_LENGTH, STK_CODE_NEXT FROM dbo.SETTINGS";
                FetchTableData(SQL);
                PREFIX = TableValues["STK_CODE_PREFIX"].ToString();
                LENGTH = (short)Convert.ToInt32(TableValues["STK_CODE_LENGTH"]);
                NEXT_SERIAL = (short)Convert.ToInt32(TableValues["STK_CODE_NEXT"]);
                NEXT_CODE = PREFIX + NEXT_SERIAL.ToString().PadLeft(LENGTH, '0');
            }
            // SQL = "UPDATE SETTINGS SET STK_CODE_NEXT = " & NEXT_SERIAL + 1
            // Call ExecuteQuery(SQL)
            else if (Entity == "SUPPLIER")
            {
                SQL = "SELECT SUPP_CODE_PREFIX, SUPP_CODE_LENGTH, SUPP_CODE_NEXT FROM dbo.SETTINGS";
                FetchTableData(SQL);
                PREFIX = TableValues["SUPP_CODE_PREFIX"].ToString();
                LENGTH = (short)Convert.ToInt32(TableValues["SUPP_CODE_LENGTH"]);
                NEXT_SERIAL = (short)Convert.ToInt32(TableValues["SUPP_CODE_NEXT"]);
                NEXT_CODE = PREFIX + NEXT_SERIAL.ToString().PadLeft(LENGTH, '0');
            }
            // SQL = "UPDATE SETTINGS SET SUPP_CODE_NEXT = " & NEXT_SERIAL + 1
            // Call ExecuteQuery(SQL)
            else if (Entity == "CUSTOMER")
            {
                SQL = "SELECT CUST_CODE_PREFIX, CUST_CODE_LENGTH, CUST_CODE_NEXT FROM dbo.SETTINGS";
                FetchTableData(SQL);
                PREFIX = TableValues["CUST_CODE_PREFIX"].ToString();
                LENGTH = (short)Convert.ToInt32(TableValues["CUST_CODE_LENGTH"]);
                NEXT_SERIAL = (short)Convert.ToInt32(TableValues["CUST_CODE_NEXT"]);
                NEXT_CODE = PREFIX + NEXT_SERIAL.ToString().PadLeft(LENGTH, '0');
            }
            // SQL = "UPDATE SETTINGS SET CUST_CODE_NEXT = " & NEXT_SERIAL + 1
            // Call ExecuteQuery(SQL)
            else if (Entity == "INVOICE")
            {
                SQL = "SELECT INV_CODE_PREFIX, INV_CODE_LENGTH, INV_CODE_NEXT FROM dbo.SETTINGS";
                FetchTableData(SQL);
                PREFIX = TableValues["INV_CODE_PREFIX"].ToString();
                LENGTH = (short)Convert.ToInt32(TableValues["INV_CODE_LENGTH"]);
                NEXT_SERIAL = (short)Convert.ToInt32(TableValues["INV_CODE_NEXT"]);
                NEXT_CODE = PREFIX + NEXT_SERIAL.ToString().PadLeft(LENGTH, '0');
                SQL = "UPDATE SETTINGS SET INV_CODE_NEXT = " + (NEXT_SERIAL + 1);
                ExecuteQuery(SQL);
            }
            else if (Entity == "CRN")
            {
                SQL = "SELECT CRN_CODE_PREFIX, CRN_CODE_LENGTH, CRN_CODE_NEXT FROM dbo.SETTINGS";
                FetchTableData(SQL);
                PREFIX = TableValues["CRN_CODE_PREFIX"].ToString();
                LENGTH = (short)Convert.ToInt32(TableValues["CRN_CODE_LENGTH"]);
                NEXT_SERIAL = (short)Convert.ToInt32(TableValues["CRN_CODE_NEXT"]);
                NEXT_CODE = PREFIX + NEXT_SERIAL.ToString().PadLeft(LENGTH, '0');
                SQL = "UPDATE SETTINGS SET CRN_CODE_NEXT = " + (NEXT_SERIAL + 1);
                ExecuteQuery(SQL);
            }
            else if (Entity == "PO")
            {
                SQL = "SELECT PO_CODE_PREFIX, PO_CODE_LENGTH, PO_CODE_NEXT FROM dbo.SETTINGS";
                FetchTableData(SQL);
                PREFIX = TableValues["PO_CODE_PREFIX"].ToString();
                LENGTH = (short)Convert.ToInt32(TableValues["PO_CODE_LENGTH"]);
                NEXT_SERIAL = (short)Convert.ToInt32(TableValues["PO_CODE_NEXT"]);
                NEXT_CODE = PREFIX + NEXT_SERIAL.ToString().PadLeft(LENGTH, '0');
                SQL = "UPDATE SETTINGS SET PO_CODE_NEXT = " + (NEXT_SERIAL + 1);
                ExecuteQuery(SQL);
            }
            else if (Entity == "GRV")
            {
                SQL = "SELECT GRV_CODE_PREFIX, GRV_CODE_LENGTH, GRV_CODE_NEXT FROM dbo.SETTINGS";
                FetchTableData(SQL);
                PREFIX = TableValues["GRV_CODE_PREFIX"].ToString();
                LENGTH = (short)Convert.ToInt32(TableValues["GRV_CODE_LENGTH"]);
                NEXT_SERIAL = (short)Convert.ToInt32(TableValues["GRV_CODE_NEXT"]);
                NEXT_CODE = PREFIX + NEXT_SERIAL.ToString().PadLeft(LENGTH, '0');
                SQL = "UPDATE SETTINGS SET GRV_CODE_NEXT = " + (NEXT_SERIAL + 1);
                ExecuteQuery(SQL);
            }
            FetchNextCodeRet = NEXT_CODE;
            return FetchNextCodeRet;
        }

        public static void FetchTableData(string Query)
        {
            // Usage
            // Call CommonFunctions.FetchTableData(SQL)
            // txtTotRetExcl.Text = Format(CDbl(TableValues("EXCL")), "n")

            short I = 0;

            if (string.IsNullOrEmpty(Query))
            {
                return;
            }
            if (CON.State == ConnectionState.Open)
                CON.Close();
            CON.ConnectionString = ConnSAGE;
            CON.Open();
            CMD = new SqlCommand(Query, CON);
            RS = CMD.ExecuteReader();
            if (RS.HasRows)
            {
                TableValues.Clear();
                RS.Read();
                var loopTo = (short)(RS.FieldCount - 1);
                for (I = 0; I <= loopTo; I++)
                    // Debug.Print(TableValues(RS.GetName(I)))
                    TableValues.Add(RS.GetName(I), RS[I].ToString());
            }
            RS.Close();
            CON.Close();

        }


        public static void UpdateCode(string Entity)
        {
            int NEXT_NO = 0;

            if (string.IsNullOrEmpty(Entity))
            {
                return;
            }
            if (Entity == "CUSTOMER")
            {
                SQL = "SELECT CUST_CODE_NEXT FROM SETTINGS";
                NEXT_NO = FetchSingleIntValue(SQL, true, false);
                SQL = "UPDATE SETTINGS SET CUST_CODE_NEXT = " + (NEXT_NO + 1);
                ExecuteQuery(SQL);
            }


        }
        //public static string GenerateCode(string Entity)
        //{
        //    string GenerateCodeRet = default;
        //    string PREFIX = "";
        //    short LENGTH = 0;
        //    int START = 0;

        //    if (string.IsNullOrEmpty(Entity))
        //    {
        //        GenerateCodeRet = "";
        //        return GenerateCodeRet;
        //    }

        //    if (Entity == "CUSTOMER")
        //    {
        //        SQL = "SELECT CUST_CODE_PREFIX, CUST_CODE_LENGTH, CUST_CODE_NEXT FROM SETTINGS";
        //    }
        //    else
        //    {
        //        GenerateCodeRet = "";
        //        return GenerateCodeRet;
        //    }
        //    if (CON.State == ConnectionState.Open)
        //        CON.Close();
        //    CON.ConnectionString = ConnSAGE;
        //    CON.Open();
        //    DA = new SqlDataAdapter(SQL, CON);
        //    DT = new DataTable();
        //    DA.Fill(DT);
        //    CON.Close();

        //    foreach (DataRow ROW in DT.Rows)
        //    {
        //        PREFIX = ROW.Item("CUST_CODE_PREFIX").ToString;
        //        LENGTH = Convert.ToInt16(ROW.Item("CUST_CODE_LENGTH"));
        //        START = (int)Convert.ToInt64(ROW.Item("CUST_CODE_NEXT"));
        //    }
        //    GenerateCodeRet = PREFIX + START.ToString().PadLeft(LENGTH, '0');
        //    return GenerateCodeRet;

        //}

        //public static int AddCustomer(string CustName, string dbName)
        //{
        //    int AddCustomerRet = default;
        //    string CustCode = "";

        //    if (string.IsNullOrEmpty(CustName))
        //    {
        //        MessageBox.Show("Project name can't be blank. Please retry.", Constants.vbCritical, "Module: AddProject");
        //        return 0;
        //        // Exit Function
        //    }
        //    if (string.IsNullOrEmpty(dbName))
        //    {
        //        MessageBox.Show("Company name can't be blank. Please retry.", Constants.vbCritical, "Module: AddProject");
        //        return 0;
        //    }
        //    // Fetch Stock Code
        //    SQL = "IF EXISTS (Select 1 From " + dbName + ".dbo.Client Where Account Like 'AR%') " + "Select Max(Account) From " + dbName + ".dbo.StkItem Where Account Like 'AR%' " + "Else Select '0'";

        //    CustCode = FetchSingleStringValue(SQL, true, false);
        //    if (CustCode == "0")
        //    {
        //        CustCode = "AR0001";
        //    }
        //    else
        //    {
        //        CustCode = "AR" + Strings.Format(Convert.ToInt32(Strings.Right(CustCode, 4)) + 1, "0000");
        //    }
        //    SQL = "INSERT INTO CLIENT ( Account, Name, dTimeStamp, " + "CT,CheckTerms,bStatPrint,bTaxPrompt,iARPriceListNameID,bSourceDocPrint,biAgeingTermID," + " AccountTerms,Credit_Limit,RepID,Interest_Rate,Discount,On_Hold,BFOpenType,AutoDisc, DiscMtrxRow, CashDebtor, UseEmail, iClassID, iAreasID, iCurrencyID, bStatEmail, bForCurAcc, iSettlementTermsID, bSourceDocEmail, iEUCountryID, iDefTaxTypeID, bCODAccount, bElecDocAcceptance,bInsuranceCustomer,Client_iBranchID,bCustomerZoneEnabled,iTaxState, " + "Title, Init, Contact_Person, Physical1, Physical2, Physical3, Physical4, Physical5, PhysicalPC, Addressee, Post1, Post2, Post3, Post4, Post5, PostPC, Delivered_To, Telephone, Telephone2, Fax1, Fax2, Tax_Number, Registration, EMail, cAccDescription, cWebPage, cStatEmailPass, cIDNumber, cPassportNumber) " + "VALUES ( '" + CustCode + "', '" + CustName + "', GetDate(), " + "1, 1, 1, 1, 1, 1, 1, " + "0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, " + "'', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '' ;" + " SELECT CAST(scope_identity() AS int)";







        //    if (CON.State == ConnectionState.Open)
        //        CON.Close();
        //    CON.ConnectionString = "Data Source=" + SageServerName + ";Initial Catalog=" + dbName + ";User ID=" + UserName + ";Password=" + Password + ";";
        //    if (CON.State == ConnectionState.Closed)
        //        CON.Open();
        //    CMD = new SqlCommand(SQL, CON);
        //    AddCustomerRet = Convert.ToInt32(CMD.ExecuteScalar());
        //    CON.Close();
        //    return AddCustomerRet;
        //}


        //public static void CreateXMLManually(string FileName, string Query)
        //{
        //    string XML = "";

        //    if (string.IsNullOrEmpty(Query))
        //    {
        //        MessageBox.Show("Illegal argument found. Please provide a valid SQL Query and retry", MsgBoxStyle.Critical, "Module: CreateXML");
        //        return;
        //    }
        //    if (string.IsNullOrEmpty(FileName))
        //    {
        //        MessageBox.Show("Empty file name not accepted. Please retry.", MsgBoxStyle.Critical, "Module: CreateXML");
        //        return;
        //    }

        //    try
        //    {
        //        if (CON.State == ConnectionState.Closed)
        //        {
        //            CON.ConnectionString = ConnSAGE;
        //            CON.Open();
        //        }
        //        DA = new SqlDataAdapter(Query, CON);
        //        CON.Close();
        //        DS = new DataSet();
        //        DA.Fill(DS, "XML");
        //        if (DS.Tables("XML").Rows.Count == 0)
        //        {
        //            // MsgBox("XML table found empty. Please add data and retry.", MsgBoxStyle.Critical, "Module: CreateXML")
        //            DS = null;
        //            DA = null;
        //            return;
        //        }

        //        XML = "";
        //        XML = XML + "<root>";
        //        foreach (DataRow DRow in DS.Tables("XML").Rows)
        //        {
        //            if (DRow.Item("TAG_START") == "<row>" | DRow.Item("TAG_START") == "</row>")
        //            {
        //                XML = XML + DRow("TAG_START");
        //            }
        //            else
        //            {
        //                XML = XML + DRow.Item("TAG_START") + DRow.Item("TAG_VALUE") + DRow.Item("TAG_END");
        //            }
        //        }
        //        XML = XML + "</root>";

        //        // WRITING THE DATA TO XML TABLE
        //        Query = "INSERT INTO TBLXML (XMLText) VALUES ('" + XML + "')";
        //        ExecuteQuery(Query);

        //        // WRITING THE DATA TO TEXT FILE
        //        TextWriter writeFile = new StreamWriter(FileName);
        //        writeFile.Write(XML);
        //        writeFile.Flush();
        //        writeFile.Close();
        //        writeFile = null;
        //        DA = null;
        //        DS = null;
        //    }
        //    // MsgBox("XML File created successfully!")

        //    catch (IOException ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //}

        //public static void FillStringList(ref List<string> ListName, string Query, string FieldName)
        //{
        //    if (string.IsNullOrEmpty(Query))
        //    {
        //        return;
        //    }
        //    if (CON.State == ConnectionState.Open)
        //        CON.Close();
        //    CON.ConnectionString = ConnSAGE;
        //    CON.Open();
        //    DA = new SqlDataAdapter(Query, CON);
        //    DT = new DataTable();
        //    DA.Fill(DT);
        //    if (DT.Rows.Count > 0)
        //    {
        //        foreach (DataRow Row in DT.Rows)
        //            ListName.Add(CommonFunctions.DT(FieldName).ToString);
        //    }

        //    CON.Close();
        //    DT = null;
        //    CON = null;
        //}

        //public static void LoadDataFromExcelIntoDataTable(string ExcelFileName, ref DataTable xlDataTable, string QuerySelectCols, string Filter)
        //{
        //    string SheetName = "";

        //    if (string.IsNullOrEmpty(ExcelFileName))
        //    {
        //        MessageBox.Show("Please select an Excel file and retry");
        //        return;
        //    }

        //    oleConnString = "provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + ExcelFileName + ";Extended Properties=Excel 8.0;";
        //    oleConn = new OleDbConnection(oleConnString);
        //    oleConn.Open();

        //    // Getting the name of 1st worksheet
        //    DT = new DataTable();
        //    DT = oleConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, nothing);
        //    if (DT.Rows.Count > 0)
        //    {
        //        SheetName = DT.Rows(0)("TABLE_NAME");
        //    }

        //    xlDataTable = new DataTable();
        //    if (string.IsNullOrEmpty(QuerySelectCols))
        //    {
        //        SQL = "select * from [" + SheetName + "] " + Filter;
        //    }
        //    else
        //    {
        //        SQL = "select " + QuerySelectCols + " from [" + SheetName + "] " + Filter;
        //    }

        //    oleDA = new OleDbDataAdapter(SQL, oleConn);
        //    oleDA.Fill(xlDataTable);
        //    oleConn.Close();

        //}


        public static string FetchSingleStringValue(string Query, bool UseSAGEConn, bool UseCommonConn)
        {
            var frmError = new frmError();
            string FetchSingleStringValueRet = "";
            if (string.IsNullOrEmpty(Query))
            {
                FetchSingleStringValueRet = "";
                return FetchSingleStringValueRet;
            }
            if (UseSAGEConn == false & UseCommonConn == false)
            {
                FetchSingleStringValueRet = "";
                return FetchSingleStringValueRet;
            }
            if (CON.State == ConnectionState.Closed)
            {
                if (UseSAGEConn == true)
                {
                    CON.ConnectionString = ConnSAGE;
                }
                else
                {
                    CON.ConnectionString = ConnCOMM;
                }

                CON.Open();
            }
            try
            {
                CMD = new SqlCommand(Query, CON);
                FetchSingleStringValueRet = Convert.ToString(CMD.ExecuteScalar());                
                CMD = null;
                CON.Close();
            }
            catch (Exception ex)
            {
                CON.Close();
                FetchSingleStringValueRet = "";
                frmError.txtError.AppendText("Error message: " + ex.Message);
                frmError.txtError.AppendText(Environment.NewLine);
                frmError.txtError.AppendText("Query : " + Query);
                frmError.ShowDialog();
            }

            return FetchSingleStringValueRet;
        }

        public static double FetchDoubleValue(string Query, bool UseSAGEConn, bool UseCommonConn)
        {
            var frmError = new frmError();
            double FetchDoubleValueRet = 0;
            if (string.IsNullOrEmpty(Query))
            {
                FetchDoubleValueRet = 0d;
                return FetchDoubleValueRet;
            }
            if (UseSAGEConn == false & UseCommonConn == false)
            {
                FetchDoubleValueRet = Convert.ToDouble("") ;
                return FetchDoubleValueRet;
            }
            if (CON.State == ConnectionState.Closed)
            {
                if (UseSAGEConn == true)
                {
                    CON.ConnectionString = ConnSAGE;
                }
                else
                {
                    CON.ConnectionString = ConnCOMM;
                }

                CON.Open();
            }
            try
            {
                CMD = new SqlCommand(Query, CON);
                FetchDoubleValueRet =Convert.ToDouble(CMD.ExecuteScalar());
                CMD = null;
                CON.Close();
            }
            catch (Exception ex)
            {
                CON.Close();
                FetchDoubleValueRet = -1;
                frmError.txtError.AppendText("Error message: " + ex.Message);
                frmError.txtError.AppendText(Environment.NewLine);
                frmError.txtError.AppendText("Query : " + Query);
                frmError.ShowDialog();
            }

            return FetchDoubleValueRet;

        }


        public static int FetchSingleIntValue(string Query, bool UseSAGEConn, bool UseCommonConn)
        {
            var frmError = new frmError();
            int FetchSingleIntValueRet = 0;
            if (string.IsNullOrEmpty(Query))
            {
                FetchSingleIntValueRet = 0;
                return FetchSingleIntValueRet;
            }
            if (UseSAGEConn == false & UseCommonConn == false)
            {
                FetchSingleIntValueRet = 0;
                return FetchSingleIntValueRet;
            }
            // If UseSAGEConn = True And UseCommonConn = True Then
            // FetchSingleIntValue = 0
            // Exit Function
            // End If

            if (CON.State == ConnectionState.Closed)
            {
                if (UseSAGEConn == true)
                {
                    CON.ConnectionString = ConnSAGE;
                }
                else
                {
                    CON.ConnectionString = ConnCOMM;
                }

                CON.Open();
            }
            try
            {
                CMD = new SqlCommand(Query, CON);
                FetchSingleIntValueRet = Convert.ToInt32(CMD.ExecuteScalar());
                CMD = null;
                CON.Close();
            }
            catch (Exception ex)
            {
                CON.Close();
                FetchSingleIntValueRet = 0;
                frmError.txtError.AppendText("Error message: " + ex.Message);
                frmError.txtError.AppendText(Environment.NewLine);
                frmError.txtError.AppendText("Query : " + Query);
                frmError.ShowDialog();
                
            }

            return FetchSingleIntValueRet;

        }

        public static decimal FetchSingleDecimalValue(string Query, bool UseSAGEConn, bool UseCommonConn)
        {
            var frmError = new frmError();
            decimal FetchSingleDecimalValueRet = 0;
            if (string.IsNullOrEmpty(Query))
            {
                FetchSingleDecimalValueRet = 0m;
                return FetchSingleDecimalValueRet;
            }
            if (UseSAGEConn == false & UseCommonConn == false)
            {
                FetchSingleDecimalValueRet = 0m;
                return FetchSingleDecimalValueRet;
            }
            // If UseSAGEConn = True And UseCommonConn = True Then
            // FetchSingleIntValue = 0
            // Exit Function
            // End If

            if (CON.State == ConnectionState.Closed)
            {
                if (UseSAGEConn == true)
                {
                    CON.ConnectionString = ConnSAGE;
                }
                else
                {
                    CON.ConnectionString = ConnCOMM;
                }

                CON.Open();
            }
            try
            {
                CMD = new SqlCommand(Query, CON);
                FetchSingleDecimalValueRet = Convert.ToDecimal(CMD.ExecuteScalar());
                CMD = null;
                CON.Close();
            }
            catch (Exception ex)
            {
                CON.Close();
                FetchSingleDecimalValueRet = 0m;
                frmError.txtError.AppendText("Error message: " + ex.Message);
                frmError.txtError.AppendText(Environment.NewLine);
                frmError.txtError.AppendText("Query : " + Query);
                frmError.ShowDialog();
            }

            return FetchSingleDecimalValueRet;

        }

        public static void SearchInCombo(ComboBox cbo, string SearchText, bool ExactMatch)
        {
            int Index = 0;
            if (ExactMatch == true)
            {
                Index = cbo.FindStringExact(SearchText);
            }
            else
            {
                Index = cbo.FindString(SearchText);
            }
            cbo.SelectedIndex = Index;
        }
        public static void LoadDatatable(string Query, DataTable DT)
        {
            var frmError = new frmError();
            try
            {
                if (string.IsNullOrEmpty(Query))
                    return;
                if (CON.State == ConnectionState.Closed)
                {
                    CON.ConnectionString = ConnSAGE;
                    CON.Open();
                }
                DA = new SqlDataAdapter(Query, CON);
                CON.Close();
                DA.Fill(DT);
            }
            catch (Exception ex)
            {
                frmError.txtError.AppendText("Error message: " + ex.Message);
                frmError.txtError.AppendText(Environment.NewLine);
                frmError.txtError.AppendText("Query : " + Query);
                frmError.ShowDialog();
            }
            
        }

        public static void LoadDataGridAndHighlight(DataGridView dgName, string Query, string ColListToHide, string ColListToEdit, string ColListToColor, Color HighlightColor)
        {
            dgName.DataSource = null;
            dgName.Rows.Clear();
            dgName.Columns.Clear();

            if (CON.State == ConnectionState.Closed)
            {
                CON.ConnectionString = ConnSAGE;
                CON.Open();
            }
            DA = new SqlDataAdapter(Query, CON);
            CON.Close();
            DT = new DataTable();
            DA.Fill(DT);
            if (DT.Rows.Count < 1)
            {
                // MsgBox("Records not found. Please retry.")
                DT = null;
                DA = null;
                return;
            }

            dgName.DataSource = DT;

            foreach (DataGridViewColumn Col in dgName.Columns)
            {
                dgName.ReadOnly = false;
                //Strings.InStr("FAT,HOV,INP,FMM", s1, CompareMethod.Text);
                if (Strings.InStr(ColListToEdit, Col.Name) > 0)
                {
                    Col.ReadOnly = false;
                }
                else
                {
                    Col.ReadOnly = true;
                }
                if (Strings.InStr(ColListToHide, Col.Name) > 0)
                {
                    Col.Visible = false;
                }
                else
                {
                    Col.Visible = true;
                }
                if (Strings.InStr(ColListToColor, Col.Name) > 0)
                {
                    Col.DefaultCellStyle.BackColor = HighlightColor;
                }
            }
            dgName.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgName.AutoResizeColumns();
            DT = null;
            DA = null;
        }
        public static void LoadDataGrid(DataGridView dgName, string Query, string ColListToHide, string ColListToEdit)
        {
            var frmError = new frmError();
            try
            {
                dgName.DataSource = null;
                dgName.Rows.Clear();
                dgName.Columns.Clear();

                if (CON.State == ConnectionState.Closed)
                {
                    CON.ConnectionString = ConnSAGE;
                    CON.Open();
                }
                DA = new SqlDataAdapter(Query, CON);
                CON.Close();
                DT = new DataTable();
                DA.Fill(DT);
                if (DT.Rows.Count < 1)
                {
                    // MsgBox("Records not found. Please retry.")
                    DT = null;
                    DA = null;
                    return;
                }

                dgName.DataSource = DT;

                foreach (DataGridViewColumn Col in dgName.Columns)
                {
                    dgName.ReadOnly = false;
                    if (Strings.InStr(ColListToEdit, Col.Name) > 0)
                    {
                        Col.ReadOnly = false;
                    }
                    else
                    {
                        Col.ReadOnly = true;
                    }
                    if (Strings.InStr(ColListToHide, Col.Name) > 0)
                    {
                        Col.Visible = false;
                    }
                    else
                    {
                        Col.Visible = true;
                    }
                }
                dgName.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dgName.AutoResizeColumns();

                DT = null;
                DA = null;

                // dgName.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.Fill)

                // Resize the column Width to an average width.
                if (dgName.Columns.Count < 0)
                    return;
                //double aveWidth = dgName.Width / dgName.Columns.Count;
                //foreach (var clmn in dgName.Columns)
                //    clmn.Width = aveWidth;
            }
            catch (Exception ex)
            {
                frmError.txtError.AppendText("Error message: " + ex.Message);
                frmError.txtError.AppendText(Environment.NewLine);
                frmError.txtError.AppendText("Query : " + Query);
                frmError.ShowDialog();
            }



        }
        public static string ConvertQuotes(string str)
        {
            string ConvertQuotesRet = "";
            ConvertQuotesRet = str.Replace("'", "''");
            return ConvertQuotesRet;
        }
        public static void LoadCombo(string SQL, ComboBox CBO, string ValueField, string DisplayField, bool EnableAutoComplete, bool ShowErrorMessage)
        {
            if (CON.State == ConnectionState.Closed)
            {
                CON.ConnectionString = ConnSAGE;
                CON.Open();
            }
            CBO.DataSource = null;
            CBO.Text = "";
            DA = new SqlDataAdapter(SQL, CON);
            CON.Close();
            DT = new DataTable();
            DA.Fill(DT);
            if (DT.Rows.Count < 1 & ShowErrorMessage == true)
            {
                MessageBox.Show("Records not found. Please retry.");
            }
            else
            {
                CBO.DataSource = DT;
                CBO.ValueMember = ValueField;
                CBO.DisplayMember = DisplayField;
                if (EnableAutoComplete == true)
                {
                    CBO.DropDownStyle = ComboBoxStyle.DropDown;
                    CBO.AutoCompleteMode = AutoCompleteMode.Suggest;
                    CBO.AutoCompleteSource = AutoCompleteSource.ListItems;
                }
                else
                {
                    CBO.DropDownStyle = ComboBoxStyle.DropDownList;
                }
                CBO.SelectedIndex = -1;
            }
            DT = null;
            DA = null;
        }

        public static int LogAction(string items_SNO, string action)
        {
            // insert into  [WIZ_KENS_LOG] ("ENTRY_BY","ITEM_SNO","ACTION") VALUES ('KUNAL',33,'APPROVED')
            SQL = "insert into  [WIZ_KENS_LOG] ([ENTRY_BY],[ITEM_SNO],[ACTION]) VALUES ('" + activeUSER + "','" + items_SNO + "','" + action + "')";

            return ExecuteQueryAndReturnID(SQL);
        }

        public static int ExecuteQueryAndReturnID(string Query)
        {
            var frmError = new frmError();
            int ExecuteQueryAndReturnIDRet = 0;
            if (string.IsNullOrEmpty(Query))
            {
                ExecuteQueryAndReturnIDRet = -1;
                return ExecuteQueryAndReturnIDRet;
            }
            if (CON.State == ConnectionState.Closed)
            {
                CON.ConnectionString = ConnSAGE;
                CON.Open();
            }
            try
            {
                CMD = new SqlCommand(Query, CON);
                ExecuteQueryAndReturnIDRet = Convert.ToInt32(CMD.ExecuteScalar());
                CON.Close();
            }
            catch (Exception ex)
            {
                CON.Close();
                ExecuteQueryAndReturnIDRet = -1;
                frmError.ShowDialog();
                frmError.txtError.Text = Query;
                MessageBox.Show(ex.Message);
            }

            return ExecuteQueryAndReturnIDRet;

        }
        public static bool FetchSingleBooleanValue(string Query)
        {
            var frmError = new frmError();
            bool FetchSingleBooleanValueRet = false;
            if (string.IsNullOrEmpty(Query))
            {
                FetchSingleBooleanValueRet = false;
                return FetchSingleBooleanValueRet;
            }
            if (CON.State == ConnectionState.Closed)
            {
                CON.ConnectionString = ConnSAGE;
                CON.Open();
            }
            try
            {
                CMD = new SqlCommand(Query, CON);
                FetchSingleBooleanValueRet = Convert.ToBoolean(CMD.ExecuteScalar());
                CMD = null;
                CON.Close();
            }
            catch (Exception ex)
            {
                CON.Close();
                FetchSingleBooleanValueRet = false;
                frmError.ShowDialog();
                frmError.txtError.Text = Query;
                MessageBox.Show(ex.Message);
            }

            return FetchSingleBooleanValueRet;
        }

        public static void NumbersOnly(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            //if (e.KeyChar == (char)13)
            //{
            //    (sender as TextBox).Text = String.Format("{0:n}", decimal.Parse((sender as TextBox).Text));
            //}

            // only allow one decimal point 
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;               
            }

            if (Regex.IsMatch((sender as TextBox).Text, @"\.\d\d") && e.KeyChar != 8) { e.Handled = true; }
        }


        public static bool ExecuteQuery(string Query)
        {
            var frmError = new frmError();         
            bool ExecuteQueryRet = false;
            if (string.IsNullOrEmpty(Query))
            {
                ExecuteQueryRet = false;
                return ExecuteQueryRet;
            }
            if (CON.State == ConnectionState.Closed)
            {
                CON.ConnectionString = ConnSAGE;
                CON.Open();
            }
            try
            {
                CMD = new SqlCommand(Query, CON);
                CMD.ExecuteNonQuery();
                CON.Close();
            }
            catch (Exception ex)
            {
                CON.Close();
                ExecuteQueryRet = false;
                frmError.ShowDialog();
                frmError.txtError.Text = Query;
                MessageBox.Show(ex.Message);
            }
            ExecuteQueryRet = true;
            return ExecuteQueryRet;

        }
        //public static string GetFileName(string FileFilter, bool ReturnFullPath = false)
        //{
        //    string GetFileNameRet = "";
        //    string FN = "";
        //    var OFD = new OpenFileDialog();

        //    OFD.Title = "Select file for importation";
        //    OFD.InitialDirectory = ApplicationPath;
        //    OFD.Filter = FileFilter;
        //    if (OFD.ShowDialog == DialogResult.OK)
        //    {
        //        if (OFD.FileName != "")
        //        {
        //            if (ReturnFullPath == true)
        //            {
        //                FN = OFD.FileName;
        //            }
        //            else
        //            {
        //                FN = Path.GetFileName(OFD.FileName);
        //            }
        //        }

        //        else
        //        {
        //            // MsgBox("Please select a file and retry. A valid EXCEL file is needed for data import")
        //            FN = "";
        //        }
        //    }
        //    else
        //    {
        //        FN = "";
        //    }
        //    GetFileNameRet = FN;
        //    return GetFileNameRet;
        //}

        public static string AmountInWords(string nAmount, string wAmount = Constants.vbNullString, object nSet = null, string CurrencyCodeBig = null, string CurrencyCodeSmall = null)
        {
            // Let's make sure entered value is numeric
            if (!Information.IsNumeric(nAmount))
                return "Please enter numeric values only.";

            string tempDecValue = string.Empty;
            if (Conversions.ToBoolean(Strings.InStr(nAmount, ".")))
                tempDecValue = nAmount.Substring(nAmount.IndexOf("."));
            nAmount = Strings.Replace(nAmount, tempDecValue, string.Empty);

            try
            {
                long intAmount = Conversions.ToLong(nAmount);
                if (intAmount > 0L)
                {
                    nSet = Interaction.IIf(intAmount.ToString().Trim().Length / 3d > (long)Math.Round(intAmount.ToString().Trim().Length / 3d), (long)Math.Round(intAmount.ToString().Trim().Length / 3d) + 1L, (long)Math.Round(intAmount.ToString().Trim().Length / 3d));


                    long eAmount = Conversions.ToLong(Strings.Left(intAmount.ToString().Trim(), Convert.ToInt32(Operators.SubtractObject(intAmount.ToString().Trim().Length, Operators.MultiplyObject(Operators.SubtractObject(nSet, 1), 3)))));
                    long multiplier = Conversions.ToLong(Operators.ExponentObject(10, Operators.MultiplyObject(Operators.SubtractObject(nSet, 1), 3)));

                    var Ones = new string[] { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };


                    var Teens = new string[] { "", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };


                    var Tens = new string[] { "", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };


                    var HMBT = new string[] { "", "", "Thousand", "Million", "Billion", "Trillion", "Quadrillion", "Quintillion" };



                    intAmount = eAmount;

                    int nHundred = (int)(intAmount / 100L);
                    intAmount = intAmount % 100L;
                    int nTen = (int)(intAmount / 10L);
                    intAmount = intAmount % 10L;
                    int nOne = (int)(intAmount / 1L);

                    if (nHundred > 0)
                        wAmount = wAmount + Ones[nHundred] + " Hundred ";
                    // This is for hundreds                
                    if (nTen > 0) // This is for tens and teens
                    {
                        if (nTen == 1 & nOne > 0) // This is for teens 
                        {
                            wAmount = wAmount + Teens[nOne] + " ";
                        }
                        else // This is for tens, 10 to 90
                        {
                            wAmount = Convert.ToString(Operators.ConcatenateObject(wAmount + Tens[nTen], Interaction.IIf(nOne > 0, "-", " ")));
                            if (nOne > 0)
                                wAmount = wAmount + Ones[nOne] + " ";
                        }
                    }
                    else if (nOne > 0) // This is for ones, 1 to 9
                        wAmount = wAmount + Ones[nOne] + " ";
                    wAmount = wAmount + HMBT[Convert.ToInt32(nSet)] + " ";
                    wAmount = AmountInWords((Conversions.ToLong(nAmount) - eAmount * multiplier).ToString().Trim() + tempDecValue, wAmount, Operators.SubtractObject(nSet, 1));
                }
                else
                {
                    if (Conversion.Val(nAmount) == 0d)
                    {
                        nAmount = nAmount + tempDecValue;
                        tempDecValue = string.Empty;
                    }
                    if (Math.Round(Conversion.Val(nAmount), 2) * 100d > 0d)
                        wAmount = Strings.Trim(AmountInWords((Math.Round(Conversion.Val(nAmount), 2) * 100d).ToString(), wAmount.Trim() + " And ", 1)) + " ";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Encountered: " + ex.Message, "Convert Numbers To Words", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return "!#ERROR_ENCOUNTERED";
            }

            // Trap null values
            // If IsNothing(wAmount) = True Then wAmount = String.Empty Else wAmount = _
            // IIf(InStr(wAmount.Trim.ToLower, "pesos"), _
            // wAmount.Trim, wAmount.Trim & " Pesos")


            if (wAmount == null == true)
                wAmount = string.Empty;
            else
                wAmount = Convert.ToString(Interaction.IIf(Conversions.ToBoolean(Strings.InStr(wAmount.Trim().ToLower(), " ")), wAmount.Trim(), wAmount.Trim() + " "));


            wAmount = CurrencyCodeBig + " " + wAmount + " " + CurrencyCodeSmall;

            // Display the result
            return wAmount;
        }

        public static bool isDirectoryValid(string path)
        {
            if (Directory.Exists(path))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
