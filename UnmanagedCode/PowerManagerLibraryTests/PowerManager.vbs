dim myObj
Set myObj = CreateObject("PowerManagerCOM.PowerManagerCom")
MsgBox myObj.GetLastSleepTime()