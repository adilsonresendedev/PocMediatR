using MediatR;

namespace PocMediatR.Application.Features.Queries.Get
{
    public class GetPriceTypeCommand : IRequest<GetPriceTypeCommandResponse>
    {
        public Guid Id { get; set; }
    }
}
