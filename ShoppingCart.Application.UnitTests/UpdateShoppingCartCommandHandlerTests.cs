using Application.Use_Cases.Commands;
using Domain.Repositories;
using Domain.Entities;
using NSubstitute;
using FluentAssertions;
using Application.Use_Cases.CommandsHandlers;

namespace ShoppingCartManagement.Application.UnitTests
{
    public class UpdateShoppingCartCommandHandlerTests
    {
        private readonly IShoppingCartRepository repository;

        public UpdateShoppingCartCommandHandlerTests()
        {
            repository = Substitute.For<IShoppingCartRepository>();
        }

        [Fact]
        public async Task Given_UpdateShoppingCartCommandHandler_When_HandleIsCalled_Then_CorrectShoppingCartShouldBeUpdated()
        {
            var shoppingCart = GenerateShoppingCart();
            var command = new UpdateShoppingCartCommand { Id = shoppingCart.Id, UserId = Guid.NewGuid() };
            repository.GetByIdAsync(shoppingCart.Id).Returns(shoppingCart);
            var handler = new UpdateShoppingCartCommandHandler(repository);

            await handler.Handle(command, CancellationToken.None);

            shoppingCart.UserId.Should().Be(command.UserId);
            await repository.Received(1).UpdateAsync(shoppingCart);
        }

        [Fact]
        public async Task Given_UpdateShoppingCartCommandHandler_When_CartDoesNotExist_Then_ThrowsKeyNotFoundException()
        {
            var command = new UpdateShoppingCartCommand { Id = Guid.NewGuid(), UserId = Guid.NewGuid() };
            repository.GetByIdAsync(command.Id).Returns((ShoppingCart)null);
            var handler = new UpdateShoppingCartCommandHandler(repository);

            
            Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage($"Shopping cart with ID {command.Id} was not found.");
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
