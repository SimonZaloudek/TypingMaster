using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TypingMasterCore.Core.Entities
{
    internal class Enemy
    {
        private readonly string[] textures = ["/Assets/zombie1.png", "/Assets/zombie2.png"];
        private readonly Random random = new();
        private readonly bool golden = false;

        public string Word { get; set; }
        public TextBlock TextBlock { get; set; }
        public Grid Grid { get; set; }
        public Image Image { get; set; }
        public int Column { get; set; }

        public Enemy(string word)
        {
            Word = word;
            Grid = new Grid();

            int percentage = random.Next(0, 100);
            string uri = textures[random.Next(0, 2)];
            if (percentage > 95)
            {
                uri = "/Assets/goldenZombie.png";
                golden = true;
            }

            Image = new Image();
            BitmapImage bitmapImage = new(new Uri(uri, UriKind.Relative));
            Image.Source = bitmapImage;
            Image.Width = 100;
            Image.Height = 100;
            Image.HorizontalAlignment = HorizontalAlignment.Center;

            TextBlock = new TextBlock()
            {
                Text = word,
                Foreground = Brushes.White,
                FontSize = 20,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Center,
            };

            Grid.Children.Add(TextBlock);
            Grid.Children.Add(Image);

            Canvas.SetLeft(Grid, Canvas.GetLeft(Image));
            Canvas.SetTop(Grid, Canvas.GetTop(Image));
        }

        public bool IsGolden()
        {
            return golden;
        }
    }
}
