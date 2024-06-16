using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System;
using System.Text.RegularExpressions;

namespace BuggedFileReader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private OpenFileDialog ofd = new OpenFileDialog();
        private string lastOpenFile = "";
        public MainWindow()
        {
            InitializeComponent();
            ofd.InitialDirectory = Directory.GetCurrentDirectory();
        }

        /**
         * Exceptions are not handled
         */
        private void BTNOpenFile_Click(object sender, RoutedEventArgs e)
        {
                lastOpenFile = TBXFileName.Text;
                loadFile();
        }

        /**
         * This method should open a Dialog window
         */
        private void BTNOpenDialog_Click(object sender, RoutedEventArgs e)
        {
            bool? result = ofd.ShowDialog();
            if (result!=null && result == true)
            {
                lastOpenFile = ofd.FileName;
                loadFile();    
            }
        }

        static string ReplaceDoubleSpaces(string str)
        {
            bool hasDoubleSpaces = true;
            while (hasDoubleSpaces)
            {
                string replaced = Regex.Replace(str, @"\s{2,}", " ");
                if (replaced != str)
                {
                    str = replaced;
                }
                else
                {
                    hasDoubleSpaces = false;
                }
            }
            return str.Trim();
        }

        private int wordCount()
        {
            int count =
                ReplaceDoubleSpaces(
                    TBKContent.Text
                    .Replace('\n', ' ')
                    .Replace('\'', ' ')
                    .Replace('-', ' ')
                    .Replace('»', ' ')
                    .Replace('«', ' ')
                    .Replace('*', ' ')
                    .Replace(':', ' ')
                    .Replace(',', ' ')
                    .Replace('\"', ' ')
                )
                .Split(' ')
                .Length;

            return count;
        }

        /**
         * This method should open the file... Where does the class "File" come from?
         */
        private void loadFile()
        {
            try {
                string text = File.ReadAllText(lastOpenFile);
                TBKContent.Text = text;
                LBLWordCountValue.Content = wordCount();
            } catch (System.ArgumentException) {
                MessageBox.Show("File name shall not be empty.");
            } catch (System.IO.FileNotFoundException) {
                MessageBox.Show("File path could not be read.\nMake sure the path is correct and readable.");
            } catch (Exception e) {
                MessageBox.Show(e.Message);
            }
        }

        /**
         * This method must be triggered by the combobox. Did we configure all the possible options?
         */
        private void CBXCase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cbx = (ComboBox)sender;
            if(lastOpenFile != "")
            {
                loadFile();
            }
            if (cbx.SelectedIndex == 0)
            {
                TBKContent.Text = TBKContent.Text.ToUpper();
            }
            else if (cbx.SelectedIndex == 1)
            {
                TBKContent.Text = TBKContent.Text.ToLower();
            }
        }
    }
}
