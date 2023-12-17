using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp
{
    internal class Twitter
    {
        private IWebDriver driver;
        private List<String> descriptionList = new List<String>();
        private List<String> tweeterList = new List<String>();
        private List<String> likeList = new List<String>();
        private String url = "";
        private String tweetString = "";
        private String endresult = "";

        public void setDriver()
        {
            // Giving the driver options
            Console.WriteLine("Setting up the driver....");
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments(/*"--headless", */"--silent", "log-levels=3");
            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;
            chromeDriverService.SuppressInitialDiagnosticInformation = true;
            chromeDriverService.EnableVerboseLogging = true;
            // Set up the WebDriver
            driver = new ChromeDriver(chromeOptions);
        }

        public void setUrl(String searchterm) {
            Console.WriteLine("Creatig the URL....");
            // Create the actual URL
            String twitterSearchterm = searchterm.Replace(" ", "%20");
            url = ($"https://twitter.com/search?q={twitterSearchterm}&src=typed_query&f=live");
        }

        public String getUrl() { 
            return url;
        }

        public void setTweet()
        {
            Console.WriteLine("Logging in....");
            // Open the Twitter page
            driver.Navigate().GoToUrl(url);
            System.Threading.Thread.Sleep(5000);

            // Log in into twitter using a username and password
            // Enter username
            Console.WriteLine("Entering the username");
            IWebElement username = driver.FindElement(By.CssSelector("[autocomplete=\"username\"]"));
            username.SendKeys("WoutPeeter18286");

            //Click the continue (volgende) button to proceed to the next login step
            var button = driver.FindElement(By.XPath("//*[@id=\"layers\"]/div/div/div/div/div/div/div[2]/div[2]/div/div/div[2]/div[2]/div/div/div/div[6]"));
            button.Click();
            System.Threading.Thread.Sleep(2000);

            // Confirm yourself if needed, else skip this step
            try
            {
                Console.WriteLine("Entering the confirmation....");
                IWebElement confirmation = driver.FindElement(By.XPath("//*[@id=\"layers\"]/div/div/div/div/div/div/div[2]/div[2]/div/div/div[2]/div[2]/div[1]/div/div[2]/label/div/div[2]/div/input"));
                confirmation.SendKeys("WoutPeeter18286");
                System.Threading.Thread.Sleep(2000);
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Skipping confirmation....");
            }

            // Enter password
            Console.WriteLine("Entering the password");
            IWebElement password = driver.FindElement(By.XPath("//*[@id=\"layers\"]/div/div/div/div/div/div/div[2]/div[2]/div/div/div[2]/div[2]/div[1]/div/div/div[3]/div/label/div/div[2]/div[1]/input"));
            password.SendKeys("abc1234!");

            // Click the login button to log in
            var button2 = driver.FindElement(By.XPath("//*[@id=\"layers\"]/div/div/div/div/div/div/div[2]/div[2]/div/div/div[2]/div[2]/div[2]/div/div[1]/div/div/div"));
            button2.Click();
            System.Threading.Thread.Sleep(5000);

            Console.WriteLine("Getting the Tweets....");

            // Click the cookie button
            var cookies = driver.FindElement(By.XPath("//*[@id=\"layers\"]/div/div/div/div/div/div[2]/div[1]"));
            cookies.Click();
            System.Threading.Thread.Sleep(3000);

            // Get the different elements
            var descriptionElement = driver.FindElements(By.CssSelector("[data-testid=\"tweetText\"]"));
            var tweeterElement = driver.FindElements(By.XPath("//*[@data-testid=\"User-Name\"]/div[1]/div/a"));
            var likeElement = driver.FindElements(By.XPath("//div[contains(@aria-label, ' Like')]"));
            // "//*[@id=\"id__jpff5yr4b4a\"]/div[3]/div"

            //Get the elements of the 5 tweets at the top
            for (int i = 0; i < 5; i++)
            {

                // Extract the Tweet description
                String description = descriptionElement[i].Text;
                while (description.Contains("?"))
                {
                    description = description.Replace("?", "");
                };
                descriptionList.Add(description);

                // Extract the Tweeter
                String tweeterHref = tweeterElement[i].GetAttribute("href");
                String tweeter = tweeterHref.Replace("https://twitter.com/", "");
                tweeterList.Add(tweeter);

                // Extract the amount of likes
                String like = likeElement[i].GetAttribute("aria-label");
                like = like.Substring(0, like.Length - 5);
                likeList.Add(like);
            }
            // Close the WebDriver
            driver.Quit();
        }
        public String getTweet()
        {
            for (int i = 0; i < 5; i++)
            {
                tweetString += $"{tweeterList[i]}\n{descriptionList[i]}\n{likeList[i]}\n--------------------";
            }
            return tweetString;
        }
        public void setTweetData()
        {
            // Put the scraped data in a human readable format
            Console.WriteLine("Scraping the Tweet data....");
            for (int j = 0; j < 5; j++)
            {

                endresult += ($"Tweet {j + 1}:\nTweeted by {tweeterList[j]}, with {likeList[j]} It contains: {descriptionList[j]}\n");
                endresult += ("------------------------------\n");
            }
        }
        public String getTweetData()
        {
            return endresult;
        }
    }
}
