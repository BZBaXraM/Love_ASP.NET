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

    /// <summary>
    ///  ToDoItems
    ///  <para>Represents a table in the database.</para>
    ///  <para>Represents a collection of entities from the database.</para>
    ///  <para>Represents a DbSet of entities from the database.</para>    
    /// </summary>
    public DbSet<ToDoItem> ToDoItems => Set<ToDoItem>();

    /// <summary>
    /// Users
    ///  <para>Represents a table in the database.</para>
    ///  <para>Represents a collection of entities from the database.</para>
    ///  <para>Represents a DbSet of entities from the database.</para>
    /// </summary>
    public DbSet<AppUser> Users => Set<AppUser>();
}