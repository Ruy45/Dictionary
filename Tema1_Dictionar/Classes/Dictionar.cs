using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Tema1_Dictionar.Classes
{
    public partial class Dictionar
    {
        public List<User> users { get; set; }
        public List<Category> categories { get; set; }

        public Dictionar()
        {
            users = new List<User>();
            categories = new List<Category>();
        }

        public Dictionar(string filePath)
        {
            users = new List<User>();
            categories = new List<Category>();

            LoadWordsFromFile(filePath);
        }

        public void LoadWordsFromFile(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);

            for (int i = 0; i < lines.Length; i += 4)
            {
                string wordContent = lines[i];
                string category = lines[i + 1];
                string description = lines[i + 2];
                string imagePath = lines[i + 3];

                if (!HasCategory(category))
                {
                    AddCategory(category);
                }

                AddWord(new Word(wordContent, category, description, imagePath));
            }
        }

        public void GetUsersFromFile()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string jsonFilePath = Path.Combine(basePath, "Resources", "UserData.json");

            string jsonText = File.ReadAllText(jsonFilePath);

            if (jsonText != null)
            {
                var usersList = JsonConvert.DeserializeObject<List<User>>(jsonText);

                if (usersList != null)
                {
                    users = new List<User>(usersList);
                }
            }
        }


        public bool HasUserInUsers(string userName, string userPassword)
        {
            return users.Any(user => user.name == userName && user.password == userPassword);
        }

        public void AddCategory(string categoryName)
        {
            categories.Add(new Category(categoryName));
        }

        public void AddWord(Word word)
        {
            Category targetCategory = categories.Find(category => category.name == word.category);

            if (HasCategory(targetCategory))
            {
                targetCategory.words.Add(word);
                Trace.WriteLine("are categoria");
            }
            else
            {
                Trace.WriteLine("nu are categoria");
                AddCategory(word.category);
                targetCategory = categories.Find(category => category.name == word.category);

                if (targetCategory != null)
                {
                    //Trace.WriteLine($"Wordcategory: {word.category} ");
                    //Trace.WriteLine($"WordDescription: {word.description} ");
                    targetCategory.words.Add(word);
                }
            }
        }

        public void ModifyWord(string wordName, string description, string imagePath)
        {
            foreach (Category category in categories)
            {
                Word targetWord = category.words.Find(word => word.content == wordName);

                if (targetWord != null)
                {
                    targetWord.description = description;
                    targetWord.imagePath = imagePath;
                    break;
                }
            }
        }

        public void DeleteWord(string wordName)
        {
            foreach (Category category in categories)
            {
                Word targetWord = category.words.Find(word => word.content == wordName);

                if (targetWord != null)
                {
                    category.words.Remove(targetWord);

                    if (category.words.Count == 0)
                    {
                        categories.Remove(category);
                    }

                    break;
                }
            }
        }


        public bool HasCategory(string categoryName)
        {
            return categories.Any(category => category.name == categoryName);
        }

        public bool HasCategory(Category category1)
        {
            return categories.Any(category => category == category1);
        }

        public bool HasWord(string wordName)
        {
            return categories.Any(category => category.words.Any(word => word.content == wordName));
        }
    }
}
