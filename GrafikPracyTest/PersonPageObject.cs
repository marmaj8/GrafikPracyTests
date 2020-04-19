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
    class PersonPageObject
    {
        public PersonPageObject()
        {
            PageFactory.InitElements(PropertiesCollection.driver, this);
            SeleniumUtilityMethods.WaitForNamedElement("chbActiveDay6");
        }

        [FindsBy(How = How.Name, Using = "btnSave")]
        public IWebElement btnSave { get; set; }

        [FindsBy(How = How.Name, Using = "txtName")]
        public IWebElement txtName { get; set; }

        [FindsBy(How = How.Name, Using = "txtSurname")]
        public IWebElement txtSurname { get; set; }

        [FindsBy(How = How.Name, Using = "txtEmail")]
        public IWebElement txtEmail { get; set; }

        [FindsBy(How = How.Name, Using = "txtHours")]
        public IWebElement txtHours { get; set; }

        [FindsBy(How = How.Name, Using = "chbAdmin")]
        public IWebElement chbAdmin { get; set; }

        [FindsBy(How = How.Name, Using = "txtPassword")]
        public IWebElement txtPassword { get; set; }

        [FindsBy(How = How.Name, Using = "tabPositions")]
        public IWebElement tabPositions { get; set; }

        public string getName()
        {
            return txtName.GetText();
        }

        public string getSurname()
        {
            return txtSurname.GetText();
        }

        public string getEmail()
        {
            return txtEmail.GetText();
        }

        public string getHours()
        {
            return txtHours.GetText();
        }

        public Boolean getAdminstrator()
        {
            return chbAdmin.GetBoolean();
        }

        public void setData(string name, string surname, string email, string hours, Boolean? admin, string password)
        {
            if (name != null)
            {
                txtName.Clears();
                txtName.EnterText(name);
            }
            if (surname != null)
            {
                txtSurname.Clears();
                txtSurname.EnterText(surname);
            }
            if (email != null)
            {
                txtEmail.Clears();
                txtEmail.EnterText(email);
            }
            if (hours != null)
            {
                txtHours.Clears();
                txtHours.EnterText(hours);
            }
            if (password != null)
            {
                txtPassword.Clears();
                txtPassword.EnterText(password);
            }
            if (admin != null && chbAdmin.GetBoolean() != admin)
                chbAdmin.Check();
        }

        public void setWorkDay(int day, string begin, string end)
        {
            if (!isWorkDay(day))
            {
                IWebElement chb = PropertiesCollection.driver.FindElement(By.Name("chbActiveDay" + day));
                chb.Check();
                SeleniumUtilityMethods.WaitForJavascript();
            }
            IWebElement tbeg = PropertiesCollection.driver.FindElement(By.Name("txtBegin" + day));
            IWebElement tend = PropertiesCollection.driver.FindElement(By.Name("txtEnd" + day));
            tbeg.EnterText(begin);
            tend.EnterText(end);
        }
        public void unSetWorkDay(int day)
        {
            if (isWorkDay(day))
            {
                IWebElement chb = PropertiesCollection.driver.FindElement(By.Name("chbActiveDay" + day));
                chb.Check();
                SeleniumUtilityMethods.WaitForJavascript();
            }
        }

        public Boolean isWorkDay(int day)
        {
            IWebElement chb = PropertiesCollection.driver.FindElement(By.Name("chbActiveDay" + day));
            return chb.GetBoolean();
        }

        public string getDayBegin(int day)
        {
            if (isWorkDay(day))
            {
                IWebElement tbeg = PropertiesCollection.driver.FindElement(By.Name("txtBegin" + day));
                return tbeg.GetText();
            }
            else
            {
                return String.Empty;
            }
        }

        public string getDayEnd(int day)
        {
            if (isWorkDay(day))
            {
                IWebElement tend = PropertiesCollection.driver.FindElement(By.Name("txtEnd" + day));
                return tend.GetText();
            }
            else
            {
                return String.Empty;
            }
        }

        public void Save()
        {
            btnSave.Clicks();
        }
    }
}
