namespace ToDo_Web_APi.Data;

public class ToDoDbContext : DbContext
{
    public ToDoDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<ToDoItem> ToDoItems { get; set; }
}