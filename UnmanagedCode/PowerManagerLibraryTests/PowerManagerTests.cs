using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PowerManagerLibrary;

namespace PowerManagerLibraryTests
{
	[TestClass]
	public class PowerManagerTests
	{
		private readonly PowerManager _powerManager = new PowerManager();

		[TestMethod]
		public void LastSleepTimeTest()
		{
			var lastSleepTime = _powerManager.GetLastSleepTime();

			Console.WriteLine("Last sleep time: {0}", lastSleepTime.ToLocalTime());
		}

		[TestMethod]
		public void LastWakeTimeTest()
		{
			var lastWakeTime = _powerManager.GetLastWakeTime();

			Console.WriteLine("Last wake time: {0}", lastWakeTime.ToLocalTime());
		}

		[TestMethod]
		public void SystemBatteryStateTest()
		{
			var state = _powerManager.GetSystemBatteryState();

			Console.WriteLine("Battery Present: {0}", state.BatteryPresent);
			Console.WriteLine("Remaining Capacity: {0}", state.RemainingCapacity);
		}

		[TestMethod]
		public void PowerInformationTest()
		{
			var info = _powerManager.GetSystemPowerInformation();

			Console.WriteLine("The current idle level: {0} %", info.Idleness);
			Console.WriteLine("The current system cooling mode (0 - active, 1 - passive, 2- invalid): {0}", info.CoolingMode);
		}

		[TestMethod]
		public void ReserveHibernationFileTest()
		{
			var result = _powerManager.ReserveHibernationFile();

			Assert.IsTrue(result);
		}

		[TestMethod]
		public void RemovetHibernationFileTest()
		{
			var result = _powerManager.RemoveHibernationFile();

			Assert.IsTrue(result);
		}

		[TestMethod]
		public void SuspendSystemTest()
		{
			_powerManager.SuspendSystem();
		}	
	}
}
