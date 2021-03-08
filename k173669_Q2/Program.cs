using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace k173669_Q2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read appSettings from config file
            var appSettings = ConfigurationManager.AppSettings;

            // Check whether both keys are present and contain valid paths
            if(appSettings.AllKeys.Contains("InputFilePath") == false ||
                appSettings.AllKeys.Contains("OutputFolderPath") == false
                )
            {
                throw new ApplicationException(
                    "AppSettings do not contain InputFilePath and/or OutputFolderPath"
                    );
            }

            string inputFilePath = appSettings["InputFilePath"];
            string outputFolderPath = appSettings["OutputFolderPath"];

            if (File.Exists(inputFilePath) == false ||
                Uri.TryCreate(outputFolderPath, uriKind: UriKind.RelativeOrAbsolute, out Uri _) == false
                )
            {
                throw new ApplicationException(
                    "Could not parse InputFilePath and/or OutputFolderPath as valid paths."
                    );
            }

            Dictionary<string, List<Scrips>> stocks = ReadScripsFromHtml(filename: inputFilePath);

            StoreScripsToXml(stocks:stocks, path:outputFolderPath);
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

        /// <summary>
        /// Creates a timestamped folder under the given path. Also creates subfolders for each
        /// category, and places one XML per scrip in the relevant category subfolder.
        /// </summary>
        /// <param name="stocks"></param>
        /// <param name="path"></param>
        static void StoreScripsToXml(Dictionary<string,List<Scrips>> stocks, string path)
        {
            string newFolder = Path.Combine(path, DateTime.Now.ToString("ddMMMyy hh.mm tt"));

            Directory.CreateDirectory(newFolder);

            XmlSerializer serializer = new XmlSerializer(typeof(List<Scrips>));

            List<Scrips> totalScrips = new List<Scrips>();

            foreach (var item in stocks)
            {
                // Remove / to prevent nested folders
                string categoryName = item.Key.Replace("/", "");

                // Create a folder against each category
                string subFolder = Path.Combine(newFolder, categoryName);
                Directory.CreateDirectory(subFolder);

                // Create one XML inside each folder
                string fileName = Path.Combine(subFolder, $"{categoryName}.xml");

                using FileStream fileStream = new FileStream(fileName, FileMode.Create);

                var scrips = item.Value;
                totalScrips.AddRange(scrips);
                serializer.Serialize(fileStream, scrips);
            }

            /// Also store the single XML containing every scrip
            using FileStream fileStream1 = new FileStream(
                Path.Combine(newFolder, "Summary.xml"), FileMode.Create
                );

            serializer.Serialize(fileStream1, totalScrips);
        }
    }
}
