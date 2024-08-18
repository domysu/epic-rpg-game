using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Engine.ViewModels;
using Engine.EventArgs;

namespace myrpggame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameSession _gameSession;
        public MainWindow()
        {
            
            InitializeComponent();
            _gameSession = new GameSession();
            _gameSession.GameInformation += OnGameMessageRaised;
            DataContext = _gameSession;

        }

        private void OnClick_MoveNorth(object sender, RoutedEventArgs e)
        {
            _gameSession.MoveNorth();
        }
        private void OnClick_MoveWest(object sender, RoutedEventArgs e)
        {
            _gameSession.MoveWest();
        }
        private void OnClick_MoveEast(object sender, RoutedEventArgs e)
        {
            _gameSession.MoveEast();
        }
        private void OnClick_MoveSouth(object sender, RoutedEventArgs e)
        {
            _gameSession.MoveSouth();
        }
        private void OnClick_WarpHome(object sender, RoutedEventArgs e)
        {
            _gameSession.WarpHome();
        }
        private void OnClick_AttackMonster(object sender, RoutedEventArgs e)
        {
            _gameSession.AttackCurrentMonster();
        }
        private void OnGameMessageRaised(object sender, GameInformationEventArgs e)
        {
            GameLogs.AppendText(e.Message + Environment.NewLine);
            GameLogs.ScrollToEnd();
        }

        private void OnGameBuyItemPressed(object sender, RoutedEventArgs e)
        {
            
            var button = sender as Button;

            if (button != null)
            {
                int itemId = (int)button.CommandParameter;
               
                _gameSession.BuyItem(itemId);

            }

        }
        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key) {
                case Key.W:
                    {
                        _gameSession.MoveNorth();
                        break;
                    }
                case Key.A:
                    {
                        _gameSession.MoveWest();
                        break;
                    }
                    case Key.S: {
                        _gameSession.MoveSouth();
                        break; 
                    }
                    case Key.D: { 
                    _gameSession.MoveEast();
                        break;
                    }
                case Key.Space:
                    {
                        break;
                    }
                default:
                    break;
            }
            
         }

        private void OnClick_UseConsumable(object sender, RoutedEventArgs e)
        {
            _gameSession.OnConsumableUsed();
        }
        private void OnClick_CraftItem(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                int itemId = (int)button.CommandParameter;
                _gameSession.CraftItem(itemId);
            }
        }
    }
}