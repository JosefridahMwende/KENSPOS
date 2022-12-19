using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using KENSPOS.FORMS;
using static KENSPOS.MODULES.CommonFunctions;

namespace KENSPOS.MODULES
{

    static class rptMaster
    {
        public static string RPT_NAME = "";
        public static bool SaveRptAsPDF;
        public static bool SendRptByEmail;
        public static int INVID;
        public static string OrderNum;
        public static string SaleDateFrom;
        public static string SaleDateTo;
        public static string DateFrom;
        public static string DateTo;
        public static string JobCard;
        public static int ReceiptNo;
        public static bool PrintingRequired;
        public static bool PreviewRequired;
        public static string InvNum = "NONE";
        public static string Recipient = "pj@tikone.biz";
        public static string Sender = "pj@wizag.biz";
        public static string Message = "This is your email.";
        public static string EmailHeader;

        private static ReportDocument cryRpt = new ReportDocument();
        private static ConnectionInfo ConnInfo = new ConnectionInfo();
        private static TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
        private static TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
        private static ConnectionInfo crConnectionInfo = new ConnectionInfo();


        private static string FPath = Application.ExecutablePath;
        private static int J = Strings.InStrRev(FPath, @"\");
        private static string AppFolder = Strings.Mid(AppFolder, 1, J);
        private static string RptFolder = AppFolder + @"\ReportsHome";
        private static string RptLocation = "";
        private static string PDFFileName;


        // Public Sub RunReports()
        // Dim DT As DataTable
        // Dim SQL As String
        // Dim RptID As Int16
        // Dim RecipientName As String

        // SQL = "Select M.SNo, M.ReportFileName, M.ReportDesc, L.RecipientName, L.RecipientEmail  from WIZ_BI_RPT_MSTR M INNER JOIN [WIZ_BI_EMAIL_LIST] L ON M.SNo = L.ReportID"

        // DT = New DataTable
        // Call LoadDatatable(SQL, DT)
        // For Each Row As DataRow In DT.Rows

        // RPT_NAME = "\REPORTS\" & Row.Item("ReportFileName").ToString
        // Recipient = Row.Item("RecipientEmail").ToString
        // RecipientName = Row.Item("RecipientName").ToString
        // RptID = Convert.ToInt16(Row.Item("SNo").ToString)
        // PDFFileName = "\" & RecipientName & "-" &
        // Format(Now, "yyyy-MM-dd-HH-mm") & ".pdf"

        // Call PrepareReport()

        // If SaveRptAsPDF = True Then Call ExportRptAsPDF()

        // If SendRptByEmail = True Then Call SendReportByEmail(Recipient, Sender, Message)


        // Application.DoEvents()

        // SQL = "INSERT INTO WIZ_BI_EMAIL_LOG([ReportID], [RecipientName], [EmailAddress], [SentOn]) VALUES(" & RptID & ", '" & RecipientName & "', '" &
        // Recipient & "', GETDATE() )"
        // ExecuteQuery(SQL)
        // Next
        // End Sub

        //public static void RunReports(string Email)
        //{
        //    // you want to split this input string
        //    Cursor.Current = Cursors.WaitCursor;
        //    //frmSales.btnEmail.Text = "Sending..";
        //    string RecipientEmails = Email;

        //    // Split string based on comma
        //    string[] RecipientEmail = RecipientEmails.Split(new char[] { ',' });

        //    // Use For Each loop over words and display them

        //    string e;
        //    foreach (var e in RecipientEmail)
        //    {
        //        Console.WriteLine(e);
        //        Recipient = e;
        //        PDFFileName = @"\" + OrderNum + "-" + Strings.Format(DateTime.Now, "yyyy-MM-dd-HH-mm") + ".pdf";

        //        EmailHeader = frmSales.GetDocType(OrderNum) + " : " + OrderNum;
        //        Message = "Attached is " + frmSales.GetDocType(OrderNum) + " : " + OrderNum;

        //        PrepareReport();

        //        if (SaveRptAsPDF == true)
        //            ExportRptAsPDF();

        //        if (SendRptByEmail == true)
        //            SendReportByEmail(Recipient, Sender, Message, EmailHeader);
        //    }

        //    frmSales.btnEmail.Text = "Email";
        //    Cursor.Current = Cursors.Default;
        //}

        public static void PrepareReport()
        {
            try
            {
                Tables CrTables;
                //Table CrTable;

                FPath = Application.ExecutablePath;
                J = Strings.InStrRev(FPath, @"\");
                FPath = Strings.Mid(FPath, 1, J);
                RptFolder = FPath + @"REPORTS";

                AppFolder = @RptFolder + @RPT_NAME;

                cryRpt.Load(AppFolder);
                cryRpt.SummaryInfo.ReportTitle = InvNum;
                // cryRpt.FileName = "pj_123"

                SetReportParams();

                {
                    var withBlock = crConnectionInfo;
                    withBlock.ServerName = CommonFunctions.SageServerName;
                    withBlock.DatabaseName = CommonFunctions.SageDBName;
                    withBlock.UserID = CommonFunctions.UserName;
                    withBlock.Password = CommonFunctions.Password;
                }

                CrTables = cryRpt.Database.Tables;

                foreach (Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                }

                PrintDialog dialog1 = new PrintDialog();
                dialog1.AllowSomePages = true;
                dialog1.AllowPrintToFile = false;

                

                if (PrintingRequired == true)
                {
                    // ***  Uncomment the following to enable printing
                    //cryRpt.PrintOptions.PrinterName = CommonFunctions.Default_Printer;
                    //cryRpt.PrintToPrinter(1, false, 0, 0);

                    if (dialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        int copies = dialog1.PrinterSettings.Copies;
                        int fromPage = dialog1.PrinterSettings.FromPage;
                        int toPage = dialog1.PrinterSettings.ToPage;
                        bool collate = dialog1.PrinterSettings.Collate;

                        cryRpt.PrintOptions.PrinterName = dialog1.PrinterSettings.PrinterName;
                        cryRpt.PrintToPrinter(copies, collate, fromPage, toPage);
                    }

                    cryRpt.Dispose();
                    dialog1.Dispose();
                }

                if (PreviewRequired == true)
                {
                    // *** Uncomment the following to view the report
                    ReportViewer reportViewer = new ReportViewer();
                    reportViewer.CRPT.ReportSource = cryRpt;
                    reportViewer.CRPT.Refresh();
                    reportViewer.Show();
                }
            }
            catch (Exception ex)
            {
                frmError err = new frmError();
                err.txtError.Text = ex.Message.ToString();
                err.ShowDialog();
            }
        }

        public static void PrintReport()
        {
            // Check if Default printer is defined
            if (Default_Printer == "NONE")
            {
                Interaction.MsgBox("Default printer not defined. Please open settings, select printer name and retry.");
                return;
            }

            PrepareReport();
        }

        private static void SetReportParams()
        {
            if (RPT_NAME == @"\OrderConfirmation.rpt")
                cryRpt.SetParameterValue("OrderNo", OrderNum);
            else if (RPT_NAME == @"\Quote.rpt")
                cryRpt.SetParameterValue("OrderNo", OrderNum);
           
        }

        private static void ExportRptAsPDF()
        {
            try
            {
                ExportOptions CrExportOptions;
                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();


                // Check if the folder exists. If not add it
                if ((!System.IO.Directory.Exists(RptFolder)))
                    System.IO.Directory.CreateDirectory(RptFolder);

                // RptLocation = RptFolder & "\crystalExport.pdf"
                RptLocation = RptFolder + PDFFileName;
                CrDiskFileDestinationOptions.DiskFileName = RptLocation;
                CrExportOptions = cryRpt.ExportOptions;

                {
                    var withBlock = CrExportOptions;
                    withBlock.ExportDestinationType = ExportDestinationType.DiskFile;
                    withBlock.ExportFormatType = ExportFormatType.PortableDocFormat;
                    withBlock.DestinationOptions = CrDiskFileDestinationOptions;
                    withBlock.FormatOptions = CrFormatTypeOptions;
                }
                cryRpt.Export();
            }
            // MsgBox("Report Exported Successfully!")
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.ToString());
            }
        }

        //private static void SendReportByEmail(string Recipient, string Sender, string Message, string EmailHeader)
        //{
        //    if (!CommonFunctions.GET_EMAIL_SETTINGS)
        //    {
        //        Interaction.MsgBox("Error in Email configurations. Please get in touch with the developers");
        //        return;
        //    }
        //    Sender = CommonFunctions.EmailUserName;
        //    if (SendEmail(Recipient, Recipient, Sender, Sender, Message, true, RptLocation, EmailHeader) == true)
        //        Interaction.MsgBox("Email sent successfully!");
        //    else
        //        Interaction.MsgBox("Email settings have issue. Please re-enter the settings and try again.");
        //}
    }
}