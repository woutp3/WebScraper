using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WebApp
{
    internal class ICTjobs
    {
        private IWebDriver driver;
        private List<String> titleList = new List<String>();
        private List<String> companyList = new List<String>();
        private List<String> locationList = new List<String>();
        private List<String> keywordList = new List<String>();
        private List<String> linkList = new List<String>();
        private String url = "";
        private String jobString = "";
        private String endresult = "";
        public void setDriver()
        {
            // Giving the driver options
            Console.WriteLine("Searching ICTjobs....");
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments(/*"--headless", */"--silent", "log-levels=3");
            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;
            chromeDriverService.SuppressInitialDiagnosticInformation = true;
            chromeDriverService.EnableVerboseLogging = true;
            // Set up the WebDriver
            driver = new ChromeDriver(chromeOptions);
        }
        public void setUrl(String searchterm)
        {
            Console.WriteLine("Creating the URL....");
            // Create the actual URL
            String ictjobsSearchterm = searchterm.Replace(" ", "+");
            url += $"https://www.ictjob.be/en/search-it-jobs?keywords={ictjobsSearchterm}";
        }

        public String getUrl() { 
            return url; 
        }
        public void setJob()
        {
            Console.WriteLine("Getting the job\'s....");
            // Open the ICTJobs page
            driver.Navigate().GoToUrl(url);
            System.Threading.Thread.Sleep(10000);

            //Click the cookie button
            var cookieButton = driver.FindElement(By.CssSelector(".button.cookie-layer-button.close-layer-button"));
            cookieButton.Click();

            //Click the button to sort the jobs on date
            var sortDateButton = driver.FindElement(By.Id("sort-by-date"));
            sortDateButton.Click();
            System.Threading.Thread.Sleep(15000);

            //Find the different elements
            var titleElement = driver.FindElements(By.ClassName("job-title"));
            var companyElement = driver.FindElements(By.ClassName("job-company"));
            var locationElement = driver.FindElements(By.CssSelector("[itemprop='addressLocality']"));
            var keywordElement = driver.FindElements(By.ClassName("job-keywords"));
            var linkElement = driver.FindElements(By.CssSelector(".job-title.search-item-link"));

            //Get the 5 elements at the top
            for (int i = 0; i < 5; i++)
            {
                // Extract the job title
                int k = i * 2;
                string title = titleElement[k].Text;
                titleList.Add(title);

                // Extract the job company
                string company = companyElement[i].Text;
                companyList.Add(company);

                // Extract the job location
                string location = locationElement[i].Text;
                locationList.Add(location);

                // Extract the job keywords
                string keyword = keywordElement[i].Text;
                keywordList.Add(keyword);

                // Extract the job link
                string link = linkElement[i].GetAttribute("href");
                linkList.Add(link);
            }
            //Close the driver
            driver.Quit();
        }

        public String getJob()
        {
            for (int i = 0; i < 5; i++)
            {
                jobString += $"{titleList[i]}\n{companyList[i]}\n{locationList[i]}\n{keywordList[i]}\n{linkList[i]}\n--------------------";
            }
            return jobString;
        }

        public void setJobData()
        {
            // Put the scraped data in a human readable format
            Console.WriteLine("Scraping the job data....");
            for (int j = 0; j < 5; j++)
            {

                endresult += ($"Job {j + 1}:\nJobtitle \"{titleList[j]}\", organised by \"{companyList[j]}\", at {locationList[j]} with as keywords \"{keywordList[j]}\"  on {linkList[j]}\n");
                endresult += ("------------------------------\n");
            }
        }
        public String getJobData()
        {
            return endresult;
        }
    }
}
