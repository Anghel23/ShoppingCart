using Application.DTOs;
using Application.Use_Cases.Commands;
using Application.Use_Cases.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [HttpGet]
        public async Task<ActionResult<List<ShoppingCartDto>>> GetAll()
        {
            return await mediator.Send(new GetShoppingCartsQuery());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppingCart(Guid id)
        {
            try
            {
                var result = await mediator.Send(new DeleteShoppingCartCommand { Id = id });

                return NoContent(); 
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message }); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while deleting the shopping cart.", Error = ex.Message });
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShoppingCart(Guid id, UpdateShoppingCartCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest(new { Message = "ID in URL does not match ID in request body." });
            }

            try
            {
                var updatedCartId = await mediator.Send(command); // Changed _mediator to mediator
                return Ok(new { Message = $"Shopping cart with ID {id} updated successfully.", Id = updatedCartId });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while updating the shopping cart.", Error = ex.Message });
            }
        }
    }
}
