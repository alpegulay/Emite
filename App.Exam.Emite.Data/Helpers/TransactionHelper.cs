using App.Exam.Emite.Data.Interfaces.Helpers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Exam.Emite.Data.Helpers
{
    public class TransactionHelper : ITransactionHelper
    {
        private readonly ILogger<TransactionHelper> _logger;

        public TransactionHelper(ILogger<TransactionHelper> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public Task StartTransaction(DataContext context, Func<Task> callback, IsolationLevel isolationLevel = IsolationLevel.Serializable)
        {
            throw new NotImplementedException();
        }
    }
}
