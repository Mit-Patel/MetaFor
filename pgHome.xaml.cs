using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Threading;

namespace MetaFor_Demo
{
    /// <summary>
    /// Interaction logic for pgHome.xaml
    /// </summary>
    public partial class pgHome : Page
    {
        MaterialMessageBox messageBox;
        List<Metadata> metadataList = new List<Metadata>();
        int filePathLength;
        string[] ext;
        
        public pgHome()
        {
            InitializeComponent();
            if (File.Exists("gpstemp.txt")) File.Delete("gpstemp.txt");
        }

        private bool validate()
        {
            SnackbarMessageQueue messageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(2));
            sbErr.MessageQueue = messageQueue;
            if (txtSourcePath.Text.Trim().Length == 0)
            {
                sbErr.IsActive = true;
                messageQueue.Enqueue("Source Path cannnot be blank!");
                return false;
            }

            if (cbPng.IsChecked == false && cbJpg.IsChecked == false && cbMp3.IsChecked == false && cbMkv.IsChecked == false && cbMp4.IsChecked == false && cbDoc.IsChecked == false && cbExcel.IsChecked == false && cbPdf.IsChecked == false && cbZip.IsChecked == false && cbJpeg.IsChecked == false)
            {
                sbErr.IsActive = true;
                messageQueue.Enqueue("Please select atleast one Extraction Type!");
                return false;
            }            

            return true;
        }

        // Process all files in the directory passed in, recurse on any directories that are found, and process the files they contain.        
        private void ProcessDirectory(ref List<string> files, string targetDirectory, string type)
        {
            // Get all the files according to their filetypes
            files.AddRange(Directory.GetFiles(targetDirectory, "*." + type));

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            Console.WriteLine(subdirectoryEntries);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(ref files, subdirectory, type);
        }

        // Insert logic for processing found files here.
        private void ProcessFile(string type, string dirPath)
        {
            List<string> files = new List<string>();

            ProcessDirectory(ref files, dirPath, type);

            foreach (string fileName in files)
            {
                try
                {
                    // Creating a new process 
                    ProcessStartInfo start = new ProcessStartInfo();

                    // Settting the parameters of the process
                    start.FileName = @"C:\Python38\python.exe";

                    // Setting the arguments for the command
                    start.Arguments = string.Format("pyfiles\\{0} \"{1}\"", type + "meta.py", fileName);

                    start.UseShellExecute = false;
                    start.CreateNoWindow = true;
                    start.RedirectStandardInput = true;
                    start.RedirectStandardOutput = true;
                    start.RedirectStandardError = true;

                    // Running the process and printing the output
                    using (Process process = Process.Start(start))
                    {
                        using (StreamReader reader = process.StandardOutput)
                        {
                            string result = reader.ReadToEnd();
                            metadataList.Add(new Metadata(fileName.Substring(filePathLength + 1), type, result));
                        }
                    }
                }
                catch(Exception e)
                {
                    System.Windows.MessageBox.Show("An Error has occurred: " + e.Message,"Error",MessageBoxButton.OKCancel, (MessageBoxImage)MessageBoxIcon.Error);
                }
            }            
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            SnackbarMessageQueue messageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(2));
            sbErr.MessageQueue = messageQueue;

            sbErr.IsActive = true;
            messageQueue.Enqueue("Processing data! Please Wait!");
            // Form Validation
            if (!validate()) return;
            
            //Get the path of Directory from Source path TextBox
            string path = txtSourcePath.Text.Trim().ToLower();
            //Check if the directory exists 
            if (Directory.Exists(path))
            {
                filePathLength = path.Length;
                //process each filetype
                if (cbJpeg.IsChecked == true)
                {
                    
                    Parallel.Invoke(() =>
                    {
                        ProcessFile("jpeg", path);
                    });
                }
                if (cbJpg.IsChecked == true)
                {
                    
                    Parallel.Invoke(() =>
                    {
                        ProcessFile("jpg", path);
                    });
                }
                if (cbPng.IsChecked == true)
                {
                    
                    Parallel.Invoke(() =>
                    {
                        ProcessFile("png", path);
                    });
                }
                if (cbMp4.IsChecked == true)
                {
                    Parallel.Invoke(() =>
                    {
                        ProcessFile("mp4", path);
                    });
                }
                if (cbMkv.IsChecked == true)
                {
                    Parallel.Invoke(() =>
                    {
                        ProcessFile("mkv", path);
                    });
                }
                if (cbMp3.IsChecked == true)
                {
                    Parallel.Invoke(() =>
                    {
                        ProcessFile("mp3", path);
                    });
                }
                if (cbDoc.IsChecked == true)
                {
                    Parallel.Invoke(() =>
                    {
                        ProcessFile("doc", path);
                    });
                }
                if (cbExcel.IsChecked == true)
                {
                    Parallel.Invoke(() =>
                    {
                        ProcessFile("xls", path);
                    });
                }
                if (cbPdf.IsChecked == true)
                {
                    Parallel.Invoke(() =>
                    {
                        ProcessFile("pdf", path);
                    });
                }
                if (cbZip.IsChecked == true)
                {
                    Parallel.Invoke(() =>
                    {
                        ProcessFile("zip", path);
                    });
                }
            }
            else
            {                
                // If the directory does not exists, then print an error message
                bool? Result = new MaterialMessageBox("No Directory found!!!", MessageType.Error, MessageButtons.Ok).ShowDialog();
            }
            
            NavigationService.Navigate(new pgReport(metadataList,this));
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            // Show the FolderBrowserDialog
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();

            // Disallow to make New Folder
            folderDlg.ShowNewFolderButton = false;

            // Getting the result of the FolderBrowserDialog
            DialogResult result = folderDlg.ShowDialog();

            // If the result if OK then change the Source Path to the Selected Path
            if (result == DialogResult.OK)
            {
                // Setting the text of Source Path Textbox to the Selected path of FolderBrowserDialog
                txtSourcePath.Text = folderDlg.SelectedPath;
            }
        }
    }
}