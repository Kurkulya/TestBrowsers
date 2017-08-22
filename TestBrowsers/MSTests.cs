using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Safari;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestBrowsers
{
    [TestClass]
    public class TestChrome : MSTestCalcJS
    {
        [ClassInitialize]
        public static void ClassIni(TestContext t)
        {
            driver = MakeDriver();
        }
        static IWebDriver MakeDriver()
        {
            return new ChromeDriver();
        }
        [ClassCleanup]
        public static void ClassCleanup()
        {
            driver.Quit();
        }
    }

    [TestClass]
    public class TestFirefox : MSTestCalcJS
    {
        [ClassInitialize]
        public static void ClassIni(TestContext t)
        {
            driver = MakeDriver();
        }
        static IWebDriver MakeDriver()
        {
            return new FirefoxDriver();
        }
        [ClassCleanup]
        public static void ClassCleanup()
        {
            driver.Quit();
        }
    }

    [TestClass]
    public class TestEdge : MSTestCalcJS
    {
        [ClassInitialize]
        public static void ClassIni(TestContext t)
        {
            driver = MakeDriver();
        }
        static IWebDriver MakeDriver()
        {
            return new EdgeDriver();
        }
        [ClassCleanup]
        public static void ClassCleanup()
        {
            driver.Quit();
        }
    }

    [TestClass]
    public class TestOpera : MSTestCalcJS
    {
        [ClassInitialize]
        public static void ClassIni(TestContext t)
        {
            driver = MakeDriver();
        }
        static IWebDriver MakeDriver()
        {
            OperaOptions opera = new OperaOptions();
            opera.BinaryLocation = @"C:\Program Files\Opera\launcher.exe";

            return new OperaDriver(opera);
        }
        [ClassCleanup]
        public static void ClassCleanup()
        {
            driver.Quit();
        }
    }

    [TestClass]
    public class TestYandex : MSTestCalcJS
    {
        [ClassInitialize]
        public static void ClassIni(TestContext t)
        {
            driver = MakeDriver();
        }
        static IWebDriver MakeDriver()
        {
            ChromeOptions yandex = new ChromeOptions();
            yandex.BinaryLocation = @"C:\Users\Kurkulya\AppData\Local\Yandex\YandexBrowser\Application\browser.exe";

            return new ChromeDriver(yandex);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            driver.Quit();
        }
    }

    [TestClass]
    public abstract class MSTestCalcJS
    {
        internal static IWebDriver driver;
        POM obj;
        [ClassCleanup]
        static public void TearDown()
        {
            driver.Quit();
        }


        [TestInitialize]
        public void TestUp()
        {
            obj = new POM(driver);
            driver.Navigate().GoToUrl("file:///E:/C%23%201708/Calcs/With%20Buttons/CalcJSWithButtons/CalcJSwithButtons.html");
        }

        [DataTestMethod]
        [DataRow("but1")]
        [DataRow("but2")]
        [DataRow("but3")]
        [DataRow("but4")]
        [DataRow("but5")]
        [DataRow("but6")]
        [DataRow("but7")]
        [DataRow("but8")]
        [DataRow("but9")]
        [DataRow("but0")]
        [DataRow("butMinus")]
        [DataRow("butPlus")]
        [DataRow("butMult")]
        [DataRow("butDiv")]
        [DataRow("butEqual")]
        [DataRow("resField")]
        public void TestExistingElements(string elementId)
        {
            IWebElement el = obj.FindElement(elementId);
            Assert.AreEqual(true, el.Displayed);
        }

        [DataTestMethod]
        [DataRow("but1", "1")]
        [DataRow("but2", "2")]
        [DataRow("but3", "3")]
        [DataRow("but4", "4")]
        [DataRow("but5", "5")]
        [DataRow("but6", "6")]
        [DataRow("but7", "7")]
        [DataRow("but8", "8")]
        [DataRow("but9", "9")]
        [DataRow("but0", "0")]
        public void TestSimpleCheck(string elementId, string res)
        {
            obj.FindElement(elementId).Click();
            string num = obj.FindElement("resField").GetAttribute("value");
            Assert.AreEqual(res, num);
        }

        [DataTestMethod]
        [DataRow(new string[] { "but1", "but2", "but3" }, "123")]
        [DataRow(new string[] { "but4", "but5", "but6" }, "456")]
        [DataRow(new string[] { "but7", "but8", "but9" }, "789")]
        [DataRow(new string[] { "but3", "but0", "but6" }, "306")]
        public void TestComplexCheck(string[] butts, string res)
        {
            foreach (string str in butts)
            {
                obj.FindElement(str).Click();
            }
            string num = obj.FindElement("resField").GetAttribute("value");
            Assert.AreEqual(res, num);
        }

        [DataTestMethod]
        [DataRow("but1", "but2", "butPlus", "3")]
        [DataRow("but3", "but4", "butMinus", "-1")]
        [DataRow("but5", "but6", "butMult", "30")]
        [DataRow("but9", "but3", "butDiv", "3")]
        [DataRow("but7", "but0", "butDiv", "Infinity")]
        public void TestRealJob(string x, string y, string op, string res)
        {
            obj.FindElement(x).Click();
            obj.FindElement(op).Click();
            obj.FindElement(y).Click();
            obj.FindElement("butEqual").Click();
            string calc = obj.FindElement("resField").GetAttribute("value");
            Assert.AreEqual(res, calc);
        }

    }
}

