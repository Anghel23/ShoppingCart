using Application.DTOs;
using MediatR;

namespace Application.Use_Cases.Queries
{
    public class GetShoppingCartsQuery : IRequest<List<ShoppingCartDto>>
    {
    }
}
