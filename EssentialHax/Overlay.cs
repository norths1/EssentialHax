using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EssentialHax {
	public partial class Overlay : Form {

		[DllImport("user32.dll")]
		private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
		[DllImport("user32.dll")]
		public static extern bool SetForegroundWindow(IntPtr hWnd);

		List<string> activeHax = new List<string>();

		CancellationTokenSource task;

		MemLib Mem;
		long WorldPTR;
		long BlipPTR;
		bool neverwanted = false;

		public Overlay() {

			try {
				Mem = new MemLib("GTA5");
				Mem.Proc.EnableRaisingEvents = true;
				Mem.Proc.Exited += Proc_Exited;
				long addr = Mem.FindPattern(new byte[] { 0x48, 0x8B, 0x05, 0x0, 0x0, 0x0, 0x0, 0x45, 0x0, 0x0, 0x0, 0x0, 0x48, 0x8B, 0x48, 0x08, 0x48, 0x85, 0xC9, 0x74, 0x07 }, "xxx????x????xxxxxxxxx");
				WorldPTR = addr + Mem.ReadInt(addr + 3, null) + 7;
				long addr2 = Mem.FindPattern(new byte[] { 0x4C, 0x8D, 0x05,0x0,0x0,0x0,0x0, 0x0F, 0xB7, 0xC1 }, "xxx????xxx");
				BlipPTR = addr2 + Mem.ReadInt(addr2 + 3, null) + 7;
			} catch {
				MessageBox.Show("GTA not Running!", "Spooky Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			RegisterKey(Keys.NumPad0);
			RegisterKey(Keys.NumPad1);
			RegisterKey(Keys.NumPad2);
			RegisterKey(Keys.NumPad3);
			RegisterKey(Keys.NumPad4);
			RegisterKey(Keys.NumPad5);
			RegisterKey(Keys.NumPad6);
			RegisterKey(Keys.NumPad7);
			RegisterKey(Keys.NumPad8);
			RegisterKey(Keys.NumPad9);
			RegisterKey(Keys.F5);
			RegisterKey(Keys.F6);
			RegisterKey(Keys.F7);
			RegisterKey(Keys.F9);

			this.Location = new Point(0, 0);
			InitializeComponent();
			label1.Text = "EssentialHax b0.99";
			displayInfo("Started EssentialHax!");
			SetForegroundWindow(Mem.Proc.MainWindowHandle);
		}

		private void Proc_Exited(object sender, EventArgs e) {
			MessageBox.Show("'GTA5' closed! Exiting...");
			neverwanted = false;
			Environment.Exit(0);
		}

		public void RegisterKey(Keys k, int modifier = 0) => RegisterHotKey(Handle, modifier ^ (int)k ^ this.Handle.ToInt32(), modifier, (int)k);

		public void startNeverWanted() {
			neverwanted = true;
			new Thread(() => {
				while(neverwanted) {
					if(Mem.ReadInt(WorldPTR, Offset.WantedLevel) != 0) Mem.WriteInt(WorldPTR, Offset.WantedLevel, 0);
					Thread.Sleep(100);
				}
			}).Start();
		}

		public void displayInfo(string text, int delaySec = 3) {
			if(task != null) task.Cancel();
			info.Text = text;
			info.ForeColor = Color.Fuchsia;
			task = new CancellationTokenSource();
			Task.Delay(delaySec * 1000).ContinueWith(t => info.Invoke(new Action(() => {
				if(task != null && !task.IsCancellationRequested) {
					info.Text = "";
					task = null;
				}
			})), task.Token);
		}

		public void displayError(string text, int delaySec = 5) {
			if(task != null) task.Cancel();
			info.Text = text;
			info.ForeColor = Color.Red;
			task = new CancellationTokenSource();
			Task.Delay(delaySec * 1000).ContinueWith(t => info.Invoke(new Action(() => {
				if(task != null && !task.IsCancellationRequested) {
					info.Text = "";
					task = null;
				}
			})), task.Token);
		}

		private void HandleHotkeys(Keys k) {
			if(k == Keys.F9) this.Visible = !this.Visible;
			if(!Visible) return;
			switch(k) {
				case Keys.NumPad0:
					if(!neverwanted) {
						startNeverWanted();
						activeHax.Add("Never Wanted");
					} else {
						neverwanted = false;
						activeHax.RemoveAll(item => item == "Never Wanted");
					}
					displayInfo(neverwanted ? "Now the Police wont give a shit!" : "Police now looking 4 u");
					break;
				case Keys.NumPad1:
					var slot = Mem.ReadInt(WorldPTR, Offset.WeaponSlot);
					Mem.WriteFloat(WorldPTR, Offset.WeaponSpread, 0);
					Mem.WriteFloat(WorldPTR, Offset.WeaponRecoil, 0);
					Mem.WriteFloat(WorldPTR, Offset.WeaponReloadSpeed, 10F);
					Mem.WriteFloat(WorldPTR, Offset.BulletDamage, 5000F);
					if(slot == 6) Mem.WriteFloat(WorldPTR, Offset.RocketVelocity, 25000F);
					if(slot == 7) Mem.WriteFloat(WorldPTR, Offset.ThrowGravity, 0.3F);
					displayInfo("Held Weapon Patched!");
					break;
				case Keys.NumPad2:
					if(isPlayerInCar()) {
						Mem.WriteFloat(WorldPTR, Offset.CarAcceleration, 5);
						Mem.WriteFloat(WorldPTR, Offset.CarGravity, 20);
						Mem.WriteFloat(WorldPTR, Offset.Seatbelt, 20);
						displayInfo("Fast Car Triggered");
					} else displayError("Not in Car!");
					break;
				case Keys.NumPad3:
					var isFast = Mem.ReadFloat(WorldPTR, Offset.SprintSpeed) != 1F;
					Mem.WriteFloat(WorldPTR, Offset.SprintSpeed, isFast ? 1 : 2);
					Mem.WriteFloat(WorldPTR, Offset.SwimSpeed, isFast ? 1 : 2);
					displayInfo((isFast ? "Deactivated" : "Activated") + " Fast Boi!");
					if(!isFast) activeHax.Add("Fast Boi");
					else activeHax.RemoveAll(item => item == "Fast Boi");
					break;
				case Keys.NumPad4:
					var isGod = Mem.ReadBool(WorldPTR, Offset.Godmode);
					Mem.WriteBool(WorldPTR, Offset.Godmode, !isGod);
					Mem.WriteBool(WorldPTR, Offset.CarGodmode, !isGod);
					displayInfo((isGod ? "Deactivated" : "Activated") + " Buddha!");
					if(!isGod) activeHax.Add("Buddha");
					else activeHax.RemoveAll(item => item == "Buddha");
					break;
				case Keys.NumPad5:
					Mem.WriteFloat(WorldPTR, Offset.MaxHealth, 2600);
					Mem.WriteFloat(WorldPTR, Offset.Health, 2600);
					Mem.WriteFloat(WorldPTR, Offset.Armor, 100);
					displayInfo("Juggernaut Mode activated & Healed!");
					break;
				case Keys.NumPad6:
					var isInvisible = Mem.ReadFloat(WorldPTR, Offset.MaxHealth) == 0F;
					Mem.WriteFloat(WorldPTR, Offset.MaxHealth, isInvisible ? 328 : 0);
					displayInfo((isInvisible ? "Deactivated" : "Activated") + " Gone from Radar!");
					if(!isInvisible) activeHax.Add("Undead Gone from Radar");
					else activeHax.RemoveAll(item => item == "Undead Gone from Radar");
					break;
				case Keys.NumPad7:
					Mem.WriteFloat(WorldPTR, Offset.Firerate, 0.01F);
					displayInfo("Rapid Fire 4 Held Weapon active!");
					break;
				case Keys.NumPad8:
					if(isPlayerInCar()) {
						Mem.WriteFloat(WorldPTR, Offset.RocketRecharge, 20);
						displayInfo("Rocketboost now Fast Recharging!");
					} else displayError("Not in Car!");
					break;
				case Keys.NumPad9:
					if(isPlayerInCar()) {
						var zeroGrav = Mem.ReadFloat(WorldPTR, Offset.CarGravity) == 0;
						Mem.WriteFloat(WorldPTR, Offset.CarGravity, zeroGrav ? 9.8F : 0);
						displayInfo("0-Gravity " + (zeroGrav ? "Deactivated" : "Activated"));
					}else displayError("Not in Car!");
					break;
				case Keys.F5:
					neverwanted = false;
					Environment.Exit(0);
					break;
				case Keys.F6:
					keymapHeader.Visible = !keymapHeader.Visible;
					keymap.Visible = keymapHeader.Visible;
					break;
				case Keys.F7:

					for(int i = 2000; i > 1; i--) {
						if(Mem.ReadInt(BlipPTR + (i*8), new int[] { 0x48 }) == 84 && Mem.ReadInt(BlipPTR + (i * 8), new int[] { 0x40 }) == 8) {
							float x = Mem.ReadFloat(BlipPTR + (i * 8), new int[] { 0x10 });
							float y = Mem.ReadFloat(BlipPTR + (i * 8), new int[] { 0x14 });

							if(isPlayerInCar()) {
								Mem.WriteFloat(WorldPTR, Offset.CarX, x);
								Mem.WriteFloat(WorldPTR, Offset.CarY, y);
								Mem.WriteFloat(WorldPTR, Offset.CarZ, -210);
								Mem.WriteFloat(WorldPTR, Offset.CarVecX, x);
								Mem.WriteFloat(WorldPTR, Offset.CarVecY, y);
								Mem.WriteFloat(WorldPTR, Offset.CarVecZ, -210);
							} else {
								Mem.WriteFloat(WorldPTR, Offset.PlayerVecX, x);
								Mem.WriteFloat(WorldPTR, Offset.PlayerVecY, y);
								Mem.WriteFloat(WorldPTR, Offset.PlayerVecZ, -210);
								Mem.WriteFloat(WorldPTR, Offset.PlayerX, x);
								Mem.WriteFloat(WorldPTR, Offset.PlayerY, y);
								Mem.WriteFloat(WorldPTR, Offset.PlayerZ, -210);
							}
							displayInfo("Teleported To Waypoint!");
							break;
						}
					}
					break;
			}
			haxinfo.Text = string.Join("\n", activeHax.ToArray());
			if(activeHax.Count == 0) haxinfo.Text = "None";
		}

		public bool isPlayerInCar() => Mem.ReadInt(WorldPTR, Offset.PlayerInCar) == 2;

		protected override void WndProc(ref Message m) {
			if(m.Msg == 0x0312) HandleHotkeys((Keys)((m.LParam.ToInt32()) >> 16));
			base.WndProc(ref m);
		}

		private void Overlay_FormClosing(object sender, FormClosingEventArgs e) => neverwanted = false;
	}
}
