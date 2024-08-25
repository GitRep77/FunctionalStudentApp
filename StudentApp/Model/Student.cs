using System;
using LanguageExt;
using System.Data;

namespace StudentApp.Model
{
    public class Student : Record<Student>
    {
        public readonly int Id;
        public readonly string Name;
        public readonly string Surname;
        public readonly Gender Gender;
        public readonly DateTime Dob;
        public int Age
        {
            get { return Dob.DurationInYears(to: DateTime.Today); }
        }

        private Student(int id, string name, string surname, Gender gender, DateTime dob)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Gender = gender;
            Dob = dob;
        }

        public static Student Create(int id, string name, string surname, Gender gender, DateTime dob)
        {
            return new Student(id, name, surname, gender, dob);
        }

        public static Student Create(IDataRecord record)
        {
            return Create(
              id: Convert.ToInt32(record["id"]),
              name: Convert.ToString(record["name"]),
              surname: Convert.ToString(record["surname"]),
              gender: GenderConvert.CharToGender(Convert.ToString(record["gender"])),
              dob: ((long)Convert.ToInt32(record["dob"])).UnixTimeSecondsToDateTime()
            );
        }
    }
}
