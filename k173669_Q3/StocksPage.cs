using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace k173669_Q3
{
    public partial class StocksPage : Form
    {
        BindingList<Scrips> ScripsList;

        public StocksPage()
        {
            InitializeComponent();

            // Read appSettings from config file
            var appSettings = ConfigurationManager.AppSettings;
            if(appSettings.AllKeys.Contains("RootFolder") && Directory.Exists(appSettings["RootFolder"]))
            {
                string folderPath = appSettings["RootFolder"];
                var directory = new DirectoryInfo(folderPath);
                var latestSubDir = directory.GetDirectories()
                    .OrderByDescending(dir => dir.LastWriteTime)
                    .FirstOrDefault();

                Console.WriteLine(latestSubDir.Name);
                Debug.WriteLine(latestSubDir.Name);

                /// Check if a directory indeed exists and whether it contains the single XML file
                if(latestSubDir != default(DirectoryInfo))
                {
                    var xmlFile = latestSubDir.GetFiles()
                        .Where(file => file.Extension == ".xml")
                        .OrderByDescending(file => file.LastWriteTime)
                        .FirstOrDefault();

                    if(xmlFile != default(FileInfo))
                    {
                        /// Load the Scrips from XML file
                        LoadScripsFromXml(xmlFile.FullName);
                    }
                    else
                    {
                        /// TODO: Display error to the user
                    }
                }
                else
                {
                    /// TODO: Display error to the user
                }
            }
            else
            {
                /// TODO: Display error to the user
            }

        }

        private void LoadScripsFromXml(string filePath)
        {
            Debug.WriteLine("WOW:");

            DataSet dataSet = new DataSet();
            dataSet.ReadXml(filePath);
        }

    }
}
