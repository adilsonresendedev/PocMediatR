using PocMediatR.Application.Events.PriceTypeChanged;
using PocMediatR.Domain.Events;
using PocMediatR.Infra.MessageBus;

namespace PocMediatR.Application.Tests.Events.PriceTypeChanged
{
    public class PriceTypeChangedEventHandlerTests
     : EventHandlerTestBase<EntityChangedDomainEvent<PriceType>, PriceTypeChangedEventHandler>
    {
        [Fact]
        public async Task Should_publish_message_to_correct_queue()
        {
            var priceType = PriceType.Create("Test");
            var @event = new EntityChangedDomainEvent<PriceType>(priceType, "Create");
            var messageBus = GetParam<IPublisherMessageBus>();

            await HandleEventAsync(@event);

            await messageBus.Received(1).PublishAsync(Arg.Any<string>(), Arg.Any<object>(), Arg.Any<CancellationToken>());
        }
    }
}
