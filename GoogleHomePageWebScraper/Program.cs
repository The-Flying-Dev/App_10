﻿using GoogleHomePageWebScraper.Builders;
using GoogleHomePageWebScraper.Data;
using GoogleHomePageWebScraper.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GoogleHomePageWebScraper
{
    class Program
    {
        private const string Method = "search";
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Please enter which city you would like to scrape information from:");
                var craiglistCity = Console.ReadLine() ?? string.Empty; // Null coalescing

                Console.WriteLine("Please enter the CraigsList category that you would like to scrape:");
                var craigslistCategoryName = Console.ReadLine() ?? string.Empty; // Null coalescing

                using (WebClient client = new WebClient())
                {
                    string content = client.DownloadString($"http://{craiglistCity.Replace(" ", string.Empty)}.craigslist.org/{Method}/{craigslistCategoryName}");

                    ScrapeCriteria scrapeCriteria = new ScrapeCriteriaBuilder()
                        .WithData(content)
                        .WithRegex(@"<a href=\""(.*?)\"" data-id=\""(.*?)\"" class=\""result-title hdrlnk\"" id=\""(.*?)\"" >(.*?)</a>")
                        .WithRegexOption(RegexOptions.ExplicitCapture)
                        .WithPart(new ScrapeCriteriaPartBuilder()
                            .WithRegex(@">(.*?)</a>")
                            .WithRegexOption(RegexOptions.Singleline)
                        .Build())
                        .WithPart(new ScrapeCriteriaPartBuilder()
                            .WithRegex(@"href=\""(.*?)\""")
                            .WithRegexOption(RegexOptions.Singleline)
                            .Build())
                        .Build();

                    Scraper scraper = new Scraper();

                    var scrapedElements = scraper.Scrape(scrapeCriteria);

                    if (scrapedElements.Any())
                    {
                        foreach (var scrapedElement in scrapedElements) Console.WriteLine(scrapedElement);
                    }
                    else
                    {
                        Console.WriteLine("There were no matches for the specified scrape criteria.");
                    }

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
    }
}
