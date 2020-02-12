using GradeBook.Enums;
using System;
using System.Linq;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {

        public RankedGradeBook(string name) : base(name)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5) // Minimum of five students required
            {
                throw new InvalidOperationException("Ranked gradings require at least five students to allow the subdivision into 20% groupings");
            }

            var threshold = (int)Math.Ceiling(Students.Count * 0.2); // How many students form 20% threshold of the total number of students

            var grades = Students.OrderByDescending(s => s.AverageGrade) // Get a descending collection of average grades for our students
                                .Select(s => s.AverageGrade)
                                .ToList();

            if (grades[threshold - 1] <= averageGrade) // If our average grade is above or equal to that of students in the top 20% - we got an 'A'
                return 'A';
            if (grades[(threshold * 2) - 1] <= averageGrade) // Top 40% - 21% - a 'B'
                return 'B';
            if (grades[(threshold * 3) - 1] <= averageGrade)
                return 'C';
            if (grades[(threshold * 4) - 1] <= averageGrade)
                return 'D';
            return 'F'; // Failing no successful match we're in the bottom 20%. Note else and else if can be ommitted due to our shortcut returns
        }
    }
}
