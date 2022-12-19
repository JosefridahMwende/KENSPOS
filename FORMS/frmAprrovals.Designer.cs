
namespace KENSPOS.FORMS
{
    partial class frmAprrovals
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tcQuotes = new System.Windows.Forms.TabPage();
            this.dgApproveQuotes = new System.Windows.Forms.DataGridView();
            this.Filters = new System.Windows.Forms.GroupBox();
            this.btnQuotesClear = new System.Windows.Forms.Button();
            this.btnLoadUAQuotes = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpSOTo = new System.Windows.Forms.DateTimePicker();
            this.dtpSOFrom = new System.Windows.Forms.DateTimePicker();
            this.chkSOApproved = new System.Windows.Forms.CheckBox();
            this.chkSORejected = new System.Windows.Forms.CheckBox();
            this.rdBtnSOAll = new System.Windows.Forms.RadioButton();
            this.rdBtnSOMine = new System.Windows.Forms.RadioButton();
            this.btLoadUASO = new System.Windows.Forms.TabPage();
            this.dgUASalesOrders = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnLoadUASO = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpToSO = new System.Windows.Forms.DateTimePicker();
            this.dtpFromSO = new System.Windows.Forms.DateTimePicker();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.tabControl1.SuspendLayout();
            this.tcQuotes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgApproveQuotes)).BeginInit();
            this.Filters.SuspendLayout();
            this.btLoadUASO.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgUASalesOrders)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tcQuotes);
            this.tabControl1.Controls.Add(this.btLoadUASO);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(12, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1299, 573);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tcQuotes
            // 
            this.tcQuotes.Controls.Add(this.dgApproveQuotes);
            this.tcQuotes.Controls.Add(this.Filters);
            this.tcQuotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tcQuotes.Location = new System.Drawing.Point(4, 33);
            this.tcQuotes.Name = "tcQuotes";
            this.tcQuotes.Padding = new System.Windows.Forms.Padding(3);
            this.tcQuotes.Size = new System.Drawing.Size(1291, 536);
            this.tcQuotes.TabIndex = 0;
            this.tcQuotes.Text = "Quotes";
            this.tcQuotes.UseVisualStyleBackColor = true;
            // 
            // dgApproveQuotes
            // 
            this.dgApproveQuotes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgApproveQuotes.Location = new System.Drawing.Point(23, 230);
            this.dgApproveQuotes.Name = "dgApproveQuotes";
            this.dgApproveQuotes.Size = new System.Drawing.Size(1235, 281);
            this.dgApproveQuotes.TabIndex = 1;
            this.dgApproveQuotes.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgApproveQuotes_CellContentDoubleClick);
            // 
            // Filters
            // 
            this.Filters.Controls.Add(this.btnQuotesClear);
            this.Filters.Controls.Add(this.btnLoadUAQuotes);
            this.Filters.Controls.Add(this.label1);
            this.Filters.Controls.Add(this.label2);
            this.Filters.Controls.Add(this.dtpSOTo);
            this.Filters.Controls.Add(this.dtpSOFrom);
            this.Filters.Controls.Add(this.chkSOApproved);
            this.Filters.Controls.Add(this.chkSORejected);
            this.Filters.Controls.Add(this.rdBtnSOAll);
            this.Filters.Controls.Add(this.rdBtnSOMine);
            this.Filters.Location = new System.Drawing.Point(23, 6);
            this.Filters.Name = "Filters";
            this.Filters.Size = new System.Drawing.Size(389, 205);
            this.Filters.TabIndex = 0;
            this.Filters.TabStop = false;
            this.Filters.Text = "Filter";
            // 
            // btnQuotesClear
            // 
            this.btnQuotesClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuotesClear.Location = new System.Drawing.Point(220, 158);
            this.btnQuotesClear.Name = "btnQuotesClear";
            this.btnQuotesClear.Size = new System.Drawing.Size(75, 30);
            this.btnQuotesClear.TabIndex = 57;
            this.btnQuotesClear.Text = "Clear";
            this.btnQuotesClear.UseVisualStyleBackColor = true;
            // 
            // btnLoadUAQuotes
            // 
            this.btnLoadUAQuotes.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadUAQuotes.Location = new System.Drawing.Point(109, 158);
            this.btnLoadUAQuotes.Name = "btnLoadUAQuotes";
            this.btnLoadUAQuotes.Size = new System.Drawing.Size(69, 31);
            this.btnLoadUAQuotes.TabIndex = 56;
            this.btnLoadUAQuotes.Text = "Load";
            this.btnLoadUAQuotes.UseVisualStyleBackColor = true;
            this.btnLoadUAQuotes.Click += new System.EventHandler(this.btnLoadUAQuotes_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(57, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 24);
            this.label1.TabIndex = 49;
            this.label1.Text = "To";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(57, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 24);
            this.label2.TabIndex = 48;
            this.label2.Text = "From";
            // 
            // dtpSOTo
            // 
            this.dtpSOTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpSOTo.CustomFormat = "dd-MMM-yy";
            this.dtpSOTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpSOTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSOTo.Location = new System.Drawing.Point(118, 124);
            this.dtpSOTo.Name = "dtpSOTo";
            this.dtpSOTo.Size = new System.Drawing.Size(210, 29);
            this.dtpSOTo.TabIndex = 55;
            // 
            // dtpSOFrom
            // 
            this.dtpSOFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpSOFrom.CustomFormat = "dd-MMM-yy";
            this.dtpSOFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpSOFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSOFrom.Location = new System.Drawing.Point(118, 92);
            this.dtpSOFrom.Name = "dtpSOFrom";
            this.dtpSOFrom.Size = new System.Drawing.Size(210, 29);
            this.dtpSOFrom.TabIndex = 54;
            // 
            // chkSOApproved
            // 
            this.chkSOApproved.AutoSize = true;
            this.chkSOApproved.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSOApproved.Location = new System.Drawing.Point(80, 58);
            this.chkSOApproved.Name = "chkSOApproved";
            this.chkSOApproved.Size = new System.Drawing.Size(112, 28);
            this.chkSOApproved.TabIndex = 52;
            this.chkSOApproved.Text = "Approved";
            this.chkSOApproved.UseVisualStyleBackColor = true;
            // 
            // chkSORejected
            // 
            this.chkSORejected.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSORejected.AutoSize = true;
            this.chkSORejected.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSORejected.Location = new System.Drawing.Point(200, 58);
            this.chkSORejected.Name = "chkSORejected";
            this.chkSORejected.Size = new System.Drawing.Size(104, 28);
            this.chkSORejected.TabIndex = 53;
            this.chkSORejected.Text = "Rejected";
            this.chkSORejected.UseVisualStyleBackColor = true;
            // 
            // rdBtnSOAll
            // 
            this.rdBtnSOAll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rdBtnSOAll.AutoSize = true;
            this.rdBtnSOAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdBtnSOAll.Location = new System.Drawing.Point(200, 19);
            this.rdBtnSOAll.Name = "rdBtnSOAll";
            this.rdBtnSOAll.Size = new System.Drawing.Size(49, 28);
            this.rdBtnSOAll.TabIndex = 51;
            this.rdBtnSOAll.Text = "All";
            this.rdBtnSOAll.UseVisualStyleBackColor = true;
            // 
            // rdBtnSOMine
            // 
            this.rdBtnSOMine.AutoSize = true;
            this.rdBtnSOMine.Checked = true;
            this.rdBtnSOMine.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdBtnSOMine.Location = new System.Drawing.Point(80, 19);
            this.rdBtnSOMine.Name = "rdBtnSOMine";
            this.rdBtnSOMine.Size = new System.Drawing.Size(70, 28);
            this.rdBtnSOMine.TabIndex = 50;
            this.rdBtnSOMine.TabStop = true;
            this.rdBtnSOMine.Text = "Mine";
            this.rdBtnSOMine.UseVisualStyleBackColor = true;
            // 
            // btLoadUASO
            // 
            this.btLoadUASO.Controls.Add(this.dgUASalesOrders);
            this.btLoadUASO.Controls.Add(this.groupBox1);
            this.btLoadUASO.Location = new System.Drawing.Point(4, 33);
            this.btLoadUASO.Name = "btLoadUASO";
            this.btLoadUASO.Padding = new System.Windows.Forms.Padding(3);
            this.btLoadUASO.Size = new System.Drawing.Size(1291, 536);
            this.btLoadUASO.TabIndex = 1;
            this.btLoadUASO.Text = "Sales Orders";
            this.btLoadUASO.UseVisualStyleBackColor = true;
            // 
            // dgUASalesOrders
            // 
            this.dgUASalesOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgUASalesOrders.Location = new System.Drawing.Point(23, 230);
            this.dgUASalesOrders.Name = "dgUASalesOrders";
            this.dgUASalesOrders.Size = new System.Drawing.Size(1235, 281);
            this.dgUASalesOrders.TabIndex = 3;
            this.dgUASalesOrders.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgApproveQuotes_CellContentDoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.btnLoadUASO);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.dtpToSO);
            this.groupBox1.Controls.Add(this.dtpFromSO);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Location = new System.Drawing.Point(23, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(405, 205);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filter";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(220, 158);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 30);
            this.button1.TabIndex = 57;
            this.button1.Text = "Clear";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnLoadUASO
            // 
            this.btnLoadUASO.Location = new System.Drawing.Point(109, 158);
            this.btnLoadUASO.Name = "btnLoadUASO";
            this.btnLoadUASO.Size = new System.Drawing.Size(69, 31);
            this.btnLoadUASO.TabIndex = 56;
            this.btnLoadUASO.Text = "Load";
            this.btnLoadUASO.UseVisualStyleBackColor = true;
            this.btnLoadUASO.Click += new System.EventHandler(this.btnLoadUASO_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(57, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 24);
            this.label3.TabIndex = 49;
            this.label3.Text = "To";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(57, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 24);
            this.label4.TabIndex = 48;
            this.label4.Text = "From";
            // 
            // dtpToSO
            // 
            this.dtpToSO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpToSO.CustomFormat = "dd-MMM-yy";
            this.dtpToSO.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToSO.Location = new System.Drawing.Point(118, 125);
            this.dtpToSO.Name = "dtpToSO";
            this.dtpToSO.Size = new System.Drawing.Size(226, 29);
            this.dtpToSO.TabIndex = 55;
            // 
            // dtpFromSO
            // 
            this.dtpFromSO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFromSO.CustomFormat = "dd-MMM-yy";
            this.dtpFromSO.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromSO.Location = new System.Drawing.Point(118, 92);
            this.dtpFromSO.Name = "dtpFromSO";
            this.dtpFromSO.Size = new System.Drawing.Size(226, 29);
            this.dtpFromSO.TabIndex = 54;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(80, 51);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(112, 28);
            this.checkBox1.TabIndex = 52;
            this.checkBox1.Text = "Approved";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(200, 51);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(104, 28);
            this.checkBox2.TabIndex = 53;
            this.checkBox2.Text = "Rejected";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(200, 17);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(49, 28);
            this.radioButton1.TabIndex = 51;
            this.radioButton1.Text = "All";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(80, 17);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(70, 28);
            this.radioButton2.TabIndex = 50;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Mine";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // frmAprrovals
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1312, 598);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmAprrovals";
            this.Text = "frmAprrovals";
            this.tabControl1.ResumeLayout(false);
            this.tcQuotes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgApproveQuotes)).EndInit();
            this.Filters.ResumeLayout(false);
            this.Filters.PerformLayout();
            this.btLoadUASO.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgUASalesOrders)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tcQuotes;
        private System.Windows.Forms.TabPage btLoadUASO;
        private System.Windows.Forms.GroupBox Filters;
        private System.Windows.Forms.Button btnQuotesClear;
        private System.Windows.Forms.Button btnLoadUAQuotes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpSOTo;
        private System.Windows.Forms.DateTimePicker dtpSOFrom;
        private System.Windows.Forms.CheckBox chkSOApproved;
        private System.Windows.Forms.CheckBox chkSORejected;
        private System.Windows.Forms.RadioButton rdBtnSOAll;
        private System.Windows.Forms.RadioButton rdBtnSOMine;
        private System.Windows.Forms.DataGridView dgApproveQuotes;
        private System.Windows.Forms.DataGridView dgUASalesOrders;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnLoadUASO;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpToSO;
        private System.Windows.Forms.DateTimePicker dtpFromSO;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
    }
}