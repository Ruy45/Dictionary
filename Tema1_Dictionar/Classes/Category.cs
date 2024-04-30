using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema1_Dictionar.Classes
{
    public partial class Category
    {
        public string name { get; set; }
        public List<Word> words { get; set; }

        public Category()
        {
            name = string.Empty;
            words = new List<Word>();
        }

        public Category(string name)
        {
            this.name = name;
            words = new List<Word>();
        }

        public void AddWord(string word, string description, string category, string imagePath)
        {
            words.Add(new Word(word, description, category, imagePath));
        }
    }

}
