using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebApp
{
    internal class Youtube
    {
        private List<String> titleList = new List<String>();
        private List<String> linkList = new List<String>();
        private List<String> uploaderList = new List<String>();
        private List<String> viewList = new List<String>();
        private String endresult = "";
        private String url = "";
        private String videoString = "";
        public void setUrl(String searchterm) {
            Console.WriteLine("Creating the URL....");
            //Create the actual URL
            String ytSearchTerm = searchterm.Replace(" ", "+");
            url = ($"https://youtube.com/results?search_query={ytSearchTerm}&sp=CAI%253D");
        }

        public String getUrl()
        {
            return url;
        }

        public void setVideo(IWebDriver driver)
        {
            Console.WriteLine("Getting the video\'s....");
            // Open the YouTube page
            driver.Navigate().GoToUrl(url);

            // Wait for the page to load (you may need to adjust the wait time)
            System.Threading.Thread.Sleep(5000);
            var agreeButton = driver.FindElement(By.CssSelector(".yt-spec-button-shape-next.yt-spec-button-shape-next--filled.yt-spec-button-shape-next--mono.yt-spec-button-shape-next--size-m"));
            agreeButton.Click();


            // Find the element you want to remove
            var reels = driver.FindElement(By.CssSelector(".ytd-reel-shelf-renderer.style-scope"));
            // Execute JavaScript to remove the element from the DOM
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            jsExecutor.ExecuteScript("arguments[0].parentNode.removeChild(arguments[0]);", reels);

            //let the proces sleep
            System.Threading.Thread.Sleep(2000);

            //scrape the data
            var titleElement = driver.FindElements(By.Id("video-title"));
            var uploaderElement = driver.FindElements(By.XPath("//*[@id=\"text\"]/a"));
            var viewsElement = driver.FindElements(By.CssSelector(".inline-metadata-item.style-scope.ytd-video-meta-block"));

            for (int i = 0; i < 5; i++)
            {

                // Extract video title
                string title = titleElement[i].Text;
                titleList.Add(title);

                // Extract video link
                string link = titleElement[i].GetAttribute("href");
                linkList.Add(link);

                // Extract uploader
                int h = 2 * i + 1;
                string uploader = uploaderElement[h].Text;
                uploaderList.Add(uploader);

                // Extract amount of views
                int k = i * 2;
                string views = viewsElement[k].Text;
                viewList.Add(views);
            }
            // Close the WebDriver
            driver.Quit();
        }

        public String getVideo()
        {
            Console.WriteLine("Scraping the video data....");
            for (int i = 0; i < 5; i++)
            {
                videoString += $"{titleList[i]}\n{linkList[i]}\n{uploaderList[i]}\n{viewList[i]}\n--------------------";
            }
            return videoString;
        }

        public void setVideoData() {
            // Put the scraped data in a human readable format
            for (int j = 0; j < 5; j++)
            {

                endresult += ($"Video {j + 1}:\nTitle {titleList[j]}, uploaded by: {uploaderList[j]}, with {viewList[j]} with link: {linkList[j]}\n");
                endresult += ("------------------------------\n");
            }
        }
        public String getVideoData()
        {
            return endresult;
        }
    }
}
