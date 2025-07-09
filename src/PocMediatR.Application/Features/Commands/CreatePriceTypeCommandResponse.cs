namespace PocMediatR.Application.Features.Commands
{
    public class CreatePriceTypeCommandResponse : CreatePriceTypeCommand
    {
        public Guid Id { get; set; }
    }
}
