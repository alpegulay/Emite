using App.Exam.Emite.Data;
using Microsoft.EntityFrameworkCore;

namespace App.Exam.Test.Setup
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
