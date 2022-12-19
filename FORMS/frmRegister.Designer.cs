namespace KENSPOS.FORMS
{
    partial class frmRegister
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.cmdCommTest = new System.Windows.Forms.Button();
            this.txtCommPwd = new System.Windows.Forms.TextBox();
            this.txtCommUsr = new System.Windows.Forms.TextBox();
            this.txtCommServer = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.cboCommonDB = new System.Windows.Forms.ComboBox();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.cmdTest = new System.Windows.Forms.Button();
            this.Label5 = new System.Windows.Forms.Label();
            this.cboSageDB = new System.Windows.Forms.ComboBox();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.chkCopy = new System.Windows.Forms.CheckBox();
            this.cmdLoad = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdSave = new System.Windows.Forms.Button();
            this.GroupBox2.SuspendLayout();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.cmdCommTest);
            this.GroupBox2.Controls.Add(this.txtCommPwd);
            this.GroupBox2.Controls.Add(this.txtCommUsr);
            this.GroupBox2.Controls.Add(this.txtCommServer);
            this.GroupBox2.Controls.Add(this.Label2);
            this.GroupBox2.Controls.Add(this.Label7);
            this.GroupBox2.Controls.Add(this.Label8);
            this.GroupBox2.Controls.Add(this.Label6);
            this.GroupBox2.Controls.Add(this.cboCommonDB);
            this.GroupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupBox2.Location = new System.Drawing.Point(12, 186);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(320, 143);
            this.GroupBox2.TabIndex = 31;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "COMMON DB";
            // 
            // cmdCommTest
            // 
            this.cmdCommTest.Location = new System.Drawing.Point(233, 74);
            this.cmdCommTest.Name = "cmdCommTest";
            this.cmdCommTest.Size = new System.Drawing.Size(75, 23);
            this.cmdCommTest.TabIndex = 31;
            this.cmdCommTest.Text = "TEST";
            this.cmdCommTest.UseVisualStyleBackColor = true;
            this.cmdCommTest.Click += new System.EventHandler(this.cmdCommTest_Click);
            // 
            // txtCommPwd
            // 
            this.txtCommPwd.Location = new System.Drawing.Point(105, 77);
            this.txtCommPwd.Name = "txtCommPwd";
            this.txtCommPwd.PasswordChar = '*';
            this.txtCommPwd.Size = new System.Drawing.Size(100, 21);
            this.txtCommPwd.TabIndex = 30;
            // 
            // txtCommUsr
            // 
            this.txtCommUsr.Location = new System.Drawing.Point(105, 48);
            this.txtCommUsr.Name = "txtCommUsr";
            this.txtCommUsr.Size = new System.Drawing.Size(100, 21);
            this.txtCommUsr.TabIndex = 29;
            // 
            // txtCommServer
            // 
            this.txtCommServer.Location = new System.Drawing.Point(105, 17);
            this.txtCommServer.Name = "txtCommServer";
            this.txtCommServer.Size = new System.Drawing.Size(203, 21);
            this.txtCommServer.TabIndex = 28;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(34, 80);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(61, 15);
            this.Label2.TabIndex = 27;
            this.Label2.Text = "Password";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(27, 51);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(70, 15);
            this.Label7.TabIndex = 26;
            this.Label7.Text = "User Name";
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(18, 20);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(79, 15);
            this.Label8.TabIndex = 25;
            this.Label8.Text = "Server Name";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(13, 115);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(84, 15);
            this.Label6.TabIndex = 24;
            this.Label6.Text = "COMMON DB";
            // 
            // cboCommonDB
            // 
            this.cboCommonDB.FormattingEnabled = true;
            this.cboCommonDB.Location = new System.Drawing.Point(105, 112);
            this.cboCommonDB.Name = "cboCommonDB";
            this.cboCommonDB.Size = new System.Drawing.Size(203, 23);
            this.cboCommonDB.TabIndex = 23;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.cmdTest);
            this.GroupBox1.Controls.Add(this.Label5);
            this.GroupBox1.Controls.Add(this.cboSageDB);
            this.GroupBox1.Controls.Add(this.txtPwd);
            this.GroupBox1.Controls.Add(this.txtUser);
            this.GroupBox1.Controls.Add(this.txtServer);
            this.GroupBox1.Controls.Add(this.Label4);
            this.GroupBox1.Controls.Add(this.Label3);
            this.GroupBox1.Controls.Add(this.Label1);
            this.GroupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupBox1.Location = new System.Drawing.Point(12, 12);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(320, 143);
            this.GroupBox1.TabIndex = 30;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "SAGE DB";
            // 
            // cmdTest
            // 
            this.cmdTest.Location = new System.Drawing.Point(228, 77);
            this.cmdTest.Name = "cmdTest";
            this.cmdTest.Size = new System.Drawing.Size(75, 23);
            this.cmdTest.TabIndex = 24;
            this.cmdTest.Text = "TEST";
            this.cmdTest.UseVisualStyleBackColor = true;
            this.cmdTest.Click += new System.EventHandler(this.cmdTest_Click);
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(29, 113);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(59, 15);
            this.Label5.TabIndex = 23;
            this.Label5.Text = "SAGE DB";
            // 
            // cboSageDB
            // 
            this.cboSageDB.FormattingEnabled = true;
            this.cboSageDB.Location = new System.Drawing.Point(101, 109);
            this.cboSageDB.Name = "cboSageDB";
            this.cboSageDB.Size = new System.Drawing.Size(203, 23);
            this.cboSageDB.TabIndex = 22;
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(100, 80);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(100, 21);
            this.txtPwd.TabIndex = 21;
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(100, 51);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(100, 21);
            this.txtUser.TabIndex = 20;
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(100, 20);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(203, 21);
            this.txtServer.TabIndex = 19;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(29, 83);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(61, 15);
            this.Label4.TabIndex = 18;
            this.Label4.Text = "Password";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(22, 54);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(70, 15);
            this.Label3.TabIndex = 17;
            this.Label3.Text = "User Name";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(13, 23);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(79, 15);
            this.Label1.TabIndex = 16;
            this.Label1.Text = "Server Name";
            // 
            // chkCopy
            // 
            this.chkCopy.AutoSize = true;
            this.chkCopy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCopy.Location = new System.Drawing.Point(104, 160);
            this.chkCopy.Margin = new System.Windows.Forms.Padding(2);
            this.chkCopy.Name = "chkCopy";
            this.chkCopy.Size = new System.Drawing.Size(113, 21);
            this.chkCopy.TabIndex = 29;
            this.chkCopy.Text = "Copy Above";
            this.chkCopy.UseVisualStyleBackColor = true;
            // 
            // cmdLoad
            // 
            this.cmdLoad.Location = new System.Drawing.Point(212, 335);
            this.cmdLoad.Name = "cmdLoad";
            this.cmdLoad.Size = new System.Drawing.Size(75, 23);
            this.cmdLoad.TabIndex = 28;
            this.cmdLoad.Text = "LOAD";
            this.cmdLoad.UseVisualStyleBackColor = true;
            this.cmdLoad.Click += new System.EventHandler(this.cmdLoad_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(127, 335);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 27;
            this.cmdCancel.Text = "CANCEL";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // cmdSave
            // 
            this.cmdSave.Enabled = false;
            this.cmdSave.Location = new System.Drawing.Point(42, 335);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
            this.cmdSave.TabIndex = 26;
            this.cmdSave.Text = "SAVE";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // frmRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(347, 369);
            this.Controls.Add(this.GroupBox2);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.chkCopy);
            this.Controls.Add(this.cmdLoad);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdSave);
            this.Name = "frmRegister";
            this.Text = "frmRegister";
            this.Load += new System.EventHandler(this.frmRegister_Load);
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.GroupBox GroupBox2;
        internal System.Windows.Forms.Button cmdCommTest;
        internal System.Windows.Forms.TextBox txtCommPwd;
        internal System.Windows.Forms.TextBox txtCommUsr;
        internal System.Windows.Forms.TextBox txtCommServer;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.ComboBox cboCommonDB;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.Button cmdTest;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.ComboBox cboSageDB;
        internal System.Windows.Forms.TextBox txtPwd;
        internal System.Windows.Forms.TextBox txtUser;
        internal System.Windows.Forms.TextBox txtServer;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.CheckBox chkCopy;
        internal System.Windows.Forms.Button cmdLoad;
        internal System.Windows.Forms.Button cmdCancel;
        internal System.Windows.Forms.Button cmdSave;
    }
}