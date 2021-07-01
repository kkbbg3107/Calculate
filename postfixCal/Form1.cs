using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using postfixCal.Models;

namespace postfixCal
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// WebApi地址
        /// </summary>
        private static readonly string Url = "https://localhost:5001/api/Calculate";

        /// <summary>
        /// 數字0-9 運算子呼叫
        /// </summary>
        private static readonly string Url2 = "https://localhost:5001/api/text/";

        /// <summary>
        /// C的呼叫 清空TEXTBOX
        /// </summary>
        private static readonly string Url3 = "https://localhost:5001/api/Blank/";

        /// <summary>
        /// "."的呼叫
        /// </summary>
        private static readonly string Url4 = "https://localhost:5001/api/Dot/";

        /// <summary>
        /// "/"的呼叫
        /// </summary>
        private static readonly string Url5 = "https://localhost:5001/api/Div/";
        public Form1()
        {
            this.InitializeComponent();
        }     

        /// <summary>
        /// 點擊對應按鈕textbox顯示v相應數值
        /// </summary>
        /// <param name="sender">引發事件的物件</param>
        /// <param name="e">事件的額外細項</param>
        private async void ButtonClick(object sender, EventArgs e)
        {            
            Button btn = sender as Button;
            
            if (textBox1.Text == string.Empty)
            {
                btnsub.Enabled = true;
                btndiv.Enabled = true;
                btnmul.Enabled = true;
                btnplus.Enabled = true;
            }

            // 呼叫webApi post方法 
            if (btn.Text == "api")
            {               
                HttpClient client = new HttpClient();

                string text = textBox1.Text;

                string json = JsonConvert.SerializeObject(text);  // 把post的參數值轉成json

                HttpContent contentPost = new StringContent(json, Encoding.UTF8, "application/json"); // 定義json內容

                HttpResponseMessage response = client.PostAsync(Url, contentPost).Result; // webapi 回傳的物件

                var ans = response.Content.ReadAsStringAsync().Result;
                if(textBox1.Text != string.Empty)
                {
                    var result = JsonConvert.DeserializeObject(ans);
                    textBox2.Text = result.ToString();
                }                                             
            }
            else if (btn.Text == "C") // 呼叫web API get方法
            {                                               
                using (HttpClient client = new HttpClient())
                {
                    var url = Url3 + btn.Text;
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    var responseBody = await response.Content.ReadAsStringAsync();
                    var resultObj = JsonConvert.DeserializeObject<NumBtn>(responseBody);
                    textBox1.Text = resultObj.text;
                }
            }        
            else if (btn.Text == ".") // 呼叫web API get方法
            {
                using (HttpClient client = new HttpClient())
                {
                    var url = Url4;
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    var responseBody = await response.Content.ReadAsStringAsync();
                    var resultObj = JsonConvert.DeserializeObject<NumBtn>(responseBody);
                    textBox1.Text += resultObj.text;
                }
            }
            else if (btn.Text == "/") // 呼叫web api get 方法 配合url + "/" 不合http規範
            {
                using (HttpClient client = new HttpClient())
                {
                    var url = Url5;
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    var responseBody = await response.Content.ReadAsStringAsync();
                    var resultObj = JsonConvert.DeserializeObject<NumBtn>(responseBody);
                    textBox1.Text += resultObj.text;
                }
            }
            else // 呼叫web API get方法
            {
                using (HttpClient client = new HttpClient())
                {
                    var url = Url2 + btn.Text;
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    var responseBody = await response.Content.ReadAsStringAsync();                    
                    var resultObj = JsonConvert.DeserializeObject<NumBtn>(responseBody);
                    textBox1.Text += resultObj.text;
                }
            }
        }        
    }
}