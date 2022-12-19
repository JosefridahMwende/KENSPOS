namespace KENSPOS.FORMS
{
    partial class ReportViewer
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
            this.CRPT = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // CRPT
            // 
            this.CRPT.ActiveViewIndex = -1;
            this.CRPT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CRPT.Cursor = System.Windows.Forms.Cursors.Default;
            this.CRPT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CRPT.Location = new System.Drawing.Point(0, 0);
            this.CRPT.Name = "CRPT";
            this.CRPT.Size = new System.Drawing.Size(800, 450);
            this.CRPT.TabIndex = 0;
            // 
            // ReportViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.CRPT);
            this.Name = "ReportViewer";
            this.Text = "Report Viewer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        public CrystalDecisions.Windows.Forms.CrystalReportViewer CRPT;
    }
}