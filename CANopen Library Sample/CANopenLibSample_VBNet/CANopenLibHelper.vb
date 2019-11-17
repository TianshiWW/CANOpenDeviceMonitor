' ************************************************************************************************
' File Name			: CANopenLibHelper.vb
' Copyright			: 2017, Shanghai AMP & MOONS' Automation Co., Ltd., All rights reserved.
' Module Name		: CANopen Library C# helper
' Author			: Lei Youbing
' Created			: 2016-11-03
'
' Revision History
' No	Version		Date		Revised By		Description
' 1		1.0.17.0223	2017-02-23	Lei Youbing		First released.
' 2		1.0.17.1009	2017-10-09	Lei Youbing		Added canlib32.dll to the included files.
'
' ************************************************************************************************

Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Runtime.InteropServices

Public Enum EnumAdapter
	Kvaser
	PEAK
	ZLG
End Enum

Public Enum EnumBaudRate
	BitRate1Mbps
	BitRate800kbps
	BitRate500kbps
	BitRate250kbps
	BitRate125kbps
	BitRate50kbps
	BitRate20kbps
	BitRate12D5kbps
End Enum

<StructLayout(LayoutKind.Sequential)> _
Public Structure CanMessage
	Public id As Integer
	Public dlc As UInteger
	<MarshalAs(UnmanagedType.ByValArray, SizeConst:=8)> _
	Public msg() As Byte
	Public flag As UInteger
	Public timeStamp As UInteger
End Structure

<StructLayout(LayoutKind.Sequential)> _
Public Structure PDOMessage
	Public NodeID As Byte
	Public No As Byte
	Public Len As Byte
	<MarshalAs(UnmanagedType.ByValArray, SizeConst:=8)> _
	Public msg() As Byte
End Structure

Public Structure ErrorInfo
	Public ErrorCode As Integer
	Public Command As String
	Public ErrorMessage As String
End Structure

<StructLayout(LayoutKind.Sequential)> _
Public Structure PDOMapping
	Public Index As Integer
	Public SubIndex As Byte
	Public BitCounts As Byte
End Structure

''' <summary>
''' PEAKDeviceType
''' </summary>
Public Enum KvaserDeviceType
	''' <summary>
	''' Unknown or undefined
	''' </summary>
	HWTYPE_NONE = 0
    ''' <summary>
    '''' The virtual CAN bus
    '''' </summary>
    'HWTYPE_VIRTUAL = 1
    '''' <summary>
    '''' LAPcan Family
    '''' </summary>
    'HWTYPE_LAPCAN = 2
    '''' <summary>
    '''' CANpari (obsolete).
    '''' </summary>
    'HWTYPE_CANPARI = 3
    '''' <summary>
    '''' PCcan Family
    '''' </summary>
    'HWTYPE_PCCAN = 8
    '''' <summary>
    '''' PCIcan Family
    '''' </summary>
    'HWTYPE_PCICAN = 9
    '''' <summary>
    '''' USBcan (obsolete).
    '''' </summary>
    'HWTYPE_USBCAN = 11
    '''' <summary>
    '''' PCIcan II family
    '''' </summary>
    'HWTYPE_PCICAN_II = 40
    '''' <summary>
    '''' USBcan II, USBcan Rugged, Kvaser Memorator
    '''' </summary>
    'HWTYPE_USBCAN_II = 42
    '''' <summary>
    '''' Simulated CAN bus for Kvaser Creator (obsolete).
    '''' </summary>
    'HWTYPE_SIMULATED = 44
    '''' <summary>
    '''' Kvaser Acquisitor (obsolete).
    '''' </summary>
    'HWTYPE_ACQUISITOR = 46
    '''' <summary>
    '''' Kvaser Leaf Family
    '''' </summary>
    'HWTYPE_LEAF = 48
    '''' <summary>
    '''' Kvaser PC104+
    '''' </summary>
    'HWTYPE_PC104_PLUS = 50
    '''' <summary>
    '''' Kvaser PCIcanx II
    '''' </summary>
    'HWTYPE_PCICANX_II = 52
    '''' <summary>
    '''' Kvaser Memorator Professional family
    '''' </summary>
    'HWTYPE_MEMORATOR_II = 54
    '''' <summary>
    '''' Kvaser Memorator Professional family
    '''' </summary>
    'HWTYPE_MEMORATOR_PRO = 54
    '''' <summary>
    '''' Kvaser USBcan Professional
    '''' </summary>
    'HWTYPE_USBCAN_PRO = 56
    '''' <summary>
    '''' Obsolete name, use canHWTYPE_BLACKBIRD instead
    '''' </summary>
    'HWTYPE_IRIS = 58
    '''' <summary>
    '''' Kvaser BlackBird
    '''' </summary>
    'HWTYPE_BLACKBIRD = 58
    '''' <summary>
    '''' Kvaser Memorator Light
    '''' </summary>
    'HWTYPE_MEMORATOR_LIGHT = 60
    '''' <summary>
    '''' Obsolete name, use canHWTYPE_EAGLE instead
    '''' </summary>
    'HWTYPE_MINIHYDRA = 62
    '''' <summary>
    '''' Kvaser Eagle family
    '''' </summary>
    'HWTYPE_EAGLE = 62
    '''' <summary>
    '''' Obsolete name, use canHWTYPE_BLACKBIRD_V2 instead
    '''' </summary>
    'HWTYPE_BAGEL = 64
    '''' <summary>
    '''' Kvaser BlackBird v2
    '''' </summary>
    'HWTYPE_BLACKBIRD_V2 = 64
    '''' <summary>
    '''' Kvaser Mini PCI Express
    '''' </summary>
    'HWTYPE_MINIPCIE = 66
    '''' <summary>
    '''' USBcan Pro HS/K-Line
    '''' </summary>
    'HWTYPE_USBCAN_KLINE = 68
    '''' <summary>
    '''' Kvaser Ethercan
    '''' </summary>
    'HWTYPE_ETHERCAN = 70
    '''' <summary>
    '''' Kvaser USBcan Light
    '''' </summary>
    'HWTYPE_USBCAN_LIGHT = 72
    '''' <summary>
    '''' Kvaser USBcan Pro 5xHS and variants
    '''' </summary>
    'HWTYPE_USBCAN_PRO2 = 74
    '''' <summary>
    '''' Kvaser PCIEcan 4xHS and variants
    '''' </summary>
    'HWTYPE_PCIE_V2 = 76
    '''' <summary>
    '''' Kvaser Memorator Pro 5xHS and variants
    '''' </summary>
    'HWTYPE_MEMORATOR_PRO2 = 78
    '''' <summary>
    '''' Kvaser Leaf Pro HS v2 and variants
    '''' </summary>
    'HWTYPE_LEAF2 = 80
    '''' <summary>
    '''' Kvaser Memorator (2nd generation)
    '''' </summary>
    'HWTYPE_MEMORATOR_V2 = 82
End Enum

''' <summary>
''' PEAKDeviceType
''' </summary>
Public Enum PEAKDeviceType
	''' <summary>
	''' Undefined/default value for a PCAN bus
	''' </summary>
	PCAN_NONEBUS = &H0

    '''' <summary>
    '''' PCAN-ISA interface, channel 1
    '''' </summary>
    'PCAN_ISABUS1 = &H21
    '''' <summary>
    '''' PCAN-ISA interface, channel 2
    '''' </summary>
    'PCAN_ISABUS2 = &H22
    '''' <summary>
    '''' PCAN-ISA interface, channel 3
    '''' </summary>
    'PCAN_ISABUS3 = &H23
    '''' <summary>
    '''' PCAN-ISA interface, channel 4
    '''' </summary>
    'PCAN_ISABUS4 = &H24
    '''' <summary>
    '''' PCAN-ISA interface, channel 5
    '''' </summary>
    'PCAN_ISABUS5 = &H25
    '''' <summary>
    '''' PCAN-ISA interface, channel 6
    '''' </summary>
    'PCAN_ISABUS6 = &H26
    '''' <summary>
    '''' PCAN-ISA interface, channel 7
    '''' </summary>
    'PCAN_ISABUS7 = &H27
    '''' <summary>
    '''' PCAN-ISA interface, channel 8
    '''' </summary>
    'PCAN_ISABUS8 = &H28

    '''' <summary>
    '''' PPCAN-Dongle/LPT interface, channel 1 
    '''' </summary>
    'PCAN_DNGBUS1 = &H31

    '''' <summary>
    '''' PCAN-PCI interface, channel 1
    '''' </summary>
    'PCAN_PCIBUS1 = &H41
    '''' <summary>
    '''' PCAN-PCI interface, channel 2
    '''' </summary>
    'PCAN_PCIBUS2 = &H42
    '''' <summary>
    '''' PCAN-PCI interface, channel 3
    '''' </summary>
    'PCAN_PCIBUS3 = &H43
    '''' <summary>
    '''' PCAN-PCI interface, channel 4
    '''' </summary>
    'PCAN_PCIBUS4 = &H44
    '''' <summary>
    '''' PCAN-PCI interface, channel 5
    '''' </summary>
    'PCAN_PCIBUS5 = &H45
    '''' <summary>
    '''' PCAN-PCI interface, channel 6
    '''' </summary>
    'PCAN_PCIBUS6 = &H46
    '''' <summary>
    '''' PCAN-PCI interface, channel 7
    '''' </summary>
    'PCAN_PCIBUS7 = &H47
    '''' <summary>
    '''' PCAN-PCI interface, channel 8
    '''' </summary>
    'PCAN_PCIBUS8 = &H48

    '''' <summary>
    '''' PCAN-USB interface, channel 1
    '''' </summary>
    'PCAN_USBBUS1 = &H51
    '''' <summary>
    '''' PCAN-USB interface, channel 2
    '''' </summary>
    'PCAN_USBBUS2 = &H52
    '''' <summary>
    '''' PCAN-USB interface, channel 3
    '''' </summary>
    'PCAN_USBBUS3 = &H53
    '''' <summary>
    '''' PCAN-USB interface, channel 4
    '''' </summary>
    'PCAN_USBBUS4 = &H54
    '''' <summary>
    '''' PCAN-USB interface, channel 5
    '''' </summary>
    'PCAN_USBBUS5 = &H55
    '''' <summary>
    '''' PCAN-USB interface, channel 6
    '''' </summary>
    'PCAN_USBBUS6 = &H56
    '''' <summary>
    '''' PCAN-USB interface, channel 7
    '''' </summary>
    'PCAN_USBBUS7 = &H57
    '''' <summary>
    '''' PCAN-USB interface, channel 8
    '''' </summary>
    'PCAN_USBBUS8 = &H58

    '''' <summary>
    '''' PCAN-PC Card interface, channel 1
    '''' </summary>
    'PCAN_PCCBUS1 = &H61
    '''' <summary>
    '''' PCAN-PC Card interface, channel 2
    '''' </summary>
    'PCAN_PCCBUS2 = &H62
End Enum

''' <summary>
''' ZLGDeviceType
''' </summary>
Public Enum ZLGDeviceType
    'PCI5121 = 1
    'PCI9810 = 2
    USBCAN1 = 3
    'USBCAN2 = 4
    'USBCAN2A = 4
    'PCI9820 = 5
    'CAN232 = 6
    'PCI5110 = 7
    'CANLITE = 8
    'ISA9620 = 9
    'ISA5420 = 10
    'PC104CAN = 11
    'CANETUDP = 12
    'CANETE = 12
    'DNP9810 = 13
    'PCI9840 = 14
    'PC104CAN2 = 15
    'PCI9820I = 16
    'CANETTCP = 17
    'PEC9920 = 18
    'PCIE_9220 = 18
    'PCI5010U = 19
    'USBCAN_E_U = 20
    'USBCAN_2E_U = 21
    'PCI5020U = 22
    'EG20T_CAN = 23
    'PCIE9221 = 24
    'WIFICAN_TCP = 25
    'WIFICAN_UDP = 26
    'PCIe9120 = 27
    'PCIe9110 = 28
    'PCIe9140 = 29
