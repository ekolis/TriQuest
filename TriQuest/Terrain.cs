using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TriQuest
{
	/// <summary>
	/// A terrain, such as grass, desert, or forest.
	/// </summary>
	public class Terrain
	{
		private Terrain(string name, char symbol, Color color, int movementCost, bool blocksLineOfSight)
		{
			Name = name;
			Symbol = symbol;
			Color = color;
			MovementCost = movementCost;
			BlocksLineOfSight = blocksLineOfSight;
		}

		public static readonly Terrain Grass = new Terrain("grass", '.', Color.Green, 1, false);
		public static readonly Terrain Forest = new Terrain("forest", '#', Color.Green, 2, true);
		public static readonly Terrain Rocks = new Terrain("rocks", '*', Color.Gray, 2, false);
		public static readonly Terrain Desert = new Terrain("desert", '.', Color.Beige, 1, false);
		public static readonly Terrain Water = new Terrain("water", '~', Color.Blue, 3, false);
		public static readonly Terrain Wasteland = new Terrain("wasteland", '+', Color.Gray, 1, false);

		public string Name {get; private set;}
		public char Symbol { get; private set; }
		public Color Color { get; private set; }
		public int MovementCost { get; private set; }
		public bool BlocksLineOfSight { get; private set; }

		public static Terrain Pick(int danger)
		{
			if (danger < 1)
				return Grass;
			if (danger < 4)
			{
				var num = Dice.Range(0.0, 1.0);
				if (num < 0.25)
					return Forest;
				else if (num < 0.5)
					return Water;
				return Grass;
			}
			if (danger < 7)
			{
				var num = Dice.Range(0.0, 1.0);
				if (num < 0.25)
					return Rocks;
				else if (num < 0.5)
					return Water;
				else if (num < 0.75)
					return Grass;
				return Desert;
			}
			if (danger < 10)
			{
				var num = Dice.Range(0.0, 1.0);
				if (num < 0.5)
					return Rocks;
				else if (num < 0.75)
					return Desert;
				return Wasteland;
			}
			return Wasteland;
		}
	}
}
