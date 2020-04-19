using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikPracyTest
{
    class PropertiesCollection
    {
        public enum PropertyType
        {
            Id,
            Name,
            LinkText,
            CssName,
            ClassName,
            URL
        }

        // prop tabulator
        public static IWebDriver driver { get; set; }
        public static WebDriverWait wait { get; set; }
    }
}
