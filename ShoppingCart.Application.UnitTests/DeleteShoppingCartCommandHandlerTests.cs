using Application.Use_Cases.Commands;
using Application.Use_Cases.CommandsHandlers;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using NSubstitute;

namespace ShoppingCartManagement.Application.UnitTests
{
    public class DeleteShoppingCartCommandHandlerTests
    {
        private readonly IShoppingCartRepository repository;
        private readonly IMapper mapper;

        public DeleteShoppingCartCommandHandlerTests()
        {
            repository = Substitute.For<IShoppingCartRepository>();
            mapper = Substitute.For<IMapper>(); 
        }

        [Fact]
        public async Task Given_DeleteShoppingCartCommandHandler_When_HandleIsCalled_Then_CorrectShoppingCartShouldBeDeleted()
        {
            var shoppingCart = GenerateShoppingCart();
            repository.GetByIdAsync(shoppingCart.Id).Returns(shoppingCart);
            repository.DeleteAsync(shoppingCart.Id).Returns(shoppingCart.Id); 

            var command = new DeleteShoppingCartCommand { Id = shoppingCart.Id };
            var handler = new DeleteShoppingCartCommandHandler(repository);

            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().Be(shoppingCart.Id, "because the returned ID should match the deleted shopping cart ID");
            await repository.Received(1).DeleteAsync(shoppingCart.Id); 
        }

        private ShoppingCart GenerateShoppingCart()
        {
            return new ShoppingCart
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                TotalPrice = 150,
                IsEmpty = false
            };
        }
    }
}
