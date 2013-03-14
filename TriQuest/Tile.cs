using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TriQuest
{
	/// <summary>
	/// A tile on the game map. Composed of nine subtiles in a 3x3 grid.
	/// </summary>
	public class Tile
	{
		public Tile()
		{
			Items = new Dictionary<AbsolutePosition, Item>();
		}

		/// <summary>
		/// The danger level of this tile. Controls its terrain and the kinds of monsters that spawn here.
		/// </summary>
		public int DangerLevel { get; set; }

		/// <summary>
		/// The terrain of this tile.
		/// </summary>
		public Terrain Terrain { get; set; }

		/// <summary>
		/// The formation (if any) that is located here.
		/// </summary>
		public Formation Formation { get; set; }

		public char Symbol
		{
			get
			{
				if (Formation != null)
					return Formation.Symbol;
				return Terrain.Symbol;
			}
		}

		public Color Color
		{
			get
			{
				if (Formation != null)
					return Formation.Color;
				return Terrain.Color;
			}
		}

		public char GetSymbol(AbsolutePosition pos)
		{
			if (Formation == null)
			{
				if (Items.ContainsKey(pos))
					return Items[pos].Symbol;
				return Terrain.Symbol;
			}
			var creature = Formation.GetCreature(pos);
			if (creature == null)
			{
				if (Items.ContainsKey(pos))
					return Items[pos].Symbol;
				return Terrain.Symbol;
			}
			return creature.Symbol;
		}

		public Color GetColor(AbsolutePosition pos)
		{
			if (Formation == null)
			{
				if (Items.ContainsKey(pos))
					return Items[pos].Color;
				return Terrain.Color;
			}
			var creature = Formation.GetCreature(pos);
			if (creature == null)
			{
				if (Items.ContainsKey(pos))
					return Items[pos].Color;
				return Terrain.Color;
			}
			return creature.Color;
		}

		/// <summary>
		/// Have the heroes seen this tile yet?
		/// </summary>
		public bool HasBeenSeen
		{
			get;
			set;
		}

		/// <summary>
		/// Items that are lying on the ground.
		/// </summary>
		public IDictionary<AbsolutePosition, Item> Items { get; private set; }
	}
}
