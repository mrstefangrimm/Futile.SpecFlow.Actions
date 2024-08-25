using System;
using FluentAssertions;
using Futile.SpecFlow.Actions.Selenium;
using Futile.SpecFlow.Actions.Selenium.DriverInitialisers;
using Moq;
using OpenQA.Selenium;
using Xunit;

namespace SpecFlow.Actions.Selenium.Tests
{
    public class BrowserDriverTests
    {
        public class BrowserDriverAccessor : BrowserDriver
        {
            public new Lazy<IWebDriver> CurrentWebDriverLazy => base.CurrentWebDriverLazy;

            public BrowserDriverAccessor(IDriverInitializer driverInitializer) : base(driverInitializer)
            {
            }
        }

        [Fact]
        public void Current_NotInstantiatedAfterCreation()
        {
            var InitialiserMock = new Mock<IDriverInitializer>();

            InitialiserMock.Setup(m => m.Initialize()).Returns(new NoopDriver());

            var target = new BrowserDriverAccessor(InitialiserMock.Object);

            target.CurrentWebDriverLazy.IsValueCreated.Should().BeFalse();
        }

        [Fact]
        public void Current_AfterAccessing_Instantiated()
        {
            var InitialiserMock = new Mock<IDriverInitializer>();

            InitialiserMock.Setup(m => m.Initialize()).Returns(new NoopDriver());

            var target = new BrowserDriverAccessor(InitialiserMock.Object);

            var webdriver = target.Current;

            target.CurrentWebDriverLazy.IsValueCreated.Should().BeTrue();
            webdriver.Should().NotBeNull();
        }
    }
}