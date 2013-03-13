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
	public class Creature : ICloneable<Creature>
	{
		public Creature()
		{
			Health = MaxHealth;
			Mana = MaxMana;
			PhysicalAttackText = "attacks";
			MentalAttackText = "mind-attacks";
			PhysicalAttackRange = 1;
			MentalAttackRange = 1;
			Skills = new List<Skill>();
		}

		public static int MaxHealth = 100;
		public static int MaxMana = 100;

		/// <summary>
		/// Creature's name.
		/// </summary>
		public string Name { get; set; }

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

		/// <summary>
		/// How many tiles can this creature see?
		/// A formation's sight is the maximum of its creatures' sight.
		/// </summary>
		public int Sight { get; set; }

		/// <summary>
		/// The delay before this creature can act again.
		/// </summary>
		public double Delay { get; set; }

		/// <summary>
		/// Descriptive text for the creature's physical attack, e.g. "slashes"
		/// </summary>
		public string PhysicalAttackText { get; set; }

		/// <summary>
		/// Descriptive text for the creature's mental attack, e.g. "ki-blasts"
		/// </summary>
		public string MentalAttackText { get; set; }

		/// <summary>
		/// Range of this creature's physical attack. Attacking enemies that are out of range incurs an accuracy penalty.
		/// </summary>
		public int PhysicalAttackRange { get; set; }

		/// <summary>
		/// Range of this creature's mental attack. Attacking enemies that are out of range incurs an accuracy penalty.
		/// </summary>
		public int MentalAttackRange { get; set; }

		/// <summary>
		/// Creature's health. Upon reaching zero, creature dies.
		/// </summary>
		public int Health { get; set; }

		/// <summary>
		/// Creature's mana. Used for skills/magic.
		/// </summary>
		public int Mana { get; set; }

		/// <summary>
		/// Creature's skills and magic spells.
		/// </summary>
		public IList<Skill> Skills { get; private set; }

		public Creature Clone()
		{
			var c = new Creature();
			c.Attack = Attack;
			c.Body = Body;
			c.Color = Color;
			c.Defense = Defense;
			c.Level = Level;
			c.Mind = Mind;
			c.Name = Name;
			c.Sight = Sight;
			c.Speed = Speed;
			c.Symbol = Symbol;
			c.Delay = Delay;
			c.PhysicalAttackText = PhysicalAttackText;
			c.MentalAttackText = MentalAttackText;
			c.Health = Health;
			return c;
		}

		object ICloneable.Clone()
		{
			return Clone();
		}

		/// <summary>
		/// Spends time on an action, incrementing the creature's delay counter in inverse proprtion to its speed.
		/// </summary>
		/// <param name="actionCost"></param>
		public void Act(double actionCost)
		{
			Delay += actionCost / Speed;
		}
	}
}
