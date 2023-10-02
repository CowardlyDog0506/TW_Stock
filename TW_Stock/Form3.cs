using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;
using System.Linq;
using System.Drawing;
namespace TW_Stock
{
    
    public partial class Form3 : Form
    {
        Form2 form2 = null;
        String company;
        public Form3(String company, Form2 form2)
        {
            this.company = company;
            this.form2 = form2;
            InitializeComponent();
        }

       
        public static string Get(string url)
        {
            var web = new WebClient();
            return web.DownloadString(url);
        }
        Data_tse company_data_tse = new Data_tse();
        Data_otc company_data_otc = new Data_otc();
        private void Form3_Load(object sender, EventArgs e)
        {
            String url = "";
            int index = form2.Get_form1().Get_Companylist_otc().FindIndex(company => company.SecuritiesCompanyCode == this.company);
            if (index == -1)
            {
                url = String.Format("http://www.twse.com.tw/exchangeReport/BWIBBU?&date={0}&stockNo={1}", "20230830", company);
                String Comapnyjsom = Get(url);
                company_data_tse = JsonSerializer.Deserialize<Data_tse>(Comapnyjsom);
                foreach (var data in company_data_tse.data)
                {
                    int newRowIdx = dataGridView1.Rows.Add();

                    for (int i = 0; i < data.Count(); i++)
                    {
                        dataGridView1.Rows[newRowIdx].Cells[i].Value = data[i];
                    }
                }
            }
            else
            {
                url = String.Format("https://www.tpex.org.tw/web/stock/aftertrading/peratio_stk/pera_result.php?l=zh-tw&d={0}&stkno={1}", "112/08", company);
                String Comapnyjsom = Get(url);
                company_data_otc = JsonSerializer.Deserialize<Data_otc>(Comapnyjsom);
                foreach (var data in company_data_otc.aaData)
                {
                    int newRowIdx = dataGridView1.Rows.Add();

                    for (int i = 0; i < data.Count(); i++)
                    {
                        dataGridView1.Rows[newRowIdx].Cells[i].Value = data[i];
                    }
                }
            }
            
            
            

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public class Data_tse
        {
            public string stat { get; set; }
            public string date { get; set; }
            public string title { get; set; }
            public List<string> fields { get; set; }
            public List<List<object>> data { get; set; }
            public int total { get; set; }
        }
        public class Data_otc
        {
            [JsonPropertyName("stkNo")]
            public string stkNo { get; set; }

            [JsonPropertyName("stkName")]
            public string stkName { get; set; }

            [JsonPropertyName("reportDate")]
            public string reportDate { get; set; }

            [JsonPropertyName("iTotalRecords")]
            public int iTotalRecords { get; set; }

            [JsonPropertyName("aaData")]
            public List<List<string>> aaData { get; set; }

            [JsonPropertyName("stkDivYear1")]
            public int stkDivYear1 { get; set; }

            [JsonPropertyName("stkDivYear2")]
            public int stkDivYear2 { get; set; }

            [JsonPropertyName("stkDivYear3")]
            public int stkDivYear3 { get; set; }

            [JsonPropertyName("stkDivVal1")]
            public string stkDivVal1 { get; set; }

            [JsonPropertyName("stkDivVal2")]
            public string stkDivVal2 { get; set; }

            [JsonPropertyName("stkDivVal3")]
            public string stkDivVal3 { get; set; }
        }
    }
}
