using App.Exam.Emite.Api.Core.Interfaces.Models;
using App.Exam.Emite.Api.Core.Interfaces.Services;
using App.Exam.Emite.Api.Core.Models;
using App.Exam.Emite.Api.Core.Services;
using App.Exam.Emite.Api.Core.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace App.Exam.Emite.Api.Core
{
    public static class ServiceExtensions
    {
        // services
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {   
            //Services
            services.AddScoped<IAgentService, AgentService>();
            services.AddScoped<ICallService, CallService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ITicketService, TicketService>();

            //Validators
            services.AddScoped<IValidator<AgentModel>, AgentModelValidator>();
            services.AddScoped<IValidator<CallModel>, CallModelValidator>();
            services.AddScoped<IValidator<CustomerModel>, CustomerModelValidator>();
            services.AddScoped<IValidator<TicketModel>, TicketModelValidator>();

            return services;
        }
    }
}
