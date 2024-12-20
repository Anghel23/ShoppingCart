﻿using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Guid> DeleteAsync(Guid id)
        {
            var cart = await context.ShoppingCarts.FindAsync(id);

            if (cart == null)
            {
                throw new KeyNotFoundException($"Shopping cart with ID {id} was not found."); 
            }

            context.ShoppingCarts.Remove(cart);
            await context.SaveChangesAsync(); 

            return id; 
        }


        public async Task<IEnumerable<ShoppingCart>> GetAllAsync()
        {
            return await context.ShoppingCarts.ToListAsync();
        }

        public async Task<ShoppingCart> GetByIdAsync(Guid id)
        {
            return await context.ShoppingCarts.FindAsync(id);
        }

        public Task<ShoppingCart> GetByUserIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(ShoppingCart shoppingCart)
        {
            var existingCart = await context.ShoppingCarts.FindAsync(shoppingCart.Id);

            if (existingCart == null)
            {
                throw new KeyNotFoundException($"Shopping cart with ID {shoppingCart.Id} was not found.");
            }

            existingCart.UserId = shoppingCart.UserId; 
            existingCart.TotalPrice = shoppingCart.TotalPrice; 
            existingCart.IsEmpty = shoppingCart.IsEmpty; 

            await context.SaveChangesAsync();
        }
    }
}
