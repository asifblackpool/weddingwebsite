
using Blackpool.Zengenti.CMS.Models.Canvas.Helpers;
using Blackpool.Zengenti.CMS.Models.Canvas.Paragraphs;
using RazorPageWeddingWebsite.Constants;
using Zengenti.Contensis.Delivery;

namespace RazorPageWeddingWebsite.Helpers
{
    public static class NavigationLinkHelper
    {
        public static string? GetLinkUrl(string? url)
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                string? temp = (url != null) ? url.Replace(WebsiteConstants.SITE_VIEW_PATH, string.Empty).Trim() : url;
                return url;
            }
            return url;
        }

        public static FragmemtParagraph GetLinkUrl(FragmemtParagraph fp)
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                if (fp.Value != null && fp.Value.Count > 0)
                {
                    Values val = fp.Value.First();
                    string? content = val.Type != null ? val.Type.ToString() : string.Empty;
                    if (val.Value != null && ParagraphHelper.CheckListType(content) == StandardListEnum.link)
                    {
                        string? temp = (val.Properties != null && val.Properties.Link != null && val.Properties.Link.Sys != null)
                           ? val.Properties.Link.Sys.Uri : string.Empty;

                        if (val.Properties != null && val.Properties.Link != null && val.Properties.Link.Sys != null && temp != null)
                        {
                            //temp = temp.Replace(WebsiteConstants.SITE_VIEW_PATH, string.Empty);
                            val.Properties.Link.Sys.Uri = temp;
                        }

                    }

                }


            }
            return fp;
        }
    }


}


