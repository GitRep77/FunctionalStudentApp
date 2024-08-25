using System;
using System.IO;
using LanguageExt;
using static LanguageExt.Prelude;

namespace StudentApp
{
    public static class Prelude
    {
        #region File / Directory
        public static Either<string, bool> DeleteFileIfExists(this string filePath)
        {
            return TryEitherLog<bool>(() => {
                var result = false;
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    result = true;
                }
                return result;
            });
        }

        public static Either<string, bool> CreateDirectory(this string filePath)
        {
            return TryEitherLog<bool>(() => {
                var result = false;
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                    result = true;
                }
                return result;
            });
        }
        #endregion

        #region Exception Handling / Logging
        public static string LogDefault(Exception e)
        {
            return string.Format("message: {0}\ntrace: {1}\n", e.Message, e.StackTrace);
        }

        public static Either<string, T> TryEitherLog<T>(Func<Either<string, T>> tryBlock)
        {
            try
            {
                return tryBlock();
            }
            catch (Exception e)
            {
                return Left(LogDefault(e));
            }
        }
        #endregion

        #region With Reference Combinator
        public static A With<A>(this A a, Action<A> f)
        {
            f(a);
            return a;
        }
        #endregion

        #region DateTime Extension Methods
        public static int DurationInYears(this DateTime from, DateTime to)
        {
            var years = to.Year - from.Year;
            return (from > to.AddYears(-years)) ? years-- : years;
        }

        public static long ToUnixTimeSeconds(this DateTime from)
        {
            var timeOffset = new DateTimeOffset(from);
            return timeOffset.ToUnixTimeSeconds();
        }

        public static DateTime UnixTimeSecondsToDateTime(this long from)
        {
            return DateTimeOffset.FromUnixTimeSeconds(from).DateTime.ToLocalTime();
        }
        #endregion

        public static T Undefined<T>(string hint = "")
        {
            throw new Exception(message: hint);
        }

    }
}
