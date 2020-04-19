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
    class MenuLayoutObject
    {
        public MenuLayoutObject()
        {
            PageFactory.InitElements(PropertiesCollection.driver, this);
        }

        [FindsBy(How = How.Name, Using = "btnShowMenu")]
        public IWebElement btnShowMenu { get; set; }

        [FindsBy(How = How.Name, Using = "btnDataPage")]
        public IWebElement btnDataPage { get; set; }

        [FindsBy(How = How.Name, Using = "btnMySchedulePage")]
        public IWebElement btnMySchedulePage { get; set; }

        [FindsBy(How = How.Name, Using = "btnPeoplePage")]
        public IWebElement btnPeoplePage { get; set; }

        [FindsBy(How = How.Name, Using = "btnSchedulesPage")]
        public IWebElement btnSchedulesPage { get; set; }

        [FindsBy(How = How.Name, Using = "btnPositionsPage")]
        public IWebElement btnPositionsPage { get; set; }

        [FindsBy(How = How.Name, Using = "btnLeavePage")]
        public IWebElement btnLeavePage { get; set; }

        [FindsBy(How = How.Name, Using = "btnLogOut")]
        public IWebElement btnLogOut { get; set; }

        public void OpenMenu()
        {
            btnShowMenu.Clicks();
        }

        public MyDataPageObject EnterDataPage()
        {
            btnShowMenu.Clicks();
            btnDataPage.Clicks();

            return new MyDataPageObject();
        }

        public LeavePageObject EnterLeavesage()
        {
            btnShowMenu.Clicks();
            btnLeavePage.Clicks();

            return new LeavePageObject();
        }

        public LoginPageObject LogOut()
        {
            btnLogOut.Click();

            return new LoginPageObject();
        }

        public PositionsPageObject EnterPositions()
        {
            btnShowMenu.Clicks();
            SeleniumUtilityMethods.WaitForNamedElement("btnPositionsPage");
            btnPositionsPage.Click();

            return new PositionsPageObject();
        }

        public PeoplePageObject EnterPeople()
        {
            btnShowMenu.Clicks();
            SeleniumUtilityMethods.WaitForNamedElement("btnPeoplePage");
            btnPeoplePage.Click();

            return new PeoplePageObject();
        }

        public SchedulesPageObject EnterSchedules()
        {
            btnShowMenu.Clicks();
            SeleniumUtilityMethods.WaitForNamedElement("btnSchedulesPage");
            btnSchedulesPage.Click();

            return new SchedulesPageObject();
        }
    }
}
