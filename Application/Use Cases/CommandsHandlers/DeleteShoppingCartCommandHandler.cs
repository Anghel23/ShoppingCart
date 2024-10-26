using Application.Use_Cases.Commands;
using Domain.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Use_Cases.CommandsHandlers
{
    public class DeleteShoppingCartCommandHandler : IRequestHandler<DeleteShoppingCartCommand, Guid> 
    {
        private readonly IShoppingCartRepository _repository;

        public DeleteShoppingCartCommandHandler(IShoppingCartRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(DeleteShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _repository.GetByIdAsync(request.Id); 
            if (cart == null)
            {
                throw new KeyNotFoundException($"Shopping cart with ID {request.Id} was not found."); 
            }

            var deletedCartId = await _repository.DeleteAsync(request.Id);
            return deletedCartId; 
        }
    }
}
