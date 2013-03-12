namespace TriQuest
{
	partial class StatsBox
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.txtSymbol = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.Label();
			this.lblHealth = new System.Windows.Forms.Label();
			this.txtHealth = new System.Windows.Forms.Label();
			this.lblAttackDefense = new System.Windows.Forms.Label();
			this.txtAttackDefense = new System.Windows.Forms.Label();
			this.txtBodyMind = new System.Windows.Forms.Label();
			this.lblBodyMind = new System.Windows.Forms.Label();
			this.lblSpeedSight = new System.Windows.Forms.Label();
			this.txtSpeedSight = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// txtSymbol
			// 
			this.txtSymbol.AutoSize = true;
			this.txtSymbol.Font = new System.Drawing.Font("Lucida Console", 12F);
			this.txtSymbol.Location = new System.Drawing.Point(4, 4);
			this.txtSymbol.Name = "txtSymbol";
			this.txtSymbol.Size = new System.Drawing.Size(18, 16);
			this.txtSymbol.TabIndex = 0;
			this.txtSymbol.Text = "@";
			// 
			// txtName
			// 
			this.txtName.AutoSize = true;
			this.txtName.Location = new System.Drawing.Point(19, 4);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(100, 13);
			this.txtName.TabIndex = 1;
			this.txtName.Text = "roguelike developer";
			// 
			// lblHealth
			// 
			this.lblHealth.AutoSize = true;
			this.lblHealth.Location = new System.Drawing.Point(4, 20);
			this.lblHealth.Name = "lblHealth";
			this.lblHealth.Size = new System.Drawing.Size(41, 13);
			this.lblHealth.TabIndex = 2;
			this.lblHealth.Text = "Health:";
			// 
			// txtHealth
			// 
			this.txtHealth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtHealth.AutoSize = true;
			this.txtHealth.Location = new System.Drawing.Point(72, 20);
			this.txtHealth.Name = "txtHealth";
			this.txtHealth.Size = new System.Drawing.Size(25, 13);
			this.txtHealth.TabIndex = 3;
			this.txtHealth.Text = "100";
			// 
			// lblAttackDefense
			// 
			this.lblAttackDefense.AutoSize = true;
			this.lblAttackDefense.Location = new System.Drawing.Point(4, 33);
			this.lblAttackDefense.Name = "lblAttackDefense";
			this.lblAttackDefense.Size = new System.Drawing.Size(54, 13);
			this.lblAttackDefense.TabIndex = 4;
			this.lblAttackDefense.Text = "Atk / Def:";
			// 
			// txtAttackDefense
			// 
			this.txtAttackDefense.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtAttackDefense.AutoSize = true;
			this.txtAttackDefense.Location = new System.Drawing.Point(67, 33);
			this.txtAttackDefense.Name = "txtAttackDefense";
			this.txtAttackDefense.Size = new System.Drawing.Size(30, 13);
			this.txtAttackDefense.TabIndex = 5;
			this.txtAttackDefense.Text = "7 / 3";
			// 
			// txtBodyMind
			// 
			this.txtBodyMind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtBodyMind.AutoSize = true;
			this.txtBodyMind.Location = new System.Drawing.Point(67, 46);
			this.txtBodyMind.Name = "txtBodyMind";
			this.txtBodyMind.Size = new System.Drawing.Size(30, 13);
			this.txtBodyMind.TabIndex = 7;
			this.txtBodyMind.Text = "1 / 9";
			// 
			// lblBodyMind
			// 
			this.lblBodyMind.AutoSize = true;
			this.lblBodyMind.Location = new System.Drawing.Point(4, 46);
			this.lblBodyMind.Name = "lblBodyMind";
			this.lblBodyMind.Size = new System.Drawing.Size(60, 13);
			this.lblBodyMind.TabIndex = 6;
			this.lblBodyMind.Text = "Bdy / Mnd:";
			// 
			// lblSpeedSight
			// 
			this.lblSpeedSight.AutoSize = true;
			this.lblSpeedSight.Location = new System.Drawing.Point(4, 59);
			this.lblSpeedSight.Name = "lblSpeedSight";
			this.lblSpeedSight.Size = new System.Drawing.Size(56, 13);
			this.lblSpeedSight.TabIndex = 8;
			this.lblSpeedSight.Text = "Spd / Sgt:";
			// 
			// txtSpeedSight
			// 
			this.txtSpeedSight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtSpeedSight.AutoSize = true;
			this.txtSpeedSight.Location = new System.Drawing.Point(67, 59);
			this.txtSpeedSight.Name = "txtSpeedSight";
			this.txtSpeedSight.Size = new System.Drawing.Size(30, 13);
			this.txtSpeedSight.TabIndex = 9;
			this.txtSpeedSight.Text = "5 / 5";
			// 
			// StatsBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.Controls.Add(this.txtSpeedSight);
			this.Controls.Add(this.lblSpeedSight);
			this.Controls.Add(this.txtBodyMind);
			this.Controls.Add(this.lblBodyMind);
			this.Controls.Add(this.txtAttackDefense);
			this.Controls.Add(this.lblAttackDefense);
			this.Controls.Add(this.txtHealth);
			this.Controls.Add(this.lblHealth);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.txtSymbol);
			this.DoubleBuffered = true;
			this.ForeColor = System.Drawing.Color.White;
			this.Name = "StatsBox";
			this.Size = new System.Drawing.Size(100, 75);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label txtSymbol;
		private System.Windows.Forms.Label txtName;
		private System.Windows.Forms.Label lblHealth;
		private System.Windows.Forms.Label txtHealth;
		private System.Windows.Forms.Label lblAttackDefense;
		private System.Windows.Forms.Label txtAttackDefense;
		private System.Windows.Forms.Label txtBodyMind;
		private System.Windows.Forms.Label lblBodyMind;
		private System.Windows.Forms.Label lblSpeedSight;
		private System.Windows.Forms.Label txtSpeedSight;
	}
}
