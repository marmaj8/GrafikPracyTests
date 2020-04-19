using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikPracyTest
{
    public static class SeleniumGetMethods
    {
        public static string GetText(this IWebElement element)
        {
            return element.GetAttribute("value");
        }

        public static string GetTextFromDDL(this IWebElement element)
        {
            return new SelectElement(element).AllSelectedOptions.SingleOrDefault().Text;
        }

        public static Boolean GetBoolean(this IWebElement element)
        {
            return element.Selected;
        }

        public static int GetTableWidth(this IWebElement element)
        {
            IWebElement table = element.FindElements(By.ClassName("q-table"))[0];
            IWebElement thead = table.FindElement(By.CssSelector("thead"));
            IWebElement tr = thead.FindElement(By.CssSelector("tr"));

            return tr.FindElements(By.XPath("./*")).Count();
        }

        public static int GetTableHeight(this IWebElement element)
        {
            IWebElement table = element.FindElements(By.ClassName("q-table"))[0];
            IWebElement tbody = table.FindElements(By.CssSelector("tbody"))[1];

            return tbody.FindElements(By.XPath("./*")).Count();
        }

        public static string GetTableCell(this IWebElement element, int row, int col)
        {
            IWebElement cell;
            if (row == -1)
            {
                IWebElement table = element.FindElements(By.ClassName("q-table"))[0];
                IWebElement thead = table.FindElement(By.CssSelector("thead"));
                IWebElement tr = thead.FindElement(By.CssSelector("tr"));
                IWebElement th = thead.FindElements(By.CssSelector("th"))[col];
                cell = th;
            }
            else
            {
                IWebElement table = element.FindElements(By.ClassName("q-table"))[0];
                IWebElement tbody = table.FindElements(By.CssSelector("tbody"))[1];
                IWebElement tr = tbody.FindElements(By.CssSelector("tr"))[row];
                IWebElement td = tr.FindElements(By.CssSelector("td"))[col];
                cell = td;
            }
            return cell.Text;
        }
    }
}
