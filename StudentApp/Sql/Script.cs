using static LanguageExt.Prelude;

namespace StudentApp.Sql
{
    public static class Script
    {
        public static class Select
        {
            public static string validUser = @"
            SELECT * 
            FROM Users 
            WHERE username = @Username;
                  ".Trim();

            public static string sqlGetUsers = @"
            SELECT * 
            FROM Users;
                  ".Trim();

            public static readonly string sqlGetSubjectsMarksByStudent = @"
            SELECT Subjects.name, Marks.Score, Marks.student_id 
            FROM Subjects 
            INNER JOIN Marks 
            ON Subjects.id = Marks.subject_id 
            WHERE Marks.student_id = @StudentID;
                  ".Trim();

            public static readonly string sqlGetSubjectsMarks = @"
            SELECT Subjects.name, Marks.Score, Marks.student_id 
            FROM Subjects 
            INNER JOIN Marks 
            ON Subjects.id = Marks.subject_id;
                  ".Trim();

            public static readonly string sqlGetStudentsById = @"
            SELECT * 
            FROM Students 
            WHERE id = @StudentID;
                  ".Trim();

            public static readonly string sqlGetAverageMark = @"
            SELECT AVG(Marks.score) AS average 
            FROM Students 
            INNER JOIN Marks 
            ON Students.id = Marks.student_id 
            INNER JOIN Subjects 
            ON Marks.subject_id = Subjects.id 
            WHERE Students.id = @StudentID;
                  ".Trim();

            public static readonly string sqlGetStudentsMaxSubjects = @"
            SELECT p.id, p.name, p.surname, p.gender, p.dob, COUNT(o.student_id) AS Total 
            FROM Students p 
            LEFT JOIN Marks o 
            ON o.student_id = p.id 
            GROUP BY p.name, p.surname, p.id, p.gender, p.dob 
            HAVING COUNT(o.student_id) >= 5;
                  ".Trim();

            public static readonly string sqlGetStudentMaxSubjects = @"
            SELECT p.id, p.name, p.surname, p.gender, p.dob, COUNT(o.student_id) as Total 
            FROM Students p 
            LEFT JOIN Marks o 
            ON o.student_id = p.id 
            WHERE p.id = @StudentID 
            GROUP BY p.name, p.surname, p.id, p.gender, p.dob 
            HAVING COUNT(o.student_id) >= 5;
                  ".Trim();

            public static readonly string sqlGetAllStudents = @"
            SELECT * 
            FROM Students;
                ".Trim();

            public static readonly string sqlStudentTotalSubjects = @"
            SELECT COUNT(o.student_id) as Total 
            FROM Students p 
            LEFT JOIN Marks o 
            ON o.student_id = p.id 
            WHERE p.id = @StudentID;
                  ".Trim();
                    }

        public static class Create
        {
            public static string students = @"
            CREATE TABLE IF NOT EXISTS Students(
              id INTEGER PRIMARY KEY AUTOINCREMENT,
              name TEXT NOT NULL,
              surname TEXT NOT NULL,
              gender CHAR(1) NULL,
              dob INTEGER NULL
            );
                  ".Trim();

            public static string marks = @"
            CREATE TABLE IF NOT EXISTS Marks(
              student_id INTEGER NOT NULL,
              subject_id INTEGER NOT NULL,
              score INTEGER NOT NULL,
              year INTEGER NOT NULL
            );
                  ".Trim();

            public static string subjects = @"
            CREATE TABLE IF NOT EXISTS Subjects(
              id INTEGER PRIMARY KEY AUTOINCREMENT,
              name TEXT NOT NULL
            );
                  ".Trim();

            public static string users = @"
            CREATE TABLE IF NOT EXISTS Users(
              id INTEGER PRIMARY KEY AUTOINCREMENT,
              username TEXT NOT NULL,
              password TEXT NOT NULL
            );
                  ".Trim();
                    }

        public static class Insert
        {
            public static string students = @"
            INSERT INTO Students (name, surname, gender, dob) VALUES('Johnny','Walker','M', 1062540000);
            INSERT INTO Students (name, surname, gender, dob) VALUES('Lucas','Radebe','M', 1031004000);
            INSERT INTO Students (name, surname, gender, dob) VALUES('Charles','Glass','M', 1062540000);
            INSERT INTO Students (name, surname, gender, dob) VALUES('Sally','Williams','F', 1031004000);
            INSERT INTO Students (name, surname, gender, dob) VALUES('Mary','Smith','F',  1062540000);
            INSERT INTO Students (name, surname, gender, dob) VALUES('Jenny','Lopez','F', 1062540000);
            INSERT INTO Students (name, surname, gender, dob) VALUES('Daniel','Steele','M', 1031004000);
                 ".Trim();

