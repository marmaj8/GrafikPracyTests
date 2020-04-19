using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GrafikPracyTest
{
    class PositionPageObject
    {
        public PositionPageObject()
        {
            PageFactory.InitElements(PropertiesCollection.driver, this);

            PropertiesCollection.wait = new WebDriverWait(PropertiesCollection.driver, new TimeSpan(0, 0, 10));
            PropertiesCollection.wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("txtBegin")));
        }

        [FindsBy(How = How.Name, Using = "txtName")]
        public IWebElement txtName { get; set; }

        [FindsBy(How = How.Name, Using = "btnSave")]
        public IWebElement btnSave { get; set; }

        [FindsBy(How = How.Name, Using = "ddlDay")]
        public IWebElement ddlDay { get; set; }

        [FindsBy(How = How.Name, Using = "txtBegin")]
        public IWebElement txtBegin { get; set; }

        [FindsBy(How = How.Name, Using = "txtEnd")]
        public IWebElement txtEnd { get; set; }

        [FindsBy(How = How.Name, Using = "txtMin")]
        public IWebElement txtMin { get; set; }

        [FindsBy(How = How.Name, Using = "txtMax")]
        public IWebElement txtMax { get; set; }

        [FindsBy(How = How.Name, Using = "btnAdd")]
        public IWebElement btnAdd { get; set; }

        public PositionPageObject AddPosition(string name)
        {
            txtName.EnterText(name);
            //PropertiesCollection.wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("btnSave")));
            //btnSave.Clicks();

            IWebElement element = PropertiesCollection.driver.FindElement(By.Name("btnSave"));
            Actions actions = new Actions(PropertiesCollection.driver);
            actions.MoveToElement(element).Click().Perform();

            return new PositionPageObject();
        }

        public void AddRow(string day, string begin, string end, string min, string max)
        {
            Thread.Sleep(1000);
            //ddlDay.SelectDropDown(day);
            txtBegin.EnterText(begin);
            txtEnd.EnterText(end);
            txtMin.EnterText(min);
            txtMax.EnterText(max);
            btnAdd.Clicks();
        }

        public string getDayName(int pos)
        {
            PropertiesCollection.wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("lblDay" + pos)));
            IWebElement el = PropertiesCollection.driver.FindElement(By.Name("lblDay" + pos));
            return el.Text;
        }

        public string getBegin(int pos)
        {
            PropertiesCollection.wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("lblDay" + pos)));
            IWebElement el = PropertiesCollection.driver.FindElement(By.Name("lblBegin" + pos));
            return el.Text;
        }

        public string getEnd(int pos)
        {
            PropertiesCollection.wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("lblDay" + pos)));
            IWebElement el = PropertiesCollection.driver.FindElement(By.Name("lblEnd" + pos));
            return el.Text;
        }

        public string getMin(int pos)
        {
            PropertiesCollection.wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("lblDay" + pos)));
            IWebElement el = PropertiesCollection.driver.FindElement(By.Name("txtMin" + pos));
            return el.GetText();
        }

        public string getMax(int pos)
        {
            PropertiesCollection.wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("lblDay" + pos)));
            IWebElement el = PropertiesCollection.driver.FindElement(By.Name("txtMax" + pos));
            return el.GetText();
        }

        public void setMin(int pos, string val)
        {
            PropertiesCollection.wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("lblDay" + pos)));
            IWebElement el = PropertiesCollection.driver.FindElement(By.Name("txtMin" + pos));
            el.EnterText(val);
        }

        public void setMax(int pos, string val)
        {
            PropertiesCollection.wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("lblDay" + pos)));
            IWebElement el = PropertiesCollection.driver.FindElement(By.Name("txtMax" + pos));
            el.EnterText(val);
        }

        public void deleteRow(int pos)
        {
            PropertiesCollection.wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("lblDay" + pos)));
            IWebElement el = PropertiesCollection.driver.FindElement(By.Name("btnDelete" + pos));
            el.Clicks();
        }

        public void Save()
        {
            IWebElement element = PropertiesCollection.driver.FindElement(By.Name("btnSave"));
            Actions actions = new Actions(PropertiesCollection.driver);
            actions.MoveToElement(element).Click().Perform();
        }
    }
}
