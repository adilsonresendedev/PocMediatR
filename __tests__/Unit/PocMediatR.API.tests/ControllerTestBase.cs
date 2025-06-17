using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System.Text;

namespace PocMediatR.API.Tests
{
    public abstract class ControllerTestBase<TController> where TController : ControllerBase
    {
        private readonly TController controller;
        private readonly IMediator mediator;
        private readonly object[] parameters;

        protected ControllerTestBase()
        {
            parameters = GetMockedParameters().ToArray();
            controller = (TController)Activator.CreateInstance(typeof(TController), parameters)!;
            mediator = GetParam<IMediator>();
            SetDefaultHeaders();
        }

        private static IEnumerable<object> GetMockedParameters()
        {
            var constructor = typeof(TController).GetConstructors().FirstOrDefault();
            var constructorParameters = constructor!.GetParameters();

            foreach (var parameter in constructorParameters)
                yield return Substitute.
                    For([parameter.ParameterType], []);
        }

        private void SetDefaultHeaders()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers.Append("Accept-Language", "pt-BR");
            httpContext.Request.Headers.Append("Authorization", "Bearer bearer");
            var controllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            controller.ControllerContext = controllerContext;
        }

        protected TParamType GetParam<TParamType>()
        {
            return (TParamType)parameters.ToList().Find(p => p is TParamType)!;
        }
    }
}
