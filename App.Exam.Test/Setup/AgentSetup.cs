using App.Exam.Emite.Api.Core.Interfaces.Models;
using App.Exam.Emite.Api.Core.Interfaces.Services;
using App.Exam.Emite.Api.Core.Models;
using App.Exam.Emite.Api.Core.Services;
using App.Exam.Emite.Api.Core.Validators;
using App.Exam.Emite.Data;
using App.Exam.Emite.Data.Entities;
using App.Exam.Emite.Data.Entities.Enums;
using App.Exam.Emite.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Exam.Test.Setup
{
    public class AgentSetup
    {
        public static void SetupData(DataContext context)
        {
            for (var x = 1; x <= 10; x++)
            {
                context.Agents.Add(new Agent()
                {
                    ID = x,
                    Name = $"Name{x}",
                    Email = $"Email{x}",
                    PhoneExtension = $"PhoneExtension{x}",
                    Status = AgentStatus.Available,

                    EntityStatus = (int)EntityStatus.Active,
                    CreatedByUserId = 1
                });
            }
            context.SaveChanges();
        }
        public static IAgentService SetupService(DataContext context)
        {
            var agentRepository = new AgentRepository(context);
            var target = new AgentService(agentRepository);
            return target;
        }
        public static IValidator<AgentModel> SetupValidator(DataContext context)
        {
            return new AgentModelValidator(context);
        }
    }
}
