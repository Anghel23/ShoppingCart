using Application.Use_Cases.Commands;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.CommandsHandlers
{
    public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, Guid>
    {
        private readonly IShoppingCartRepository repository;
        private readonly IMapper mapper;

        public CreateShoppingCartCommandHandler(IShoppingCartRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Guid> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var shoppingCart = mapper.Map<ShoppingCart>(request);
            return await repository.AddAsync(shoppingCart);
        }
    }
}
