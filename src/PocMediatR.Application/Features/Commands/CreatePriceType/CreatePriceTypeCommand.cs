using MediatR;

namespace PocMediatR.Application.Features.Commands.CreatePriceType
{
    public class CreatePriceTypeCommand : IRequest<CreatePriceTypeCommandResponse>
    {
        public string Description { get; set; }
    }
}
