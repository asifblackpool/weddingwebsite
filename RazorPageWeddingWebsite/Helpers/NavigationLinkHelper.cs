
using Blackpool.Zengenti.CMS.Helpers;
using Blackpool.Zengenti.CMS.Models.Canvas.Common;
using Blackpool.Zengenti.CMS.Models.Canvas.Helpers;
using Blackpool.Zengenti.CMS.Models.Canvas.Paragraphs;
using RazorPageWeddingWebsite.Constants;
using Zengenti.Contensis.Delivery;

namespace RazorPageWeddingWebsite.Helpers
{
    public static class NavigationLinkHelper
    {

        public static string GetHomeLink()
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Development")
            {
                return string.Format("/{0}", WebsiteConstants.SITE_VIEW_PATH);
            }
            return "/";
        }

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
                        string? temp = (val.FragmentProperties != null && val.FragmentProperties.Link != null )
                           ? val.FragmentProperties.Link.Properties?.Link?.System?.Uri : string.Empty;

                        if (val.FragmentProperties != null && val.FragmentProperties.Link != null && val.FragmentProperties.Link!= null && temp != null)
                        {
                            //temp = temp.Replace(WebsiteConstants.SITE_VIEW_PATH, string.Empty);
                            ContentLink? linktoChange = val.FragmentProperties.Link?.Properties?.Link;
                            if (linktoChange != null && linktoChange.System != null)
                            {
                                linktoChange.System.Uri = temp;
                            }
                        }

                    }

                }


            }
            return fp;
        }
    }


}


