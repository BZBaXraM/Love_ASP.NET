using Microsoft.AspNetCore.Mvc.Filters;

namespace Filters_.Filters;

public class DateTimeExcAttribute : Attribute, IResultFilter
{
    public void OnResultExecuted(ResultExecutedContext context)
    {
    }

    public void OnResultExecuting(ResultExecutingContext context)
    {
        var dateTimeHeaderValue = DateTime.Now.ToString("ddd-dd-MMM-yyyy");
        var encodedDateTimeHeaderValue = Uri.EscapeDataString(dateTimeHeaderValue);

        context
            .HttpContext
            .Response
            .Headers
            .Add("DateTime", encodedDateTimeHeaderValue);
    }

}