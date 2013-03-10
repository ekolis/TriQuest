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
	}

	public class AbsolutePosition : IPosition
	{
		private AbsolutePosition(string name, int column, int row, Keys key)
		{
			Name = name;
			Column = column;
			Row = row;
			Key = key;
		}

		public string Name { get; private set; }
		public int Column { get; private set; }
		public int Row { get; private set; }
		/// <summary>
		/// The keypad key associated with this position.
		/// </summary>
		public Keys Key { get; private set; }

		public static readonly AbsolutePosition Northwest = new AbsolutePosition("northwest", 0, 0, Keys.NumPad7);
		public static readonly AbsolutePosition North = new AbsolutePosition("north", 1, 0, Keys.NumPad8);
		public static readonly AbsolutePosition Northeast = new AbsolutePosition("northeast", 2, 0, Keys.NumPad9);
		public static readonly AbsolutePosition West = new AbsolutePosition("west", 0, 1, Keys.NumPad4);
		public static readonly AbsolutePosition Center = new AbsolutePosition("center", 1, 1, Keys.NumPad5);
		public static readonly AbsolutePosition East = new AbsolutePosition("east", 2, 1, Keys.NumPad6);
		public static readonly AbsolutePosition Southwest = new AbsolutePosition("southwest", 0, 2, Keys.NumPad1);
		public static readonly AbsolutePosition South = new AbsolutePosition("south", 1, 2, Keys.NumPad2);
		public static readonly AbsolutePosition Southeast = new AbsolutePosition("southeast", 2, 2, Keys.NumPad3);

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
				if (pos.Key == key)
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
		/// For instance, for a facing of west, the northeast position would be considered front right.
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
	}
}
