using System;
using System.Collections.Generic;

namespace StudentExercises
{
    public class Instructor
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SlackHandle { get; set; }
        public Cohort Cohort { get; set; }

        // constructor
        public Instructor (string firstName, string lastName, string slackHandle, Cohort cohort)
        {
            FirstName = firstName;
            LastName = lastName;
            SlackHandle = slackHandle;
            Cohort = cohort;
        }

        // Assign an exercise to a student
        public void AssignExercise (Exercise exercise, Student student) {
            student.Exercises.Add(exercise);
        }

    }
}
