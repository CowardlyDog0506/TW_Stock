using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;

namespace TW_Stock
{


    public partial class Form1 : Form
    {
        private Timer timer;
        public static string Get(string url)
        {
            
            var web = new WebClient();
            return web.DownloadString(url);
        }
        public Form1()
        {
            InitializeComponent();

        }
        Form2 f = null;
        List<Company_tse>? Companylist_tse = new List<Company_tse>();
        List<Company_otc>? Companylist_otc = new List<Company_otc>();
        List<CompanyEPS>? Company_EPS_list = new List<CompanyEPS>();
        List<List<String>> Companylists = new List<List<string>>(10);
        CompanyPrices? CompanyMsg = new CompanyPrices();
        String stock_list = "";
        String url = "";
        String[] stock_urls = new String[10];
        String url_eps = "https://openapi.twse.com.tw/v1/opendata/t187ap14_L";
        private async void Form1_Load(object sender, EventArgs e)
        {
            url = "http://mis.twse.com.tw/stock/api/getStockInfo.jsp?ex_ch=tse_0050.tw|tse_0056.tw|tse_2330.tw|tse_2317.tw|tse_1216.tw|otc_6547.tw|otc_6180.tw";
            

            url = "https://openapi.twse.com.tw/v1/opendata/t187ap03_L";
            String Comapnyjsom = Get(url);
            Companylist_tse = JsonSerializer.Deserialize<List<Company_tse>>(Comapnyjsom);
            url = "https://www.tpex.org.tw/openapi/v1/mopsfin_t187ap03_O";
            Comapnyjsom = Get(url);
            Companylist_otc = JsonSerializer.Deserialize<List<Company_otc>>(Comapnyjsom);




            foreach (var company in Companylist_tse)
            {
                listBox1.Items.Add(company.scode + " " + company.name);
            }
            foreach (var company in Companylist_otc)
            {
                listBox1.Items.Add(company.SecuritiesCompanyCode + " " + company.Name);
            }
            String Comapny_EPS = Get(url_eps);//EPS
            Company_EPS_list = JsonSerializer.Deserialize<List<CompanyEPS>>(Comapny_EPS);
            //await Timer_Tick(sender, e, url);
            timer = new Timer();
            timer.Interval = 5000; // 每秒更新一次文本
            timer.Tick += async (sender, e) => await Timer_Tick(sender, e, url);
            


        }

        public static string Time2String(long t)
        {
            Console.WriteLine(t);
            double timestamp = (double)t / 1000 + 8 * 60 * 60; // # UTC時間加8小時為台灣時間

            DateTime dateTime = DateTimeOffset.FromUnixTimeSeconds((long)timestamp).DateTime;
            string formattedTime = dateTime.ToString("yyyy-MM-dd HH:mm:ss");

            return formattedTime;
        }

