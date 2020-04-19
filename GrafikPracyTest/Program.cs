using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikPracyTest
{
    [TestFixture]
    class Program
    {
        string BaseUrl = "http://localhost:8081/";

        [SetUp]
        public void Initialize()
        {
            PropertiesCollection.driver = new ChromeDriver();
            PropertiesCollection.driver.Navigate().GoToUrl(BaseUrl);

            Console.WriteLine("Otwarcie strony");
        }

        [Test]
        [TestCase("admin@admin.pl", "admin")]
        [TestCase("user@user.pl", "user1")]
        public void LogInPage_LogIn_RedirectToMySchedule(string login, string password)
        {
            LoginPageObject loginPage = new LoginPageObject();
            MySchedPageObject mSchedPage = loginPage.Login(login, password);

            string url = PropertiesCollection.driver.Url;

            StringAssert.IsMatch(BaseUrl + "#", url);
        }

        [Test]
        [TestCase("admin@admin.pl", "admina")]
        [TestCase("admin@admin.pl", "admi")]
        [TestCase("admin@admin.pl", "admix")]
        [TestCase("niema@niema.pl", "niema")]
        public void LogIn_WrongLogin_WrongPasswordPopUp(string login, string password)
        {
            LoginPageObject loginPage = new LoginPageObject();
            loginPage.LoginWORedirect(login, password);

            string txt = loginPage.getPopUpText();

            StringAssert.IsMatch("Nieprawidłowy Email lub Hasło", txt);
        }

        [Test]
        [TestCase("admin")]
        [TestCase("admina")]
        [TestCase("admi")]
        [TestCase("admix")]
        [TestCase("")]
        public void LogIn_NoEmail_StayOnPage(string password)
        {
            LoginPageObject loginPage = new LoginPageObject();

            try
            {
                MySchedPageObject mSchedPage = loginPage.Login("", password);
            }
            catch
            { }

            string url = PropertiesCollection.driver.Url;

            StringAssert.IsMatch(BaseUrl + "#/login", url);
        }

        [Test]
        [TestCase("admin@admin.pl")]
        [TestCase("user@user.pl")]
        [TestCase("")]
        public void LogIn_NoPassword_StayOnPage(string login)
        {
            LoginPageObject loginPage = new LoginPageObject();

            try
            {
                MySchedPageObject mSchedPage = loginPage.Login(login, "");
            }
            catch
            { }

            string url = PropertiesCollection.driver.Url;

            StringAssert.IsMatch(BaseUrl + "#/login", url);
        }

        [Test]
        [TestCase("admin@admin.pl","admin","Kasjer","2020-04-06",8)]
        [TestCase("user@user.pl", "user1", "Kasjer,Sprzatacz", "2020-04-06", 8)]
        [TestCase("admin@admin.pl", "admin", "Kasjer", "2010-04-06", 0)]
        [TestCase("user@user.pl", "user1", "Kasjer,Sprzatacz", "2010-04-06", 0)]
        [TestCase("admin@admin.pl", "admin", "Kasjer", "2020-11-06", 0)]
        [TestCase("user@user.pl", "user1", "Kasjer,Sprzatacz", "2020-11-06", 0)]
        [TestCase("admin@admin.pl", "admin", "Kasjer", "2020-12-24", 0)]
        [TestCase("user@user.pl", "user1", "Kasjer,Sprzatacz", "2020-12-24", 0)]
        public void MySchedulePage_UsserAndDate_ScheduleForUser(string login, string password, string positions, string date, int rows)
        {
            LoginPageObject loginPage = new LoginPageObject();
            MySchedPageObject mSchedPage = loginPage.Login(login, password);

            mSchedPage.SetDate(date);

            int width = mSchedPage.GetTableWidth();
            int height = mSchedPage.GetTableHeight();

            String[,] table = new String[height+1, width];
            for(int i = 0; i < height+1; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    table[i, j] = mSchedPage.GetCellContent(i-1, j);
                }
            }
            positions = "(" + positions.Replace(",", ")|(") + ".?)*";

            Assert.That(width == 2);
            Assert.That(height == rows);
            for(int i = 0; i < height+1; i++)
            {
                if (i == 0)
                {
                    StringAssert.IsMatch("Godzina", table[0, 0]);
                    StringAssert.IsMatch("Stanowiska", table[0, 1]);
                }
                else
                {
                    StringAssert.IsMatch("[0-2][0-9]:[0-6][0-9] - [0-2][0-9]:[0-6][0-9]", table[i, 0]);
                    StringAssert.IsMatch(positions, table[i, 1]);
                }
            }
        }

        [Test]
        public void Menu_ByAdmin_AllButtons()
        {
            LoginPageObject loginPage = new LoginPageObject();
            MySchedPageObject mSchedPage = loginPage.Login("admin@admin.pl", "admin");
            MenuLayoutObject menu = new MenuLayoutObject();
            menu.OpenMenu();

            string myData = menu.btnDataPage.Text;
            string mySchedule = menu.btnMySchedulePage.Text;
            string peoples = menu.btnPeoplePage.Text;
            string schedules = menu.btnSchedulesPage.Text;
            string positions = menu.btnPositionsPage.Text;
            string leave = menu.btnLeavePage.Text;

            StringAssert.IsMatch("Moje Dane", myData);
            StringAssert.IsMatch("Mój Grafik", mySchedule);
            StringAssert.IsMatch("Pracownicy", peoples);
            StringAssert.IsMatch("Grafiki", schedules);
            StringAssert.IsMatch("Stanowiska", positions);
            StringAssert.IsMatch("Urlopy", leave);
        }

        [Test]
        public void Menu_ByUser_ThreeBtons()
        {
            LoginPageObject loginPage = new LoginPageObject();
            MySchedPageObject mSchedPage = loginPage.Login("user@user.pl", "user1");
            MenuLayoutObject menu = new MenuLayoutObject();
            menu.OpenMenu();

            string myData = menu.btnDataPage.Text;
            string mySchedule = menu.btnMySchedulePage.Text;
            string leave = menu.btnLeavePage.Text;

            StringAssert.IsMatch("Moje", myData);
            StringAssert.IsMatch("Mój Grafik", mySchedule);
            StringAssert.IsMatch("Urlopy", leave);
            Assert.That(() => menu.btnPeoplePage.Text, Throws.Exception);
            Assert.That(() => menu.btnSchedulesPage.Text, Throws.Exception);
            Assert.That(() => menu.btnPositionsPage.Text, Throws.Exception);
        }

        [Test]
        public void Menu_ByGuest_NoButtons()
        {
            //LoginPageObject loginPage = new LoginPageObject();
            //MySchedPageObject mSchedPage = loginPage.Login("user@user.pl", "user");
            MenuLayoutObject menu = new MenuLayoutObject();
            menu.OpenMenu();

            Assert.That(() => menu.btnDataPage.Text, Throws.Exception);
            Assert.That(() => menu.btnMySchedulePage.Text, Throws.Exception);
            Assert.That(() => menu.btnLeavePage.Text, Throws.Exception);
            Assert.That(() => menu.btnPeoplePage.Text, Throws.Exception);
            Assert.That(() => menu.btnSchedulesPage.Text, Throws.Exception);
            Assert.That(() => menu.btnPositionsPage.Text, Throws.Exception);
        }

        [Test]
        [TestCase("admin@admin.pl", "admin", "admin","admin","1", true, "160", "0111110")]
        [TestCase("user@user.pl", "user1", "user", "user", "2", false, "160", "0111110")]
        public void MyData_ByUser_UsersData(string email, string password, string name, string surname, string id, Boolean admin, string hours, string days)
        {
            LoginPageObject loginPage = new LoginPageObject();
            MySchedPageObject mSchedPage = loginPage.Login(email, password);
            MenuLayoutObject menu = new MenuLayoutObject();
            MyDataPageObject mdataPage = menu.EnterDataPage();

            string pname = mdataPage.getName();
            string psurname = mdataPage.getSurname();
            string pid = mdataPage.getId();
            string pemail = mdataPage.getEmail();
            string phours = mdataPage.getHours();
            int pdaysCount = mdataPage.getDaysCount();

            int daysCount = days.Count(c => c == '1');

            Assert.That(pdaysCount == daysCount);
            StringAssert.IsMatch(pname, name);
            StringAssert.IsMatch(psurname, surname);
            StringAssert.IsMatch(pid, id);
            StringAssert.IsMatch(pemail, email);
            StringAssert.IsMatch(phours, hours);
            if (admin)
            {
                string padmin = mdataPage.getAdmin();
                StringAssert.IsMatch(padmin, "Posiada Uprawnienia Administratora");
            }
            else
            {
                Assert.That(() => mdataPage.getAdmin(), Throws.Exception);
            }
            
            for(int i = 0; i<7; i++)
            {
                if(days[i]=='1')
                {
                    StringAssert.IsMatch("(niedziela)|(poniedziałek)|(wtorek)|(środa)|(czwartek)|(piątek)|(sobota)", mdataPage.getDayName(i));
                    StringAssert.IsMatch("[0-2][0-9]:[0-6][0-9] - [0-2][0-9]:[0-6][0-9]", mdataPage.getWorkHours(i));
                }
            }
        }

        [Test]
        [TestCase("user@user.pl","user1","user2")]
        [TestCase("user@user.pl", "user1", "1234567890")]
        public void MyData_ChangePassword_PasswordChanged(string email, string password, string newPassword)
        {
            LoginPageObject loginPage = new LoginPageObject();
            MySchedPageObject mSchedPage = loginPage.Login(email, password);
            MenuLayoutObject menu = new MenuLayoutObject();
            MyDataPageObject mdataPage = menu.EnterDataPage();

            mdataPage.changePassword(newPassword);

            loginPage = menu.LogOut();
            mSchedPage = loginPage.Login(email, newPassword);
            mdataPage = menu.EnterDataPage();

            mdataPage.changePassword(password);
        }

        [Test]
        [TestCase("user@user.pl", "user1", "123")]
        [TestCase("user@user.pl", "user1", "user")]
        public void MyData_ChangeToShortPassword_PopUpWrongPasswordAndCannotLoginWithNewPassword(string email, string password, string newPassword)
        {
            LoginPageObject loginPage = new LoginPageObject();
            MySchedPageObject mSchedPage = loginPage.Login(email, password);
            MenuLayoutObject menu = new MenuLayoutObject();
            MyDataPageObject mdataPage = menu.EnterDataPage();

            mdataPage.changePassword(newPassword);
            SeleniumUtilityMethods.WaitForJavascript();
            string txt = mdataPage.getPopUpText();

            loginPage = menu.LogOut();

            StringAssert.IsMatch("Hasło musi zawierać conajmniej 5 znaków.",txt);
            Assert.That(() => loginPage.Login(email,newPassword), Throws.Exception);

        }

        [Test]
        [TestCase("user@user.pl", "user1", "user user", "2020-04-20","2020-04-22","test", false)]
        [TestCase("admin@admin.pl", "admin", "admin admin", "2020-04-20", "2020-04-22", "test", true)]
        public void Leaves_Add_LeaveAdded(string email, string password, string name, string begin, string end, string purpose, Boolean admin)
        {
            LoginPageObject loginPage = new LoginPageObject();
            MySchedPageObject mySchedPage = loginPage.Login(email, password);
            MenuLayoutObject menu = new MenuLayoutObject();
            LeavePageObject leavesPage = menu.EnterLeavesage();

            leavesPage.SetDate(begin);
            leavesPage.AddLeave(begin, end, purpose);
            SeleniumUtilityMethods.WaitForJavascript();

            int i = 0;
            string pId = leavesPage.GetCellContent(0, i);
            i++;
            string pPerson ="";
            if (admin)
            {
                pPerson = leavesPage.GetCellContent(0, i);
                i++;
            }
            string pBegin = leavesPage.GetCellContent(0, i);
            i++;
            string pEnd = leavesPage.GetCellContent(0, i);
            i++;
            string pPurpose = leavesPage.GetCellContent(0, i);
            i++;
            string pConfirm = leavesPage.GetCellContent(0, i);

            string rBegin = begin.Split('-')[2] + "." + begin.Split('-')[1] + "." + begin.Split('-')[0];
            string rEnd = end.Split('-')[2] + "." + end.Split('-')[1] + "." + end.Split('-')[0];

            StringAssert.IsMatch("[0-9]*", pId);
            if (admin)
            {
                StringAssert.IsMatch(name, pPerson);
            }
            StringAssert.IsMatch(rBegin, pBegin);
            StringAssert.IsMatch(rEnd, pEnd);
            StringAssert.IsMatch(purpose, pPurpose);
            StringAssert.IsMatch("NIE", pConfirm);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void Leaves_Confirm_LeaveConfirmed(int row)
        {
            LoginPageObject loginPage = new LoginPageObject();
            MySchedPageObject mySchedPage = loginPage.Login("admin@admin.pl", "admin");
            MenuLayoutObject menu = new MenuLayoutObject();
            LeavePageObject leavesPage = menu.EnterLeavesage();

            leavesPage.SetDate("2020-04-20");
            for (int i = 0; i < row; i++)
            {
                leavesPage.AddLeave("2020-04-20", "2020-04-23", "test");
                SeleniumUtilityMethods.WaitForJavascript();
            }
            string pconfirm1 = leavesPage.GetCellContent(row, 5);
            leavesPage.Confirm(row);
            SeleniumUtilityMethods.WaitForJavascript();

            string pconfirm2 = leavesPage.GetCellContent(row, 5);

            StringAssert.IsMatch("NIE", pconfirm1);
            StringAssert.IsMatch("TAK", pconfirm2);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void Leaves_PretendConfirm_NotConfirmed(int row)
        {
            LoginPageObject loginPage = new LoginPageObject();
            MySchedPageObject mySchedPage = loginPage.Login("admin@admin.pl", "admin");
            MenuLayoutObject menu = new MenuLayoutObject();
            LeavePageObject leavesPage = menu.EnterLeavesage();

            leavesPage.SetDate("2020-04-20");
            for (int i = 0; i < row; i++)
            {
                leavesPage.AddLeave("2020-04-20", "2020-04-23", "test");
                SeleniumUtilityMethods.WaitForJavascript();
            }
            leavesPage.PretendConfirm(row);
            SeleniumUtilityMethods.WaitForJavascript();

            string pconfirm = leavesPage.GetCellContent(row, 5);

            StringAssert.IsMatch("NIE", pconfirm);
        }

        [Test]
        [TestCase("2021-04-20","2021-05-21")]
        [TestCase("2021-11-17", "2021-12-01")]
        public void Leaves_TryConfirm_CorrectConfirmPopUpText(string begin, string end)
        {
            LoginPageObject loginPage = new LoginPageObject();
            MySchedPageObject mySchedPage = loginPage.Login("admin@admin.pl", "admin");
            MenuLayoutObject menu = new MenuLayoutObject();
            LeavePageObject leavesPage = menu.EnterLeavesage();

            leavesPage.SetDate(begin);
            leavesPage.AddLeave(begin, end, "test");
            SeleniumUtilityMethods.WaitForJavascript();

            string pconfirm = leavesPage.GetConfirmText(0);
            
            string rBegin = begin.Split('-')[2] + "-" + begin.Split('-')[1] + "-" + begin.Split('-')[0];
            string rEnd = end.Split('-')[2] + "-" + end.Split('-')[1] + "-" + end.Split('-')[0];
            
            StringAssert.IsMatch("Czy zatwierdzasz urlop admin admin w okresie od "+ rBegin + " do "+ rEnd + "?", pconfirm);
        }

        [Test]
        [TestCase("2021-04-20", "2021-05-21")]
        [TestCase("2021-11-17", "2021-12-01")]
        public void Leaves_ChangeDateFilter_FilterTable(string date, string filter)
        {
            LoginPageObject loginPage = new LoginPageObject();
            MySchedPageObject mySchedPage = loginPage.Login("admin@admin.pl", "admin");
            MenuLayoutObject menu = new MenuLayoutObject();
            LeavePageObject leavesPage = menu.EnterLeavesage();

            leavesPage.SetDate(date);
            leavesPage.AddLeave(date, date, "test");
            leavesPage.AddLeave(filter, filter, "test");
            SeleniumUtilityMethods.WaitForJavascript();

            string Id1 = leavesPage.tabLeaves.GetTableCell(0, 0);
            string Id2 = leavesPage.tabLeaves.GetTableCell(1, 0);

            leavesPage.SetDate(filter);
            string Id3 = leavesPage.tabLeaves.GetTableCell(0, 0);
            string Id4 = "";
            try
            {
                Id4 = leavesPage.tabLeaves.GetTableCell(1, 0);
            }
            catch { }

            StringAssert.IsMatch(Id1, Id3);
            StringAssert.DoesNotMatch(Id3, Id4);
        }

        [Test]
        [TestCase(1, 2, false)]
        [TestCase(1, 2, true)]
        [TestCase(2, 1, true)]
        [TestCase(2, 1, false)]
        public void Leaves_CheckOnlyConfirmed_HideNotConfirmed(int firsts, int secounds, Boolean firstsConfirmed)
        {
            LoginPageObject loginPage = new LoginPageObject();
            MySchedPageObject mySchedPage = loginPage.Login("admin@admin.pl", "admin");
            MenuLayoutObject menu = new MenuLayoutObject();
            LeavePageObject leavesPage = menu.EnterLeavesage();

            leavesPage.SetDate("2021-04-20");
            for(int i=0; i<firsts+secounds;i++)
            {
                leavesPage.AddLeave("2021-04-20", "2021-05-21", "test");
                SeleniumUtilityMethods.WaitForJavascript();
            }
            int start;
            int end;
            int count;
            if (firstsConfirmed)
            {
                start = 0;
                end = firsts;
                count = firsts;
            }
            else
            {
                start = firsts;
                end = firsts+secounds;
                count = secounds;
            }
            string[] tab = new string[count];
            int j = 0;
            for (int i = start; i < end; i++)
            {
                leavesPage.Confirm(i);
                SeleniumUtilityMethods.WaitForJavascript();
                tab[j] = leavesPage.GetCellContent(i, 0);
                j++;
            }

            leavesPage.ChangeOnlyConfirmed();
            SeleniumUtilityMethods.WaitForJavascript();

            for(int i=0; i < count; i++)
            {
                StringAssert.IsMatch(tab[i], leavesPage.GetCellContent(i, 0));
            }
        }

        [Test]
        [TestCase("tester")]
        [TestCase("ogrodnik")]
        public void Positions_Add_PositionAdded(string name)
        {
            LoginPageObject loginPage = new LoginPageObject();
            MySchedPageObject mySchedPage = loginPage.Login("admin@admin.pl", "admin");
            MenuLayoutObject menu = new MenuLayoutObject();
            PositionsPageObject positionsPage = menu.EnterPositions();
            PositionPageObject positionPage = positionsPage.AddPosition();

            string url1 = PropertiesCollection.driver.Url;
            positionPage = positionPage.AddPosition(name);
            SeleniumUtilityMethods.WaitForJavascript();
            string url2 = PropertiesCollection.driver.Url;

            positionsPage = menu.EnterPositions();

            string sId = url2.Substring(url1.Length + 4);
            int id = Int32.Parse(sId);
            string tId = positionsPage.GetCellContent(0, 0);
            string tName = positionsPage.GetCellContent(0, 1);

            StringAssert.IsMatch(sId, tId);
            StringAssert.IsMatch(sId, tId);
            StringAssert.IsMatch(name, tName);
        }

        [Test]
        [TestCase("niedziela", "8:00","9:00","1","2")]
        [TestCase("niedziela", "8:00", "9:00", "1", "2")]
        [TestCase("niedziela", "0:00", "9:00", "1", "2")]
        [TestCase("niedziela", "8:00", "23:59", "1", "2")]
        [TestCase("niedziela", "8:00", "9:00", "0", "2")]
        [TestCase("niedziela", "8:00", "9:00", "1", "3")]
        [TestCase("niedziela", "7:00", "22:00", "2", "4")]
        public void Position_AddRow_RowAdded(string day, string begin, string end, string min, string max)
        {
            LoginPageObject loginPage = new LoginPageObject();
            MySchedPageObject mySchedPage = loginPage.Login("admin@admin.pl", "admin");
            MenuLayoutObject menu = new MenuLayoutObject();
            PositionsPageObject positionsPage = menu.EnterPositions();
            PositionPageObject positionPage = positionsPage.AddPosition();

            positionPage.AddRow(day, begin, end, min, max);

            positionPage.AddPosition("tester");
            positionsPage = menu.EnterPositions();
            positionPage = positionsPage.EnterPosition(0);

            string tday = positionPage.getDayName(0);
            string tbegin = positionPage.getBegin(0);
            string tend = positionPage.getEnd(0);
            string tmin = positionPage.getMin(0);
            string tmax = positionPage.getMax(0);

            StringAssert.IsMatch(day, tday);
            StringAssert.IsMatch(begin, tbegin);
            StringAssert.IsMatch(end, tend);
            StringAssert.IsMatch(min, tmin);
            StringAssert.IsMatch(max, tmax);
        }

        [Test]
        public void Position_RemoveRow_RowRemoved()
        {
            string day = "niedziela";
            string begin = "8:00";
            string end = "9:00";
            string min = "1";
            string max = "2";

            LoginPageObject loginPage = new LoginPageObject();
            MySchedPageObject mySchedPage = loginPage.Login("admin@admin.pl", "admin");
            MenuLayoutObject menu = new MenuLayoutObject();
            PositionsPageObject positionsPage = menu.EnterPositions();
            PositionPageObject positionPage = positionsPage.AddPosition();

            positionPage.AddRow(day, begin, end, min, max);
            positionPage.AddPosition("tester");

            positionsPage = menu.EnterPositions();
            positionPage = positionsPage.EnterPosition(0);

            positionPage.deleteRow(0);
            positionPage.Save();

            positionsPage = menu.EnterPositions();
            positionPage = positionsPage.EnterPosition(0);
            
            Assert.That(() => positionPage.getDayName(0), Throws.Exception);
        }


        [Test]
        [TestCase("itest","ntest","etest@etest.pl","ptest","100")]
        [TestCase("Zenon", "ntest", "etest@etest.pl", "ptest", "100")]
        [TestCase("itest", "Zenkowski", "etest@etest.pl", "ptest", "100")]
        [TestCase("itest", "ntest", "zenek@zenkowski.pl", "ptest", "100")]
        [TestCase("itest", "ntest", "etest@etest.pl", "zenkowski", "100")]
        [TestCase("itest", "ntest", "etest@etest.pl", "ptest", "220")]
        [TestCase("Zenon", "Zenkowski", "zenek@zenkowski.pl", "zenkowski", "220")]
        public void People_AddWithCorrectValues_PersonAdded(string name, string surname, string email, string password, string hours)
        {
            LoginPageObject loginPage = new LoginPageObject();
            MySchedPageObject mySchedPage = loginPage.Login("admin@admin.pl", "admin");
            MenuLayoutObject menu = new MenuLayoutObject();
            PeoplePageObject peoplePage = menu.EnterPeople();

            string id = "";
            try
            {
                id = peoplePage.GetCellContent(0, 0);
            }
            catch { }

            peoplePage.AddPerson(name, surname, id+email, password, hours);
            SeleniumUtilityMethods.WaitForJavascript();

            string tname = peoplePage.GetCellContent(0, 1);
            string tsunrame = peoplePage.GetCellContent(0, 2);
            string temail = peoplePage.GetCellContent(0, 3);
            string thours = peoplePage.GetCellContent(0, 4);

            StringAssert.IsMatch(name, tname);
            StringAssert.IsMatch(surname, tsunrame);
            StringAssert.IsMatch(id + email, temail);
            StringAssert.IsMatch(hours, thours);
        }


        [Test]
        [TestCase("itest", "ntest", "etestetest.pl", "ptest", "100")]
        [TestCase("1itest", "ntest", "etest@etest.pl", "ptest", "100")]
        [TestCase("itest", "1ntest", "etest@etest.pl", "ptest", "100")]
        [TestCase("itest", "ntest", "etest@etest.pl", "pte", "100")]
        [TestCase("itest", "ntest", "etest@etest.pl", "ptest", "221")]
        public void People_AddWithInCorrectValues_PersonNotAdded(string name, string surname, string email, string password, string hours)
        {
            LoginPageObject loginPage = new LoginPageObject();
            MySchedPageObject mySchedPage = loginPage.Login("admin@admin.pl", "admin");
            MenuLayoutObject menu = new MenuLayoutObject();
            PeoplePageObject peoplePage = menu.EnterPeople();

            string id = "";
            try
            {
                id = peoplePage.GetCellContent(0, 0);
            }
            catch { }

            peoplePage.AddPerson(name, surname, id + email, password, hours);
            SeleniumUtilityMethods.WaitForJavascript();

            string nid = "";
            try
            {
                nid = peoplePage.GetCellContent(0, 0);
            }
            catch { }

            StringAssert.IsMatch(id, nid);
        }


        [Test]
        [TestCase("tesa", "", "tester", false)]
        [TestCase("tes","","tester", true)]
        [TestCase("", "tes", "tester", true)]
        [TestCase("", "tesa", "tester", false)]
        public void People_Filter_TableFiltered(string filtrName, string filtrSurname, string personValues, Boolean result)
        {
            LoginPageObject loginPage = new LoginPageObject();
            MySchedPageObject mySchedPage = loginPage.Login("admin@admin.pl", "admin");
            MenuLayoutObject menu = new MenuLayoutObject();
            PeoplePageObject peoplePage = menu.EnterPeople();

            string id = "0";
            try
            {
                id = peoplePage.GetCellContent(0, 0);
            }
            catch{}

            peoplePage.AddPerson(personValues, personValues, personValues + id + "@" + personValues + id + ".pl", "1qazxsw2", "1");
            SeleniumUtilityMethods.WaitForJavascript();
            string tId = peoplePage.GetCellContent(0, 0);

            peoplePage.setFilter(filtrName, filtrSurname);
            SeleniumUtilityMethods.WaitForJavascript();
            string nId = "";
            try
            {
                nId = peoplePage.GetCellContent(0, 0);
            }
            catch { }

            Assert.That(result == (tId == nId));
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        public void People_ClickRow_EnteredPersonPage(int row)
        {
            String personValues = "test";
            LoginPageObject loginPage = new LoginPageObject();
            MySchedPageObject mySchedPage = loginPage.Login("admin@admin.pl", "admin");
            MenuLayoutObject menu = new MenuLayoutObject();
            PeoplePageObject peoplePage = menu.EnterPeople();
            PersonPageObject personPage;

            string id = "0";
            try
            {
                id = peoplePage.GetCellContent(0, 0);
            }
            catch { }
            peoplePage.AddPerson(personValues, personValues, personValues + id + "@" + personValues + id + ".pl", "1qazxsw2", "1");
            SeleniumUtilityMethods.WaitForJavascript();
            personPage = peoplePage.EneterPerson(row);

            Assert.That(() => personPage.getEmail(), Throws.Nothing);
        }

        [Test]
        [TestCase("itest", "ntest", null, "ptest", "100", true)]
        [TestCase("itest", "ntest", null, "ptest", "100", false)]
        public void Person_ChangeDataToAnotherCorrectValue_DataChanged(string name, string surname, string email, string password, string hours, Boolean? admin)
        {
            String personValues = "test";
            LoginPageObject loginPage = new LoginPageObject();
            MySchedPageObject mySchedPage = loginPage.Login("admin@admin.pl", "admin");
            MenuLayoutObject menu = new MenuLayoutObject();
            PeoplePageObject peoplePage = menu.EnterPeople();
            PersonPageObject personPage;

            string id = "0";
            try
            {
                id = peoplePage.GetCellContent(0, 0);
            }
            catch { }
            peoplePage.AddPerson(personValues, personValues, personValues + id + "@" + personValues + id + ".pl", "1qazxsw2", "1");
            SeleniumUtilityMethods.WaitForJavascript();
            personPage = peoplePage.EneterPerson(0);

            personPage.setData(name, surname, email, hours, admin, password);
            personPage.Save();
            peoplePage = menu.EnterPeople();
            personPage = peoplePage.EneterPerson(0);

            string nname = personPage.getName();
            string nsurname = personPage.getSurname();
            string nemail = personPage.getEmail();
            string nhours = personPage.getHours();
            Boolean nadmin = personPage.getAdminstrator();

            if (name != null)
                StringAssert.IsMatch(name, nname);
            if (surname != null)
                StringAssert.IsMatch(surname, nsurname);
            if (email != null)
                StringAssert.IsMatch(email, nemail);
            if (hours != null)
                StringAssert.IsMatch(hours, nhours);
            if (admin != null)
                Assert.That(nadmin == admin);
        }

        [Test]
        [TestCase("itest", "ntest", "tonieemail", "ptest", "100")]
        [TestCase("1itest", "ntest", "tonie@email.pl", "ptest", "100")]
        [TestCase("itest", "1ntest", "tonie@email.pl", "ptest", "100")]
        [TestCase("itest", "ntest", "tonie@email.pl", "pte", "100")]
        [TestCase("itest", "ntest", "tonie@email.pl", "ptest", "221")]
        [TestCase("itest", "ntest", "tonie@email.pl", "ptest", "999")]
        public void Person_ChangeDataToAnotherInCorrectValue_DataChanged(string name, string surname, string email, string password, string hours)
        {
            String personValues = "test";
            LoginPageObject loginPage = new LoginPageObject();
            MySchedPageObject mySchedPage = loginPage.Login("admin@admin.pl", "admin");
            MenuLayoutObject menu = new MenuLayoutObject();
            PeoplePageObject peoplePage = menu.EnterPeople();
            PersonPageObject personPage;

            string id = "0";
            try
            {
                id = peoplePage.GetCellContent(0, 0);
            }
            catch { }
            peoplePage.AddPerson(personValues, personValues, personValues + id + "@" + personValues + id + ".pl", "1qazxsw2", "1");
            SeleniumUtilityMethods.WaitForJavascript();
            personPage = peoplePage.EneterPerson(0);

            personPage.setData(name, surname, email, hours, false, password);
            personPage.Save();
            peoplePage = menu.EnterPeople();
            personPage = peoplePage.EneterPerson(0);

            string nname = personPage.getName();
            string nsurname = personPage.getSurname();
            string nemail = personPage.getEmail();
            string nhours = personPage.getHours();

            StringAssert.DoesNotMatch(name, nname);
            StringAssert.DoesNotMatch(surname, nsurname);
            StringAssert.DoesNotMatch(email, nemail);
            StringAssert.DoesNotMatch(hours, nhours);
        }

        [Test]
        [TestCase(1, "08:00", "09:00")]
        [TestCase(2, "08:00", "09:00")]
        [TestCase(1, "01:00", "09:00")]
        [TestCase(1, "08:00", "23:59")]
        [TestCase(1, "08:59", "09:00")]
        [TestCase(1, "08:00", "08:01")]
        public void Person_SetWorkDayWithCorrectValues_SetedWorkDay(int day, string begin, string end)
        {
            String personValues = "test";
            LoginPageObject loginPage = new LoginPageObject();
            MySchedPageObject mySchedPage = loginPage.Login("admin@admin.pl", "admin");
            MenuLayoutObject menu = new MenuLayoutObject();
            PeoplePageObject peoplePage = menu.EnterPeople();
            PersonPageObject personPage;

            string id = "0";
            try
            {
                id = peoplePage.GetCellContent(0, 0);
            }
            catch { }
            peoplePage.AddPerson(personValues, personValues, personValues + id + "@" + personValues + id + ".pl", "1qazxsw2", "1");
            SeleniumUtilityMethods.WaitForJavascript();
            personPage = peoplePage.EneterPerson(0);

            personPage.setWorkDay(day, begin, end);
            personPage.Save();
            peoplePage = menu.EnterPeople();
            personPage = peoplePage.EneterPerson(0);

            string nbegin = personPage.getDayBegin(day);
            string nend = personPage.getDayEnd(day);

            StringAssert.IsMatch(begin, nbegin);
            StringAssert.IsMatch(end, nend);
        }

        [Test]
        [TestCase(1, "10:00", "9:00")]
        [TestCase(2, "10:00", "9:00")]
        [TestCase(1, "24:00", "9:00")]
        [TestCase(1, "10:00", "1:00")]
        [TestCase(1, "10:00", "10:00")]
        [TestCase(1, "10:01", "10:00")]
        public void Person_SetWorkDayWithInCorrectValues_SetedWorkDay(int day, string begin, string end)
        {
            String personValues = "test";
            LoginPageObject loginPage = new LoginPageObject();
            MySchedPageObject mySchedPage = loginPage.Login("admin@admin.pl", "admin");
            MenuLayoutObject menu = new MenuLayoutObject();
            PeoplePageObject peoplePage = menu.EnterPeople();
            PersonPageObject personPage;

            string id = "0";
            try
            {
                id = peoplePage.GetCellContent(0, 0);
            }
            catch { }
            peoplePage.AddPerson(personValues, personValues, personValues + id + "@" + personValues + id + ".pl", "1qazxsw2", "1");
            SeleniumUtilityMethods.WaitForJavascript();
            personPage = peoplePage.EneterPerson(0);

            personPage.setWorkDay(day, begin, end);
            personPage.Save();
            peoplePage = menu.EnterPeople();
            personPage = peoplePage.EneterPerson(0);

            Boolean isworking = personPage.isWorkDay(day);

            Assert.That(isworking == false);
        }

        [Test]
        [TestCase("2020-04-05", "2020-04-08", true)]
        [TestCase("2020-01-01", "2020-09-08", true)]
        [TestCase("2020-04-05", "2020-04-08", false)]
        [TestCase("2020-01-01", "2020-09-08", false)]
        public void Schedules_Filter_TableFiltered(string begin, string end, Boolean confirmed)
        {
            String personValues = "test";
            LoginPageObject loginPage = new LoginPageObject();
            MySchedPageObject mySchedPage = loginPage.Login("admin@admin.pl", "admin");
            MenuLayoutObject menu = new MenuLayoutObject();
            SchedulesPageObject schedulesPage = menu.EnterSchedules();

            schedulesPage.FilterTable(begin, end, confirmed);

            DateTime dateBegin = new DateTime();
            DateTime dateEnd = new DateTime();
            DateTime dateBeginT = new DateTime();
            DateTime dateEndT = new DateTime();
            string tconf = "";
            Boolean isData = true;
            try
            {
                string tbegin = schedulesPage.GetCellContent(0, 1);
                string tend = schedulesPage.GetCellContent(0, 2);
                tconf = schedulesPage.GetCellContent(0, 3);

                dateBegin = new DateTime(Int32.Parse(begin.Split('-')[0]), Int32.Parse(begin.Split('-')[1]), Int32.Parse(begin.Split('-')[2]));
                dateEnd = new DateTime(Int32.Parse(end.Split('-')[0]), Int32.Parse(end.Split('-')[1]), Int32.Parse(end.Split('-')[2]));
                dateBeginT = new DateTime(Int32.Parse(tbegin.Split('.')[2]), Int32.Parse(tbegin.Split('.')[1]), Int32.Parse(tbegin.Split('.')[0]));
                dateEndT = new DateTime(Int32.Parse(tend.Split('.')[2]), Int32.Parse(tend.Split('.')[1]), Int32.Parse(tend.Split('.')[0]));
            }
            catch { isData = false; }

            if(isData)
            {
                Assert.That((dateBegin >= dateBeginT && dateBegin <= dateEndT) || (dateEnd >= dateBeginT && dateEnd <= dateEndT) || (dateBegin <= dateBeginT && dateEnd >= dateEndT));

                if (confirmed)
                    StringAssert.DoesNotMatch("NIE", tconf);
                else
                    StringAssert.IsMatch("(NIE)|([0-3][0-9]\\.[0-3][0-9]\\.[0-9][0-9][0-9][0-9])", tconf);
            }
            else
            {
                Assert.That(() => schedulesPage.GetCellContent(0, 0), Throws.Exception);
            }
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        public void Schedules_EnterSchedules_SchedulePageWithCorrectData(int row)
        {
            String personValues = "test";
            LoginPageObject loginPage = new LoginPageObject();
            MySchedPageObject mySchedPage = loginPage.Login("admin@admin.pl", "admin");
            MenuLayoutObject menu = new MenuLayoutObject();
            SchedulesPageObject schedulesPage = menu.EnterSchedules();

            string tid = schedulesPage.GetCellContent(row, 0);
            string tbegin = schedulesPage.GetCellContent(row, 1);
            string tend = schedulesPage.GetCellContent(row, 2);
            string tconf = schedulesPage.GetCellContent(row, 3);

            SchedulePageObject schedulePage = schedulesPage.EnterSchedule(row);

            string sid = schedulePage.getId();
            string sdates = schedulePage.getDates();
            string sconf = schedulePage.getConfirmed();

            StringAssert.IsMatch("Grafik nr "+tid, sid);
            StringAssert.IsMatch(tbegin + " - " + tend, sdates);
            if (tconf == "NIE")
                StringAssert.IsMatch("Wymaga Zatwiedzenia", sconf);
            else
                StringAssert.IsMatch(tconf, sconf);
        }

        [TearDown]
        public void CleanUp()
        {
            // zamkniecie przegladarki
            PropertiesCollection.driver.Close();
            PropertiesCollection.driver.Quit();

            Console.WriteLine("Zamkiecie przegladarki");
        }

        static void Main(string[] args)
        {
        }
    }
}
