using System;
using System.Data;
using System.Data.SQLite;
using System.Data.SqlClient;
using System.Collections.Generic;
using LanguageExt;
using StudentApp_FunctionalProgramming_.Model;

namespace StudentApp_FunctionalProgramming_.Sql
{
    public static class Query
    {

        public static Action<IDataParameterCollection> NewUserParam(string username, string password)
        {
            return fn => {
                fn.Add(new SQLiteParameter("@UserName", username));
                fn.Add(new SQLiteParameter("@Password", password));
            };
        }

        public static Action<IDataParameterCollection> IsValidUserParam(string username)
        {
            return fn => {
                fn.Add(new SQLiteParameter("@UserName", username));
            };
        }

        public static Action<IDataParameterCollection> StudentParam(int studentId)
        {
            return fn => {
                fn.Add(new SQLiteParameter("@StudentID", studentId));
            };
        }

        public static Either<string, int> Initialise()
        {
            Console.WriteLine("initialise");
            var newDB = SQL.CreateSQLiteDB(Config.dbFilePath);
            return SQL
              .ConnectToSQLite(Config.dbConnection)
              .Bind(SQL.BindSQL(script: Script.initialise))
              .Bind(SQL.ExecuteNonQuery());
        }

        public static Either<string, int> AddUser(string username, string password)
        {
            password.EncryptAES().Match(
              Right: (encryptedResult) => password = encryptedResult,
              Left: (error) => { throw new Exception(error); }
            );

            return SQL
              .ConnectToSQLite(Config.dbConnection)
              .Bind(SQL.Parameters(NewUserParam(username, password)))
              .Bind(SQL.BindSQL(script: Script.Insert.newUser))
              .Bind(SQL.ExecuteNonQuery());
        }

        public static bool IsValidUser(string username, string password)
        {
            password.EncryptAES().Match(
              Right: (encryptedResult) => password = encryptedResult,
              Left: (error) => { throw new Exception(error); }
            );
            var candidate = User.Create(username, password);
            var resultSet = SQL
              .ConnectToSQLite(Config.dbConnection)
              .Bind(SQL.Parameters(IsValidUserParam(username)))
              .Bind(SQL.BindSQL(script: Script.Select.validUser))
              .Bind(SQL.ExecuteReader(transform: User.Create));
            var result = false;
            resultSet.Match(
              Right: (users) => { result = users.Contains(candidate); },
              Left: (error) => { } // NOP
            );
            return result;
        }

        public static Either<string, List<User>> GetUsers()
        {
            return SQL
              .ConnectToSQLite(Config.dbConnection)
              .Bind(SQL.BindSQL(script: Script.Select.sqlGetUsers))
              .Bind(SQL.ExecuteReader(transform: User.Create));
        }

        public static Either<string, int> GetAverageMark(int studentId)
        {
            return SQL
              .ConnectToSQLite(Config.dbConnection)
              .Bind(SQL.Parameters(StudentParam(studentId)))
              .Bind(SQL.BindSQL(script: Script.Select.sqlGetAverageMark))
              .Bind(SQL.ExecuteScalar(transform: (average) => {
                  if (average.GetType() != typeof(DBNull))
                  {
                      return Convert.ToInt32(average);
                  }
                  else
                  {
                      throw new Exception("GetAverageMark: DBNull");
                  }
              }));
        }

        public static Either<string, List<Student>> GetStudentById(int studentId)
        {
            return SQL
              .ConnectToSQLite(Config.dbConnection)
              .Bind(SQL.Parameters(StudentParam(studentId)))
              .Bind(SQL.BindSQL(script: Script.Select.sqlGetStudentsById))
              .Bind(SQL.ExecuteReader(transform: Student.Create));
        }

        public static Either<string, List<Tuple<int, string, int>>> GetSubjectMarksStudentById(int studentId)
        {
            return SQL
              .ConnectToSQLite(Config.dbConnection)
              .Bind(SQL.Parameters(StudentParam(studentId)))
              .Bind(SQL.BindSQL(script: Script.Select.sqlGetSubjectsMarksByStudent))
              .Bind(SQL.ExecuteReader(transform: (record) => {
                  return Tuple.Create(
              Convert.ToInt32(record["student_id"]),
              Convert.ToString(record["name"]),
              Convert.ToInt32(record["score"])
            );
              }));
        }

        public static Either<string, List<Tuple<int, string, int>>> GetSubjectMarks()
        {
            return SQL
              .ConnectToSQLite(Config.dbConnection)
              .Bind(SQL.BindSQL(script: Script.Select.sqlGetSubjectsMarks))
              .Bind(SQL.ExecuteReader(transform: (record) => {
                  return Tuple.Create(
              Convert.ToInt32(record["student_id"]),
              Convert.ToString(record["name"]),
              Convert.ToInt32(record["score"])
            );
              }));
        }

        public static Either<string, List<Student>> GetAllStudents()
        {
            return SQL
              .ConnectToSQLite(Config.dbConnection)
              .Bind(SQL.BindSQL(script: Script.Select.sqlGetAllStudents))
              .Bind(SQL.ExecuteReader(transform: Student.Create));
        }

        public static Either<string, List<Tuple<int, string, string, int>>> GetStudentsMaxSubjects()
        {
            return SQL
              .ConnectToSQLite(Config.dbConnection)
              .Bind(SQL.BindSQL(script: Script.Select.sqlGetStudentsMaxSubjects))
              .Bind(SQL.ExecuteReader(transform: (record) => {
                  return Tuple.Create(
              Convert.ToInt32(record["id"]),
              Convert.ToString(record["name"]),
              Convert.ToString(record["surname"]),
              Convert.ToInt32(record["total"])
            );
              }));
        }

        public static Either<string, List<Tuple<int, string, string, Gender, DateTime, int>>> GetStudentMaxSubjects(int studentId)
        {
            return SQL
              .ConnectToSQLite(Config.dbConnection)
              .Bind(SQL.Parameters(StudentParam(studentId)))
              .Bind(SQL.BindSQL(script: Script.Select.sqlGetStudentMaxSubjects))
              .Bind(SQL.ExecuteReader(transform: (record) => {
                  return Tuple.Create(
              Convert.ToInt32(record["id"]),
              Convert.ToString(record["name"]),
              Convert.ToString(record["surname"]),
              GenderConvert.CharToGender(Convert.ToString(record["gender"])),
              ((long)Convert.ToInt32(record["dob"])).UnixTimeSecondsToDateTime(),
              Convert.ToInt32(record["total"])
            );
              }));
        }

        public static Either<string, int> GetTotalSubjects(int studentId)
        {
            return SQL
              .ConnectToSQLite(Config.dbConnection)
              .Bind(SQL.Parameters(StudentParam(studentId)))
              .Bind(SQL.BindSQL(script: Script.Select.sqlStudentTotalSubjects))
              .Bind(SQL.ExecuteScalar(transform: (total) => {
                  if (total.GetType() != typeof(DBNull))
                  {
                      return Convert.ToInt32(total);
                  }
                  else
                  {
                      throw new Exception("GetTotalSubjects: DBNull");
                  }
              }));
        }
    }
}
