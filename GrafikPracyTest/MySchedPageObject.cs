using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikPracyTest
{
    class MySchedPageObject
    {
        public MySchedPageObject()
        {
            PageFactory.InitElements(PropertiesCollection.driver, this);
            
            PropertiesCollection.wait = new WebDriverWait(PropertiesCollection.driver, new TimeSpan(0, 0, 10));
            PropertiesCollection.wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("txtDate")));
        }

        [FindsBy(How = How.Name, Using = "txtDate")]
        public IWebElement txtDate { get; set; }

        [FindsBy(How = How.Name, Using = "tabSchedule")]
        public IWebElement tabSchedule { get; set; }

        public void SetDate(string date)
        {
            txtDate.EnterDate(date);
            SeleniumUtilityMethods.WaitForJavascript(5);
        }

        public int GetTableHeight()
        {
            return tabSchedule.GetTableHeight();
        }

        public int GetTableWidth()
        {
            return tabSchedule.GetTableWidth();
        }

        public string GetCellContent(int row, int col)
        {
            return tabSchedule.GetTableCell(row, col);
        }
    }
}