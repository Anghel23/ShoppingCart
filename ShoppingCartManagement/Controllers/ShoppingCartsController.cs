using Application.DTOs;
using Application.Use_Cases.Commands;
using Application.Use_Cases.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingCartManagement.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ShoppingCartsController : ControllerBase
    {
        private readonly IMediator mediator;

        public ShoppingCartsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateShoppingCart(CreateShoppingCartCommand command)
        {
            var id = await mediator.Send(command);
            return CreatedAtAction("GetById", new { Id = id }, id);
        }

        [HttpGet("id")]
        public async Task<ActionResult<ShoppingCartDto>> GetById(Guid id)
        {
            var result = await mediator.Send(new GetShoppingCartByIdQuery { Id = id });

            if (result == null)
            {
                return NotFound(new { Message = $"ShoppingCart with ID {id} was not found." });
            }

            return Ok(result);
        }

    }
}
