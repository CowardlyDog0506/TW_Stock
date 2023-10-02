using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;
using System.Net.Http;
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration.Attributes;
using System.IO;

namespace TW_Stock
{
    public partial class Form5 : Form
    {
        Form1 form1;
        public Form5(Form1 form1)
        {
            this.form1 = form1;
            InitializeComponent();
        }
        public static string Get(string url)
        {
            var web = new WebClient();
            return web.DownloadString(url);
        }
        List<Revenue> revenueList = new List<Revenue>();
        List<List<Revenue>> nestedList = new List<List<Revenue>>();
        List<List<Revenue>> singleRevenue = new List<List<Revenue>>();
        List<List<MonthReport>> Report_table = new List<List<MonthReport>>();
        int now_year = 0;
        int now_season = 0;
        List<List<string[]>> tableData ;
        int count = 6;
        private async void Form5_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now; // 获取当前日期和时间
            int month = now.Month; // 获取当前月份
            int year = now.Year - 1911; // 获取当前年份
            int season = 4;
            
            while (count != 0)
            {
                //MessageBox.Show(year.ToString() + " + " + season.ToString() + " + " + count.ToString());
                if (season == 0)
                {
                    season = 4;
                    year--;
                }
                if (await get_Financial_Report(year.ToString(), season.ToString()) == 1)
                {
                    now_year = count == 6 ? year : now_year;
                    now_season = count == 6 ? season : now_season;
                    //MessageBox.Show(now_year.ToString() + " + " + now_year.ToString());
                    count--;
                    
                }
                season--;

            }
            await Get_Month_Data((now.Year-1911).ToString(), (month-1).ToString());
            Change2SingleSeason();


        }
        public void Change2SingleSeason()       
        {
            for (int i = nestedList.Count-2; i >= 0; i--){
                List<Revenue> revenueList = new List<Revenue>();
                foreach (var revenues in nestedList[i])
                {
                    Revenue revenue = new Revenue();
                    revenue.comparison_last_month = new List<string>();
                    revenue.comparison_last_year = new List<string>();
                    revenue.cumulative_comparison = new List<string>();
                    foreach (var target in nestedList[i+1])
                    {
                        
                        if (revenues.Code == target.Code)
                        {
                            
                            revenue.Year = revenues.Year;
                            revenue.Season = revenues.Season;
                            revenue.Code = revenues.Code;
                            revenue.Name = revenues.Name;
                            revenue.Op_Income = Minus(revenues.Op_Income, target.Op_Income);
                            revenue.Op_Cost = Minus(revenues.Op_Cost, target.Op_Cost);
                            revenue.Gross = Minus(revenues.Gross, target.Gross);
                            revenue.Gross_Margin = Divide(revenue.Gross, revenue.Op_Income);
                            revenue.Op = Minus(revenues.Op, target.Op);
                            revenue.Op_Margin = Divide(revenue.Op, revenue.Op_Income);
                            revenue.EBT = Minus(revenues.EBT, target.EBT);
                            revenue.EBT_Margin = Divide(revenue.EBT, revenue.Op_Income);
                            revenue.Net_Income = Minus(revenues.Net_Income, target.Net_Income);
                            revenue.Net_Income_Margin = Divide(revenue.Net_Income, revenue.Op_Income);
                            
                        }
                    }
                    foreach (var reports in Report_table)
                    {
                        foreach(var report in reports)
                        {
                            int j = 0;
                            if (revenues.Code == report.Code && j == 0)
                            {

                                revenue.month = report.month;
                                revenue.month_report = report.month_report;
                                revenue.comparison_last_month.Add(report.comparison_last_month);
                                revenue.comparison_last_year.Add(report.comparison_last_year);
                                revenue.cumulative_comparison.Add(report.cumulative_comparison);
                                revenue.remark = report.remark;
                                j++;
                            }else if (revenues.Code == report.Code && j != 0)
                            {
                                revenue.month_report = report.month_report;
                                revenue.comparison_last_month.Add(report.comparison_last_month);
                                revenue.comparison_last_year.Add(report.comparison_last_year);
                                revenue.cumulative_comparison.Add(report.cumulative_comparison);
                                j++;
                            }
                        }
                            
                        
                    }
                    revenueList.Add(revenue);
                }
                singleRevenue.Add(revenueList);
            }
            Show_data();
        }
        public async Task Get_Month_Data(String year, String month)
        {
            int month_count = int.Parse(month);
            int year_count = int.Parse(year);
            for(int i = 0; i<5; i++)
            {
                if (month_count == 0)
                {
                    month_count = 12;
                    year_count--;
                }

                string month_sii_url = String.Format("https://mops.twse.com.tw/server-java/FileDownLoad?step=9&functionName=show_file2&filePath=%2Ft21%2Fsii%2F&fileName=t21sc03_{0}_{1}.csv", year_count, month_count);
                string sii_path = String.Format("path/csv/oii{0}_{1}.csv", year_count, month_count);
                await DownloadCsvFile(month_sii_url, sii_path);
                await Task.Delay(TimeSpan.FromSeconds(10));
                string month_otc_url = String.Format("https://mops.twse.com.tw/server-java/FileDownLoad?step=9&functionName=show_file2&filePath=%2Ft21%2Fotc%2F&fileName=t21sc03_{0}_{1}.csv", year_count, month_count);
                string otc_path = String.Format("path/csv/otc{0}_{1}.csv", year_count, month_count);
                await DownloadCsvFile(month_otc_url, otc_path);
                await Task.Delay(TimeSpan.FromSeconds(10));
                string total_path = String.Format("path/csv/total{0}_{1}.csv", year_count, month_count);

                // MergeCsvFiles(sii_path, otc_path, total_path);
                var reports = ReadCsvFile(sii_path).Select(report =>
                {
                    report.year = int.Parse(year);
                    report.month = int.Parse(month);
                    return report;
                });
                var Reports2 = ReadCsvFile(otc_path).Select(report =>
                {
                    report.year = int.Parse(year);
                    report.month = int.Parse(month);
                    return report;
                });

                Report_table.Add(reports.Concat(Reports2).ToList());
                month_count--;
            }
            
        }
        public void MergeCsvFiles(string inputFilePath1, string inputFilePath2, string outputFilePath)
        {
            // 读取第一个CSV文件
            var records1 = ReadCsvFile(inputFilePath1);

            // 读取第二个CSV文件
            var records2 = ReadCsvFile(inputFilePath2);

            // 合并两个CSV文件的数据
            var mergedRecords = records1.Concat(records2);
            // 写入合并后的数据到输出CSV文件
            WriteCsvFile(outputFilePath, mergedRecords);

        }

