using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Tema1_Dictionar.Classes;

namespace Tema1_Dictionar.Windows
{
    public partial class SearchWindow : Window
    {
        private Dictionar dictionar;
        private List<string> suggestions;

        public SearchWindow(Dictionar dictionar)
        {
            InitializeComponent();
            this.dictionar = dictionar;
            suggestions = dictionar.categories
                .SelectMany(cat => cat.words)
                .Select(word => word.content)
                .ToList();

            UpdateSuggestions("");
        }

        private void CmbSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbSearch.SelectedItem is string selectedWord)
            {
                UpdateWord(selectedWord);
                UpdateSuggestions("");
            }
        }

        private void CmbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string selectedWord = cmbSearch.Text;
            string trimmedSearchText = selectedWord.TrimEnd(' ');

            if (!string.IsNullOrEmpty(trimmedSearchText))
            {
                UpdateWord(trimmedSearchText);
                UpdateSuggestions("");
            }
            else
            {
                imageControl.Source = null;
                DataContext = null;
                UpdateSuggestions("");
            }
        }

        private void UpdateWord(string selectedWord)
        {
            Word word = dictionar.categories
                .SelectMany(cat => cat.words)
                .FirstOrDefault(w => w.content == selectedWord);

            if (word != null)
            {
                DataContext = word;
                imageControl.Source = word.LoadImage(word.imagePath);
            }
            else
            {
                imageControl.Source = null;
                DataContext = null;
                return;
            }
        }


        private void UpdateSuggestions(string searchText)
        {
            string trimmedSearchText = searchText.TrimEnd(' ');
            cmbSearch.ItemsSource = suggestions
                .Where(word => word.StartsWith(trimmedSearchText, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
