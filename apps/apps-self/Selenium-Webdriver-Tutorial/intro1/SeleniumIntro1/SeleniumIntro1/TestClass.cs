using System;
using System.Diagnostics;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace SeleniumIntro1
{
    public class TestClass
    {
        [Fact]
        public void Test()
        {
            // open browser
            using IWebDriver driver = new ChromeDriver("C:\\Programs2\\chromedriver_win32");
            driver.Manage().Window.Maximize();

            // navigate to login
            driver.Url = "";
            Thread.Sleep(TimeSpan.FromSeconds(3));

            // enter email
            IWebElement email = driver.FindElement(By.Id("sign-in-email"));
            IWebElement signInPassword = driver.FindElement(By.Id("sign-in-password"));
            email.SendKeys("aytekin.ozdemir@blackbaud.me");
            signInPassword.Click();

            // wait for microsoft redirect
            Thread.Sleep(TimeSpan.FromSeconds(3));

            // redirects to microsoft
            IWebElement microsoftPassword = driver.FindElement(By.CssSelector("[name='passwd']"));
            microsoftPassword.SendKeys("");

            // click on submit
            IWebElement passwordSubmit = driver.FindElement(By.CssSelector("[type='submit']"));
            passwordSubmit.Click();

            // wait for phone verification (2Fa) - microsoft redirect
            Thread.Sleep(TimeSpan.FromSeconds(15));

            // stay sign in page, click on yes
            // Stay signed in?
            // Do this to reduce the number of times you are asked to sign in.
            IWebElement findElement = driver.FindElement(By.CssSelector("[type='submit']"));
            findElement.Click();

            // End of the test
            Thread.Sleep(TimeSpan.FromSeconds(10));
            Debugger.Break();
        }
    }
}
