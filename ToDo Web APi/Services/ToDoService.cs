using ToDo_WEB_API.DTOs.Pagination;

namespace ToDo_Web_APi.Services;

/// <summary>
/// 
/// </summary>
public class ToDoService : IAsyncToDoService
{
    private readonly ToDoDbContext _context;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public ToDoService(ToDoDbContext context)
    {
        _context = context;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="isCompleted"></param>
    /// <returns></returns>
    public async Task<ToDoItemDto> ChangeTodoItemStatusAsync(int id, bool isCompleted)
    {
        var item = await _context.ToDoItems.FindAsync(id);
        if (item is null) return null!;
        item.IsCompleted = isCompleted;
        item.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return ConvertToDoItemDto(item);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<ToDoItemDto> CreateToDoItemAsync(CreateToDoItemRequest request)
    {
        var now = DateTime.UtcNow;
        var item = new ToDoItem
        {
            Text = request.Text,
            CreatedAt = now,
            UpdatedAt = now,
            IsCompleted = false
        };

        item = _context.ToDoItems.Add(item).Entity;
        await _context.SaveChangesAsync();
        return ConvertToDoItemDto(item);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<ToDoItemDto> GetToDoItemAsync(int id)
    {
        var item = await _context.ToDoItems.FindAsync(id);
        return item is not null ? ConvertToDoItemDto(item) : null!;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="search"></param>
    /// <param name="isCompleted"></param>
    /// <returns></returns>
    public async Task<PaginationListDto<ToDoItemDto>> GetToDoItemsAsync(int page,
        int pageSize,
        string? search,
        bool? isCompleted)
    {
        IQueryable<ToDoItem> query = _context.ToDoItems;
        if (string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(item => item.Text.Contains(search));
        }

        if (isCompleted.HasValue)
        {
            query = query.Where(
                item => item.IsCompleted == isCompleted);
        }

        var totalCount = await query.CountAsync();
        var items = await query.Skip((page - 1) *
                                     pageSize).Take(pageSize)
            .ToListAsync();
        return new PaginationListDto<ToDoItemDto>(
            items.Select(item => ConvertToDoItemDto(item)), new PaginationMeta(page, pageSize, totalCount));
    }


    private static ToDoItemDto ConvertToDoItemDto(ToDoItem item)
    {
        var todoItem = new ToDoItemDto
        {
            Id = item.Id,
            Text = item.Text,
            CreatedAt = item.CreatedAt,
            IsCompleted = item.IsCompleted
        };

        return todoItem;
    }
}

/*
 * Id	Text	IsCompleted	CreatedAt	UpdatedAt
3	First todo	FALSE	2023-07-27 23:16:49.95326+04	2023-07-27 23:16:49.95326+04
4	Second todo	FALSE	2023-07-27 23:17:27.803533+04	2023-07-27 23:17:27.803533+04
5	To be or not to be	FALSE	2023-07-27 23:18:15.2619+04	2023-07-27 23:18:15.2619+04
6	First lesson in STEP IT Academy with Nadir Zamanov: 04.10.2021	FALSE	2023-07-27 23:24:13.00282+04	2023-07-27 23:24:13.00282+04
7	Evde 4 ay oturmaq	FALSE	2023-07-27 23:25:04.813541+04	2023-07-27 23:25:04.813541+04
8	System programming is love	FALSE	2023-07-27 23:25:38.789469+04	2023-07-27 23:25:38.789469+04
9	My old friend from 2021: Suleyman Babayev	FALSE	2023-07-27 23:26:40.894327+04	2023-07-27 23:26:40.894327+04
10	Ya Sabr	FALSE	2023-07-27 23:27:15.350261+04	2023-07-27 23:27:15.350261+04

 */