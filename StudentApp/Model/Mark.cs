using System;
using LanguageExt;
using System.Data;

namespace StudentApp_FunctionalProgramming_.Model
{
    public class Mark : Record<Mark>
    {
        public readonly int StudentId;
        public readonly int SubjectId;
        public readonly int Score;
        public readonly int Year;

        private Mark(int studentId, int subjectId, int score, int year)
        {
            StudentId = studentId;
            SubjectId = subjectId;
            Score = score;
            Year = year;
        }

        public static Mark Create(int studentId, int subjectId, int score, int year)
        {
            return new Mark(studentId, subjectId, score, year);
        }

        public static Mark Create(IDataRecord record)
        {
            return Create(
              studentId: Convert.ToInt32(record["student_id"]),
              subjectId: Convert.ToInt32(record["subject_id"]),
              score: Convert.ToInt32(record["score"]),
              year: Convert.ToInt32(record["year"])
            );
        }
    }
}
