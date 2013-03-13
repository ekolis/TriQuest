using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriQuest
{
	/// <summary>
	/// Skills that use mana to cast.
	/// </summary>
	public class Skill
	{
		public Skill()
		{
			Verb = "uses";
		}

		/// <summary>
		/// The name of the skill.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The verb for using the skill. Defaults to "uses".
		/// </summary>
		public string Verb { get; set; }

		/// <summary>
		/// Description of this skill.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// The mana cost of the skill.
		/// </summary>
		public int ManaCost { get; set; }

		/// <summary>
		/// Uses the skill.
		/// The parameters are, in order:
		/// 1. The creature using the skill
		/// 2. The formation that the user belongs to
		/// 3. The tile directly in front of the user's formation.
		/// 4. The map.
		/// </summary>
		public Action<Creature, Formation, Tile, Map> Use { get; set; }
	}
}
