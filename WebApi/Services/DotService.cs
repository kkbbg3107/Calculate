using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.InterFace;
using WebApi.Models;

namespace WebApi.Services
{
    public class DotService : IDot
    {
        public Task<Dot> GetDot()
        {      
            return Task.Run(() =>
            {
                Dot result = new Dot();                
                result.Text = ".";

                return result;
            });
        }
    }
}
