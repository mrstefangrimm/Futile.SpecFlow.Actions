﻿using SpecFlow.Actions.Selenium;
using SpecFlow.Actions.Selenium.DriverOptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace SpecFlow.Actions.Browserstack
{
    public class RemoteWebDriverOptions : IWebDriverOptions
    {
        private readonly ScenarioContext _scenarioContext;
        private static Lazy<string?> BrowserstackUsername => new(() => Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME"));
        private static Lazy<string?> AccessKey => new(() => Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY"));

        public RemoteWebDriverOptions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        public IOptionsWrapper GetOptions(Browser browser, Dictionary<string, string>? capabilities = null, string[]? args = null)
        {
            IOptionsWrapper options = browser switch
            {
                Browser.Chrome => new ChromeOptionsWrapper(),
                Browser.Firefox => new FirefoxOptionsWrapper(),
                Browser.Edge => new EdgeOptionsWrapper(),
                Browser.InternetExplorer => new InternetExplorerOptionsWrapper(),
                Browser.Safari => new SafariOptionsWrapper(),
                _ => throw new ArgumentOutOfRangeException(nameof(browser), browser, null),
            };

            if (BrowserstackUsername.Value is not null && AccessKey.Value is not null)
            {
                options.AddAdditionalCapability("browserstack.user", BrowserstackUsername.Value);
                options.AddAdditionalCapability("browserstack.key", AccessKey.Value); 
            }

            options.AddAdditionalCapability("name", GetScenarioTitle());

            if (capabilities.Any() && capabilities is not null)
            {
                foreach (var capability in capabilities)
                {
                    options.AddAdditionalCapability(capability.Key, capability.Value);
                }
            }

            return options;
        }

        private string GetScenarioTitle()
        {
            var testName = _scenarioContext.ScenarioInfo.Title;

            if (_scenarioContext.ScenarioInfo.Arguments.Count > 0)
            {
                testName += ": ";
            }

            foreach (DictionaryEntry argument in _scenarioContext.ScenarioInfo.Arguments)
            {
                testName += argument.Key + ":" + argument.Value + "; ";
            }

            return testName.Trim();
        }
    }
}