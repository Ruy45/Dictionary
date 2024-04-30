using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema1_Dictionar.Classes
{
    public partial class Game
    {
        public List<Word> words;
        public int currentIndex = 0;
        public int correctAnswers = 0;
        public Random random = new Random();

        public Game(List<Word> words)
        {
            this.words = words.OrderBy(x => random.Next()).Take(5).ToList();
        }

        public Word GetCurrentWord()
        {
            if (currentIndex < words.Count)
                return words[currentIndex];
            else
                return null;
        }

        public void MoveToNextWord()
        {
            currentIndex++;
        }

        public bool CheckAnswer(string userGuess)
        {
            Word currentWord = GetCurrentWord();
            if (currentWord != null && userGuess.ToLower() == currentWord.content.ToLower())
            {
                correctAnswers++;
                return true;
            }
            return false;
        }

        public string GetGameResult()
        {
            return $"Game Over! You have guessed {correctAnswers} out of {words.Count} words.";
        }

        public void ResetGame()
        {
            currentIndex = 0;
            correctAnswers = 0;
        }
    }
}
