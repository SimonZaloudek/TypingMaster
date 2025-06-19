using System.Windows;
using System.Windows.Controls;

namespace TypingMasterCore
{

    public partial class MainMenu : Window
    {

        public MainMenu()
        {
            InitializeComponent();
            contentControl.Content = new MainMenuControls.Menu(this);
        }

        public ContentControl GetContentControl()
        {
            return contentControl;
        }

        public void SetContentControl(ContentControl pContentControl)
        {
            contentControl.Content = pContentControl;
        }
    }
}