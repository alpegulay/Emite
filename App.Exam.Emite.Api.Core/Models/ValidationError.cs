using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Exam.Emite.Api.Core.Models
{
    public class ValidationError
    {
        public string Key { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return $"{Key} | {Message}";
        }
    }
}
