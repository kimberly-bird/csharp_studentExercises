using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Data.Sqlite;
using StudentExercises.Data;
using StudentExercises.Models;

namespace StudentExercises {
    class Program {
        static void Main (string[] args) {
            // --------------- DAPPER ---------------
            SqliteConnection db = DatabaseInterface.Connection;
            DatabaseInterface.CheckExerciseTable ();
            DatabaseInterface.CheckCohortTable ();
            DatabaseInterface.CheckStudentTable ();
            DatabaseInterface.CheckInstructorTable ();
            DatabaseInterface.CheckStudentExerciseTable ();

            // 3. Query the database for all the Exercises.
            // whenever dapper does a query, it'll do the query, and get a row back and then make a new exercise object. so then new exercise - then it'll make an assumption that it'll have a parameterless constructor because it must be generic.
            db.Query<Exercise> (@"SELECT * FROM Exercise")
                .ToList ()
                .ForEach (ex => Console.WriteLine ($"{ex.Name}: {ex.Language}"));

            // 4. Fnd all the exercises in the database where the language is JavaScript.
            db.Query<Exercise> (@"SELECT * FROM Exercise
                WHERE Exercise.Language == 'Javascript'")
                .ToList()
                .ForEach (ex => Console.WriteLine($"{ex.Name} are Javascript exercises"));

            // 5. Insert a new exercise into the database.
            // db.Execute(@"
            //     INSERT INTO Exercise (Name, Language) VALUES ('Dapper', 'C#')
            // ");



            // --------------- LINQ ---------------
            // Create 4, or more, exercises.
            Exercise loops = new Exercise ("loops", "Javascript");
            Exercise objects = new Exercise ("objects", "Javascript");
            Exercise dictionaries = new Exercise ("dictionaries", "C#");
            Exercise lists = new Exercise ("lists", "C#");

            // Create 3, or more, cohorts.
            Cohort twentyFive = new Cohort ("Day 25");
            Cohort twentySix = new Cohort ("Day 26");
            Cohort twentySeven = new Cohort ("Day 27");

            // Create 4, or more, students and assign them to one of the cohorts.
            Student Rachel = new Student ("Rachel", "Greene", "haha", twentyFive);
            Student Monica = new Student ("Monica", "Geller", "keep it clean", twentyFive);
            Student Ross = new Student ("Ross", "Geller", "we were on a break", twentySix);
            Student Chandler = new Student ("Chandler", "Bing", "could I be more ...", twentySix);
            Student Joey = new Student ("Joey", "Tribiani", "sandwiches", twentySeven);
            Student Phoebe = new Student ("Phoebe", "Bouffay", "nestle tollhouse", twentySeven);
            Student Phoebe2 = new Student ("Phoebe2", "Bouffay2", "nestle tollhouse2", twentySeven);

            // Create 3, or more, instructors and assign them to one of the cohorts.
            Instructor Joe = new Instructor ("Joe", "Shepherd", "joes", twentyFive);
            Instructor Jisie = new Instructor ("Jisie", "David", "jisie", twentySix);
            Instructor Jordan = new Instructor ("Jordan", "C", "jordan", twentySix);
            Instructor Steve = new Instructor ("Steve", "Brownlee", "coach", twentySeven);

            // Have each instructor assign 2 exercises to each of the students.
            Joe.AssignExercise (loops, Rachel);
            Joe.AssignExercise (objects, Rachel);
            Joe.AssignExercise (loops, Monica);
            Joe.AssignExercise (objects, Monica);
            Joe.AssignExercise (dictionaries, Monica);
            Joe.AssignExercise (lists, Monica);
            Jisie.AssignExercise (dictionaries, Ross);
            Jisie.AssignExercise (lists, Ross);
            Jisie.AssignExercise (loops, Ross);
            Jordan.AssignExercise (lists, Chandler);
            Jordan.AssignExercise (dictionaries, Chandler);
            Steve.AssignExercise (lists, Joey);
            Steve.AssignExercise (dictionaries, Joey);

            // Create a list of students. Add all of the student instances to it.
            List<Student> students = new List<Student> () {
                Rachel,
                Monica,
                Ross,
                Chandler,
                Joey,
                Phoebe,
                Phoebe2
            };

            // Create a list of exercises. Add all of the exercise instances to it.
            List<Exercise> exercises = new List<Exercise> () {
                loops,
                objects,
                dictionaries,
                lists
            };

            // list instructors
            List<Instructor> instructors = new List<Instructor> () {
                Joe,
                Jisie,
                Jordan,
                Steve
            };

            // list of cohorts
            List<Cohort> cohorts = new List<Cohort> () {
                twentyFive,
                twentySix,
                twentySeven
            };

            // 1. List exercises for the JavaScript language by using the Where() LINQ method.
            IEnumerable<Exercise> JSEx = exercises.Where (ex => ex.Language == "Javascript");
            foreach (var ex in JSEx) {
                // Console.WriteLine($"Javascript exercises: {ex.Name}");
            }

            // 2. List students in a particular cohort by using the Where() LINQ method.
            IEnumerable<Student> studentsIn27 = students.Where (stu => stu.Cohort == twentySeven);
            foreach (var stu in studentsIn27) {
                // Console.WriteLine($"Students in Cohort 27: {stu.FirstName} {stu.LastName}");
            }

            // 3. List instructors in a particular cohort by using the Where() LINQ method.
            IEnumerable<Instructor> instructorsIn26 = instructors.Where (ins => ins.Cohort == twentySix);
            foreach (var i in instructorsIn26) {
                // Console.WriteLine($"Instructors in Cohort 26: {i.FirstName} {i.LastName}");
            }

            // 4. Sort the students by their last name.
            IEnumerable<Student> sortedStudents = students.OrderBy (stu => stu.LastName);
            foreach (var stu in sortedStudents) {
                // Console.WriteLine($"Sorted students by last name: {stu.LastName}, {stu.FirstName}");
            }

            // 5. Display any students that aren't working on any exercises 
            List<Student> studentsWithNoExercises = students.Where (stu => stu.Exercises.Count == 0).ToList ();
            foreach (var stu in studentsWithNoExercises) {
                // Console.WriteLine($"Students who aren't working on exercises: {stu.FirstName} {stu.LastName}");
            }

            // 6. Which student is working on the most exercises?
            var studentWithMostExercises = (from s in students
                    // select is like .map and generates a new thing and put it into the final collection
                    select new {
                        FirstName = s.FirstName,
                            Exercises = s.Exercises.Count ()
                    })
                // put in order of descending number of exercises
                .OrderByDescending (s => s.Exercises)
                // grab just the first one -> first or default if the list is empty
                .Take (1).ToList () [0];
            // Console.WriteLine($"Student working on most exercises: {studentWithMostExercises.FirstName} {studentWithMostExercises.Exercises}");

            // 7. How many students in each cohort?
            // GroupBy gives you a collection of groups - each group has something that it's being grouped by (the key). The group itself is the list of all of the values of the group. Returns a collection of groups.
            // collection of groups (numberOfStudentsInEachCohort)
            // METHOD WAY
            var numberOfStudentsInEachCohort = students.GroupBy (c => c.Cohort.Name);
            // looks at every group of students
            foreach (var studentGroup in numberOfStudentsInEachCohort) {
                // key is the thing you grouped by
                // Console.WriteLine($"{studentGroup.Key} has {studentGroup.Count()} students");
            }

            // SQL/QUERY WAY
            var totalStudents = from student in students
            group student by student.Cohort into sorted
            select new {
                Cohort = sorted.Key,
                Students = sorted.ToList ()
            };
            foreach (var total in totalStudents) {
                // Console.WriteLine($"Cohort {total.Cohort.Name} has {total.Students.Count()} students");
            }

            // Generate a report that displays which students are working on which exercises.
            foreach (Exercise ex in exercises) {
                List<string> assignedStudents = new List<string> ();

                foreach (Student stu in students) {
                    if (stu.Exercises.Contains (ex)) {
                        assignedStudents.Add (stu.FirstName);
                    }
                }
                // Console.WriteLine ($"{ex.Name} is being worked on by {String.Join(", ", assignedStudents)}");
            }

            // Query the database for all the Exercises.

        }
    }
}