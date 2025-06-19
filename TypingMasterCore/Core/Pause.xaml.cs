using System.Windows;

namespace TypingMasterCore.Core
{
    /// <summary>
    /// Interaction logic for Pause.xaml
    /// </summary>
    public partial class Pause : Window
    {
        public Pause()
        {
            InitializeComponent();
        }

        private void ResumeClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BackToMenuClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }
    }
}
