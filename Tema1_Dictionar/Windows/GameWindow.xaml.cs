using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Tema1_Dictionar.Classes;

namespace Tema1_Dictionar.Windows
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public Game game;

        public GameWindow(Dictionar dictionar)
        {
            InitializeComponent();
            List<Word> words = dictionar.categories.SelectMany(category => category.words).ToList();
            game = new Game(words);
            InitializeGame();
        }

        private void InitializeGame()
        {
            DisplayDescriptionOrImage();
            game.ResetGame();
            FinishButton.Visibility = Visibility.Collapsed;
            NextButton.IsEnabled = true;
            CheckAnswerButton.IsEnabled = true;
            GuessTextBox.Text = "";
        }

        private void DisplayDescriptionOrImage()
        {
            Word currentWord = game.GetCurrentWord();
            if (currentWord != null)
            {
                bool displayImage = game.random.Next(2) == 0;
                if (displayImage && currentWord.imagePath != "default.jpg")
                {
                    DescriptionTextBlock.Text = string.Empty;
                    GuessTextBox.Visibility = Visibility.Visible;
                    ImageControl.Visibility = Visibility.Visible;
                    ImageControl.Source = currentWord.LoadImage(currentWord.imagePath);
                }
                else
                {
                    DescriptionTextBlock.Text = currentWord.description;
                    GuessTextBox.Visibility = Visibility.Visible;
                    ImageControl.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                FinishButton.Visibility = Visibility.Visible;
                NextButton.IsEnabled = false;
                CheckAnswerButton.IsEnabled = false;
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            game.MoveToNextWord();
            DisplayDescriptionOrImage();
        }

        private void ResetGame()
        {
            InitializeGame();
            DisplayDescriptionOrImage();
        }

        private void CheckAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            string userGuess = GuessTextBox.Text;
            bool isCorrect = game.CheckAnswer(userGuess);

            if (isCorrect)
            {
                MessageBox.Show("Correct answer!", "Result", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Word currentWord = game.GetCurrentWord();
                MessageBox.Show($"Incorrect! The correct word is: {currentWord.content}", "Result", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            NextButton_Click(sender, e);
        }


        private void FinishButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show($"Game Over! You have guessed {game.correctAnswers} out of {game.words.Count} words.\nDo you want to start again?", "Game Over", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                ResetGame();
            }
            else
            {
                this.Close();
            }
        }
    }
}

