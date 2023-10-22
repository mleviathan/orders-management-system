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

        public static IEnumerable<object[]> InvalidGuids
        {
            get
            {
                yield return new object[] { Guid.Empty, Guid.NewGuid() };
                yield return new object[] { Guid.Empty, Guid.Empty };
                yield return new object[] { Guid.NewGuid(), Guid.Empty };
            }
        }

        public OrdersControllerTests()
        {
            _mockContext = new Mock<OrdersContext>();
            this.MockDbSet();
            _controller = new OrdersController(_mockContext.Object);
        }

        #region PostOrder

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
        public async Task PostOrder_ReturnsOk_WhenOrderIsValid()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var order = new Order { Id = guid };
            _mockContext.Setup(c => c.CreateOrder(order)).Returns(new OperationResult<Order> { Value = order });

            // Act
            var result = _controller.Post(order);

            // Assert
            var createdResult = Assert.IsType<ActionResult<Order>>(result);
            Assert.Equal(order, (result.Result as CreatedResult).Value);
        }

        [Fact]
        public async Task PostOrder_ReplacesEmptyGuid_ReturnsOk()
        {
            // Arrange
            var order = new Order { Id = Guid.Empty };
            _mockContext.Setup(c => c.CreateOrder(order)).Returns(new OperationResult<Order> { Value = order });

            // Act
            var result = _controller.Post(order);

            // Assert
            var createdResult = Assert.IsType<ActionResult<Order>>(result);
            var createdOrder = (result.Result as CreatedResult).Value;
            Assert.IsType<Order>(createdOrder);
            Assert.NotEqual(Guid.Empty, createdOrder);
        }

        #endregion

        #region GetOrder

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
        public async Task GetOrder_EmptyGuid_ReturnsError()
        {
            // Arrange
            _mockContext.Setup(c => c.GetOrder(Guid.Empty)).Returns(new OperationResult<Order>());

            // Act
            var result = _controller.Get(Guid.Empty);

            // Assert
            var okResult = Assert.IsType<ActionResult<Order>>(result);
            Assert.Null(okResult.Value);
        }

        [Fact]
        public async Task GetOrder_NotFound_ReturnsError()
        {
            var guid = Guid.NewGuid();
            // Arrange
            _mockContext.Setup(c => c.GetOrder(guid)).Returns(new OperationResult<Order>());

            // Act
            var result = _controller.Get(guid);

            // Assert
            var okResult = Assert.IsType<ActionResult<Order>>(result);
            Assert.Null(okResult.Value);
        }

        #endregion

        #region GetFiltered

        [Fact]
        public async Task GetFiltered_ValidParams_ReturnsOrders()
        {
            // Arrange

            // Act
            var result = _controller.GetFiltered(Guid.NewGuid(), Guid.NewGuid());

            // Assert
            var okResult = Assert.IsType<ActionResult<IEnumerable<Order>>>(result);
            var model = Assert.IsAssignableFrom<List<Order>>(okResult.Value);
            Assert.IsType<List<Order>>(model);
        }

        [Theory, MemberData(nameof(InvalidGuids))]
        public async Task GetFiltered_InvalidIds_ReturnBadRequest(Guid? userId, Guid? addressId)
        {
            // Arrange

            // Act
            var result = _controller.GetFiltered(userId, addressId);

            // Assert
            var okResult = Assert.IsType<ActionResult<IEnumerable<Order>>>(result);
            Assert.Null(okResult.Value);
        }

        #endregion

        #region PutOrder

        [Fact]
        public async Task PutOrder_ReturnsBadRequest_WhenOrderIsNull()
        {
            // Arrange
            Order order = null;

            // Act
            var result = _controller.Put(order);

            // Assert
            Assert.Null(result.Value);
        }

        [Fact]
        public async Task PutOrder_ReturnsOk_WhenOrderIsValid()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var order = new Order { Id = guid };
            _mockContext.Setup(c => c.UpdateOrder(order)).Returns(new OperationResult<Order> { Value = order });

            // Act
            var result = _controller.Put(order);

            // Assert
            var okResult = Assert.IsType<ActionResult<Order>>(result);
            var model = Assert.IsAssignableFrom<Order>(okResult.Value);
            Assert.IsType<Order>(model);
            Assert.Equal(guid, model.Id);
        }

        #endregion

        #region DeleteOrder

        [Fact]
        public async Task DeleteOrder_ReturnsNotFound_WhenOrderIdIsEmpty()
        {
            // Arrange

            // Act
            var result = _controller.Delete(Guid.Empty);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteOrder_ReturnsOk_WhenOrderIdIsValid()
        {
            // Arrange
            var guid = Guid.NewGuid();
            _mockContext.Setup(c => c.DeleteOrder(guid)).Returns(new OperationResult<bool> { Value = true });

            // Act
            var result = _controller.Delete(guid);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
        }


        #endregion

        private void MockDbSet()
        {
            _mockContext.Setup(c => c.GetOrder(_orders.First().Id)).Returns(new OperationResult<Order> { Value = _orders.First() });
            _mockContext.Setup(c => c.GetOrders(It.Is<Guid>(g => g != Guid.Empty), It.Is<Guid>(g => g != Guid.Empty))).Returns(new OperationResult<IEnumerable<Order>> { Value = _orders });
            _mockContext.Setup(c => c.GetOrders(null, null)).Returns(new OperationResult<IEnumerable<Order>> { Value = _orders });
            _mockContext.Setup(c => c.GetOrders(It.Is<Guid>(g => g == Guid.Empty), It.Is<Guid>(g => g != Guid.Empty))).Returns(new OperationResult<IEnumerable<Order>> { Message = "User ID cannot be empty" });
            _mockContext.Setup(c => c.GetOrders(It.Is<Guid>(g => g != Guid.Empty), It.Is<Guid>(g => g == Guid.Empty))).Returns(new OperationResult<IEnumerable<Order>> { Message = "Address ID cannot be empty" });
            _mockContext.Setup(c => c.GetOrders(It.Is<Guid>(g => g == Guid.Empty), It.Is<Guid>(g => g == Guid.Empty))).Returns(new OperationResult<IEnumerable<Order>> { Message = "User ID cannot be empty" });
        }
    }
}