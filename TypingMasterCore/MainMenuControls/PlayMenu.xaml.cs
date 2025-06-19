using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using TypingMasterCore.Core;
using TypingMasterEditor;

namespace TypingMasterCore.MainMenuControls
{
    public partial class PlayMenu : UserControl
    {
        private readonly MainMenu _mainMenu;
        DictionaryParser? dictionaryParser;
        List<string>? customList;
        private readonly string[] skins = ["/Assets/player3.png", "/Assets/player2.png"];
        private int _skin = 0;

        public PlayMenu(MainMenu mainMenu)
        {
            InitializeComponent();
            _mainMenu = mainMenu;
            startButton.IsEnabled = false;
            PlayerRefresh(0);
        }

        private void PlayerRefresh(int skin) 
        {
            Player.Source = new BitmapImage(new Uri(skins[skin], UriKind.Relative));
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            _mainMenu.SetContentControl(new Menu(_mainMenu));
        }

        private void StartButtonClick(object sender, RoutedEventArgs e)
        {
            List<string> dictionary = [];
            if (difficultySetting.SelectedItem == easyDiff || difficultySetting.SelectedItem == mediumDiff || difficultySetting.SelectedItem == hardDiff || difficultySetting.SelectedItem == impossDiff)
            {
                dictionaryParser = new DictionaryParser((ComboBoxItem)difficultySetting.SelectedItem, difficultySetting.Items);
                if (dictionaryParser.Dictionary != null)
                    dictionary = dictionaryParser.Dictionary;
            }
            else
            {
                if (customList != null)
                dictionary = customList;
            }
            if (dictionary != null) 
            {
                Game game = new(dictionary, skins[_skin]);
                game.Show();
                _mainMenu.Close();
            }
        }

        private void DifficultySetting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            startButton.IsEnabled = true;
        }

        private void LeftButton_Click(object sender, RoutedEventArgs e)
        {
            if (_skin > 0)
                _skin--;
            PlayerRefresh(_skin);
        }

        private void RightButton_Click(object sender, RoutedEventArgs e)
        {
            if (_skin < 1)
                _skin++;
            PlayerRefresh(_skin);
        }

        private void CustomFileButton(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new()
            {
                Title = "Select a .txt file",
                Filter = ".txt files | *.txt"
            };
            bool? opened = fileDialog.ShowDialog();
            if (opened == true)
            {
                TxtHandler txtHandler = new(fileDialog.FileName);
                if (txtHandler.Handle())
                {
                    customList = txtHandler.Dictionary;
                    difficultySetting.Items.Add("CUSTOM DICTIONARY");
                }
                else 
                {
                    _ = MessageBox.Show(_mainMenu, "Input file does not match the required format!", "Input file error!", MessageBoxButton.OK);
                }
            }
        }
    }
}