End Enum

Public Class CANopenLibHelper

	Public Delegate Sub EventCallback()

	Private Const DLL_FILENAME As String = "CANopenLib_x86.dll"	' for 64-bit windows,, please comment this line and uncomment next line
	'Private Const DLL_FILENAME As String = "CANopenLib_x64.dll"	  ' for 32-bit windows, please comment this line and uncomment previous line

#Region "public fields"
	Public MBERROR_OK As Integer = 0 ' OK
	'standard specific
	Public MBERROR_FUNC_CODE As Integer = 1
	Public MBERROR_DATA_ADDR As Integer = 2

	'manufacture specific
	Public MBERROR_CAN_NOT_READ As Integer = 17
	Public MBERROR_CAN_NOT_WRITE As Integer = 18
	Public MBERROR_DATA_RANG As Integer = 19

	'other
	Public MBERROR_EXCEPTION As Integer = 30
	Public MBERROR_PORT_IS_CLOSED As Integer = 101
	Public MBERROR_NO_RESPONSE As Integer = 102
	Public MBERROR_RESPONSE_NOT_ENOUGH As Integer = 103
	Public MBERROR_DATA_ERROR As Integer = 104

	Public MBERROR_OpenFAILED As Integer = 105
	Public MBERROR_PORTISNOTOPEN As Integer = 106
	Public MBERROR_NORESPONSE As Integer = 107
	Public MBERROR_INCORRECTRESPONSE As Integer = 108
	Public MBERROR_CHECKSUMERROR As Integer = 109
	Public MBERROR_SCLREGISTER_NOTFOUND As Integer = 110
#End Region

