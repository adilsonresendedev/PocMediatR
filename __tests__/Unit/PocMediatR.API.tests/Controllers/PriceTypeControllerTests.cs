using PocMediatR.Application.Features.Commands.CreatePriceType;

namespace PocMediatR.API.Tests.Controllers
{
    public class PriceTypeControllerTests : ControllerTestBase<PriceTypesController>
    {
        [Fact]
        public async Task Should_call_post_method()
        {
            var request = Builder<CreatePriceTypeCommand>
                .CreateNew()
                .Build();

            var response = await controller.Post(request);

            await mediator.Received().Send(Arg.Any<CreatePriceTypeCommand>());

            response.ShouldBeOfType<CreatedResult>();
        }
    }
}
