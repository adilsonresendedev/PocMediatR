using MediatR;

namespace PocMediatR.Application.Features.Commands
{
    public class CreatePriceTypeCommand : IRequest<CreatePriceTypeCommandResponse>
    {
        public string Description { get; set; }
    }
}
