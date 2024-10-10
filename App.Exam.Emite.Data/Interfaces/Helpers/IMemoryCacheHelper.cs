using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Exam.Emite.Data.Interfaces.Helpers
{
    public interface IMemoryCacheHelper
    {
        Task<T> GetObjectFromCache<T>(string cacheItemName, double cacheTimeInMinutes, Func<Task<T>> objectSetterCallback);
    }
}
