using Blackpool.Zengenti.CMS.Models.Weddings;
using Blackpool.Zengenti.CMS.Models.Weddings.Base;
using Microsoft.Extensions.Logging;
using Moq;
using RazorPageWeddingWebsite.Core.Interfaces;
using RazorPageWeddingWebsite.Core.Models;
using RazorPageWeddingWebsite.Services.Breadcrumb;
using RazorPageWeddingWebsite.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WeddingWebsite.UnitTests.Pages
{
    public class BasePageTests
    {
        [Fact]
        public async Task BasePageModel_Can_Get_ChildEntriesAsync()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<BasePageModel<GettingMarriedBlog>>>();
            var mockDataService = new Mock<IDataService<GettingMarriedBlog>>();
            var mockBreadcrumb = new Mock<BreadcrumbService>();
            var mockRepo = new Mock<IContentRepository>();

            var fakeChildren = new List<GettingMarriedBlog>
        {
            new GettingMarriedBlog { Title = "Child 1" },
            new GettingMarriedBlog { Title = "Child 2" }
        };

            mockRepo.Setup(r => r.GetChildEntries<GettingMarriedBlog>("/weddings"))
                    .Returns(fakeChildren);

            mockDataService.Setup(x => x.GetAllAsync("")).ReturnsAsync(fakeChildren);

            var pageModel = new BasePageModel<GettingMarriedBlog>(
                mockLogger.Object,
                mockDataService.Object,
                mockRepo.Object,
                mockBreadcrumb.Object
               );


            await pageModel.OnGetByPathAsync("/weddings");
            var result = pageModel.Items.ToList();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Child 1", result[0].Title);
        }
    }

}
