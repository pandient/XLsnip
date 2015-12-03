   Dim xl  
   Dim a
   Dim i
   Dim nm
   Dim WshShell, strCurDir

	Set WshShell = CreateObject("WScript.Shell")
	strCurDir = WshShell.CurrentDirectory
	Set WshShell = Nothing   


    Set xl = CreateObject("Excel.Application")
	xl.Visible = false
	xl.DisplayAlerts = false

	For i = 1 To xl.Addins.Count
		Set a = xl.Addins.item(i)
		nm = UCase(a.Name)
		If nm = ucase("XLRangeLoader.XLAM") Then
			if a.Installed = false then
				a.Installed = true
			End If
            set a = nothing
			Exit For
		End If
		set a = nothing
	Next
	
	If i > xl.Addins.Count Then
		xl.Workbooks.Open(strCurDir + "\XLRangeLoader.xlam")
		XL.Workbooks("XLRangeLoader.xlam").RunAutoMacros 1
	End If
	
	xl.Quit
	Set xl = Nothing