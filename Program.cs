using System;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebApp;
class Program
{
    static void Main()
    {
        mainMenu mainMenu = new mainMenu();

        List<String> availableChoices = new List<String> {"1", "2", "3", "q", "Q"};
        String choice = mainMenu.getFirstMenu();

        while (!availableChoices.Contains(choice))
        {
            choice = mainMenu.getRepeatingMenu();
        }
        while (choice != ("q") && choice != "Q")
        {
            String searchTerm = mainMenu.getSearchTerm();
            Console.WriteLine(searchTerm);
            String endresult = "";
            if (choice.Equals("1"))
            {
                Youtube youtube = new Youtube();
                youtube.setDriver();
                youtube.setUrl(searchTerm);
                youtube.setVideo();
                youtube.setVideoData();
                endresult = youtube.getVideoData();
            }
            else if (choice.Equals("2"))
            {
                ICTjobs job = new ICTjobs();
                job.setDriver();
                job.setUrl(searchTerm);
                job.setJob();
                job.setJobData();
                endresult = job.getJobData();
            }
            else
            {
                Twitter twitter = new Twitter();
                twitter.setDriver();
                twitter.setUrl(searchTerm);
                twitter.setTweet();
                twitter.setTweetData();
                endresult = twitter.getTweetData();
            }
            Console.WriteLine(endresult);
            Console.Write("Enter your choice: ");
            choice = Console.ReadLine();
        }
        Console.WriteLine("Quiting the application...");
        System.Threading.Thread.Sleep(2000);
    }
}
