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
    class LeavePageObject
    {
        public LeavePageObject()
        {
            PageFactory.InitElements(PropertiesCollection.driver, this);

            PropertiesCollection.wait = new WebDriverWait(PropertiesCollection.driver, new TimeSpan(0, 0, 10));
            PropertiesCollection.wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("txtBegin")));
        }

        [FindsBy(How = How.Name, Using = "txtDate")]
        public IWebElement txtDate { get; set; }

        [FindsBy(How = How.Name, Using = "chbConfirmed")]
        public IWebElement chbConfirmed { get; set; }

        [FindsBy(How = How.Name, Using = "chbMy")]
        public IWebElement chbMy { get; set; }

        [FindsBy(How = How.Name, Using = "tabLeaves")]
        public IWebElement tabLeaves { get; set; }

        [FindsBy(How = How.Name, Using = "txtBegin")]
        public IWebElement txtBegin { get; set; }

        [FindsBy(How = How.Name, Using = "txtEnd")]
        public IWebElement txtEnd { get; set; }

        [FindsBy(How = How.Name, Using = "txtPurpose")]
        public IWebElement txtPurpose { get; set; }

        [FindsBy(How = How.Name, Using = "btnAdd")]
        public IWebElement btnAdd { get; set; }

        [FindsBy(How = How.Name, Using = "lblConfirmConfirm")]
        public IWebElement lblConfirmConfirm { get; set; }

        [FindsBy(How = How.Name, Using = "btnCancelConfirm")]
        public IWebElement btnCancelConfirm { get; set; }

        [FindsBy(How = How.Name, Using = "btnConfirmConfirm")]
        public IWebElement btnConfirmConfirm { get; set; }

        public void ChangeOnlyConfirmed()
        {
            SeleniumUtilityMethods.WaitForNamedElement("chbConfirmed");
            chbConfirmed.Check();
        }
        public void ChangeOnlyMy()
        {
            SeleniumUtilityMethods.WaitForNamedElement("chbMy");
            //chbMy.Click();
            chbMy.Check();
        }
        public void SetDate(string date)
        {
            txtDate.EnterDate(date);
        }
        public void AddLeave(string begin, string end, string purpose)
        {
            txtBegin.EnterDate(begin);
            txtEnd.EnterDate(end);
            txtPurpose.EnterText(purpose);
            btnAdd.Click();
        }
        public void Confirm(int row)
        {
            tabLeaves.ClickTableRow(row);
            btnConfirmConfirm.Clicks();
        }
        public void PretendConfirm(int row)
        {
            tabLeaves.ClickTableRow(row);
            btnCancelConfirm.Clicks();
        }
        public string GetConfirmText(int row)
        {
            tabLeaves.ClickTableRow(row);
            string txt = lblConfirmConfirm.Text;
            btnCancelConfirm.Clicks();
            return txt;
        }
        public string GetCellContent(int row, int col)
        {
            return tabLeaves.GetTableCell(row, col);
        }
    }
}
