using Microsoft.Extensions.DependencyInjection;
using PocMediatR.Application.Features;

namespace PocMediatR.Application.Configurations.Mediator
{
    public static class MediatorConfiguration
    {
        public static IServiceCollection AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(HandlerBase<,>).Assembly));
            return services;
        }
    }
}
