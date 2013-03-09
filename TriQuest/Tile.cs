using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriQuest
{
	/// <summary>
	/// A tile on the game map. Composed of nine subtiles in a 3x3 grid.
	/// </summary>
	public class Tile
	{
		/// <summary>
		/// The danger level of this tile. Controls its terrain and the kinds of monsters that spawn here.
		/// </summary>
		public int DangerLevel { get; set; }

		/// <summary>
		/// The terrain of this tile.
		/// </summary>
		public Terrain Terrain { get; set; }
	}
}
