using System;
using Xunit;

namespace Epinova.ResourceProvider.Tests
{
    public class RegisterTests : IClassFixture<CommonCollectionFixture>
    {
        [Fact]
        public void Register_ThrowsOnNullAssembly()
        {
            Assert.Throws<ArgumentNullException>(() => Register.Assembly(null, ResourceType.All));
        }
    }
}