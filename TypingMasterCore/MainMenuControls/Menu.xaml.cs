using System.Windows;
using System.Windows.Controls;

namespace TypingMasterCore.MainMenuControls
{
    public partial class Menu : UserControl
    {
        private readonly MainMenu _mainMenu;

        public Menu(MainMenu mainMenu)
        {
            InitializeComponent();
            _mainMenu = mainMenu;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _mainMenu.SetContentControl(new PlayMenu(_mainMenu));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _mainMenu.SetContentControl(new HelpMenu(_mainMenu));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            new TypingMasterEditor.MainWindow().Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            _mainMenu.Close();
        }
    }
}
