using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ApplicationDbContext context;

        public ShoppingCartRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Guid> AddAsync(ShoppingCart shoppingCart)
        {
            await context.ShoppingCarts.AddAsync(shoppingCart);
            await context.SaveChangesAsync();
            return shoppingCart.Id;
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ShoppingCart>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ShoppingCart> GetByIdAsync(Guid id)
        {
            return await context.ShoppingCarts.FindAsync(id);
        }

        public Task<ShoppingCart> GetByUserIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ShoppingCart shoppingCart)
        {
            throw new NotImplementedException();
        }
    }
}
