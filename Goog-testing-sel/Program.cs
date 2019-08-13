using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace Goog_testing_sel
{
    class Program
    {
        public static void Main(string[] args)
        {
            IWebDriver driver = Driversetting.ChrmDriverSetup();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            driver.Navigate().GoToUrl("https://www.google.com/gmail");
            driver.Manage().Window.Maximize();

            LogInOut.UserLogIn(driver, UserSettings.name());
            LogInOut.UserPassword(driver, UserSettings.psWord());
            Thread.Sleep(3000);
            Email.UserEmailCompose(driver, UserSettings.name(), UserSettings.bodyText());
            Email.ValidateRecievedEmail(driver, UserSettings.bodyText());
            LogInOut.EmailLogOut(driver);
            
            
            //  ********************** delete the 2 lines below during cleanup
            Console.WriteLine("end of the line");
            Console.ReadKey();
            driver.Quit();
        }
    }
}

 