        public void WriteCsvFile(string filePath, IEnumerable<MonthReport> records)
        {
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<MonthReportMap>();
                csv.WriteRecords(records);
            }
        }
        private static async Task DownloadCsvFile(string url, string savePath)
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    response.EnsureSuccessStatusCode();

                    using (Stream stream = await response.Content.ReadAsStreamAsync())
                    using (FileStream fileStream = File.Create(savePath))
                    {
                        await stream.CopyToAsync(fileStream);
                    }
                }
            }
        }
        public IEnumerable<MonthReport> ReadCsvFile(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<MonthReportMap>();
                return csv.GetRecords<MonthReport>().ToList();
            }
        }
        public void Show_data()
        {
            /*
            for(int i = 0; i < revenueList.Count(); i++)
            {
                dataGridView1.Rows.Add(revenueList[i].Year, revenueList[i].Season, revenueList[i].Code, revenueList[i].Name, revenueList[i].Op_Income, revenueList[i].Op_Cost, revenueList[i].Gross, revenueList[i].Gross_Margin, revenueList[i].Op, revenueList[i].Op_Margin, revenueList[i].EBT, revenueList[i].EBT_Margin, revenueList[i].Net_Income, revenueList[i].Net_Income_Margin);
            }
            */

            foreach (var revenue in singleRevenue[4])
            {
                foreach (var target in singleRevenue[3])
                {
                    if (revenue.Code == target.Code)
                    {
                        revenue.Last_Season_Gross_Margin = Minus(revenue.Gross_Margin , target.Gross_Margin);
                        revenue.Last_Season_Op_Margin = Minus(revenue.Op_Margin, target.Op_Margin);
                        revenue.Last_Season_EBT_Margin = Minus(revenue.EBT_Margin, target.EBT_Margin);
                        revenue.Last_Season_Net_Income_Margin = Minus(revenue.Net_Income_Margin, target.Net_Income_Margin);
                        break;
                    }
                    else
                    {
                        revenue.Last_Season_Gross_Margin = "0";
                        revenue.Last_Season_Op_Margin = "0";
                        revenue.Last_Season_EBT_Margin = "0";
                        revenue.Last_Season_Net_Income_Margin = "0";
                    }
                }
                foreach (var target in nestedList[0])
                {
                    if (revenue.Code == target.Code)
                    {
                        revenue.Last_Year_Gross_Margin = Minus(revenue.Gross_Margin, target.Gross_Margin);
                        revenue.Last_Year_Op_Margin = Minus(revenue.Op_Margin, target.Op_Margin);
                        revenue.Last_Year_EBT_Margin = Minus(revenue.EBT_Margin, target.EBT_Margin);
                        revenue.Last_Year_Net_Income_Margin = Minus(revenue.Net_Income_Margin, target.Net_Income_Margin);
                        break;
                    }
                    else
                    {
                        revenue.Last_Year_Gross_Margin = "0";
                        revenue.Last_Year_Op_Margin = "0";
                        revenue.Last_Year_EBT_Margin = "0";
                        revenue.Last_Year_Net_Income_Margin = "0";
                    }
                }
                /*
                dataGridView1.Rows.Add(revenue.Year, revenue.Season, revenue.Code, revenue.Name, revenue.Op_Income, revenue.Op_Cost, revenue.Gross, revenue.Gross_Margin, revenue.Last_Season_Gross_Margin, revenue.Last_Year_Gross_Margin, revenue.Op, revenue.Op_Margin, 
                    revenue.Last_Season_Op_Margin, revenue.Last_Year_Op_Margin, revenue.EBT, revenue.EBT_Margin, revenue.Last_Season_EBT_Margin, revenue.Last_Year_EBT_Margin, revenue.Net_Income, revenue.Net_Income_Margin, revenue.Last_Season_Net_Income_Margin, revenue.Last_Year_Net_Income_Margin);
                */
                double[] comparison_last_month = new double[revenue.comparison_last_month.Count];
                double[] comparison_last_year = new double[revenue.comparison_last_year.Count];
                double[] cumulative_comparison = new double[revenue.cumulative_comparison.Count];
                for (int i = 0; i < revenue.comparison_last_month.Count; i++)
                {
                    comparison_last_month[i] = double.TryParse(revenue.comparison_last_month[i], out double temp1) ? Math.Round(temp1, 2) : 0.0;
                    comparison_last_year[i] = double.TryParse(revenue.comparison_last_month[i], out double temp2) ? Math.Round(temp2, 2) : 0.0;
                    cumulative_comparison[i] = double.TryParse(revenue.comparison_last_month[i], out double temp3) ? Math.Round(temp3, 2) : 0.0;
                }
                switch (revenue.comparison_last_month.Count)
                {
                    case 0:
                        {
                            dataGridView1.Rows.Add(
                                revenue.Year,
                                revenue.Season,
                                revenue.Code,
                                revenue.Name,
                                double.Parse(revenue.Op_Income),
                                double.Parse(revenue.Op_Cost),
                                double.Parse(revenue.Gross),
                                double.Parse(revenue.Gross_Margin),
                                double.Parse(revenue.Last_Season_Gross_Margin),
                                double.Parse(revenue.Last_Year_Gross_Margin),
                                double.Parse(revenue.Op),
                                double.Parse(revenue.Op_Margin),
                                double.Parse(revenue.Last_Season_Op_Margin),
                                double.Parse(revenue.Last_Year_Op_Margin),
                                double.Parse(revenue.EBT),
                                double.Parse(revenue.EBT_Margin),
                                double.Parse(revenue.Last_Season_EBT_Margin),
                                double.Parse(revenue.Last_Year_EBT_Margin),
                                double.Parse(revenue.Net_Income),
                                double.Parse(revenue.Net_Income_Margin),
                                double.Parse(revenue.Last_Season_Net_Income_Margin),
                                double.Parse(revenue.Last_Year_Net_Income_Margin),
                                revenue.month,
                                revenue.month_report,
                                comparison_last_month[0],
                                0,
                                0,
                                0,
                                comparison_last_year[0],
                                0,
                                0,
                                0,
                                cumulative_comparison[0],
                                0,
                                0,
                                0,
                                revenue.remark
                            );
                            break;
                        }
                    case 1:
                        {
                            dataGridView1.Rows.Add(
                                revenue.Year,
                                revenue.Season,
                                revenue.Code,
                                revenue.Name,
                                double.Parse(revenue.Op_Income),
                                double.Parse(revenue.Op_Cost),
                                double.Parse(revenue.Gross),
                                double.Parse(revenue.Gross_Margin),
                                double.Parse(revenue.Last_Season_Gross_Margin),
                                double.Parse(revenue.Last_Year_Gross_Margin),
                                double.Parse(revenue.Op),
                                double.Parse(revenue.Op_Margin),
                                double.Parse(revenue.Last_Season_Op_Margin),
                                double.Parse(revenue.Last_Year_Op_Margin),
                                double.Parse(revenue.EBT),
                                double.Parse(revenue.EBT_Margin),
                                double.Parse(revenue.Last_Season_EBT_Margin),
                                double.Parse(revenue.Last_Year_EBT_Margin),
                                double.Parse(revenue.Net_Income),
                                double.Parse(revenue.Net_Income_Margin),
                                double.Parse(revenue.Last_Season_Net_Income_Margin),
                                double.Parse(revenue.Last_Year_Net_Income_Margin),
                                revenue.month,
                                revenue.month_report,
                                comparison_last_month[0],
                                0,
                                0,
                                0,
                                0,
                                comparison_last_year[0],
                                0,
                                0,
                                0,
                                0,
                                cumulative_comparison[0],
                                0,
                                0,
                                0,
                                0,
                                revenue.remark
                            );
                            break;
                        }
                    case 2:
                        {
                            dataGridView1.Rows.Add(
                                revenue.Year,
                                revenue.Season,
                                revenue.Code,
                                revenue.Name,
                                double.Parse(revenue.Op_Income),
                                double.Parse(revenue.Op_Cost),
                                double.Parse(revenue.Gross),
                                double.Parse(revenue.Gross_Margin),
                                double.Parse(revenue.Last_Season_Gross_Margin),
                                double.Parse(revenue.Last_Year_Gross_Margin),
                                double.Parse(revenue.Op),
                                double.Parse(revenue.Op_Margin),
                                double.Parse(revenue.Last_Season_Op_Margin),
                                double.Parse(revenue.Last_Year_Op_Margin),
                                double.Parse(revenue.EBT),
                                double.Parse(revenue.EBT_Margin),
                                double.Parse(revenue.Last_Season_EBT_Margin),
                                double.Parse(revenue.Last_Year_EBT_Margin),
                                double.Parse(revenue.Net_Income),
                                double.Parse(revenue.Net_Income_Margin),
                                double.Parse(revenue.Last_Season_Net_Income_Margin),
                                double.Parse(revenue.Last_Year_Net_Income_Margin),
                                revenue.month,
                                revenue.month_report,
                                comparison_last_month[0],
                                comparison_last_month[1],
                                0,
                                0,
                                0,
                                comparison_last_year[0],
                                comparison_last_year[1],
                                0,
                                0,
                                0,
                                cumulative_comparison[0],
                                cumulative_comparison[1],
                                0,
                                0,
                                0,
                                revenue.remark
                            );
                            break;
                        }
                    case 3:
                        {
                            dataGridView1.Rows.Add(
                                revenue.Year,
                                revenue.Season,
                                revenue.Code,
                                revenue.Name,
                                double.Parse(revenue.Op_Income),
                                double.Parse(revenue.Op_Cost),
                                double.Parse(revenue.Gross),
                                double.Parse(revenue.Gross_Margin),
                                double.Parse(revenue.Last_Season_Gross_Margin),
                                double.Parse(revenue.Last_Year_Gross_Margin),
                                double.Parse(revenue.Op),
                                double.Parse(revenue.Op_Margin),
                                double.Parse(revenue.Last_Season_Op_Margin),
                                double.Parse(revenue.Last_Year_Op_Margin),
                                double.Parse(revenue.EBT),
                                double.Parse(revenue.EBT_Margin),
                                double.Parse(revenue.Last_Season_EBT_Margin),
                                double.Parse(revenue.Last_Year_EBT_Margin),
                                double.Parse(revenue.Net_Income),
                                double.Parse(revenue.Net_Income_Margin),
                                double.Parse(revenue.Last_Season_Net_Income_Margin),
                                double.Parse(revenue.Last_Year_Net_Income_Margin),
                                revenue.month,
                                revenue.month_report,
                                comparison_last_month[0],
                                comparison_last_month[1],
                                comparison_last_month[2],
                                0,
                                0,
                                comparison_last_year[0],
                                comparison_last_year[1],
                                comparison_last_year[2],
                                0,
                                0,
                                cumulative_comparison[0],
                                cumulative_comparison[1],
                                cumulative_comparison[2],
                                0,
                                0,
                                revenue.remark
                            );
                            break;
                        }
                    case 4:
                        {
                            dataGridView1.Rows.Add(
                                revenue.Year,
                                revenue.Season,
                                revenue.Code,
                                revenue.Name,
                                double.Parse(revenue.Op_Income),
                                double.Parse(revenue.Op_Cost),
                                double.Parse(revenue.Gross),
                                double.Parse(revenue.Gross_Margin),
                                double.Parse(revenue.Last_Season_Gross_Margin),
                                double.Parse(revenue.Last_Year_Gross_Margin),
                                double.Parse(revenue.Op),
                                double.Parse(revenue.Op_Margin),
                                double.Parse(revenue.Last_Season_Op_Margin),
                                double.Parse(revenue.Last_Year_Op_Margin),
                                double.Parse(revenue.EBT),
                                double.Parse(revenue.EBT_Margin),
                                double.Parse(revenue.Last_Season_EBT_Margin),
                                double.Parse(revenue.Last_Year_EBT_Margin),
                                double.Parse(revenue.Net_Income),
                                double.Parse(revenue.Net_Income_Margin),
                                double.Parse(revenue.Last_Season_Net_Income_Margin),
                                double.Parse(revenue.Last_Year_Net_Income_Margin),
                                revenue.month,
                                revenue.month_report,
                                comparison_last_month[0],
                                comparison_last_month[1],
                                comparison_last_month[2],
                                comparison_last_month[3],
                                0,
                                comparison_last_year[0],
                                comparison_last_year[1],
                                comparison_last_year[2],
                                comparison_last_year[3],
                                0,
                                cumulative_comparison[0],
                                cumulative_comparison[1],
                                cumulative_comparison[2],
                                cumulative_comparison[3],
                                0,
                                revenue.remark
                            );
                            break;
                        }
                    case 5:
                        {
                            dataGridView1.Rows.Add(
                                revenue.Year,
                                revenue.Season,
                                revenue.Code,
                                revenue.Name,
                                double.Parse(revenue.Op_Income),
                                double.Parse(revenue.Op_Cost),
                                double.Parse(revenue.Gross),
                                double.Parse(revenue.Gross_Margin),
                                double.Parse(revenue.Last_Season_Gross_Margin),
                                double.Parse(revenue.Last_Year_Gross_Margin),
                                double.Parse(revenue.Op),
                                double.Parse(revenue.Op_Margin),
                                double.Parse(revenue.Last_Season_Op_Margin),
                                double.Parse(revenue.Last_Year_Op_Margin),
                                double.Parse(revenue.EBT),
                                double.Parse(revenue.EBT_Margin),
                                double.Parse(revenue.Last_Season_EBT_Margin),
                                double.Parse(revenue.Last_Year_EBT_Margin),
                                double.Parse(revenue.Net_Income),
                                double.Parse(revenue.Net_Income_Margin),
                                double.Parse(revenue.Last_Season_Net_Income_Margin),
                                double.Parse(revenue.Last_Year_Net_Income_Margin),
                                revenue.month,
                                revenue.month_report,
                                comparison_last_month[0],
                                comparison_last_month[1],
                                comparison_last_month[2],
                                comparison_last_month[3],
                                comparison_last_month[4],
                                comparison_last_year[0],
                                comparison_last_year[1],
                                comparison_last_year[2],
                                comparison_last_year[3],
                                comparison_last_year[4],
                                cumulative_comparison[0],
                                cumulative_comparison[1],
                                cumulative_comparison[2],
                                cumulative_comparison[3],
                                cumulative_comparison[4],
                                revenue.remark
                            );
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                /*
                dataGridView1.Rows.Add(revenue.Year, revenue.Season, revenue.Code, revenue.Name, double.Parse(revenue.Op_Income), double.Parse(revenue.Op_Cost), double.Parse(revenue.Gross), double.Parse(revenue.Gross_Margin),
                    double.Parse(revenue.Last_Season_Gross_Margin), double.Parse(revenue.Last_Year_Gross_Margin), double.Parse(revenue.Op), double.Parse(revenue.Op_Margin), double.Parse(revenue.Last_Season_Op_Margin),
                    double.Parse(revenue.Last_Year_Op_Margin), double.Parse(revenue.EBT), double.Parse(revenue.EBT_Margin), double.Parse(revenue.Last_Season_EBT_Margin), double.Parse(revenue.Last_Year_EBT_Margin), double.Parse(revenue.Net_Income),
                    double.Parse(revenue.Net_Income_Margin), double.Parse(revenue.Last_Season_Net_Income_Margin), double.Parse(revenue.Last_Year_Net_Income_Margin), revenue.month, revenue.month_report,
                    comparison_last_month[0], comparison_last_month[1], comparison_last_month[2], comparison_last_month[3], comparison_last_month[4],
                    comparison_last_year[0], comparison_last_year[1], comparison_last_year[2], comparison_last_year[3], comparison_last_year[4],
                    cumulative_comparison[0], cumulative_comparison[1], cumulative_comparison[2], cumulative_comparison[3], cumulative_comparison[4], revenue.remark);
                */
                
                /*
                dataGridView1.Rows.Add(
                revenue.Year,
                revenue.Season,
                revenue.Code,
                revenue.Name,
                double.Parse(revenue.Op_Income),
                double.Parse(revenue.Op_Cost),
                double.Parse(revenue.Gross),
                double.Parse(revenue.Gross_Margin),
                double.Parse(revenue.Last_Season_Gross_Margin),
                double.Parse(revenue.Last_Year_Gross_Margin),
                double.Parse(revenue.Op),
                double.Parse(revenue.Op_Margin),
                double.Parse(revenue.Last_Season_Op_Margin),
                double.Parse(revenue.Last_Year_Op_Margin),
                double.Parse(revenue.EBT),
                double.Parse(revenue.EBT_Margin),
                double.Parse(revenue.Last_Season_EBT_Margin),
                double.Parse(revenue.Last_Year_EBT_Margin),
                double.Parse(revenue.Net_Income),
                double.Parse(revenue.Net_Income_Margin),
                double.Parse(revenue.Last_Season_Net_Income_Margin),
                double.Parse(revenue.Last_Year_Net_Income_Margin),
                revenue.month,
                revenue.month_report,
                comparison_last_month[0],
                comparison_last_month[1],
                comparison_last_month[2],
                comparison_last_month[3],
                comparison_last_month[4],
                comparison_last_year[0],
                comparison_last_year[1],
                comparison_last_year[2],
                comparison_last_year[3],
                comparison_last_year[4],
                cumulative_comparison[0],
                cumulative_comparison[1],
                cumulative_comparison[2],
                cumulative_comparison[3] != null ? cumulative_comparison[3] : 0,// 如果cumulative_comparison[3]为null，则输出0
                cumulative_comparison[4],
                revenue.remark
                );
                */

            }

        }
        public async Task<int> get_Financial_Report(string year, string season)
        {
            string url = String.Format("https://mops.twse.com.tw/mops/web/ajax_t163sb04?encodeURIComponent=1&step=1&firstin=1&off=1&isQuery=Y&TYPEK=sii&year={0}&season={1}", year, season);

            // Create HttpClient instance
            HttpClient client = new HttpClient();
            string html = await client.GetStringAsync(url);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            // 檢查 tableData 是否為空，如果是，新增一個元素


            // 选择所有表格中的tr元素
            HtmlNodeCollection tables = doc.DocumentNode.SelectNodes("//table[@class='hasBorder']");
            if (tables == null)
            {
                return 0;
            }
            tableData = new List<List<string[]>>(tables.Count);
            int i = 0;

            foreach (HtmlNode table in tables)
            {
                int j = 0;
                tableData.Add(new List<string[]>(table.SelectNodes("./tr").Count()));
                //tableData[i] = new List<string[]>(table.SelectNodes("./tr").Count()); 
                foreach (HtmlNode rowNode in table.SelectNodes("./tr"))
                {
                    int k = 0;

                    if (rowNode.SelectNodes("./td") != null)
                    {
                        // 檢查 tableData[0] 是否為空，如果是，新增一個元素

                        //ableData[i][j] = new string[rowNode.SelectNodes("./td").Count()];
                        tableData[i].Add(new string[rowNode.SelectNodes("./td").Count()]);
                        // 遍歷行的每一個單元格
                        foreach (HtmlNode cellNode in rowNode.SelectNodes("./td"))
                        {
                            tableData[i][j][k] = cellNode.InnerText.Trim();
                            //MessageBox.Show(k.ToString() + "  " + tableData[i][j][k]);
                            k++;
                        }
                    }
                    else
                    {
                        tableData[i].Add(new string[rowNode.SelectNodes("./th").Count()]);
                        // 遍歷行的每一個單元格
                        foreach (HtmlNode cellNode in rowNode.SelectNodes("./th"))
                        {
                            tableData[i][j][k] = cellNode.InnerText.Trim();
                            //MessageBox.Show(k.ToString() + "  " + tableData[i][j][k]);
                            k++;
                        }
                    }

                    j++;

                }
                i++;
            }
            //上櫃
            url = String.Format("https://mops.twse.com.tw/mops/web/ajax_t163sb04?encodeURIComponent=1&step=1&firstin=1&off=1&isQuery=Y&TYPEK=otc&year={0}&season={1}", year, season);
            client = new HttpClient();
            html = await client.GetStringAsync(url);
            doc = new HtmlDocument();
            doc.LoadHtml(html);
            tables = doc.DocumentNode.SelectNodes("//table[@class='hasBorder']");
            i = 1;
            foreach (HtmlNode table in tables)
            {
                int j = 0;
                //tableData[i] = new string[table.SelectNodes("./tr").Count()][];
                //List<string[]> otc_table = new List<string[]>(table.SelectNodes("./tr").Count());
                foreach (HtmlNode rowNode in table.SelectNodes("./tr"))
                {
                    if(j == 0)
                    {
                        j++;
                        continue;
                    }
                    int k = 0;
                    string[] row = new string[rowNode.SelectNodes("./td").Count()];
                    // 遍歷行的每一個單元格
                    foreach (HtmlNode cellNode in rowNode.SelectNodes("./td"))
                    {
                        row[k] = cellNode.InnerText.Trim();
                        //MessageBox.Show(k.ToString() + "  " + row[k]);
                        k++;
                    }
                    tableData[i].Add(row);
                    j++;

                }
                
                i++;
            }
            countRate(year, season);
            return 1;
        }
        public string Minus(string str1, string str2)
        {
            string result;
            if (double.TryParse(str1, out double num1) && double.TryParse(str2, out double num2))
            {
                result = (num1 - num2).ToString("F2");
                return result;
            }
            return "0";
        }
        public string Divide(string str1, string str2)
        {
            string result;
            if (double.TryParse(str1, out double num1) && double.TryParse(str2, out double num2))
            {
                if (num2 != 0)
                {

                    result = (num1 / num2 * 100).ToString("F2");
                    return result;
                }
                else
                {
                    return "0";
                }
            }

            return "0";
        }
        public void countRate(string year, string season)
        {
            List<Revenue> revenueList = new List<Revenue>();
            for (int i = 0; i< tableData.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        {
                            for (int j = 1; j < tableData[i].Count; j++)
                            {
                                Revenue revenue = new Revenue();
                                revenue.Year = year;
                                revenue.Season = season;
                                revenue.Code = tableData[i][j][0];
                                revenue.Name = tableData[i][j][1];
                                revenue.Op_Income = (double.Parse(tableData[i][j][2]) + double.Parse(tableData[i][j][3])).ToString(); //營業收入
                                revenue.Op_Cost = "0"; //營業成本
                                revenue.Gross = (double.Parse(revenue.Op_Income) - double.Parse(tableData[i][j][2])).ToString(); //毛利
                                revenue.Gross_Margin = "0"; //毛利率
                                revenue.Op = "0"; //營業利益
                                revenue.Op_Margin = "0"; //營益率
                                revenue.EBT = "0"; //稅前純益
                                revenue.EBT_Margin = "0";//稅前純益率
                                revenue.Net_Income = "0";//稅後純益
                                revenue.Net_Income_Margin = "0";    //稅後純益率
                                revenueList.Add(revenue);
                            }
                                
                            break;
                        }
                    case 1:
                        {
                            for (int j = 1; j < tableData[i].Count; j++)
                            {
                                Revenue revenue = new Revenue();
                                /*
                                revenue.Year = year;
                                revenue.Season = season;
                                revenue.Code = tableData[i][j][0];
                                revenue.Name = tableData[i][j][1];
                                revenue.Op_Income = tableData[i][j][2];
                                revenue.Op_Cost = "0";
                                revenue.Gross = "0";
                                revenue.Gross_Margin = "0";
                                revenue.Op = tableData[i][j][4];
                                revenue.Op_Margin = (double.Parse(revenue.Op) / double.Parse(revenue.Op_Income)*100).ToString();
                                revenue.EBT = tableData[i][j][6];
                                revenue.EBT_Margin = (double.Parse(revenue.EBT) / double.Parse(revenue.Op_Income)*100).ToString();
                                revenue.Net_Income = tableData[i][j][15];
                                revenue.Net_Income_Margin = (double.Parse(revenue.Net_Income) / double.Parse(revenue.Op_Income)*100).ToString();
                                */
                                revenue.Year = year;
                                revenue.Season = season;
                                revenue.Code = tableData[i][j][0];
                                revenue.Name = tableData[i][j][1];
                                revenue.Op_Income = tableData[i][j][2];
                                revenue.Op_Cost = "0";
                                revenue.Gross = "0";
                                revenue.Gross_Margin = "0";
                                revenue.Op = tableData[i][j][4];
                                revenue.Op_Margin = Divide(revenue.Op, revenue.Op_Income) ;
                                revenue.EBT = tableData[i][j][6];
                                revenue.EBT_Margin = Divide(revenue.EBT, revenue.Op_Income) ;
                                revenue.Net_Income = tableData[i][j][15];
                                revenue.Net_Income_Margin = Divide(revenue.Net_Income, revenue.Op_Income) ;
                                revenueList.Add(revenue);
                            }
                            break;
                        }
                    case 2:
                        {
                            for (int j = 1; j < tableData[i].Count; j++)
                            {
                                Revenue revenue = new Revenue();
                                /*
                                revenue.Year = year;
                                revenue.Season = season;
                                revenue.Code = tableData[i][j][0];
                                revenue.Name = tableData[i][j][1];
                                revenue.Op_Income = tableData[i][j][2];
                                revenue.Op_Cost = tableData[i][j][3];
                                revenue.Gross = (double.Parse(revenue.Op_Income) - double.Parse(revenue.Op_Cost)).ToString();
                                revenue.Gross_Margin = (double.Parse(revenue.Gross) / double.Parse(revenue.Op_Income)*100).ToString();
                                revenue.Op = (double.Parse(revenue.Gross) - double.Parse(tableData[i][j][10])).ToString();
                                revenue.Op_Margin = (double.Parse(revenue.Op) / double.Parse(revenue.Op_Income)*100).ToString();
                                revenue.EBT = tableData[i][j][14];
                                revenue.EBT_Margin = (double.Parse(revenue.EBT) / double.Parse(revenue.Op_Income)*100).ToString();
                                revenue.Net_Income = tableData[i][j][23];
                                revenue.Net_Income_Margin = (double.Parse(revenue.Net_Income) / double.Parse(revenue.Op_Income)*100).ToString();
                                */

                                revenue.Year = year;
                                revenue.Season = season;
                                revenue.Code = tableData[i][j][0];
                                revenue.Name = tableData[i][j][1];
                                revenue.Op_Income = tableData[i][j][2];
                                revenue.Op_Cost = tableData[i][j][3];
                                revenue.Gross = Minus(revenue.Op_Income,  revenue.Op_Cost);
                                revenue.Gross_Margin = Divide(revenue.Gross, revenue.Op_Income) ;
                                revenue.Op = Minus(revenue.Gross, tableData[i][j][10]);
                                revenue.Op_Margin = Divide(revenue.Op, revenue.Op_Income);
                                revenue.EBT = tableData[i][j][14];
                                revenue.EBT_Margin = Divide(revenue.EBT, revenue.Op_Income);
                                revenue.Net_Income = tableData[i][j][23];
                                revenue.Net_Income_Margin = Divide(revenue.Net_Income, revenue.Op_Income);
                                revenueList.Add(revenue);
                            }
                            break;
                        }
                    case 3:
                        {
                            for (int j = 1; j < tableData[i].Count; j++)
                            {
                                Revenue revenue = new Revenue();
                                /*
                                revenue.Year = year;
                                revenue.Season = season;
                                revenue.Code = tableData[i][j][0];
                                revenue.Name = tableData[i][j][1];
                                revenue.Op_Income = tableData[i][j][4];
                                revenue.Op_Cost = "0";
                                revenue.Gross = "0";
                                revenue.Gross_Margin = "0";
                                revenue.Op = "0";
                                revenue.Op_Margin = "0";
                                revenue.EBT = tableData[i][j][8];
                                revenue.EBT_Margin = (double.Parse(revenue.EBT) / double.Parse(revenue.Op_Income) * 100).ToString();
                                revenue.Net_Income = tableData[i][j][12];
                                revenue.Net_Income_Margin = (double.Parse(revenue.Net_Income) / double.Parse(revenue.Op_Income) * 100).ToString();
                                */
                                revenue.Year = year;
                                revenue.Season = season;
                                revenue.Code = tableData[i][j][0];
                                revenue.Name = tableData[i][j][1];
                                revenue.Op_Income = tableData[i][j][2];
                                revenue.Op_Cost = tableData[i][j][3];
                                revenue.Gross = (double.Parse(revenue.Op_Income) - double.Parse(revenue.Op_Cost)).ToString();
                                revenue.Gross_Margin = Divide(revenue.Gross, revenue.Op_Income);
                                revenue.Op = tableData[i][j][5];
                                revenue.Op_Margin = Divide(revenue.Op, revenue.Op_Income);
                                revenue.EBT = tableData[i][j][7];
                                revenue.EBT_Margin = Divide(revenue.EBT, revenue.Op_Income);
                                revenue.Net_Income = tableData[i][j][9];
                                revenue.Net_Income_Margin = Divide(revenue.Net_Income, revenue.Op_Income) ;
                                revenueList.Add(revenue);
                            }
                            break;
                        }
                    case 4:
                        {
                            for (int j = 1; j < tableData[i].Count; j++)
                            {
                                Revenue revenue = new Revenue();
                                /*
                                revenue.Year = year;
                                revenue.Season = season;
                                revenue.Code = tableData[i][j][0];
                                revenue.Name = tableData[i][j][1];
                                revenue.Op_Income = tableData[i][j][2];
                                revenue.Op_Cost = tableData[i][j][3];
                                revenue.Gross = (double.Parse(revenue.Op_Income) - double.Parse(revenue.Op_Cost)).ToString();
                                revenue.Gross_Margin = (double.Parse(revenue.Gross) / double.Parse(revenue.Op_Income) * 100).ToString();
                                revenue.Op = tableData[i][j][5];
                                revenue.Op_Margin = (double.Parse(revenue.Op) / double.Parse(revenue.Op_Income) * 100).ToString();
                                revenue.EBT = tableData[i][j][7];
                                revenue.EBT_Margin = (double.Parse(revenue.EBT) / double.Parse(revenue.Op_Income) * 100).ToString();
                                revenue.Net_Income = tableData[i][j][9];
                                revenue.Net_Income_Margin = (double.Parse(revenue.Net_Income) / double.Parse(revenue.Op_Income) * 100).ToString();
                                */
                                revenue.Year = year;
                                revenue.Season = season;
                                revenue.Code = tableData[i][j][0];
                                revenue.Name = tableData[i][j][1];
                                revenue.Op_Income = tableData[i][j][2];
                                revenue.Op_Cost = tableData[i][j][3];
                                revenue.Gross = (double.Parse(revenue.Op_Income) - double.Parse(revenue.Op_Cost)).ToString();
                                revenue.Gross_Margin = Divide(revenue.Gross, revenue.Op_Income);
                                revenue.Op = tableData[i][j][5];
                                revenue.Op_Margin = Divide(revenue.Op, revenue.Op_Income);
                                revenue.EBT = tableData[i][j][7];
                                revenue.EBT_Margin = Divide(revenue.EBT, revenue.Op_Income);
                                revenue.Net_Income = tableData[i][j][9];
                                revenue.Net_Income_Margin = Divide(revenue.Net_Income, revenue.Op_Income);
                                revenueList.Add(revenue);
                            }
                            break;
                        }
                    case 5:
                        {
                            for (int j = 1; j < tableData[i].Count; j++)
                            {
                                Revenue revenue = new Revenue();
                                /*
                                revenue.Year = year;
                                revenue.Season = season;
                                revenue.Code = tableData[i][j][0];
                                revenue.Name = tableData[i][j][1];
                                revenue.Op_Income = tableData[i][j][2];
                                revenue.Op_Cost = tableData[i][j][3];
                                revenue.Gross = (double.Parse(revenue.Op_Income) - double.Parse(revenue.Op_Cost)).ToString();
                                revenue.Gross_Margin = (double.Parse(revenue.Gross) / double.Parse(revenue.Op_Income) * 100).ToString();
                                revenue.Op = tableData[i][j][8];
                                revenue.Op_Margin = (double.Parse(revenue.Op) / double.Parse(revenue.Op_Income) * 100).ToString();
                                revenue.EBT = tableData[i][j][4];
                                revenue.EBT_Margin = (double.Parse(revenue.EBT) / double.Parse(revenue.Op_Income) * 100).ToString();
                                revenue.Net_Income = tableData[i][j][11];
                                revenue.Net_Income_Margin = (double.Parse(revenue.Net_Income) / double.Parse(revenue.Op_Income) * 100).ToString();
                                */
                                revenue.Year = year;
                                revenue.Season = season;
                                revenue.Code = tableData[i][j][0];
                                revenue.Name = tableData[i][j][1];
                                revenue.Op_Income = tableData[i][j][2];
                                revenue.Op_Cost = tableData[i][j][3];
                                revenue.Gross = (double.Parse(revenue.Op_Income) - double.Parse(revenue.Op_Cost)).ToString();
                                revenue.Gross_Margin = Divide(revenue.Gross, revenue.Op_Income);
                                revenue.Op = tableData[i][j][8];
                                revenue.Op_Margin = Divide(revenue.Op, revenue.Op_Income);
                                revenue.EBT = tableData[i][j][4];
                                revenue.EBT_Margin = Divide(revenue.EBT, revenue.Op_Income);
                                revenue.Net_Income = tableData[i][j][11];
                                revenue.Net_Income_Margin = Divide(revenue.Net_Income, revenue.Op_Income);
                                revenueList.Add(revenue);
                            }
                            break;
                        }
                    default: break;
                }
                    
            }
            nestedList.Add(revenueList);
            
            
        }
        
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Visible = true; // 设置每一行的可见性为 true
            }
            foreach (String condition in checkedListBox1.CheckedItems)
            {
                switch (condition)
                {
                    case "營業收入(百萬元)>1000":
                        {
                            for (int rowIndex = 0; rowIndex < dataGridView1.Rows.Count; rowIndex++)
                            {
                                DataGridViewRow row = dataGridView1.Rows[rowIndex];
                                DataGridViewCell cell = row.Cells["Op_Income"];

                                if (cell.Value != null && double.Parse(cell.Value.ToString()) < 1000)
                                {
                                    row.Visible = false;
                                }
                            }
                            break;
                        }

                    case "毛利率(%)>15":
                        {
                            for (int rowIndex = 0; rowIndex < dataGridView1.Rows.Count; rowIndex++)
                            {
                                DataGridViewRow row = dataGridView1.Rows[rowIndex];
                                DataGridViewCell cell = row.Cells["Gross_Margin"];

                                if (cell.Value != null && double.Parse(cell.Value.ToString()) < 15)
                                {
                                    row.Visible = false;
                                }
                            }
                            break;
                        }
                        
                    case "營業利益率(%)>15":
                        {
                            for (int rowIndex = 0; rowIndex < dataGridView1.Rows.Count; rowIndex++)
                            {
                                DataGridViewRow row = dataGridView1.Rows[rowIndex];
                                DataGridViewCell cell = row.Cells["Op_Margin"];

                                if (cell.Value != null && double.Parse(cell.Value.ToString()) < 15)
                                {
                                    row.Visible = false;
                                }
                            }
                            break;
                        }
                    case "稅前純益率(%)>15":
                        {
                            for (int rowIndex = 0; rowIndex < dataGridView1.Rows.Count; rowIndex++)
                            {
                                DataGridViewRow row = dataGridView1.Rows[rowIndex];
                                DataGridViewCell cell = row.Cells["EBT_Margin"];

                                if (cell.Value != null && double.Parse(cell.Value.ToString()) < 15)
                                {
                                    row.Visible = false;
                                }
                            }
                            break;
                        }
                    case "稅後純益率(%)>15":
                        {
                            for (int rowIndex = 0; rowIndex < dataGridView1.Rows.Count; rowIndex++)
                            {
                                DataGridViewRow row = dataGridView1.Rows[rowIndex];
                                DataGridViewCell cell = row.Cells["Net_Income_Margin"];

                                if (cell.Value != null && double.Parse(cell.Value.ToString()) < 15)
                                {
                                    //MessageBox.Show(cell.Value.ToString());
                                    row.Visible = false;
                                }
                            }
                            break;
                        }
                    case "三率三升(上季)":
                        {
                            for (int rowIndex = 0; rowIndex < dataGridView1.Rows.Count; rowIndex++)
                            {
                                DataGridViewRow row = dataGridView1.Rows[rowIndex];
                                double Last_Season_Gross_Margin = double.Parse(row.Cells["Last_Season_Gross_Margin"].Value.ToString().Trim());
                                double Last_Season_Op_Margin = double.Parse(row.Cells["Last_Season_Op_Margin"].Value.ToString().Trim());
                                double Last_Season_EBT_Margin = double.Parse(row.Cells["Last_Season_EBT_Margin"].Value.ToString().Trim());
                                double Last_Season_Net_Income_Margin = double.Parse(row.Cells["Last_Season_Net_Income_Margin"].Value.ToString().Trim());
                                
                                if (Last_Season_Gross_Margin <= 0 || Last_Season_Op_Margin <= 0 || Last_Season_EBT_Margin <= 0 || Last_Season_Net_Income_Margin <= 0)
                                {
                                    row.Visible = false;
                                }
                                else
                                {

                                }
                            }
                            break;
                        }
                    case "三率三升(去年)":
                        {
                            for (int rowIndex = 0; rowIndex < dataGridView1.Rows.Count; rowIndex++)
                            {
                                DataGridViewRow row = dataGridView1.Rows[rowIndex];
                                double Last_Year_Gross_Margin = double.Parse(row.Cells["Last_Year_Gross_Margin"].Value.ToString().Trim());
                                double Last_Year_Op_Margin = double.Parse(row.Cells["Last_Year_Op_Margin"].Value.ToString().Trim());
                                double Last_Year_EBT_Margin = double.Parse(row.Cells["Last_Year_EBT_Margin"].Value.ToString().Trim());
                                double Last_Year_Net_Income_Margin = double.Parse(row.Cells["Last_Year_Net_Income_Margin"].Value.ToString().Trim());
                                if (Last_Year_Gross_Margin <= 0 || Last_Year_Op_Margin <= 0 || Last_Year_EBT_Margin <= 0 || Last_Year_Net_Income_Margin <= 0)
                                {
                                    row.Visible = false;
                                }
                            }
                            break;
                        }
                    case "全為正":
                        {
                            for (int rowIndex = 0; rowIndex < dataGridView1.Rows.Count; rowIndex++)
                            {
                                DataGridViewRow row = dataGridView1.Rows[rowIndex];
                                double Gross_Margin = double.Parse(row.Cells["Gross_Margin"].Value.ToString().Trim());
                                double Op_Margin = double.Parse(row.Cells["Op_Margin"].Value.ToString().Trim());
                                double EBT_Margin = double.Parse(row.Cells["EBT_Margin"].Value.ToString().Trim());
                                double Net_Income_Margin = double.Parse(row.Cells["Net_Income_Margin"].Value.ToString().Trim());
                                if (Gross_Margin <= 0 || Op_Margin <= 0 || EBT_Margin <= 0 || Net_Income_Margin <= 0)
                                {
                                    row.Visible = false;
                                }
                            }
                            break;
                        }
                    case "近五月月營收為正成長":
                        {
                            for (int rowIndex = 0; rowIndex < dataGridView1.Rows.Count; rowIndex++)
                            {
                                DataGridViewRow row = dataGridView1.Rows[rowIndex];
                                double comparison_last_month1 = double.Parse(row.Cells["comparison_last_month1"].Value.ToString().Trim());
                                double comparison_last_month2 = double.Parse(row.Cells["comparison_last_month2"].Value.ToString().Trim());
                                double comparison_last_month3 = double.Parse(row.Cells["comparison_last_month3"].Value.ToString().Trim());
                                double comparison_last_month4 = double.Parse(row.Cells["comparison_last_month4"].Value.ToString().Trim());
                                double comparison_last_month5 = double.Parse(row.Cells["comparison_last_month5"].Value.ToString().Trim());
                                if (comparison_last_month1 <= 0 || comparison_last_month2 <= 0 || comparison_last_month3 <= 0 || comparison_last_month4 <= 0 || comparison_last_month5 <= 0)
                                {
                                    row.Visible = false;
                                }
                            }
                            break;
                        }
                    //近五月月營收為正成長
                    case "近五月去年比為正成長":
                        {
                            for (int rowIndex = 0; rowIndex < dataGridView1.Rows.Count; rowIndex++)
                            {
                                DataGridViewRow row = dataGridView1.Rows[rowIndex];
                                double comparison_last_year1 = double.Parse(row.Cells["comparison_last_year1"].Value.ToString().Trim());
                                double comparison_last_year2 = double.Parse(row.Cells["comparison_last_year2"].Value.ToString().Trim());
                                double comparison_last_year3 = double.Parse(row.Cells["comparison_last_year3"].Value.ToString().Trim());
                                double comparison_last_year4 = double.Parse(row.Cells["comparison_last_year4"].Value.ToString().Trim());
                                double comparison_last_year5 = double.Parse(row.Cells["comparison_last_year5"].Value.ToString().Trim());
                                if (comparison_last_year1 <= 0 || comparison_last_year2 <= 0 || comparison_last_year3 <= 0 || comparison_last_year4 <= 0 || comparison_last_year5 <= 0)
                                {
                                    row.Visible = false;
                                }
                            }
                            break;
                        }
                    //近五月月營收為正成長
                    case "近五月累計收入為正成長":
                        {
                            for (int rowIndex = 0; rowIndex < dataGridView1.Rows.Count; rowIndex++)
                            {
                                DataGridViewRow row = dataGridView1.Rows[rowIndex];
                                double cumulative_comparison1 = double.Parse(row.Cells["cumulative_comparison1"].Value.ToString().Trim());
                                double cumulative_comparison2 = double.Parse(row.Cells["cumulative_comparison2"].Value.ToString().Trim());
                                double cumulative_comparison3 = double.Parse(row.Cells["cumulative_comparison3"].Value.ToString().Trim());
                                double cumulative_comparison4 = double.Parse(row.Cells["cumulative_comparison4"].Value.ToString().Trim());
                                double cumulative_comparison5 = double.Parse(row.Cells["cumulative_comparison5"].Value.ToString().Trim());
                                if (cumulative_comparison1 <= 0 || cumulative_comparison2 <= 0 || cumulative_comparison3 <= 0 || cumulative_comparison4 <= 0 || cumulative_comparison5 <= 0)
                                {
                                    row.Visible = false;
                                }
                            }
                            break;
                        }
                    //近五月月營收為正成長
                    case "近3月為正":
                        {
                            for (int rowIndex = 0; rowIndex < dataGridView1.Rows.Count; rowIndex++)
                            {
                                DataGridViewRow row = dataGridView1.Rows[rowIndex];
                                double cumulative_comparison1 = double.Parse(row.Cells["cumulative_comparison1"].Value.ToString().Trim());
                                double cumulative_comparison2 = double.Parse(row.Cells["cumulative_comparison2"].Value.ToString().Trim());
                                double cumulative_comparison3 = double.Parse(row.Cells["cumulative_comparison3"].Value.ToString().Trim());

                                if (cumulative_comparison1 <= 0 || cumulative_comparison2 <= 0 || cumulative_comparison3 <= 0 )
                                {
                                    row.Visible = false;
                                }
                            }
                            break;
                        }
                    default:
                        break;

                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<List<String>> Companylists = new List<List<string>>(10);
            List<String> selectlist = new List<String>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if(row.Visible == true)
                {
                    string select = row.Cells["code"].Value?.ToString();
                    int count = 0;
                    //string[][] Companylist = new string[100][];
                    int index = form1.Get_Companylist_otc().FindIndex(company => company.SecuritiesCompanyCode == select);
                    for (count = 0; count < 10; count++)
                    {
                        if (Companylists.Count == 0)
                        {
                            Companylists.Add(new List<string>());
                        }
                        if (Companylists[count].Count != 30)
                        {

                            if (index != -1)
                            {
                                Companylists[count].Add(String.Format("otc_{0}.tw", select));
                                selectlist.Add(select);
                                break;
                            }
                            else
                            {
                                Companylists[count].Add(String.Format("tse_{0}.tw", select));
                                selectlist.Add(select);
                                break;
                            }
                        }
                        else if (Companylists.Count == count + 1)
                        {
                            Companylists.Add(new List<string>());
                        }


                    }
                    
                }
                
            }
            form1.add_Select(Companylists, selectlist);
        }
    }

    public class MonthReportMap : ClassMap<MonthReport>
    {
        public MonthReportMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.year).Ignore();
            Map(m => m.month).Ignore();
        }
    }
    public class MonthReport
    {
        
        [Name("公司代號")]
        public string Code { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        [Name("營業收入-當月營收")]
        public string month_report { get; set; }
        [Name("營業收入-上月比較增減(%)")]
        public string comparison_last_month { get; set; }
        [Name("營業收入-去年同月增減(%)")]
        public string comparison_last_year { get; set; }
        [Name("累計營業收入-前期比較增減(%)")]
        public string cumulative_comparison { get; set; }
        [Name("備註")]
        public string remark { get; set; }
        /*
        public static explicit operator MonthReport(List<MonthReport> v)
        {
            throw new NotImplementedException();
        }
        */
    }

    public class Revenue
    {
        /*
        [JsonPropertyName("出表日期")]
        public string  Data{ get; set; }
        */
        [JsonPropertyName("年度")]
        public string  Year{ get; set; }

        [JsonPropertyName("季別")]
        public string  Season{ get; set; }

        [JsonPropertyName("公司代號")]
        public string  Code{ get; set; }

        [JsonPropertyName("公司名稱")]
        public string  Name{ get; set; }

        [JsonPropertyName("營業收入")]
        public string  Op_Income{ get; set; }
        public string Op_Cost { get; set; }
        public string Gross { get; set; }

        [JsonPropertyName("毛利率(%)(營業毛利)/(營業收入)")]
        public string Gross_Margin{ get; set; }

        public string Last_Season_Gross_Margin { get; set; }
        public string Last_Year_Gross_Margin { get; set; }
        public string Op { get; set; }

        [JsonPropertyName("營業利益率(%)(營業利益)/(營業收入)")]
        public string  Op_Margin{ get; set; }
        public string Last_Season_Op_Margin { get; set; }
        public string Last_Year_Op_Margin { get; set; }
        public string EBT { get; set; }

        [JsonPropertyName("稅前純益率(%)(稅前純益)/(營業收入)")]
        public string EBT_Margin{ get; set; }
        public string Last_Season_EBT_Margin { get; set; }
        public string Last_Year_EBT_Margin { get; set; }
        public string Net_Income { get; set; }

        [JsonPropertyName("稅後純益率(%)(稅後純益)/(營業收入)")]
        public string Net_Income_Margin { get; set; }
        public string Last_Season_Net_Income_Margin { get; set; }
        public string Last_Year_Net_Income_Margin { get; set; }
        public int month { get; set; }
        public string month_report { get; set; }

        public List<string> comparison_last_month { get; set; }
        public List<string> comparison_last_year { get; set; }
        public List<string> cumulative_comparison { get; set; }
        public string remark { get; set; }

    }
}
