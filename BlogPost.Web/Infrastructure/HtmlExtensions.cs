using BlogPost.Web.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace BlogPost.Web.Infrastructure
{
    public static class HtmlExtensions
    {
        public static HtmlString Image(this IHtmlHelper html, ImageModel imageModel)
        {
            var content = Convert.ToBase64String(imageModel.Content);
            var img = $"data:{imageModel.Type};base64,{content}";

            return new HtmlString("<img src='" + img + "'   width='" + 50 + " height='" + 50 + "' />");
        }
    }
}
