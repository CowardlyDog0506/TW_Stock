
namespace TW_Stock
{
    partial class Form4
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
            this.Dividend_Season = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Earnings_period = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SH_Meeting_Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ex_Dividend_Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ex_Dividend_Price_Offer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Diviend_Completion_Day = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Diviend_Completion_Day_Spent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CD_Payment_Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EX_Right_Trading_Day = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ex_Right_reference_Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Right_Completion_Day = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Right_Completion_Day_Spent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cash_Dividend_Surplus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cash_Dividend_Capital_Reserve = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cash_Dividend_Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Stock_Dividend_Surplus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Stock_Dividend_Capital_Reserve = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Stock_Dividend_Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dividend_Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Dividend_Season,
            this.Earnings_period,
            this.SH_Meeting_Date,
            this.Ex_Dividend_Date,
            this.Ex_Dividend_Price_Offer,
            this.Diviend_Completion_Day,
            this.Diviend_Completion_Day_Spent,
            this.CD_Payment_Date,
            this.EX_Right_Trading_Day,
            this.Ex_Right_reference_Price,
            this.Right_Completion_Day,
            this.Right_Completion_Day_Spent,
            this.Cash_Dividend_Surplus,
            this.Cash_Dividend_Capital_Reserve,
            this.Cash_Dividend_Total,
            this.Stock_Dividend_Surplus,
            this.Stock_Dividend_Capital_Reserve,
            this.Stock_Dividend_Total,
            this.Dividend_Total});
            this.dataGridView1.Location = new System.Drawing.Point(81, 53);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(666, 332);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Dividend_Season
            // 
            this.Dividend_Season.HeaderText = "股利發放年度";
            this.Dividend_Season.Name = "Dividend_Season";
            // 
            // Earnings_period
            // 
            this.Earnings_period.HeaderText = "股利所屬盈餘期間";
            this.Earnings_period.Name = "Earnings_period";
            // 
            // SH_Meeting_Date
            // 
            this.SH_Meeting_Date.HeaderText = "股東會日期";
            this.SH_Meeting_Date.Name = "SH_Meeting_Date";
            // 
            // Ex_Dividend_Date
            // 
            this.Ex_Dividend_Date.HeaderText = "除息交易日";
            this.Ex_Dividend_Date.Name = "Ex_Dividend_Date";
            // 
            // Ex_Dividend_Price_Offer
            // 
            this.Ex_Dividend_Price_Offer.HeaderText = "除息參考價";
            this.Ex_Dividend_Price_Offer.Name = "Ex_Dividend_Price_Offer";
            // 
            // Diviend_Completion_Day
            // 
            this.Diviend_Completion_Day.HeaderText = "填息完成日";
            this.Diviend_Completion_Day.Name = "Diviend_Completion_Day";
            // 
            // Diviend_Completion_Day_Spent
            // 
            this.Diviend_Completion_Day_Spent.HeaderText = "花費天數";
            this.Diviend_Completion_Day_Spent.Name = "Diviend_Completion_Day_Spent";
            // 
            // CD_Payment_Date
            // 
            this.CD_Payment_Date.HeaderText = "現金股利發放日";
            this.CD_Payment_Date.Name = "CD_Payment_Date";
            // 
            // EX_Right_Trading_Day
            // 
            this.EX_Right_Trading_Day.HeaderText = "除權交易日";
            this.EX_Right_Trading_Day.Name = "EX_Right_Trading_Day";
            // 
            // Ex_Right_reference_Price
            // 
            this.Ex_Right_reference_Price.HeaderText = "除權參考價";
            this.Ex_Right_reference_Price.Name = "Ex_Right_reference_Price";
            // 
            // Right_Completion_Day
            // 
            this.Right_Completion_Day.HeaderText = "填權完成日";
            this.Right_Completion_Day.Name = "Right_Completion_Day";
            // 
            // Right_Completion_Day_Spent
            // 
            this.Right_Completion_Day_Spent.HeaderText = "填權花費日數";
            this.Right_Completion_Day_Spent.Name = "Right_Completion_Day_Spent";
            // 
            // Cash_Dividend_Surplus
            // 
            this.Cash_Dividend_Surplus.HeaderText = "現金股利(盈餘)";
            this.Cash_Dividend_Surplus.Name = "Cash_Dividend_Surplus";
            // 
            // Cash_Dividend_Capital_Reserve
            // 
            this.Cash_Dividend_Capital_Reserve.HeaderText = "現金股利(公積)";
            this.Cash_Dividend_Capital_Reserve.Name = "Cash_Dividend_Capital_Reserve";
            // 
            // Cash_Dividend_Total
            // 
            this.Cash_Dividend_Total.HeaderText = "現金股利(合計)";
            this.Cash_Dividend_Total.Name = "Cash_Dividend_Total";
            // 
            // Stock_Dividend_Surplus
            // 
            this.Stock_Dividend_Surplus.HeaderText = "股票股利(盈餘)";
            this.Stock_Dividend_Surplus.Name = "Stock_Dividend_Surplus";
            // 
            // Stock_Dividend_Capital_Reserve
            // 
            this.Stock_Dividend_Capital_Reserve.HeaderText = "股票股利(公積)";
            this.Stock_Dividend_Capital_Reserve.Name = "Stock_Dividend_Capital_Reserve";
            // 
            // Stock_Dividend_Total
            // 
            this.Stock_Dividend_Total.HeaderText = "股票股利(合計)";
            this.Stock_Dividend_Total.Name = "Stock_Dividend_Total";
            // 
            // Dividend_Total
            // 
            this.Dividend_Total.HeaderText = "股利合計";
            this.Dividend_Total.Name = "Dividend_Total";
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form4";
            this.Text = "Form4";
            this.Load += new System.EventHandler(this.Form4_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dividend_Season;
        private System.Windows.Forms.DataGridViewTextBoxColumn Earnings_period;
        private System.Windows.Forms.DataGridViewTextBoxColumn SH_Meeting_Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ex_Dividend_Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ex_Dividend_Price_Offer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Diviend_Completion_Day;
        private System.Windows.Forms.DataGridViewTextBoxColumn Diviend_Completion_Day_Spent;
        private System.Windows.Forms.DataGridViewTextBoxColumn CD_Payment_Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn EX_Right_Trading_Day;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ex_Right_reference_Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn Right_Completion_Day;
        private System.Windows.Forms.DataGridViewTextBoxColumn Right_Completion_Day_Spent;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cash_Dividend_Surplus;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cash_Dividend_Capital_Reserve;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cash_Dividend_Total;
        private System.Windows.Forms.DataGridViewTextBoxColumn Stock_Dividend_Surplus;
        private System.Windows.Forms.DataGridViewTextBoxColumn Stock_Dividend_Capital_Reserve;
        private System.Windows.Forms.DataGridViewTextBoxColumn Stock_Dividend_Total;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dividend_Total;
    }
}