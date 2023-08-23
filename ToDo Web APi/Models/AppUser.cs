using Microsoft.AspNetCore.Identity;

namespace ToDo_Web_APi.Models;

/// <summary>
/// AppUser inherits from IdentityUser
/// </summary>
public sealed class AppUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public ICollection<ToDoItem> ToDoItems { get; set; } = new List<ToDoItem>();
}