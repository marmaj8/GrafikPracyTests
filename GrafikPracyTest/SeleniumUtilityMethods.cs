using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GrafikPracyTest
{
    class SeleniumUtilityMethods
    {
        public static void WaitForJavascript(int seconds)
        {
            Thread.Sleep(1000);
            WebDriverWait wait = new WebDriverWait(PropertiesCollection.driver, new TimeSpan(0,0,seconds));
            wait.Until(driver => (bool)((IJavaScriptExecutor)driver).
            //ExecuteScript("return jQuery.active == 0"));
            ExecuteScript("return window.jQuery == undefined || jQuery.active === 0"));
        }
        public static void WaitForJavascript()
        {
            WaitForJavascript(5);
        }

        public static void WaitForNamedElement(string name)
        {
            WebDriverWait wait = new WebDriverWait(PropertiesCollection.driver, new TimeSpan(0, 0, 5));
            PropertiesCollection.wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name(name)));
        }

        public static void WaitForClickableElement(string name)
        {
            WebDriverWait wait = new WebDriverWait(PropertiesCollection.driver, new TimeSpan(0, 0, 5));
            PropertiesCollection.wait.Until(ExpectedConditions.ElementToBeClickable(By.Name(name)));
        }
    }
}
