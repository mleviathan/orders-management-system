using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using OrdersManagementSystem.Models;
using OrdersManagementSystem.Orders.Controllers;
using OrdersManagementSystem.Orders.Repositories;

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
            _mockContext.Setup(c => c.Orders).Returns(MockDbSet(_orders));
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
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<Order>(okResult.Value);
            Assert.IsType<Order>(model);
        }

        private DbSet<T> MockDbSet<T>(IEnumerable<T> data) where T : class
        {
            var queryableData = data.AsQueryable();
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryableData.GetEnumerator());
            return mockSet.Object;
        }
    }
}