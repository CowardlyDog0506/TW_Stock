
namespace TW_Stock
{
    partial class Form3
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.D_Yield = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PB_Ratio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.D_Year = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PE_Ratio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Financial_Year = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.date,
            this.D_Yield,
            this.PB_Ratio,
            this.D_Year,
            this.PE_Ratio,
            this.Financial_Year});
            this.dataGridView1.Location = new System.Drawing.Point(87, 51);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(643, 331);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // date
            // 
            this.date.HeaderText = "日期";
            this.date.Name = "date";
            // 
            // D_Yield
            // 
            this.D_Yield.HeaderText = "殖利率";
            this.D_Yield.Name = "D_Yield";
            // 
            // PB_Ratio
            // 
            this.PB_Ratio.HeaderText = "股價淨值比";
            this.PB_Ratio.Name = "PB_Ratio";
            // 
            // D_Year
            // 
            this.D_Year.HeaderText = "股利年度";
            this.D_Year.Name = "D_Year";
            // 
            // PE_Ratio
            // 
            this.PE_Ratio.HeaderText = "本益比";
            this.PE_Ratio.Name = "PE_Ratio";
            // 
            // Financial_Year
            // 
            this.Financial_Year.HeaderText = "財報年/季";
            this.Financial_Year.Name = "Financial_Year";
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form3";
            this.Text = "Form3";
            this.Load += new System.EventHandler(this.Form3_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewTextBoxColumn D_Yield;
        private System.Windows.Forms.DataGridViewTextBoxColumn PB_Ratio;
        private System.Windows.Forms.DataGridViewTextBoxColumn D_Year;
        private System.Windows.Forms.DataGridViewTextBoxColumn PE_Ratio;
        private System.Windows.Forms.DataGridViewTextBoxColumn Financial_Year;
    }
}