            public static string subjects = @"
            INSERT INTO Subjects (name) VALUES('Maths');
            INSERT INTO Subjects (name) VALUES('Geometry');
            INSERT INTO Subjects (name) VALUES('Science');
            INSERT INTO Subjects (name) VALUES('English');
            INSERT INTO Subjects (name) VALUES('Life Orientation');
            INSERT INTO Subjects (name) VALUES('Woodwork');
            INSERT INTO Subjects (name) VALUES('Computer Science');
                  ".Trim();

            public static string marks = @"
            INSERT INTO Marks (student_id, subject_id, score, year) VALUES(1, 1, 50, 2018);
            INSERT INTO Marks (student_id, subject_id, score, year) VALUES(1, 2, 55, 2018);
            INSERT INTO Marks (student_id, subject_id, score, year) VALUES(1, 3, 61, 2018);
            INSERT INTO Marks (student_id, subject_id, score, year) VALUES(1, 4, 70, 2018);
            INSERT INTO Marks (student_id, subject_id, score, year) VALUES(1, 5, 35, 2018);
            INSERT INTO Marks (student_id, subject_id, score, year) VALUES(2, 1, 25, 2018);
            INSERT INTO Marks (student_id, subject_id, score, year) VALUES(2, 3, 45, 2018);
            INSERT INTO Marks (student_id, subject_id, score, year) VALUES(2, 4, 75, 2018);
            INSERT INTO Marks (student_id, subject_id, score, year) VALUES(2, 6, 76, 2018);
            INSERT INTO Marks (student_id, subject_id, score, year) VALUES(3, 2, 80, 2018);
            INSERT INTO Marks (student_id, subject_id, score, year) VALUES(3, 3, 82, 2018);
            INSERT INTO Marks (student_id, subject_id, score, year) VALUES(3, 4, 89, 2018);
            INSERT INTO Marks (student_id, subject_id, score, year) VALUES(3, 5, 56, 2018);
            INSERT INTO Marks (student_id, subject_id, score, year) VALUES(3, 7, 78, 2018);
            INSERT INTO Marks (student_id, subject_id, score, year) VALUES(4, 1, 65, 2018);
            INSERT INTO Marks (student_id, subject_id, score, year) VALUES(4, 2, 65, 2018);
            INSERT INTO Marks (student_id, subject_id, score, year) VALUES(4, 4, 66, 2018);
            INSERT INTO Marks (student_id, subject_id, score, year) VALUES(4, 5, 68, 2018);
            INSERT INTO Marks (student_id, subject_id, score, year) VALUES(4, 6, 74, 2018);
            INSERT INTO Marks (student_id, subject_id, score, year) VALUES(5, 2, 55, 2018);
            INSERT INTO Marks (student_id, subject_id, score, year) VALUES(5, 3, 56, 2018);
            INSERT INTO Marks (student_id, subject_id, score, year) VALUES(5, 5, 79, 2018);
            INSERT INTO Marks (student_id, subject_id, score, year) VALUES(5, 6, 24, 2018);
            INSERT INTO Marks (student_id, subject_id, score, year) VALUES(6, 1, 78, 2018);
            INSERT INTO Marks (student_id, subject_id, score, year) VALUES(6, 4, 36, 2018);
            INSERT INTO Marks (student_id, subject_id, score, year) VALUES(6, 5, 90, 2018);
            INSERT INTO Marks (student_id, subject_id, score, year) VALUES(6, 6, 45, 2018);
            INSERT INTO Marks (student_id, subject_id, score, year) VALUES(7, 1, 78, 2018);
            INSERT INTO Marks (student_id, subject_id, score, year) VALUES(7, 3, 76, 2018);
            INSERT INTO Marks (student_id, subject_id, score, year) VALUES(7, 4, 71, 2018);
            INSERT INTO Marks (student_id, subject_id, score, year) VALUES(7, 5, 69, 2018);
            INSERT INTO Marks (student_id, subject_id, score, year) VALUES(7, 7, 55, 2018);
                  ".Trim();

            public static string users = @"
INSERT INTO Users (username, password) VALUES ('admin', 'eOr3sFal9lg/wCrUsnP6bQ==');
      ".Trim();

            public static string newUser = @"
INSERT INTO Users(username, password) VALUES(@Username, @Password);
      ".Trim();
        }

        public static string initialise =
          List(Create.students, Create.subjects, Create.marks, Create.users,
               Insert.students, Insert.subjects, Insert.marks, Insert.users)
            .Fold("", (a, e) => a + e + "\n");
    }
}
