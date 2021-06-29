using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.InterFace
{
    public interface IGetData
    {
        Task<Num> GetText(string text); // 回傳所有資料        
    }
}
