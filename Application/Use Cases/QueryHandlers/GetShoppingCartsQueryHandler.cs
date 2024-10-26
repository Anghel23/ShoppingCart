using Application.DTOs;
using Application.Use_Cases.Queries;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.QueryHandlers
{
    public class GetShoppingCartsQueryHandler : IRequestHandler<GetShoppingCartsQuery, List<ShoppingCartDto>>
    {
        private readonly IShoppingCartRepository repository;
        private readonly IMapper mapper;

        public GetShoppingCartsQueryHandler(IShoppingCartRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<ShoppingCartDto>> Handle(GetShoppingCartsQuery request, CancellationToken cancellationToken)
        {
            var shoppingCarts = await repository.GetAllAsync();
            return mapper.Map<List<ShoppingCartDto>>(shoppingCarts);
        }
    }
}
