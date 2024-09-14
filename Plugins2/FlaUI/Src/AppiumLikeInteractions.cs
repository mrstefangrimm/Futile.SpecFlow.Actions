using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;

namespace Futile.Specflow.Actions.FlaUI
{
    /// <summary>
    /// Use an Appium-like interaction interface for convenience.
    /// </summary>
    public class AppiumLikeInteractions
    {
        protected readonly FlaUIDriver _driver;

        public AppiumLikeInteractions(FlaUIDriver driver)
        {
            _driver = driver;
        }

        /// <summary>
        /// <code>_driver.Current.FindFirstDescendant(_driver.Get.ByAutomationId(id))</code>
        /// </summary>
        public AutomationElement FindElementByAccessibilityId(string id)
        {
            return FirstDescendant(_driver.Get.ByAutomationId(id));
        }

        /// <summary>
        /// <code>_driver.Current.FindAllDescendants(_driver.Get.ByAutomationId(id))</code>
        /// </summary>
        public AutomationElement[] FindElementsByAccessibilityId(string id)
        {
            return Descendants(_driver.Get.ByAutomationId(id));
        }

        /// <summary>
        /// <code>_driver.Current.FindFirstDescendant(_driver.Get.ByName(name))</code>
        /// </summary>
        public AutomationElement FindElementByName(string name)
        {
            return FirstDescendant(_driver.Get.ByName(name));
        }

        /// <summary>
        /// <code>_driver.Current.FindAllDescendants(_driver.Get.ByName(name))</code>
        /// </summary>
        public AutomationElement[] FindElementsByName(string name)
        {
            return Descendants(_driver.Get.ByName(name));
        }

        /// <summary>
        /// <code>_driver.Current.FindFirstDescendant(_driver.Get.ByClassName(className))</code>
        /// </summary>
        public AutomationElement FindElementByClassName(string className)
        {
            return FirstDescendant(_driver.Get.ByClassName(className));
        }

        /// <summary>
        /// <code>_driver.Current.FindAllDescendants(_driver.Get.ByClassName(className))</code>
        /// </summary>
        public AutomationElement[] FindElementsByClassName(string className)
        {
            return Descendants(_driver.Get.ByClassName(className));
        }

        private AutomationElement FirstDescendant(ConditionBase condition)
        {
            return _driver.Current.FindFirstDescendant(condition);
        }

        private AutomationElement[] Descendants(ConditionBase condition)
        {
            return _driver.Current.FindAllDescendants(condition);
        }
    }
}