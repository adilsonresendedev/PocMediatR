namespace PocMediatR.Application.Tests.Features.Commands
{
    public class CreatePriceTypeCommandValidatorTests : ValidatorTestBase<CreatePriceTypeCommand, CreatePriceTypeCommandValidator>
    {
        [Theory]
        [MemberData(nameof(CreatePriceData))]
        public override void Should_validate_request(CreatePriceTypeCommand request, bool isValid)
        {
            Validate(request, isValid);
        }

        public static IEnumerable<object[]> CreatePriceData =>
        [
            [
                new CreatePriceTypeCommand
                {
                    Description = default
                },
                false
            ],
            [
                new CreatePriceTypeCommand
                {
                    Description = string.Empty
                },
                false
            ],
            [
                new CreatePriceTypeCommand
                {
                    Description = "Description"
                },
                true
            ]
        ];
    }
}