        private async Task Timer_Tick(object sender, EventArgs e, String url)
        {
            timer.Stop(); // 停止计时器

            if (Companylists.Count() != 0)
            {
                int count = 0;
                foreach(string stock_url in stock_urls)
                {
                    if (stock_url == null)
                        break;
                    if(count != 0)
                        await Task.Delay(TimeSpan.FromSeconds(5));
                    String ComapnyData = Get(stock_url);
                    CompanyMsg = JsonSerializer.Deserialize<CompanyPrices>(ComapnyData);
                    if (f != null)
                        f.companyMsg_Set(CompanyMsg);
                    textBox2.Text = ComapnyData;
                    foreach (var company in CompanyMsg.msgArray)
                    {
                        /*
                        data = String.Format("股票代號: {0} 公司簡稱: {1} 成交價: {2} 資料更新時間: {3}", company.c, company.n, company.z, Time2String(long.Parse(company.tlong)));
                        int index = Company_EPS_list.FindIndex(c => c.code == company.c);
                        if (index != -1)
                        {
                            data += String.Format(" EPS: {0}({1} {2})", Company_EPS_list[index].eps, Company_EPS_list[index].year, Company_EPS_list[index].season);
                            listBox2.Items.Add(data);
                        }
                        */
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            string cellValue = row.Cells["code"].Value?.ToString();
                            int rowIndex = -1;
                            String change_price = "";
                            String change_rate = "";
                            if (cellValue == company.c)
                            {

                                rowIndex = row.Index;
                                if (company.z == "-")
                                {
                                    dataGridView1.Rows[rowIndex].Cells["price"].Value = dataGridView1.Rows[rowIndex].Cells["price"].Value;
                                }
                                else
                                {
                                    dataGridView1.Rows[rowIndex].Cells["price"].Value = company.z;
                                }
                                if (dataGridView1.Rows[rowIndex].Cells["price"].Value.Equals("-"))
                                {
                                    change_price = "-";
                                    change_rate = "-";


                                    dataGridView1.Rows[rowIndex].Cells["price"].Style.ForeColor = Color.Yellow;
                                    dataGridView1.Rows[rowIndex].Cells["change_price"].Style.ForeColor = Color.Yellow;
                                    dataGridView1.Rows[rowIndex].Cells["change_rate"].Style.ForeColor = Color.Yellow;
                                }
                                else
                                {
                                    change_price = (double.Parse(company.z) - double.Parse(company.y)).ToString("0.00");
                                    change_rate = (CalculatePriceChange(double.Parse(company.y), double.Parse(company.z))).ToString("0.00");
                                    if (double.Parse(change_price) < 0)
                                    {

                                        dataGridView1.Rows[rowIndex].Cells["price"].Style.ForeColor = Color.Green;
                                        dataGridView1.Rows[rowIndex].Cells["change_price"].Style.ForeColor = Color.Green;
                                        dataGridView1.Rows[rowIndex].Cells["change_rate"].Style.ForeColor = Color.Green;
                                    }
                                    else if (double.Parse(change_price) == 0)
                                    {


                                        dataGridView1.Rows[rowIndex].Cells["price"].Style.ForeColor = Color.Yellow;
                                        dataGridView1.Rows[rowIndex].Cells["change_price"].Style.ForeColor = Color.Yellow;
                                        dataGridView1.Rows[rowIndex].Cells["change_rate"].Style.ForeColor = Color.Yellow;
                                    }
                                    else
                                    {


                                        dataGridView1.Rows[rowIndex].Cells["price"].Style.ForeColor = Color.Red;
                                        dataGridView1.Rows[rowIndex].Cells["change_price"].Style.ForeColor = Color.Red;
                                        dataGridView1.Rows[rowIndex].Cells["change_rate"].Style.ForeColor = Color.Red;
                                    }
                                }
                                dataGridView1.Rows[rowIndex].Cells["update_time"].Value = Time2String(long.Parse(company.tlong));

                                break;
                            }


                        }
                    }
                    count++;
                }
                
            }
            timer.Start(); // 重新启动计时器

        }
        public List<Company_otc> Get_Companylist_otc()
        {
            return Companylist_otc;
        }
        void f_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Show();
        }
        public static double CalculatePriceChange(double closePrice, double nowPrice)
        {
            double change = ((nowPrice - closePrice) / closePrice) * 100;
            return change;
        }

        private void propertyGrid1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void dataGridView1_CellDoubleClick(Object sender, DataGridViewCellEventArgs e)
        {
            /*
            System.Text.StringBuilder messageBoxCS = new System.Text.StringBuilder();
            messageBoxCS.AppendFormat("{0} = {1}", "ColumnIndex", e.ColumnIndex);
            messageBoxCS.AppendLine();
            messageBoxCS.AppendFormat("{0} = {1}", "RowIndex", e.RowIndex);
            messageBoxCS.AppendLine();
            MessageBox.Show(messageBoxCS.ToString(), "CellDoubleClick Event");
            */
            String company = dataGridView1.Rows[e.RowIndex].Cells["Code"].Value.ToString();
            f = new Form2(company, CompanyMsg, this);
            f.companyMsg_Set(CompanyMsg);
            f.FormClosing += new FormClosingEventHandler(f_FormClosing);
            f.Show();
            //this.Hide();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            timer.Start();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            timer.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int count = 0;
            //string[][] Companylist = new string[100][];
            String select = listBox1.SelectedItem.ToString().Split(' ')[0];
            int index = Companylist_otc.FindIndex(company => company.SecuritiesCompanyCode == select);
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
                        break;
                    }
                    else
                    {
                        Companylists[count].Add(String.Format("tse_{0}.tw", select));
                        break;
                    }
                }
                else if (Companylists.Count == count + 1)
                {
                    Companylists.Add(new List<string>());
                }


            }


            count = 0;
            foreach (var Companylist in Companylists)
            {
                
                
                stock_list = String.Join<String>('|', Companylist);
                url = String.Format("http://mis.twse.com.tw/stock/api/getStockInfo.jsp?ex_ch={0}", stock_list);
                stock_urls[count] = url;
                String ComapnyData = Get(url);
                CompanyMsg = JsonSerializer.Deserialize<CompanyPrices>(ComapnyData);

                index = CompanyMsg.msgArray.FindIndex(company => company.c == select);
                if (index == -1)
                    continue;
                String change_price = "";
                String change_rate = "";
                if (CompanyMsg.msgArray[index].z.Equals("-"))
                {
                    change_price = "-";
                    change_rate = "-";
                    int newRowIdx = dataGridView1.Rows.Add(CompanyMsg.msgArray[index].c, CompanyMsg.msgArray[index].n, CompanyMsg.msgArray[index].z, change_price, change_rate, Time2String(long.Parse(CompanyMsg.msgArray[index].tlong)));

                    dataGridView1.Rows[newRowIdx].Cells["price"].Style.ForeColor = Color.Yellow;
                    dataGridView1.Rows[newRowIdx].Cells["change_price"].Style.ForeColor = Color.Yellow;
                    dataGridView1.Rows[newRowIdx].Cells["change_rate"].Style.ForeColor = Color.Yellow;
                }
                else
                {
                    change_price = (double.Parse(CompanyMsg.msgArray[index].z) - double.Parse(CompanyMsg.msgArray[index].y)).ToString("0.00");
                    change_rate = (CalculatePriceChange(double.Parse(CompanyMsg.msgArray[index].y), double.Parse(CompanyMsg.msgArray[index].z))).ToString("0.00");
                    if (double.Parse(change_price) < 0)
                    {
                        int newRowIdx = dataGridView1.Rows.Add(CompanyMsg.msgArray[index].c, CompanyMsg.msgArray[index].n, CompanyMsg.msgArray[index].z, change_price, change_rate, Time2String(long.Parse(CompanyMsg.msgArray[index].tlong)));

                        dataGridView1.Rows[newRowIdx].Cells["price"].Style.ForeColor = Color.Green;
                        dataGridView1.Rows[newRowIdx].Cells["change_price"].Style.ForeColor = Color.Green;
                        dataGridView1.Rows[newRowIdx].Cells["change_rate"].Style.ForeColor = Color.Green;
                    }
                    else if (double.Parse(change_price) == 0)
                    {
                        int newRowIdx = dataGridView1.Rows.Add(CompanyMsg.msgArray[index].c, CompanyMsg.msgArray[index].n, CompanyMsg.msgArray[index].z, change_price, change_rate, Time2String(long.Parse(CompanyMsg.msgArray[index].tlong)));

                        dataGridView1.Rows[newRowIdx].Cells["price"].Style.ForeColor = Color.Yellow;
                        dataGridView1.Rows[newRowIdx].Cells["change_price"].Style.ForeColor = Color.Yellow;
                        dataGridView1.Rows[newRowIdx].Cells["change_rate"].Style.ForeColor = Color.Yellow;
                    }
                    else
                    {
                        int newRowIdx = dataGridView1.Rows.Add(CompanyMsg.msgArray[index].c, CompanyMsg.msgArray[index].n, CompanyMsg.msgArray[index].z, change_price, change_rate, Time2String(long.Parse(CompanyMsg.msgArray[index].tlong)));

                        dataGridView1.Rows[newRowIdx].Cells["price"].Style.ForeColor = Color.Red;
                        dataGridView1.Rows[newRowIdx].Cells["change_price"].Style.ForeColor = Color.Red;
                        dataGridView1.Rows[newRowIdx].Cells["change_rate"].Style.ForeColor = Color.Red;
                    }
                }
                count++;
            }
            
           
            
            

            
        }
        public async void add_Select(List<List<String>> Companylists, List<String> selectlist)
        {
            int count = 0;
            int index = 0;
            this.Companylists = Companylists;

            count = 0;
            foreach (var Companylist in Companylists)
            {

                await Task.Delay(TimeSpan.FromSeconds(5));
                stock_list = String.Join<String>('|', Companylist);
                url = String.Format("http://mis.twse.com.tw/stock/api/getStockInfo.jsp?ex_ch={0}", stock_list);
                stock_urls[count] = url;
                String ComapnyData = Get(url);
                CompanyMsg = JsonSerializer.Deserialize<CompanyPrices>(ComapnyData);
                foreach(var select in selectlist)
                {
                    index = CompanyMsg.msgArray.FindIndex(company => company.c == select);
                    if(index != -1)
                    {
                        String change_price = "";
                        String change_rate = "";
                        if (CompanyMsg.msgArray[index].z.Equals("-"))
                        {
                            change_price = "-";
                            change_rate = "-";
                            int newRowIdx = dataGridView1.Rows.Add(CompanyMsg.msgArray[index].c, CompanyMsg.msgArray[index].n, CompanyMsg.msgArray[index].z, change_price, change_rate, Time2String(long.Parse(CompanyMsg.msgArray[index].tlong)));

                            dataGridView1.Rows[newRowIdx].Cells["price"].Style.ForeColor = Color.Yellow;
                            dataGridView1.Rows[newRowIdx].Cells["change_price"].Style.ForeColor = Color.Yellow;
                            dataGridView1.Rows[newRowIdx].Cells["change_rate"].Style.ForeColor = Color.Yellow;
                        }
                        else
                        {
                            change_price = (double.Parse(CompanyMsg.msgArray[index].z) - double.Parse(CompanyMsg.msgArray[index].y)).ToString("0.00");
                            change_rate = (CalculatePriceChange(double.Parse(CompanyMsg.msgArray[index].y), double.Parse(CompanyMsg.msgArray[index].z))).ToString("0.00");
                            if (double.Parse(change_price) < 0)
                            {
                                int newRowIdx = dataGridView1.Rows.Add(CompanyMsg.msgArray[index].c, CompanyMsg.msgArray[index].n, CompanyMsg.msgArray[index].z, change_price, change_rate, Time2String(long.Parse(CompanyMsg.msgArray[index].tlong)));

                                dataGridView1.Rows[newRowIdx].Cells["price"].Style.ForeColor = Color.Green;
                                dataGridView1.Rows[newRowIdx].Cells["change_price"].Style.ForeColor = Color.Green;
                                dataGridView1.Rows[newRowIdx].Cells["change_rate"].Style.ForeColor = Color.Green;
                            }
                            else if (double.Parse(change_price) == 0)
                            {
                                int newRowIdx = dataGridView1.Rows.Add(CompanyMsg.msgArray[index].c, CompanyMsg.msgArray[index].n, CompanyMsg.msgArray[index].z, change_price, change_rate, Time2String(long.Parse(CompanyMsg.msgArray[index].tlong)));

                                dataGridView1.Rows[newRowIdx].Cells["price"].Style.ForeColor = Color.Yellow;
                                dataGridView1.Rows[newRowIdx].Cells["change_price"].Style.ForeColor = Color.Yellow;
                                dataGridView1.Rows[newRowIdx].Cells["change_rate"].Style.ForeColor = Color.Yellow;
                            }
                            else
                            {
                                int newRowIdx = dataGridView1.Rows.Add(CompanyMsg.msgArray[index].c, CompanyMsg.msgArray[index].n, CompanyMsg.msgArray[index].z, change_price, change_rate, Time2String(long.Parse(CompanyMsg.msgArray[index].tlong)));

                                dataGridView1.Rows[newRowIdx].Cells["price"].Style.ForeColor = Color.Red;
                                dataGridView1.Rows[newRowIdx].Cells["change_price"].Style.ForeColor = Color.Red;
                                dataGridView1.Rows[newRowIdx].Cells["change_rate"].Style.ForeColor = Color.Red;
                            }
                        }
                    }
                    
                }
                    
                if (index == -1)
                {
                    count++;
                    continue;
                }                  
                
                count++;
            }
            




        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = textBox1.Text.Trim().ToLower(); // 获取搜索文本并转换为小写

            // 清除之前的搜索结果
            listBox1.ClearSelected();

            // 遍历 ListBox 中的每一项，查找匹配的项
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                string itemText = listBox1.Items[i].ToString().ToLower();

                if (itemText.ToString().Split(' ')[0].Contains(searchText))
                {
                    listBox1.SetSelected(i, true); // 选中匹配的项
                }
                
            }
        }
        // 搜索文本框按下回车键事件处理程序
        private void textBoxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(sender, e); // 模拟点击搜索按钮
                e.Handled = true;
                e.SuppressKeyPress = true; // 防止回车键产生默认操作
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form5 f1= new Form5(this);
            f1.FormClosing += new FormClosingEventHandler(f_FormClosing);
            f1.Show();
        }

        private void Set_Companylists(List<List<String>> Companylists)
        {
            this.Companylists = Companylists;
        }
        public List<List<String>> Get_Companylists()
        {
            return Companylists;
        }
    }
    
    
    public class Company_tse
    {
        [JsonPropertyName("公司簡稱")] public string name { get; set; }
        [JsonPropertyName("公司代號")] public string scode { get; set; }
    }
    public class MsgArray
    {
        public string tv { get; set; }
        public string ps { get; set; }
        public string pz { get; set; }
        public string bp { get; set; }
        public string a { get; set; }
        public string b { get; set; }
        public string c { get; set; }
        public string d { get; set; }
        public string ch { get; set; }
        public string tlong { get; set; }
        public string f { get; set; }
        public string ip { get; set; }
        public string g { get; set; }
        public string mt { get; set; }
        public string h { get; set; }
        public string i { get; set; }
        public string it { get; set; }
        public string l { get; set; }
        public string n { get; set; }
        public string o { get; set; }
        public string p { get; set; }
        public string ex { get; set; }
        public string s { get; set; }
        public string t { get; set; }
        public string u { get; set; }
        public string v { get; set; }
        public string w { get; set; }
        public string nf { get; set; }
        public string y { get; set; }
        public string z { get; set; }
        public string ts { get; set; }
    }

    public class QueryTime
    {
        public string sysDate { get; set; }
        public int stockInfoItem { get; set; }
        public int stockInfo { get; set; }
        public string sessionStr { get; set; }
        public string sysTime { get; set; }
        public bool showChart { get; set; }
        public int sessionFromTime { get; set; }
        public int sessionLatestTime { get; set; }
    }

    public class CompanyPrices
    {
        public List<MsgArray> msgArray { get; set; }
        public string referer { get; set; }
        public int userDelay { get; set; }
        public string rtcode { get; set; }
        public QueryTime queryTime { get; set; }
        public string rtmessage { get; set; }
    }

    public class CompanyEPS
    {
        [JsonPropertyName("出表日期")]
        public string  date { get; set; }

        [JsonPropertyName("年度")]
        public string  year { get; set; }

        [JsonPropertyName("季別")]
        public string  season { get; set; }

        [JsonPropertyName("公司代號")]
        public string  code { get; set; }

        [JsonPropertyName("公司名稱")]
        public string  name { get; set; }

        [JsonPropertyName("產業別")]
        public string category { get; set; }

        [JsonPropertyName("基本每股盈餘(元)")]
        public string eps { get; set; }

        [JsonPropertyName("普通股每股面額")]
        public string per_val { get; set; }

        [JsonPropertyName("營業收入")]
        public string revenue { get; set; }

        [JsonPropertyName("營業利益")]
        public string op_income { get; set; }

        [JsonPropertyName("營業外收入及支出")]
        public string other_income { get; set; }

        [JsonPropertyName("稅後淨利")]
        public string net_income { get; set; }
    }
    public class Company_otc
    {
        [JsonPropertyName("Date")]
        public string Date { get; set; }

        [JsonPropertyName("SecuritiesCompanyCode")]
        public string SecuritiesCompanyCode { get; set; }

        [JsonPropertyName("CompanyName")]
        public string CompanyName { get; set; }

        [JsonPropertyName("公司簡稱")]
        public string  Name { get; set; }

        [JsonPropertyName("外國企業註冊地國")]
        public string Registration { get; set; }

        [JsonPropertyName("SecuritiesIndustryCode")]
        public string SecuritiesIndustryCode { get; set; }

        [JsonPropertyName("Address")]
        public string Address { get; set; }

        [JsonPropertyName("UnifiedBusinessNo.")]
        public string UnifiedBusinessNo { get; set; }

        [JsonPropertyName("Chairman")]
        public string Chairman { get; set; }

        [JsonPropertyName("GeneralManager")]
        public string GeneralManager { get; set; }

        [JsonPropertyName("Spokesman")]
        public string Spokesman { get; set; }

        [JsonPropertyName("TitleOfSpokesman")]
        public string TitleOfSpokesman { get; set; }

        [JsonPropertyName("DeputySpokesperson")]
        public string DeputySpokesperson { get; set; }

        [JsonPropertyName("Telephone")]
        public string Telephone { get; set; }

        [JsonPropertyName("DateOfIncorporation")]
        public string DateOfIncorporation { get; set; }

        [JsonPropertyName("DateOfListing")]
        public string DateOfListing { get; set; }

        [JsonPropertyName("ParValueOfCommonStock")]
        public string ParValueOfCommonStock { get; set; }

        [JsonPropertyName("Paidin.Capital.NTDollars")]
        public string PaidinCapitalNTDollars { get; set; }

        [JsonPropertyName("PrivateStock.shares")]
        public string PrivateStockshares { get; set; }

        [JsonPropertyName("PreferredStock.shares")]
        public string PreferredStockshares { get; set; }

        [JsonPropertyName("PreparationOfFinancialReportType")]
        public string PreparationOfFinancialReportType { get; set; }

        [JsonPropertyName("StockTransferAgent")]
        public string StockTransferAgent { get; set; }

        [JsonPropertyName("StockTransferAgentTelephone")]
        public string StockTransferAgentTelephone { get; set; }

        [JsonPropertyName("StockTransferAgentAddress")]
        public string StockTransferAgentAddress { get; set; }

        [JsonPropertyName("AccountingFirm")]
        public string AccountingFirm { get; set; }

        [JsonPropertyName("CPA.CharteredPublicAccountant.First")]
        public string CPACharteredPublicAccountantFirst { get; set; }

        [JsonPropertyName("CPA.CharteredPublicAccountant.Second")]
        public string CPACharteredPublicAccountantSecond { get; set; }

        [JsonPropertyName("Symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("Fax")]
        public string Fax { get; set; }

        [JsonPropertyName("EmailAddress")]
        public string EmailAddress { get; set; }

        [JsonPropertyName("WebAddress")]
        public string WebAddress { get; set; }
    }
    
}
