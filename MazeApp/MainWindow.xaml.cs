using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MazeApp
{
    public partial class MainWindow : Window
    {
        private readonly int[,] _maze =
        {
            { 1, 0, 1, 1, 1 },
            { 1, 0, 1, 0, 1 },
            { 1, 1, 1, 0, 1 },
            { 0, 0, 0, 0, 1 },
            { 1, 1, 1, 2, 1 }
        };

        public int PlayerX { get; private set; } = 0;
        public int PlayerY { get; private set; } = 0;
        public bool IsGameWon { get; private set; } = false;

        public MainWindow()
        {
            InitializeComponent();
            GenerateMaze();
            DrawPlayer();
        }

        private void GenerateMaze()
        {
            MazeGrid.RowDefinitions.Clear();
            MazeGrid.ColumnDefinitions.Clear();
            MazeGrid.Children.Clear();

            for (int i = 0; i < _maze.GetLength(0); i++)
                MazeGrid.RowDefinitions.Add(new RowDefinition());
            for (int j = 0; j < _maze.GetLength(1); j++)
                MazeGrid.ColumnDefinitions.Add(new ColumnDefinition());

            for (int i = 0; i < _maze.GetLength(0); i++)
            {
                for (int j = 0; j < _maze.GetLength(1); j++)
                {
                    Rectangle rect = new Rectangle { Width = 50, Height = 50 };
                    if (_maze[i, j] == 1)
                        rect.Fill = Brushes.White;
                    else if (_maze[i, j] == 0)
                        rect.Fill = Brushes.Black;
                    else if (_maze[i, j] == 2)
                        rect.Fill = Brushes.Green;

                    MazeGrid.Children.Add(rect);
                    Grid.SetRow(rect, i);
                    Grid.SetColumn(rect, j);
                }
            }
        }

        private void DrawPlayer()
        {
            foreach (UIElement element in MazeGrid.Children)
            {
                if (element is Rectangle rect)
                {
                    int row = Grid.GetRow(rect);
                    int col = Grid.GetColumn(rect);

                    if (row == PlayerX && col == PlayerY)
                        rect.Fill = Brushes.Blue;
                    else if (_maze[row, col] == 1)
                        rect.Fill = Brushes.White;
                    else if (_maze[row, col] == 2)
                        rect.Fill = Brushes.Green;
                    else
                        rect.Fill = Brushes.Black;
                }
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            int newX = PlayerX, newY = PlayerY;

            if (e.Key == Key.Up) newX--;
            else if (e.Key == Key.Down) newX++;
            else if (e.Key == Key.Left) newY--;
            else if (e.Key == Key.Right) newY++;

            TryMovePlayer(newX, newY);
        }

        public bool TryMovePlayer(int newX, int newY)
        {
            if (newX >= 0 && newX < _maze.GetLength(0) && newY >= 0 && newY < _maze.GetLength(1) && _maze[newX, newY] != 0)
            {
                PlayerX = newX;
                PlayerY = newY;
                DrawPlayer();

                if (_maze[PlayerX, PlayerY] == 2)
                {
                    IsGameWon = true;
                    StartExternalProgram(); 
                    ResetGame();
                }
                return true;
            }
            return false;
        }

        public void ResetGame()
        {
            PlayerX = 0;
            PlayerY = 0;
            IsGameWon = false;
            DrawPlayer();
        }

        private void StartExternalProgram()
        {
            try
            {
                string exePath = @"C:\Intel\Scream";
                Process.Start(exePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось запустить программу: {ex.Message}", "Ошибка запуска", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
