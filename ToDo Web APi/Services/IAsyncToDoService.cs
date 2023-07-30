using ToDo_Web_APi.DTOs;
using ToDo_WEB_API.DTOs.Pagination;

namespace ToDo_Web_APi.Services;

/// <summary>
/// 
/// </summary>
public interface IAsyncToDoService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="search"></param>
    /// <param name="isCompleted"></param>
    /// <returns></returns>
    Task<PaginationListDto<ToDoItemDto>> GetToDoItemsAsync(int page,
        int pageSize,
        string? search,
        bool? isCompleted);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ToDoItemDto> GetToDoItemAsync(int id);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="isCompleted"></param>
    /// <returns></returns>
    Task<ToDoItemDto> ChangeTodoItemStatusAsync(int id, bool isCompleted);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<ToDoItemDto> CreateToDoItemAsync(CreateToDoItemRequest request);
}