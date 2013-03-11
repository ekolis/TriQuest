using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TriQuest
{
	public partial class MainForm : Form
	{
		public MainForm(Map map)
		{
			InitializeComponent();
			this.map = map;
		}

		private Map map;
		private static Dictionary<Color, SolidBrush> brushes = new Dictionary<Color, SolidBrush>();

		private void picMap_Paint(object sender, PaintEventArgs e)
		{
			if (map == null)
				return;

			var g = e.Graphics;
			int subTiles = 3;
			int border = 1;
			var xsize = picMap.Width / (subTiles + border * 2) / (map.Heroes.Sight * 2 + 1);
			var ysize = picMap.Height / (subTiles + border * 2) / (map.Heroes.Sight * 2 + 1);
			int charSize = Math.Min(xsize, ysize);
			var font = new Font("Lucida Console", charSize);

			// center on heroes
			var dx = -map.HeroX * charSize * (subTiles + border * 2) + picMap.Width / 2 - (charSize * subTiles + border * 2) / 2;
			var dy = -map.HeroY * charSize * (subTiles + border * 2) + picMap.Height / 2 - (charSize * subTiles + border * 2) / 2;

			g.Clear(picMap.BackColor);
			for (int x = 0; x < map.Width; x++)
			{
				for (int y = 0; y < map.Height; y++)
				{
					if (map.TestLineOfSight(map.HeroX, map.HeroY, x, y))
						DrawTileFull(map.Tiles[x, y], g, font, border, dx + x * charSize * (subTiles + border * 2), dy + y * charSize * (subTiles + border * 2));
				}
			}
		}

		private void DrawTileFull(Tile tile, Graphics g, Font font, int border, int x, int y)
		{
			foreach (var pos in AbsolutePosition.All)
				DrawSubtile(tile, g, font, border, x, y, pos);
			var charSize = font.Size;
			if (tile.Formation != null)
			{
				// draw facing arrows
				int subTiles = 3;
				var facing = tile.Formation.Facing;
				if (facing == Direction.North)
					g.DrawString("^", font, GetBrush(Color.White), new PointF(x + charSize * subTiles / 2 + 0.5f * charSize, y));
				else if (facing == Direction.South)
					g.DrawString("v", font, GetBrush(Color.White), new PointF(x + charSize * subTiles / 2 + 0.5f * charSize, y + charSize * (border + subTiles)));
				else if (facing == Direction.East)
					g.DrawString(">", font, GetBrush(Color.White), new PointF(x + charSize * (border + subTiles), y + charSize * subTiles / 2 + 0.5f * charSize));
				else if (facing == Direction.West)
					g.DrawString("<", font, GetBrush(Color.White), new PointF(x, y + charSize * subTiles / 2 + 0.5f * charSize));
			}
		}

		private void DrawTileSimple(Tile tile, Graphics g, Font font, int x, int y)
		{
			g.DrawString(tile.Symbol.ToString(), font, GetBrush(tile.Color), new PointF(x, y));
		}

		private void DrawTileSimpleFogged(Tile tile, Graphics g, Font font, int x, int y)
		{
			g.DrawString(tile.Terrain.Symbol.ToString(), font, GetBrush(tile.Terrain.Color), new PointF(x, y));
		}

		private void DrawSubtile(Tile tile, Graphics g, Font font, int border, int x, int y, AbsolutePosition pos)
		{
			var charSize = font.Size;
			g.DrawString(tile.GetSymbol(pos).ToString(), font, GetBrush(tile.GetColor(pos)), new PointF(x + charSize * (pos.Column + border), y + charSize * (pos.Row + border)));
		}

		private static SolidBrush GetBrush(Color color)
		{
			if (!brushes.ContainsKey(color))
				brushes[color] = new SolidBrush(color);
			return brushes[color];
		}

		private void MainForm_Resize(object sender, EventArgs e)
		{
			picMap.Invalidate();
		}

		private void picMinimap_Paint(object sender, PaintEventArgs e)
		{
			if (map == null)
				return;

			var g = e.Graphics;
			int charSize = Math.Min(picMinimap.Width / map.Width, picMinimap.Height / map.Height);
			var font = new Font("Lucida Console", charSize);

			g.Clear(picMinimap.BackColor);
			for (int x = 0; x < map.Width; x++)
			{
				for (int y = 0; y < map.Height; y++)
				{
					if (map.TestLineOfSight(map.HeroX, map.HeroY, x, y))
						DrawTileSimple(map.Tiles[x, y], g, font, x * charSize, y * charSize);
					else if (map.Tiles[x,y].HasBeenSeen)
						DrawTileSimpleFogged(map.Tiles[x, y], g, font, x * charSize, y * charSize);
				}
			}
		}

		private void MainForm_KeyDown(object sender, KeyEventArgs e)
		{
			var dir = Direction.FromKey(e.KeyCode);
			if (dir != null)
			{
				if (ModifierKeys.HasFlag(Keys.Shift))
				{
					// shift-arrow moves without rotating (strafe move)
					map.Move(map.Heroes, dir);
				}
				else
				{
					// just plain arrow attempts to rotate instead of move if not facing direction of movement
					map.MoveOrTurn(map.Heroes, dir);
				}
				map.LetMonstersAct();
				if (!map.CoordsInBounds(map.HeroX, map.HeroY))
				{
					MessageBox.Show("Oh no! The heroes have fallen...");
					Application.Exit();
				}
				picMap.Invalidate();
				picMinimap.Invalidate();
			}
		}
	}
}
