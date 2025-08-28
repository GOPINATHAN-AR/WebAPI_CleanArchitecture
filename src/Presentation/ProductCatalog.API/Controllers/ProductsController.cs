using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.Common.Models;
using ProductCatalog.Application.Features.Products.Commands;
using ProductCatalog.Application.Features.Products.DTOs;
using ProductCatalog.Application.Features.Products.Queries;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<ProductDto>>> GetAll([FromQuery] PaginationQuery pagination)
    {
        var result = await _mediator.Send(new GetAllProductsQuery(pagination));
        
        if (!result.IsSuccess)
            return BadRequest(result.Errors);
            
        return Ok(result.Data);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProductDto>> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetProductByIdQuery(id));
        
        if (!result.IsSuccess)
            return NotFound(result.Errors);
            
        return Ok(result.Data);
    }

    [HttpGet("search")]
    public async Task<ActionResult<PagedResult<ProductDto>>> Search([FromQuery] string searchTerm, [FromQuery] PaginationQuery pagination)
    {
        var result = await _mediator.Send(new SearchProductsQuery(searchTerm, pagination));
        
        if (!result.IsSuccess)
            return BadRequest(result.Errors);
            
        return Ok(result.Data);
    }

    [HttpPost("filter")]
    public async Task<ActionResult<PagedResult<ProductDto>>> GetFiltered([FromBody] ProductQueryDto filter, [FromQuery] PaginationQuery pagination)
    {
        var result = await _mediator.Send(new GetProductsQuery(filter, pagination));
        
        if (!result.IsSuccess)
            return BadRequest(result.Errors);
            
        return Ok(result.Data);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> Create([FromBody] CreateProductDto createProductDto)
    {
        var result = await _mediator.Send(new CreateProductCommand(createProductDto));
        
        if (!result.IsSuccess)
            return BadRequest(result.Errors);
            
        return CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ProductDto>> Update(Guid id, [FromBody] UpdateProductDto updateProductDto)
    {
        var result = await _mediator.Send(new UpdateProductCommand(id, updateProductDto));
        
        if (!result.IsSuccess)
            return BadRequest(result.Errors);
            
        return Ok(result.Data);
    }

    [HttpPatch("{id:guid}/activate")]
    public async Task<ActionResult<ProductDto>> Activate(Guid id)
    {
        var result = await _mediator.Send(new ActivateProductCommand(id));
        
        if (!result.IsSuccess)
            return BadRequest(result.Errors);
            
        return Ok(result.Data);
    }

    [HttpPatch("{id:guid}/deactivate")]
    public async Task<ActionResult<ProductDto>> Deactivate(Guid id)
    {
        var result = await _mediator.Send(new DeactivateProductCommand(id));
        
        if (!result.IsSuccess)
            return BadRequest(result.Errors);
            
        return Ok(result.Data);
    }

    [HttpPatch("{id:guid}/status")]
    public async Task<ActionResult<ProductDto>> UpdateStatus(Guid id, [FromBody] ProductStatus status)
    {
        var result = await _mediator.Send(new UpdateProductStatusCommand(id, status));
        
        if (!result.IsSuccess)
            return BadRequest(result.Errors);
            
        return Ok(result.Data);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await _mediator.Send(new DeleteProductCommand(id));
        
        if (!result.IsSuccess)
            return NotFound(result.Errors);
            
        return NoContent();
    }
}
