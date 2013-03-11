using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriQuest
{
	public static class Extensions
	{
		/// <summary>
		/// Capitalizes the first character of a string if it is a letter.
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static string Capitalize(this string s)
		{
			return s[0].ToString().ToUpper() + s.Substring(1);
		}
	}
}
