using System;
using System.Collections.Generic;

namespace SQLite
{
    public class Search
    {
        //connection to the sqlite database
        private System.Data.SQLite.SQLiteConnection db_connection;
        //word to search in the database
        private string searchWord;

        /// <summary>
        /// Create a new Instance of the calll search no values
        /// </summary>
        public Search()
        {
        }//search()


        /// <summary>
        /// Create new instance of seach with open databse connnction
        /// </summary>
        /// <param name="db_connection">Open connection to SQLite database</param>
        public Search(System.Data.SQLite.SQLiteConnection db_connection)
        {
            //check database connection is not null
            if (this.db_connection == null)
            {
                throw new ArgumentNullException("db_connection", "Database connection is null or not provided");
            }
            //set the connectin to the class
            db_connection = this.db_connection;
        }//search(1)


        /// <summary>
        /// Create new instance of the Search Using path to sqlite database file
        /// </summary>
        /// <param name="pathToDatabase">Path to database file</param>
        public Search(string pathToDatabase)
        {
            //path is not provided
            if (string.IsNullOrWhiteSpace(pathToDatabase))
            {
                throw new ArgumentNullException("pathToDatabase", "Path to the database not provided");
            }
            //todo check if the file exists

            //open database connection to file
            db_connection = new System.Data.SQLite.SQLiteConnection("Data Source=" + @pathToDatabase.Trim() + ";Version=3;");
            db_connection.Open();
        }//search(path)


        /// <summary>
        /// Method to set the search pattern to search in the database
        /// </summary>
        /// <param name="word">The word to search</param>
        public void setSearchTerm(string word)
        {
            //check for null or empty input
            checkInputString(word);
            //input owk set to member variable
            searchWord = word;
        }//setSearchTerm


        /// <summary>
        /// Set the SQLite database connection to member variable
        /// </summary>
        /// <param name="sqliteConnection">Sqlite database connecction</param>
        private void setDbConnection(System.Data.SQLite.SQLiteConnection sqliteConnection)
        {
            //if datbase is  null comaplain
            if (sqliteConnection == null) { throw new ArgumentNullException("sqliteConnection", "The db connection is null"); }
            //set the database to member variable
            db_connection = sqliteConnection;
        }//set database connection

        public string[] getMatches(string searchPattern)
        {
            //no databse connection opened complain
            if (db_connection == null) { throw new ArgumentNullException("db_connection", "Connection to the databse file not provided or is null"); }
            //the input to search is null, complain
            //todo check for strings only no numbers
            if (string.IsNullOrWhiteSpace(searchPattern)) { throw new ArgumentException("String Input to search is empty or null", "searchPattern"); }
            //run the method to seatch for matches
            return searchMatching(searchPattern).ToArray();
        }//get matches


        /// <summary>
        /// Validates the input to check for empty strings and illegaeal characters(numbers and special characterrs)
        /// </summary>
        /// <param name="inputString">The word to search for illeagel characters</param>
        private void checkInputString(string inputString)
        {
            //string is null or empty, throw exception
            if (string.IsNullOrEmpty(inputString))
            {
                throw new ArgumentException("The input is null or empty", "searchTerm");
            }
        }//check input string


        /// <summary>
        /// Method to search the database for words matching the pattern
        /// </summary>
        /// <param name="searchPattern">The SQL expression to match from the database</param>
        /// <returns>List of words such that item 1 = number of founds found, 0 no words found</returns>
        private List<string> searchMatching(string searchPattern)
        {
            //prepare sql satatement to search for in the database
            var sql = @"SELECT word FROM dict WHERE word LIKE '" + searchPattern.Trim() + "';";
            //array to hold the results
            var results = new List<String>();
            //run the sql command to the open databse connection, add results to the array
            using (var query = new System.Data.SQLite.SQLiteCommand(sql, db_connection))
            {
                using (var data = query.ExecuteReader())
                {
                    //has no rows. add -1 to result[0]
                    if (!data.HasRows) { results.Add((0).ToString()); return results; }
                    //has rows add to list
                    while (data.Read())
                    {
                        results.Add(data.GetString(0));
                    }//while

                }//using excecute
            }//using
            //return list ass array
            return results;
        }

    }//class search
}
