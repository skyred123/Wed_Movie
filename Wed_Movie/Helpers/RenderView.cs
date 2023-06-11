using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace Wed_Movie.Helpers
{
    public static class RenderView
    {
        public static string RenderViewToStringAsync(string viewPath,string url)
        {

            var html = string.Empty;

            if (System.IO.File.Exists(viewPath))
            {
                html = System.IO.File.ReadAllText(viewPath);
                string formattedHtml = string.Format(html, url);
                return formattedHtml;
            }
            return html;
        }
    }

}
