
using Microsoft.Win32;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media.Animation;

namespace TypingMasterEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            saveButton.IsEnabled = false;
        }

        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            if (TextBox.Text.Length > 0) 
            {
                SaveFileDialog saveDialog = new()
                {
                    Title = "Save to .txt",
                    Filter = ".txt files | *.txt",
                    FileName = "CustomTextField1",
                    DefaultExt = ".txt"
                };

                bool? dialogResult = saveDialog.ShowDialog();
                if (dialogResult == true) 
                {
                    SaveToTxtFile(saveDialog.FileName, SerializeList());
                }
            }
        }

        private string[] SerializeList() 
        {
            string[] array = TextBox.Text.Split(" ");
            List<string> list = [.. array]; 

            foreach (string word in array)
            {
                if (word == "" || word.Contains(' ') || !word.All(Char.IsLetter))
                    list.Remove(word);
            }
            array = [.. list];
            for (int i = 0; i < array.Length; i++) 
            {
                if (array[i].Length > 12)
                    list[i] = array[i][..12];
            } 
            return [.. list];
        }

        private static void SaveToTxtFile(string path, string[] list) 
        {
            File.WriteAllLines(path, list);
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (TextBox.Text.Length > 0)
            {
                saveButton.IsEnabled = true;
            }
            else 
            {
                saveButton.IsEnabled = false;
            }
        }
    }
}