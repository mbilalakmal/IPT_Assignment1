using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace k173669_Q2
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\bilal\Downloads\Summary04Mar21.html";

            Dictionary<string, List<Scrips>> stocks = ReadScripsFromHtml(filename:path);

            Console.WriteLine(stocks.Count);

            // TODO: Create a single timestamped folder. Create a sub-folder against
            // each category. Create an XML file for each scrip in created sub-folders.
            StoreScripsToXml(stocks, @"C:\Users\bilal\Downloads\IPT_Assignment_Q2");
        }

        /// <summary>
        /// Reads an HTML file and scrapes the cateogories, scrips, and current prices.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        static Dictionary<string, List<Scrips>> ReadScripsFromHtml(string filename)
        {
            Dictionary<string, List<Scrips>> stocks = new Dictionary<string, List<Scrips>>();

            HtmlDocument document = new HtmlDocument();
            document.Load(filename);

            HtmlNode htmlNode = document.DocumentNode.SelectSingleNode("//body");

            // Select each table head for category name
            foreach (HtmlNode nNode in htmlNode.Descendants("th"))
            {
                if (nNode.NodeType != HtmlNodeType.Element){ continue;}

                string category = nNode.InnerText.Trim();

                if (stocks.ContainsKey(category) != true)
                {
                    /// If a new cateogory is encountered, initialize its List
                    stocks[category] = new List<Scrips>();
                }

                // Reach table that includes all scrips and their values in this category
                HtmlNode pNode = nNode.ParentNode.ParentNode.ParentNode;

                var collection = pNode.SelectNodes(".//comment()/following-sibling::td").ToList();

                // Scrip names are 8 indices apart
                for (int idx = 0; idx < collection.Count; idx += 8)
                {
                    string companyName = collection[idx].InnerText.Trim();
                    // Current price is 5 indices ahead of scrip name
                    var currentValue = collection[idx + 5];

                    if (double.TryParse(currentValue.InnerText, out double currentPrice) == false)
                    {
                        throw new ApplicationException($"Unable to parse current price of {companyName} as a double.");
                    }

                    Scrips stock = new Scrips
                    {
                        Category = category,
                        Price = currentPrice,
                        Scrip = companyName,
                    };

                    stocks[category].Add(stock);
                }
            }
            return stocks;
        }

        static void StoreScripsToXml(Dictionary<string,List<Scrips>> stocks, string path)
        {
            string newFolder = System.IO.Path.Combine(path, DateTime.Now.ToString("ddMMMyy hh.mm tt"));

            System.IO.Directory.CreateDirectory(newFolder);

            foreach(var item in stocks)
            {
                // TODO: create a folder against each category
                string subFolder = System.IO.Path.Combine(newFolder, item.Key);
                System.IO.Directory.CreateDirectory(subFolder);

                // Create one XML inside each folder

                var scrips = item.Value;

                foreach (var scrip in scrips)
                {
                    string filename = System.IO.Path.Combine(subFolder, scrip.Scrip + ".xml");
                    scrip.Serialize(filename:filename);
                }
            }

        }


    }
}
