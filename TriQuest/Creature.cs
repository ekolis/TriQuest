using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TriQuest
{
	/// <summary>
	/// A hero or a monster.
	/// </summary>
	public class Creature
	{
		/// <summary>
		/// Creature's attack power with physical and magical attacks.
		/// </summary>
		public int Attack { get; set; }

		/// <summary>
		/// Creature's defense power against physical and magical attacks.
		/// </summary>
		public int Defense { get; set; }

		/// <summary>
		/// Bonus to attack and defense for physical attacks.
		/// </summary>
		public int Body { get; set; }

		/// <summary>
		/// Bonus to attack and defense for magical attacks.
		/// </summary>
		public int Mind { get; set; }

		/// <summary>
		/// Creature's movement and attack speed.
		/// A formation moves at the speed of its slowest creature, but faster creatures can get multiple attacks.
		/// </summary>
		public int Speed { get; set; }

		/// <summary>
		/// The character used to represent this creature on the map.
		/// </summary>
		public char Symbol { get; set; }

		/// <summary>
		/// The color used to represent this creature on the map.
		/// </summary>
		public Color Color { get; set; }

		/// <summary>
		/// The experience (for heroes) or danger (for monsters) level of this creature.
		/// </summary>
		public int Level { get; set; }
	}
}
