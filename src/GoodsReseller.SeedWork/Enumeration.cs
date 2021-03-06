using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GoodsReseller.SeedWork
{
    public abstract class Enumeration
    {
        public string Name { get; }
        public int Id { get; }

        protected Enumeration(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString() => Name;

        public static IEnumerable<T> GetAll<T>() where T : Enumeration

        {
            var fields = typeof(T).GetFields(BindingFlags.Public |
                                             BindingFlags.Static |
                                             BindingFlags.DeclaredOnly);
            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        public static bool TryParse<T>(string name, out T value) where T : Enumeration
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            value = GetAll<T>().FirstOrDefault(x => x.Name == name);
            return value != null;
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as Enumeration;
            if (otherValue == null)
                return false;
            var typeMatches = GetType() == obj.GetType();
            var valueMatches = Id.Equals(otherValue.Id);
            return typeMatches && valueMatches;
        }

        public int CompareTo(object other) => Id.CompareTo(((Enumeration) other).Id);
    }
}