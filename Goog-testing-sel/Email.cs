using System;
using System.Threading;
using OpenQA.Selenium;


namespace Goog_testing_sel
{
    class Email
    {
        internal static void UserEmailCompose(IWebDriver driver, string name, string bodyText)
        {
            By toReciept = By.XPath("//*[@name='to']");
            By subject = By.XPath("//*[@name='subjectbox']");

            driver.FindElement(By.XPath("//*[contains(text(),'Compose')]")).Click();
            //WaitForPage(driver, 45, "//*[contains(text(),'to')]");
            driver.FindElement(toReciept).SendKeys(name + "@gmail.com");
            driver.FindElement(toReciept).Click();
            driver.FindElement(subject).SendKeys("THIS IS A TEST");
            driver.FindElement(By.XPath("//*[@role = 'textbox']")).SendKeys(bodyText);
            driver.FindElement(By.XPath("//*[@class='T-I J-J5-Ji aoO v7 T-I-atl L3']")).Click();
        }
        internal static void ValidateRecievedEmail(IWebDriver driver, string bodyText)
        {
            driver.FindElement(By.XPath("//*[@class='Cp']/div/table/tbody/tr[1]/td[6]")).Click();
            var textBodyText = driver.FindElement(By.XPath("//*[@class='a3s aXjCH ']/div[1]")).Text;
            Thread.Sleep(3000);
            if (bodyText.Equals(textBodyText))
            {
                Console.WriteLine("Message body text Validated");
            }
            else
            {
                Console.WriteLine("Message body does NOT match");
            }

        }

    }
}
