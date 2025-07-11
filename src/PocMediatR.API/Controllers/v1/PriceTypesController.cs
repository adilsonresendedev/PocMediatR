using MediatR;
using Microsoft.AspNetCore.Mvc;
using PocMediatR.Application.Features.Commands.CreatePriceType;
using PocMediatR.Common.Models;

namespace PocMediatR.API.Controllers
{
    [Route("api/v1/price-types")]
    public class PriceTypesController(IMediator mediator) : PocMediatRController
    {
        [HttpPost]
        [ProducesResponseType(typeof(CreatePriceTypeCommandResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] CreatePriceTypeCommand request)
        {
            await mediator.Send(request);
            return Created();
        }
    }
}