#Region "DllImport"

	<DllImport(DLL_FILENAME, EntryPoint:="OnHeartBeatReceive", CharSet:=CharSet.Ansi)> _
	Private Shared Sub _OnHeartBeatReceive(ByVal callback As EventCallback)
	End Sub

	<DllImport(DLL_FILENAME, EntryPoint:="OnDataSend", CharSet:=CharSet.Ansi)> _
	Private Shared Sub _OnDataSend(ByVal callback As EventCallback)
	End Sub

	<DllImport(DLL_FILENAME, EntryPoint:="OnDataReceive", CharSet:=CharSet.Ansi)> _
	Private Shared Sub _OnDataReceive(ByVal callback As EventCallback)
	End Sub

	<DllImport(DLL_FILENAME, EntryPoint:="OnTPDOReceived", CharSet:=CharSet.Ansi)> _
	Private Shared Sub _OnTPDOReceived(ByVal callback As EventCallback)
	End Sub

	<DllImport(DLL_FILENAME, EntryPoint:="GetLastHeartBeatMessage", CharSet:=CharSet.Ansi)> _
	Private Shared Function _GetLastHeartBeatMessage(ByRef CANMessage As CanMessage) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="GetLastSentMessage", CharSet:=CharSet.Ansi)> _
	Private Shared Function _GetLastSentMessage(ByRef CANMessage As CanMessage) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="GetLastReceivedMessage", CharSet:=CharSet.Ansi)> _
	Private Shared Function _GetLastReceivedMessage(ByRef CANMessage As CanMessage) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="GetLastTPDOMessage", CharSet:=CharSet.Ansi)> _
	Private Shared Function _GetLastTPDOMessage(ByRef PDOMessage As PDOMessage) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="GetLastTPDOMessageByNodeID", CharSet:=CharSet.Ansi)> _
	Private Shared Function _GetLastTPDOMessageByNodeID(ByRef nNodeID As Byte, ByRef nPDONo As Byte, ByRef PDOMessage As PDOMessage) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="Open", CharSet:=CharSet.Ansi)> _
	Private Shared Function _Open(ByVal adapter As Integer, ByVal nBaudRate As Integer, ByVal nChannel As Byte) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="IsOpen", CharSet:=CharSet.Ansi)> _
	Private Shared Function _IsOpen() As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="Close", CharSet:=CharSet.Ansi)> _
	Private Shared Function _Close() As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="SetExecuteTimeOut", CharSet:=CharSet.Ansi)> _
	Private Shared Sub _SetExecuteTimeOut(ByVal nExecuteTimeOut As Byte)
	End Sub

	<DllImport(DLL_FILENAME, EntryPoint:="GetExecuteTimeOut", CharSet:=CharSet.Ansi)> _
	Private Shared Function _GetExecuteTimeOut() As Byte
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="SetExecuteRetryTimes", CharSet:=CharSet.Ansi)> _
	Private Shared Sub _SetExecuteRetryTimes(ByVal nExecuteRetryTimes As Byte)
	End Sub

	<DllImport(DLL_FILENAME, EntryPoint:="GetExecuteRetryTimes", CharSet:=CharSet.Ansi)> _
	Private Shared Function _GetExecuteRetryTimes() As Byte
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ResetBuffer", CharSet:=CharSet.Ansi)> _
	Private Shared Function _ResetBuffer() As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="Write", CharSet:=CharSet.Ansi)> _
	Private Shared Function _Write(ByVal sSendCanMessage As CanMessage) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ExecuteCommand", CharSet:=CharSet.Ansi)> _
	Private Shared Function _ExecuteCommand(ByVal sSendCanMessage As CanMessage, ByVal pReceivedCanMessage As IntPtr, ByVal nCanFunction As Integer, ByVal bMatchNodeID As Boolean, ByVal nNodeID As Byte, Optional ByVal bMatchIndex As Boolean = False, Optional ByVal nIndex As Integer = 0, Optional ByVal bMatchFirstByte As Boolean = False, Optional ByVal nFirstByte As Byte = 0) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ClearSendBuffer", CharSet:=CharSet.Ansi)> _
	Private Shared Function _ClearSendBuf() As Integer
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ClearReceivedBuffer", CharSet:=CharSet.Ansi)> _
	Private Shared Function _ClearReceivedBuf() As Integer
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ClearBuffer", CharSet:=CharSet.Ansi)> _
	Private Shared Function _ClearBuffer() As Integer
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadSDOInt8", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadSDOInt8(ByVal nNodeID As Byte, ByVal nIndex As Integer, ByVal nSubIndex As Byte, ByRef nData As SByte) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadSDOUInt8", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadSDOUInt8(ByVal nNodeID As Byte, ByVal nIndex As Integer, ByVal nSubIndex As Byte, ByRef nData As Byte) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadSDOInt16", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadSDOInt16(ByVal nNodeID As Byte, ByVal nIndex As Integer, ByVal nSubIndex As Byte, ByRef nData As Short) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadSDOUInt16", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadSDOUInt16(ByVal nNodeID As Byte, ByVal nIndex As Integer, ByVal nSubIndex As Byte, ByRef nData As UShort) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadSDOInt32", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadSDOInt32(ByVal nNodeID As Byte, ByVal nIndex As Integer, ByVal nSubIndex As Byte, ByRef nData As Integer) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadSDOUInt32", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadSDOUInt32(ByVal nNodeID As Byte, ByVal nIndex As Integer, ByVal nSubIndex As Byte, ByRef nData As UInteger) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WriteSDOInt8", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WriteSDOInt8(ByVal nNodeID As Byte, ByVal nIndex As Integer, ByVal nSubIndex As Byte, ByVal nData As SByte) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WriteSDOUInt8", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WriteSDOUInt8(ByVal nNodeID As Byte, ByVal nIndex As Integer, ByVal nSubIndex As Byte, ByVal nData As Byte) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WriteSDOInt16", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WriteSDOInt16(ByVal nNodeID As Byte, ByVal nIndex As Integer, ByVal nSubIndex As Byte, ByVal nData As Short) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WriteSDOUInt16", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WriteSDOUInt16(ByVal nNodeID As Byte, ByVal nIndex As Integer, ByVal nSubIndex As Byte, ByVal nData As UShort) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WriteSDOInt32", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WriteSDOInt32(ByVal nNodeID As Byte, ByVal nIndex As Integer, ByVal nSubIndex As Byte, ByVal nData As Integer) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WriteSDOUInt32", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WriteSDOUInt32(ByVal nNodeID As Byte, ByVal nIndex As Integer, ByVal nSubIndex As Byte, ByVal nData As UInteger) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="GetLastErrorInfo", CharSet:=CharSet.Ansi)> _
	Private Shared Sub _GetLastErrorInfo(ByRef errorInfo As ErrorInfo)
	End Sub

	<DllImport(DLL_FILENAME, EntryPoint:="SetToPreoperationalMode", CharSet:=CharSet.Ansi)> _
	Public Shared Function _SetToPreoperationalMode(ByVal nNodeID As Byte) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="SetToOperationalMode", CharSet:=CharSet.Ansi)> _
	Public Shared Function _SetToOperationalMode(ByVal nNodeID As Byte) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="SetRPDOMapping", CharSet:=CharSet.Ansi)> _
	Private Shared Function _SetRPDOMapping(ByVal nNodeID As Byte, ByVal nRPDONo As Byte, ByVal nLen As Integer, ByVal PDOMappingInfo() As PDOMapping) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="SetTPDOMapping", CharSet:=CharSet.Ansi)> _
	Private Shared Function _SetTPDOMapping(ByVal nNodeID As Byte, ByVal nTPDONo As Byte, ByVal nLen As Integer, ByVal PDOMappingInfo() As PDOMapping) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="RestorePDOMappingSettings", CharSet:=CharSet.Ansi)> _
	Public Shared Function _RestorePDOMappingSettings(ByVal nNodeID As Byte) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WriteRPDO", CharSet:=CharSet.Ansi)> _
	Private Shared Function _WriteRPDO(ByVal nNodeID As Byte, ByVal nRPDONo As Byte, ByVal nLen As Integer, ByVal aData() As Byte) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="SaveParameters", CharSet:=CharSet.Ansi)> _
	Private Shared Function _SaveParameters(ByVal nNodeID As Byte) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadPositionGain", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadPositionGain(ByVal nNodeID As Byte, ByRef nPositionGain As UShort) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WritePositionGain", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WritePositionGain(ByVal nNodeID As Byte, ByVal nPositionGain As UShort) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadPositionDeriGain", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadPositionDeriGain(ByVal nNodeID As Byte, ByRef nPositionDeriGain As UShort) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WritePositionDeriGain", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WritePositionDeriGain(ByVal nNodeID As Byte, ByVal nPositionDeriGain As UShort) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadPositionDeriFilter", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadPositionDeriFilter(ByVal nNodeID As Byte, ByRef nPositionDeriFilter As UShort) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WritePositionDeriFilter", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WritePositionDeriFilter(ByVal nNodeID As Byte, ByVal nPositionDeriFilter As UShort) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadVelocityGain", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadVelocityGain(ByVal nNodeID As Byte, ByRef nVelocityGain As UShort) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WriteVelocityGain", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WriteVelocityGain(ByVal nNodeID As Byte, ByVal nVelocityGain As UShort) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadVelocityIntegGain", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadVelocityIntegGain(ByVal nNodeID As Byte, ByRef nVelocityIntegGain As UShort) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WriteVelocityIntegGain", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WriteVelocityIntegGain(ByVal nNodeID As Byte, ByVal nVelocityIntegGain As UShort) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadAccFeedForward", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadAccFeedForward(ByVal nNodeID As Byte, ByRef nAccFeedForward As UShort) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WriteAccFeedForward", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WriteAccFeedForward(ByVal nNodeID As Byte, ByVal nAccFeedForward As UShort) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadPIDFilter", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadPIDFilter(ByVal nNodeID As Byte, ByRef nPIDFilter As UShort) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WritePIDFilter", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WritePIDFilter(ByVal nNodeID As Byte, ByVal nPIDFilter As UInteger) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadNotchFilter", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadNotchFilter(ByVal nNodeID As Byte, ByVal nIndex As Integer, ByRef nNotchFilter As Short) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WriteNotchFilter", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WriteNotchFilter(ByVal nNodeID As Byte, ByVal nIndex As Integer, ByVal nNotchFilter As Short) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadPositionError", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadPositionError(ByVal nNodeID As Byte, ByRef nPositionError As UShort) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WritePositionError", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WritePositionError(ByVal nNodeID As Byte, ByVal nPositionError As UShort) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadVelocityMax", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadVelocityMax(ByVal nNodeID As Byte, ByRef nVelocityMax As UShort) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WriteVelocityMax", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WriteVelocityMax(ByVal nNodeID As Byte, ByVal nVelocityMax As UShort) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadSmoothFilter", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadSmoothFilter(ByVal nNodeID As Byte, ByRef nSmoothFilter As UShort) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WriteSmoothFilter", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WriteSmoothFilter(ByVal nNodeID As Byte, ByVal nSmoothFilter As UShort) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadDriverTemp", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadDriverTemp(ByVal nNodeID As Byte, ByRef nDriverTemp As Short) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadErrorCode", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadErrorCode(ByVal nNodeID As Byte, ByRef nErrorCode As UShort) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadErrorCodeUpper", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadErrorCodeUpper(ByVal nNodeID As Byte, ByRef nErrorCode As UShort) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WriteControlWord", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WriteControlWord(ByVal nNodeID As Byte, ByVal nControlWord As UShort) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadStatusWord", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadStatusWord(ByVal nNodeID As Byte, ByRef nStatusWord As UShort) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadQuickStopOptionCode", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadQuickStopOptionCode(ByVal nNodeID As Byte, ByRef nOptionCode As Short) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WriteQuickStopOptionCode", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WriteQuickStopOptionCode(ByVal nNodeID As Byte, ByVal ErrorCode As Short) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WriteModesofOperation", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WriteModesofOperation(ByVal nNodeID As Byte, ByVal nModesofOperation As SByte) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadModesofOperation", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadModesofOperation(ByVal nNodeID As Byte, ByRef nModesofOperation As SByte) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadPositionTargetValueCalculated", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadPositionTargetValueCalculated(ByVal nNodeID As Byte, ByRef nPositionTargetValueCalculated As Integer) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadFollowingErrorWindow", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadFollowingErrorWindow(ByVal nNodeID As Byte, ByRef nFollowingError As UInteger) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WriteFollowingErrorWindow", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WriteFollowingErrorWindow(ByVal nNodeID As Byte, ByVal nFollowingError As UInteger) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadVelocityTargetValueCalculated", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadVelocityTargetValueCalculated(ByVal nNodeID As Byte, ByRef dVelocityTargetValueCalculated As Double) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadTargetTorque", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadTargetTorque(ByVal nNodeID As Byte, ByRef nTargetTorque As Short) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WriteTargetTorque", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WriteTargetTorque(ByVal nNodeID As Byte, ByVal nTargetTorque As Short) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadMaxRunningCurrent", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadMaxRunningCurrent(ByVal nNodeID As Byte, ByRef dRunningCurrent As Double) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WriteMaxRunningCurrent", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WriteMaxRunningCurrent(ByVal nNodeID As Byte, ByVal dRunningCurrent As Double) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadTorqueDemandValue", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadTorqueDemandValue(ByVal nNodeID As Byte, ByRef nTorqueDemandValue As Double) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadCurrentActualValue", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadCurrentActualValue(ByVal nNodeID As Byte, ByRef dCurrentActualValue As Double) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadTargetPosition", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadTargetPosition(ByVal nNodeID As Byte, ByRef nTargetPositiont As Integer) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WriteTargetPosition", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WriteTargetPosition(ByVal nNodeID As Byte, ByVal nTargetPositiont As Integer) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadHomingOffset", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadHomingOffset(ByVal nNodeID As Byte, ByRef nHomingOffset As Integer) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WriteHomingOffset", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WriteHomingOffset(ByVal nNodeID As Byte, ByVal nHomingOffset As Integer) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadPolarity", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadPolarity(ByVal nNodeID As Byte, ByRef nPolarity As Byte) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WritePolarity", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WritePolarity(ByVal nNodeID As Byte, ByVal nPolarity As Byte) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadMaxProfileSpeed", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadMaxProfileSpeed(ByVal nNodeID As Byte, ByRef dMaxProfileSpeed As Double) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WriteMaxProfileSpeed", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WriteMaxProfileSpeed(ByVal nNodeID As Byte, ByVal dMaxProfileSpeed As Double) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadProfileVelocity", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadProfileVelocity(ByVal nNodeID As Byte, ByRef dProfileVelocity As Double) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WriteProfileVelocity", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WriteProfileVelocity(ByVal nNodeID As Byte, ByVal dProfileVelocity As Double) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadProfileAcceleration", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadProfileAcceleration(ByVal nNodeID As Byte, ByRef dProfileAcceleration As Double) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WriteProfileAcceleration", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WriteProfileAcceleration(ByVal nNodeID As Byte, ByVal dProfileAcceleration As Double) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadProfileDeceleration", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadProfileDeceleration(ByVal nNodeID As Byte, ByRef dProfileAcceleration As Double) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WriteProfileDeceleration", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WriteProfileDeceleration(ByVal nNodeID As Byte, ByVal dProfileAcceleration As Double) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadQuickStopDeceleration", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadQuickStopDeceleration(ByVal nNodeID As Byte, ByRef dQuickStopDeceleration As Double) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WriteQuickStopDeceleration", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WriteQuickStopDeceleration(ByVal nNodeID As Byte, ByVal dQuickStopDeceleration As Double) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadTorqueSlop", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadTorqueSlop(ByVal nNodeID As Byte, ByRef nTorqueSlop As UInteger) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WriteTorqueSlop", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WriteTorqueSlop(ByVal nNodeID As Byte, ByVal nTorqueSlop As UInteger) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadHomingMethod", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadHomingMethod(ByVal nNodeID As Byte, ByRef nHomingMethod As Byte) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WriteHomingMethod", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WriteHomingMethod(ByVal nNodeID As Byte, ByVal nHomingMethod As Byte) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadHomingSpeedSearchSwitch", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadHomingSpeedSearchSwitch(ByVal nNodeID As Byte, ByRef dSpeed As Double) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WriteHomingSpeedSearchSwitch", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WriteHomingSpeedSearchSwitch(ByVal nNodeID As Byte, ByVal dSpeed As Double) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadHomingSpeedSearchIndex", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadHomingSpeedSearchIndex(ByVal nNodeID As Byte, ByRef dSpeed As Double) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WriteHomingSpeedSearchIndex", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WriteHomingSpeedSearchIndex(ByVal nNodeID As Byte, ByVal dSpeed As Double) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadHomingAcceleration", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadHomingAcceleration(ByVal nNodeID As Byte, ByRef dHomingAcceleration As Double) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WriteHomingAcceleration", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WriteHomingAcceleration(ByVal nNodeID As Byte, ByVal dHomingAcceleration As Double) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadDriveOutputs", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadDriveOutputs(ByVal nNodeID As Byte, ByRef nDriveOutputs As UInteger) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WriteDriveOutputs", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WriteDriveOutputs(ByVal nNodeID As Byte, ByVal nDriveOutputs As UInteger) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadTargetVelocity", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadTargetVelocity(ByVal nNodeID As Byte, ByRef pTargetVelocity As Double) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WriteTargetVelocity", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WriteTargetVelocity(ByVal nNodeID As Byte, ByVal dTargetVelocity As Double) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadSupportedDriveModes", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadSupportedDriveModes(ByVal nNodeID As Byte, ByRef nModes As UInteger) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadHomingSwitch", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadHomingSwitch(ByVal nNodeID As Byte, ByRef nHomingSwitch As Byte) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WriteHomingSwitch", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WriteHomingSwitch(ByVal nNodeID As Byte, ByVal nHomingSwitch As Byte) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadIdleCurrent", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadIdleCurrent(ByVal nNodeID As Byte, ByRef dIdleCurrent As Double) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WriteIdleCurrent", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WriteIdleCurrent(ByVal nNodeID As Byte, ByVal dIdleCurrent As Double) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadDisplayDriveInputs", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadDisplayDriveInputs(ByVal nNodeID As Byte, ByRef nInputs As Integer) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadTorqueConstant", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadTorqueConstant(ByVal nNodeID As Byte, ByRef nTorqueConstant As UShort) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WriteTorqueConstant", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WriteTorqueConstant(ByVal nNodeID As Byte, ByVal nTorqueConstant As UShort) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WriteDSPClearAlarm", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WriteDSPClearAlarm(ByVal nNodeID As Byte) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadQSegment", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadQSegment(ByVal nNodeID As Byte, ByRef nQSegment As Byte) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WriteQSegment", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WriteQSegment(ByVal nNodeID As Byte, ByVal nQSegment As Byte) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadActualVelocity", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadActualVelocity(ByVal nNodeID As Byte, ByRef dActualVelocity As Double) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadActualPosition", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadActualPosition(ByVal nNodeID As Byte, ByRef nActualPosition As Integer) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadDSPStatusCode", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadDSPStatusCode(ByVal nNodeID As Byte, ByRef nStatusCode As UShort) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WriteClearPosition", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WriteClearPosition(ByVal nNodeID As Byte) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadAccelerationCurrent", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadAccelerationCurrent(ByVal nNodeID As Byte, ByRef dAccelerationCurrent As Double) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WriteAccelerationCurrent", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WriteAccelerationCurrent(ByVal nNodeID As Byte, ByVal dAccelerationCurrent As Double) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ReadAnalogInput1", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ReadAnalogInput1(ByVal nNodeID As Byte, ByRef nAnalogInput1 As UShort) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="WriteProfileParam", CharSet:=CharSet.Ansi)> _
	Public Shared Function _WriteProfileParam(ByVal nNodeID As Byte, ByVal nModes As IntPtr, ByVal nDistance As IntPtr, ByVal dVelocity As IntPtr, ByVal dAccel As IntPtr, ByVal dDecel As IntPtr) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="SwitchControlWord", CharSet:=CharSet.Ansi)> _
	Public Shared Function _SwitchControlWord(ByVal nNodeID As Byte, ByVal nControlWord1 As UShort, ByVal nControlWord2 As UShort) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="DriveEnable", CharSet:=CharSet.Ansi)> _
	Public Shared Function _DriveEnable(ByVal nNodeID As Byte, ByVal bEnabled As Boolean) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="Stop", CharSet:=CharSet.Ansi)> _
	Public Shared Function _Stop(ByVal nNodeID As Byte) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="AlarmReset", CharSet:=CharSet.Ansi)> _
	Public Shared Function _AlarmReset(ByVal nNodeID As Byte) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="RelMove", CharSet:=CharSet.Ansi)> _
	Public Shared Function _RelMove(ByVal nNodeID As Byte, ByVal nDistance As Integer, ByVal dVelocity As IntPtr, ByVal dAccel As IntPtr, ByVal dDecel As IntPtr) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="AbsMove", CharSet:=CharSet.Ansi)> _
	Public Shared Function _AbsMove(ByVal nNodeID As Byte, ByVal nDistance As Integer, ByVal dVelocity As IntPtr, ByVal dAccel As IntPtr, ByVal dDecel As IntPtr) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="MultipleAbsMoveWithStopping", CharSet:=CharSet.Ansi)> _
	Public Shared Function _MultipleAbsMoveWithStopping(ByVal nNodeID As Byte, ByVal nDistance1 As Integer, ByVal nDistance2 As Integer, ByVal dVelocity1 As IntPtr, ByVal dVelocity2 As IntPtr, ByVal dAccel As IntPtr, ByVal dDecel As IntPtr) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="MultipleAbsMoveContinuous", CharSet:=CharSet.Ansi)> _
	Public Shared Function _MultipleAbsMoveContinuous(ByVal nNodeID As Byte, ByVal nDistance1 As Integer, ByVal nDistance2 As Integer, ByVal dVelocity1 As IntPtr, ByVal dVelocity2 As IntPtr, ByVal dAccel As IntPtr, ByVal dDecel As IntPtr, <System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)> ByVal bImmediateChange As Boolean) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ExecuteNormalQProgram", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ExecuteNormalQProgram(ByVal nNodeID As Byte, ByVal nSegment As Byte) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="ExecuteSyncQProgram", CharSet:=CharSet.Ansi)> _
	Public Shared Function _ExecuteSyncQProgram(ByVal nNodeID As Byte, ByVal nSegment As Byte, ByVal nSyncPulse As UInteger) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="Homing", CharSet:=CharSet.Ansi)> _
	Public Shared Function _Homing(ByVal nNodeID As Byte, ByVal nHomingMethod As Integer, ByVal dHomingVelocity As IntPtr, ByVal dIndexVelocity As IntPtr, ByVal dHomingAccel As IntPtr, ByVal nHomingOffset As IntPtr, ByVal nHomingSwitch As IntPtr) As Boolean
	End Function

	<DllImport(DLL_FILENAME, EntryPoint:="LaunchVelocityMode", CharSet:=CharSet.Ansi)> _
	Public Shared Function _LaunchVelocityMode(ByVal nNodeID As Byte, ByVal dVelocity As IntPtr, ByVal dAccel As IntPtr, ByVal dDecel As IntPtr) As Boolean
	End Function

