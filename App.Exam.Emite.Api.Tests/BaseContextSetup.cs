using App.Exam.Emite.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Exam.Emite.Api.Core.Tests
{
    public class BaseContextSetup
    {
        protected readonly DbContextOptions<DataContext> _options;

        public BaseContextSetup()
        {
            _options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }
    }
}
