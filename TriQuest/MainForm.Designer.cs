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
			((System.ComponentModel.ISupportInitialize)(this.picMap)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.picMinimap)).BeginInit();
			this.SuspendLayout();
			// 
			// picMap
			// 
			this.picMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.picMap.BackColor = System.Drawing.Color.Black;
			this.picMap.Location = new System.Drawing.Point(12, 12);
			this.picMap.Name = "picMap";
			this.picMap.Size = new System.Drawing.Size(454, 537);
			this.picMap.TabIndex = 0;
			this.picMap.TabStop = false;
			this.picMap.Paint += new System.Windows.Forms.PaintEventHandler(this.picMap_Paint);
			// 
			// picMinimap
			// 
			this.picMinimap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.picMinimap.BackColor = System.Drawing.Color.Black;
			this.picMinimap.Location = new System.Drawing.Point(472, 12);
			this.picMinimap.Name = "picMinimap";
			this.picMinimap.Size = new System.Drawing.Size(300, 300);
			this.picMinimap.TabIndex = 1;
			this.picMinimap.TabStop = false;
			this.picMinimap.Paint += new System.Windows.Forms.PaintEventHandler(this.picMinimap_Paint);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(784, 561);
			this.Controls.Add(this.picMinimap);
			this.Controls.Add(this.picMap);
			this.DoubleBuffered = true;
			this.Name = "MainForm";
			this.Text = "TriQuest";
			this.Resize += new System.EventHandler(this.MainForm_Resize);
			((System.ComponentModel.ISupportInitialize)(this.picMap)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.picMinimap)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox picMap;
		private System.Windows.Forms.PictureBox picMinimap;
	}
}