#End Region

#Region "public Methods"

	Private Shared m_HeartBeatReceiveCallback As EventCallback

	Private Shared m_DataReceivedCallback As EventCallback

	Private Shared m_DataSentCallback As EventCallback

	Private Shared m_TPDOReceivedCallback As EventCallback

	Public Delegate Sub OnCanMessageEventHandler(ByVal e As CanMessageEventHandle)

	Public Delegate Sub OnTPDOReceivedEventHandler(ByVal e As TPDOReceivedEventHandle)

	Public Event HeartBeatReceive As OnCanMessageEventHandler

	Public Event DataSent As OnCanMessageEventHandler

	Public Event DataReceived As OnCanMessageEventHandler

	Public Event TPDOReceived As OnTPDOReceivedEventHandler

	Public Sub HeartBeatReceiveCallbackFunction()
		If HeartBeatReceiveEvent IsNot Nothing Then
			Dim heartBeatMessage As CanMessage = New CanMessage()
			GetLastHeartBeatMessage(heartBeatMessage)
			Dim e As New CanMessageEventHandle(heartBeatMessage)
			RaiseEvent HeartBeatReceive(e)
		End If
	End Sub

	Public Sub DataSentCallbackFunction()
		If DataSentEvent IsNot Nothing Then
			Dim canMessage As CanMessage = New CanMessage()
			GetLastSentMessage(canMessage)
			Dim e As New CanMessageEventHandle(canMessage)
			RaiseEvent DataSent(e)
		End If
	End Sub

	Public Sub DataReceivedCallbackFunction()
		If DataReceivedEvent IsNot Nothing Then
			Dim canMessage As CanMessage = New CanMessage()
			GetLastReceivedMessage(canMessage)
			Dim e As New CanMessageEventHandle(canMessage)
			RaiseEvent DataReceived(e)
		End If
	End Sub

	Public Sub TPDOReceivedCallbackFunction()
		If TPDOReceivedEvent IsNot Nothing Then
			Dim TPDOMessage As PDOMessage = New PDOMessage()
			GetLastTPDOMessage(TPDOMessage)
			Dim e As New TPDOReceivedEventHandle(TPDOMessage)
			RaiseEvent TPDOReceived(e)
		End If
	End Sub

	Public Sub GetLastHeartBeatMessage(ByRef CANMessage As CanMessage)
		_GetLastHeartBeatMessage(CANMessage)
	End Sub

	Public Sub GetLastSentMessage(ByRef CANMessage As CanMessage)
		_GetLastSentMessage(CANMessage)
	End Sub

	Public Sub GetLastReceivedMessage(ByRef CANMessage As CanMessage)
		_GetLastReceivedMessage(CANMessage)
	End Sub

	Public Sub GetLastTPDOMessage(ByRef TPDOMessage As PDOMessage)
		_GetLastTPDOMessage(TPDOMessage)
	End Sub

	Public Sub GetLastTPDOMessageByNodeID(ByRef nNodeID As Byte, ByRef nPDONo As Byte, ByRef PDOMessage As PDOMessage)
		_GetLastTPDOMessageByNodeID(nNodeID, nPDONo, PDOMessage)
	End Sub

	Public Sub New()
		m_HeartBeatReceiveCallback = AddressOf HeartBeatReceiveCallbackFunction
		_OnHeartBeatReceive(m_HeartBeatReceiveCallback)

		m_DataSentCallback = AddressOf DataSentCallbackFunction
		_OnDataSend(m_DataSentCallback)

		m_DataReceivedCallback = AddressOf DataReceivedCallbackFunction
		_OnDataReceive(m_DataReceivedCallback)

		m_TPDOReceivedCallback = AddressOf TPDOReceivedCallbackFunction
		_OnTPDOReceived(m_TPDOReceivedCallback)
	End Sub

	Protected Overrides Sub Finalize()
		'_Close();
	End Sub

	Public Function Open(ByVal adapter As EnumAdapter, ByVal nBaudRate As EnumBaudRate, ByVal nChannel As Byte) As Boolean
		Return _Open(adapter, nBaudRate, nChannel)
	End Function

	Public Function IsOpen() As Boolean
		Return _IsOpen()
	End Function

	Public Function Close() As Boolean
		Return _Close()
	End Function

	Public Sub SetExecuteTimeOut(ByVal nExecuteTimeOut As Byte)
		_SetExecuteTimeOut(nExecuteTimeOut)
	End Sub

	Public Function GetExecuteTimeOut() As Byte
		Return _GetExecuteTimeOut()
	End Function

	Public Sub SetExecuteRetryTimes(ByVal nExecuteRetryTimes As Byte)
		_SetExecuteRetryTimes(nExecuteRetryTimes)
	End Sub

	Public Function GetExecuteRetryTimes() As Byte
		Return _GetExecuteRetryTimes()
	End Function

	Public Function ResetBuffer() As Boolean
		Return _ResetBuffer()
	End Function

	Public Function Write(ByVal sSendCanMessage As CanMessage) As Boolean
		Return _Write(sSendCanMessage)
	End Function

	Public Function ExecuteCommand(ByVal sSendCanMessage As CanMessage, ByRef sReceivedCanMessage As CanMessage, ByVal nCanFunction As Integer, ByVal bMatchNodeID As Boolean, ByVal nNodeID As Byte, Optional ByVal bMatchIndex As Boolean = False, Optional ByVal nIndex As Integer = 0, Optional ByVal bMatchFirstByte As Boolean = False, Optional ByVal nFirstByte As Byte = 0) As Boolean
		Dim pReceivedCanMessage As IntPtr = Marshal.AllocHGlobal(Marshal.SizeOf(GetType(CanMessage)))

		Dim ret As Boolean = _ExecuteCommand(sSendCanMessage, pReceivedCanMessage, nCanFunction, bMatchNodeID, nNodeID, bMatchIndex, nIndex, bMatchFirstByte, nFirstByte)

		If ret Then
			sReceivedCanMessage = CType(Marshal.PtrToStructure(CType(CUInt(pReceivedCanMessage), IntPtr), GetType(CanMessage)), CanMessage)
		End If
		Marshal.FreeHGlobal(pReceivedCanMessage)
		Return ret
	End Function

	Public Function ReadSDOInt8(ByVal nNodeID As Byte, ByVal nIndex As Integer, ByVal nSubIndex As Byte, ByRef nData As SByte) As Boolean
		Return _ReadSDOInt8(nNodeID, nIndex, nSubIndex, nData)
	End Function

	Public Function ReadSDOUInt8(ByVal nNodeID As Byte, ByVal nIndex As Integer, ByVal nSubIndex As Byte, ByRef nData As Byte) As Boolean
		Return _ReadSDOUInt8(nNodeID, nIndex, nSubIndex, nData)
	End Function

	Public Function ReadSDOInt16(ByVal nNodeID As Byte, ByVal nIndex As Integer, ByVal nSubIndex As Byte, ByRef nData As Short) As Boolean
		Return _ReadSDOInt16(nNodeID, nIndex, nSubIndex, nData)
	End Function

	Public Function ReadSDOUInt16(ByVal nNodeID As Byte, ByVal nIndex As Integer, ByVal nSubIndex As Byte, ByRef nData As UShort) As Boolean
		Return _ReadSDOUInt16(nNodeID, nIndex, nSubIndex, nData)
	End Function

	Public Function ReadSDOInt32(ByVal nNodeID As Byte, ByVal nIndex As Integer, ByVal nSubIndex As Byte, ByRef nData As Integer) As Boolean
		Return _ReadSDOInt32(nNodeID, nIndex, nSubIndex, nData)
	End Function

	Public Function ReadSDOUInt32(ByVal nNodeID As Byte, ByVal nIndex As Integer, ByVal nSubIndex As Byte, ByRef nData As UInteger) As Boolean
		Return _ReadSDOUInt32(nNodeID, nIndex, nSubIndex, nData)
	End Function

	Public Function WriteSDOInt8(ByVal nNodeID As Byte, ByVal nIndex As Integer, ByVal nSubIndex As Byte, ByVal nData As SByte) As Boolean
		Return _WriteSDOInt8(nNodeID, nIndex, nSubIndex, nData)
	End Function

	Public Function WriteSDOUInt8(ByVal nNodeID As Byte, ByVal nIndex As Integer, ByVal nSubIndex As Byte, ByVal nData As Byte) As Boolean
		Return _WriteSDOUInt8(nNodeID, nIndex, nSubIndex, nData)
	End Function

	Public Function WriteSDOInt16(ByVal nNodeID As Byte, ByVal nIndex As Integer, ByVal nSubIndex As Byte, ByVal nData As Short) As Boolean
		Return _WriteSDOInt16(nNodeID, nIndex, nSubIndex, nData)
	End Function

	Public Function WriteSDOUInt16(ByVal nNodeID As Byte, ByVal nIndex As Integer, ByVal nSubIndex As Byte, ByVal nData As UShort) As Boolean
		Return _WriteSDOUInt16(nNodeID, nIndex, nSubIndex, nData)
	End Function

	Public Function WriteSDOInt32(ByVal nNodeID As Byte, ByVal nIndex As Integer, ByVal nSubIndex As Byte, ByVal nData As Integer) As Boolean
		Return _WriteSDOInt32(nNodeID, nIndex, nSubIndex, nData)
	End Function

	Public Function WriteSDOUInt32(ByVal nNodeID As Byte, ByVal nIndex As Integer, ByVal nSubIndex As Byte, ByVal nData As UInteger) As Boolean
		Return _WriteSDOUInt32(nNodeID, nIndex, nSubIndex, nData)
	End Function

	Public Sub GetLastErrorInfo(ByRef errorInfo As ErrorInfo)
		_GetLastErrorInfo(errorInfo)
	End Sub

	Public Function SetToPreoperationalMode(ByVal nNodeID As Byte) As Boolean
		Return _SetToPreoperationalMode(nNodeID)
	End Function

	Public Function SetToOperationalMode(ByVal nNodeID As Byte) As Boolean
		Return _SetToOperationalMode(nNodeID)
	End Function

	Public Function SetRPDOMapping(ByVal nNodeID As Byte, ByVal nRPDONo As Byte, ByVal nLen As Integer, ByVal PdoMappingArr() As PDOMapping) As Boolean
		Return _SetRPDOMapping(nNodeID, nRPDONo, nLen, PdoMappingArr)
	End Function

	Public Function SetTPDOMapping(ByVal nNodeID As Byte, ByVal nTPDONo As Byte, ByVal nLen As Integer, ByVal PdoMappingArr() As PDOMapping) As Boolean
		Return _SetTPDOMapping(nNodeID, nTPDONo, nLen, PdoMappingArr)
	End Function

	Public Function RestorePDOMappingSettings(ByVal nNodeID As Byte) As Boolean
		Return _RestorePDOMappingSettings(nNodeID)
	End Function

	Public Function WriteRPDO(ByVal nNodeID As Byte, ByVal nRPDONo As Byte, ByVal nLen As Integer, ByVal aData() As Byte) As Boolean
		If aData.Length < nLen Then
			Throw New ArgumentException()
		End If
		Return _WriteRPDO(nNodeID, nRPDONo, nLen, aData)
	End Function

	Public Function SaveParameters(ByVal nNodeID As Byte) As Boolean
		Return _SaveParameters(nNodeID)
	End Function

	Public Function ReadPositionGain(ByVal nNodeID As Byte, ByRef nPositionGain As UShort) As Boolean
		Return _ReadPositionGain(nNodeID, nPositionGain)
	End Function

	Public Function WritePositionGain(ByVal nNodeID As Byte, ByVal nPositionGain As UShort) As Boolean
		Return _WritePositionGain(nNodeID, nPositionGain)
	End Function

	Public Function ReadPositionDeriGain(ByVal nNodeID As Byte, ByRef nPositionDeriGain As UShort) As Boolean
		Return _ReadPositionDeriGain(nNodeID, nPositionDeriGain)
	End Function

	Public Function WritePositionDeriGain(ByVal nNodeID As Byte, ByVal nPositionDeriGain As UShort) As Boolean
		Return _WritePositionDeriGain(nNodeID, nPositionDeriGain)
	End Function

	Public Function ReadPositionDeriFilter(ByVal nNodeID As Byte, ByRef nPositionDeriFilter As UShort) As Boolean
		Return _ReadPositionDeriFilter(nNodeID, nPositionDeriFilter)
	End Function

	Public Function WritePositionDeriFilter(ByVal nNodeID As Byte, ByVal nPositionDeriFilter As UShort) As Boolean
		Return _WritePositionDeriFilter(nNodeID, nPositionDeriFilter)
	End Function

	Public Function ReadVelocityGain(ByVal nNodeID As Byte, ByRef nVelocityGain As UShort) As Boolean
		Return _ReadVelocityGain(nNodeID, nVelocityGain)
	End Function

	Public Function WriteVelocityGain(ByVal nNodeID As Byte, ByVal nVelocityGain As UShort) As Boolean
		Return _WriteVelocityGain(nNodeID, nVelocityGain)
	End Function

	Public Function ReadVelocityIntegGain(ByVal nNodeID As Byte, ByRef nVelocityIntegGain As UShort) As Boolean
		Return _ReadVelocityIntegGain(nNodeID, nVelocityIntegGain)
	End Function

	Public Function WriteVelocityIntegGain(ByVal nNodeID As Byte, ByVal nVelocityIntegGain As UShort) As Boolean
		Return _WriteVelocityIntegGain(nNodeID, nVelocityIntegGain)
	End Function

	Public Function ReadAccFeedForward(ByVal nNodeID As Byte, ByRef nAccFeedForward As UShort) As Boolean
		Return _ReadAccFeedForward(nNodeID, nAccFeedForward)
	End Function

	Public Function WriteAccFeedForward(ByVal nNodeID As Byte, ByVal nAccFeedForward As UShort) As Boolean
		Return _WriteAccFeedForward(nNodeID, nAccFeedForward)
	End Function

	Public Function ReadPIDFilter(ByVal nNodeID As Byte, ByRef nPIDFilter As UShort) As Boolean
		Return _ReadPIDFilter(nNodeID, nPIDFilter)
	End Function

	Public Function WritePIDFilter(ByVal nNodeID As Byte, ByVal nPIDFilter As UInteger) As Boolean
		Return _WritePIDFilter(nNodeID, nPIDFilter)
	End Function

	Public Function ReadNotchFilter(ByVal nNodeID As Byte, ByVal nIndex As Integer, ByRef nNotchFilter As Short) As Boolean
		Return _ReadNotchFilter(nNodeID, nIndex, nNotchFilter)
	End Function

	Public Function WriteNotchFilter(ByVal nNodeID As Byte, ByVal nIndex As Integer, ByVal nNotchFilter As Short) As Boolean
		Return _WriteNotchFilter(nNodeID, nIndex, nNotchFilter)
	End Function

	Public Function ReadPositionError(ByVal nNodeID As Byte, ByRef nPositionError As UShort) As Boolean
		Return _ReadPositionError(nNodeID, nPositionError)
	End Function

	Public Function WritePositionError(ByVal nNodeID As Byte, ByVal nPositionError As UShort) As Boolean
		Return _WritePositionError(nNodeID, nPositionError)
	End Function

	Public Function ReadVelocityMax(ByVal nNodeID As Byte, ByRef nVelocityMax As UShort) As Boolean
		Return _ReadVelocityMax(nNodeID, nVelocityMax)
	End Function

	Public Function WriteVelocityMax(ByVal nNodeID As Byte, ByVal nVelocityMax As UShort) As Boolean
		Return _WriteVelocityMax(nNodeID, nVelocityMax)
	End Function

	Public Function ReadSmoothFilter(ByVal nNodeID As Byte, ByRef nSmoothFilter As UShort) As Boolean
		Return _ReadSmoothFilter(nNodeID, nSmoothFilter)
	End Function

	Public Function WriteSmoothFilter(ByVal nNodeID As Byte, ByVal nSmoothFilter As UShort) As Boolean
		Return _WriteSmoothFilter(nNodeID, nSmoothFilter)
	End Function

	Public Function ReadDriverTemp(ByVal nNodeID As Byte, ByRef nDriverTemp As Short) As Boolean
		Return _ReadDriverTemp(nNodeID, nDriverTemp)
	End Function

	Public Function ReadErrorCode(ByVal nNodeID As Byte, ByRef nErrorCode As UShort) As Boolean
		Return _ReadErrorCode(nNodeID, nErrorCode)
	End Function

	Public Function ReadErrorCodeUpper(ByVal nNodeID As Byte, ByRef nErrorCode As UShort) As Boolean
		Return _ReadErrorCodeUpper(nNodeID, nErrorCode)
	End Function

	Public Function WriteControlWord(ByVal nNodeID As Byte, ByVal nControlWord As UShort) As Boolean
		Return _WriteControlWord(nNodeID, nControlWord)
	End Function

	Public Function ReadStatusWord(ByVal nNodeID As Byte, ByRef nStatusWord As UShort) As Boolean
		Return _ReadStatusWord(nNodeID, nStatusWord)
	End Function

	Public Function ReadQuickStopOptionCode(ByVal nNodeID As Byte, ByRef nOptionCode As Short) As Boolean
		Return _ReadQuickStopOptionCode(nNodeID, nOptionCode)
	End Function

	Public Function WriteQuickStopOptionCode(ByVal nNodeID As Byte, ByVal ErrorCode As Short) As Boolean
		Return _WriteQuickStopOptionCode(nNodeID, ErrorCode)
	End Function

	Public Function WriteModesofOperation(ByVal nNodeID As Byte, ByVal nModesofOperation As Byte) As Boolean
		Return _WriteModesofOperation(nNodeID, nModesofOperation)
	End Function

	Public Function ReadModesofOperation(ByVal nNodeID As Byte, ByRef nModesofOperation As Byte) As Boolean
		Return _ReadModesofOperation(nNodeID, nModesofOperation)
	End Function

	Public Function ReadPositionTargetValueCalculated(ByVal nNodeID As Byte, ByRef nPositionTargetValueCalculated As Integer) As Boolean
		Return _ReadPositionTargetValueCalculated(nNodeID, nPositionTargetValueCalculated)
	End Function

	Public Function ReadFollowingErrorWindow(ByVal nNodeID As Byte, ByRef nFollowingError As UInteger) As Boolean
		Return _ReadFollowingErrorWindow(nNodeID, nFollowingError)
	End Function

	Public Function WriteFollowingErrorWindow(ByVal nNodeID As Byte, ByVal nFollowingError As UInteger) As Boolean
		Return _WriteFollowingErrorWindow(nNodeID, nFollowingError)
	End Function

	Public Function ReadVelocityTargetValueCalculated(ByVal nNodeID As Byte, ByRef dVelocityTargetValueCalculated As Double) As Boolean
		Return _ReadVelocityTargetValueCalculated(nNodeID, dVelocityTargetValueCalculated)
	End Function

	Public Function ReadTargetTorque(ByVal nNodeID As Byte, ByRef nTargetTorque As Short) As Boolean
		Return _ReadTargetTorque(nNodeID, nTargetTorque)
	End Function

	Public Function WriteTargetTorque(ByVal nNodeID As Byte, ByVal nTargetTorque As Short) As Boolean
		Return _WriteTargetTorque(nNodeID, nTargetTorque)
	End Function

	Public Function ReadMaxRunningCurrent(ByVal nNodeID As Byte, ByRef dRunningCurrent As Double) As Boolean
		Return _ReadMaxRunningCurrent(nNodeID, dRunningCurrent)
	End Function

	Public Function WriteMaxRunningCurrent(ByVal nNodeID As Byte, ByVal dRunningCurrent As Double) As Boolean
		Return _WriteMaxRunningCurrent(nNodeID, dRunningCurrent)
	End Function

	Public Function ReadTorqueDemandValue(ByVal nNodeID As Byte, ByRef nTorqueDemandValue As Double) As Boolean
		Return _ReadTorqueDemandValue(nNodeID, nTorqueDemandValue)
	End Function

	Public Function ReadCurrentActualValue(ByVal nNodeID As Byte, ByRef dCurrentActualValue As Double) As Boolean
		Return _ReadCurrentActualValue(nNodeID, dCurrentActualValue)
	End Function

	Public Function ReadTargetPosition(ByVal nNodeID As Byte, ByRef nTargetPositiont As Integer) As Boolean
		Return _ReadTargetPosition(nNodeID, nTargetPositiont)
	End Function

	Public Function WriteTargetPosition(ByVal nNodeID As Byte, ByVal nTargetPositiont As Integer) As Boolean
		Return _WriteTargetPosition(nNodeID, nTargetPositiont)
	End Function

	Public Function ReadHomingOffset(ByVal nNodeID As Byte, ByRef nHomingOffset As Integer) As Boolean
		Return _ReadHomingOffset(nNodeID, nHomingOffset)
	End Function

	Public Function WriteHomingOffset(ByVal nNodeID As Byte, ByVal nHomingOffset As Integer) As Boolean
		Return _WriteHomingOffset(nNodeID, nHomingOffset)
	End Function

	Public Function ReadPolarity(ByVal nNodeID As Byte, ByRef nPolarity As Byte) As Boolean
		Return _ReadPolarity(nNodeID, nPolarity)
	End Function

	Public Function WritePolarity(ByVal nNodeID As Byte, ByVal nPolarity As Byte) As Boolean
		Return _WritePolarity(nNodeID, nPolarity)
	End Function

	Public Function ReadMaxProfileSpeed(ByVal nNodeID As Byte, ByRef dMaxProfileSpeed As Double) As Boolean
		Return _ReadMaxProfileSpeed(nNodeID, dMaxProfileSpeed)
	End Function

	Public Function WriteMaxProfileSpeed(ByVal nNodeID As Byte, ByVal dMaxProfileSpeed As Double) As Boolean
		Return _WriteMaxProfileSpeed(nNodeID, dMaxProfileSpeed)
	End Function

	Public Function ReadProfileVelocity(ByVal nNodeID As Byte, ByRef dProfileVelocity As Double) As Boolean
		Return _ReadProfileVelocity(nNodeID, dProfileVelocity)
	End Function

	Public Function WriteProfileVelocity(ByVal nNodeID As Byte, ByVal dProfileVelocity As Double) As Boolean
		Return _WriteProfileVelocity(nNodeID, dProfileVelocity)
	End Function

	Public Function ReadProfileAcceleration(ByVal nNodeID As Byte, ByRef dProfileAcceleration As Double) As Boolean
		Return _ReadProfileAcceleration(nNodeID, dProfileAcceleration)
	End Function

	Public Function WriteProfileAcceleration(ByVal nNodeID As Byte, ByVal dProfileAcceleration As Double) As Boolean
		Return _WriteProfileAcceleration(nNodeID, dProfileAcceleration)
	End Function

	Public Function ReadProfileDeceleration(ByVal nNodeID As Byte, ByRef dProfileAcceleration As Double) As Boolean
		Return _ReadProfileDeceleration(nNodeID, dProfileAcceleration)
	End Function

	Public Function WriteProfileDeceleration(ByVal nNodeID As Byte, ByVal dProfileAcceleration As Double) As Boolean
		Return _WriteProfileDeceleration(nNodeID, dProfileAcceleration)
	End Function

	Public Function ReadQuickStopDeceleration(ByVal nNodeID As Byte, ByRef dQuickStopDeceleration As Double) As Boolean
		Return _ReadQuickStopDeceleration(nNodeID, dQuickStopDeceleration)
	End Function

	Public Function WriteQuickStopDeceleration(ByVal nNodeID As Byte, ByVal dQuickStopDeceleration As Double) As Boolean
		Return _WriteQuickStopDeceleration(nNodeID, dQuickStopDeceleration)
	End Function

	Public Function ReadTorqueSlop(ByVal nNodeID As Byte, ByRef nTorqueSlop As UInteger) As Boolean
		Return _ReadTorqueSlop(nNodeID, nTorqueSlop)
	End Function

	Public Function WriteTorqueSlop(ByVal nNodeID As Byte, ByVal nTorqueSlop As UInteger) As Boolean
		Return _WriteTorqueSlop(nNodeID, nTorqueSlop)
	End Function

	Public Function ReadHomingMethod(ByVal nNodeID As Byte, ByRef nHomingMethod As Byte) As Boolean
		Return _ReadHomingMethod(nNodeID, nHomingMethod)
	End Function

	Public Function WriteHomingMethod(ByVal nNodeID As Byte, ByVal nHomingMethod As Byte) As Boolean
		Return _WriteHomingMethod(nNodeID, nHomingMethod)
	End Function

	Public Function ReadHomingSpeedSearchSwitch(ByVal nNodeID As Byte, ByRef dSpeed As Double) As Boolean
		Return _ReadHomingSpeedSearchSwitch(nNodeID, dSpeed)
	End Function

	Public Function WriteHomingSpeedSearchSwitch(ByVal nNodeID As Byte, ByVal dSpeed As Double) As Boolean
		Return _WriteHomingSpeedSearchSwitch(nNodeID, dSpeed)
	End Function

	Public Function ReadHomingSpeedSearchIndex(ByVal nNodeID As Byte, ByRef dSpeed As Double) As Boolean
		Return _ReadHomingSpeedSearchIndex(nNodeID, dSpeed)
	End Function

	Public Function WriteHomingSpeedSearchIndex(ByVal nNodeID As Byte, ByVal dSpeed As Double) As Boolean
		Return _WriteHomingSpeedSearchIndex(nNodeID, dSpeed)
	End Function

	Public Function ReadHomingAcceleration(ByVal nNodeID As Byte, ByRef dHomingAcceleration As Double) As Boolean
		Return _ReadHomingAcceleration(nNodeID, dHomingAcceleration)
	End Function

	Public Function WriteHomingAcceleration(ByVal nNodeID As Byte, ByVal dHomingAcceleration As Double) As Boolean
		Return _WriteHomingAcceleration(nNodeID, dHomingAcceleration)
	End Function

	Public Function ReadDriveOutputs(ByVal nNodeID As Byte, ByRef nDriveOutputs As UInteger) As Boolean
		Return _ReadDriveOutputs(nNodeID, nDriveOutputs)
	End Function

	Public Function WriteDriveOutputs(ByVal nNodeID As Byte, ByVal nDriveOutputs As UInteger) As Boolean
		Return _WriteDriveOutputs(nNodeID, nDriveOutputs)
	End Function

	Public Function ReadTargetVelocity(ByVal nNodeID As Byte, ByRef dTargetVelocity As Double) As Boolean
		Return _ReadTargetVelocity(nNodeID, dTargetVelocity)
	End Function

	Public Function WriteTargetVelocity(ByVal nNodeID As Byte, ByVal dTargetVelocity As Double) As Boolean
		Return _WriteTargetVelocity(nNodeID, dTargetVelocity)
	End Function

	Public Function ReadSupportedDriveModes(ByVal nNodeID As Byte, ByRef nModes As UInteger) As Boolean
		Return _ReadSupportedDriveModes(nNodeID, nModes)
	End Function

	Public Function ReadHomingSwitch(ByVal nNodeID As Byte, ByRef nHomingSwitch As Byte) As Boolean
		Return _ReadHomingSwitch(nNodeID, nHomingSwitch)
	End Function

	Public Function WriteHomingSwitch(ByVal nNodeID As Byte, ByVal nHomingSwitch As Byte) As Boolean
		Return _WriteHomingSwitch(nNodeID, nHomingSwitch)
	End Function

	Public Function ReadIdleCurrent(ByVal nNodeID As Byte, ByRef dIdleCurrent As Double) As Boolean
		Return _ReadIdleCurrent(nNodeID, dIdleCurrent)
	End Function

	Public Function WriteIdleCurrent(ByVal nNodeID As Byte, ByVal dIdleCurrent As Double) As Boolean
		Return _WriteIdleCurrent(nNodeID, dIdleCurrent)
	End Function

	Public Function ReadDisplayDriveInputs(ByVal nNodeID As Byte, ByRef nInputs As Integer) As Boolean
		Return _ReadDisplayDriveInputs(nNodeID, nInputs)
	End Function

	Public Function ReadTorqueConstant(ByVal nNodeID As Byte, ByRef nTorqueConstant As UShort) As Boolean
		Return _ReadTorqueConstant(nNodeID, nTorqueConstant)
	End Function

	Public Function WriteTorqueConstant(ByVal nNodeID As Byte, ByVal nTorqueConstant As UShort) As Boolean
		Return _WriteTorqueConstant(nNodeID, nTorqueConstant)
	End Function

	Public Function WriteDSPClearAlarm(ByVal nNodeID As Byte) As Boolean
		Return _WriteDSPClearAlarm(nNodeID)
	End Function

	Public Function ReadQSegment(ByVal nNodeID As Byte, ByRef nQSegment As Byte) As Boolean
		Return _ReadQSegment(nNodeID, nQSegment)
	End Function

	Public Function WriteQSegment(ByVal nNodeID As Byte, ByVal nQSegment As Byte) As Boolean
		Return _WriteQSegment(nNodeID, nQSegment)
	End Function

	Public Function ReadActualVelocity(ByVal nNodeID As Byte, ByRef dActualVelocity As Double) As Boolean
		Return _ReadActualVelocity(nNodeID, dActualVelocity)
	End Function

	Public Function ReadActualPosition(ByVal nNodeID As Byte, ByRef nActualPosition As Integer) As Boolean
		Return _ReadActualPosition(nNodeID, nActualPosition)
	End Function

	Public Function ReadDSPStatusCode(ByVal nNodeID As Byte, ByRef nStatusCode As UShort) As Boolean
		Return _ReadDSPStatusCode(nNodeID, nStatusCode)
	End Function

	Public Function WriteClearPosition(ByVal nNodeID As Byte) As Boolean
		Return _WriteClearPosition(nNodeID)
	End Function

	Public Function ReadAccelerationCurrent(ByVal nNodeID As Byte, ByRef dAccelerationCurrent As Double) As Boolean
		Return _ReadAccelerationCurrent(nNodeID, dAccelerationCurrent)
	End Function

	Public Function WriteAccelerationCurrent(ByVal nNodeID As Byte, ByVal dAccelerationCurrent As Double) As Boolean
		Return _WriteAccelerationCurrent(nNodeID, dAccelerationCurrent)
	End Function

	Public Function ReadAnalogInput1(ByVal nNodeID As Byte, ByRef nAnalogInput1 As UShort) As Boolean
		Return _ReadAnalogInput1(nNodeID, nAnalogInput1)
	End Function

	Public Function WriteProfileParam(ByVal nNodeID As Byte, ByVal nModes? As Integer, ByVal nDistance? As Integer, ByVal dVelocity? As Double, ByVal dAccel? As Double, ByVal dDecel? As Double) As Boolean
		Dim ptrModes As IntPtr = IntPtr.Zero
		If nModes IsNot Nothing Then
			ptrModes = Marshal.AllocHGlobal(Marshal.SizeOf(GetType(Integer)))
			Marshal.StructureToPtr(nModes.Value, ptrModes, True)
		End If
		Dim ptrDistance As IntPtr = IntPtr.Zero
		If nDistance IsNot Nothing Then
			ptrDistance = Marshal.AllocHGlobal(Marshal.SizeOf(GetType(Integer)))
			Marshal.StructureToPtr(nDistance.Value, ptrDistance, True)
		End If
		Dim ptrVelocity As IntPtr = IntPtr.Zero
		If dVelocity IsNot Nothing Then
			ptrVelocity = Marshal.AllocHGlobal(Marshal.SizeOf(GetType(Double)))
			Marshal.StructureToPtr(dVelocity.Value, ptrVelocity, True)
		End If
		Dim ptrAccel As IntPtr = IntPtr.Zero
		If dAccel IsNot Nothing Then
			ptrAccel = Marshal.AllocHGlobal(Marshal.SizeOf(GetType(Double)))
			Marshal.StructureToPtr(dAccel.Value, ptrAccel, True)
		End If
		Dim ptrDecel As IntPtr = IntPtr.Zero
		If dDecel IsNot Nothing Then
			ptrDecel = Marshal.AllocHGlobal(Marshal.SizeOf(GetType(Double)))
			Marshal.StructureToPtr(dDecel.Value, ptrDecel, True)
		End If
		Return _WriteProfileParam(nNodeID, ptrModes, ptrDistance, ptrVelocity, ptrAccel, ptrDecel)
	End Function

	Public Function SwitchControlWord(ByVal nNodeID As Byte, ByVal nControlWord1 As UShort, ByVal nControlWord2 As UShort) As Boolean
		Return _SwitchControlWord(nNodeID, nControlWord1, nControlWord2)
	End Function

	Public Function DriveEnable(ByVal nNodeID As Byte, ByVal bEnabled As Boolean) As Boolean
		Return _DriveEnable(nNodeID, bEnabled)
	End Function

	Public Function [Stop](ByVal nNodeID As Byte) As Boolean
		Return _Stop(nNodeID)
	End Function

	Public Function AlarmReset(ByVal nNodeID As Byte) As Boolean
		Return _AlarmReset(nNodeID)
	End Function

	Public Function RelMove(ByVal nNodeID As Byte, ByVal nDistance As Integer, ByVal dVelocity? As Double, ByVal dAccel? As Double, ByVal dDecel? As Double) As Boolean
		Dim ptrVelocity As IntPtr = IntPtr.Zero
		If dVelocity IsNot Nothing Then
			ptrVelocity = Marshal.AllocHGlobal(Marshal.SizeOf(GetType(Double)))
			Marshal.StructureToPtr(dVelocity.Value, ptrVelocity, True)
		End If
		Dim ptrAccel As IntPtr = IntPtr.Zero
		If dAccel IsNot Nothing Then
			ptrAccel = Marshal.AllocHGlobal(Marshal.SizeOf(GetType(Double)))
			Marshal.StructureToPtr(dAccel.Value, ptrAccel, True)
		End If
		Dim ptrDecel As IntPtr = IntPtr.Zero
		If dDecel IsNot Nothing Then
			ptrDecel = Marshal.AllocHGlobal(Marshal.SizeOf(GetType(Double)))
			Marshal.StructureToPtr(dDecel.Value, ptrDecel, True)
		End If
		Return _RelMove(nNodeID, nDistance, ptrVelocity, ptrAccel, ptrDecel)
	End Function

	Public Function AbsMove(ByVal nNodeID As Byte, ByVal nDistance As Integer, ByVal dVelocity? As Double, ByVal dAccel? As Double, ByVal dDecel? As Double) As Boolean
		Dim ptrVelocity As IntPtr = IntPtr.Zero
		If dVelocity IsNot Nothing Then
			ptrVelocity = Marshal.AllocHGlobal(Marshal.SizeOf(GetType(Double)))
			Marshal.StructureToPtr(dVelocity.Value, ptrVelocity, True)
		End If
		Dim ptrAccel As IntPtr = IntPtr.Zero
		If dAccel IsNot Nothing Then
			ptrAccel = Marshal.AllocHGlobal(Marshal.SizeOf(GetType(Double)))
			Marshal.StructureToPtr(dAccel.Value, ptrAccel, True)
		End If
		Dim ptrDecel As IntPtr = IntPtr.Zero
		If dDecel IsNot Nothing Then
			ptrDecel = Marshal.AllocHGlobal(Marshal.SizeOf(GetType(Double)))
			Marshal.StructureToPtr(dDecel.Value, ptrDecel, True)
		End If
		Return _AbsMove(nNodeID, nDistance, ptrVelocity, ptrAccel, ptrDecel)
	End Function

	Public Function MultipleAbsMoveWithStopping(ByVal nNodeID As Byte, ByVal nDistance1 As Integer, ByVal nDistance2 As Integer, ByVal dVelocity1? As Double, ByVal dVelocity2? As Double, ByVal dAccel? As Double, ByVal dDecel? As Double) As Boolean
		Dim ptrVelocity1 As IntPtr = IntPtr.Zero
		If dVelocity1 IsNot Nothing Then
			ptrVelocity1 = Marshal.AllocHGlobal(Marshal.SizeOf(GetType(Double)))
			Marshal.StructureToPtr(dVelocity1.Value, ptrVelocity1, True)
		End If
		Dim ptrVelocity2 As IntPtr = IntPtr.Zero
		If dVelocity2 IsNot Nothing Then
			ptrVelocity2 = Marshal.AllocHGlobal(Marshal.SizeOf(GetType(Double)))
			Marshal.StructureToPtr(dVelocity2.Value, ptrVelocity2, True)
		End If
		Dim ptrAccel As IntPtr = IntPtr.Zero
		If dAccel IsNot Nothing Then
			ptrAccel = Marshal.AllocHGlobal(Marshal.SizeOf(GetType(Double)))
			Marshal.StructureToPtr(dAccel.Value, ptrAccel, True)
		End If
		Dim ptrDecel As IntPtr = IntPtr.Zero
		If dDecel IsNot Nothing Then
			ptrDecel = Marshal.AllocHGlobal(Marshal.SizeOf(GetType(Double)))
			Marshal.StructureToPtr(dDecel.Value, ptrDecel, True)
		End If
		Return _MultipleAbsMoveWithStopping(nNodeID, nDistance1, nDistance2, ptrVelocity1, ptrVelocity2, ptrAccel, ptrDecel)
	End Function

	Public Function MultipleAbsMoveContinuous(ByVal nNodeID As Byte, ByVal nDistance1 As Integer, ByVal nDistance2 As Integer, ByVal dVelocity1? As Double, ByVal dVelocity2? As Double, ByVal dAccel? As Double, ByVal dDecel? As Double, ByVal bImmediateChange As Boolean) As Boolean
		Dim ptrVelocity1 As IntPtr = IntPtr.Zero
		If dVelocity1 IsNot Nothing Then
			ptrVelocity1 = Marshal.AllocHGlobal(Marshal.SizeOf(GetType(Double)))
			Marshal.StructureToPtr(dVelocity1.Value, ptrVelocity1, True)
		End If
		Dim ptrVelocity2 As IntPtr = IntPtr.Zero
		If dVelocity2 IsNot Nothing Then
			ptrVelocity2 = Marshal.AllocHGlobal(Marshal.SizeOf(GetType(Double)))
			Marshal.StructureToPtr(dVelocity2.Value, ptrVelocity2, True)
		End If
		Dim ptrAccel As IntPtr = IntPtr.Zero
		If dAccel IsNot Nothing Then
			ptrAccel = Marshal.AllocHGlobal(Marshal.SizeOf(GetType(Double)))
			Marshal.StructureToPtr(dAccel.Value, ptrAccel, True)
		End If
		Dim ptrDecel As IntPtr = IntPtr.Zero
		If dDecel IsNot Nothing Then
			ptrDecel = Marshal.AllocHGlobal(Marshal.SizeOf(GetType(Double)))
			Marshal.StructureToPtr(dDecel.Value, ptrDecel, True)
		End If
		Return _MultipleAbsMoveContinuous(nNodeID, nDistance1, nDistance2, ptrVelocity1, ptrVelocity2, ptrAccel, ptrDecel, bImmediateChange)
	End Function

	Public Function ExecuteNormalQProgram(ByVal nNodeID As Byte, ByVal nSegment As Integer) As Boolean
		Return _ExecuteNormalQProgram(nNodeID, nSegment)
	End Function

	Public Function ExecuteSyncQProgram(ByVal nNodeID As Byte, ByVal nSegment As Integer, ByVal nSyncPulse As UInteger) As Boolean
		Return _ExecuteSyncQProgram(nNodeID, nSegment, nSyncPulse)
	End Function

	Public Function Homing(ByVal nNodeID As Byte, ByVal nHomingMethod As Integer, ByVal dHomingVelocity? As Double, ByVal dIndexVelocity? As Double, ByVal dHomingAccel? As Double, ByVal nHomingOffset? As Integer, ByVal nHomingSwitch? As Integer) As Boolean
		Dim ptrHomingVelocity As IntPtr = IntPtr.Zero
		If dHomingVelocity IsNot Nothing Then
			ptrHomingVelocity = Marshal.AllocHGlobal(Marshal.SizeOf(GetType(Double)))
			Marshal.StructureToPtr(dHomingVelocity.Value, ptrHomingVelocity, True)
		End If
		Dim ptrIndexVelocity As IntPtr = IntPtr.Zero
		If dIndexVelocity IsNot Nothing Then
			ptrIndexVelocity = Marshal.AllocHGlobal(Marshal.SizeOf(GetType(Double)))
			Marshal.StructureToPtr(dIndexVelocity.Value, ptrIndexVelocity, True)
		End If
		Dim ptrHomingAccel As IntPtr = IntPtr.Zero
		If dHomingAccel IsNot Nothing Then
			ptrHomingAccel = Marshal.AllocHGlobal(Marshal.SizeOf(GetType(Double)))
			Marshal.StructureToPtr(dHomingAccel.Value, ptrHomingAccel, True)
		End If
		Dim ptrHomingOffset As IntPtr = IntPtr.Zero
		If nHomingOffset IsNot Nothing Then
			ptrHomingOffset = Marshal.AllocHGlobal(Marshal.SizeOf(GetType(Integer)))
			Marshal.StructureToPtr(nHomingOffset.Value, ptrHomingOffset, True)
		End If
		Dim ptrHomingSwitch As IntPtr = IntPtr.Zero
		If nHomingSwitch IsNot Nothing Then
			ptrHomingSwitch = Marshal.AllocHGlobal(Marshal.SizeOf(GetType(Integer)))
			Marshal.StructureToPtr(nHomingSwitch.Value, ptrHomingSwitch, True)
		End If
		Return _Homing(nNodeID, nHomingMethod, ptrHomingVelocity, ptrIndexVelocity, ptrHomingAccel, ptrHomingOffset, ptrHomingSwitch)
	End Function

	Public Function LaunchVelocityMode(ByVal nNodeID As Byte, ByVal dVelocity? As Double, ByVal dAccel? As Double, ByVal dDecel? As Double) As Boolean
		Dim ptrVelocity As IntPtr = IntPtr.Zero
		If dVelocity IsNot Nothing Then
			ptrVelocity = Marshal.AllocHGlobal(Marshal.SizeOf(GetType(Double)))
			Marshal.StructureToPtr(dVelocity.Value, ptrVelocity, True)
		End If
		Dim ptrAccel As IntPtr = IntPtr.Zero
		If dAccel IsNot Nothing Then
			ptrAccel = Marshal.AllocHGlobal(Marshal.SizeOf(GetType(Double)))
			Marshal.StructureToPtr(dAccel.Value, ptrAccel, True)
		End If
		Dim ptrDecel As IntPtr = IntPtr.Zero
		If dDecel IsNot Nothing Then
			ptrDecel = Marshal.AllocHGlobal(Marshal.SizeOf(GetType(Double)))
			Marshal.StructureToPtr(dDecel.Value, ptrDecel, True)
		End If
		Return _LaunchVelocityMode(nNodeID, ptrVelocity, ptrAccel, ptrDecel)
	End Function



#End Region

End Class
