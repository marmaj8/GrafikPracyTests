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
    class SchedulesPageObject
    {
        public SchedulesPageObject()
        {
            PageFactory.InitElements(PropertiesCollection.driver, this);
            SeleniumUtilityMethods.WaitForNamedElement("tabSchedules");
        }

        [FindsBy(How = How.Name, Using = "txtDateBegin")]
        public IWebElement txtDateBegin { get; set; }

        [FindsBy(How = How.Name, Using = "txtDateEnd")]
        public IWebElement txtDateEnd { get; set; }

        [FindsBy(How = How.Name, Using = "chbConfirmed")]
        public IWebElement chbConfirmed { get; set; }

        [FindsBy(How = How.Name, Using = "tabSchedules")]
        public IWebElement tabSchedules { get; set; }

        [FindsBy(How = How.Name, Using = "txtNewDateBegin")]
        public IWebElement txtNewDateBegin { get; set; }

        [FindsBy(How = How.Name, Using = "txtNewDateEnd")]
        public IWebElement txtNewDateEnd { get; set; }

        [FindsBy(How = How.Name, Using = "txtNumbers")]
        public IWebElement txtNumbers { get; set; }

        [FindsBy(How = How.Name, Using = "btnGenerate")]
        public IWebElement btnGenerate { get; set; }

        public void FilterTable(string begin, string end, Boolean confirmed)
        {
            if (begin != null)
            {
                txtDateBegin.EnterDate(begin);
            }
            if (end != null)
            {
                txtDateEnd.EnterDate(end);
            }
            if(chbConfirmed.GetBoolean() != confirmed)
            {
                chbConfirmed.Check();
            }
        }

        public string GetCellContent(int row, int col)
        {
            return tabSchedules.GetTableCell(row, col);
        }

        public void Generate(string begin, string end, string number)
        {
            txtNewDateBegin.EnterDate(begin);
            txtNewDateEnd.EnterDate(end);
            txtNumbers.EnterText(number);
            btnGenerate.Clicks();
        }

        public SchedulePageObject EnterSchedule(int row)
        {
            tabSchedules.ClickTableRow(row);

            return new SchedulePageObject();
        }
    }
}
