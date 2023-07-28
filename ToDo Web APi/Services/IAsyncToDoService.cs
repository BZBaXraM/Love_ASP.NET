using ToDo_WEB_API.DTOs.Pagination;

namespace ToDo_Web_APi.Services;

public interface IAsyncToDoService
{
    Task<PaginationListDto<ToDoItemDto>> GetToDoItemsAsync(int page,
        int pageSize,
        string? search,
        bool? isCompleted);

    Task<ToDoItemDto> GetToDoItemAsync(int id);
    Task<ToDoItemDto> ChangeTodoItemStatusAsync(int id, bool isCompleted);
    Task<ToDoItemDto> CreateToDoItemAsync(CreateToDoItemRequest request);
}