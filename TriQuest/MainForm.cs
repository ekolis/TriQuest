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
					else if (map.Tiles[x, y].HasBeenSeen)
						DrawTileSimpleFogged(map.Tiles[x, y], g, font, x * charSize, y * charSize);
				}
			}
		}

		private int heroBeingPlaced = -1;
		private char mode = ' ';
		private Creature heroDoingStuff = null;
		private Dictionary<AbsolutePosition, Creature> newHeroPositions;
		private bool heroesDead = false;

		private void MainForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (heroesDead)
				return; // no doing stuff when you're dead!

			if (heroBeingPlaced >= 0)
			{
				if (e.KeyCode == Keys.F)
				{
					heroBeingPlaced = -1; // cancel
					Log.Append("Never mind, then.");
				}
				else
				{

					// place the hero
					var pos = AbsolutePosition.FromKey(e.KeyCode);
					if (pos == null)
						Log.Append("Use the numeric keypad to place the " + map.Heroes.CreaturePositions.Values.ElementAt(heroBeingPlaced).Name + " or press F to cancel.");
					else
					{
						if (newHeroPositions.Keys.Contains(pos))
							Log.Append("The " + newHeroPositions[pos].Name + " is already in the " + pos.Name + ". Try again or press F to cancel.");
						else
						{
							// set placement
							newHeroPositions[pos] = map.Heroes.CreaturePositions.Values.ElementAt(heroBeingPlaced);

							// move on to next hero, or be done if last hero was placed
							heroBeingPlaced++;
							if (heroBeingPlaced >= map.Heroes.CreaturePositions.Values.Count)
							{
								map.Heroes.CreaturePositions.Clear();
								foreach (var kvp in newHeroPositions)
									map.Heroes.CreaturePositions.Add(kvp.Key.RelativeTo(map.Heroes.Facing), kvp.Value);
								heroBeingPlaced = -1;
								Log.Append("The heroes rearrange their formation.");
								map.Heroes.Act(map.Tiles[map.HeroX, map.HeroY].Terrain.MovementCost);
								map.LetMonstersAct();
							}
							else
								Log.Append("Please choose a position for the " + map.Heroes.CreaturePositions.Values.ElementAt(heroBeingPlaced).Name + ".");
						}
					}
				}
			}
			else if (mode == 's')
			{
				if (heroDoingStuff != null)
				{
					if (e.KeyCode == Keys.S)
					{
						// cancel
						mode = ' ';
						heroDoingStuff = null;
						Log.Append("Never mind, then.");
					}
					else
					{

						// pick skill
						var skillIdx = GetNumberFromKey(e.KeyCode) - 1;
						var skillToUse = heroDoingStuff.Skills.ElementAtOrDefault(skillIdx);

						if (skillToUse == null)
						{
							// prompt for skill to use
							Log.Append("What skill will the " + heroDoingStuff.Name + " use? (press S again to cancel)");
							int skillNum = 0;
							foreach (var skill in heroDoingStuff.Skills)
							{
								skillNum++;
								Log.Append(skillNum + ": " + skill.Name + " (" + skill.ManaCost + " mana) - " + skill.Description);
							}
						}
						else if (skillToUse.ManaCost > heroDoingStuff.Mana)
						{
							// not enough mana, cancel
							Log.Append("The " + heroDoingStuff.Name + " lacks the mana for " + skillToUse.Name + "!");
							mode = ' ';
							heroDoingStuff = null;
							Log.Append("Never mind, then.");
						}
						else
						{
							// use skill
							Log.Append("The " + heroDoingStuff.Name + " " + skillToUse.Verb + " " + skillToUse.Name + "!");
							int x = map.HeroX + map.Heroes.Facing.DeltaX;
							int y = map.HeroY + map.Heroes.Facing.DeltaY;
							Tile target = null;
							if (map.CoordsInBounds(x, y))
								target = map.Tiles[x, y];
							skillToUse.Use(heroDoingStuff, map.Heroes, target, map);

							// spend mana
							heroDoingStuff.Mana -= skillToUse.ManaCost;

							// use time
							map.Heroes.Act(map.Tiles[map.HeroX, map.HeroY].Terrain.MovementCost);
							map.LetMonstersAct();

							// done using skill
							mode = ' ';
							heroDoingStuff = null;
						}
					}
				}
				else
				{
					if (e.KeyCode == Keys.S)
					{
						// cancel
						mode = ' ';
						heroDoingStuff = null;
						Log.Append("Never mind, then.");
					}
					else
					{
						// pick hero
						var heroIdx = GetNumberFromKey(e.KeyCode) - 1;
						heroDoingStuff = map.Heroes.CreaturePositions.Values.ElementAtOrDefault(heroIdx);
						if (heroDoingStuff == null)
						{
							// didn't pick a hero
							Log.Append("Who will use a skill? (press S again to cancel)");
							int heroNum = 0;
							foreach (var hero in map.Heroes.CreaturePositions.Values)
							{
								heroNum++;
								Log.Append(heroNum + ": " + hero.Name, hero.Color);
							}
						}
						else
						{
							// prompt for skill to use
							Log.Append("What skill will the " + heroDoingStuff.Name + " use? (press S again to cancel)");
							int skillNum = 0;
							foreach (var skill in heroDoingStuff.Skills)
							{
								skillNum++;
								Log.Append(skillNum + ": " + skill.Name + " (" + skill.ManaCost + " mana) - " + skill.Description);
							}
						}
					}
				}
			}
			else if (mode == 'w')
			{
				if (heroDoingStuff != null)
				{
					if (e.KeyCode == Keys.W)
					{
						// cancel
						mode = ' ';
						heroDoingStuff = null;
						Log.Append("Never mind, then.");
					}
					else
					{
						// pick weapon
						var idx = GetNumberFromKey(e.KeyCode) - 1;
						var available = Weapon.All.Where(w => w.Hero == heroDoingStuff && w.HasBeenFound);
						var weapon = available.ElementAtOrDefault(idx);

						if (weapon == null)
						{
							if (!available.Any())
							{
								// nothing to equip
								Log.Append("The " + heroDoingStuff.Name + " doesn't have any weapons to equip!");
								mode = ' ';
								heroDoingStuff = null;
							}
							else
							{
								// prompt for weapon to equip
								Log.Append("What weapon will the " + heroDoingStuff.Name + " equip? (press W again to cancel)");
								int num = 0;
								foreach (var w in available)
								{
									num++;
									Log.Append(num + ": " + w.Name + " (" + w.Description + ")");
								}
							}
						}
						else
						{
							if (heroDoingStuff.Weapon == weapon)
							{
								// nothing to do
								Log.Append("But the " + heroDoingStuff.Name + " has already equipped the " + weapon.Name + "!"); mode = ' ';
								heroDoingStuff = null;
							}
							else
							{
								// equip weapon
								Log.Append("The " + heroDoingStuff.Name + " equips the " + weapon.Name + ".");
								heroDoingStuff.Weapon = weapon;

								// use time
								map.Heroes.Act(map.Tiles[map.HeroX, map.HeroY].Terrain.MovementCost);
								map.LetMonstersAct();

								// done equipping
								mode = ' ';
								heroDoingStuff = null;
							}
						}
					}
				}
				else
				{
					if (e.KeyCode == Keys.W)
					{
						// cancel
						mode = ' ';
						heroDoingStuff = null;
						Log.Append("Never mind, then.");
					}
					else
					{
						// pick hero
						var heroIdx = GetNumberFromKey(e.KeyCode) - 1;
						heroDoingStuff = map.Heroes.CreaturePositions.Values.ElementAtOrDefault(heroIdx);
						if (heroDoingStuff == null)
						{
							// didn't pick a hero
							Log.Append("Who will equip a weapon? (press W again to cancel)");
							int heroNum = 0;
							foreach (var hero in map.Heroes.CreaturePositions.Values)
							{
								heroNum++;
								Log.Append(heroNum + ": " + hero.Name, hero.Color);
							}
						}
						else
						{
							var available = Weapon.All.Where(w => w.Hero == heroDoingStuff && w.HasBeenFound);
							if (!available.Any())
							{
								// nothing to equip
								Log.Append("The " + heroDoingStuff.Name + " doesn't have any weapons to equip!");
								mode = ' ';
								heroDoingStuff = null;
							}
							else
							{
								// prompt for weapon to equip
								Log.Append("What weapon will the " + heroDoingStuff.Name + " equip? (press W again to cancel)");
								int num = 0;
								foreach (var w in available)
								{
									num++;
									Log.Append(num + ": " + w.Name + " (" + w.Description + ")");
								}
							}
						}
					}
				}
			}
			else if (mode == 'a')
			{
				if (heroDoingStuff != null)
				{
					if (e.KeyCode == Keys.A)
					{
						// cancel
						mode = ' ';
						heroDoingStuff = null;
						Log.Append("Never mind, then.");
					}
					else
					{
						// pick armor
						var idx = GetNumberFromKey(e.KeyCode) - 1;
						var available = Armor.All.Where(a => a.Hero == heroDoingStuff && a.HasBeenFound);
						var armor = available.ElementAtOrDefault(idx);

						if (armor == null)
						{
							if (!available.Any())
							{
								// nothing to equip
								Log.Append("The " + heroDoingStuff.Name + " doesn't have any armor to equip!");
								mode = ' ';
								heroDoingStuff = null;
							}
							else
							{
								// prompt for armor to equip
								Log.Append("What armor will the " + heroDoingStuff.Name + " equip? (press A again to cancel)");
								int num = 0;
								foreach (var a in available)
								{
									num++;
									Log.Append(num + ": " + a.Name + " (" + a.Description + ")");
								}
							}
						}
						else
						{
							if (heroDoingStuff.Armor == armor)
							{
								// nothing to do
								Log.Append("But the " + heroDoingStuff.Name + " has already equipped the " + armor.Name + "!");
								mode = ' ';
								heroDoingStuff = null;
							}
							else
							{
								// equip armor
								Log.Append("The " + heroDoingStuff.Name + " equips the " + armor.Name + ".");
								heroDoingStuff.Armor = armor;

								// use time
								map.Heroes.Act(map.Tiles[map.HeroX, map.HeroY].Terrain.MovementCost);
								map.LetMonstersAct();

								// done equipping
								mode = ' ';
								heroDoingStuff = null;
							}
						}
					}
				}
				else
				{
					if (e.KeyCode == Keys.A)
					{
						// cancel
						mode = ' ';
						heroDoingStuff = null;
						Log.Append("Never mind, then.");
					}
					else
					{
						// pick hero
						var heroIdx = GetNumberFromKey(e.KeyCode) - 1;
						heroDoingStuff = map.Heroes.CreaturePositions.Values.ElementAtOrDefault(heroIdx);
						if (heroDoingStuff == null)
						{
							// didn't pick a hero
							Log.Append("Who will equip armor? (press A again to cancel)");
							int heroNum = 0;
							foreach (var hero in map.Heroes.CreaturePositions.Values)
							{
								heroNum++;
								Log.Append(heroNum + ": " + hero.Name, hero.Color);
							}
						}
						else
						{
							var available = Armor.All.Where(a => a.Hero == heroDoingStuff && a.HasBeenFound);
							if (!available.Any())
							{
								// nothing to equip
								Log.Append("The " + heroDoingStuff.Name + " doesn't have any armor to equip!");
								mode = ' ';
								heroDoingStuff = null;
							}
							else
							{
								// prompt for armor to equip
								Log.Append("What armor will the " + heroDoingStuff.Name + " equip? (press A again to cancel)");
								int num = 0;
								foreach (var a in available)
								{
									num++;
									Log.Append(num + ": " + a.Name + " (" + a.Description + ")");
								}
							}
						}
					}
				}
			}
			else
			{
				if (e.KeyCode == Keys.F)
				{
					// F: set formation
					heroBeingPlaced = 0;
					newHeroPositions = new Dictionary<AbsolutePosition, Creature>();
					Log.Append("Please choose a position for the " + map.Heroes.CreaturePositions.Values.ElementAt(heroBeingPlaced).Name + ".");
				}
				else if (e.KeyCode == Keys.S)
				{
					// S: use skill
					mode = 's';
					Log.Append("Who will use a skill? (press S again to cancel)");
					int heroNum = 0;
					foreach (var hero in map.Heroes.CreaturePositions.Values)
					{
						heroNum++;
						Log.Append(heroNum + ": " + hero.Name, hero.Color);
					}
				}
				else if (e.KeyCode == Keys.W)
				{
					// W: change weapon
					mode = 'w';
					Log.Append("Who will change his weapon? (press W again to cancel)");
					int heroNum = 0;
					foreach (var hero in map.Heroes.CreaturePositions.Values)
					{
						heroNum++;
						Log.Append(heroNum + ": " + hero.Name, hero.Color);
					}
				}
				else if (e.KeyCode == Keys.A)
				{
					// W: change weapon
					mode = 'a';
					Log.Append("Who will change his armor? (press A again to cancel)");
					int heroNum = 0;
					foreach (var hero in map.Heroes.CreaturePositions.Values)
					{
						heroNum++;
						Log.Append(heroNum + ": " + hero.Name, hero.Color);
					}
				}
				else if (e.KeyCode == Keys.D5 || e.KeyCode == Keys.NumPad5 || e.KeyCode == Keys.Clear)
				{
					// wait a turn
					map.Heroes.Act(map.Tiles[map.HeroX, map.HeroY].Terrain.MovementCost);
					map.LetMonstersAct();
				}
				else
				{
					var dir = Direction.FromKey(e.KeyCode);
					if (dir != null)
					{
						// movement
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
							heroesDead = true;
							Log.Append("Oh no! The heroes have fallen...", Color.Red);
						}
					}
				}
			}
			picMap.Invalidate();
			picMinimap.Invalidate();
			BindStatsBoxes(map.Heroes);
			RefreshLog();

		}

		private int GetNumberFromKey(Keys key)
		{
			if (key == Keys.D0 || key == Keys.NumPad0)
				return 0;
			else if (key == Keys.D1 || key == Keys.NumPad1)
				return 1;
			else if (key == Keys.D2 || key == Keys.NumPad2)
				return 2;
			else if (key == Keys.D3 || key == Keys.NumPad3)
				return 3;
			else if (key == Keys.D4 || key == Keys.NumPad4)
				return 4;
			else if (key == Keys.D5 || key == Keys.NumPad5)
				return 5;
			else if (key == Keys.D6 || key == Keys.NumPad6)
				return 6;
			else if (key == Keys.D7 || key == Keys.NumPad7)
				return 7;
			else if (key == Keys.D8 || key == Keys.NumPad8)
				return 8;
			else if (key == Keys.D9 || key == Keys.NumPad9)
				return 9;
			return -1; // not a number key
		}

		private void RefreshLog()
		{
			var entries = Log.UnreadEntries.ToArray().Reverse().Take(10).Reverse().ToArray();
			tblLog.Controls.Clear();
			for (var i = 0; i < entries.Count(); i++)
			{
				var lbl = new Label();
				tblLog.Controls.Add(lbl);
				tblLog.SetRow(lbl, i);
				lbl.Dock = DockStyle.Fill;
				lbl.Text = entries[i].Message;
				lbl.ForeColor = entries[i].Color;
			}
			Log.MarkAllRead();
		}

		private void BindStatsBoxes(Formation f)
		{
			foreach (var pos in AbsolutePosition.All)
			{
				var relpos = pos.RelativeTo(f.Facing);
				if (f.CreaturePositions.ContainsKey(relpos))
					FindStatsBox(pos).Creature = f.CreaturePositions[relpos];
				else
					FindStatsBox(pos).Creature = null;
			}
		}

		private StatsBox FindStatsBox(AbsolutePosition pos)
		{
			if (pos == AbsolutePosition.Northwest)
				return sbNW;
			if (pos == AbsolutePosition.North)
				return sbN;
			if (pos == AbsolutePosition.Northeast)
				return sbNE;
			if (pos == AbsolutePosition.West)
				return sbW;
			if (pos == AbsolutePosition.Center)
				return sbC;
			if (pos == AbsolutePosition.East)
				return sbE;
			if (pos == AbsolutePosition.Southwest)
				return sbSW;
			if (pos == AbsolutePosition.South)
				return sbS;
			if (pos == AbsolutePosition.Southeast)
				return sbSE;
			throw new Exception("Invalid position specified for FindStatsBox");
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			BindStatsBoxes(map.Heroes);
		}
	}
}
