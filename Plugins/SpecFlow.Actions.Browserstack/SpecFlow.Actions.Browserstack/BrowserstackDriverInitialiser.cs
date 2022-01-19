﻿using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using SpecFlow.Actions.Browserstack;
using SpecFlow.Actions.Selenium;
using SpecFlow.Actions.Selenium.DriverOptions;
using System;
using System.Collections.Generic;

namespace Specflow.Actions.Browserstack
{
    public class BrowserstackDriverInitialiser : IDriverInitialiser
    {
        private readonly IWebDriverOptions _optionsInitialiser;
        private Uri _browserstackRemoteUri;

        public BrowserstackDriverInitialiser(RemoteWebDriverOptions optionsInitialiser)
        {
            _optionsInitialiser = optionsInitialiser;
            _browserstackRemoteUri = new Uri("https://hub-cloud.browserstack.com/wd/hub/");
        }

        public IWebDriver Initialise(Browser browser, Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            var options = _optionsInitialiser.GetOptions(browser, capabilities, args);

            return new RemoteWebDriver(_browserstackRemoteUri, options.GetCapabilities());
        }
    }
}