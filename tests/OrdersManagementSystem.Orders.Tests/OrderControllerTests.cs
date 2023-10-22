using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using OrdersManagementSystem.Models;
using OrdersManagementSystem.Orders.Controllers;
using OrdersManagementSystem.Orders.Repositories;
using Xunit.Sdk;

namespace OrdersManagementSystem.Orders.Tests;

public class OrderControllerTests
{
    public class OrdersControllerTests
    {
        private readonly OrdersController _controller;
        private readonly Mock<OrdersContext> _mockContext;
        private readonly List<Order> _orders = new List<Order>
        {
            new Order { Id = Guid.NewGuid() },
            new Order { Id = Guid.NewGuid() }
        };

        public OrdersControllerTests()
        {
            _mockContext = new Mock<OrdersContext>();
            this.MockDbSet();
            _controller = new OrdersController(_mockContext.Object);
        }

        [Fact]
        public async Task PostOrder_ReturnsBadRequest_WhenOrderIsNull()
        {
            // Arrange
            Order order = null;

            // Act
            var result = _controller.Post(order);

            // Assert
            Assert.Null(result.Value);
        }

        [Fact]
        public async Task GetOrder_ReturnsOrder()
        {
            // Arrange

            // Act
            var result = _controller.Get(_orders.First().Id);

            // Assert
            var okResult = Assert.IsType<ActionResult<Order>>(result);
            var model = Assert.IsAssignableFrom<Order>(okResult.Value);
            Assert.IsType<Order>(model);
        }

        [Fact]
        public async Task GetFiltered_ReturnsOrders()
        {
            // Arrange

            // Act
            var result = _controller.GetFiltered(It.IsAny<Guid>(), It.IsAny<Guid>());

            // Assert
            var okResult = Assert.IsType<ActionResult<IEnumerable<Order>>>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Order>>(okResult.Value);
        }

        private void MockDbSet()
        {
            _mockContext.Setup(c => c.GetOrder(_orders.First().Id)).Returns(new OperationResult<Order> { Value = _orders.First() });
            _mockContext.Setup(c => c.GetOrders(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(new OperationResult<IEnumerable<Order>> { Value = _orders });
        }
    }
}