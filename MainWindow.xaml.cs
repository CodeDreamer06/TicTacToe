using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    public enum MarkType
    {
        Free,
        Nought,
        Cross
    }

    public partial class MainWindow : Window
    {
        private MarkType[] Results = {};
        private bool Player1Turn;
        private bool GameEnded;

        public MainWindow()
        {
            InitializeComponent();
            NewGame();
        }

        private void NewGame()
        {
            Results = new MarkType[9];
            Player1Turn = true;

            for (var i = 0; i < Results.Length; i++)
            {
                Results[i] = MarkType.Free;
            }

            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = String.Empty;
                button.Background = new SolidColorBrush(Color.FromRgb(33, 34, 43));
                button.Foreground = Brushes.Thistle;
            });

            GameEnded = false;
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

            if (Results[index] != MarkType.Free)
            {
                return;
            }

            Results[index] = Player1Turn ? MarkType.Cross : MarkType.Nought;
            button.Content = Player1Turn ? "X" : "O";
            Player1Turn ^= true;

            if (Player1Turn)
            {
                button.Foreground = Brushes.Azure;
            }

            CheckForWinner();
        }

        private void CheckForWinner()
        {
            if (Results[0] != MarkType.Free && (Results[0] & Results[1] & Results[2]) == Results[0])
            {
                GameEnded = true;
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }

            else if (Results[3] != MarkType.Free && (Results[3] & Results[4] & Results[5]) == Results[3])
            {
                GameEnded = true;
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }

            else if (Results[6] != MarkType.Free && (Results[6] & Results[7] & Results[8]) == Results[6])
            {
                GameEnded = true;
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }

            else if (Results[0] != MarkType.Free && (Results[0] & Results[3] & Results[6]) == Results[0])
            {
                GameEnded = true;
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }

            else if (Results[1] != MarkType.Free && (Results[1] & Results[4] & Results[7]) == Results[1])
            {
                GameEnded = true;
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }

            else if (Results[2] != MarkType.Free && (Results[2] & Results[5] & Results[8]) == Results[2])
            {
                GameEnded = true;
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }

            if (Results[0] != MarkType.Free && (Results[0] & Results[4] & Results[8]) == Results[0])
            {
                GameEnded = true;
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }

            else if (Results[1] != MarkType.Free && (Results[2] & Results[4] & Results[6]) == Results[1])
            {
                GameEnded = true;
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }

            if (!Results.Any(result => result == MarkType.Free))
            {
                GameEnded = true;
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = Brushes.Orange;
                });
            }
        }
    }
}
