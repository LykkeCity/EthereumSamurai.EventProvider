namespace EthereumSamurai.EventProvider.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Api.Hosting;
    using Hosting;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Service.Hosting;


    [TestClass]
    public class CoreHostTest
    {
        [TestMethod]
        public void Dispose__ApiHostAndServiceHostDisposed()
        {
            var apiHost             = new Mock<IApiHost>();
            var apiHostDisposed     = false;
            var serviceHost         = new Mock<IServiceHost>();
            var serviceHostDisposed = false;
            
            apiHost.Setup(x => x.Dispose()).Callback(() =>
            {
                apiHostDisposed = true;
            });

            serviceHost.Setup(x => x.Dispose()).Callback(() =>
            {
                serviceHostDisposed = true;
            });

            (new CoreHost(apiHost.Object, serviceHost.Object)).Dispose();

            Assert.IsTrue(apiHostDisposed);
            Assert.IsTrue(serviceHostDisposed);
        }

        [TestMethod]
        public void Start__ServiceHostStartedBeforeApiHost()
        {
            var apiHost     = new Mock<IApiHost>();
            var serviceHost = new Mock<IServiceHost>();

            var startSequence = new List<Type>(2);

            apiHost.Setup(x => x.Start()).Callback(() =>
            {
                startSequence.Add(typeof(IApiHost));
            });

            serviceHost.Setup(x => x.Start()).Callback(() =>
            {
                startSequence.Add(typeof(IServiceHost));
            });

            (new CoreHost(apiHost.Object, serviceHost.Object)).Start();

            Assert.AreEqual(typeof(IServiceHost), startSequence[0]);
            Assert.AreEqual(typeof(IApiHost),     startSequence[1]);
        }

        [TestMethod]
        public void StopAsync__ApiHostShouldBeStoppedBeforeServiceHost()
        {
            var apiHost     = new Mock<IApiHost>();
            var serviceHost = new Mock<IServiceHost>();

            var stopSequence = new List<Type>(2);

            apiHost
                .Setup(x => x.StopAsync())
                .Callback(() => { stopSequence.Add(typeof(IApiHost)); })
                .Returns(Task.CompletedTask);

            serviceHost
                .Setup(x => x.StopAsync())
                .Callback(() => { stopSequence.Add(typeof(IServiceHost));})
                .Returns(Task.CompletedTask);

            (new CoreHost(apiHost.Object, serviceHost.Object)).StopAsync().Wait();

            Assert.AreEqual(typeof(IApiHost),     stopSequence[0]);
            Assert.AreEqual(typeof(IServiceHost), stopSequence[1]);
        }
    }
}