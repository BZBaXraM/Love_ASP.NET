using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HTML_Helpers.Models.HtmlHelpers;

public static class HtmlListHelpers
{
    public static HtmlString ListFor(this IHtmlHelper helper, IEnumerable<object> enumerable, string listTag = "ul",
        string color = "black",
        float fontSize = 16)
    {
        StringBuilder sb = new();
        sb.Append($@"<{listTag}
        style='color: {color};
        font-size: {fontSize}px;'>");
        foreach (var item in enumerable)
        {
            sb.Append($"<li>{item}</li>");
        }

        sb.Append($"</{listTag}>");

        return new HtmlString(sb.ToString());
    }
}