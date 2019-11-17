
Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.Diagnostics

''' <summary>
''' Class MainForm.
''' </summary>
Partial Public Class MainForm
	Inherits Form

	Private m_CANopenLibHelper As New CANopenLibHelper()

	Private m_NodeID As Byte = &H1

	''' <summary>
	''' Set Action Enable
	''' </summary>
	Private Sub SetActionEnable()
		Dim _IsOpen As Boolean = m_CANopenLibHelper.IsOpen()
		Me.btn_Open.Enabled = Not _IsOpen
        'Me.btn_Close.Enabled = _IsOpen
        Me.cmb_Adapter.Enabled = Not _IsOpen
		Me.cmb_BitRate.Enabled = Not _IsOpen

		Me.btn_Enable.Enabled = _IsOpen
		Me.btn_Disable.Enabled = _IsOpen
		Me.btn_Close.Enabled = _IsOpen

    End Sub

	Public Sub DisplayCanMessage(ByVal bDirIsToDrive As Boolean, ByVal canMessage As CanMessage)
		'INSTANT VB NOTE: The variable text was renamed since Visual Basic does not handle local variables named the same as class members well:
		Dim text_Renamed As String = String.Empty
		If bDirIsToDrive Then
			If chk_ShowPCtoDrive.Checked = True Then
				text_Renamed = String.Format("PC->Drive: Msg={0:X3} Node ID={1:X2} Len={2:X} Data={3:X2} {4:X2} {5:X2} {6:X2} {7:X2} {8:X2} {9:X2} {10:X2} Time Stamp={11:D8}" & vbCrLf, (canMessage.id And &H780), (canMessage.id And &H7F), canMessage.dlc, canMessage.msg(0), canMessage.msg(1), canMessage.msg(2), canMessage.msg(3), canMessage.msg(4), canMessage.msg(5), canMessage.msg(6), canMessage.msg(7), canMessage.timeStamp)
			End If
		Else
			If chk_ShowDrivetoPC.Checked = True Then
				text_Renamed = String.Format("Drive->PC: Msg={0:X3} Node ID={1:X2} Len={2:X} Data={3:X2} {4:X2} {5:X2} {6:X2} {7:X2} {8:X2} {9:X2} {10:X2} Time Stamp={11:D8}" & vbCrLf, (canMessage.id And &H780), (canMessage.id And &H7F), canMessage.dlc, canMessage.msg(0), canMessage.msg(1), canMessage.msg(2), canMessage.msg(3), canMessage.msg(4), canMessage.msg(5), canMessage.msg(6), canMessage.msg(7), canMessage.timeStamp)
			End If
		End If
		AppendControlText(Me.txt_CommandHistory, text_Renamed)
	End Sub

	''' <summary>
	''' SendCanMessage
	''' </summary>
	''' <param name="CanMessage">canMessage</param>
	''' <returns>Return true if write command successfully, otherwise return false</returns>
	Public Function SendCanMessage(ByVal canMessage As CanMessage) As Boolean
		Dim ret As Boolean = m_CANopenLibHelper.Write(canMessage)

		If Not ret Then
			Return False
		End If
		Return True
	End Function


	''' <summary>
	''' SendCanMessage
	''' </summary>
	''' <param name="sSendCanMessage">sSendCanMessage</param>
	''' <param name="canFunction">canFunction</param>
	''' <param name="sReceivedCanMessage">sReceivedCanMessage</param>
	''' <param name="bMatchNodeID">bMatchNodeID</param>
	''' <param name="nNodeID">nNodeID</param>
	''' <param name="bMatchIndex">bMatchIndex</param>
	''' <param name="nIndex">nIndex</param>
	''' <param name="bMatchFirstByte">bMatchFirstByte</param>
	''' <param name="nFirstByte">nFirstByte</param>
	''' <param name="nTimeOut">nTimeOut</param>
	''' <returns>Return true if write command successfully, otherwise return false</returns>
	''' <remarks></remarks>
	''' 
	Public Function ExecuteCanMessage(ByVal sSendCanMessage As CanMessage, ByVal canFunction As Integer, ByRef sReceivedCanMessage As CanMessage, ByVal bMatchNodeID As Boolean, ByVal nNodeID As Byte, Optional ByVal bMatchIndex As Boolean = False, Optional ByVal nIndex As Integer = 0, Optional ByVal bMatchFirstByte As Boolean = False, Optional ByVal nFirstByte As Byte = 0, Optional ByVal nTimeOut As Integer = 30) As Boolean
		Dim ret As Boolean = m_CANopenLibHelper.ExecuteCommand(sSendCanMessage, sReceivedCanMessage, canFunction, bMatchNodeID, nNodeID, bMatchIndex, nIndex, bMatchFirstByte, nFirstByte)

		If Not ret Then
			Return False
		End If
		Return True
	End Function

	''' <summary>
	''' This procedure shows how to set RPDO and TPDO mapping
	''' </summary>
	Private Sub SetPDOMappingSettingsSample()
		Dim ret As Boolean = True
		Dim nNodeID As Byte = 1
		Dim nRPDONo As Byte	' (0~3) 1 means RPDO 2
		Dim nTPDONo As Byte	' (0~3) 1 means TPDO 2
		Dim nLen As Integer = 2

		'			
		'			this function is invalid if the drive is not in pre-operational mode.
		'			ret = m_CANopenLibHelper.SetToPreoperationalMode(nNodeID);
		'			

		Dim PdoMappingArr() As PDOMapping

		' RPDO 1
		nLen = 1
		nRPDONo = 0
		PdoMappingArr = New PDOMapping(nLen - 1) {}
		PdoMappingArr(0).Index = &H6040
		PdoMappingArr(0).SubIndex = 0
		PdoMappingArr(0).BitCounts = &H10
		ret = m_CANopenLibHelper.SetRPDOMapping(nNodeID, nRPDONo, nLen, PdoMappingArr)
		If ret = False Then
			Dim errorInfo As New ErrorInfo()
			m_CANopenLibHelper.GetLastErrorInfo(errorInfo)
			txt_CommandHistory.AppendText(String.Format("Fail to SetRPDOMapping. Error Code:{0}, Message:{1}" & vbCrLf, errorInfo.ErrorCode, errorInfo.ErrorMessage))
		End If

		' RPDO 2
		nLen = 2
		nRPDONo = 1
		PdoMappingArr = New PDOMapping(nLen - 1) {}
		PdoMappingArr(0).Index = &H6040
		PdoMappingArr(0).SubIndex = 0
		PdoMappingArr(0).BitCounts = &H10
		PdoMappingArr(1).Index = &H607A
		PdoMappingArr(1).SubIndex = 0
		PdoMappingArr(1).BitCounts = &H20
		ret = m_CANopenLibHelper.SetRPDOMapping(nNodeID, nRPDONo, nLen, PdoMappingArr)
		If ret = False Then
			txt_CommandHistory.AppendText(String.Format("SetRPDOMapping failed" & vbCrLf))
		End If

		' RPDO 3
		nLen = 2
		nRPDONo = 2
		PdoMappingArr = New PDOMapping(nLen - 1) {}
		PdoMappingArr(0).Index = &H6040
		PdoMappingArr(0).SubIndex = 0
		PdoMappingArr(0).BitCounts = &H10
		PdoMappingArr(1).Index = &H60FF
		PdoMappingArr(1).SubIndex = 0
		PdoMappingArr(1).BitCounts = &H20
		ret = m_CANopenLibHelper.SetRPDOMapping(nNodeID, nRPDONo, nLen, PdoMappingArr)
		If ret = False Then
			txt_CommandHistory.AppendText(String.Format("SetRPDOMapping failed" & vbCrLf))
		End If

		' RPDO 4
		nLen = 1
		nRPDONo = 3
		PdoMappingArr = New PDOMapping(nLen - 1) {}
		PdoMappingArr(0).Index = &H60FE
		PdoMappingArr(0).SubIndex = &H1
		PdoMappingArr(0).BitCounts = &H20
		ret = m_CANopenLibHelper.SetRPDOMapping(nNodeID, nRPDONo, nLen, PdoMappingArr)
		If ret = False Then
			txt_CommandHistory.AppendText(String.Format("SetRPDOMapping failed" & vbCrLf))
		End If

		' TPDO 1
		nLen = 1
		nTPDONo = 0
		PdoMappingArr = New PDOMapping(nLen - 1) {}
		PdoMappingArr(0).Index = &H6041
		PdoMappingArr(0).SubIndex = 0
		PdoMappingArr(0).BitCounts = &H10
		ret = m_CANopenLibHelper.SetTPDOMapping(nNodeID, nTPDONo, nLen, PdoMappingArr)
		If ret = False Then
			txt_CommandHistory.AppendText(String.Format("SetTPDOMapping failed" & vbCrLf))
		End If

		' TPDO 2
		nLen = 2
		nTPDONo = 1
		PdoMappingArr = New PDOMapping(nLen - 1) {}
		PdoMappingArr(0).Index = &H6041
		PdoMappingArr(0).SubIndex = 0
		PdoMappingArr(0).BitCounts = &H10
		PdoMappingArr(1).Index = &H700A
		PdoMappingArr(1).SubIndex = 0
		PdoMappingArr(1).BitCounts = &H20
		ret = m_CANopenLibHelper.SetTPDOMapping(nNodeID, nTPDONo, nLen, PdoMappingArr)
		If ret = False Then
			txt_CommandHistory.AppendText(String.Format("SetTPDOMapping failed" & vbCrLf))
		End If

		' TPDO 3
		nLen = 2
		nTPDONo = 2
		PdoMappingArr = New PDOMapping(nLen - 1) {}
		PdoMappingArr(0).Index = &H6041
		PdoMappingArr(0).SubIndex = 0
		PdoMappingArr(0).BitCounts = &H10
		PdoMappingArr(1).Index = &H7009
		PdoMappingArr(1).SubIndex = 0
		PdoMappingArr(1).BitCounts = &H10
		ret = m_CANopenLibHelper.SetTPDOMapping(nNodeID, nTPDONo, nLen, PdoMappingArr)
		If ret = False Then
			txt_CommandHistory.AppendText(String.Format("SetTPDOMapping failed" & vbCrLf))
		End If

		' TPDO 4
		nLen = 1
		nTPDONo = 3
		PdoMappingArr = New PDOMapping(nLen - 1) {}
		PdoMappingArr(0).Index = &H7003
		PdoMappingArr(0).SubIndex = 0
		PdoMappingArr(0).BitCounts = &H10
		ret = m_CANopenLibHelper.SetTPDOMapping(nNodeID, nTPDONo, nLen, PdoMappingArr)
		If ret = False Then
			txt_CommandHistory.AppendText(String.Format("SetTPDOMapping failed" & vbCrLf))
		End If
	End Sub

	''' <summary>
	''' This procedure shows how to write RPDO to the drive
	''' The drive must be in operational mode
	''' </summary>
	Private Sub WriteRPDOSample()
		Dim RPDOLen As Integer = 2
		Dim aData() As Byte = {&H6, &H0, &H0, &H0, &H0, &H0, &H0, &H0}
		Dim ret As Boolean = m_CANopenLibHelper.WriteRPDO(m_NodeID, 0, RPDOLen, aData)
		If ret = False Then
			' failure handle
			Dim errorInfo As New ErrorInfo()
			m_CANopenLibHelper.GetLastErrorInfo(errorInfo)
			MessageBox.Show(errorInfo.ErrorMessage)
		End If
	End Sub

	Public Sub New()
		InitializeComponent()
	End Sub

	Private Sub MainForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		Me.cmb_Adapter.SelectedIndex = 0
		Me.cmb_BitRate.SelectedIndex = 0



        AddHandler m_CANopenLibHelper.DataSent, AddressOf m_CANopenLibHelper_DataSent
		AddHandler m_CANopenLibHelper.DataReceived, AddressOf m_CANopenLibHelper_DataReceived
		AddHandler m_CANopenLibHelper.TPDOReceived, AddressOf m_CANopenLibHelper_TPDOReceived
	End Sub

	Private Delegate Sub m_dSetControlText(ByVal control As Control, ByVal text As String)

	Private Sub ThreadSetControlText(ByVal control As Control, ByVal text As String)
		control.Text = text
	End Sub

	Private Sub SetControlText(ByVal control As Control, ByVal text As String)
		If control.InvokeRequired = False Then
			ThreadSetControlText(control, text)
		Else
			Dim d As New m_dSetControlText(AddressOf ThreadSetControlText)
			control.BeginInvoke(d, New Object() {control, text})
		End If
	End Sub

	Private Delegate Sub m_dAppendControlText(ByVal textBox As TextBox, ByVal text As String)

	Private Sub ThreadAppendControlText(ByVal textBox As TextBox, ByVal text As String)
		textBox.AppendText(text)
	End Sub

	Private Sub AppendControlText(ByVal textBox As TextBox, ByVal text As String)
		If textBox.InvokeRequired = False Then
			ThreadAppendControlText(textBox, text)
		Else
			Dim d As New m_dAppendControlText(AddressOf ThreadAppendControlText)
			textBox.BeginInvoke(d, New Object() {textBox, text})
		End If
	End Sub

	Private Delegate Sub m_dShowHeartBeat(ByVal canMessage As CanMessage)



    Private Sub m_CANopenLibHelper_DataSent(ByVal e As CanMessageEventHandle)
		DisplayCanMessage(True, e.CanMessage)
	End Sub

	Private Sub m_CANopenLibHelper_DataReceived(ByVal e As CanMessageEventHandle)
		DisplayCanMessage(False, e.CanMessage)
	End Sub

	Private Sub m_CANopenLibHelper_TPDOReceived(ByVal e As TPDOReceivedEventHandle)

	End Sub

	Private Sub GetFirmwareRevision()
		Dim _SendCanMessage As CanMessage = New CanMessage With {.id = &H600 + m_NodeID, .dlc = 8, .msg = New Byte(7) {&H40, &HA, &H10, 0, 0, 0, 0, 0}}
		Dim _ReceivedCanMessage As CanMessage = New CanMessage With {.msg = New Byte(7) {0, 0, 0, 0, 0, 0, 0, 0}}

		Dim n As Integer = System.Runtime.InteropServices.Marshal.SizeOf(GetType(CanMessage))

		Dim ret As Boolean = ExecuteCanMessage(_SendCanMessage, &H580, _ReceivedCanMessage, True, m_NodeID)

		If ret = True Then
			Dim m_ARMRevision As String = System.Text.ASCIIEncoding.Default.GetString(New Byte() {_ReceivedCanMessage.msg(4), _ReceivedCanMessage.msg(5), _ReceivedCanMessage.msg(6), _ReceivedCanMessage.msg(7)}).Insert(1, ".")

            'SetControlText(Me.lbl_ARMRev, m_ARMRevision)
        Else
            'SetControlText(Me.lbl_ARMRev, String.Empty)
        End If

		_SendCanMessage = New CanMessage With {.id = &H600 + m_NodeID, .dlc = 8, .msg = New Byte(7) {&H40, &H18, &H10, &H2, 0, 0, 0, 0}}
		_ReceivedCanMessage = New CanMessage With {.msg = New Byte(7) {0, 0, 0, 0, 0, 0, 0, 0}}
		ret = ExecuteCanMessage(_SendCanMessage, &H580, _ReceivedCanMessage, True, m_NodeID)

		If ret = True Then
			Dim m_DSPRevision As String = String.Format("1.0{0}{1}", ChrW(_ReceivedCanMessage.msg(6) + 48), ChrW(_ReceivedCanMessage.msg(4)))

            'SetControlText(Me.lbl_DSPRev, m_DSPRevision)
        Else
            'SetControlText(Me.lbl_DSPRev, String.Empty)
        End If
	End Sub

	Private Sub btn_Open_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_Open.Click
		Dim ret As Boolean = True
        'Selected USBCAN1
        If Me.cmb_Adapter.SelectedIndex = 0 Then
            ret = m_CANopenLibHelper.Open(EnumAdapter.ZLG, EnumBaudRate.BitRate1Mbps, CInt(Fix(ZLGDeviceType.USBCAN1)))
        End If

        'If false, display error
        If ret = False Then
			MessageBox.Show("Fail to open CANopen adapter.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information)
		End If

        SetActionEnable()
        AppendControlText(Me.txt_CommandHistory, "Open Device Success!" + vbLf)
        ' MessageBox.Show("Open Device Success!1")
        GetFirmwareRevision()
        'MessageBox.Show("Open Device Success!2")


    End Sub



    Private Sub btn_Enable_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_Enable.Click
		Dim ret As Boolean = m_CANopenLibHelper.DriveEnable(m_NodeID, True)
	End Sub

	Private Sub btn_Disable_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_Disable.Click
		Dim ret As Boolean = m_CANopenLibHelper.DriveEnable(m_NodeID, False)
	End Sub

	Private Sub btn_Clear_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_Clear.Click
		Me.txt_CommandHistory.Clear()
	End Sub


    Private Sub btn_Operational_Click(ByVal sender As Object, ByVal e As EventArgs)
        m_CANopenLibHelper.SetToOperationalMode(m_NodeID)
    End Sub

    Private Sub btn_PreOperational_Click(ByVal sender As Object, ByVal e As EventArgs)
        m_CANopenLibHelper.SetToPreoperationalMode(m_NodeID)
    End Sub

    Private Sub btn_StopAll_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btn_StopAll.Click
        Dim ret As Boolean = m_CANopenLibHelper.Stop(m_NodeID)
    End Sub



    Private Sub btn_StopAbsoluteMove_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.btn_StopAll_Click(Nothing, Nothing)
    End Sub



    Private Sub btn_StopRelativeMove_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.btn_StopAll_Click(Nothing, Nothing)
    End Sub






    Private Sub btn_StopHoming_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.btn_StopAll_Click(Nothing, Nothing)
    End Sub



    Private Sub btn_StopQMode_Click(ByVal sender As Object, ByVal e As EventArgs)
        Me.btn_StopAll_Click(Nothing, Nothing)
    End Sub

    Private Sub btn_AdapterType_Click(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

    Private Sub btn_AlarmReset_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim ret As Boolean = m_CANopenLibHelper.AlarmReset(m_NodeID)
    End Sub

    Private Sub btn_Close_Click(sender As Object, e As EventArgs) Handles btn_Close.Click
        Dim ret As Boolean = m_CANopenLibHelper.Close()

        SetActionEnable()
        AppendControlText(Me.txt_CommandHistory, "Device Closed!" + vbLf)
        'MessageBox.Show("Device Closed")

    End Sub
End Class
