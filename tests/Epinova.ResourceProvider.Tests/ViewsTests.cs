using System;
using System.IO;
using System.Web;
using System.Web.Hosting;
using Epinova.ResourceProvider.Registration;
using Xunit;

namespace Epinova.ResourceProvider.Tests
{
    public class ViewsTests : IClassFixture<CommonCollectionFixture>
    {
        private readonly CommonCollectionFixture _fixture;

        public ViewsTests(CommonCollectionFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Register_ThrowsOnNullAssembly()
        {
            Assert.Throws<ArgumentNullException>(() => Views.Register(null));
        }

        [Fact]
        public void Register_EmbeddedViewFound_AddsVpp()
        {
            HttpContext.Current = new HttpContext(
                new HttpRequest("", "http://tempuri.org", ""),
                new HttpResponse(new StringWriter())
            );

            VppRegistration.VppRegistrator = VppRegistrator;

            Views.Register(typeof(LocalizationTests).Assembly);

            Assert.True(_fixture.VppProviders.Count > 0);
        }

        private void VppRegistrator(VirtualPathProvider virtualPathProvider)
        {
            _fixture.VppProviders.Add(virtualPathProvider);
        }
    }
}