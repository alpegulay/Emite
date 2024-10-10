using App.Exam.Emite.Api.Core.Models;
using App.Exam.Emite.Data;
using App.Exam.Test.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace App.Exam.Test.Cores.Validator
{
    public class AgentModelValidatorTest : BaseContextSetup
    {
        [Trait("Category", "Unit")]
        [Fact]
        public async Task ShouldValidateDuplicateName()
        {
            using (var context = new DataContext(_options, null))
            {
               AgentSetup.SetupData(context);

                var service = AgentSetup.SetupService(context);
                var validator = AgentSetup.SetupValidator(context);

                var data = new AgentModel(context.Agents.FirstOrDefault());

                Assert.NotNull(data);

                var model = await service.GetByIdAsync(data.Id);

                model.Id = new int();
                var isValid = await validator.IsValidAsync(model);

                Assert.False(isValid);

            }
        }

        [Trait("Category", "Unit")]
        [Fact]
        public async Task ShouldValidateEmptyName()
        {
            using (var context = new DataContext(_options, null))
            {
                AgentSetup.SetupData(context);

                var service = AgentSetup.SetupService(context);
                var validator = AgentSetup.SetupValidator(context);

                var data = new AgentModel(context.Agents.FirstOrDefault());

                Assert.NotNull(data);

                var model = await service.GetByIdAsync(data.Id);

                model.Name = "";
                var isValid = await validator.IsValidAsync(model);

                Assert.False(isValid);

            }
        }
    }
}
