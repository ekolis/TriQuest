using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TriQuest
{
	public class MonsterTemplate
	{
		public MonsterTemplate(Creature archetype, int minLevel, int maxLevel)
		{
			Archetype = archetype;
			MinLevel = minLevel;
			MaxLevel = maxLevel;
			all.Add(this);
		}

		public Creature Archetype { get; private set; }

		public int MinLevel { get; private set; }

		public int MaxLevel { get; private set; }

		public static Creature Spawn(int dangerLevel)
		{
			var ok = All.Where(mt => mt.MinLevel <= dangerLevel && mt.MaxLevel >= dangerLevel);
			if (ok.Any())
			{
				var m = ok.Pick().Archetype.Clone();
				m.Level = dangerLevel;
				return m;
			}
			throw new Exception("No monsters defined for danger level " + dangerLevel);
		}

		private static HashSet<MonsterTemplate> all = new HashSet<MonsterTemplate>();

		public static IEnumerable<MonsterTemplate> All { get { return all; } }

		public static MonsterTemplate Rat = new MonsterTemplate(new Creature
		{
			Name = "rat",
			Attack = 2,
			Defense = 1,
			Mind = 1,
			Body = 2,
			Speed = 4,
			Symbol = 'r',
			Color = Color.Brown,
			Sight = 3,
		}, 1, 3);

		public static MonsterTemplate Wolf = new MonsterTemplate(new Creature
		{
			Name = "wolf",
			Attack = 3,
			Defense = 2,
			Mind = 1,
			Body = 4,
			Speed = 5,
			Symbol = 'w',
			Color = Color.Gray,
			Sight = 5,
		}, 2, 4);

		public static MonsterTemplate Goblin = new MonsterTemplate(new Creature
		{
			Name = "goblin",
			Attack = 4,
			Defense = 4,
			Mind = 2,
			Body = 5,
			Speed = 5,
			Symbol = 'g',
			Color = Color.Green,
			Sight = 4,
		}, 3, 5);

		public static MonsterTemplate OrcBrute = new MonsterTemplate(new Creature
		{
			Name = "orc brute",
			Attack = 5,
			Defense = 5,
			Mind = 1,
			Body = 5,
			Speed = 5,
			Symbol = 'o',
			Color = Color.Red,
			Sight = 5,
		}, 4, 6);

		public static MonsterTemplate OrcShaman = new MonsterTemplate(new Creature
		{
			Name = "orc shaman",
			Attack = 4,
			Defense = 7,
			Mind = 5,
			Body = 4,
			Speed = 5,
			Symbol = 'o',
			Color = Color.Green,
			Sight = 5,
		}, 5, 7);

		public static MonsterTemplate Golem = new MonsterTemplate(new Creature
		{
			Name = "golem",
			Attack = 5,
			Defense = 8,
			Mind = 1,
			Body = 7,
			Speed = 3,
			Symbol = 'G',
			Color = Color.Gray,
			Sight = 4,
		}, 6, 8);

		public static MonsterTemplate DarkSage = new MonsterTemplate(new Creature
		{
			Name = "dark sage",
			Attack = 6,
			Defense = 4,
			Mind = 8,
			Body = 3,
			Speed = 5,
			Symbol = 's',
			Color = Color.Purple,
			Sight = 5,
		}, 7, 9);

		public static MonsterTemplate ChaosDisciple = new MonsterTemplate(new Creature
		{
			Name = "chaos disciple",
			Attack = 8,
			Defense = 2,
			Mind = 6,
			Body = 6,
			Speed = 5,
			Symbol = 'x',
			Color = Color.Magenta,
			Sight = 5,
		}, 8, 10);

		public static MonsterTemplate ChaosLord = new MonsterTemplate(new Creature
		{
			Name = "chaos lord",
			Attack = 10,
			Defense = 5,
			Mind = 8,
			Body = 8,
			Speed = 5,
			Symbol = 'X',
			Color = Color.Magenta,
			Sight = 5,
		}, 10, 10);
	}
}
