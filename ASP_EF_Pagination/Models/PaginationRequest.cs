namespace ASP_EF_Pagination.Models;

public class PaginationRequest
{
    [FromQuery(Name = "page")]
    [Range(1, int.MaxValue)]
    [Required]
    public int Page { get; set; } = 1;

    [FromQuery(Name = "pageSize")]
    [Range(1, int.MaxValue)]
    [Required]
    public int PageSize { get; set; } = 10;
}