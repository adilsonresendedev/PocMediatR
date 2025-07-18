using System.Diagnostics.CodeAnalysis;

namespace PocMediatR.Application.Tests.Events
{
    class EventHandlerTestBase
    {
    }
    [ExcludeFromCodeCoverage]
    public abstract class EventHandlerTestBase<TEvent, THandler>
    where TEvent : INotification
    where THandler : class, INotificationHandler<TEvent>
    {
        protected readonly object[] parameters;
        protected readonly THandler handler;

        protected EventHandlerTestBase()
        {
            parameters = GetMockedParameters().ToArray();
            handler = (THandler)Activator.CreateInstance(typeof(THandler), parameters)!;
        }

        public static IEnumerable<object> GetMockedParameters()
        {
            var constructor = typeof(THandler).GetConstructors().First();
            var constructorParams = constructor.GetParameters();

            foreach (var param in constructorParams)
            {
                yield return Substitute.For(new[] { param.ParameterType }, null);
            }
        }

        protected Task HandleEventAsync(TEvent notification, CancellationToken cancellationToken = default)
        {
            return handler.Handle(notification, cancellationToken);
        }

        protected TParamType GetParam<TParamType>()
        {
            return (TParamType)parameters.FirstOrDefault(p => p is TParamType)!;
        }
    }
}
