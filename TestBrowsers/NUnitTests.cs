using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Safari;
using NUnit.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestBrowsers
{
    [TestFixture(typeof(ChromeDriver))]
    [TestFixture(typeof(FirefoxDriver))]
    [TestFixture(typeof(EdgeDriver))]
    [TestFixture(typeof(OperaDriver))]
    [TestFixture(typeof(YandexDriver))]
    public class NUnitTets<TPage> where TPage : IWebDriver, new()
    {
        static IWebDriver driver = null;
        [OneTimeSetUp]
        public void DriverPath()
        {
            if (typeof(TPage) == typeof(OperaDriver))
            {
                OperaOptions opera = new OperaOptions();
                opera.BinaryLocation = @"C:\Program Files\Opera\launcher.exe";

                driver = new OperaDriver(opera);
            }
            else if (typeof(TPage) == typeof(YandexDriver))
            {
                ChromeOptions yandex = new ChromeOptions();
                yandex.BinaryLocation = @"C:\Users\Kurkulya\AppData\Local\Yandex\YandexBrowser\Application\browser.exe";
                driver = new ChromeDriver(yandex);
            }
            else
            {
                driver = new TPage();
            }
        }

        [OneTimeTearDown]
        public static void TearDown()
        {
            driver.Quit();
        }

        [SetUp]
        public void TestUp()
        {
            driver.Navigate().GoToUrl("file:///E:/C%23%201708/Calcs/With%20Buttons/CalcJSWithButtons/CalcJSwithButtons.html");
        }

        [Test]
        [TestCase("but1", "1")]
        [TestCase("but2", "2")]
        [TestCase("but3", "3")]
        [TestCase("but4", "4")]
        [TestCase("but5", "5")]
        [TestCase("but6", "6")]
        [TestCase("but7", "7")]
        [TestCase("but8", "8")]
        [TestCase("but9", "9")]
        [TestCase("but0", "0")]
        [TestCase("butMinus", "-")]
        [TestCase("butPlus", "+")]
        [TestCase("butMult", "*")]
        [TestCase("butDiv", "/")]
        [TestCase("butEqual", "=")]
        [TestCase("resField", "")]
        public void TestExistingElements(string elementId, string res)
        {
            IWebElement el = driver.FindElement(By.Id(elementId));
            NUnit.Framework.Assert.AreEqual(res, el.GetAttribute("value"));
        }

        [Test]
        [TestCase("but1", "1")]
        [TestCase("but2", "2")]
        [TestCase("but3", "3")]
        [TestCase("but4", "4")]
        [TestCase("but5", "5")]
        [TestCase("but6", "6")]
        [TestCase("but7", "7")]
        [TestCase("but8", "8")]
        [TestCase("but9", "9")]
        [TestCase("but0", "0")]
        public void TestSimpleCheck(string elementId, string res)
        {
            driver.FindElement(By.Id(elementId)).Click();
            string num = driver.FindElement(By.Id("resField")).GetAttribute("value");
            NUnit.Framework.Assert.AreEqual(res, num);
        }

        [Test]
        [TestCase(new string[] { "but1", "but2", "but3" }, "123")]
        [TestCase(new string[] { "but4", "but5", "but6" }, "456")]
        [TestCase(new string[] { "but7", "but8", "but9" }, "789")]
        [TestCase(new string[] { "but3", "but0", "but6" }, "306")]
        public void TestComplexCheck(string[] butts, string res)
        {
            foreach (string str in butts)
            {
                driver.FindElement(By.Id(str)).Click();
            }
            string num = driver.FindElement(By.Name("resField")).GetAttribute("value");
            NUnit.Framework.Assert.AreEqual(res, num);
        }

        [Test]
        [TestCase("but1", "but2", "butPlus", "3")]
        [TestCase("but3", "but4", "butMinus", "-1")]
        [TestCase("but5", "but6", "butMult", "30")]
        [TestCase("but9", "but3", "butDiv", "3")]
        [TestCase("but7", "but0", "butDiv", "Infinity")]
        public void TestRealJob(string x, string y, string op, string res)
        {
            driver.FindElement(By.Id(x)).Click();
            driver.FindElement(By.Id(op)).Click();
            driver.FindElement(By.Id(y)).Click();
            driver.FindElement(By.Id("butEqual")).Click();
            string calc = driver.FindElement(By.Name("resField")).GetAttribute("value");
            NUnit.Framework.Assert.AreEqual(res, calc);
        }

    }
}

