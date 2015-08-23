If Not IsNull(WScript.Arguments(0)) Then 
Set WshShell = WScript.CreateObject("WScript.Shell") 
WshShell.AppActivate("Google Chrome")
WshShell.SendKeys "{F5}"
End If
