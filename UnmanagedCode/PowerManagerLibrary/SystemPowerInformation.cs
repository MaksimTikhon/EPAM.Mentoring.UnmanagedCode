using System.Runtime.InteropServices;

namespace PowerManagerLibrary
{
	[StructLayout(LayoutKind.Sequential)]
	public struct SystemPowerInformation
	{
		public uint MaxIdlenessAllowed;
		public uint Idleness;
		public uint TimeRemaining;
		public sbyte CoolingMode;
	}
}
