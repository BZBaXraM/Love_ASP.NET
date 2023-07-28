using Microsoft.EntityFrameworkCore;
using ToDo_Web_APi.Models;

namespace ToDo_Web_APi.Data;

public class ToDoDbContext : DbContext
{
    public ToDoDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<ToDoItem> ToDoItems => Set<ToDoItem>();
}