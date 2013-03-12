using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriQuest
{
	/// <summary>
	/// Randomization.
	/// </summary>
	public static class Dice
	{
		private static Random r = new Random();

		/// <summary>
		/// Returns a random integer within a range. Includes the max.
		/// </summary>
		/// <param name="min"></param>
		/// <param name="max"></param>
		/// <returns></returns>
		public static int Range(int min, int max)
		{
			return r.Next(max - min + 1) + min;
		}

		/// <summary>
		/// Returns a random double within a range. Does not include the max.
		/// </summary>
		/// <param name="min"></param>
		/// <param name="max"></param>
		/// <returns></returns>
		public static double Range(double min, double max)
		{
			return r.NextDouble() * (max - min) + min;
		}

		/// <summary>
		/// Picks a random item from a list.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="items"></param>
		/// <returns></returns>
		public static T Pick<T>(this IEnumerable<T> items)
		{
			return items.ElementAt(Range(0, items.Count() - 1));
		}

		/// <summary>
		/// Rolls multiple dice with the same number of sides.
		/// </summary>
		/// <param name="dice"></param>
		/// <param name="sides"></param>
		/// <returns></returns>
		public static int Roll(int dice, int sides)
		{
			int result = 0;
			for (var i = 0; i < dice; i++)
				result += Range(1, sides);
			return result;
		}
	}
}
