﻿using Moq;
using RazorPageWeddingWebsite.Core.Services.ContentProcessing.Interfaces;
using System.Threading.Tasks;

namespace WeddingWebsite.UnitTests.Mocks
{
    // Mocks/MockTextProcessor.cs

    namespace WeddingWebsite.UnitTests.Mocks
    {
        public static class MockTextProcessor
        {
            public static Mock<ITextProcessor> Create()
            {
                var mock = new Mock<ITextProcessor>();

                // Default setup - returns input unchanged
                mock.Setup(x => x.ProcessAsync(It.IsAny<string>()))
                    .ReturnsAsync((string input) => input);

                return mock;
            }

            public static Mock<ITextProcessor> WithTransformation(
                this Mock<ITextProcessor> mock,
                string transformFrom,
                string transformTo)
            {
                mock.Setup(x => x.ProcessAsync(transformFrom))
                    .ReturnsAsync(transformTo);
                return mock;
            }
        }
    }
}
