using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System.Drawing;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using System.Text.RegularExpressions;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.Web.WebView2.WinForms;
using Microsoft.Web.WebView2.Core;

namespace TW_Stock
{
    public partial class Form2 : Form
    {
        private WebView2 webView;
        String company = "";
        Form1 form1;
        CompanyPrices companymsg = new CompanyPrices();
        private Chart chart1 = new Chart();
        public Form2(String company, CompanyPrices companymsg, Form1 form1)
        {
            InitializeComponent();
            InitializeWebView();
            this.company = company;
            this.form1 = form1;
        }

        private void Form2_Load(object sender, EventArgs e)
        {


            try
            {
                WebClient client = new WebClient();
                client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36");

                // 设置 Referer 头信息
                client.Headers.Add("Referer", "https://www.example.com");
                // 指定要获取的网页的 URL
                string url = String.Format("https://goodinfo.tw/tw/StockDividendSchedule.asp?STOCK_ID={0}", company);

                // 使用 WebClient 的 DownloadString 方法下载网页的 HTML 内容
                string htmlContent = client.DownloadString(url);
                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(htmlContent);
                string divId = "divDetail";
                //MessageBox.Show(htmlContent);
                // 使用 XPath 选择特定的 div 元素
                HtmlNodeCollection divNodes = htmlDoc.DocumentNode.SelectNodes($"//div[@id='{divId}']");

                // 添加長條圖的資料
                chart1.Location = new System.Drawing.Point(400, 200);
                chart1.ChartAreas.Add(new ChartArea());
                chart1.Series.Clear();
                //this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
                chart1.Size = new Size(400, 30);
                //chart1.Series.Clear();
                chart1.Series.Add("s1");
                chart1.Series.Add("s2");
                chart1.Series["s1"].ChartType = SeriesChartType.StackedBar100;
                chart1.Series["s2"].ChartType = SeriesChartType.StackedBar100;
                // 添加两个数据点，一个代表百分比值，另一个代表剩余值
                chart1.Series["s1"].Points.AddXY("A", 40);
                chart1.Series["s2"].Points.AddXY("A", 60);
                // 设置第一个数据点的颜色为绿色，第二个数据点的颜色为红色
                chart1.Series["s1"].Points[0].Color = Color.Green;
                chart1.Series["s2"].Points[0].Color = Color.Red;
                //chart1.Series["Data"].Points[1].Color = Color.Red;
                chart1.ChartAreas[0].AxisY.Minimum = 0;
                chart1.ChartAreas[0].AxisY.Maximum = 100;

                chart1.Enabled = false;
                
                // 隐藏 X 轴的刻度线和标签
                chart1.ChartAreas[0].AxisX.MajorTickMark.Enabled = false;
                chart1.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
                chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                chart1.ChartAreas[0].AxisX.Enabled = AxisEnabled.False;
                chart1.ChartAreas[0].AxisX.IsMarginVisible = false;
                // 隐藏 Y 轴的刻度线和标签
                chart1.ChartAreas[0].AxisY.MajorTickMark.Enabled = false;
                chart1.ChartAreas[0].AxisY.LabelStyle.Enabled = false;
                chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
                chart1.ChartAreas[0].AxisY.Enabled = AxisEnabled.False;
                chart1.ChartAreas[0].AxisY.IsMarginVisible = false;
                chart1.ChartAreas[0].Position.Auto = false;
                
                chart1.ChartAreas[0].Position.Width = 100;
                chart1.ChartAreas[0].Position.Height = 100;
                chart1.ChartAreas[0].Position.X = 0;
                chart1.ChartAreas[0].Position.Y = 0;
                Controls.Add(this.chart1);

                // 加载网页
                webView.Source = new Uri("https://goodinfo.tw/tw/StockDetail.asp?STOCK_ID=4707");
            }

            catch (Exception ex)
            {
                // 处理异常情况
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            
        }
        public Form1 Get_form1()
        {
            return form1;
        }
        void f_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4(company, this);
            f.FormClosing += new FormClosingEventHandler(f_FormClosing);
            f.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3(company, this);
            f.FormClosing += new FormClosingEventHandler(f_FormClosing);
            f.Show();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        public void companyMsg_Set(CompanyPrices companymsg)
        {
            this.companymsg = companymsg;
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
        private void InitializeWebView()
        {
            webView = new WebView2();
            webView.Dock = DockStyle.Fill;

            // 将 WebView2 控件添加到窗体中
            Controls.Add(webView);

            // 注册 WebView2 控件的事件
            webView.NavigationCompleted += WebView_NavigationCompleted;

            // 初始化 WebView2 控件
            webView.EnsureCoreWebView2Async(null);
            panel1.Controls.Add(webView);
        }
        private void WebView_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            // 网页导航完成后的处理逻辑
        }
    }
}
