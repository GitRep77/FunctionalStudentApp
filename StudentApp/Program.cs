using System;
using System.Windows.Forms;
using StudentApp.Extensions;
using StudentApp.Model;
using StudentApp.Sql;

namespace StudentApp
{
    class Program
    {
        static void Main(string[] args)
        {
#if NET472
        Console.WriteLine("C# 7.3 is likely being used.");
#else
            Console.WriteLine("A different version of C# is in use.");
#endif

            #region Test Code
            Console.WriteLine("\n----------------------SQL.Initialise");
            Console.WriteLine(Query.Initialise());

            Console.WriteLine("\n----------------------AddUser");
            Console.WriteLine(Query.AddUser("hacker", "password"));

            Console.WriteLine("\n----------------------GetUsers");
            Query.GetUsers().Match(
              Right: (users) =>
              {
                  foreach (User user in users)
                  {
                      Console.WriteLine(user);
                  }
              },
              Left: Console.WriteLine
            );

            Console.WriteLine("\n----------------------IsValidUser");
            Console.WriteLine(Query.IsValidUser("admin", "admin"));

            Console.WriteLine("\n----------------------GetAverageMark");
            Console.WriteLine(Query.GetAverageMark(studentId: 3));

            Console.WriteLine("\n----------------------GetStudentById");
            Query.GetStudentById(studentId: 3).Match(
              Right: (students) =>
              {
                  foreach (Student student in students)
                  {
                      Console.WriteLine(student);
                  }
              },
              Left: Console.WriteLine
            );

            Console.WriteLine("\n----------------------GetSubjectMarksStudentById");
            Query.GetSubjectMarksStudentById(studentId: 3).Match(
              Right: (subjectMarks) =>
              {
                  foreach (Tuple<int, string, int> subjectMark in subjectMarks)
                  {
                      Console.WriteLine(subjectMark);
                  }
              },
              Left: Console.WriteLine
            );

            Console.WriteLine("\n----------------------GetSubjectMarks");
            Query.GetSubjectMarks().Match(
              Right: (subjectMarks) => {
                  foreach (Tuple<int, string, int> subjectMark in subjectMarks)
                  {
                      Console.WriteLine(subjectMark);
                  }
              },
              Left: Console.WriteLine
            );

            Console.WriteLine("\n----------------------GetAllStudents");
            Query.GetAllStudents().Match(
              Right: (students) => {
                  foreach (Student student in students)
                  {
                      Console.WriteLine(student);
                  }
              },
              Left: Console.WriteLine
            );

            Console.WriteLine("\n----------------------GetStudentsMaxSubjects");
            Query.GetStudentsMaxSubjects().Match(
              Right: (students) => {
                  foreach (Tuple<int, string, string, int> student in students)
                  {
                      Console.WriteLine(student);
                  }
              },
              Left: Console.WriteLine
            );


            Console.WriteLine("\n----------------------GetStudentMaxSubjects");
            Query.GetStudentMaxSubjects(3).Match(
              Right: (students) => {
                  foreach (Tuple<int, string, string, Gender, DateTime, int> student in students)
                  {
                      Console.WriteLine(student);
                  }
              },
              Left: Console.WriteLine
            );

            Console.WriteLine("\n----------------------GetTotalSubjects");
            Console.WriteLine(Query.GetTotalSubjects(studentId: 3));

            Console.WriteLine("\n----------------------DataGridView Styling");
            var dgv = new DataGridView().ApplyStyle();
            Console.WriteLine(dgv.GridColor);

            Console.WriteLine("\n----------------------AESLib.Decrypt");
            var decryptResult = "eOr3sFal9lg/wCrUsnP6bQ==".DecryptAES();
            Console.WriteLine(decryptResult);

            Console.WriteLine("\n----------------------AESLib.Encrypt");
            var encryptResult = "admin".EncryptAES();
            Console.WriteLine(encryptResult);

            //var test1 = Undefined<string>("not implemented");
            #endregion

            Console.ReadLine();
        }
    }
}
