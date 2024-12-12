
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShoppingTask.Api.Controllers.Public;
using ShoppingTask.Core.DTOs;
using ShoppingTask.Core.Shared;
using ShoppingTask.Domain.Services.Orders;
using Xunit;

namespace ShoppingTask.Test.Controllers;

public class TestOrdersController
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly OrdersController _ordersController;

    public TestOrdersController()
    {
        _mediatorMock = new Mock<IMediator>();
        _ordersController = new OrdersController(_mediatorMock.Object);
    }

    [Fact]
    public async Task GetAllByUser_ReturnsOkResult()
    {
        // Arrange
        var request = new GetAllByUserRequest();
        var response = new PaginationResponseDTO<GetAllByUserResponse>(
        new List<GetAllByUserResponse> { new GetAllByUserResponse() },
         1
        );
        _mediatorMock.Setup(m => m.Send(request, It.IsAny<CancellationToken>())).ReturnsAsync(response);

        // Act
        var result = await _ordersController.Get(request, CancellationToken.None) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(response, result.Value);
    }

    [Fact]
    public async Task GetOrderDetails_ReturnsOkResult()
    {
        // Arrange
        var request = new GetOrderDetailsRequest();
        var response = new List<GetOrderDetailsResponse>();
        _mediatorMock.Setup(m => m.Send(request, It.IsAny<CancellationToken>())).ReturnsAsync(response);

        // Act
        var result = await _ordersController.Get(request, CancellationToken.None) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(response, result.Value);
    }

    [Theory]
    [InlineData("2022-01-01", "26C88310-C18F-411E-EF0A-08DD1A130A64", 1, 10.99, 4)]
    [InlineData("2022-01-02", "26C88310-C18F-411E-EF0A-08DD1A130A64", 1, 3, 5.99, 4)]
    public async Task Add_ReturnsCreatedResult(string date, string userId,  decimal orderItem1Quantity, decimal orderItem1Price, int orderItem1ProductId)
    {
        // Arrange
        var request = new AddOrderRequest
        {
            Date = DateTime.Parse(date),
            UserId = Guid.Parse(userId),
            OrderItemDTOs = new List<OrderItemDTO>
        {
            new OrderItemDTO(0, orderItem1Quantity, orderItem1Price, orderItem1ProductId)    
        }
        };

        var result = Result.Success();
        _mediatorMock.Setup(m => m.Send(It.IsAny<AddOrderRequest>(), It.IsAny<CancellationToken>())).ReturnsAsync(result);

        // Act
        var response = await _ordersController.Add(request, CancellationToken.None) as CreatedResult;

        // Assert
        Assert.NotNull(response);
    }

}
