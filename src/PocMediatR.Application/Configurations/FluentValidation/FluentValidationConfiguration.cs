using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PocMediatR.Application.Features;

namespace PocMediatR.Application.Configurations.FluentValidation
{
    public static class FluentValidationConfiguration
    {
        public static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            var type = typeof(AbstractValidator<>);
            var assembly = typeof(HandlerBase<,>).Assembly;
            (from classes in assembly.GetTypes()
             where classes.BaseType != null
             && classes.BaseType!.IsAbstract
             && classes.BaseType!.Name.Contains(type.Name)
             select classes)
             .ToList()
             .ForEach(delegate (Type e)
             {
                 services.AddTransient(e.BaseType!, e);
             });

            return services;
        }
    }
}
