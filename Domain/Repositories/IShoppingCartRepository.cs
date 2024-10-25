using Domain.Entities;

namespace Domain.Repositories
{
    public interface IShoppingCartRepository
    {
       Task<IEnumerable<ShoppingCart>> GetAllAsync();
       Task<ShoppingCart> GetByIdAsync(Guid id);
       Task<ShoppingCart> GetByUserIdAsync(Guid userId);
       Task<Guid> AddAsync(ShoppingCart shoppingCart);
       Task UpdateAsync(ShoppingCart shoppingCart);
       Task DeleteAsync(Guid id);

    }
}
