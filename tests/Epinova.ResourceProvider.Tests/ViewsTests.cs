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
        public void Register_EmbeddedViewFound_AddsVpp()
        {
            HttpContext.Current = new HttpContext(
                new HttpRequest("", "http://tempuri.org", ""),
                new HttpResponse(new StringWriter())
            );

            VppRegistration.VppRegistrator = VppRegistrator;

            Register.Assembly<ViewsTests>(ResourceType.View);

            Assert.True(_fixture.VppProviders.Count > 0);
        }


        private void VppRegistrator(VirtualPathProvider virtualPathProvider)
        {
            _fixture.VppProviders.Add(virtualPathProvider);
        }
    }
}