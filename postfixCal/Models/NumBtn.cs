namespace postfixCal.Models
{
    /// <summary>
    /// 提供物件存放按鈕的值
    /// </summary>
    class NumBtn : MainBtn
    {
        public string Text { get; set; }
        public override string ActionMethod()
        {
            return Text;
        }
    }
}
