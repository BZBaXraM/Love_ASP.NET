using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ToDo_Web_APi.DTOs.Pagination;

public class PaginationRequest
{
    [Range(1, int.MaxValue)]
    [FromQuery(Name = "page")]
    [Required]
    public int Page { get; set; } = 1;

    [Range(1, int.MaxValue)]
    [FromQuery(Name = "pageSize")]
    [Required]
    public int PageSize { get; set; } = 10;
}