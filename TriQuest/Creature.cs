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
			BerserkTimer -= actionCost / Speed;
			if (BerserkTimer <= 0)
				StopBerserk();
		}

		public double BerserkTimer { get; private set; }
		public bool IsBerserk { get; private set; }

		/// <summary>
		/// Starts berserk mode.
		/// </summary>
		/// <param name="time">How long to be berserk?</param>
		public void StartBerserk(int time)
		{
			if (!IsBerserk)
			{
				Attack += 1;
				Defense -= 1;
				Body += 1;
				Mind -= 1;
			}
			BerserkTimer += time;
			IsBerserk = true;
		}

		/// <summary>
		/// Stops berserk mode.
		/// </summary>
		public void StopBerserk()
		{
			if (IsBerserk)
			{
				Attack -= 1;
				Defense += 1;
				Body -= 1;
				Mind += 1;
			}
			BerserkTimer = 0;
			IsBerserk = false;
		}

		/// <summary>
		/// Has this creature been slowed already? If so, it's immune to further slowing.
		/// </summary>
		public bool HasBeenSlowed { get; set; }

		public static readonly Creature Warrior = new Creature
			{
				Name = "warrior",
				Attack = 7,
				Defense = 6,
				Mind = 1,
				Body = 6,
				Speed = 5,
				Symbol = '@',
				Color = Color.Red,
				Level = 1,
				Sight = 5,
				PhysicalAttackText = "slashes",
				PhysicalAttackRange = 1,
				MentalAttackText = "ki-blasts",
				MentalAttackRange = 2,
			};

		public static readonly Creature Mage = new Creature
			{
				Name = "mage",
				Attack = 6,
				Defense = 2,
				Mind = 10,
				Body = 2,
				Speed = 5,
				Symbol = '@',
				Color = Color.Blue,
				Level = 1,
				Sight = 5,
				PhysicalAttackText = "bashes",
				PhysicalAttackRange = 2,
				MentalAttackText = "casts a magic missile at",
				MentalAttackRange = 3,
			};

		public static readonly Creature Priest = new Creature
			{
				Name = "priest",
				Attack = 5,
				Defense = 4,
				Mind = 7,
				Body = 4,
				Speed = 5,
				Symbol = '@',
				Color = Color.Green,
				Level = 1,
				Sight = 5,
				PhysicalAttackText = "smites",
				PhysicalAttackRange = 1,
				MentalAttackText = "casts Cause Wounds at",
				MentalAttackRange = 3,
			};

		static Creature()
		{
			Warrior.Skills.Add(new Skill
			{
				Name = "Berserk",
				Verb = "goes",
				Description = "Temporarily increases attack and body at the expense of defense and mind.",
				ManaCost = 10,
				Use = (user, us, target, map) =>
				{
					user.StartBerserk(10);
					Log.Append("RAAAAR! Warrior SMASH!");
				},
			});
			Warrior.Skills.Add(new Skill
			{

				Name = "Omnislash",
				Verb = "uses",
				Description = "Hits all enemies in the tile directly ahead with a physical attack.",
				ManaCost = 20,
				Use = (user, us, target, map) =>
					{
						if (target == null || target.Formation == null)
							Log.Append("But there's no one ahead to slash!");
						else
						{
							foreach (var defender in target.Formation.CreaturePositions.Values)
							{
								var atkRoll = Dice.Roll(user.Attack, user.Body);
								var defRoll = Dice.Roll(user.Defense, defender.Body);
								var damage = Math.Max(0, atkRoll - defRoll);
								var msg = "Hits the " + defender.Name + " (" + user.Attack + "d" + user.Body + " vs. " + defender.Defense + "d" + defender.Body + ") for " + damage + " damage.";
								defender.Health -= damage;
								if (defender.Health <= 0)
								{
									msg += " The " + defender.Name + " is slain!";
									foreach (var p in RelativePosition.All)
									{
										if (target.Formation.CreaturePositions.ContainsKey(p) && target.Formation.CreaturePositions[p] == defender)
											target.Formation.CreaturePositions.Remove(p); // he's dead, Jim...
									}
									if (!target.Formation.CreaturePositions.Any())
										target.Formation = null; // they're all dead, Dave...
								}
								Log.Append(msg);
							}
						}
					},
			});



			Mage.Skills.Add(new Skill
			{
				Name = "Fireball",
				Verb = "casts",
				Description = "Hits all enemies in the tile directly ahead with a magical attack.",
				ManaCost = 10,
				Use = (user, us, target, map) =>
				{
					if (target == null || target.Formation == null)
						Log.Append("But there's no one ahead to blast!");
					else
					{
						foreach (var defender in target.Formation.CreaturePositions.Values)
						{
							var atkRoll = Dice.Roll(user.Attack, user.Mind);
							var defRoll = Dice.Roll(user.Defense, defender.Mind);
							var damage = Math.Max(0, atkRoll - defRoll);
							var msg = "Hits the " + defender.Name + " (" + user.Attack + "d" + user.Body + " vs. " + defender.Defense + "d" + defender.Body + ") for " + damage + " damage.";
							defender.Health -= damage;
							if (defender.Health <= 0)
							{
								msg += " The " + defender.Name + " is slain!";
								foreach (var p in RelativePosition.All)
								{
									if (target.Formation.CreaturePositions.ContainsKey(p) && target.Formation.CreaturePositions[p] == defender)
										target.Formation.CreaturePositions.Remove(p); // he's dead, Jim...
								}
								if (!target.Formation.CreaturePositions.Any())
									target.Formation = null; // they're all dead, Dave...
							}
							Log.Append(msg);
						}
					}
				},
			});
			Mage.Skills.Add(new Skill
			{
				Name = "Slow",
				Verb = "casts",
				Description = "Reduces the speed of a group of enemies. Not cumulative over multiple casts.",
				ManaCost = 20,
				Use = (user, us, target, map) =>
				{
					if (target == null || target.Formation == null)
						Log.Append("But there's no one ahead to slow!");
					else
					{
						foreach (var defender in target.Formation.CreaturePositions.Values)
						{
							if (!defender.HasBeenSlowed)
							{
								defender.Speed /= 2;
								Log.Append("The " + defender.Name + " seems more sluggish.");
								defender.HasBeenSlowed = true;
							}
							else
								Log.Append("The " + defender.Name + " is already slow.");
						}
					}
				},
			});

			Priest.Skills.Add(new Skill
			{
				Name = "Heal Wounds",
				Verb = "casts",
				Description = "Heals some of the HP of the party. Tougher party members are easier to heal.",
				ManaCost = 10,
				Use = (user, us, target, map) =>
				{
					foreach (var hero in us.CreaturePositions.Values)
					{
						var healing = Dice.Roll(user.Mind, hero.Body);
						var msg = "Heals the " + hero.Name + " (" + user.Mind + "d" + hero.Body + ") for " + healing + " HP.";
						hero.Health += healing;
						if (hero.Health > Creature.MaxHealth)
						{
							hero.Health = Creature.MaxHealth;
						}
						Log.Append(msg);
					}

				},
			});
			Priest.Skills.Add(new Skill
			{
				Name = "Banish",
				Verb = "casts",
				Description = "Teleports a group of enemies to a random location.",
				ManaCost = 20,
				Use = (user, us, target, map) =>
				{
					if (target == null || target.Formation == null)
						Log.Append("But there's no one ahead to banish!");
					else
					{
						var locations = map.Tiles.Cast<Tile>().Where(t => t.Formation == null);
						locations.Pick().Formation = target.Formation;
						target.Formation = null;
						Log.Append("The enemy disappear in a white flash!");
					}
				},
			});
		}
	}
}
