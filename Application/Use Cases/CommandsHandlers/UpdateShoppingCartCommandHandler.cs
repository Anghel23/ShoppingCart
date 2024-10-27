using Application.Use_Cases.Commands;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Use_Cases.CommandsHandlers
{
    public class UpdateShoppingCartCommandHandler : IRequestHandler<UpdateShoppingCartCommand, Guid>
    {
        private readonly IShoppingCartRepository _repository;

        public UpdateShoppingCartCommandHandler(IShoppingCartRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(UpdateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _repository.GetByIdAsync(request.Id); 
            if (cart == null)
            {
                throw new KeyNotFoundException($"Shopping cart with ID {request.Id} was not found.");
            }

            cart.UserId = request.UserId; 
            cart.TotalPrice = request.TotalPrice; 
            cart.IsEmpty = request.IsEmpty; 

            await _repository.UpdateAsync(cart); 
            return cart.Id; 
        }
    }
}
