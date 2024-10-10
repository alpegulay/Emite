using App.Exam.Emite.Data.Interfaces.Helpers;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Exam.Emite.Data.Helpers
{
    public class PostgresConnectionHelper : IConnectionHelper
    {
        public async Task<DbConnection> GetConnectionStringAsync()
        {
            var connectionString = Environment.GetEnvironmentVariable("ConnectionString");
            return new NpgsqlConnection(connectionString);
        }
    }
}
