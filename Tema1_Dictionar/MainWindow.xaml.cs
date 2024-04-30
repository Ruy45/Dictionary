using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Tema1_Dictionar.Classes;
using Tema1_Dictionar.Windows;

namespace Tema1_Dictionar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionar dictionar;

        public MainWindow()
        {
            InitializeComponent();
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            Trace.WriteLine($"basePath: {basePath}\n");
            string wordsFilePath = Path.Combine(basePath, "Resources", "WordData.txt");
            dictionar = new Dictionar(wordsFilePath);
            dictionar.GetUsersFromFile();
        }

        private void AdministratorModeButton_Click(object sender, RoutedEventArgs e)
        {
            var authentificationWindow = new AuthentificationWindow();
            if (authentificationWindow.ShowDialog() == true)
            {
                string username = authentificationWindow.Username;
                string password = authentificationWindow.Password;

                if (dictionar.HasUserInUsers(username, password))
                {
                    AdminWindow adminWindow = new AdminWindow(dictionar);
                    adminWindow.Show();
                    this.Hide();
                    adminWindow.Closed += AdminWindow_Closed;
                }
                else
                {
                    MessageBox.Show("Invalid username or password. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void AdminWindow_Closed(object sender, EventArgs e)
        {
            this.Show();
        }

        private void SearchWordButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SearchWindow searchWindow = new SearchWindow(dictionar);
                searchWindow.Show();
                this.Hide();
                searchWindow.Closed += SearchWindow_Closed;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SearchWindow_Closed(object? sender, EventArgs e)
        {
            this.Show();
        }

        private void GameModeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GameWindow gameWindow = new GameWindow(dictionar);
                gameWindow.Show();
                this.Hide();
                gameWindow.Closed += GameWindow_Closed;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GameWindow_Closed(object? sender, EventArgs e)
        {
            this.Show();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}