using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TriQuest
{
	/// <summary>
	/// A formation of heroes or monsters.
	/// </summary>
	public class Formation
	{
		public Formation()
		{
			CreaturePositions = new Dictionary<RelativePosition, Creature>();
			Facing = Direction.North;
		}

		/// <summary>
		/// The positions of the creatures in this formation.
		/// </summary>
		public IDictionary<RelativePosition, Creature> CreaturePositions { get; private set; }

		/// <summary>
		/// The direction that the entire formation is facing.
		/// </summary>
		public Direction Facing { get; set; }

		/// <summary>
		/// The character used to represent this formation on the map.
		/// </summary>
		public char Symbol
		{
			get
			{
				if (Representative == null)
					return ' ';
				return Representative.Symbol;
			}
		}

		/// <summary>
		/// The color used to represent this formation on the map.
		/// </summary>
		public Color Color
		{
			get
			{
				if (Representative == null)
					return Color.Transparent;
				return Representative.Color;
			}
		}

		/// <summary>
		/// The creature used to represent this formation on the map.
		/// </summary>
		public Creature Representative
		{
			get
			{
				// no creatures?
				if (!CreaturePositions.Any())
					return null;

				var maxLevel = CreaturePositions.Values.Select(c => c.Level).Max();
				var maxes = CreaturePositions.Where(kvp => kvp.Value.Level == maxLevel);

				// favor high level creatures
				if (maxes.Count() == 1)
					return maxes.Single().Value;

				// favor creatures in the middle
				if (maxes.Select(kvp => kvp.Key).Contains(RelativePosition.Center))
					return CreaturePositions[RelativePosition.Center];

				// pick one arbitrarily from the high level creatures
				return maxes.First().Value;
			}
		}

		public Creature GetCreature(RelativePosition pos)
		{
			if (CreaturePositions.ContainsKey(pos))
				return CreaturePositions[pos];
			return null;
		}

		public Creature GetCreature(AbsolutePosition pos)
		{
			return GetCreature(pos.RelativeTo(Facing));
		}
	}
}
