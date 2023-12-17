using System;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebApp;
class Program
{
    static void Main()
    {
        MainMenu mainMenu = new MainMenu();

        List<String> availableChoices = new List<String> {"1", "2", "3", "q", "Q"};
        String choice = mainMenu.getFirstMenu();
        IWebDriver driver;
        while (!availableChoices.Contains(choice))
        {
            choice = mainMenu.getRepeatingMenu();
        }
        while (choice != ("q") && choice != "Q")
        {
            String searchTerm = mainMenu.getSearchTerm();
            Console.WriteLine(searchTerm);
            WebDriver webDriver = new WebDriver();
            driver = webDriver.setDriver();
            String endresult;
            if (choice.Equals("1"))
            {
                Youtube youtube = new Youtube();
                youtube.setUrl(searchTerm);
                youtube.setVideo(driver);
                youtube.setVideoData();
                endresult = youtube.getVideoData();
            }
            else if (choice.Equals("2"))
            {
                ICTjobs job = new ICTjobs();
                job.setUrl(searchTerm);
                job.setJob(driver);
                job.setJobData();
                endresult = job.getJobData();
            }
            else
            {
                Twitter twitter = new Twitter();
                twitter.setUrl(searchTerm);
                twitter.setTweet(driver);
                twitter.setTweetData();
                endresult = twitter.getTweetData();
            }
            Console.WriteLine(endresult);
            Console.Write("Enter your choice: ");
            choice = Console.ReadLine();
            while (!availableChoices.Contains(choice))
            {
                choice = mainMenu.getRepeatingMenu();
            }
        }
        Console.WriteLine("Quitting the application...");
        System.Threading.Thread.Sleep(2000);
    }
}
