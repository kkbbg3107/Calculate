using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.InterFace
{
    public interface IDot
    {
        Task<Dot> GetDot(); // 回傳"."
    }
}
