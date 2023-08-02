using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo_Web_APi.Data;
using ToDo_Web_APi.DTOs;
using ToDo_Web_APi.DTOs.Pagination;
using ToDo_WEB_API.DTOs.Pagination;
using ToDo_Web_APi.Services;

namespace ToDo_Web_APi.Controllers;

/// <summary>
/// ToDoController
/// </summary>
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ToDoController : ControllerBase
{
    private readonly IAsyncToDoService _service;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    public ToDoController(ToDoDbContext context)
    {
        _service = new ToDoService(context);
    }

    /// <summary>
    ///  Gets a list of ToDoItems
    /// </summary>
    /// <param name="filters"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<PaginationListDto<ToDoItemDto>>> GetAsync(
        [FromQuery] ToDoQueryFilters filters,
        [FromQuery] PaginationRequest request
    )
    {
        return await _service.GetToDoItemsAsync(
            request.Page,
            request.PageSize,
            filters.Search,
            filters.IsCompleted
        );
    }

    /// <summary>
    /// Gets a ToDoItem by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ToDoItemDto>> GetIdAsync(int id)
    {
        var item = await _service.GetToDoItemAsync(id);
        return item is not null ? item : NotFound();
    }

    /// <summary>
    /// Creates a ToDoItem
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<ToDoItemDto>> PostAsync(
        [FromBody] CreateToDoItemRequest request
    )
    {
        var createdItem = await _service.CreateToDoItemAsync(request);
        return createdItem;
    }

    /// <summary>
    /// Updates a ToDoItem
    /// </summary>
    /// <param name="id"></param>
    /// <param name="isCompleted"></param>
    /// <returns></returns>
    [HttpPatch("{id}/status")]
    public async Task<ActionResult<ToDoItemDto>> PatchAsync(int id, [FromBody] bool isCompleted)
    {
        var todoItem = await _service.ChangeTodoItemStatusAsync(id, isCompleted);
        return todoItem is not null ? todoItem : NotFound();
    }
}