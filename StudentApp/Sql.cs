using System;
using System.Collections.Generic;
using LanguageExt;
using static LanguageExt.Prelude;
using System.Data.SQLite;
using System.Data;
using System.Data.SqlClient;
using static StudentApp.Prelude;

namespace StudentApp
{
    using DBInstance = Tuple<IDbConnection, IDbCommand>;

    public static class SQL
    {

        #region New SQLite Database Instance
        /// <summary>
        /// Creates a new SQLite Database file at the specified path.
        /// </summary>
        public static Either<string, bool> CreateSQLiteDB(string dbFilePath)
        {
            return TryEitherLog<bool>(() => {
                dbFilePath.DeleteFileIfExists().Match(
                  Right: (success) => { }, // NOP
                  Left: (message) => { throw new Exception(message); }
                );
                SQLiteConnection.CreateFile(dbFilePath);
                return Right(true);
            });
        }
        #endregion

        #region Connectivity
        /// <summary>
        /// Connect to SQLite instance using a specified connectionString.
        /// </summary>
        public static Either<string, DBInstance> ConnectToSQLite(string connectionString)
        {
            return TryEitherLog<DBInstance>(() => {
                var connection = new SQLiteConnection(connectionString);
                connection.Open();
                var command = connection.CreateCommand();
                return Right(new DBInstance(connection, command));
            });
        }

        /// <summary>
        /// Connect to SqlClient instance using a specified connectionString.
        /// </summary>
        public static Either<string, DBInstance> ConnectToSqlClient(string connectionString)
        {
            return TryEitherLog<DBInstance>(() => {
                var connection = new SqlConnection(connectionString);
                connection.Open();
                var command = connection.CreateCommand();
                return Right(new DBInstance(connection, command));
            });
        }
        #endregion

        #region Execute Queries
        /// <summary>
        /// Executes the non query.
        /// </summary>
        public static Func<DBInstance, Either<string, int>> ExecuteNonQuery()
        {
            return db => {
                return TryEitherLog<int>(() => {
                    var connection = db.Item1; var command = db.Item2;
                    var result = command.ExecuteNonQuery();
                    db.DisposeAndClose();
                    return Right(result);
                });
            };
        }

        /// <summary>
        /// Executes the reader.
        /// </summary>
        public static Func<DBInstance, Either<string, List<T>>> ExecuteReader<T>(Func<IDataReader, T> transform)
        {
            return db => {
                return TryEitherLog<List<T>>(() => {
                    var connection = db.Item1; var command = db.Item2;
                    var reader = command.ExecuteReader();
                    var result = reader.ToType(transform);
                    reader.Dispose();
                    db.DisposeAndClose();
                    return Right(result);
                });
            };
        }

        /// <summary>
        /// Executes the scalar.
        /// </summary>
        public static Func<DBInstance, Either<string, T>> ExecuteScalar<T>(Func<object, T> transform)
        {
            return db => {
                return TryEitherLog<T>(() => {
                    var connection = db.Item1; var command = db.Item2;
                    var result = transform(command.ExecuteScalar());
                    db.DisposeAndClose();
                    return Right(result);
                });
            };
        }
        #endregion

        #region Bind SQL or Stored Procedure
        public static Func<DBInstance, Either<string, DBInstance>> BindSQL(string script)
        {
            return db => {
                return TryEitherLog<DBInstance>(() => {
                    var connection = db.Item1; var command = db.Item2;
                    command.CommandText = script;
                    return Right(new DBInstance(connection, command));
                });
            };
        }

        public static Func<DBInstance, Either<string, DBInstance>> BindProcedure(string script)
        {
            return db => {
                return TryEitherLog<DBInstance>(() => {
                    var connection = db.Item1; var command = db.Item2;
                    command.CommandText = script;
                    command.CommandType = CommandType.StoredProcedure;
                    return Right(new DBInstance(connection, command));
                });
            };
        }

        #endregion

        #region Sql Parameters tied to Values
        public static Func<DBInstance, Either<string, DBInstance>> Parameters(Action<IDataParameterCollection> fn)
        {
            return (db) => {
                return TryEitherLog<DBInstance>(() => {
                    var connection = db.Item1; var command = db.Item2;
                    fn(command.Parameters);
                    return Right(new DBInstance(connection, command));
                });
            };
        }
        #endregion

        #region SqlExtension Methods
        public static void DisposeAndClose(this DBInstance db)
        {
            db.Item2.Dispose();
            db.Item1.Close();
        }

        public static List<T> ToType<T>(this IDataReader reader, Func<IDataReader, T> fn)
        {
            var result = new List<T>();
            while (reader.Read())
            {
                result.Add(fn(reader));
            }
            return result;
        }
        #endregion
    }
}
