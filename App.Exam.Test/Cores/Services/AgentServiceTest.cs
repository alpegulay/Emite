using App.Exam.Emite.Api.Core.Models;
using App.Exam.Emite.Data;
using App.Exam.Test.Setup;
using Xunit;

namespace App.Exam.Test.Cores.Services
{
    public class AgentServiceTest : BaseContextSetup
    {
        [Trait("Category", "Unit")]
        [Fact]
        public async Task ShouldGetAll()
        {
            using (var context = new DataContext(_options, null))
            {
               AgentSetup.SetupData(context);

                var service = AgentSetup.SetupService(context);

                var data = await service.GetAllAsync();
                Assert.NotEmpty(data);
            }
        }

        [Trait("Category", "Unit")]
        [Fact]
        public async Task ShouldGetById()
        {
            using (var context = new DataContext(_options, null))
            {
                AgentSetup.SetupData(context);
                var service = AgentSetup.SetupService(context);

                var data = await service.GetByIdAsync(1);
                Assert.NotNull(data);
            }
        }

        [Trait("Category", "Unit")]
        [Fact]
        public async Task ShouldThrowIfNotFoundOnGetById()
        {
            using (var context = new DataContext(_options, null))
            {
                AgentSetup.SetupData(context);
                var service = AgentSetup.SetupService(context);

                await Assert.ThrowsAnyAsync<Exception>(async () => await service.GetByIdAsync(0));
            }
        }

        [Trait("Category", "Unit")]
        [Fact]
        public async Task ShouldEnsureNewObject()
        {
            using (var context = new DataContext(_options, null))
            {

                var service = AgentSetup.SetupService(context);

                var model = new AgentModel()
                {
                    Name = "Test Name",
                    Email = "test@gmail.com"
                };

                model = await service.EnsureAsync(0, model);

                // ReSharper disable once PossibleNullReferenceException
                var id = context.Agents.FirstOrDefault().ID;

                model = await service.GetByIdAsync(id);

                Assert.Equal("Test Name", model.Name);
            }
        }

        [Trait("Category", "Unit")]
        [Fact]
        public async Task ShouldEnsureExistingObject()
        {
            using (var context = new DataContext(_options, null))
            {

                var service = AgentSetup.SetupService(context);

                var model = new AgentModel()
                {
                    Name = "Test Name",
                    Email = "test@gmail.com"
                };

                model = await service.EnsureAsync(0, model);

                model = await service.GetByIdAsync(1);

                Assert.Equal("Test Name", model.Name);

                var updatedModel = new AgentModel()
                {
                    Id = 1,
                    Name = "Test Name",
                    Email = "test@gmail.com"
                };
                model = await service.EnsureAsync(0, updatedModel);

                Assert.NotEqual("Test Name", model.Name);
                Assert.Equal("Updated Name", model.Name);
            }
        }

        [Trait("Category", "Unit")]
        [Fact]
        public async Task ShouldDeleteExistingObject()
        {
            using (var context = new DataContext(_options, null))
            {
                AgentSetup.SetupData(context);

                var service = AgentSetup.SetupService(context);

                var model = await service.GetAllAsync();
                Assert.NotEmpty(model);

                var id = model.First().Id;
                await service.DeleteAsync(0, id);

                await Assert.ThrowsAnyAsync<Exception>(async () => await service.GetByIdAsync(id));
            }
        }
    }
}
