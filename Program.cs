using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentExercises {
    class Program {
        static void Main (string[] args) {
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
            List<Student> students = new List<Student> ()
            {
                Rachel,
                Monica,
                Ross,
                Chandler,
                Joey,
                Phoebe,
                Phoebe2
            };

            // Create a list of exercises. Add all of the exercise instances to it.
            List<Exercise> exercises = new List<Exercise> () 
            {
                loops,
                objects,
                dictionaries,
                lists
            };

            // list instructors
            List<Instructor> instructors = new List<Instructor>()
            {
                Joe,
                Jisie,
                Jordan,
                Steve
            };

            // list of cohorts
            List<Cohort> cohorts = new List<Cohort>() {
                twentyFive,
                twentySix,
                twentySeven
            };

            // List exercises for the JavaScript language by using the Where() LINQ method.
            var JSEx = exercises.Where(ex => ex.Language == "Javascript");
            foreach (var ex in JSEx)
            {
                Console.WriteLine($"Javascript exercises: {ex.Name}");
            }

            // List students in a particular cohort by using the Where() LINQ method.
            var studentsIn27 = students.Where(stu => stu.Cohort == twentySeven);
            foreach (var stu in studentsIn27)
            {
                Console.WriteLine($"Students in Cohort 27: {stu.FirstName} {stu.LastName}");
            }

            // List instructors in a particular cohort by using the Where() LINQ method.
            var instructorsIn26 = instructors.Where(ins => ins.Cohort == twentySix);
            foreach (var i in instructorsIn26)
            {
                Console.WriteLine($"Instructors in Cohort 26: {i.FirstName} {i.LastName}");
            }

            // Sort the students by their last name.
            var sortedStudents = students.OrderBy(stu => stu.LastName);
            foreach (var stu in sortedStudents)
            {
                Console.WriteLine($"Sorted students by last name: {stu.LastName}, {stu.FirstName}");
            }

            // Display any students that aren't working on any exercises 
            var studentsWithNoExercises = students.Where(stu => stu.Exercises.Count() == 0);
            foreach (var stu in studentsWithNoExercises)
            {
                Console.WriteLine($"Students who aren't working on exercises: {stu.FirstName} {stu.LastName}");
            }

            // Which student is working on the most exercises?
            var studentWithMostExercises = (from s in students
                // select is like .map and generates a new thing and put it into the final collection
                select new {
                    FirstName = s.FirstName,
                    Exercises = s.Exercises.Count()
                })
                // put in order of descending number of exercises
                .OrderByDescending(s => s.Exercises)
                // grab just the first one -> first or default if the list is empty
                .FirstOrDefault();
                Console.WriteLine($"Student working on most exercises: {studentWithMostExercises.FirstName} {studentWithMostExercises.Exercises}");


            // How many students in each cohort?
            // GroupBy gives you a collection of groups - each group has something that it's being grouped by (the key). The group itself is the list of all of the values of the group. Returns a collection of groups.
            // collection of groups (numberOfStudentsInEachCohort)
            var numberOfStudentsInEachCohort = students.GroupBy(c => c.Cohort.Name);
            foreach (var studentGroup in numberOfStudentsInEachCohort)
            {
                // key is the thing you grouped by
                Console.WriteLine($"{studentGroup.Key} has {studentGroup.Count()} students");
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

        }
    }
}