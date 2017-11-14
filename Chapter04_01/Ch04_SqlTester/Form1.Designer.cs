namespace Ch04_SqlTester
{
    partial class frmSqlTester
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
            this.grdSqlTester = new System.Windows.Forms.DataGridView();
            this.txtSqlTester = new System.Windows.Forms.TextBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblRecords = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdSqlTester)).BeginInit();
            this.SuspendLayout();
            // 
            // grdSqlTester
            // 
            this.grdSqlTester.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdSqlTester.Location = new System.Drawing.Point(12, 12);
            this.grdSqlTester.Name = "grdSqlTester";
            this.grdSqlTester.Size = new System.Drawing.Size(760, 230);
            this.grdSqlTester.TabIndex = 0;
            this.grdSqlTester.TabStop = false;
            // 
            // txtSqlTester
            // 
            this.txtSqlTester.Location = new System.Drawing.Point(12, 249);
            this.txtSqlTester.Multiline = true;
            this.txtSqlTester.Name = "txtSqlTester";
            this.txtSqlTester.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSqlTester.Size = new System.Drawing.Size(575, 55);
            this.txtSqlTester.TabIndex = 1;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(593, 249);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(179, 25);
            this.btnTest.TabIndex = 2;
            this.btnTest.Text = "&Test SQL Statement";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(598, 287);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Records Returned";
            // 
            // lblRecords
            // 
            this.lblRecords.AutoSize = true;
            this.lblRecords.BackColor = System.Drawing.Color.White;
            this.lblRecords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecords.Location = new System.Drawing.Point(698, 282);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.Size = new System.Drawing.Size(20, 22);
            this.lblRecords.TabIndex = 4;
            this.lblRecords.Text = "0";
            this.lblRecords.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmSqlTester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 311);
            this.Controls.Add(this.lblRecords);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.txtSqlTester);
            this.Controls.Add(this.grdSqlTester);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmSqlTester";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SQL Tester";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSqlTester_FormClosing);
            this.Load += new System.EventHandler(this.frmSqlTester_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdSqlTester)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grdSqlTester;
        private System.Windows.Forms.TextBox txtSqlTester;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblRecords;
    }
}

