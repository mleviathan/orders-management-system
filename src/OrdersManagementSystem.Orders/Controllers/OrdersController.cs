using Microsoft.AspNetCore.Mvc;
using OrdersManagementSystem.Orders.Repositories;

namespace OrdersManagementSystem.Orders.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly OrdersContext _context;

    public OrdersController(OrdersContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get the order with specified ID
    /// </summary>
    /// <param name="orderId">ID of the entity to fetch from DB</param>
    /// <returns>Order fetched from DB</returns>
    /// <response code="200">Returns the requested order</response>
    /// <response code="400">If the OrderId is an Empty Guid</response>
    /// <response code="404">If the requested order is not present in DB</response>
    [HttpGet("Order")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Models.Order> Get(Guid orderId)
    {
        var order = _context.GetOrder(orderId);

        if (order.Message != null)
        {
            return BadRequest(order.Message);
        }

        if (order.Value == null)
        {
            return NotFound();
        }

        return order.Value;
    }

    /// <summary>
    /// Filters orders by given parameters
    /// </summary>
    /// <param name="userId">The Order object to persist in database</param>
    /// <returns>A list of filtered Orders</returns>
    [HttpGet("OrdersFiltered")]
    public ActionResult<IEnumerable<Models.Order>> GetFiltered(Guid? userId, Guid? addressId)
    {
        if (userId == Guid.Empty)
        {
            return BadRequest("User ID cannot be empty");
        }

        if (userId == Guid.Empty)
        {
            return BadRequest("Address ID cannot be empty");
        }

        var ordersResult = _context.GetOrders(userId, addressId);


        return ordersResult.Value.ToList();
    }

    /// <summary>
    /// Create an Order in DB
    /// </summary>
    /// <param name="order">Order object to persist in DB</param>
    /// <returns>201 with given order if successful</returns>
    /// <response code="200">If the Order was successfully created</response>
    /// <response code="400">If there was an error creating the entity</response>
    [HttpPost(Name = "Order")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Models.Order> Post(Models.Order order)
    {
        if (order == null)
        {
            return BadRequest("Order cannot be null");
        }

        // Verify that the order has an ID to give more flexibility to the client
        if (order.Id == Guid.Empty)
        {
            order.Id = Guid.NewGuid();
        }

        var creationResult = _context.CreateOrder(order);

        if (creationResult.Message != null)
        {
            return BadRequest(creationResult.Message);
        }

        return creationResult.Value;
    }

    /// <summary>
    /// Update an Order in DB
    /// </summary>
    /// <param name="orderId">ID of the Order to update</param>
    /// <param name="order">Updated entity</param>
    /// <returns>200 if Update was successful</returns>
    // [HttpPut(Name = "Order")]
    // public ActionResult Put(Models.Order order)
    // {
    //     _context.UpdateOrder(order);
    // }

    /// <summary>
    /// Delete an Order
    /// </summary>
    /// <param name="orderId">ID of the Order to delete</param>
    /// <returns>200 if the order was removed</returns>
    // [HttpDelete(Name = "Order")]
    // public ActionResult<Models.Order> Delete(Guid orderId)
    // {
    //     throw new NotImplementedException();
    //     // return _context.GetOrders(userId, addressId);
    // }
}