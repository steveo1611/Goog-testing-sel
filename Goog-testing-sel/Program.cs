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
            string name = "testfieldsteve";
            string psWord = "J0hn3:16";
            By loginButton = By.XPath("//*[@id='identifierNext']/span/span");
            string bodyText ="Body test: this is a test of info in the body of email";

            ChromeOptions options = new ChromeOptions();
            // switch between below lines for when I do not use incognito version
            // options.AddArguments(@"C:\source\selenium-chrome\");
            options.AddArguments(@"C:\source\selenium-chrome\", "--incognito");
            IWebDriver driver = new ChromeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            driver.Navigate().GoToUrl("https://www.google.com/gmail");
            driver.Manage().Window.Maximize();
            
            UserLogIn(driver, name);
            UserPassword(driver, psWord);
            UserEmailCompose(driver, name, bodyText);
            ValidateRecievedEmail(driver, bodyText);
            EmailLogOut(driver);
            //  ********************** delete the 2 lines below during cleanup
            Console.WriteLine("end of the line");
            Console.ReadKey();
            driver.Quit();
        }

        public static void UserLogIn(IWebDriver driver, string name)
        {
            IWebElement signIn = driver.FindElement(By.XPath("//*[@id='identifierId']"));
            signIn.SendKeys(name);
            By loginButton = By.XPath("//*[@id='identifierNext']/span/span");
            driver.FindElement(loginButton).Click();
         }

        public static void UserPassword(IWebDriver driver, string psWord)
        {
            driver.FindElement(By.XPath("//*[@id='password']/div[1]/div/div[1]/input")).SendKeys(psWord);
            driver.FindElement(By.XPath("//*[@id='passwordNext']/span/span")).Click();
        }

        public static void EmailLogOut(IWebDriver driver)
        {
            driver.FindElement(By.XPath("//*[@title='Inbox']")).Click();
            driver.FindElement(By.XPath("//a[starts-with(@aria-label, 'Google Account: Steve Testfield')]")).Click();
            driver.FindElement(By.XPath("//a[@class='gb_3 gb_6f gb_eg gb_Ne gb_pb']")).Click();


        }
        //next steps: TODOs wait for mail to open: then select the 'compose' button
        //  DONE focus on new message - To - enter email address, add a subject line and something in the body then click the send button
        // click on the inbox  on left side to refresh email and then open and verify sent/recieved email
        //sign out
        public static void UserEmailCompose(IWebDriver driver, string name, string bodyText)
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

        public static void ValidateRecievedEmail(IWebDriver driver, string bodyText)
        {
            driver.FindElement(By.XPath("//*[@class='Cp']/div/table/tbody/tr[1]/td[6]")).Click();
            var textBodyText  = driver.FindElement(By.XPath("//*[@class='a3s aXjCH ']/div[1]")).Text;
            if(bodyText.Equals(textBodyText))
            {
                Console.WriteLine("Message body text Validated");
            }
            else
            {
                Console.WriteLine("Message body does NOT match");
            }

        }
        private static object WaitForPage(IWebDriver driver, int waitTime, string elementWaitingOn)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitTime));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(NotFoundException), typeof(InvalidOperationException));
            IWebElement element = wait.Until(d => driver.FindElement(By.XPath(elementWaitingOn)));
            return element;
        }
    }
}

 