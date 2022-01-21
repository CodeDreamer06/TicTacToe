using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    public enum Mark
    {
        Empty,
        Nought,
        Cross
    }

    public partial class MainWindow : Window
    {
        private Mark[] Results = { };
        private bool Player1Turn;
        private bool GameEnded;

        public MainWindow()
        {
            InitializeComponent();
            NewGame();
        }

        private void NewGame()
        {
            Results = new Mark[9].Select(m => Mark.Empty).ToArray();
            Player1Turn = true;

            SetupButtons();

            GameEnded = false;
        }

        private void SetupButtons()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Button button = new Button();
                    button.Name = $"Button{i}_{j}";
                    button.Content = String.Empty;
                    button.Background = new SolidColorBrush(Color.FromRgb(33, 34, 43));
                    button.Foreground = Brushes.Thistle;
                    button.Click += Button_Click;

                    Grid.SetColumn(button, j);
                    Grid.SetRow(button, i);
                    Container.Children.Add(button);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (GameEnded)
            {
                NewGame();
                return;
            }

            var button = (Button)sender;
            var row = Grid.GetRow(button);
            var column = Grid.GetColumn(button);

            var index = column + (row * 3);

            if (Results[index] != Mark.Empty)
            {
                return;
            }

            Results[index] = Player1Turn ? Mark.Cross : Mark.Nought;
            button.Content = Player1Turn ? "X" : "O";
            Player1Turn ^= true;

            if (Player1Turn)
            {
                button.Foreground = Brushes.Azure;
            }

            GameEnded = CheckForWinner();
        }

        private bool CheckForWinner()
        {
            var buttons = Container.Children.Cast<Button>().ToList();

            for (int i = 0; i <= 6; i += 3)
            {
                if (Results[i] != Mark.Empty && (Results[i] & Results[i + 1] & Results[i + 2]) == Results[i])
                {
                    buttons[i].Background = buttons[i + 1].Background = buttons[i + 2].Background = Brushes.Green;
                    return true;
                }
            }

            for (int i = 0; i <= 2; i++)
            {
                if (Results[i] != Mark.Empty && (Results[i] & Results[i + 3] & Results[i + 6]) == Results[i])
                {
                    buttons[i].Background = buttons[i + 3].Background = buttons[i + 6].Background = Brushes.Green;
                    return true;
                }
            }

            for (int i = 2; i <= 4; i += 2)
            {
                if (Results[4] != Mark.Empty && (Results[4] & Results[4 - i] & Results[4 + i]) == Results[4])
                {
                    buttons[4].Background = buttons[4 - i].Background = buttons[4 + i].Background = Brushes.Green;
                    return true;
                }
            }

            if (Results.All(m => m != Mark.Empty))
            {
                GameEnded = true;
                return true;
            }

            return false;
        }
    }
}
