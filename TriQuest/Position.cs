using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TriQuest
{
	/// <summary>
	/// The position of a creature within a formation.
	/// </summary>
	public interface IPosition
	{
		/// <summary>
		/// The name of the position.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// The column in the formation.
		/// </summary>
		int Column { get; }

		/// <summary>
		/// The row in the formation.
		/// </summary>
		int Row { get; }
	}

	public class RelativePosition : IPosition
	{
		private RelativePosition(string name, int column, int row)
		{
			Name = name;
			Column = column;
			Row = row;
		}

		public string Name { get; private set; }
		public int Column { get; private set; }
		public int Row { get; private set; }

		public static readonly RelativePosition FrontLeft = new RelativePosition("front left", 0, 0);
		public static readonly RelativePosition Front = new RelativePosition("front", 1, 0);
		public static readonly RelativePosition FrontRight = new RelativePosition("front right", 2, 0);
		public static readonly RelativePosition Left = new RelativePosition("left", 0, 1);
		public static readonly RelativePosition Center = new RelativePosition("center", 1, 1);
		public static readonly RelativePosition Right = new RelativePosition("right", 2, 1);
		public static readonly RelativePosition RearLeft = new RelativePosition("rear left", 0, 2);
		public static readonly RelativePosition Rear = new RelativePosition("rear", 1, 2);
		public static readonly RelativePosition RearRight = new RelativePosition("rear right", 2, 2);

		public static readonly IEnumerable<RelativePosition> All = new RelativePosition[]
		{
			FrontLeft, Front, FrontRight, Left, Center, Right, RearLeft, Rear, RearRight
		};

		public static RelativePosition FromCoordinates(int col, int row)
		{
			return All.SingleOrDefault(dir => dir.Column == col && dir.Row == row);
		}

		/// <summary>
		/// Determines the absolute position of this position for a formation facing this direction.
		/// For instance, for a facing of west, the front right position would be located in the northwest.
		/// </summary>
		/// <param name="facing"></param>
		/// <returns></returns>
		public AbsolutePosition AsAbsolute(Direction facing)
		{
			if (facing == Direction.North)
				return AbsolutePosition.FromCoordinates(Column, Row);
			if (facing == Direction.South)
				return AbsolutePosition.FromCoordinates(2 - Column, 2 - Row);
			if (facing == Direction.East)
				return AbsolutePosition.FromCoordinates(2 - Row, Column);
			if (facing == Direction.West)
				return AbsolutePosition.FromCoordinates(Row, 2 - Column);

			throw new ArgumentException("Invalid facing specified, not north/south/east/west");
		}
	}

	public class AbsolutePosition : IPosition
	{
		private AbsolutePosition(string name, int column, int row, params Keys[] keys)
		{
			Name = name;
			Column = column;
			Row = row;
			Keys = keys;
		}

		public string Name { get; private set; }
		public int Column { get; private set; }
		public int Row { get; private set; }
		/// <summary>
		/// The keypad keys associated with this position.
		/// </summary>
		public IEnumerable<Keys> Keys { get; private set; }

		public static readonly AbsolutePosition Northwest = new AbsolutePosition("northwest", 0, 0, System.Windows.Forms.Keys.NumPad7, System.Windows.Forms.Keys.D7, System.Windows.Forms.Keys.Home);
		public static readonly AbsolutePosition North = new AbsolutePosition("north", 1, 0, System.Windows.Forms.Keys.NumPad8, System.Windows.Forms.Keys.D8, System.Windows.Forms.Keys.Up);
		public static readonly AbsolutePosition Northeast = new AbsolutePosition("northeast", 2, 0, System.Windows.Forms.Keys.NumPad9, System.Windows.Forms.Keys.D9, System.Windows.Forms.Keys.PageUp);
		public static readonly AbsolutePosition West = new AbsolutePosition("west", 0, 1, System.Windows.Forms.Keys.NumPad4, System.Windows.Forms.Keys.D4, System.Windows.Forms.Keys.Left);
		public static readonly AbsolutePosition Center = new AbsolutePosition("center", 1, 1, System.Windows.Forms.Keys.NumPad5,  System.Windows.Forms.Keys.D5, System.Windows.Forms.Keys.Clear);
		public static readonly AbsolutePosition East = new AbsolutePosition("east", 2, 1, System.Windows.Forms.Keys.NumPad6, System.Windows.Forms.Keys.D6, System.Windows.Forms.Keys.Right);
		public static readonly AbsolutePosition Southwest = new AbsolutePosition("southwest", 0, 2, System.Windows.Forms.Keys.NumPad1, System.Windows.Forms.Keys.D1, System.Windows.Forms.Keys.End);
		public static readonly AbsolutePosition South = new AbsolutePosition("south", 1, 2, System.Windows.Forms.Keys.NumPad2, System.Windows.Forms.Keys.D2, System.Windows.Forms.Keys.Down);
		public static readonly AbsolutePosition Southeast = new AbsolutePosition("southeast", 2, 2, System.Windows.Forms.Keys.NumPad3, System.Windows.Forms.Keys.D3, System.Windows.Forms.Keys.PageDown);

		public static readonly IEnumerable<AbsolutePosition> All = new AbsolutePosition[]
		{
			Northwest, North, Northeast, West, Center, East, Southwest, South, Southeast
		};

		/// <summary>
		/// Gets the position associated with a key press.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public static AbsolutePosition FromKey(Keys key)
		{
			foreach (var pos in All)
			{
				if (pos.Keys.Contains(key))
					return pos;
			}
			return null;
		}

		public static AbsolutePosition FromCoordinates(int col, int row)
		{
			return All.SingleOrDefault(dir => dir.Column == col && dir.Row == row);
		}

		/// <summary>
		/// Determines the relative position of this position to a direction.
		/// For instance, for a facing of west, the northwest position would be considered front right.
		/// </summary>
		/// <param name="facing"></param>
		/// <returns></returns>
		public RelativePosition RelativeTo(Direction facing)
		{
			if (facing == Direction.North)
				return RelativePosition.FromCoordinates(Column, Row);
			if (facing == Direction.South)
				return RelativePosition.FromCoordinates(2 - Column, 2 - Row);
			if (facing == Direction.East)
				return RelativePosition.FromCoordinates(Row, 2 - Column);
			if (facing == Direction.West)
				return RelativePosition.FromCoordinates(2 - Row, Column);

			throw new ArgumentException("Invalid facing specified, not north/south/east/west");
		}

		/// <summary>
		/// Computes Manhattan distance from this position to the target.
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		public int DistanceTo(AbsolutePosition target)
		{
			return Math.Abs(Column - target.Column) + Math.Abs(Row - target.Row);
		}
	}
}
