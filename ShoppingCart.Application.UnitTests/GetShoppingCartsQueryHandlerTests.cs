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
    public class GetShoppingCartsQueryHandlerTests
    {
        private readonly IShoppingCartRepository repository;
        private readonly IMapper mapper;

        public GetShoppingCartsQueryHandlerTests()
        {
            repository = Substitute.For<IShoppingCartRepository>();
            mapper = Substitute.For<IMapper>();
        }

        [Fact]
        public async void Given_GetShoppingCartsQueryHandlerTests_When_HandleIsCalled_Then_AListOfShoppingCartsShouldBeReturned()
        {
            //Arrange
            List<ShoppingCart> shoppingCarts = GenerateShoppingCarts();
            repository.GetAllAsync().Returns(shoppingCarts);
            var query = new GetShoppingCartsQuery();
            GenerateShoppingCartsDto(shoppingCarts);
            var handler = new GetShoppingCartsQueryHandler(repository, mapper);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNull();
            result.Count.Should().Be(shoppingCarts.Count);

            for (int i = 0; i < shoppingCarts.Count; i++)
            {
                result[i].Id.Should().Be(shoppingCarts[i].Id);
                result[i].UserId.Should().Be(shoppingCarts[i].UserId);
                result[i].CreatedAt.Should().Be(shoppingCarts[i].CreatedAt);
                result[i].TotalPrice.Should().Be(shoppingCarts[i].TotalPrice);
                result[i].IsEmpty.Should().Be(shoppingCarts[i].IsEmpty);
            }
        }

        private void GenerateShoppingCartsDto(List<ShoppingCart> shoppingCarts)
        {
            mapper.Map<List<ShoppingCartDto>>(shoppingCarts).Returns(new List<ShoppingCartDto>
            {
                new ShoppingCartDto
                {
                    Id = shoppingCarts[0].Id,
                    UserId = shoppingCarts[0].UserId,
                    CreatedAt = shoppingCarts[0].CreatedAt,
                    TotalPrice = shoppingCarts[0].TotalPrice,
                    IsEmpty = shoppingCarts[0].IsEmpty
                },
                new ShoppingCartDto
                {
                    Id = shoppingCarts[1].Id,
                    UserId = shoppingCarts[1].UserId,
                    CreatedAt = shoppingCarts[1].CreatedAt,
                    TotalPrice = shoppingCarts[1].TotalPrice,
                    IsEmpty = shoppingCarts[1].IsEmpty
                }
            });
        }

        private List<ShoppingCart> GenerateShoppingCarts()
        {
            return new List<ShoppingCart>
            {
                new ShoppingCart
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    CreatedAt = DateTime.Now,
                    TotalPrice = 100,
                    IsEmpty = false
                },
                new ShoppingCart
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    CreatedAt = DateTime.Now,
                    TotalPrice = 200,
                    IsEmpty = false
                }
            };
        }
    }
}