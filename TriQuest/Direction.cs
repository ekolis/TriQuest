using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TriQuest
{
	/// <summary>
	/// A cardinal direction of movement.
	/// </summary>
	public class Direction
	{
		private Direction(int dx, int dy, params Keys[] keys)
		{
			DeltaX = dx;
			DeltaY = dy;
			Keys = keys;
		}

		/// <summary>
		/// Change in the X coordinate when moving in this direction.
		/// </summary>
		public int DeltaX { get; private set; }
		/// <summary>
		/// Change in the Y coordinate when moving in this direction.
		/// </summary>
		public int DeltaY { get; private set; }
		/// <summary>
		/// Keys that can be pressed to move in this direction.
		/// </summary>
		public IEnumerable<Keys> Keys { get; private set; }

		public static readonly Direction North = new Direction(0, -1, System.Windows.Forms.Keys.Up, System.Windows.Forms.Keys.NumPad8, System.Windows.Forms.Keys.D8);
		public static readonly Direction South = new Direction(0, 1, System.Windows.Forms.Keys.Down, System.Windows.Forms.Keys.NumPad2, System.Windows.Forms.Keys.D2);
		public static readonly Direction East = new Direction(1, 0, System.Windows.Forms.Keys.Right, System.Windows.Forms.Keys.NumPad6, System.Windows.Forms.Keys.D6);
		public static readonly Direction West = new Direction(-1, 0, System.Windows.Forms.Keys.Left, System.Windows.Forms.Keys.NumPad4, System.Windows.Forms.Keys.D4);

		public static readonly IEnumerable<Direction> All = new Direction[] { North, South, East, West };

		/// <summary>
		/// Gets the direction associated with a key press.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static Direction FromKey(Keys key)
		{
			foreach (var dir in All)
			{
				if (dir.Keys.Contains(key))
					return dir;
			}
			return null;
		}
	}
}
