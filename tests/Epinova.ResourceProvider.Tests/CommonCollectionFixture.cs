using System;
using System.Collections.Generic;
using System.Web.Hosting;
using EPiServer.Framework.Localization;
using EPiServer.ServiceLocation;
using Moq;

namespace Epinova.ResourceProvider.Tests
{
    public class CommonCollectionFixture : IDisposable
    {
        private ProviderBasedLocalizationService _service;

        public CommonCollectionFixture()
        {
            _service = new TestableProviderBasedLocalizationService();
            VppProviders = new List<VirtualPathProvider>();
            Mock<IServiceLocator> locatorMock = new Mock<IServiceLocator>();
            locatorMock.Setup(m => m.GetInstance<LocalizationService>()).Returns(_service);
            ServiceLocator.SetLocator(locatorMock.Object);
        }


        public ProviderBasedLocalizationService LocalizationService => _service;

        public List<VirtualPathProvider> VppProviders { get; private set; }


        public void Dispose()
        {
            _service = null;
            VppProviders = null;
            ServiceLocator.SetLocator(null);
        }
    }
}