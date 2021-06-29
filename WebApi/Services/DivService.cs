using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.InterFace;
using WebApi.Models;

namespace WebApi.Services
{
    public class DivService : IDiv

    {
        public Task<Div> GetDiv()
        {
            return Task.Run(() =>
            {
                Div result = new Div();
                result.Text = "/";

                return result;
            });
        }
    }
}
