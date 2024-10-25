using Application.DTOs;
using MediatR;

namespace Application.Use_Cases.Queries
{
    public class GetShoppingCartByIdQuery : IRequest<ShoppingCartDto>
    {
        public Guid Id { get; set; }
    }
}
