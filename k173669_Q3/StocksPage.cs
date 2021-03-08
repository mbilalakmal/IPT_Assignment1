using System;
using System.Collections.Generic;
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
        public StocksPage()
        {
            InitializeComponent();

            CheckRootDirectory();
        }

        private void CheckRootDirectory()
        {
            // Read appSettings from config file
            var appSettings = ConfigurationManager.AppSettings;
            if (appSettings.AllKeys.Contains("RootFolder") && Directory.Exists(appSettings["RootFolder"]))
            {
                string folderPath = appSettings["RootFolder"];
                var directory = new DirectoryInfo(folderPath);
                var latestSubDir = directory.GetDirectories()
                    .OrderByDescending(dir => dir.LastWriteTime)
                    .FirstOrDefault();

                /// Check if a directory indeed exists and whether it contains the single XML file
                if (latestSubDir != default(DirectoryInfo))
                {
                    var xmlFile = latestSubDir.GetFiles()
                        .Where(file => file.Extension == ".xml")
                        .OrderByDescending(file => file.LastWriteTime)
                        .FirstOrDefault();

                    if (xmlFile != default(FileInfo))
                    {
                        var categoryNames = latestSubDir.GetDirectories()
                            .Select(dir => dir.Name).ToList();

                        /// Load the Scrips from XML file
                        LoadScripsFromXml(filePath: xmlFile.FullName, categories: categoryNames);
                    }
                    else
                    {
                        /// Display error to the user about no xml found
                        MessageBox.Show(
                            text: "Unable to locate any XML files inside the subdirectory.",
                            caption: "XML File Not Found"
                        );
                    }
                }
                else
                {
                    /// Display error to the user about no subdirectories found
                    MessageBox.Show(
                        text: "Unable to locate any directories inside the root folder.",
                        caption: "SubDirectory Not Found"
                    );
                }
            }
            else
            {
                /// Display error to the user about invalid appSettings
                MessageBox.Show(
                    text: "Unable to locate the specified root folder in appSettings.",
                    caption: "RootFolder Not Found"
                    );
            }
        }

        /// <summary>
        /// Reads Data from provided XML file path and sets it as DataSource
        /// for scripsDataGridView.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="categories"></param>
        private void LoadScripsFromXml(string filePath, List<string> categories)
        {
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(filePath);

            scripsDataGridView.DataSource = dataSet.Tables[0];

            categoryComboBox.DataSource = categories;
            categoryComboBox.SelectedIndex = -1;
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            CheckRootDirectory();
        }

        private void categoryComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            (scripsDataGridView.DataSource as DataTable)
                .DefaultView.RowFilter = $"Category = '{(categoryComboBox.SelectedItem as string)}'";
        }

        private void categoryComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter && categoryComboBox.SelectedItem != null)
            {
                categoryComboBox_SelectionChangeCommitted(sender,e);
            }
        }
    }
}
