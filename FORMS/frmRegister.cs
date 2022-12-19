using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic; // Install-Package Microsoft.VisualBasic
using Microsoft.VisualBasic.CompilerServices; // Install-Package Microsoft.VisualBasic
using KENSPOS.MODULES;
using TIMS.MODULES;

namespace KENSPOS.FORMS
{
    public partial class frmRegister : Form
    {
        public static frmRegister Self;
        public String FPath = "";
        public String PwdKey = "65f02fab40f56c8e409b121965fa1d657a80d237fabacab95f9c59f8f967becb";
        public int J = 0;

        public String Conn = "";
        private SqlConnection CON = new SqlConnection();
        private SqlCommand CMD = new SqlCommand();
        private DataTable DT = null;
        private SqlDataAdapter DA;
        private DataSet DS;
        private SqlDataReader RS;//       
        public String FormToLaunch = "MAIN";
        public String DB = "";
        public String User = "";
        public String PWD = "";
        public String CommonDB = "";
        public String SageDB = "";
        public String CommDB = "";
        public String CommUsr = "";
        public String CommPwd = "";
        public String SQL = "";
        public String SQL1 = "";
        public String ConnStrSAGE = "";
        public String ConnStrCOMM = "";
        public Boolean ProceedFurther = false;
        public static string AppLicenceType, StartDate = string.Empty;
        public static double AppDaysRemaining=0;

        public frmRegister()
        {
            InitializeComponent();
           
        }

