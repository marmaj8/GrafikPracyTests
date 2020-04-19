using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikPracyTest
{
    class LoginPageObject
    {
        public LoginPageObject()
        {
            PageFactory.InitElements(PropertiesCollection.driver, this);
        }

        [FindsBy(How = How.Name, Using = "txtLogin")]
        public IWebElement txtLogin { get; set; }

        [FindsBy(How = How.Name, Using = "txtPassword")]
        public IWebElement txtPassword { get; set; }

        [FindsBy(How = How.Name, Using = "btnLogIn")]
        public IWebElement btnLogIn { get; set; }

        [FindsBy(How = How.ClassName, Using  = "q-notification__message")]
        public IWebElement popUp { get; set; }

        public MySchedPageObject Login(string username, string password)
        {
            txtLogin.EnterText(username);
            txtPassword.EnterText(password);
            btnLogIn.Clicks();

            return new MySchedPageObject();
        }

        public void LoginWORedirect(string username, string password)
        {
            txtLogin.EnterText(username);
            txtPassword.EnterText(password);
            btnLogIn.Clicks();
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
