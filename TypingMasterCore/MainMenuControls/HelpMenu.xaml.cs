using System.Windows;
using System.Windows.Controls;

namespace TypingMasterCore.MainMenuControls
{

    public partial class HelpMenu : UserControl
    {

        readonly MainMenu _mainMenu;

        public HelpMenu(MainMenu mainMenu)
        {
            InitializeComponent();
            _mainMenu = mainMenu;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _mainMenu.SetContentControl(new Menu(_mainMenu));
        }
    }
}
