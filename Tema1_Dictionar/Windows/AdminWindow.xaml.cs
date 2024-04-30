using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tema1_Dictionar.Classes;

namespace Tema1_Dictionar.Windows
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        Dictionar dictionar;

        public AdminWindow(Dictionar mainDictionar)
        {
            InitializeComponent();
            dictionar = mainDictionar;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(WordTextBox.Text) ||
                string.IsNullOrWhiteSpace(CategoryTextBox.Text) ||
                string.IsNullOrWhiteSpace(DescriptionTextBox.Text))
            {
                MessageBox.Show("Please fill the first three fields");
                return;
            }

            Word word = new Word(WordTextBox.Text, CategoryTextBox.Text, DescriptionTextBox.Text, ImagePathTextBox.Text);
            if (dictionar.HasWord(word.content))
            {
                dictionar.ModifyWord(word.content, word.description, word.imagePath);
            }
            else
            {
                dictionar.AddWord(word);

            }

            WordTextBox.Text = "";
            CategoryTextBox.Text = "";
            DescriptionTextBox.Text = "";
            ImagePathTextBox.Text = "";
        }

        private void DeleteWordButton_Click(Object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(WordTextBox.Text))
            {
                MessageBox.Show("Please fill the first field");
                return;
            }
            dictionar.DeleteWord(WordTextBox.Text);
            
            WordTextBox.Text = "";
            CategoryTextBox.Text = "";
            DescriptionTextBox.Text = "";
            ImagePathTextBox.Text = "";
        }
    }
}
