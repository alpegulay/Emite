using App.Exam.Emite.Data.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Exam.Emite.Data.Helpers
{
    public class AuditTrailHelper
    {
        public static void SetCreated(int userId, IAuditable entity)
        {
            entity.CreatedByUserId = userId;
            entity.CreatedDate = DateTime.UtcNow;
            SetUpdated(userId, entity);
        }
        public static void SetUpdated(int userId, IAuditable entity)
        {
            entity.UpdatedByUserId = userId;
            entity.UpdatedDate = DateTime.UtcNow;
        }
    }
}
