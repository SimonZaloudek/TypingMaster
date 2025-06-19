using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using TypingMasterCore.Core.Entities;

namespace TypingMasterCore.Core
{
    public partial class Game : Window
    {
        private readonly DispatcherTimer _gameTimer = new();
        private readonly DispatcherTimer _spawnTimer = new();
        private readonly Random random = new();

        private readonly Rectangle[] columns;
        private int _playerColumn = 3;

        private readonly List<Enemy> enemies = [];
        private readonly List<string> _words;

        private int health = 5;
        private int score = 0;
        private double _speed = 1.00;

        private bool input;
        private string? keyInput;

        public Game(List<string> words, string skin)
        {
            InitializeComponent();
            Player.Source = new BitmapImage(new Uri(skin, UriKind.Relative));
            _words = words;

            columns = [Column1, Column2, Column3, Column4, Column5];
            Player.Source = new BitmapImage(new Uri(skin, UriKind.Relative));
            Canvas.SetLeft(Player, Canvas.GetLeft(Column3) + 78);
            Canvas.SetLeft(gunfire, (Canvas.GetLeft(Column3) + 140));

            _gameTimer.Tick += new EventHandler(Tick);
            _gameTimer.Interval = TimeSpan.FromMilliseconds(10);
            _gameTimer.Start();

            _spawnTimer.Tick += new EventHandler(Spawn);
            _spawnTimer.Interval = TimeSpan.FromSeconds(2);
            _spawnTimer.Start();
        }

        private void Spawn(object? sender, EventArgs e)
        {
            if (score > 10)
            {
                _spawnTimer.Interval = TimeSpan.FromMilliseconds(1500);
            }
            if (score % 10 == 0)
                _speed += 0.10;
            if (score > 50)
                _spawnTimer.Interval = TimeSpan.FromMilliseconds(1200);

            Enemy newEnemy = new(_words[random.Next(0, _words.Count - 1)])
            {
                Column = random.Next(1, 5)
            };
            Canvas.SetLeft(newEnemy.Grid, Canvas.GetLeft(columns[newEnemy.Column - 1]) + 78);
            Canvas.SetTop(newEnemy.Grid, -100);
            if (!canvas.Children.Contains(newEnemy.Grid))
            {
                canvas.Children.Add(newEnemy.Grid);
                enemies.Add(newEnemy);
            }

        }

        private void Tick(object? sender, EventArgs e)
        {
            if (health == 0)
            {
                EndGame();
            }

            Core();
        }

        public void Core()
        {
            gunfire.Visibility = Visibility.Hidden;
            Enemy? enemyForDeletion = null;
            foreach (Enemy forEnemy in enemies)
            {
                Canvas.SetTop(forEnemy.Grid, Canvas.GetTop(forEnemy.Grid) + _speed);

                if (forEnemy.Column == _playerColumn)
                {
                    if (forEnemy.Word.Length > 0 && keyInput != null)
                    {
                        if (input && keyInput[0].Equals(forEnemy.Word[0]))
                        {
                            gunfire.Visibility = Visibility.Visible;
                            forEnemy.TextBlock.Text = forEnemy.Word[1..];
                            forEnemy.Word = forEnemy.TextBlock.Text;


                            input = false;
                            keyInput = "";
                        }
                        if (forEnemy.TextBlock.Text.Length < 1)
                        {
                            if (forEnemy.IsGolden())
                            {
                                score += 10;
                            }
                            else
                            {
                                score++;
                            }
                            enemyForDeletion = forEnemy;
                            scoreTB.Text = score.ToString();
                        }
                    }
                }

                if (Canvas.GetTop(forEnemy.Grid) > 435)
                {
                    enemyForDeletion = forEnemy;
                    health--;
                    healthTB.Text = health.ToString();
                }
            }
            if (enemyForDeletion != null)
            {
                enemies.Remove(enemyForDeletion);
                canvas.Children.Remove(enemyForDeletion.Grid);
            }
        }

        private void EndGame()
        {
            _gameTimer.Stop();
            _spawnTimer.Stop();

            MessageBoxResult result;
            result = MessageBox.Show("GAME OVER! \n Total score: " + score, "GAME OVER!", MessageBoxButton.OK);
            if (result == MessageBoxResult.OK)
            {
                BackToMenu();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs? e)
        {
            if (e != null)
            {
                if (e.Key == Key.Left)
                {
                    if (_playerColumn > 1)
                    {
                        _playerColumn--;
                        Canvas.SetLeft(Player, (Canvas.GetLeft(columns[_playerColumn - 1]) + 78));
                        Canvas.SetLeft(gunfire, (Canvas.GetLeft(columns[_playerColumn - 1]) + 140));
                    }
                    input = false;
                }
                else if (e.Key == Key.Right)
                {
                    if (_playerColumn < 5)
                    {
                        _playerColumn++;
                        Canvas.SetLeft(Player, (Canvas.GetLeft(columns[_playerColumn - 1]) + 78));
                        Canvas.SetLeft(gunfire, (Canvas.GetLeft(columns[_playerColumn - 1]) + 140));

                    }
                    input = false;
                }
                else if (e.Key == Key.Escape)
                {
                    _gameTimer.Stop();
                    _spawnTimer.Stop();
                    Pause pause = new();
                    pause.ShowDialog();
                    if (pause.DialogResult == false)
                    {
                        _gameTimer.Start();
                        _spawnTimer.Start();
                    }
                    else
                    {
                        BackToMenu();
                    }
                }
                else
                {
                    input = true;
                    keyInput = e.Key.ToString();
                }
            }
        }

        public void BackToMenu()
        {
            MainMenu menu = new();
            menu.Show();

            this.Close();
        }
    }
}
