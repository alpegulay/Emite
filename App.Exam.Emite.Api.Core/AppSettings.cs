using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace App.Exam.Emite.Api.Core
{
    public  class AppSettings
    {
        public static MemoryCacheEntryOptions GetCacheOption()
        {
            return  new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(30));
        }
    }
}
