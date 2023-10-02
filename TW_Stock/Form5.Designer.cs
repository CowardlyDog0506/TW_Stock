
namespace TW_Stock
{
    partial class Form5
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
            this.Year = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Season = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Op_Income = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Op_Cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Gross = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Gross_Margin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Last_Season_Gross_Margin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Last_Year_Gross_Margin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Op = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Op_Margin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Last_Season_Op_Margin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Last_Year_Op_Margin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EBT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EBT_Margin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Last_Season_EBT_Margin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Last_Year_EBT_Margin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Net_Income = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Net_Income_Margin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Last_Season_Net_Income_Margin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Last_Year_Net_Income_Margin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.month = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.month_report = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comparison_last_month1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comparison_last_month2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comparison_last_month3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comparison_last_month4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comparison_last_month5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comparison_last_year1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comparison_last_year2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comparison_last_year3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comparison_last_year4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comparison_last_year5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cumulative_comparison1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cumulative_comparison2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cumulative_comparison3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cumulative_comparison4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cumulative_comparison5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Year,
            this.Season,
            this.Code,
            this.name,
            this.Op_Income,
            this.Op_Cost,
            this.Gross,
            this.Gross_Margin,
            this.Last_Season_Gross_Margin,
            this.Last_Year_Gross_Margin,
            this.Op,
            this.Op_Margin,
            this.Last_Season_Op_Margin,
            this.Last_Year_Op_Margin,
            this.EBT,
            this.EBT_Margin,
            this.Last_Season_EBT_Margin,
            this.Last_Year_EBT_Margin,
            this.Net_Income,
            this.Net_Income_Margin,
            this.Last_Season_Net_Income_Margin,
            this.Last_Year_Net_Income_Margin,
            this.month,
            this.month_report,
            this.comparison_last_month1,
            this.comparison_last_month2,
            this.comparison_last_month3,
            this.comparison_last_month4,
            this.comparison_last_month5,
            this.comparison_last_year1,
            this.comparison_last_year2,
            this.comparison_last_year3,
            this.comparison_last_year4,
            this.comparison_last_year5,
            this.cumulative_comparison1,
            this.cumulative_comparison2,
            this.cumulative_comparison3,
            this.cumulative_comparison4,
            this.cumulative_comparison5,
            this.remark});
            this.dataGridView1.Location = new System.Drawing.Point(25, 112);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(741, 326);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Year
            // 
            this.Year.HeaderText = "年度";
            this.Year.Name = "Year";
            this.Year.ReadOnly = true;
            // 
            // Season
            // 
            this.Season.HeaderText = "季別";
            this.Season.Name = "Season";
            this.Season.ReadOnly = true;
            // 
            // Code
            // 
            this.Code.HeaderText = "公司代號";
            this.Code.Name = "Code";
            this.Code.ReadOnly = true;
            // 
            // name
            // 
            this.name.HeaderText = "公司名稱";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // Op_Income
            // 
            this.Op_Income.HeaderText = "本季營業收入";
            this.Op_Income.Name = "Op_Income";
            this.Op_Income.ReadOnly = true;
            // 
            // Op_Cost
            // 
            this.Op_Cost.HeaderText = "營業成本";
            this.Op_Cost.Name = "Op_Cost";
            this.Op_Cost.ReadOnly = true;
            // 
            // Gross
            // 
            this.Gross.HeaderText = "毛利";
            this.Gross.Name = "Gross";
            this.Gross.ReadOnly = true;
            // 
            // Gross_Margin
            // 
            this.Gross_Margin.HeaderText = "毛利率(%)";
            this.Gross_Margin.Name = "Gross_Margin";
            this.Gross_Margin.ReadOnly = true;
            // 
            // Last_Season_Gross_Margin
            // 
            this.Last_Season_Gross_Margin.HeaderText = "上季毛利成長";
            this.Last_Season_Gross_Margin.Name = "Last_Season_Gross_Margin";
            this.Last_Season_Gross_Margin.ReadOnly = true;
            // 
            // Last_Year_Gross_Margin
            // 
            this.Last_Year_Gross_Margin.HeaderText = "去年同期毛利";
            this.Last_Year_Gross_Margin.Name = "Last_Year_Gross_Margin";
            this.Last_Year_Gross_Margin.ReadOnly = true;
            // 
            // Op
            // 
            this.Op.HeaderText = "營業利益";
            this.Op.Name = "Op";
            this.Op.ReadOnly = true;
            // 
            // Op_Margin
            // 
            this.Op_Margin.HeaderText = "營業利益率(%)";
            this.Op_Margin.Name = "Op_Margin";
            this.Op_Margin.ReadOnly = true;
            // 
            // Last_Season_Op_Margin
            // 
            this.Last_Season_Op_Margin.HeaderText = "上季營益成長";
            this.Last_Season_Op_Margin.Name = "Last_Season_Op_Margin";
            this.Last_Season_Op_Margin.ReadOnly = true;
            // 
            // Last_Year_Op_Margin
            // 
            this.Last_Year_Op_Margin.HeaderText = "去年營益成長";
            this.Last_Year_Op_Margin.Name = "Last_Year_Op_Margin";
            this.Last_Year_Op_Margin.ReadOnly = true;
            // 
            // EBT
            // 
            this.EBT.HeaderText = "稅前淨利";
            this.EBT.Name = "EBT";
            this.EBT.ReadOnly = true;
            // 
            // EBT_Margin
            // 
            this.EBT_Margin.HeaderText = "稅前純益率(%)";
            this.EBT_Margin.Name = "EBT_Margin";
            this.EBT_Margin.ReadOnly = true;
            // 
            // Last_Season_EBT_Margin
            // 
            this.Last_Season_EBT_Margin.HeaderText = "上季稅前純益";
            this.Last_Season_EBT_Margin.Name = "Last_Season_EBT_Margin";
            this.Last_Season_EBT_Margin.ReadOnly = true;
            // 
            // Last_Year_EBT_Margin
            // 
            this.Last_Year_EBT_Margin.HeaderText = "去年同期稅前純益";
            this.Last_Year_EBT_Margin.Name = "Last_Year_EBT_Margin";
            this.Last_Year_EBT_Margin.ReadOnly = true;
            // 
            // Net_Income
            // 
            this.Net_Income.HeaderText = "稅後淨利";
            this.Net_Income.Name = "Net_Income";
            this.Net_Income.ReadOnly = true;
            // 
            // Net_Income_Margin
            // 
            this.Net_Income_Margin.HeaderText = "稅後純益率(%)";
            this.Net_Income_Margin.Name = "Net_Income_Margin";
            this.Net_Income_Margin.ReadOnly = true;
            // 
            // Last_Season_Net_Income_Margin
            // 
            this.Last_Season_Net_Income_Margin.HeaderText = "上季稅後純益";
            this.Last_Season_Net_Income_Margin.Name = "Last_Season_Net_Income_Margin";
            this.Last_Season_Net_Income_Margin.ReadOnly = true;
            // 
            // Last_Year_Net_Income_Margin
            // 
            this.Last_Year_Net_Income_Margin.HeaderText = "去年同期稅後純益";
            this.Last_Year_Net_Income_Margin.Name = "Last_Year_Net_Income_Margin";
            this.Last_Year_Net_Income_Margin.ReadOnly = true;
            // 
            // month
            // 
            this.month.HeaderText = "月";
            this.month.Name = "month";
            this.month.ReadOnly = true;
            // 
            // month_report
            // 
            this.month_report.HeaderText = "本月營收";
            this.month_report.Name = "month_report";
            this.month_report.ReadOnly = true;
            // 
            // comparison_last_month1
            // 
            this.comparison_last_month1.HeaderText = "上月比較增減(%)";
            this.comparison_last_month1.Name = "comparison_last_month1";
            this.comparison_last_month1.ReadOnly = true;
            // 
            // comparison_last_month2
            // 
            this.comparison_last_month2.HeaderText = "上上月比";
            this.comparison_last_month2.Name = "comparison_last_month2";
            this.comparison_last_month2.ReadOnly = true;
            // 
            // comparison_last_month3
            // 
            this.comparison_last_month3.HeaderText = "上上上月比";
            this.comparison_last_month3.Name = "comparison_last_month3";
            this.comparison_last_month3.ReadOnly = true;
            // 
            // comparison_last_month4
            // 
            this.comparison_last_month4.HeaderText = "上上上上月比";
            this.comparison_last_month4.Name = "comparison_last_month4";
            this.comparison_last_month4.ReadOnly = true;
            // 
            // comparison_last_month5
            // 
            this.comparison_last_month5.HeaderText = "上上上上上月比";
            this.comparison_last_month5.Name = "comparison_last_month5";
            this.comparison_last_month5.ReadOnly = true;
            // 
            // comparison_last_year1
            // 
            this.comparison_last_year1.HeaderText = "去年比";
            this.comparison_last_year1.Name = "comparison_last_year1";
            this.comparison_last_year1.ReadOnly = true;
            // 
            // comparison_last_year2
            // 
            this.comparison_last_year2.HeaderText = "去去年比";
            this.comparison_last_year2.Name = "comparison_last_year2";
            this.comparison_last_year2.ReadOnly = true;
            // 
            // comparison_last_year3
            // 
            this.comparison_last_year3.HeaderText = "去去去年比";
            this.comparison_last_year3.Name = "comparison_last_year3";
            this.comparison_last_year3.ReadOnly = true;
            // 
            // comparison_last_year4
            // 
            this.comparison_last_year4.HeaderText = "去去去去年比";
            this.comparison_last_year4.Name = "comparison_last_year4";
            this.comparison_last_year4.ReadOnly = true;
            // 
            // comparison_last_year5
            // 
            this.comparison_last_year5.HeaderText = "去去去去去年比";
            this.comparison_last_year5.Name = "comparison_last_year5";
            this.comparison_last_year5.ReadOnly = true;
            // 
            // cumulative_comparison1
            // 
            this.cumulative_comparison1.HeaderText = "前期增減";
            this.cumulative_comparison1.Name = "cumulative_comparison1";
            this.cumulative_comparison1.ReadOnly = true;
            // 
            // cumulative_comparison2
            // 
            this.cumulative_comparison2.HeaderText = "前前期增減";
            this.cumulative_comparison2.Name = "cumulative_comparison2";
            this.cumulative_comparison2.ReadOnly = true;
            // 
            // cumulative_comparison3
            // 
            this.cumulative_comparison3.HeaderText = "前前前期增減";
            this.cumulative_comparison3.Name = "cumulative_comparison3";
            this.cumulative_comparison3.ReadOnly = true;
            // 
            // cumulative_comparison4
            // 
            this.cumulative_comparison4.HeaderText = "前前前前期增減";
            this.cumulative_comparison4.Name = "cumulative_comparison4";
            this.cumulative_comparison4.ReadOnly = true;
            // 
            // cumulative_comparison5
            // 
            this.cumulative_comparison5.HeaderText = "前前前前前期增減";
            this.cumulative_comparison5.Name = "cumulative_comparison5";
            this.cumulative_comparison5.ReadOnly = true;
            // 
            // remark
            // 
            this.remark.HeaderText = "備註";
            this.remark.Name = "remark";
            this.remark.ReadOnly = true;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "營業收入(百萬元)>1000",
            "毛利率(%)>15",
            "營業利益率(%)>15",
            "稅前純益率(%)>15",
            "稅後純益率(%)>15",
            "全為正",
            "三率三升(上季)",
            "三率三升(去年)",
            "近五月月營收為正成長",
            "近五月去年比為正成長",
            "近五月累計收入為正成長",
            "近3月為正"});
            this.checkedListBox1.Location = new System.Drawing.Point(25, 12);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(176, 94);
            this.checkedListBox1.TabIndex = 1;
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(254, 38);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "篩選";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(390, 38);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "加入群組";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form5";
            this.Text = "Form5";
            this.Load += new System.EventHandler(this.Form5_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Year;
        private System.Windows.Forms.DataGridViewTextBoxColumn Season;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Op_Income;
        private System.Windows.Forms.DataGridViewTextBoxColumn Op_Cost;
        private System.Windows.Forms.DataGridViewTextBoxColumn Gross;
        private System.Windows.Forms.DataGridViewTextBoxColumn Gross_Margin;
        private System.Windows.Forms.DataGridViewTextBoxColumn Last_Season_Gross_Margin;
        private System.Windows.Forms.DataGridViewTextBoxColumn Last_Year_Gross_Margin;
        private System.Windows.Forms.DataGridViewTextBoxColumn Op;
        private System.Windows.Forms.DataGridViewTextBoxColumn Op_Margin;
        private System.Windows.Forms.DataGridViewTextBoxColumn Last_Season_Op_Margin;
        private System.Windows.Forms.DataGridViewTextBoxColumn Last_Year_Op_Margin;
        private System.Windows.Forms.DataGridViewTextBoxColumn EBT;
        private System.Windows.Forms.DataGridViewTextBoxColumn EBT_Margin;
        private System.Windows.Forms.DataGridViewTextBoxColumn Last_Season_EBT_Margin;
        private System.Windows.Forms.DataGridViewTextBoxColumn Last_Year_EBT_Margin;
        private System.Windows.Forms.DataGridViewTextBoxColumn Net_Income;
        private System.Windows.Forms.DataGridViewTextBoxColumn Net_Income_Margin;
        private System.Windows.Forms.DataGridViewTextBoxColumn Last_Season_Net_Income_Margin;
        private System.Windows.Forms.DataGridViewTextBoxColumn Last_Year_Net_Income_Margin;
        private System.Windows.Forms.DataGridViewTextBoxColumn month;
        private System.Windows.Forms.DataGridViewTextBoxColumn month_report;
        private System.Windows.Forms.DataGridViewTextBoxColumn comparison_last_month1;
        private System.Windows.Forms.DataGridViewTextBoxColumn comparison_last_month2;
        private System.Windows.Forms.DataGridViewTextBoxColumn comparison_last_month3;
        private System.Windows.Forms.DataGridViewTextBoxColumn comparison_last_month4;
        private System.Windows.Forms.DataGridViewTextBoxColumn comparison_last_month5;
        private System.Windows.Forms.DataGridViewTextBoxColumn comparison_last_year1;
        private System.Windows.Forms.DataGridViewTextBoxColumn comparison_last_year2;
        private System.Windows.Forms.DataGridViewTextBoxColumn comparison_last_year3;
        private System.Windows.Forms.DataGridViewTextBoxColumn comparison_last_year4;
        private System.Windows.Forms.DataGridViewTextBoxColumn comparison_last_year5;
        private System.Windows.Forms.DataGridViewTextBoxColumn cumulative_comparison1;
        private System.Windows.Forms.DataGridViewTextBoxColumn cumulative_comparison2;
        private System.Windows.Forms.DataGridViewTextBoxColumn cumulative_comparison3;
        private System.Windows.Forms.DataGridViewTextBoxColumn cumulative_comparison4;
        private System.Windows.Forms.DataGridViewTextBoxColumn cumulative_comparison5;
        private System.Windows.Forms.DataGridViewTextBoxColumn remark;
    }
}