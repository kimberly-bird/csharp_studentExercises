using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;
using System.Collections;
using Dapper;
using StudentExercises.Models;

namespace StudentExercises.Data
{
    public class DatabaseInterface
    {
        // readonly property that returns a connection to our SQLite database so we can then write and read through the connection
        public static SqliteConnection Connection
        {
            get
            {
                // block scope variable
                string connectionString = $"Data Source=./StudentExercises.db";
                // this is one of the 3rd party dependency package that we installed through nuget that is in the csproj file
                return new SqliteConnection(connectionString);
            }
        }


        // method
        public static void CheckCohortTable()
        {
            // will return a new sqlite connection that is stored in db
            SqliteConnection db = DatabaseInterface.Connection;

            try
            {
                /*
                    new variable that is of type List of Department.
                    Query is Dapper method to be able to query the database
                    query also has to be typed of what you expect to get back 
                    what should be created from that data you're querying? 
                    In this case, department instances from the data that comes back
                    It will create object instances based on the cs models we have created - so for example, the db has 3 departments, so we get back 3 instances of departments with the Id and DeptName back in each instance with the data from the db
                 */ 
                List<Cohort> cohorts = db.Query<Cohort>
                // select = what to do on the database side 
                    ("SELECT Id FROM Cohort").ToList();
            }
            // if the database table doesn't exist, check the exception and if there is no table, create the table
            catch (System.Exception ex)
            {
                if (ex.Message.Contains("no such table"))
                {
                    // execute statement to make a new table into database
                    db.Execute(@"CREATE TABLE `Cohort` (
                        `Id` INTEGER PRIMARY KEY AUTOINCREMENT,
                        `Name` TEXT NOT NULL
                    )");

                    db.Execute(@"
                    INSERT INTO Cohort (Name) VALUES ('Day 26');
                    INSERT INTO Cohort (Name) VALUES ('Day 27');
                    INSERT INTO Cohort (Name) VALUES ('Day 28');
                    ");
                }
            }
        }

        public static void CheckExerciseTable()
        {
            // will return a new sqlite connection that is stored in db
            SqliteConnection db = DatabaseInterface.Connection;

            try
            {
                List<Exercise> exercises = db.Query<Exercise>
                // select = what to do on the database side 
                    ("SELECT Id FROM Exercise").ToList();
            }
            // if the database table doesn't exist, check the exception and if there is no table, create the table
            catch (System.Exception ex)
            {
                if (ex.Message.Contains("no such table"))
                {
                    // execute statement to make a new table into database
                    db.Execute($@"CREATE TABLE `Exercise` (
                        `Id` INTEGER PRIMARY KEY AUTOINCREMENT,
                        `Name` TEXT NOT NULL,
                        `Language` TEXT NOT NULL
                    )");

                    db.Execute($@"
                    INSERT INTO Exercise (Name, Language) VALUES ('chicken monkey', 'javascript');
                    INSERT INTO Exercise (Name, Language) VALUES ('coins to cash', 'javascript');
                    INSERT INTO Exercise (Name, Language) VALUES ('lists', 'C#');
                    INSERT INTO Exercise (Name, Language) VALUES ('hashsets', 'C#');
                    ");
                }
            }
        }
    }
}