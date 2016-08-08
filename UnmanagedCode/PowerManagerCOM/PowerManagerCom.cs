using System;
using System.Runtime.InteropServices;
using PowerManagerLibrary;

namespace PowerManagerCOM
{
	[ComVisible(true)]
	[Guid("7D79F1A8-E50D-4CA2-AA24-CAE3B4D53157")]
	[ClassInterface(ClassInterfaceType.None)]
	public sealed class PowerManagerCom : IPowerManagerCom
	{
		private readonly PowerManager _powerManager;

		public PowerManagerCom()
		{
			_powerManager = new PowerManager();
		}

		public DateTime GetLastSleepTime()
		{
			return _powerManager.GetLastSleepTime();
		}

		public DateTime GetLastWakeTime()
		{
			return _powerManager.GetLastWakeTime();
		}

		public bool GetSystemBatteryState()
		{
			return _powerManager.GetSystemBatteryState().BatteryPresent;
		}

		public int GetSystemCoolingMode()
		{
			return _powerManager.GetSystemPowerInformation().CoolingMode;
		}
	}
}
