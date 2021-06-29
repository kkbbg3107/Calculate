using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.InterFace
{
    /// <summary>
    /// 提供返回 string,empty的介面
    /// </summary>
    public interface IClear
    {
        Task<Blank> GetBlank(string text); // 回傳string.empty
    }
}
