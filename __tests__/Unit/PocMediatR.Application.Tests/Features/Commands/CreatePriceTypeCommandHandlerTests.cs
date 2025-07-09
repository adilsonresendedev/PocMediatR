namespace PocMediatR.Application.Tests.Features.Commands
{
    public class CreatePriceTypeCommandHandlerTests : HandlerTestBase<CreatePriceTypeCommand, CreatePriceTypeCommandHandler, CreatePriceTypeCommandResponse>
    {
        private IPocMediatRContext Context => GetParam<IPocMediatRContext>();
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
        public void Should_insert_correctly()
        {
            var result = CallHandler(defaultRequest);

            Context.PriceTypes.Received(1).AddAsync(Arg.Any<PriceType>(), Arg.Any<CancellationToken>());
            result.Id.ShouldNotBe(Guid.Empty);
            result.Description.ShouldBe(defaultRequest.Description);
        }
    }
}
