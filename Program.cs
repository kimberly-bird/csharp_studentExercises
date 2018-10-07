using System;
using System.Collections.Generic;

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

            // Create 3, or more, instructors and assign them to one of the cohorts.
            Instructor Joe = new Instructor ("Joe", "Shepherd", "joes", twentyFive);
            Instructor Jisie = new Instructor ("Jisie", "David", "jisie", twentySix);
            Instructor Steve = new Instructor ("Steve", "Brownlee", "coach", twentySeven);

            // Have each instructor assign 2 exercises to each of the students.
            Joe.AssignExercise (loops, Rachel);
            Joe.AssignExercise (objects, Rachel);
            Joe.AssignExercise (loops, Monica);
            Joe.AssignExercise (objects, Monica);
            Jisie.AssignExercise (dictionaries, Ross);
            Jisie.AssignExercise (lists, Ross);
            Jisie.AssignExercise (lists, Chandler);
            Jisie.AssignExercise (dictionaries, Chandler);
            Steve.AssignExercise (lists, Joey);
            Steve.AssignExercise (dictionaries, Joey);
            Steve.AssignExercise (lists, Phoebe);
            Steve.AssignExercise (dictionaries, Phoebe);

            // Create a list of students. Add all of the student instances to it.
            List<Student> students = new List<Student> ();
            students.Add (Rachel);
            students.Add (Monica);
            students.Add (Ross);
            students.Add (Chandler);
            students.Add (Joey);
            students.Add (Phoebe);

            // Create a list of exercises. Add all of the exercise instances to it.
            List<Exercise> exercises = new List<Exercise> ();
            exercises.Add (loops);
            exercises.Add (objects);
            exercises.Add (dictionaries);
            exercises.Add (lists);

            // Generate a report that displays which students are working on which exercises.
            foreach (Exercise ex in exercises) {
                List<string> assignedStudents = new List<string> ();

                foreach (Student stu in students) {
                    if (stu.Exercises.Contains (ex)) {
                        assignedStudents.Add (stu.FirstName);
                    }
                }
                Console.WriteLine ($"{ex.Name} is being worked on by {String.Join(", ", assignedStudents)}");
            }

        }
    }
}