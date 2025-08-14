using Azure.Core;
using MediatR;
using PocMediatR.Application.Features.Commands.CreatePriceType;
using PocMediatR.Domain.Events;

namespace PocMediatR.Application.Tests.Features.Commands.CreatePriceType
{
    public class CreatePriceTypeCommandHandlerTests : HandlerTestBase<CreatePriceTypeCommand, CreatePriceTypeCommandHandler, CreatePriceTypeCommandResponse>
    {
        private IPocMediatrWriteContext Context => GetParam<IPocMediatrWriteContext>();
        private IMediator Mediator => GetParam<IMediator>();
        private CreatePriceTypeCommand defaultRequest;
        public CreatePriceTypeCommandHandlerTests()
        {
            defaultRequest = Builder<CreatePriceTypeCommand>
                .CreateNew()
                .Build();
        }

        [Fact]
        public void Should_throw_if_domain_is_invalid()
        {
            defaultRequest.Description = string.Empty;

            Should.Throw<AggregateException>(() => CallHandler(defaultRequest));
        }

        [Fact]
        public void Should_throw_when_no_data_is_inserted()
        {
            Context.SaveChangesAsync(Arg.Any<CancellationToken>())
                .Returns(Task.FromResult(0));

            Should.Throw<InvalidOperationException>(() => CallHandler(defaultRequest));
        }

        [Fact]
        public void Should_insert_correctly()
        {
            Context.SaveChangesAsync(Arg.Any<CancellationToken>())
               .Returns(Task.FromResult(1));

            var result = CallHandler(defaultRequest);

            Context.PriceTypes.Received(1).AddAsync(Arg.Any<PriceType>(), Arg.Any<CancellationToken>());

            result.Id.ShouldNotBe(Guid.Empty);
            result.Description.ShouldBe(defaultRequest.Description);
        }

    }
}
