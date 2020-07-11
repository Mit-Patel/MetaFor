using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MetaFor_Demo
{
    /// <summary>
    /// Interaction logic for pgReport.xaml
    /// </summary>
    public partial class pgReport : Page
    {
        public List<Metadata> metadataList;
        public pgReport()
        {
            InitializeComponent();
            //NavigationService.LoadCompleted += NavigationService_LoadCompleted;
        }

        public pgReport(List<Metadata> metadataList,pgHome window) : this()
        {
            this.metadataList = metadataList;
            this.Loaded += new RoutedEventHandler(NavigationService_LoadCompleted);            
        }

        private void NavigationService_LoadCompleted(object sender, RoutedEventArgs e)
        {
            int row = gridMain.RowDefinitions.Count;

            for (int i = 0; i < metadataList.Count; i++) gridMain.RowDefinitions.Add(new RowDefinition());

            string currentType = "";
            foreach (Metadata metadata in metadataList)
            {
                if (currentType != metadata.type)
                {
                    TextBlock heading = new TextBlock();
                    heading.Text = metadata.type + "Files";
                    heading.FontSize = 20;
                    heading.Margin = new Thickness(0, 5, 0, 0);
                    Grid.SetColumn(heading, 0);
                    Grid.SetRow(heading, row++);
                    gridMain.RowDefinitions.Add(new RowDefinition());
                    gridMain.Children.Add(heading);
                    currentType = metadata.type;
                }

                TextBlock textBlock = new TextBlock();
                textBlock.Text = metadata.file;
                textBlock.Margin = new Thickness(0, 5, 0, 0);
                Grid.SetColumn(textBlock, 0);
                Grid.SetRow(textBlock, row);

                System.Windows.Controls.TextBox textBox = new System.Windows.Controls.TextBox();
                textBox.Text = metadata.metadata;
                textBox.Style = (Style)FindResource("MaterialDesignOutlinedTextFieldTextBox");
                textBox.IsReadOnly = true;
                textBox.Margin = new Thickness(0, 5, 0, 0);
                Grid.SetColumn(textBox, 1);
                Grid.SetRow(textBox, row);

                gridMain.Children.Add(textBlock);
                gridMain.Children.Add(textBox);

                Console.WriteLine(metadata.file);
                Console.WriteLine(metadata.metadata);

                row++;
            }
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excel = new ExcelPackage();

            //add worksheet
            excel.Workbook.Worksheets.Add("Worksheet1");

            // select worksheet
            var excelWorksheet = excel.Workbook.Worksheets["Worksheet1"];

            // Create header row
            List<string[]> headerRow = new List<string[]>()
            {
                new string[] { "Extention", "File Name", "Metadata"}
            };

            // Determine the header range (e.g. A1:E1)
            string headerRange = "A1:" + Char.ConvertFromUtf32(headerRow[0].Length + 64) + "1";

            // Popular header row data
            excelWorksheet.Cells[headerRange].LoadFromArrays(headerRow);

            // Styling headers
            excelWorksheet.Cells[headerRange].Style.Font.Bold = true;
            excelWorksheet.Cells[headerRange].Style.Font.Size = 14;
            //excelWorksheet.Cells.Style.WrapText = true;

            // create list of data
            for (int i = 0; i < metadataList.Count; i++)
            {
                excelWorksheet.Cells[i + 2, 1].Value = metadataList[i].type;
                excelWorksheet.Cells[i + 2, 2].Value = metadataList[i].file;
                string data = metadataList[i].metadata;//.Replace(System.Environment.NewLine,@"\r\n");
                excelWorksheet.Cells[i + 2, 3].Value = data;
            }


            //open save dialog 
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = @"C:\";
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.Title = "Export Excel File";
            saveFileDialog.DefaultExt = "xlsx";
            saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                //save the excel file
                FileInfo excelFile = new FileInfo(saveFileDialog.FileName);
                excel.SaveAs(excelFile);
            }
        }

        private void btnGPS_Click(object sender, RoutedEventArgs e)
        {
            GPSMap gPSMap = new GPSMap();
            gPSMap.Show();
        }
    }
}
