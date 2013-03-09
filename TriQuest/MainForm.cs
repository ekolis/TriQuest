using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TriQuest
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private Map map;

		private void MainForm_Load(object sender, EventArgs e)
		{
			map = new Map(100, 100);
		}

		private void picMap_Paint(object sender, PaintEventArgs e)
		{
			if (map == null)
				return;

			var g = e.Graphics;
			int charSize = 12;
			for (int x = 0; x < map.Width; x++)
			{
				for (int y = 0; y < map.Height; y++)
				{
					DrawTile(map.Tiles[x, y], g, charSize, x * charSize, y * charSize);
				}
			}
		}

		private void DrawTile(Tile tile, Graphics g, int charSize, int x, int y)
		{
			g.DrawString(tile.Terrain.Symbol.ToString(), new Font("Sans Serif", charSize), new SolidBrush(tile.Terrain.Color), new PointF(x, y));
		}
	}
}
