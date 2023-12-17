using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp
{
    public class mainMenu
    {
        public static string getFirstMenu() {
            Console.WriteLine("Welcome to my webscraper \nSelect one of the following choices:\n1. Youtube\n2. ICTjobs\n3. Twitter\nQ. Quit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();
            return choice;
        }
        public static string getRepeatingMenu() { 
            Console.Write("Please enter one of the choices above: ");
            string choice = Console.ReadLine();
            return choice;
        }

        public static string getSearchTerm() {
            Console.Write("Enter your search term: ");
            string searchTerm = Console.ReadLine();
            return searchTerm;
        }
    }
}
