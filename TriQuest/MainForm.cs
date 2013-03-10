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
		private static int charSize = 8;
		private static Font font = new Font("Lucida Console", charSize);
		private static Dictionary<Color, SolidBrush> brushes = new Dictionary<Color, SolidBrush>();

		private void picMap_Paint(object sender, PaintEventArgs e)
		{
			if (map == null)
				return;

			var g = e.Graphics;
			int charSize = 8;
			int subTiles = 3;
			int border = 1;

			// center on heroes
			var dx = -map.HeroX * charSize * (subTiles + border * 2) + picMap.Width / 2;
			var dy = -map.HeroY * charSize * (subTiles + border * 2) + picMap.Height / 2;

			g.Clear(picMap.BackColor);
			for (int x = 0; x < map.Width; x++)
			{
				for (int y = 0; y < map.Height; y++)
				{
					DrawTile(map.Tiles[x, y], g, border, dx + x * charSize * (subTiles + border * 2), dy + y * charSize * (subTiles + border * 2));
				}
			}
		}

		private void DrawTile(Tile tile, Graphics g, int border, int x, int y)
		{
			foreach (var pos in AbsolutePosition.All)
				DrawSubtile(tile, g, border, x, y, pos);
			if (tile.Formation != null)
			{
				// draw facing arrows
				int subTiles = 3;
				var facing = tile.Formation.Facing;
				if (facing == Direction.North)
					g.DrawString("^", font, GetBrush(Color.White), new PointF(x + charSize * subTiles / 2, y));
				else if (facing == Direction.South)
					g.DrawString("v", font, GetBrush(Color.White), new PointF(x + charSize * subTiles / 2, y + charSize * (border + subTiles)));
				else if (facing == Direction.East)
					g.DrawString(">", font, GetBrush(Color.White), new PointF(x + charSize * (border + subTiles), y + charSize * subTiles / 2));
				else if (facing == Direction.West)
					g.DrawString("<", font, GetBrush(Color.White), new PointF(x, y + charSize * subTiles / 2));
			}
		}

		private void DrawSubtile(Tile tile, Graphics g, int border, int x, int y, AbsolutePosition pos)
		{
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
	}
}
