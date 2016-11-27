using System;
using Xunit;

namespace Epinova.ResourceProvider.Tests
{
    public class LocalizationTests : IClassFixture<CommonCollectionFixture>
    {
        private readonly CommonCollectionFixture _fixture;

        public LocalizationTests(CommonCollectionFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public void Register_ThrowsOnNullAssembly()
        {
            Assert.Throws<ArgumentNullException>(() => Localization.Register(null));
        }

        [Fact]
        public void Register_EmbeddedXmlFound_AddsProvider()
        {
            Localization.Register(typeof(LocalizationTests).Assembly);

            Assert.True(_fixture.LocalizationService.Providers.Count > 0);
        }
    }
}