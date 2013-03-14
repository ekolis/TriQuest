using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TriQuest
{
	/// <summary>
	/// An equippable or consumable item.
	/// </summary>
	public abstract class Item
	{
		/// <summary>
		/// The name of the item.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The symbol used to represent the item.
		/// </summary>
		public char Symbol { get; set; }

		/// <summary>
		/// The color used to represent the item.
		/// </summary>
		public Color Color { get; set; }

		public static IEnumerable<Item> All
		{
			get
			{
				return Equipment.All.Cast<Item>().Union(Consumable.All.Cast<Item>());
			}
		}

		/// <summary>
		/// What happens when this item is found by the heroes?
		/// </summary>
		public abstract void Found(Formation heroes);
	}

	/// <summary>
	/// An equippable item. Modifies the wearer's stats.
	/// </summary>
	public abstract class Equipment : Item
	{
		/// <summary>
		/// The hero who can wield/wear this item.
		/// </summary>
		public Creature Hero { get; set; }

		/// <summary>
		/// Has the party found this equipment yet?
		/// </summary>
		public bool HasBeenFound { get; set; }

		/// <summary>
		/// Attack modifier.
		/// </summary>
		public int Attack { get; set; }

		/// <summary>
		/// Defense modifier.
		/// </summary>
		public int Defense { get; set; }

		/// <summary>
		/// Body modifier.
		/// </summary>
		public int Body { get; set; }

		/// <summary>
		/// Mind modifier.
		/// </summary>
		public int Mind { get; set; }

		/// <summary>
		/// Speed modifier.
		/// </summary>
		public int Speed { get; set; }

		/// <summary>
		/// Sight modifier.
		/// </summary>
		public int Sight { get; set; }

		public string Description
		{
			get
			{
				var list = new List<string>();
				if (Attack != 0)
					list.Add("Attack" + PlusOrMinus(Attack));
				if (Defense != 0)
					list.Add("Defense" + PlusOrMinus(Defense));
				if (Body != 0)
					list.Add("Body" + PlusOrMinus(Body));
				if (Mind != 0)
					list.Add("Mind" + PlusOrMinus(Mind));
				if (Speed != 0)
					list.Add("Speed" + PlusOrMinus(Speed));
				if (Sight != 0)
					list.Add("Sight" + PlusOrMinus(Sight));
				return string.Join(", ", list.ToArray());
			}
		}

		private string PlusOrMinus(int i)
		{
			if (i > 0)
				return "+" + i;
			else if (i == 0)
				return " " + i;
			else
				return i.ToString();
		}

		public static IEnumerable<Equipment> All
		{
			get
			{
				return Weapon.All.Cast<Equipment>().Union(Armor.All.Cast<Equipment>());
			}
		}

		public override void Found(Formation heroes)
		{
			HasBeenFound = true;
			Log.Append("The party finds a " + Name + ".");
		}
	}

	public class Weapon : Equipment
	{
		private static IEnumerable<Weapon> all = new Weapon[]
		{
			Shortsword,
			Longsword,
			Greatsword,
			BattleAxe,
			FireRod,
			IceRod,
			LightningRod,
			TempestRod,
			Mace,
			BoStaff,
			Talisman,
			DivinersRing,
		};

		public static IEnumerable<Weapon> All { get { return all; } }

		public static Weapon Shortsword = new Weapon
		{
			Name = "shortsword",
			Symbol = '/',
			Color = Color.Silver,
			Hero = Creature.Warrior,
			Attack = 2,
		};

		public static Weapon Longsword = new Weapon
		{
			Name = "longsword",
			Symbol = '\\',
			Color = Color.Silver,
			Hero = Creature.Warrior,
			Attack = 3,
			Defense = -1,
		};

		public static Weapon Greatsword = new Weapon
		{
			Name = "greatsword",
			Symbol = '|',
			Color = Color.Silver,
			Hero = Creature.Warrior,
			Attack = 4,
			Defense = -2,
		};

		public static Weapon BattleAxe = new Weapon
		{
			Name = "battle-axe",
			Symbol = '{',
			Color = Color.Silver,
			Hero = Creature.Warrior,
			Attack = 5,
			Defense = -3,
		};

		public static Weapon FireRod = new Weapon
		{
			Name = "fire rod",
			Symbol = '/',
			Color = Color.Red,
			Hero = Creature.Mage,
			Attack = 1,
			Mind = 2,
		};

		public static Weapon IceRod = new Weapon
		{
			Name = "ice rod",
			Symbol = '/',
			Color = Color.Cyan,
			Hero = Creature.Mage,
			Defense = 1,
			Mind = 2,
		};

		public static Weapon LightningRod = new Weapon
		{
			Name = "lightning rod",
			Symbol = '/',
			Color = Color.Yellow,
			Hero = Creature.Mage,
			Sight = 1,
			Mind = 2,
		};

		public static Weapon TempestRod = new Weapon
		{
			Name = "tempest rod",
			Symbol = '/',
			Color = Color.White,
			Hero = Creature.Mage,
			Attack = 1,
			Defense = 1,
			Sight = 1,
		};

		public static Weapon Mace = new Weapon
		{
			Name = "mace",
			Symbol = '\\',
			Color = Color.DarkGray,
			Hero = Creature.Priest,
			Attack = 2,
		};

		public static Weapon BoStaff = new Weapon
		{
			Name = "bo staff",
			Symbol = '\\',
			Color = Color.Brown,
			Hero = Creature.Priest,
			Attack = 1,
			Defense = 1,
		};

		public static Weapon Talisman = new Weapon
		{
			Name = "talisman",
			Symbol = '0',
			Color = Color.Yellow,
			Hero = Creature.Priest,
			Body = 1,
			Mind = 1,
		};

		public static Weapon DivinersRing = new Weapon
		{
			Name = "diviner's ring",
			Symbol = '=',
			Color = Color.Cyan,
			Hero = Creature.Priest,
			Sight = 3,
		};
	}

	public class Armor : Equipment
	{
		private static IEnumerable<Armor> all = new Armor[]
		{
			Chainmail,
			Platemail,
			CrystalArmor,
			WizardsRobe,
			RobeOfProtection,
			RobeOfWildMagic,
			Cassock,
			MonksRobe,
			LeatherCuirass,
		};

		public static IEnumerable<Armor> All { get { return all; } }

		public static Armor Chainmail = new Armor
		{
			Name = "chainmail",
			Symbol = '#',
			Color = Color.LightGray,
			Hero = Creature.Warrior,
			Defense = 2,
		};

		public static Armor Platemail = new Armor
		{
			Name = "platemail",
			Symbol = '#',
			Color = Color.Silver,
			Hero = Creature.Warrior,
			Defense = 3,
			Body = -1,
		};

		public static Armor CrystalArmor = new Armor
		{
			Name = "crystal armor",
			Symbol = '#',
			Color = Color.Cyan,
			Hero = Creature.Warrior,
			Defense = 4,
			Body = -2,
		};

		public static Armor WizardsRobe = new Armor
		{
			Name = "wizard's robe",
			Symbol = '#',
			Color = Color.Blue,
			Hero = Creature.Mage,
			Defense = 1,
			Mind = 1,
		};

		public static Armor RobeOfProtection = new Armor
		{
			Name = "robe of protection",
			Symbol = '#',
			Color = Color.Gray,
			Hero = Creature.Mage,
			Defense = 2,
		};

		public static Armor RobeOfWildMagic = new Armor
		{
			Name = "robe of wild magic",
			Symbol = '#',
			Color = Color.Red,
			Hero = Creature.Mage,
			Defense = -1,
			Attack = 1,
			Mind = 2,
		};

		public static Armor Cassock = new Armor
		{
			Name = "cassock",
			Symbol = '#',
			Color = Color.DarkGray,
			Hero = Creature.Priest,
			Defense = 1,
			Body = 1,
		};

		public static Armor MonksRobe = new Armor
		{
			Name = "monk's robe",
			Symbol = '#',
			Color = Color.Brown,
			Hero = Creature.Priest,
			Body = 1,
			Mind = 1,
			Defense = 1,
			Attack = -1,
		};

		public static Armor LeatherCuirass = new Armor
		{
			Name = "leather cuirass",
			Symbol = '#',
			Color = Color.Beige,
			Hero = Creature.Priest,
			Defense = 2,
		};
	}

	public class Consumable : Item
	{
		private static Consumable[] all = new Consumable[] { HealthPack, HealthPotion, ManaPack, ManaPotion };

		public static IEnumerable<Consumable> All { get { return all; } }

		public override void Found(Formation heroes)
		{
			Log.Append("The party finds a " + Name + ".");
			Action(heroes);
		}

		public Action<Formation> Action { get; set; }

		public static Consumable HealthPack = new Consumable
		{
			Name = "pack of small health potions",
			Symbol = '%',
			Color = Color.Red,
			Action = (heroes) =>
				{
					Log.Append("All the heroes regain 20 HP!");
					foreach (var hero in heroes.CreaturePositions.Values)
					{
						hero.Health += 20;
						if (hero.Health > Creature.MaxHealth)
							hero.Health = Creature.MaxHealth;
					}
				}
		};
		public static Consumable HealthPotion = new Consumable
		{
			Name = "large health potion",
			Symbol = '!',
			Color = Color.Red,
			Action = (heroes) =>
			{
				foreach (var hero in heroes.CreaturePositions.Values)
				{
					if (hero.Health == heroes.CreaturePositions.Values.Min(h => h.Health))
					{
						Log.Append("The " + hero.Name + " regains 60 HP!");
						hero.Health += 60;
						if (hero.Health > Creature.MaxHealth)
							hero.Health = Creature.MaxHealth;
						break;
					}
				}
			}
		};
		public static Consumable ManaPack = new Consumable
		{
			Name = "pack of small mana potions",
			Symbol = '%',
			Color = Color.Blue,
			Action = (heroes) =>
			{
				Log.Append("All the heroes regain 20 MP!");
				foreach (var hero in heroes.CreaturePositions.Values)
				{
					hero.Mana += 20;
					if (hero.Mana > Creature.MaxMana)
						hero.Mana = Creature.MaxMana;
				}
			}
		};
		public static Consumable ManaPotion = new Consumable
		{
			Name = "large mana potion",
			Symbol = '!',
			Color = Color.Red,
			Action = (heroes) =>
			{
				foreach (var hero in heroes.CreaturePositions.Values)
				{
					if (hero.Mana == heroes.CreaturePositions.Values.Min(h => h.Mana))
					{
						Log.Append("The " + hero.Name + " regains 60 MP!");
						hero.Health += 60;
						if (hero.Mana > Creature.MaxMana)
							hero.Mana = Creature.MaxMana;
						break;
					}
				}
			}
		};
	}
}
