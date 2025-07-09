using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace PocMediatR.Application.Tests.Features
{
    [ExcludeFromCodeCoverage]
    public abstract class HandlerTestBase<TRequest, THandler, TResponse> where TRequest : IRequest<TResponse>
    {
        protected readonly object[] parameters;
        protected readonly HandlerBase<TRequest, TResponse> handler;

        protected HandlerTestBase()
        {
            var cultureInfo = new CultureInfo("en");
            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;

            parameters = GetMockedParameters().ToArray();
            handler = (HandlerBase<TRequest, TResponse>)Activator.CreateInstance(typeof(THandler), parameters)!;
        }

        public static IEnumerable<object> GetMockedParameters()
        {
            var constructorHandler = typeof(THandler).GetConstructors().First();
            var constructorParameters = constructorHandler.GetParameters();

            foreach ( var parameter in constructorParameters)
            {
                yield return Substitute.For([parameter.ParameterType], null);
            }
        }

        protected TResponse CallHandler(TRequest request)
        {
            return Task.Run(() => handler.Handle(request, CancellationToken.None)).GetAwaiter().GetResult();
        }

        protected TParamType GetParam<TParamType>()
        {
            return (TParamType)parameters.FirstOrDefault(p => p is TParamType)!;
        }
    }
}
