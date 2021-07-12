using System;
using System.Collections.Generic;

namespace mbgex2
{
	internal static class Extensions
	{
		public static string Sanitize(this string str)
		{
			var arr = new List<char>();

			for (var i = 0; i < str.Length; i++)
			{
				if (i < str.Length - 1 && str[i] == '\\' && str[i + 1] == 'u')
				{
					arr.Add(char.Parse(str.Substring(i, 5)));
					i += 4;
					continue;
				}

				arr.Add(str[i]);
			}

			return new string(arr.ToArray());
		}
	}
}
