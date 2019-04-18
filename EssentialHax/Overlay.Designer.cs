namespace EssentialHax {
	partial class Overlay {
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Verwendete Ressourcen bereinigen.
		/// </summary>
		/// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
		protected override void Dispose(bool disposing) {
			if(disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Vom Windows Form-Designer generierter Code

		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung.
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Overlay));
			this.label1 = new System.Windows.Forms.Label();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.label2 = new System.Windows.Forms.Label();
			this.haxinfo = new System.Windows.Forms.Label();
			this.keymapHeader = new System.Windows.Forms.Label();
			this.keymap = new System.Windows.Forms.Label();
			this.info = new System.Windows.Forms.Label();
			this.flowLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label1.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.Red;
			this.label1.Location = new System.Drawing.Point(3, 3);
			this.label1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(190, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "EssentialHax v420";
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanel1.Controls.Add(this.label1);
			this.flowLayoutPanel1.Controls.Add(this.label2);
			this.flowLayoutPanel1.Controls.Add(this.haxinfo);
			this.flowLayoutPanel1.Controls.Add(this.keymapHeader);
			this.flowLayoutPanel1.Controls.Add(this.keymap);
			this.flowLayoutPanel1.Controls.Add(this.info);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(415, 413);
			this.flowLayoutPanel1.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.CausesValidation = false;
			this.label2.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.LimeGreen;
			this.label2.Location = new System.Drawing.Point(3, 26);
			this.label2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(84, 16);
			this.label2.TabIndex = 13;
			this.label2.Text = "Active Hax:";
			// 
			// haxinfo
			// 
			this.haxinfo.AutoSize = true;
			this.haxinfo.CausesValidation = false;
			this.haxinfo.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.haxinfo.ForeColor = System.Drawing.Color.ForestGreen;
			this.haxinfo.Location = new System.Drawing.Point(10, 45);
			this.haxinfo.Margin = new System.Windows.Forms.Padding(10, 0, 3, 10);
			this.haxinfo.Name = "haxinfo";
			this.haxinfo.Size = new System.Drawing.Size(40, 13);
			this.haxinfo.TabIndex = 7;
			this.haxinfo.Text = "None";
			// 
			// keymapHeader
			// 
			this.keymapHeader.AutoSize = true;
			this.keymapHeader.CausesValidation = false;
			this.keymapHeader.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.keymapHeader.ForeColor = System.Drawing.Color.Yellow;
			this.keymapHeader.Location = new System.Drawing.Point(3, 68);
			this.keymapHeader.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.keymapHeader.Name = "keymapHeader";
			this.keymapHeader.Size = new System.Drawing.Size(72, 16);
			this.keymapHeader.TabIndex = 12;
			this.keymapHeader.Text = "Keymap:";
			this.keymapHeader.Visible = false;
			// 
			// keymap
			// 
			this.keymap.AutoSize = true;
			this.keymap.CausesValidation = false;
			this.keymap.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.keymap.ForeColor = System.Drawing.Color.Orange;
			this.keymap.Location = new System.Drawing.Point(10, 87);
			this.keymap.Margin = new System.Windows.Forms.Padding(10, 0, 3, 3);
			this.keymap.Name = "keymap";
			this.keymap.Size = new System.Drawing.Size(211, 272);
			this.keymap.TabIndex = 11;
			this.keymap.Text = resources.GetString("keymap.Text");
			this.keymap.Visible = false;
			// 
			// info
			// 
			this.info.AutoSize = true;
			this.info.CausesValidation = false;
			this.info.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.info.ForeColor = System.Drawing.Color.Fuchsia;
			this.info.Location = new System.Drawing.Point(3, 362);
			this.info.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.info.Name = "info";
			this.info.Size = new System.Drawing.Size(56, 16);
			this.info.TabIndex = 10;
			this.info.Text = "Infobox";
			// 
			// Overlay
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(415, 413);
			this.Controls.Add(this.flowLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Overlay";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "EssentialHax";
			this.TopMost = true;
			this.TransparencyKey = System.Drawing.Color.Black;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Overlay_FormClosing);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Label haxinfo;
		private System.Windows.Forms.Label info;
		private System.Windows.Forms.Label keymap;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label keymapHeader;
	}
}

