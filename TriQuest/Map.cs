using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TriQuest
{
	/// <summary>
	/// The main game map.
	/// </summary>
	public class Map
	{
		public Map(int width, int height)
		{
			Width = width;
			Height = height;
			GenerateTerrain();
			PlaceHeroes();
			PlaceMonsters();
		}

		public const int MaxDangerLevel = 10;
		public const int DangerHotspotTightness = 50;
		public const int DangerHotspotCount = 50;
		public const int MonsterRarity = 20;

		public int Width { get; private set; }

		public int Height { get; private set; }

		public Tile[,] Tiles { get; private set; }

		public Formation Heroes { get; private set; }
		public int HeroX { get; private set; }
		public int HeroY { get; private set; }

		private void GenerateTerrain()
		{
			// initialize tiles
			Tiles = new Tile[Width, Height];
			for (int x = 0; x < Width; x++)
			{
				for (int y = 0; y < Height; y++)
				{
					Tiles[x, y] = new Tile();
				}
			}

			// assign danger hotspots
			for (int i = 0; i < DangerHotspotCount; i++)
			{
				PlaceHotspot(Dice.Range(0, Width - 1), Dice.Range(0, Height - 1), Dice.Range(1, MaxDangerLevel - 1));
			}

			// assign "final boss" danger hotspot
			PlaceHotspot(Dice.Range(0, Width - 1), Dice.Range(0, Height - 1), MaxDangerLevel);

			// assign terrain
			for (int x = 0; x < Width; x++)
			{
				for (int y = 0; y < Height; y++)
				{
					Tiles[x, y].Terrain = Terrain.Pick(Tiles[x, y].DangerLevel);
				}
			}
		}

		private void PlaceHotspot(int x, int y, int danger)
		{
			var dangerGradientSize = (int)(Math.Sqrt(Width * Height) / DangerHotspotTightness);
			if (CoordsInBounds(x, y))
			{
				Tiles[x, y].DangerLevel = Math.Max(danger, Tiles[x, y].DangerLevel);
				Tiles[x, y].Formation = Formation.SpawnMonsters(danger);
			}
			for (int dx = 0; dx < danger * dangerGradientSize; dx++)
			{
				for (int dy = 0; dy < danger * dangerGradientSize; dy++)
				{
					if (CoordsInBounds(x + dx, y + dy))
						Tiles[x + dx, y + dy].DangerLevel = Math.Max(danger - dx / dangerGradientSize - dy / dangerGradientSize, Tiles[x + dx, y + dy].DangerLevel);
					if (CoordsInBounds(x + dx, y - dy))
						Tiles[x + dx, y - dy].DangerLevel = Math.Max(danger - dx / dangerGradientSize - dy / dangerGradientSize, Tiles[x + dx, y - dy].DangerLevel);
					if (CoordsInBounds(x - dx, y + dy))
						Tiles[x - dx, y + dy].DangerLevel = Math.Max(danger - dx / dangerGradientSize - dy / dangerGradientSize, Tiles[x - dx, y + dy].DangerLevel);
					if (CoordsInBounds(x - dx, y - dy))
						Tiles[x - dx, y - dy].DangerLevel = Math.Max(danger - dx / dangerGradientSize - dy / dangerGradientSize, Tiles[x - dx, y - dy].DangerLevel);
				}
			}
		}

		private void PlaceHeroes()
		{
			Heroes = new Formation();
			Heroes.CreaturePositions[RelativePosition.Front] = Creature.Warrior;
			Heroes.CreaturePositions[RelativePosition.RearLeft] = Creature.Mage;
			Heroes.CreaturePositions[RelativePosition.RearRight] = Creature.Priest;

			var lowDanger = Tiles.Cast<Tile>().Where(t => t.DangerLevel == 1);
			lowDanger.Pick().Formation = Heroes;
			bool foundHeroes = false;
			for (var x = 0; x < Width; x++)
			{
				for (var y = 0; y < Height; y++)
				{
					if (Tiles[x, y].Formation == Heroes)
					{
						HeroX = x;
						HeroY = y;
						foundHeroes = true;
						break;
					}
				}
				if (foundHeroes)
					break;
			}

			for (var x = 0; x < Width; x++)
			{
				for (var y = 0; y < Height; y++)
				{
					if (TestLineOfSight(HeroX, HeroY, x, y))
						Tiles[x, y].HasBeenSeen = true;
				}
			}
		}

		private void PlaceMonsters()
		{
			for (var i = 0; i < Width * Height / MonsterRarity; i++)
			{
				var tile = Tiles.Cast<Tile>().Where(t => t.Formation == null && t.DangerLevel > 0).Pick();
				tile.Formation = Formation.SpawnMonsters(tile.DangerLevel);
			}
		}

		public bool CoordsInBounds(int x, int y)
		{
			return x >= 0 && x < Width && y >= 0 && y < Height;
		}

		public void Move(Formation f, Direction dir)
		{
			// find formation
			bool found = false;
			int fx = -99, fy = -99;
			for (var x = 0; x < Width; x++)
			{
				for (var y = 0; y < Height; y++)
				{
					if (Tiles[x, y].Formation == f)
					{
						fx = x;
						fy = y;
						found = true;
						break;
					}
				}
				if (found)
					break;
			}

			if (CoordsInBounds(fx + dir.DeltaX, fy + dir.DeltaY))
			{
				var targetTile = Tiles[fx + dir.DeltaX, fy + dir.DeltaY];
				if (targetTile.Formation == null)
				{
					// move
					Tiles[fx, fy].Formation = null;
					targetTile.Formation = f;
					if (f == Heroes)
					{
						HeroX = fx + dir.DeltaX;
						HeroY = fy + dir.DeltaY;

						// pick up items
						foreach (var item in targetTile.Items.Values)
							item.Found(Heroes);
						targetTile.Items.Clear();

						// update fog of war
						for (var x = 0; x < Width; x++)
						{
							for (var y = 0; y < Height; y++)
							{
								if (TestLineOfSight(HeroX, HeroY, x, y))
									Tiles[x, y].HasBeenSeen = true;
							}
						}

					}
				}
				else
				{
					// fight
					Fight(Tiles[fx, fy], targetTile);
				}

				// spend time
				f.Act(Tiles[fx + dir.DeltaX, fy + dir.DeltaY].Terrain.MovementCost);
			}
		}

		public void MoveOrTurn(Formation f, Direction dir)
		{
			// find formation
			bool found = false;
			int fx = -99, fy = -99;
			for (var x = 0; x < Width; x++)
			{
				for (var y = 0; y < Height; y++)
				{
					if (Tiles[x, y].Formation == f)
					{
						fx = x;
						fy = y;
						found = true;
						break;
					}
				}
				if (found)
					break;
			}

			if (f.Facing == dir)
				Move(f, dir);
			else
			{
				f.Facing = dir;
				f.Act(Tiles[fx, fy].Terrain.MovementCost);
			}
		}

		private void Fight(Tile attackerTile, Tile defenderTile)
		{
			var attackers = attackerTile.Formation;
			var defenders = defenderTile.Formation;

			if (attackers == null || defenders == null)
				return;

			int tileSize = 3;
			foreach (var kvp in attackers.CreaturePositions)
			{
				// who is attacking now?
				var attacker = attackers.CreaturePositions[kvp.Key];

				// is attacker blocked behind another attacker? then he can't attack
				bool blocked = false;
				RelativePosition pos = kvp.Key;
				while (true)
				{
					var apos = pos.AsAbsolute(attackers.Facing);
					apos = AbsolutePosition.FromCoordinates(apos.Column + attackers.Facing.DeltaX, apos.Row + attackers.Facing.DeltaY);
					if (apos == null)
						break;
					pos = apos.RelativeTo(attackers.Facing);
					if (pos != kvp.Key && attackers.CreaturePositions.ContainsKey(pos))
					{
						Log.Append("The " + attacker.Name + " is blocked by the " + attackers.CreaturePositions[pos].Name + ".");
						blocked = true;
						break;
					}
				}
				if (blocked)
					continue;

				// find nearest target
				Creature defender = null;
				int distance;
				var absPos = kvp.Key.AsAbsolute(attackers.Facing);
				AbsolutePosition targetPos;
				if (attackers.Facing == Direction.North)
				{
					distance = absPos.Row;
					targetPos = AbsolutePosition.FromCoordinates(absPos.Column, tileSize - 1);
				}
				else if (attackers.Facing == Direction.South)
				{
					distance = tileSize - 1 - absPos.Row;
					targetPos = AbsolutePosition.FromCoordinates(absPos.Column, 0);
				}
				else if (attackers.Facing == Direction.East)
				{
					distance = tileSize - 1 - absPos.Column;
					targetPos = AbsolutePosition.FromCoordinates(0, absPos.Column);
				}
				else if (attackers.Facing == Direction.West)
				{
					distance = absPos.Column;
					targetPos = AbsolutePosition.FromCoordinates(tileSize - 1, absPos.Column);
				}
				else
					throw new Exception("Invalid formation facing, not north/south/east/west");
				foreach (var distanceGroup in AbsolutePosition.All.GroupBy(pos2 => targetPos.DistanceTo(pos2)))
				{

					var matches = distanceGroup.Where(pos2 =>
						{
							var relpos = pos2.RelativeTo(defenders.Facing);
							return defenders.CreaturePositions.ContainsKey(relpos) && defenders.CreaturePositions[relpos] != null;
						});
					if (matches.Any())
					{
						// multiple targets at same distance? pick one at random!
						defender = defenders.CreaturePositions[matches.Pick().RelativeTo(defenders.Facing)];
						distance += distanceGroup.Key;
						break;
					}
				}

				// attaaaaack!
				if (defender == null)
					Log.Append("The " + attacker.Name + " has no one to attack."); // everyone's already dead!
				else
				{
					var attack = attacker.Attack;
					var defense = defender.Defense;
					var atkBody = attacker.Body;
					var atkMind = attacker.Mind;
					var defBody = defender.Body;
					var defMind = defender.Mind;
					int atkStat, defStat, range;
					string atkText;
					if (atkBody - defBody >= atkMind - defMind)
					{
						// use physical attack
						atkStat = atkBody;
						defStat = defBody;
						atkText = attacker.PhysicalAttackText;
						range = attacker.PhysicalAttackRange;
					}
					else
					{
						// use mental attack
						atkStat = atkMind;
						defStat = defMind;
						atkText = attacker.MentalAttackText;
						range = attacker.MentalAttackRange;
					}
					var color = defenders == Heroes ? Color.Yellow : Color.White;

					// accuracy penalty: 1/3 chance to miss for each subtile out of range
					bool miss = false;
					for (int i = 0; i < distance - range; i++)
					{
						if (Dice.Range(1, 3) == 1)
						{
							miss = true;
							break;
						}
					}

					string msg;
					if (miss)
					{
						color = Color.Gray;
						msg = "The " + attacker.Name + " " + atkText + " the " + defender.Name + ", but misses (attack range " + range + ", target distance " + distance + ").";
					}
					else
					{
						var atkRoll = Dice.Roll(attack, atkStat);
						var defRoll = Dice.Roll(defense, defStat);
						var damage = Math.Max(0, atkRoll - defRoll);
						msg = "The " + attacker.Name + " " + atkText + " the " + defender.Name + " (" + attack + "d" + atkStat + " vs. " + defense + "d" + defStat + ") for " + damage + " damage.";
						defender.Health -= damage;
						if (defender.Health <= 0)
						{
							msg += " The " + defender.Name + " is slain!";
							if (defenders == Heroes)
								color = Color.Red;
							foreach (var p in RelativePosition.All)
							{
								if (defenders.CreaturePositions.ContainsKey(p) && defenders.CreaturePositions[p] == defender)
								{
									KillCreature(defenderTile, p); // he's dead, Jim...
								}
							}
							if (!defenders.CreaturePositions.Any())
								defenderTile.Formation = null; // they're all dead, Dave...
						}
					}
					Log.Append(msg, color);
				}
			}
		}

		public static void KillCreature(Tile tile, RelativePosition p)
		{
			var creature = tile.Formation.CreaturePositions[p];
			if (Dice.Range(0.0, 1.0) < creature.ItemDropChance)
			{
				Item item;
				if (creature.ItemDropType == ItemType.Armor)
					item = Armor.All.Pick();
				else if (creature.ItemDropType == ItemType.Consumable)
					item = Consumable.All.Pick();
				else if (creature.ItemDropType == ItemType.Weapon)
					item = Weapon.All.Pick();
				else
					throw new Exception("Invalid item drop type, must be armor/consumable/weapon.");
				var msg = "The " + creature.Name + " drops a " + item.Name + ".";
				var apos = p.AsAbsolute(tile.Formation.Facing);
				if (tile.Items.ContainsKey(apos) && tile.Items[apos] != null)
					msg += " The " + tile.Items[apos].Name + " is crushed underneath.";
				Log.Append(msg, Color.Cyan);
				tile.Items[apos] = item;
			}
			tile.Formation.CreaturePositions.Remove(p);
		}

		/// <summary>
		/// Tests whether the formation in the source tile can see the destination.
		/// </summary>
		/// <returns></returns>
		public bool TestLineOfSight(int sourceX, int sourceY, int destX, int destY)
		{
			if (!CoordsInBounds(sourceX, sourceY) || !CoordsInBounds(destX, destY))
				return false;
			var f = Tiles[sourceX, sourceY].Formation;
			if (f == null)
				return false;
			var dist = Math.Abs(destX - sourceX) + Math.Abs(destY - sourceY);
			return dist <= f.Sight;
		}

		public void LetMonstersAct()
		{
			// let time pass
			var time = Heroes.Delay;
			foreach (var t in Tiles)
			{
				if (t.Formation != null)
					t.Formation.ElapseTime(time);
			}

			// let monsters act
			IEnumerable<Formation> actors;
			do
			{
				actors = Tiles.Cast<Tile>().Select(t => t.Formation).Where(f => f != null && f.Delay <= 0 && f != Heroes).OrderBy(f => f.Delay);
				foreach (var f in actors)
				{
					int fx = -99, fy = -99;

					// find location
					for (var x = 0; x < Width; x++)
					{
						for (var y = 0; y < Height; y++)
						{
							if (Tiles[x, y].Formation == f)
							{
								fx = x;
								fy = y;
								break;
							}
						}
						if (fx >= 0 && fy >= 0)
							break;
					}

					// test LoS to heroes
					if (TestLineOfSight(fx, fy, HeroX, HeroY))
					{
						// pursue heroes
						var dx = HeroX - fx;
						var dy = HeroY - fy;
						if (Math.Abs(dx) > Math.Abs(dy))
						{
							if (dx > 0)
								MoveOrTurn(f, Direction.East);
							else
								MoveOrTurn(f, Direction.West);
						}
						else if (Math.Abs(dx) < Math.Abs(dy))
						{
							if (dy > 0)
								MoveOrTurn(f, Direction.South);
							else
								MoveOrTurn(f, Direction.North);
						}
						else
						{
							if (Dice.Range(0, 1) == 0)
							{
								if (dx > 0)
									MoveOrTurn(f, Direction.East);
								else if (dx < 0)
									MoveOrTurn(f, Direction.West);
							}
							else
							{
								if (dy > 0)
									MoveOrTurn(f, Direction.South);
								else if (dy < 0)
									MoveOrTurn(f, Direction.North);
							}
						}
					}
					else
						f.Pass();
				}
			} while (actors.Count() > 0);

			// see if heroes are still alive
			bool found = false;
			HeroX = -99;
			HeroY = -99;
			for (var x = 0; x < Width; x++)
			{
				for (var y = 0; y < Height; y++)
				{
					if (Tiles[x, y].Formation == Heroes)
					{
						HeroX = x;
						HeroY = y;
						found = true;
						break;
					}
				}
				if (found)
					break;
			}
		}
	}
}
