using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Exam.Emite.Data.Interfaces.Helpers
{
    public interface IConnectionHelper
    {
        Task<DbConnection> GetConnectionStringAsync();
    }
}
