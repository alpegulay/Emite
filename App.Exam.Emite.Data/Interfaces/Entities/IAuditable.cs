using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Exam.Emite.Data.Interfaces.Entities
{
    public interface IAuditable
    {

        DateTime? CreatedDate { get; set; }

        int CreatedByUserId { get; set; }

        DateTime? UpdatedDate { get; set; }

        int UpdatedByUserId { get; set; }
    }
}
