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
    class MyDataPageObject
    {
        public MyDataPageObject()
        {
            PageFactory.InitElements(PropertiesCollection.driver, this);

            PropertiesCollection.wait = new WebDriverWait(PropertiesCollection.driver, new TimeSpan(0, 0, 10));
            PropertiesCollection.wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("liWorkDays")));
        }

        [FindsBy(How = How.Name, Using = "liData")]
        public IWebElement liData { get; set; }

        [FindsBy(How = How.Name, Using = "liWorkDays")]
        public IWebElement liWorkDays { get; set; }

        [FindsBy(How = How.Name, Using = "lblName")]
        public IWebElement lblName { get; set; }

        [FindsBy(How = How.Name, Using = "lblSurame")]
        public IWebElement lblSurame { get; set; }

        [FindsBy(How = How.Name, Using = "lblId")]
        public IWebElement lblId { get; set; }

        [FindsBy(How = How.Name, Using = "lblEmail")]
        public IWebElement lblEmail { get; set; }

        [FindsBy(How = How.Name, Using = "lblHours")]
        public IWebElement lblHours { get; set; }

        [FindsBy(How = How.Name, Using = "lblAdmin")]
        public IWebElement lblAdmin { get; set; }

        [FindsBy(How = How.Name, Using = "txtPassword")]
        public IWebElement txtPassword { get; set; }

        [FindsBy(How = How.Name, Using = "btnChangePassword")]
        public IWebElement btnChangePassword { get; set; }

        [FindsBy(How = How.ClassName, Using = "q-notification__message")]
        public IWebElement popUp { get; set; }

        public string getName()
        {
            return lblName.Text;
        }
        public string getSurname()
        {
            return lblSurame.Text;
        }
        public string getId()
        {
            return lblId.Text;
        }
        public string getEmail()
        {
            return lblEmail.Text;
        }
        public string getHours()
        {
            return lblHours.Text;
        }
        public string getAdmin()
        {
            return lblAdmin.Text;
        }
        public string getPassword()
        {
            return txtPassword.GetText();
        }
        public void changePassword(string password)
        {
            txtPassword.SendKeys(password);
            btnChangePassword.Clicks();
        }
        public int getDaysCount()
        {
            return liWorkDays.FindElements(By.ClassName("workDay")).Count();
        }
        public string getDayName(int day)
        {
            return liWorkDays.FindElement(By.Name("lblDay" + day)).Text;
        }
        public string getWorkHours(int day)
        {
            return liWorkDays.FindElement(By.Name("lblDayHours" + day)).Text;
        }

        public string getPopUpText()
        {
            SeleniumUtilityMethods.WaitForJavascript();
            try
            {
                return popUp.Text;
            }
            catch
            {
                return null;
            }
        }

    }
}
