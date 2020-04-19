using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikPracyTest
{
    public static class SeleniumSetMethods
    {
        /// <summary>
        /// Rozszerzona metoda sluzaca do wprowadzenia tekstu do kontrolki
        /// </summary>
        /// <param name="element"></param>
        /// <param name="valu"></param>
        public static void EnterText(this IWebElement element, string valu)
        {
            element.SendKeys(valu);
        }

        /// <summary>
        /// Rozszerzona metoda klikajaca na dany element
        /// </summary>
        /// <param name="element"></param>
        public static void Clicks(this IWebElement element)
        {
            element.Click();
        }

        /// <summary>
        /// Rozszerzona metoda zmieniajaca stan checkboxa
        /// </summary>
        /// <param name="element"></param>
        public static void Check(this IWebElement element)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)PropertiesCollection.driver;
            js.ExecuteScript("arguments[0].click();", element);
        }


        /// <summary>
        /// Rozszerzona metoda usuwająca tekst z kontrolki
        /// </summary>
        /// <param name="element"></param>
        public static void Clears(this IWebElement element)
        {
            element.SendKeys(Keys.Control + "a");
            element.SendKeys(Keys.Delete);
        }

        /// <summary>
        /// Rozszerzona metoda klikajaca na dany wiersz tabeli
        /// </summary>
        /// <param name="element"></param>
        public static void ClickTableRow(this IWebElement element, int row)
        {
            SeleniumUtilityMethods.WaitForJavascript();
            IWebElement table = element.FindElements(By.ClassName("q-table"))[0];
            IWebElement tbody = table.FindElements(By.CssSelector("tbody"))[1];
            IWebElement tr = tbody.FindElements(By.CssSelector("tr"))[row];
            tr.Clicks();
        }

        /// <summary>
        /// Rozszerzona metoda wybierajaca pozycje z kontrolki drop down
        /// </summary>
        /// <param name="element"></param>
        /// <param name="valu"></param>
        public static void SelectDropDown(this IWebElement element, string valu)
        {
            new SelectElement(element).SelectByText(valu);
        }
        public static void SelectValueDropDown(this IWebElement element, string valu)
        {
            new SelectElement(element).SelectByValue(valu);
        }

        /// <summary>
        /// Rozszerzona metoda wpisujaca date do kontrolki input typu date
        /// </summary>
        /// <param name="element"></param>
        /// <param name="valu"></param>
        public static void EnterDate(this IWebElement element, string valu)
        {
            string str = valu.Split('-')[0];
            //if (str[0] == '0') str = str.Remove(0);
            element.SendKeys(str);
            element.SendKeys(Keys.Tab);

            str = valu.Split('-')[1];
            if( Int32.Parse(str) == 1)
            {
                element.SendKeys("1");
                element.SendKeys(Keys.Tab);
            }
            else if ( Int32.Parse(str) >= 10)
            {
                element.SendKeys(str);
            }
            else
            {
                str = str.Remove(0,1);
                element.SendKeys(str);
            }

            str = valu.Split('-')[2];
            if (Int32.Parse(str) <= 3)
            {
                element.SendKeys("1");
            }
            else if (Int32.Parse(str) >= 10)
            {
                element.SendKeys(str);
            }
            else
            {
                str = str.Remove(0,1);
                element.SendKeys(str);
            }
            element.SendKeys(Keys.Tab);

            /*
            if (str[0] == '0')
            {
                str = str.Remove(0);
                element.SendKeys(str);
                element.SendKeys(Keys.Tab);
            }
            else
            {
                element.SendKeys(str);
            }

            str = valu.Split('-')[2];
            if (str[0] == '0')
            {
                str = str.Remove(0);
                element.SendKeys(str);
            }
            else
            {
                element.SendKeys(str);
            }
            element.SendKeys(Keys.Tab);
            */

        }
    }
}
