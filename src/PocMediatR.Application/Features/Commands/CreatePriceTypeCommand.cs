using MediatR;

namespace PocMediatR.Application.Features.Commands
{
    public class CreatePriceTypeCommand : IRequest<Unit>
    {
        public string Description { get; set; }
    }
}
