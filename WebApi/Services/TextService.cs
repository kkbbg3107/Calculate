using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.InterFace;

namespace WebApi.Services
{
    /// <summary>
    /// 按鈕按下後對數值的處理
    /// </summary>
    public class TextService : IGetData
    {
        public Task<Num> GetText(string text)
        {
            return Task.Run(() =>
            {
                Num result = new Num();
                result.Text = text;

                return result;
            });       
        }       
    }
}
