using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MediaLakeCore.BuildingBlocks.Domain
{
	public abstract class EnumerationValueObject<T> : ValueObject, IEnumerationValueObject
		where T : IEnumerationValueObject
	{
		private EnumerationValueObject()
		{
			// For EF Core
		}
		protected EnumerationValueObject(string value)
		{
			Value = value;
		}
		public string Value { get; }
		public static T Create(string value)
		{
			T matchingValue = GetAll().FirstOrDefault(x => x.Value.Equals(value));
			if (matchingValue == null)
			{
				throw new ArgumentException($"{value} is not a valid {typeof(T).Name}");
			}
			return matchingValue;
		}
		public static IEnumerable<T> GetAll()
		{
			PropertyInfo[] properties = typeof(T).GetProperties(
				BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
			return properties.Select(f => f.GetValue(null)).Cast<T>();
		}
		public static bool HasValue(string value)
		{
			return GetAll().Any(x => x.Value == value);
		}
	}
}
