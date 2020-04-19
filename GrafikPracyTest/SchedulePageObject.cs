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
    class SchedulePageObject
    {
        public SchedulePageObject()
        {
            PageFactory.InitElements(PropertiesCollection.driver, this);
            SeleniumUtilityMethods.WaitForNamedElement("tabSchedule");
        }

        [FindsBy(How = How.Name, Using = "lblScheduleId")]
        public IWebElement lblScheduleId { get; set; }

        [FindsBy(How = How.Name, Using = "lblDates")]
        public IWebElement lblDates { get; set; }

        [FindsBy(How = How.Name, Using = "lblConfirmed")]
        public IWebElement lblConfirmed { get; set; }

        [FindsBy(How = How.Name, Using = "btnConfirm")]
        public IWebElement btnConfirm { get; set; }

        [FindsBy(How = How.Name, Using = "txtDate")]
        public IWebElement txtDate { get; set; }

        [FindsBy(How = How.Name, Using = "tabSchedule")]
        public IWebElement tabSchedule { get; set; }

        public string getId()
        {
            SeleniumUtilityMethods.WaitForClickableElement("lblScheduleId");
            return lblScheduleId.Text;
        }
        public string getDates()
        {
            SeleniumUtilityMethods.WaitForClickableElement("lblDates");
            return lblDates.Text;
        }
        public string getConfirmed()
        {
            SeleniumUtilityMethods.WaitForClickableElement("lblConfirmed");
            return lblConfirmed.Text;
        }
        public void Confirm()
        {
            btnConfirm.Clicks();
            SeleniumUtilityMethods.WaitForJavascript();
        }
        public void enterDate(string date)
        {
            txtDate.EnterDate(date);
        }

        public string GetCellContent(int row, int col)
        {
            return tabSchedule.GetTableCell(row, col);
        }
    }
}
