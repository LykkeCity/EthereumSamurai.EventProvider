namespace EthereumSamurai.EventProvider.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Hosting;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Service.Hosting.Interfaces;


    [TestClass]
    public class CoreHostTest
    {
        [TestMethod]
        public void Dispose__ApiHostAndServiceHostDisposed()
        {
            var apiHost             = new Mock<IWebHost>();
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
            var apiHost     = new Mock<IWebHost>();
            var serviceHost = new Mock<IServiceHost>();

            var startSequence = new List<Type>(2);

            apiHost.Setup(x => x.Start()).Callback(() =>
            {
                startSequence.Add(typeof(IWebHost));
            });

            serviceHost.Setup(x => x.Start()).Callback(() =>
            {
                startSequence.Add(typeof(IServiceHost));
            });

            (new CoreHost(apiHost.Object, serviceHost.Object)).Start();

            Assert.AreEqual(typeof(IServiceHost), startSequence[0]);
            Assert.AreEqual(typeof(IWebHost),     startSequence[1]);
        }

        [TestMethod]
        public void StopAsync__ApiHostShouldBeStoppedBeforeServiceHost()
        {
            var apiHost     = new Mock<IWebHost>();
            var serviceHost = new Mock<IServiceHost>();

            var stopSequence = new List<Type>(2);

            apiHost
                .Setup(x => x.StopAsync(CancellationToken.None))
                .Callback(() => { stopSequence.Add(typeof(IWebHost)); })
                .Returns(Task.CompletedTask);

            serviceHost
                .Setup(x => x.StopAsync())
                .Callback(() => { stopSequence.Add(typeof(IServiceHost));})
                .Returns(Task.CompletedTask);

            (new CoreHost(apiHost.Object, serviceHost.Object)).StopAsync().Wait();

            Assert.AreEqual(typeof(IWebHost),     stopSequence[0]);
            Assert.AreEqual(typeof(IServiceHost), stopSequence[1]);
        }
    }
}