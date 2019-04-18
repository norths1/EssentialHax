using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace EssentialHax {
	public class MemLib {
		[DllImport("kernel32.dll")]
		public static extern int WriteProcessMemory(IntPtr Handle, long Address, byte[] buffer, int Size, int BytesWritten = 0);
		[DllImport("kernel32.dll")]
		public static extern int ReadProcessMemory(IntPtr Handle, long Address, byte[] buffer, int Size, int BytesRead = 0);

		public Process Proc;
		public long BaseAddress;

		public MemLib(string process) {
			try {
				Proc = Process.GetProcessesByName(process)[0];
				BaseAddress = Proc.MainModule.BaseAddress.ToInt64();
			}
			catch {
				throw new Exception();
			}

		}

		public IntPtr GetProcHandle() {
			try {
				return Proc.Handle;
			}
			catch {
				return IntPtr.Zero;
			}
		}

		public long GetPtrAddr(long Pointer, int[] Offset = null) {
			byte[] Buffer = new byte[8];

			ReadProcessMemory(GetProcHandle(), Pointer, Buffer, Buffer.Length);

			if(Offset != null) {
				for(int x = 0; x < (Offset.Length - 1); x++) {
					Pointer = BitConverter.ToInt64(Buffer, 0) + Offset[x];
					ReadProcessMemory(GetProcHandle(), Pointer, Buffer, Buffer.Length);
				}

				Pointer = BitConverter.ToInt64(Buffer, 0) + Offset[Offset.Length - 1];
			}

			return Pointer;
		}

		public long FindPattern(byte[] pattern, string mask) {
			int moduleSize = Proc.MainModule.ModuleMemorySize;

			if(moduleSize == 0)
				throw new Exception($"Size of module {Proc.MainModule.ModuleName} is INVALID.");

			byte[] moduleBytes = new byte[moduleSize];
			ReadProcessMemory(GetProcHandle(), BaseAddress, moduleBytes, moduleSize);

			for(long i = 0; i < moduleSize; i++)
			{
				bool found = true;

				for(int l = 0; l < mask.Length; l++) {
					found = mask[l] == '?' || moduleBytes[l + i] == pattern[l];

					if(!found)
						break;
				}

				if(found) {
					moduleBytes = null;
					return i;
				}

			}
			return 0;
		}

		public void WriteBytes(long BasePTR, int[] offset, byte[] Bytes) => WriteProcessMemory(GetProcHandle(), GetPtrAddr(BaseAddress + BasePTR, offset), Bytes, Bytes.Length);
		public void WriteBool(long BasePTR, int[] offset, bool b) => WriteBytes(BasePTR, offset, b ? new byte[] { 0x01 } : new byte[] { 0x00 });
		public void WriteFloat(long BasePTR, int[] offset, float Value) => WriteBytes(BasePTR, offset, BitConverter.GetBytes(Value));
		public void WriteDouble(long BasePTR, int[] offset, double Value) => WriteBytes(BasePTR, offset, BitConverter.GetBytes(Value));
		public void WriteInt(long BasePTR, int[] offset, int Value) => WriteBytes(BasePTR, offset, BitConverter.GetBytes(Value));
		public void WriteString(long BasePTR, int[] offset, string String) {
			byte[] Buffer = new ASCIIEncoding().GetBytes(String);
			WriteProcessMemory(GetProcHandle(), GetPtrAddr(BaseAddress + BasePTR, offset), Buffer, Buffer.Length);
		}
		public bool ReadBool(long BasePTR, int[] offset) => ReadBytes(BasePTR, offset, 1)[0] != 0x00;
		public byte[] ReadBytes(long BasePTR, int[] offset, int Length) {
			byte[] Buffer = new byte[Length];
			ReadProcessMemory(GetProcHandle(), GetPtrAddr(BaseAddress + BasePTR, offset), Buffer, Length);
			return Buffer;
		}
		public float ReadFloat(long BasePTR, int[] offset) => BitConverter.ToSingle(ReadBytes(BasePTR, offset, 4), 0);
		public double ReadDouble(long BasePTR, int[] offset) => BitConverter.ToDouble(ReadBytes(BasePTR, offset, 8), 0);
		public int ReadInt(long BasePTR, int[] offset) => BitConverter.ToInt32(ReadBytes(BasePTR, offset, 4), 0);
		public string ReadString(long BasePTR, int[] offset, int size) => new ASCIIEncoding().GetString(ReadBytes(BasePTR, offset, size));
		public long ReadPointer(long BasePTR, int[] offset) => BitConverter.ToInt64(ReadBytes(BasePTR, offset, 8), 0);
	}
}