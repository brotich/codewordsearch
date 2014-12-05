﻿using System;
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

        private void Search_Click(object sender, EventArgs e)
        {
            var test = textBox1.Text;       //temp code
            textBox2.Text = null;//temp code
            
        }//search click

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
                    for( int i = 0; i < inputString.Length; i++)
                    {
                        char sameCh = inputString[i];
                        //if matching add to array
                        if (sameCh == ch)
                        {
                            loc.Add(i);
                        }
                    }//for each
                    //add to dictinary
                    dictionary.Add(ch, loc.ToArray() );
                }//if
            }//foreach
            return dictionary;
        }//create pattern

        /// <summary>
        /// method to remove dashes from text
        /// </summary>
        /// <param name="input">The String Containing dashes</param>
        /// <returns>String with dashes removed</returns>
        private string removeDashes(string input)
        {
            //check input string for null
            isStringNull(input);
            return System.Text.RegularExpressions.Regex.Replace(input, @"-", "");
        }//removeDashes


        private void isStringNull(string input)
        {
            //the input string is empty or null complain
            if (string.IsNullOrWhiteSpace(input)) { throw new NullReferenceException("Input string is null or empty"); }
        }//isstringnull
    }//class form
}//namespace
