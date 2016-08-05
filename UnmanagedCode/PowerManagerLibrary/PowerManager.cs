﻿using System;
using System.Runtime.InteropServices;

namespace PowerManagerLibrary
{
	public class PowerManager
	{
		public DateTime GetLastSleepTime()
		{
			return GetLastTime(PowerInformationLevel.LastSleepTime);
		}

		public DateTime GetLastWakeTime()
		{
			return GetLastTime(PowerInformationLevel.LastWakeTime);
		}

		public SystemBatteryState GetSystemBatteryState()
		{
			return GetSystemInformation<SystemBatteryState>(PowerInformationLevel.SystemBatteryState);
		}

		public SystemPowerInformation GetSystemPowerInformation()
		{
			return GetSystemInformation<SystemPowerInformation>(PowerInformationLevel.SystemPowerInformation);
		}

		public bool ReserveHibernationFile()
		{
			return ManageHibernationFile(true);
		}

		public bool RemoveHibernationFile()
		{
			return ManageHibernationFile(false);
		}

		public void SuspendSystem()
		{
			SetSuspendState(false, false, false);
		}

		// Get system's uptime 
		[DllImport("kernel32")]
		public static extern ulong GetTickCount64();

		[DllImport("powrprof.dll")]
		private static extern uint CallNtPowerInformation(
			int informationLevel,
			IntPtr inputBuffer,
			uint inputBufferSize,
			[Out] IntPtr outputBuffer,
			uint outputBufferSize);

		[DllImport("powrprof.dll")]
		private static extern bool SetSuspendState(bool hibernate, bool forceCritical, bool disableWakeEvent);

		private DateTime GetLastTime(PowerInformationLevel level)
		{
			int outputBufferSize = Marshal.SizeOf(typeof(ulong));
			IntPtr outputBuffer = Marshal.AllocHGlobal(outputBufferSize);

			CallNtPowerInformation((int)level, IntPtr.Zero, 0, outputBuffer, (uint)outputBufferSize);

			// The interrupt-time count, in 100-nanosecond units
			long ticksCount = Marshal.ReadInt64(outputBuffer);
			Marshal.FreeHGlobal(outputBuffer);
			var systemStartupTime = GetTickCount64() * 1000000 / 100;

			return DateTime.UtcNow - TimeSpan.FromTicks((long)systemStartupTime) + TimeSpan.FromTicks(ticksCount);
		}

		private T GetSystemInformation<T>(PowerInformationLevel level)
		{
			int outputBufferSize = Marshal.SizeOf(typeof(T));
			IntPtr outputBuffer = Marshal.AllocHGlobal(outputBufferSize);

			CallNtPowerInformation((int)level, IntPtr.Zero, 0, outputBuffer, (uint)outputBufferSize);

			var powerInformation = Marshal.PtrToStructure<T>(outputBuffer);
			Marshal.FreeHGlobal(outputBuffer);

			return powerInformation;
		}

		private bool ManageHibernationFile(bool reserve)
		{
			int inputBufferSize = Marshal.SizeOf(typeof(int));
			IntPtr inputBuffer = Marshal.AllocHGlobal(inputBufferSize);

			Marshal.WriteInt32(inputBuffer, 0, reserve ? 1 : 0);
			var status = CallNtPowerInformation((int)PowerInformationLevel.SystemReserveHiberFile, inputBuffer, (uint)inputBufferSize, IntPtr.Zero, 0);

			Marshal.FreeHGlobal(inputBuffer);

			return status == 0;
		}
	}
}
