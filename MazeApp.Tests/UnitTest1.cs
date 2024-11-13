using MazeApp;
using Xunit;

namespace MazeApp.Tests
{
    public class MazeAppTests
    {
        private MainWindow CreateTestMainWindow()
        {
            var mainWindow = new MainWindow();
            mainWindow.InitializeComponent();
            return mainWindow;
        }

        [Fact]
        public void Test_InitialPlayerPosition_IsCorrect()
        {
            var mainWindow = CreateTestMainWindow();
            Assert.Equal(0, mainWindow.PlayerX);
            Assert.Equal(0, mainWindow.PlayerY);
        }

        [Fact]
        public void Test_PlayerCannotMoveThroughWalls()
        {
            var mainWindow = CreateTestMainWindow();
            bool canMove = mainWindow.TryMovePlayer(-1, 0);
            Assert.False(canMove);
        }

        [Fact]
        public void Test_PlayerCanMoveToOpenPath()
        {
            var mainWindow = CreateTestMainWindow();
            bool canMove = mainWindow.TryMovePlayer(0, 1);
            Assert.True(canMove);
        }

        [Fact]
        public void Test_PlayerWinsWhenReachingExit()
        {
            var mainWindow = CreateTestMainWindow();
            mainWindow.TryMovePlayer(3, 3);
            mainWindow.TryMovePlayer(4, 3);
            Assert.True(mainWindow.IsGameWon);
        }

        [Fact]
        public void Test_GameResetsAfterWin()
        {
            var mainWindow = CreateTestMainWindow();
            mainWindow.TryMovePlayer(3, 3);
            mainWindow.TryMovePlayer(4, 3);
            mainWindow.ResetGame();
            Assert.Equal(0, mainWindow.PlayerX);
            Assert.Equal(0, mainWindow.PlayerY);
        }
    }
}


