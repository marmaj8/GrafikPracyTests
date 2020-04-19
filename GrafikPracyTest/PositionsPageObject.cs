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
    class PositionsPageObject
    {
        public PositionsPageObject()
        {
            PageFactory.InitElements(PropertiesCollection.driver, this);

            PropertiesCollection.wait = new WebDriverWait(PropertiesCollection.driver, new TimeSpan(0, 0, 10));
            PropertiesCollection.wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.Name("tabPositions")));
        }

        [FindsBy(How = How.Name, Using = "txtNameFiltr")]
        public IWebElement txtNameFiltr { get; set; }

        [FindsBy(How = How.Name, Using = "btnAdd")]
        public IWebElement btnAdd { get; set; }

        [FindsBy(How = How.Name, Using = "tabPositions")]
        public IWebElement tabPositions { get; set; }

        public PositionPageObject AddPosition()
        {
            btnAdd.Clicks();
            return new PositionPageObject();
        }

        public void FilterData(string filrt)
        {
            txtNameFiltr.EnterText(filrt);
        }

        public string GetCellContent(int row, int col)
        {
            return tabPositions.GetTableCell(row, col);
        }

        public PositionPageObject EnterPosition(int row)
        {
            tabPositions.ClickTableRow(row);
            return new PositionPageObject();
        }
    }
}
