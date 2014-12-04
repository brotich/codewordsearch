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

        private void Search_Click(object sender, EventArgs e)
        {
            var test = textBox1.Text;       //temp code
            textBox2.Text = null;//temp code
            
        }//search click


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
