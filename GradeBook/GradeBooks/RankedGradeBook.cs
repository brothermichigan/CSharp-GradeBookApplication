using GradeBook.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GradeBook.GradeBooks
{
    class RankedGradeBook : BaseGradeBook
    {
        private List<double> averageGrades = new List<double>();

        public RankedGradeBook(string name) : base(name)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
            {
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work");
            }

            foreach (var student in Students)
            {
                var grade = student.AverageGrade;
                averageGrades.Add(grade);
            }
            averageGrades = averageGrades.OrderByDescending(d => d).ToList();

            var gradeBinSize = Students.Count / 5;
            var gradeBin = averageGrades.IndexOf(averageGrade);

            switch (gradeBin / gradeBinSize)
            {
                case 0:
                    return 'A';
                case 1:
                    return 'B';
                case 2:
                    return 'C';
                case 3:
                    return 'D';
            }

            return 'F';
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }

            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            int studentCount = 0;

            foreach (var student in Students)
            {
                var gradeCount = student.Grades.Count();
                if (gradeCount > 0) studentCount++;
            }

            if (studentCount < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }

            base.CalculateStudentStatistics(name);
        }
    }
}
