namespace ToDo_Web_APi.DTOs;

/// <summary>
/// 
/// </summary>
public class ToDoItemDto
{
    public int Id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string Text { get; set; } = string.Empty;
    /// <summary>
    /// 
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }
    public bool IsCompleted { get; set; }
}