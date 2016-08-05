using System;
using System.Runtime.InteropServices;

namespace PowerManagerCOM
{
	[ComVisible(true)]
	[Guid("7CEFE7A0-E2DB-49FB-8A90-A2FC3A9578CD")]
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	public interface IPowerManagerCom
	{
		DateTime GetLastSleepTime();

		DateTime GetLastWakeTime();
	}
}
