using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Dto;
using Order.Application.Interfaces.Commands;
using Order.Application.Interfaces.Queries;

namespace Order.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly ICreateOrderCommand _createOrderCommand;
    private readonly IGetOrderQuery _getOrderQuery;
    private readonly IGetAllOrdersQuery _getAllOrdersQuery;
    private readonly IUpdateOrderCommand _updateOrderCommand;
    private readonly IDeleteOrderCommand _deleteOrderCommand;

    public OrderController(ICreateOrderCommand createOrderCommand, IGetOrderQuery getOrderQuery, 
        IGetAllOrdersQuery getAllOrdersQuery, IUpdateOrderCommand updateOrderCommand, IDeleteOrderCommand deleteOrderCommand)
    {
        _createOrderCommand = createOrderCommand;
        _getOrderQuery = getOrderQuery;
        _getAllOrdersQuery = getAllOrdersQuery;
        _updateOrderCommand = updateOrderCommand;
        _deleteOrderCommand = deleteOrderCommand;
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Post(CreateOrderDto dto)
    {
        try
        {
            _createOrderCommand.Create(dto);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }   
    }
    
    [HttpGet("AllOrders/")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<OrderQueryDto>> GetAllOrders()
    {
        var result = _getAllOrdersQuery.GetAllOrders().ToList();
        if (!result.Any()) 
            return NotFound();
        
        return result.ToList();
    }
    
    [HttpGet("{id}/")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<OrderQueryDto> GetOrderById(int id)
    {
        var result = _getOrderQuery.GetOrderById(id);
        
        return result;
    }
    
    [HttpPut("UpdateOrder")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Put([FromBody] UpdateOrderDto dto)
    {
        try
        {
            _updateOrderCommand.Update(dto);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpDelete("DeleteOrder/{id}/")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<DeleteOrderDto> Delete(int id)
    {
        try
        {
            _deleteOrderCommand.Delete(id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}