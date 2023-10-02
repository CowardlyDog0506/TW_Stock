using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using HtmlAgilityPack;
using Newtonsoft.Json;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using System.Text.RegularExpressions;
using System.Net.Http;

namespace TW_Stock
{
    public partial class Form4 : Form
    {
        Form2 form2 = null;
        String company = "";
        public Form4(String company, Form2 form2)
        {
            this.company = company;
            this.form2 = form2;
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private async void Form4_Load(object sender, EventArgs e)
        {

          
            try
            {
                string url = String.Format("https://goodinfo.tw/tw/StockDividendSchedule.asp?STOCK_ID={0}", company);
                WebClient client = new WebClient();
                client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36");

                // 设置 Referer 头信息
                client.Headers.Add("Referer", "https://www.example.com");
                // 指定要获取的网页的 URL
                

                // 使用 WebClient 的 DownloadString 方法下载网页的 HTML 内容
                string htmlContent = client.DownloadString(url);
                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(htmlContent);
                string divId = "divDetail";
                HtmlNode divNode = htmlDoc.GetElementbyId(divId);
                string htmlTable = divNode.InnerHtml; ; // Replace with your HTML table string

                // Load the HTML table string into an HtmlDocument
                htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(htmlTable);

                // Find the table element
                HtmlNode tableNode = htmlDoc.DocumentNode.SelectSingleNode("//table");

                // Check if the table was found
                if (tableNode != null)
                {
                    // Get all rows in the table
                    HtmlNodeCollection rowNodes = tableNode.SelectNodes(".//tr");

                    // Define a list to store the rows as string arrays
                    List<string[]> tableData = new List<string[]>();

                    // Iterate over the rows
                    foreach (HtmlNode rowNode in rowNodes)
                    {
                        // Get all cells in the row
                        HtmlNodeCollection cellNodes = rowNode.SelectNodes(".//td");

                        // Check if the row contains cells
                        if (cellNodes != null)
                        {
                            // Create a string array to store the cell values
                            string[] rowData = new string[cellNodes.Count];

                            // Iterate over the cells
                            for (int i = 0; i < cellNodes.Count; i++)
                            {
                                // Extract the text content of the cell
                                string cellValue = cellNodes[i].InnerText.Trim();

                                // Store the cell value in the array
                                rowData[i] = cellValue;
                                
                            }

                            // Add the row data to the table data list
                            tableData.Add(rowData);

                        }
                    }
                    for(int i = 0; i < 5; i++)
                    {
                        int newRowIdx = dataGridView1.Rows.Add();
                        for (int j = 0; j <19; j++)
                        {
                            dataGridView1.Rows[newRowIdx].Cells[j].Value = tableData[i][j];
                        }
                    }
                    // Convert the table data list to a string array (if needed)
                    string[][] tableArray = tableData.ToArray();

                }
                url  = "google.com";
                htmlContent = client.DownloadString(url);
                MessageBox.Show(htmlContent);
            }
            catch (Exception ex)
            {
                // 处理异常情况
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            
        }
    }
    class ProxyInfo
    {
        public string IP { get; set; }
        public string Port { get; set; }

        public ProxyInfo(string ip, string port)
        {
            IP = ip;
            Port = port;
        }
    }
}
