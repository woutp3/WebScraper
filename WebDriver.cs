using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp
{
    internal class WebDriver
    {
        private IWebDriver driver;
        public IWebDriver setDriver()
        {
            // Giving the driver options
            Console.WriteLine("Setting up the browser....");
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("--headless", "--silent", "log-levels=3");
            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;
            chromeDriverService.SuppressInitialDiagnosticInformation = true;
            chromeDriverService.EnableVerboseLogging = true;
            // Set up the WebDriver
            driver = new ChromeDriver(chromeOptions);
            return driver;
        }
    }
}
