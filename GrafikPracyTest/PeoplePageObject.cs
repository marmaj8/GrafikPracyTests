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
    class PeoplePageObject
    {
        public PeoplePageObject()
        {
            PageFactory.InitElements(PropertiesCollection.driver, this);
            SeleniumUtilityMethods.WaitForNamedElement("btnAdd");
        }

        [FindsBy(How = How.Name, Using = "txtNameFiltr")]
        public IWebElement txtNameFiltr { get; set; }

        [FindsBy(How = How.Name, Using = "txtSurnameFilter")]
        public IWebElement txtSurnameFilter { get; set; }

        [FindsBy(How = How.Name, Using = "tabPeople")]
        public IWebElement tabPeople { get; set; }

        [FindsBy(How = How.Name, Using = "txtName")]
        public IWebElement txtName { get; set; }

        [FindsBy(How = How.Name, Using = "txtSurname")]
        public IWebElement txtSurname { get; set; }

        [FindsBy(How = How.Name, Using = "txtEmail")]
        public IWebElement txtEmail { get; set; }

        [FindsBy(How = How.Name, Using = "txtPassword")]
        public IWebElement txtPassword { get; set; }

        [FindsBy(How = How.Name, Using = "txtHours")]
        public IWebElement txtHours { get; set; }

        [FindsBy(How = How.Name, Using = "btnAdd")]
        public IWebElement btnAdd { get; set; }

        public void setFilter(string name, string surname)
        {
            txtNameFiltr.EnterText(name);
            txtSurnameFilter.EnterText(surname);
        }

        public PersonPageObject EneterPerson(int row)
        {
            tabPeople.ClickTableRow(row);
            return new PersonPageObject();
        }

        public void AddPerson(string name, string surname, string email, string passwrod, string hours)
        {
            txtName.EnterText(name);
            txtSurname.EnterText(surname);
            txtEmail.EnterText(email);
            txtPassword.EnterText(passwrod);
            txtHours.EnterText(hours);

            btnAdd.Clicks();
        }

        public string GetCellContent(int row, int col)
        {
            return tabPeople.GetTableCell(row, col);
        }
    }
}
