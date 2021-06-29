using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.InterFace;

namespace WebApi.Services
{
    public class ToBlankService : IClear
    {
        public Task<Blank> GetBlank(string text)
        {
            return Task.Run(() =>
            {
                Blank result = new Blank();
                result.Text = string.Empty;

                return result;
            });
        }
    }
}
