namespace TriQuest
{
	partial class MainForm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.picMap = new System.Windows.Forms.PictureBox();
			this.picMinimap = new System.Windows.Forms.PictureBox();
			this.tblLog = new System.Windows.Forms.TableLayoutPanel();
			this.pnlStats = new System.Windows.Forms.TableLayoutPanel();
			this.sbSE = new TriQuest.StatsBox();
			this.sbS = new TriQuest.StatsBox();
			this.sbSW = new TriQuest.StatsBox();
			this.sbE = new TriQuest.StatsBox();
			this.sbC = new TriQuest.StatsBox();
			this.sbW = new TriQuest.StatsBox();
			this.sbNE = new TriQuest.StatsBox();
			this.sbN = new TriQuest.StatsBox();
			this.sbNW = new TriQuest.StatsBox();
			((System.ComponentModel.ISupportInitialize)(this.picMap)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picMinimap)).BeginInit();
			this.pnlStats.SuspendLayout();
			this.SuspendLayout();
			// 
			// picMap
			// 
			this.picMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.picMap.BackColor = System.Drawing.Color.Black;
			this.picMap.Location = new System.Drawing.Point(12, 162);
			this.picMap.Name = "picMap";
			this.picMap.Size = new System.Drawing.Size(678, 555);
			this.picMap.TabIndex = 0;
			this.picMap.TabStop = false;
			this.picMap.Paint += new System.Windows.Forms.PaintEventHandler(this.picMap_Paint);
			// 
			// picMinimap
			// 
			this.picMinimap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.picMinimap.BackColor = System.Drawing.Color.Black;
			this.picMinimap.Location = new System.Drawing.Point(696, 417);
			this.picMinimap.Name = "picMinimap";
			this.picMinimap.Size = new System.Drawing.Size(300, 300);
			this.picMinimap.TabIndex = 1;
			this.picMinimap.TabStop = false;
			this.picMinimap.Paint += new System.Windows.Forms.PaintEventHandler(this.picMinimap_Paint);
			// 
			// tblLog
			// 
			this.tblLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tblLog.BackColor = System.Drawing.Color.Black;
			this.tblLog.ColumnCount = 1;
			this.tblLog.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tblLog.ForeColor = System.Drawing.Color.White;
			this.tblLog.Location = new System.Drawing.Point(12, 12);
			this.tblLog.Name = "tblLog";
			this.tblLog.RowCount = 10;
			this.tblLog.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tblLog.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tblLog.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tblLog.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tblLog.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tblLog.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tblLog.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tblLog.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tblLog.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tblLog.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
			this.tblLog.Size = new System.Drawing.Size(984, 144);
			this.tblLog.TabIndex = 3;
			// 
			// pnlStats
			// 
			this.pnlStats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlStats.BackColor = System.Drawing.Color.Black;
			this.pnlStats.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this.pnlStats.ColumnCount = 3;
			this.pnlStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.pnlStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.pnlStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.pnlStats.Controls.Add(this.sbSE, 2, 2);
			this.pnlStats.Controls.Add(this.sbS, 1, 2);
			this.pnlStats.Controls.Add(this.sbSW, 0, 2);
			this.pnlStats.Controls.Add(this.sbE, 2, 1);
			this.pnlStats.Controls.Add(this.sbC, 1, 1);
			this.pnlStats.Controls.Add(this.sbW, 0, 1);
			this.pnlStats.Controls.Add(this.sbNE, 2, 0);
			this.pnlStats.Controls.Add(this.sbN, 1, 0);
			this.pnlStats.Controls.Add(this.sbNW, 0, 0);
			this.pnlStats.ForeColor = System.Drawing.Color.White;
			this.pnlStats.Location = new System.Drawing.Point(696, 162);
			this.pnlStats.Name = "pnlStats";
			this.pnlStats.RowCount = 3;
			this.pnlStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.pnlStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.pnlStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.pnlStats.Size = new System.Drawing.Size(300, 249);
			this.pnlStats.TabIndex = 4;
			// 
			// sbSE
			// 
			this.sbSE.BackColor = System.Drawing.Color.Black;
			this.sbSE.Creature = null;
			this.sbSE.ForeColor = System.Drawing.Color.White;
			this.sbSE.Location = new System.Drawing.Point(202, 168);
			this.sbSE.Name = "sbSE";
			this.sbSE.Size = new System.Drawing.Size(92, 75);
			this.sbSE.TabIndex = 8;
			this.sbSE.TabStop = false;
			// 
			// sbS
			// 
			this.sbS.BackColor = System.Drawing.Color.Black;
			this.sbS.Creature = null;
			this.sbS.ForeColor = System.Drawing.Color.White;
			this.sbS.Location = new System.Drawing.Point(103, 168);
			this.sbS.Name = "sbS";
			this.sbS.Size = new System.Drawing.Size(92, 75);
			this.sbS.TabIndex = 7;
			this.sbS.TabStop = false;
			// 
			// sbSW
			// 
			this.sbSW.BackColor = System.Drawing.Color.Black;
			this.sbSW.Creature = null;
			this.sbSW.ForeColor = System.Drawing.Color.White;
			this.sbSW.Location = new System.Drawing.Point(4, 168);
			this.sbSW.Name = "sbSW";
			this.sbSW.Size = new System.Drawing.Size(92, 75);
			this.sbSW.TabIndex = 6;
			this.sbSW.TabStop = false;
			// 
			// sbE
			// 
			this.sbE.BackColor = System.Drawing.Color.Black;
			this.sbE.Creature = null;
			this.sbE.ForeColor = System.Drawing.Color.White;
			this.sbE.Location = new System.Drawing.Point(202, 86);
			this.sbE.Name = "sbE";
			this.sbE.Size = new System.Drawing.Size(92, 75);
			this.sbE.TabIndex = 5;
			this.sbE.TabStop = false;
			// 
			// sbC
			// 
			this.sbC.BackColor = System.Drawing.Color.Black;
			this.sbC.Creature = null;
			this.sbC.ForeColor = System.Drawing.Color.White;
			this.sbC.Location = new System.Drawing.Point(103, 86);
			this.sbC.Name = "sbC";
			this.sbC.Size = new System.Drawing.Size(92, 75);
			this.sbC.TabIndex = 4;
			this.sbC.TabStop = false;
			// 
			// sbW
			// 
			this.sbW.BackColor = System.Drawing.Color.Black;
			this.sbW.Creature = null;
			this.sbW.ForeColor = System.Drawing.Color.White;
			this.sbW.Location = new System.Drawing.Point(4, 86);
			this.sbW.Name = "sbW";
			this.sbW.Size = new System.Drawing.Size(92, 75);
			this.sbW.TabIndex = 3;
			this.sbW.TabStop = false;
			// 
			// sbNE
			// 
			this.sbNE.BackColor = System.Drawing.Color.Black;
			this.sbNE.Creature = null;
			this.sbNE.ForeColor = System.Drawing.Color.White;
			this.sbNE.Location = new System.Drawing.Point(202, 4);
			this.sbNE.Name = "sbNE";
			this.sbNE.Size = new System.Drawing.Size(92, 75);
			this.sbNE.TabIndex = 2;
			this.sbNE.TabStop = false;
			// 
			// sbN
			// 
			this.sbN.BackColor = System.Drawing.Color.Black;
			this.sbN.Creature = null;
			this.sbN.ForeColor = System.Drawing.Color.White;
			this.sbN.Location = new System.Drawing.Point(103, 4);
			this.sbN.Name = "sbN";
			this.sbN.Size = new System.Drawing.Size(92, 75);
			this.sbN.TabIndex = 1;
			this.sbN.TabStop = false;
			// 
			// sbNW
			// 
			this.sbNW.BackColor = System.Drawing.Color.Black;
			this.sbNW.Creature = null;
			this.sbNW.ForeColor = System.Drawing.Color.White;
			this.sbNW.Location = new System.Drawing.Point(4, 4);
			this.sbNW.Name = "sbNW";
			this.sbNW.Size = new System.Drawing.Size(92, 75);
			this.sbNW.TabIndex = 0;
			this.sbNW.TabStop = false;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1008, 729);
			this.Controls.Add(this.pnlStats);
			this.Controls.Add(this.tblLog);
			this.Controls.Add(this.picMinimap);
			this.Controls.Add(this.picMap);
			this.DoubleBuffered = true;
			this.KeyPreview = true;
			this.Name = "MainForm";
			this.Text = "TriQuest";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
			this.Resize += new System.EventHandler(this.MainForm_Resize);
			((System.ComponentModel.ISupportInitialize)(this.picMap)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picMinimap)).EndInit();
			this.pnlStats.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox picMap;
		private System.Windows.Forms.PictureBox picMinimap;
		private System.Windows.Forms.TableLayoutPanel tblLog;
		private System.Windows.Forms.TableLayoutPanel pnlStats;
		private StatsBox sbNW;
		private StatsBox sbSE;
		private StatsBox sbS;
		private StatsBox sbSW;
		private StatsBox sbE;
		private StatsBox sbC;
		private StatsBox sbW;
		private StatsBox sbNE;
		private StatsBox sbN;
	}
}

