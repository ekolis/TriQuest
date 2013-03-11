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
			var warrior = new Creature
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
			};
			var mage = new Creature
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
			};
			var priest = new Creature
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
			};

			Heroes = new Formation();
			Heroes.CreaturePositions[RelativePosition.Front] = warrior;
			Heroes.CreaturePositions[RelativePosition.RearLeft] = mage;
			Heroes.CreaturePositions[RelativePosition.RearRight] = priest;

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

			// TODO - update fog of war
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
				// TODO - check for collisions (fighting)
				Tiles[fx, fy].Formation = null;
				Tiles[fx + dir.DeltaX, fy + dir.DeltaY].Formation = f;

				// spend time
				f.Act(Tiles[fx + dir.DeltaX, fy + dir.DeltaY].Terrain.MovementCost);

				if (f == Heroes)
				{
					HeroX = fx + dir.DeltaX;
					HeroY = fy + dir.DeltaY;
				}
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

		/// <summary>
		/// Tests whether the formation in the source tile can see the destination.
		/// </summary>
		/// <returns></returns>
		public bool TestLineOfSight(int sourceX, int sourceY, int destX, int destY)
		{
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
