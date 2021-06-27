using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace postfixCal
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// WebApi地址
        /// </summary>
        private static readonly string Url = "https://localhost:5001/api/Calculate";

        public Form1()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 點擊對應按鈕textbox顯示v相應數值
        /// </summary>
        /// <param name="sender">引發事件的物件</param>
        /// <param name="e">事件的額外細項</param>
        private void ButtonClick(object sender, EventArgs e)
        {            
            Button b = sender as Button;
            if (textBox1.Text == string.Empty)
            {
                btnsub.Enabled = true;
                btndiv.Enabled = true;
                btnmul.Enabled = true;
                btnplus.Enabled = true;
            }
            // 呼叫webApi post方法 
            if (b.Text == "api")
            {               
                HttpClient client = new HttpClient();

                string text = textBox1.Text;

                string json = JsonConvert.SerializeObject(text);  // 把post的參數值轉成json

                HttpContent contentPost = new StringContent(json, Encoding.UTF8, "application/json"); // 定義json內容

                HttpResponseMessage response = client.PostAsync(Url, contentPost).Result; // webapi 回傳的物件

                var ans = response.Content.ReadAsStringAsync().Result;

                var result = JsonConvert.DeserializeObject(ans);
                textBox2.Text = result.ToString();                                
            }
            else if (b.Text == "C")
            {
                textBox1.Text = string.Empty;
            }
            else if (b.Text == "run")
            {
                dobtn d = new dobtn();
                var a = d.PostNumber(textBox1.Text);
                textBox1.Text = a;
            }
            else
            {
                textBox1.Text += b.Text;
            }
        }    
    }
}