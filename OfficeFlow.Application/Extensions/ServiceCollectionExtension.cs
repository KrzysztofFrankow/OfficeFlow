using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OfficeFlow.Application.AutoMapper;
using OfficeFlow.Application.Users.Commands.CreateUsers;

namespace OfficeFlow.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateUsersCommand));

            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddValidatorsFromAssemblyContaining<CreateUsersCommandValidator>()
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
        }
    }
}
