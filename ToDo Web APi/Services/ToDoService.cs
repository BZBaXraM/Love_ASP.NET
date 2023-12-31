using Microsoft.EntityFrameworkCore;
using ToDo_Web_APi.Data;
using ToDo_Web_APi.DTOs;
using ToDo_Web_APi.DTOs.Pagination;
using ToDo_WEB_API.DTOs.Pagination;
using ToDo_Web_APi.Models;

namespace ToDo_Web_APi.Services;

/// <summary>
/// ToDoService
/// </summary>
public class ToDoService : IAsyncToDoService
{
    private readonly ToDoDbContext _context;

    /// <summary>
    ///  Constructor
    /// </summary>
    /// <param name="context"></param>
    public ToDoService(ToDoDbContext context)
        => _context = context;

    /// <summary>
    ///  Converts ToDoItem to ToDoItemDto    
    /// </summary>
    /// <param name="id"></param>
    /// <param name="isCompleted"></param>
    /// <returns></returns>
    public async Task<ToDoItemDto> ChangeTodoItemStatusAsync(string userId, int id, bool isCompleted)
    {
        var item = await _context.ToDoItems.FirstOrDefaultAsync(
            x => x.Id == id && x.UserId == userId);
        if (item is null) return null;
        item.IsCompleted = isCompleted;
        item.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return ConvertToDoItemDto(item);
    }

    public async Task<ToDoItemDto> CreateToDoItemAsync(string userId, CreateToDoItemRequest request)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user is null)
        {
            throw new KeyNotFoundException();
        }

        var now = DateTime.UtcNow;
        var item = new ToDoItem
        {
            Text = request.Text,
            CreatedAt = now,
            UpdatedAt = now,
            IsCompleted = false,
            UserId = user.Id,
        };

        await _context.ToDoItems.AddAsync(item);
        await _context.SaveChangesAsync();

        return ConvertToDoItemDto(item);
    }

    public async Task<ToDoItemDto> GetToDoItemAsync(string userId, int id)
    {
        var item = await _context.ToDoItems.FirstOrDefaultAsync(
            x => x.Id == id && x.UserId == userId);
        return item is not null ? ConvertToDoItemDto(item) : null!;
    }

    /// <summary>
    ///  Converts ToDoItem to ToDoItemDto
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="search"></param>
    /// <param name="isCompleted"></param>
    /// <returns></returns>
    public async Task<PaginationListDto<ToDoItemDto>> GetToDoItemsAsync(string userId, int page,
        int pageSize,
        string? search,
        bool? isCompleted)
    {
        IQueryable<ToDoItem> query = _context.ToDoItems;
        if (!string.IsNullOrWhiteSpace(search))
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