        private void frmRegister_Load(object sender, System.EventArgs e)
        {     
            FPath = Application.ExecutablePath; // & "\AppInfo.Log"
            J = Strings.InStrRev(FPath, @"\");
            FPath = Strings.Mid(FPath, 1, J);
            FPath = FPath + "AppInfo.Log";

            if (File.Exists(FPath) == false)
            {
                // Debug.Print(FPath)
                Interaction.MsgBox("Please enter Server login details");
            }
            else
            {
                ConnStrSAGE = MakeConn();

                try
                {
                    CreateSQLObjects();
                }
                catch (Exception ex)
                {
                    Interaction.MsgBox(Information.Err().Description);
                }

                try
                {                                

                    CommonFunctions.SageServerName = DB;
                    CommonFunctions.SageDBName = SageDB;
                    CommonFunctions.CommServerName = CommDB;
                    CommonFunctions.CommonDBName = CommonDB;
                    CommonFunctions.UserName = User;
                    CommonFunctions.Password = PWD;
                    CommonFunctions.ConnSAGE = ConnStrSAGE;
                    CommonFunctions.ConnCOMM = ConnStrCOMM;
                    CommonFunctions.ApplicationPath = Strings.Replace(FPath, "AppInfo.Log", "");                    

                    //TODO: Check if licence file exits

                    if (FormToLaunch == "MAIN")
                    {                       
                        var frmLogin = new frmLogin();
                        frmLogin.ShowDialog();
                    }
                    this.Close();

                    //FPath = Application.ExecutablePath;
                    //J = Strings.InStrRev(FPath, @"\");
                    //FPath = Strings.Mid(FPath, 1, J);
                    //FPath = FPath + "licence-gen.lic";

                    //if (!File.Exists(FPath))
                    //{
                    //    //MessageBox.Show("Licence for user is missing", "Application Licence", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    //using (frmLicence frm = new frmLicence())
                    //    //{
                    //    //    frm.ShowDialog();
                    //    //}
                    //    //this.Close();
                    //}
                    //else
                    //{
                    //    MachineInfo machineInfo = new MachineInfo();
                    //    machineInfo = licenceInfo.LoadLicence();
                    //    machineInfo.isValid = true;
                    //    if (machineInfo.isValid){
                    //        if (FormToLaunch == "MAIN")
                    //        {
                    //            //CommonFunctions.AppLicenceType = machineInfo.LicenceType;
                    //            ////AppDaysRemaining = machineInfo.Days;
                    //            //StartDate = machineInfo.StartDate;
                    //            //DateTime sTartDate = Convert.ToDateTime(StartDate);
                    //            //DateTime endDate = sTartDate.AddDays(machineInfo.Days);
                    //            //CommonFunctions.AppDaysRemaining = (endDate - sTartDate).TotalDays;
                    //            var frmFileWatcher = new frmFileWatcher();
                    //            frmFileWatcher.ShowDialog();
                    //        }
                    //        this.Close();
                    //    }
                    //    else
                    //    {    
                    //        using (frmLicence frm = new frmLicence())
                    //        {
                    //            frm.ShowDialog();
                    //        }
                    //        //this.Close();
                    //    }
                    //}

                    ////if (FormToLaunch == "MAIN")
                    ////{
                    ////    var frmFileWatcher = new frmFileWatcher();
                    ////    frmFileWatcher.ShowDialog();                        
                    ////}
                     
                    ////this.Close();
                }
                catch (Exception ex)
                {
                    // MsgBox(Err.Description)
                    Interaction.MsgBox("Error found in the Database settings. Please re-enter the settings and retry" + ex);
                }
            }
        }

       

        private string MakeConn()
        {
            string MakeConnRet = "";
            var wrapper = new Simple3Des(PwdKey);
            int I = 1;

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
                                    DB = wrapper.DecryptData(line);
                                    break;
                                }
                            case 2:
                                {
                                    User = wrapper.DecryptData(line);
                                    break;
                                }
                            case 3:
                                {
                                    PWD = wrapper.DecryptData(line);
                                    break;
                                }
                            case 4:
                                {
                                    SageDB = wrapper.DecryptData(line);
                                    break;
                                }
                            case 5:
                                {
                                    CommDB = wrapper.DecryptData(line);
                                    break;
                                }
                            case 6:
                                {
                                    CommUsr = wrapper.DecryptData(line);
                                    break;
                                }
                            case 7:
                                {
                                    CommPwd = wrapper.DecryptData(line);
                                    break;
                                }
                            case 8:
                                {
                                    CommonDB = wrapper.DecryptData(line);
                                    break;
                                }
                        }
                        I += 1;
                    }
                }
                readFile.Close();
                readFile = null;
                Conn = "Data Source=" + DB + ";";
                Conn = Conn + "Initial Catalog=" + SageDB + ";";
                Conn = Conn + "User ID=" + User + ";";
                Conn = Conn + "Password=" + PWD + ";";
            }
            catch (IOException ex)
            {
                Interaction.MsgBox(ex.ToString());
            }

            MakeConnRet = Conn;
            // Debug.Print("SAGEDB " & Conn)
            Conn = "";

            Conn = "Data Source=" + CommDB + ";";
            Conn = Conn + "Initial Catalog=" + CommonDB + ";";
            Conn = Conn + "User ID=" + CommUsr + ";";
            Conn = Conn + "Password=" + CommPwd + ";";
            ConnStrCOMM = Conn;
            return MakeConnRet;
            // Debug.Print("COMMONDB " & Conn)
        }

        private void CreateSQLObjects()
        {
            string tblSQL = "";

            tblSQL = " If Not Exists (Select * from sys.tables Where Name = 'WIZ_DOC_APPROVAL') " + Constants.vbCrLf + "   CREATE TABLE [dbo].[WIZ_DOC_APPROVAL]([DocIndex] [int] NULL, [Mgr_Approve] [bit] NULL, [Dir__Approve] [bit] NULL) ON [PRIMARY] ";


            if (CON.State == ConnectionState.Open)
            {
                CON.Close();
            }

            CON.ConnectionString = ConnStrSAGE;
            try
            {
                CON.Open();
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
                return;
            }
            CMD.CommandText = tblSQL;
            CMD.CommandType = CommandType.Text;
            CMD.Connection = CON;
            try
            {
                CMD.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
            }

            tblSQL = " If Not Exists (Select * from sys.tables Where Name = 'WIZ_DOC_APPROVERS') " + Constants.vbCrLf + "   CREATE TABLE [dbo].[WIZ_DOC_APPROVERS]([ID] [int] NULL, [APP_NAME] [varchar](50) NULL, [IS_MGR] [bit] NULL, [IS_DIR] [bit] NULL, [CRN] [bit] NULL, [SO] [bit] NULL, [GRV] [bit] NULL, [PO] [bit] NULL, [RTS] [bit] NULL ) ON [PRIMARY]";


            CMD.CommandText = tblSQL;
            CMD.CommandType = CommandType.Text;
            CMD.Connection = CON;
            try
            {
                CMD.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
            }


            tblSQL = "IF EXISTS (Select * from sys.triggers Where Name = 'WIZ_TRG_APPROVE_DOCS') " + Constants.vbCrLf + " DROP TRIGGER  WIZ_TRG_APPROVE_DOCS";
            CMD.CommandText = tblSQL;
            CMD.CommandType = CommandType.Text;
            CMD.Connection = CON;
            try
            {
                CMD.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message.ToString());
            }

        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            var wrapper = new Simple3Des(PwdKey);

            if (cboCommonDB.SelectedIndex == -1)
            {
                Interaction.MsgBox("Please select Common Database Name");
                return;
            }
            else if (cboSageDB.SelectedIndex == -1)
            {
                Interaction.MsgBox("Please select SAGE Database Name");
                return;
            }
            else if (cboCommonDB.SelectedIndex == cboSageDB.SelectedIndex)
            {
                Interaction.MsgBox("Please select different Database Names");
                return;
            }

            try
            {
                TextWriter writeFile = new StreamWriter(FPath);
                writeFile.WriteLine(wrapper.EncryptData(txtServer.Text));    // LINE 1: SAGE DB SERVER NAME
                writeFile.WriteLine(wrapper.EncryptData(txtUser.Text));      // LINE 2: SAGE DB USER NAME
                writeFile.WriteLine(wrapper.EncryptData(txtPwd.Text));       // LINE 3: SAGE DB PASSWORD
                writeFile.WriteLine(wrapper.EncryptData(cboSageDB.Text));    // LINE 4: SAGE DB NAME

                writeFile.WriteLine(wrapper.EncryptData(txtCommServer.Text));    // LINE 5: COMMON DB SERVER NAME
                writeFile.WriteLine(wrapper.EncryptData(txtCommUsr.Text));       // LINE 6: COMMON DB USER NAME
                writeFile.WriteLine(wrapper.EncryptData(txtCommPwd.Text));       // LINE 7: COMMON DB PASSWORD
                writeFile.WriteLine(wrapper.EncryptData(cboCommonDB.Text));      // LINE 8: COMMON DB NAME

                writeFile.Flush();
                writeFile.Close();
                writeFile = null;
            }
            catch (IOException ex)
            {
                Interaction.MsgBox(ex.ToString());
            }
            Interaction.MsgBox("Server details saved successfully");
            this.Close();
        }

        private void cmdTest_Click(object sender, EventArgs e)
        {
            TestAndAddData("SAGE");
        }
        private void TestAndAddData(string DBName)
        {
            if (DBName == "SAGE")
            {
                SQL = "Data Source=" + txtServer.Text + ";User Id=" + txtUser.Text + ";Password=" + txtPwd.Text;
            }
            else if (DBName == "COMMON")
            {
                SQL = "Data Source=" + txtCommServer.Text + ";User Id=" + txtCommUsr.Text + ";Password=" + txtCommPwd.Text;
            }

            CON.ConnectionString = SQL;
            try
            {
                CON.Open();
                SQL = "SELECT name FROM master.sys.databases Order By Name";
                CMD.CommandText = SQL;
                CMD.CommandType = CommandType.Text;
                CMD.Connection = CON;

                RS = CMD.ExecuteReader();
                if (RS.HasRows)
                {
                    cboCommonDB.Items.Clear();
                    if (DBName == "SAGE")
                    {
                        cboSageDB.Items.Clear();
                    }
                    else if (DBName == "COMMON")
                    {
                        cboCommonDB.Items.Clear();
                    }

                    while (RS.Read())
                    {
                        if (DBName == "SAGE")
                        {
                            cboSageDB.Items.Add(RS["name"]);
                        }
                        else if (DBName == "COMMON")
                        {
                            cboCommonDB.Items.Add(RS["name"]);
                        }
                    }
                }
                RS.Close();
                Interaction.MsgBox("Database login succeeded. Please select a database and SAVE the setting.");
                ProceedFurther = true;
                cmdSave.Enabled = true;
                CON.Close();
            }
            catch (Exception ex)
            {
                Interaction.MsgBox("Please check the login details and retry");
                ProceedFurther = false;
                return;
            }
        }

        private void cmdLoad_Click(object sender, EventArgs e)
        {
            ReadFile();
        }

        private void cmdCommTest_Click(object sender, EventArgs e)
        {
            TestAndAddData("COMMON");
        }

        public void ReadFile()
        {
            Simple3Des wrapper = new Simple3Des(PwdKey);
            int I = 1;
            try
            {
                string line;
                System.IO.TextReader readFile = new StreamReader(FPath);
                while (true)
                {
                    line = readFile.ReadLine();
                    if (line == null)
                        break;
                    else
                    {
                        switch (I)
                        {
                            case 1:
                                {
                                    txtServer.Text = wrapper.DecryptData(line);
                                    break;
                                }

                            case 2:
                                {
                                    txtUser.Text = wrapper.DecryptData(line);
                                    break;
                                }

                            case 3:
                                {
                                    txtPwd.Text = wrapper.DecryptData(line);
                                    break;
                                }

                            case 4:
                                {
                                    cboSageDB.Text = wrapper.DecryptData(line);
                                    break;
                                }

                            case 5:
                                {
                                    txtCommServer.Text = wrapper.DecryptData(line);
                                    break;
                                }

                            case 6:
                                {
                                    txtCommUsr.Text = wrapper.DecryptData(line);
                                    break;
                                }

                            case 7:
                                {
                                    txtCommPwd.Text = wrapper.DecryptData(line);
                                    break;
                                }

                            case 8:
                                {
                                    cboCommonDB.Text = wrapper.DecryptData(line);
                                    break;
                                }
                        }
                        I += 1;
                    }
                }
                readFile.Close();
                readFile = null;
            }
            catch (IOException ex)
            {
                Interaction.MsgBox(ex.ToString());
            }
            Interaction.MsgBox("Server data loaded successfully");
        }

    }
}
