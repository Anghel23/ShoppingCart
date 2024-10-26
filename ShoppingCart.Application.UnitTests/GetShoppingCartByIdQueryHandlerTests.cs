using AutoMapper;
using Domain.Repositories;
using Domain.Entities;
using NSubstitute;
using Application.Use_Cases.Queries;
using Application.Use_Cases.QueryHandlers;
using Application.DTOs;
using FluentAssertions;

namespace ShoppingCartManagement.Application.UnitTests
{
    public class GetShoppingCartByIdQueryHandlerTests
    {
        private readonly IShoppingCartRepository repository;
        private readonly IMapper mapper;

        public GetShoppingCartByIdQueryHandlerTests()
        {
            repository = Substitute.For<IShoppingCartRepository>();
            mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public async void Given_GetShoppingCartByIdQueryHandler_When_HandleIsCalled_Then_CorrectShoppingCartShouldBeReturned()
        {
            //Arrange
            var shoppingCart = GenerateShoppingCart();
            var shoppingCartDto = GenerateShoppingCartDto(shoppingCart);
            repository.GetByIdAsync(shoppingCart.Id).Returns(shoppingCart);
            mapper.Map<ShoppingCartDto>(shoppingCart).Returns(shoppingCartDto);
            var query = new GetShoppingCartByIdQuery { Id = shoppingCart.Id };
            var handler = new GetShoppingCartByIdQueryHandler(repository, mapper);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(shoppingCart.Id);
            result.UserId.Should().Be(shoppingCart.UserId);
            result.CreatedAt.Should().Be(shoppingCart.CreatedAt);
            result.TotalPrice.Should().Be(shoppingCart.TotalPrice);
            result.IsEmpty.Should().Be(shoppingCart.IsEmpty);
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

        private ShoppingCartDto GenerateShoppingCartDto(ShoppingCart shoppingCart)
        {
            return new ShoppingCartDto
            {
                Id = shoppingCart.Id,
                UserId = shoppingCart.UserId,
                CreatedAt = shoppingCart.CreatedAt,
                TotalPrice = shoppingCart.TotalPrice,
                IsEmpty = shoppingCart.IsEmpty
            };
        }
    }
}
