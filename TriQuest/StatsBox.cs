using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TriQuest
{
	/// <summary>
	/// Displays stats of a creature.
	/// </summary>
	public partial class StatsBox : UserControl
	{
		public StatsBox()
		{
			InitializeComponent();
		}

		private Creature creature;
		public Creature Creature
		{
			get { return creature; }
			set
			{
				creature = value;
				Bind();
			}
		}

		public void Bind()
		{
			if (Creature == null)
			{
				foreach (Control ctl in this.Controls)
					ctl.Hide();
			}
			else
			{
				txtSymbol.Text = Creature.Symbol.ToString();
				txtSymbol.ForeColor = Creature.Color;
				txtName.Text = Creature.Name;
				txtHealthMana.Text = string.Format("{0} / {1}", Creature.Health, Creature.Mana);
				txtAttackDefense.Text = string.Format("{0} / {1}", Creature.Attack, Creature.Defense);
				txtBodyMind.Text = string.Format("{0} / {1}", Creature.Body, Creature.Mind);
				txtSpeedSight.Text = string.Format("{0} / {1}", Creature.Speed, Creature.Sight);

				foreach (Control ctl in this.Controls)
					ctl.Show();
			}
		}
	}
}
