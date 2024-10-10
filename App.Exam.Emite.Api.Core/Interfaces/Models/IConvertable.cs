using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Exam.Emite.Api.Core.Interfaces.Models
{
    public interface IConvertable<TEntity>
    {
        TEntity ToEntity(TEntity entity);
        void ToModel(TEntity entity);
    }
}
