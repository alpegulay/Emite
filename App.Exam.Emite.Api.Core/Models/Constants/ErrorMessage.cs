using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Exam.Emite.Data.Constants
{
    public class ErrorMessage
    {
        public const string AlreadyExists = "{0} already exists.";
        public const string DoesNotExist = "{0} does not exists.";
        public const string IsRequired = "This field is required.";
        public const string MaxLength = "This field can only have a maximum of {0} characters.";
    }
}
