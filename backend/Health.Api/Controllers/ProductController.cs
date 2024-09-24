using Health.Core.Features.Products.Commands.Create;
using Health.Core.Features.Products.Commands.Delete;
using Health.Core.Features.Products.Commands.Update;
using Health.Core.Features.Products.Dto;
using Health.Core.Features.Products.Queries.Get;
using Health.Domain.Models.Enums;
using Health.Domain.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Health.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetProducts")]
    public async Task<ActionResult<CollectionResponse<ProductDto>>> GetProducts()
    {
        var result = await _mediator.Send(new GetProductsQuery());
        return result.ISuccessful
            ? Ok(result)
            : BadRequest(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("CreateProduct")]
    public async Task<ActionResult<BaseResponse<long>>> CreateProduct([FromForm] CreateProductCommand request)
    {
        var result = await _mediator.Send(request);
        return result.ISuccessful
            ? Ok(result)
            : BadRequest(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("UpdateProduct")]
    public async Task<ActionResult<BaseResponse<ProductDto>>> UpdateProduct([FromBody] UpdateProductCommand request)
    {
        var result = await _mediator.Send(request);
        return result.ISuccessful
            ? Ok(result.Data)
            : BadRequest(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("DeleteProduct/{id}")]
    public async Task<ActionResult<BaseResponse<ProductDto>>> DeleteProduct(long id)
    {
        var result = await _mediator.Send(new DeleteProductCommand(id));
        return result.ISuccessful
            ? Ok(result.Data)
            : BadRequest(result);
    }
}
