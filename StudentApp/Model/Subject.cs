using System;
using LanguageExt;
using System.Data;


namespace StudentApp_FunctionalProgramming_.Model
{
    public class Subject : Record<Subject>
    {
        public readonly int Id;
        public readonly string Name;

        private Subject(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public static Subject Create(int id, string name)
        {
            return new Subject(id, name);
        }

        public static Subject Create(IDataRecord record)
        {
            return Create(
              id: Convert.ToInt32(record["id"]),
              name: Convert.ToString(record["name"])
            );
        }
    }
}
