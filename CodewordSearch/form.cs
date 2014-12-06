using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CodewordSearch
{
    public partial class form : Form
    {
        //path to sqlite databse file
        string path = @"C:\Users\brian\Documents\Visual Studio 2010\Projects\CodewordSearchGUI\CodewordSearchGUI\test.sqlite";
        //connection to the database
        private SQLite.Search connection;


        public form()
        {
            InitializeComponent();
            initialize();
        }

        private void initialize()
        {
            connection = new SQLite.Search(path);
        }//initilize


        //button clicked
        private void Search_Click(object sender, EventArgs e)
        {
            
        }//search click

        /// <summary>
        /// Matches a string to a pattern 
        /// </summary>
        /// <param name="word">The word to Match to pattern</param>
        /// <param name="pattern">Dictionary containing the location of characters</param>
        /// <returns>True if the word matches</returns>
        private bool isMatching(string word, Dictionary<char, int[]> pattern)
        {
            //check if the string is null
            isStringNull(word);
            //check dictonary is null
            if (pattern == null) { throw new ArgumentNullException("pattern", "The pattern is null"); }
            //iterate over the dictionary
            foreach (var item in pattern)
            {
                //check if the key is digit
                if (char.IsDigit(item.Key))
                {
                    //location of similar characters
                    var loc = item.Value;
                    //if has more than one location
                    if (loc.Length > 1)
                    {
                        //compare index 1 to other location
                        foreach (int index in loc)
                        {
                            //return false if not the same
                            if (word[loc[0]] != word[index]) return false;
                        }//for each
                    }//if
                }//if
            }//foreach
            //if reach here the word matches
            return true;
        }//isMatching

        /// <summary>
        /// Generates a dictionary of characters and location in the input string
        /// </summary>
        /// <param name="inputString">The ssource string</param>
        /// <returns>Dictionary(char, int[]) of the charcters in the array</returns>
        private Dictionary<char, int[]> createPattern(string inputString)
        {
            //check for null string input
            isStringNull(inputString);
            //create dictionary to hold the char
            Dictionary<char, int[]> dictionary = new Dictionary<char, int[]>();
            //iterate over the pattern to find char
            foreach (char ch in inputString.ToCharArray())
            {
                //add char and location if not
                if (!dictionary.ContainsKey(ch))
                {
                    //list hold the array
                    List<int> loc = new List<int>();
                    //find matching charcters in the array
                    for (int i = 0; i < inputString.Length; i++)
                    {
                        char sameCh = inputString[i];
                        //if matching add to array
                        if (sameCh == ch)
                        {
                            loc.Add(i);
                        }
                    }//for each
                    //add to dictinary
                    dictionary.Add(ch, loc.ToArray());
                }//if
            }//foreach
            return dictionary;
        }//create pattern


        /// <summary>
        /// Returns array of sting[] of input, separates using '-'
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string[] createArrayOfInput(string input)
        {
            //check if the string is null
            isStringNull(input);
            //split input using - to array
            return input.Split('-');
        }//createArray

        /// <summary>
        /// Method to replace from the string with _
        /// </summary>
        /// <param name="input">Input containing numbers</param>
        /// <returns>String with numbers replaced with underscore</returns>
        private string replaceNumberWithUnderScore(string input)
        {
            //check for null string
            isStringNull(input);
            //replace string with underscore
            return System.Text.RegularExpressions.Regex.Replace(input, @"\d", "_");
        }//replaceNumbers

        /// <summary>
        /// Checks for the null or empty string
        /// </summary>
        /// <param name="input">String to check for empty or null</param>
        private void isStringNull(string input)
        {
            //the input string is empty or null complain
            if (string.IsNullOrWhiteSpace(input)) { throw new NullReferenceException("Input string is null or empty"); }
        }//isstringnull
    }//class form
}//namespace
