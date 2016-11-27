using System.Collections.Generic;
using EPiServer.Framework.Localization;

namespace Epinova.ResourceProvider.Tests
{
    public class TestableProviderBasedLocalizationService : ProviderBasedLocalizationService
    {
        public TestableProviderBasedLocalizationService()
        {
            Providers = new List<LocalizationProvider>();
        }
    }
}