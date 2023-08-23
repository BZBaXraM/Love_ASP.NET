using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo_Web_APi.Data;
using ToDo_Web_APi.DTOs;
using ToDo_Web_APi.DTOs.Pagination;
using ToDo_WEB_API.DTOs.Pagination;
using ToDo_Web_APi.Providers;
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
    private readonly IRequestUserProvider _provider;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    public ToDoController(ToDoDbContext context, IRequestUserProvider provider)
    {
        _service = new ToDoService(context);
        _provider = provider;
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
        var user = _provider.GetUserInfo();
        return await _service.GetToDoItemsAsync(
            user!.Id,
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
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ToDoItemDto>> GetIdAsync(int id)
    {
        var user = _provider.GetUserInfo();
        var item = await _service.GetToDoItemAsync(user!.Id, id);
        return item;
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
        var user = _provider.GetUserInfo();
        var createdItem = await _service.CreateToDoItemAsync(user.Id, request);
        return createdItem;
    }

    /// <summary>
    /// Updates a ToDoItem
    /// </summary>
    /// <param name="id"></param>
    /// <param name="isCompleted"></param>
    /// <returns></returns>
    [HttpPatch("{id:int}/status")]
    public async Task<ActionResult<ToDoItemDto>> PatchAsync(int id, [FromBody] bool isCompleted)
    {
        var user = _provider.GetUserInfo();
        var todoItem = await _service.ChangeTodoItemStatusAsync(user!.Id, id, isCompleted);
        return todoItem;
    }
}