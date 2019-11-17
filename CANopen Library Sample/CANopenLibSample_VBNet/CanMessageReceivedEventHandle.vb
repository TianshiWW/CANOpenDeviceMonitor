
Imports System
Imports System.Collections.Generic
Imports System.Text

Public Class CanMessageEventHandle
	Inherits EventArgs

	Public Sub New(ByVal _CanMessage As CanMessage)
		Me.CanMessage = _CanMessage
	End Sub

	' The fire event will have two pieces of information-- 
	' 1) Where the fire is, and 2) how "ferocious" it is.  

	Public Property CanMessage() As CanMessage
End Class 'end of class FireEventArgs
