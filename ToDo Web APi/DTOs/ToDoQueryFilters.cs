namespace ToDo_Web_APi.DTOs;

public class ToDoQueryFilters
{
    [FromQuery(Name = "search")] public string? Search { get; set; }
    [FromQuery(Name = "completed")] public bool? IsCompleted { get; set; }
}