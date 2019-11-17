
Imports System
Imports System.Collections.Generic
Imports System.Text

Public Class TPDOReceivedEventHandle
	Inherits EventArgs

	Public Sub New(ByVal _TPDOMessage As PDOMessage)
		Me.TPDOMessage = _TPDOMessage
	End Sub

	' The fire event will have two pieces of information-- 
	' 1) Where the fire is, and 2) how "ferocious" it is.  

	Public Property TPDOMessage() As PDOMessage
End Class 'end of class FireEventArgs
