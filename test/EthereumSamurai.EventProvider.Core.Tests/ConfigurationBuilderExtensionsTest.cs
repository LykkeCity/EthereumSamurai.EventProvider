namespace EthereumSamurai.EventProvider.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Configuration;
    using Exceptions;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Configuration.Json;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Moq.Protected;


    [TestClass]
    public class ConfigurationBuilderExtensionsTest
    {
        [TestMethod]
        public void AddLykkeSettings__ConnectionStringNotSpecified__NoExceptionThrown()
        {
            var configurationBuilder = new ConfigurationBuilder();
            
            try
            {
                configurationBuilder.AddLykkeSettings("LykkeSettings");
            }
            catch (Exception e)
            {
                Assert.Fail($"Expected no exception, but got: {e.Message}");
            }
        }

        [TestMethod]
        public void AddLykkeSettings__ConnectionStringSpecifiedValidAndReachable__JsonConfigurationSourceAdded()
        {
            var configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.AddInMemoryCollection(new Dictionary<string, string>
            {
                {"ConnectionStrings:LykkeSettings", "http://localhost/"}
            });

            var httpMessageHandler = new Mock<HttpMessageHandler>();

            httpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content    = new StringContent("{}")
                });

            configurationBuilder.AddLykkeSettings(httpMessageHandler.Object, "LykkeSettings");

            Assert.AreEqual(2, configurationBuilder.Sources.Count);
            Assert.IsInstanceOfType(configurationBuilder.Sources[1], typeof(JsonConfigurationSource));
        }

        [TestMethod]
        public void AddLykkeSettings__ConnectionStringSpecifiedButInvalid__LykkeSettingsExceptionThrown()
        {
            var configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.AddInMemoryCollection(new Dictionary<string, string>
            {
                {"ConnectionStrings:LykkeSettings", "invalid uri"}
            });

            Assert.ThrowsException<LykkeSettingsException>
            (
                () => configurationBuilder.AddLykkeSettings("LykkeSettings")
            );
        }
    }
}
