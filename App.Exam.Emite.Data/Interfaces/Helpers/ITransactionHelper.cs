using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Exam.Emite.Data.Interfaces.Helpers
{
    public interface ITransactionHelper
    {
        Task StartTransaction(DataContext context, Func<Task> callback, IsolationLevel isolationLevel = IsolationLevel.Serializable);
    }
}
