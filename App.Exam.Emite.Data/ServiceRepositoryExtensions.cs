using App.Exam.Emite.Data;
using App.Exam.Emite.Data.Helpers;
using App.Exam.Emite.Data.Interfaces.Helpers;
using App.Exam.Emite.Data.Interfaces.Repositiories;
using App.Exam.Emite.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace App.Exam.Emite.Api.Core
{
    public static class ServiceRepositoryExtensions
    {
        // services
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase(databaseName: "emite"));

            services.AddSingleton<IMemoryCacheHelper, MemoryCacheHelper>();

          //  services.AddScoped<IConnectionHelper, PostgresConnectionHelper>();

            //Repositories
            services.AddScoped<IAgentRepository, AgentRepository>();
            services.AddScoped<ICallRepository, CallRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();

            //transaction helper
            services.AddSingleton<ITransactionHelper, TransactionHelper>();

            return services;
        }
    }
}
