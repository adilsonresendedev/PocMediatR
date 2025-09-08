using MediatR;
using Microsoft.AspNetCore.Mvc;
using PocMediatR.Application.Features.Commands.CreatePriceType;
using PocMediatR.Application.Features.Queries.Get;
using PocMediatR.Common.Models;

namespace PocMediatR.API.Controllers
{
    [Route("api/v1/price-types")]
    public class PriceTypesController(IMediator mediator) : PocMediatRController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] CreatePriceTypeCommand request)
        {
            var response = await mediator.Send(request);

            return CreatedAtRoute(
                routeName: nameof(Get),
                routeValues: new { id = response.Id },
                value: null
            );
        }

        [HttpGet("{id:guid}", Name = nameof(Get))]
        [ProducesResponseType(typeof(CreatePriceTypeCommandResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var getPriceTypeCommand = new GetPriceTypeCommand
            {
                Id = id
            };

            var response = await mediator.Send(getPriceTypeCommand);
            return Ok(response);
        }
    }
}
