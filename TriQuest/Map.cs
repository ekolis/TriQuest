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
		}

		public const int MaxDangerLevel = 10;
		public const int DangerHotspotTightness = 50;
		public const int DangerHotspotCount = 50;

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
				Tiles[x, y].DangerLevel = Math.Max(danger, Tiles[x, y].DangerLevel);
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

		private bool CoordsInBounds(int x, int y)
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
				if (f == Heroes)
				{
					HeroX = fx + dir.DeltaX;
					HeroY = fy + dir.DeltaY;
				}
			}
		}
	}
}
