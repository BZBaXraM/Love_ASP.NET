using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDo_Web_APi.Models;

namespace ToDo_Web_APi.Data;

/// <summary>
/// ToDoDbContext
/// </summary>
public class ToDoDbContext : IdentityDbContext<AppUser>
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="options"></param>
    public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options)
    {
    }

    public DbSet<ToDoItem> ToDoItems => Set<ToDoItem>();

    public DbSet<AppUser> Users => Set<AppUser>();
}