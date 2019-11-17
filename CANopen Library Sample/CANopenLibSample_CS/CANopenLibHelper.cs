// ************************************************************************************************
// File Name        : CANopenLibHelper.cs
// Copyright		: 2017, Shanghai AMP & MOONS' Automation Co., Ltd., All rights reserved.
// Module Name		: CANopen Library C# helper
// Author           : Lei Youbing
// Created          : 2016-11-03
//
// Revision History
// No	Version		Date		Revised By		Description
// 1	1.0.17.0223	2017-02-23	Lei Youbing		First released.
// 2	1.0.17.1009	2017-10-09	Lei Youbing		Added canlib32.dll to the included files.
// ************************************************************************************************

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace CANopenLib
{
	public enum EnumAdapter
	{
		Kvaser,
		PEAK,
		ZLG,
	}

	public enum EnumBaudRate
	{
		BitRate1Mbps,
		BitRate800kbps,
		BitRate500kbps,
		BitRate250kbps,
		BitRate125kbps,
		BitRate50kbps,
		BitRate20kbps,
		BitRate12D5kbps,
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct CanMessage
	{
		public int id;
		public uint dlc;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public byte[] msg;
		public uint flag;
		public uint timeStamp;
	};

	[StructLayout(LayoutKind.Sequential)]
	public struct PDOMessage
	{
		public byte NodeID;
		public byte No;
		public byte Len;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public byte[] msg;
	};

	public struct ErrorInfo
	{
		public int ErrorCode;
		public string Command;
		public string ErrorMessage;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct PDOMapping
	{
		public int Index;
		public byte SubIndex;
		public byte BitCounts;
	}


	/// <summary>
	/// DeviceType
	/// </summary>
	public enum ZLGDeviceType
	{
		USBCAN1 = 3,
	}

	public class CANopenLibHelper : IDisposable
	{
		public delegate void EventCallback();

		private const string DLL_FILENAME = "CANopenLib_x86.dll";   // for 64-bit windows,, please comment this line and uncomment next line
		//private const string DLL_FILENAME = "CANopenLib_x64.dll";   // for 32-bit windows, please comment this line and uncomment previous line

		#region public fields
		public int MBERROR_OK = 0; // OK
		//standard specific
		public int MBERROR_FUNC_CODE = 1;
		public int MBERROR_DATA_ADDR = 2;

		//manufacture specific
		public int MBERROR_CAN_NOT_READ = 17;
		public int MBERROR_CAN_NOT_WRITE = 18;
		public int MBERROR_DATA_RANG = 19;

		//other
		public int MBERROR_EXCEPTION = 30;
		public int MBERROR_PORT_IS_CLOSED = 101;
		public int MBERROR_NO_RESPONSE = 102;
		public int MBERROR_RESPONSE_NOT_ENOUGH = 103;
		public int MBERROR_DATA_ERROR = 104;

		public int MBERROR_OpenFAILED = 105;
		public int MBERROR_PORTISNOTOPEN = 106;
		public int MBERROR_NORESPONSE = 107;
		public int MBERROR_INCORRECTRESPONSE = 108;
		public int MBERROR_CHECKSUMERROR = 109;
		public int MBERROR_SCLREGISTER_NOTFOUND = 110;
		#endregion

		#region DllImport

		[DllImport(DLL_FILENAME, EntryPoint = "OnHeartBeatReceive", CharSet = CharSet.Ansi)]
		private static extern void _OnHeartBeatReceive(EventCallback callback);

		[DllImport(DLL_FILENAME, EntryPoint = "OnDataSend", CharSet = CharSet.Ansi)]
		private static extern void _OnDataSend(EventCallback callback);

		[DllImport(DLL_FILENAME, EntryPoint = "OnDataReceive", CharSet = CharSet.Ansi)]
		private static extern void _OnDataReceive(EventCallback callback);

		[DllImport(DLL_FILENAME, EntryPoint = "OnTPDOReceived", CharSet = CharSet.Ansi)]
		private static extern void _OnTPDOReceived(EventCallback callback);

		[DllImport(DLL_FILENAME, EntryPoint = "GetLastHeartBeatMessage", CharSet = CharSet.Ansi)]
		private static extern bool _GetLastHeartBeatMessage(ref CanMessage CANMessage);

		[DllImport(DLL_FILENAME, EntryPoint = "GetLastSentMessage", CharSet = CharSet.Ansi)]
		private static extern bool _GetLastSentMessage(ref CanMessage CANMessage);

		[DllImport(DLL_FILENAME, EntryPoint = "GetLastReceivedMessage", CharSet = CharSet.Ansi)]
		private static extern bool _GetLastReceivedMessage(ref CanMessage CANMessage);

		[DllImport(DLL_FILENAME, EntryPoint = "GetLastTPDOMessage", CharSet = CharSet.Ansi)]
		private static extern bool _GetLastTPDOMessage(ref PDOMessage PDOMessage);

		[DllImport(DLL_FILENAME, EntryPoint = "GetLastTPDOMessageByNodeID", CharSet = CharSet.Ansi)]
		private static extern bool _GetLastTPDOMessageByNodeID(byte nNodeID, byte nPDONo, ref PDOMessage PDOMessage);

		[DllImport(DLL_FILENAME, EntryPoint = "Open", CharSet = CharSet.Ansi)]
		private static extern bool _Open(int adapter, int nBaudRate, byte nChannel);

		[DllImport(DLL_FILENAME, EntryPoint = "IsOpen", CharSet = CharSet.Ansi)]
		private static extern bool _IsOpen();

		[DllImport(DLL_FILENAME, EntryPoint = "Close", CharSet = CharSet.Ansi)]
		private static extern bool _Close();

		[DllImport(DLL_FILENAME, EntryPoint = "SetExecuteTimeOut", CharSet = CharSet.Ansi)]
		private static extern void _SetExecuteTimeOut(byte nExecuteTimeOut);

		[DllImport(DLL_FILENAME, EntryPoint = "GetExecuteTimeOut", CharSet = CharSet.Ansi)]
		private static extern byte _GetExecuteTimeOut();

		[DllImport(DLL_FILENAME, EntryPoint = "SetExecuteRetryTimes", CharSet = CharSet.Ansi)]
		private static extern void _SetExecuteRetryTimes(byte nExecuteRetryTimes);

		[DllImport(DLL_FILENAME, EntryPoint = "GetExecuteRetryTimes", CharSet = CharSet.Ansi)]
		private static extern byte _GetExecuteRetryTimes();

		[DllImport(DLL_FILENAME, EntryPoint = "ResetBuffer", CharSet = CharSet.Ansi)]
		private static extern bool _ResetBuffer();

		[DllImport(DLL_FILENAME, EntryPoint = "Write", CharSet = CharSet.Ansi)]
		private static extern bool _Write(CanMessage sSendCanMessage);

		[DllImport(DLL_FILENAME, EntryPoint = "ExecuteCommand", CharSet = CharSet.Ansi)]
		private static extern bool _ExecuteCommand(CanMessage sSendCanMessage,
			IntPtr pReceivedCanMessage,
			int nCanFunction,
			bool bMatchNodeID,
			byte nNodeID,
			bool bMatchIndex = false,
			int nIndex = 0,
			bool bMatchFirstByte = false,
			byte nFirstByte = 0);

		[DllImport(DLL_FILENAME, EntryPoint = "ClearSendBuffer", CharSet = CharSet.Ansi)]
		private static extern int _ClearSendBuf();

		[DllImport(DLL_FILENAME, EntryPoint = "ClearReceivedBuffer", CharSet = CharSet.Ansi)]
		private static extern int _ClearReceivedBuf();

		[DllImport(DLL_FILENAME, EntryPoint = "ClearBuffer", CharSet = CharSet.Ansi)]
		private static extern int _ClearBuffer();

		[DllImport(DLL_FILENAME, EntryPoint = "ReadSDOInt8", CharSet = CharSet.Ansi)]
		public static extern bool _ReadSDOInt8(byte nNodeID, int nIndex, byte nSubIndex, ref sbyte nData);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadSDOUInt8", CharSet = CharSet.Ansi)]
		public static extern bool _ReadSDOUInt8(byte nNodeID, int nIndex, byte nSubIndex, ref byte nData);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadSDOInt16", CharSet = CharSet.Ansi)]
		public static extern bool _ReadSDOInt16(byte nNodeID, int nIndex, byte nSubIndex, ref short nData);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadSDOUInt16", CharSet = CharSet.Ansi)]
		public static extern bool _ReadSDOUInt16(byte nNodeID, int nIndex, byte nSubIndex, ref ushort nData);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadSDOInt32", CharSet = CharSet.Ansi)]
		public static extern bool _ReadSDOInt32(byte nNodeID, int nIndex, byte nSubIndex, ref int nData);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadSDOUInt32", CharSet = CharSet.Ansi)]
		public static extern bool _ReadSDOUInt32(byte nNodeID, int nIndex, byte nSubIndex, ref uint nData);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteSDOInt8", CharSet = CharSet.Ansi)]
		public static extern bool _WriteSDOInt8(byte nNodeID, int nIndex, byte nSubIndex, sbyte nData);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteSDOUInt8", CharSet = CharSet.Ansi)]
		public static extern bool _WriteSDOUInt8(byte nNodeID, int nIndex, byte nSubIndex, byte nData);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteSDOInt16", CharSet = CharSet.Ansi)]
		public static extern bool _WriteSDOInt16(byte nNodeID, int nIndex, byte nSubIndex, short nData);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteSDOUInt16", CharSet = CharSet.Ansi)]
		public static extern bool _WriteSDOUInt16(byte nNodeID, int nIndex, byte nSubIndex, ushort nData);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteSDOInt32", CharSet = CharSet.Ansi)]
		public static extern bool _WriteSDOInt32(byte nNodeID, int nIndex, byte nSubIndex, int nData);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteSDOUInt32", CharSet = CharSet.Ansi)]
		public static extern bool _WriteSDOUInt32(byte nNodeID, int nIndex, byte nSubIndex, uint nData);

		[DllImport(DLL_FILENAME, EntryPoint = "GetLastErrorInfo", CharSet = CharSet.Ansi)]
		private static extern void _GetLastErrorInfo(ref ErrorInfo errorInfo);

		[DllImport(DLL_FILENAME, EntryPoint = "SetToPreoperationalMode", CharSet = CharSet.Ansi)]
		public static extern bool _SetToPreoperationalMode(byte nNodeID);

		[DllImport(DLL_FILENAME, EntryPoint = "SetToOperationalMode", CharSet = CharSet.Ansi)]
		public static extern bool _SetToOperationalMode(byte nNodeID);

		[DllImport(DLL_FILENAME, EntryPoint = "SetRPDOMapping", CharSet = CharSet.Ansi)]
		private static extern bool _SetRPDOMapping(byte nNodeID, byte nRPDONo, int nLen, PDOMapping[] PDOMappingInfo);

		[DllImport(DLL_FILENAME, EntryPoint = "SetTPDOMapping", CharSet = CharSet.Ansi)]
		private static extern bool _SetTPDOMapping(byte nNodeID, byte nTPDONo, int nLen, PDOMapping[] PDOMappingInfo);

		[DllImport(DLL_FILENAME, EntryPoint = "RestorePDOMappingSettings", CharSet = CharSet.Ansi)]
		public static extern bool _RestorePDOMappingSettings(byte nNodeID);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteRPDO", CharSet = CharSet.Ansi)]
		private static extern bool _WriteRPDO(byte nNodeID, byte nRPDONo, int nLen, byte[] aData);

		[DllImport(DLL_FILENAME, EntryPoint = "SaveParameters", CharSet = CharSet.Ansi)]
		private static extern bool _SaveParameters(byte nNodeID);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadPositionGain", CharSet = CharSet.Ansi)]
		public static extern bool _ReadPositionGain(byte nNodeID, ref ushort nPositionGain);

		[DllImport(DLL_FILENAME, EntryPoint = "WritePositionGain", CharSet = CharSet.Ansi)]
		public static extern bool _WritePositionGain(byte nNodeID, ushort nPositionGain);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadPositionDeriGain", CharSet = CharSet.Ansi)]
		public static extern bool _ReadPositionDeriGain(byte nNodeID, ref ushort nPositionDeriGain);

		[DllImport(DLL_FILENAME, EntryPoint = "WritePositionDeriGain", CharSet = CharSet.Ansi)]
		public static extern bool _WritePositionDeriGain(byte nNodeID, ushort nPositionDeriGain);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadPositionDeriFilter", CharSet = CharSet.Ansi)]
		public static extern bool _ReadPositionDeriFilter(byte nNodeID, ref ushort nPositionDeriFilter);

		[DllImport(DLL_FILENAME, EntryPoint = "WritePositionDeriFilter", CharSet = CharSet.Ansi)]
		public static extern bool _WritePositionDeriFilter(byte nNodeID, ushort nPositionDeriFilter);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadVelocityGain", CharSet = CharSet.Ansi)]
		public static extern bool _ReadVelocityGain(byte nNodeID, ref ushort nVelocityGain);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteVelocityGain", CharSet = CharSet.Ansi)]
		public static extern bool _WriteVelocityGain(byte nNodeID, ushort nVelocityGain);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadVelocityIntegGain", CharSet = CharSet.Ansi)]
		public static extern bool _ReadVelocityIntegGain(byte nNodeID, ref ushort nVelocityIntegGain);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteVelocityIntegGain", CharSet = CharSet.Ansi)]
		public static extern bool _WriteVelocityIntegGain(byte nNodeID, ushort nVelocityIntegGain);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadAccFeedForward", CharSet = CharSet.Ansi)]
		public static extern bool _ReadAccFeedForward(byte nNodeID, ref ushort nAccFeedForward);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteAccFeedForward", CharSet = CharSet.Ansi)]
		public static extern bool _WriteAccFeedForward(byte nNodeID, ushort nAccFeedForward);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadPIDFilter", CharSet = CharSet.Ansi)]
		public static extern bool _ReadPIDFilter(byte nNodeID, ref ushort nPIDFilter);

		[DllImport(DLL_FILENAME, EntryPoint = "WritePIDFilter", CharSet = CharSet.Ansi)]
		public static extern bool _WritePIDFilter(byte nNodeID, uint nPIDFilter);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadNotchFilter", CharSet = CharSet.Ansi)]
		public static extern bool _ReadNotchFilter(byte nNodeID, int nIndex, ref short nNotchFilter);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteNotchFilter", CharSet = CharSet.Ansi)]
		public static extern bool _WriteNotchFilter(byte nNodeID, int nIndex, short nNotchFilter);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadPositionError", CharSet = CharSet.Ansi)]
		public static extern bool _ReadPositionError(byte nNodeID, ref ushort nPositionError);

		[DllImport(DLL_FILENAME, EntryPoint = "WritePositionError", CharSet = CharSet.Ansi)]
		public static extern bool _WritePositionError(byte nNodeID, ushort nPositionError);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadVelocityMax", CharSet = CharSet.Ansi)]
		public static extern bool _ReadVelocityMax(byte nNodeID, ref double nVelocityMax);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteVelocityMax", CharSet = CharSet.Ansi)]
		public static extern bool _WriteVelocityMax(byte nNodeID, double nVelocityMax);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadSmoothFilter", CharSet = CharSet.Ansi)]
		public static extern bool _ReadSmoothFilter(byte nNodeID, ref ushort nSmoothFilter);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteSmoothFilter", CharSet = CharSet.Ansi)]
		public static extern bool _WriteSmoothFilter(byte nNodeID, ushort nSmoothFilter);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadDriverTemp", CharSet = CharSet.Ansi)]
		public static extern bool _ReadDriverTemp(byte nNodeID, ref short nDriverTemp);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadErrorCode", CharSet = CharSet.Ansi)]
		public static extern bool _ReadErrorCode(byte nNodeID, ref ushort nErrorCode);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadErrorCodeUpper", CharSet = CharSet.Ansi)]
		public static extern bool _ReadErrorCodeUpper(byte nNodeID, ref ushort nErrorCode);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteControlWord", CharSet = CharSet.Ansi)]
		public static extern bool _WriteControlWord(byte nNodeID, ushort nControlWord);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadStatusWord", CharSet = CharSet.Ansi)]
		public static extern bool _ReadStatusWord(byte nNodeID, ref ushort nStatusWord);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadQuickStopOptionCode", CharSet = CharSet.Ansi)]
		public static extern bool _ReadQuickStopOptionCode(byte nNodeID, ref short nOptionCode);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteQuickStopOptionCode", CharSet = CharSet.Ansi)]
		public static extern bool _WriteQuickStopOptionCode(byte nNodeID, short ErrorCode);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteModesofOperation", CharSet = CharSet.Ansi)]
		public static extern bool _WriteModesofOperation(byte nNodeID, sbyte nModesofOperation);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadModesofOperation", CharSet = CharSet.Ansi)]
		public static extern bool _ReadModesofOperation(byte nNodeID, ref sbyte nModesofOperation);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadPositionTargetValueCalculated", CharSet = CharSet.Ansi)]
		public static extern bool _ReadPositionTargetValueCalculated(byte nNodeID, ref int nPositionTargetValueCalculated);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadFollowingErrorWindow", CharSet = CharSet.Ansi)]
		public static extern bool _ReadFollowingErrorWindow(byte nNodeID, ref uint nFollowingError);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteFollowingErrorWindow", CharSet = CharSet.Ansi)]
		public static extern bool _WriteFollowingErrorWindow(byte nNodeID, uint nFollowingError);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadVelocityTargetValueCalculated", CharSet = CharSet.Ansi)]
		public static extern bool _ReadVelocityTargetValueCalculated(byte nNodeID, ref double dVelocityTargetValueCalculated);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadTargetTorque", CharSet = CharSet.Ansi)]
		public static extern bool _ReadTargetTorque(byte nNodeID, ref short nTargetTorque);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteTargetTorque", CharSet = CharSet.Ansi)]
		public static extern bool _WriteTargetTorque(byte nNodeID, short nTargetTorque);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadMaxRunningCurrent", CharSet = CharSet.Ansi)]
		public static extern bool _ReadMaxRunningCurrent(byte nNodeID, ref double dRunningCurrent);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteMaxRunningCurrent", CharSet = CharSet.Ansi)]
		public static extern bool _WriteMaxRunningCurrent(byte nNodeID, double dRunningCurrent);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadTorqueDemandValue", CharSet = CharSet.Ansi)]
		public static extern bool _ReadTorqueDemandValue(byte nNodeID, ref double nTorqueDemandValue);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadCurrentActualValue", CharSet = CharSet.Ansi)]
		public static extern bool _ReadCurrentActualValue(byte nNodeID, ref double dCurrentActualValue);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadTargetPosition", CharSet = CharSet.Ansi)]
		public static extern bool _ReadTargetPosition(byte nNodeID, ref int nTargetPositiont);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteTargetPosition", CharSet = CharSet.Ansi)]
		public static extern bool _WriteTargetPosition(byte nNodeID, int nTargetPositiont);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadHomingOffset", CharSet = CharSet.Ansi)]
		public static extern bool _ReadHomingOffset(byte nNodeID, ref int nHomingOffset);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteHomingOffset", CharSet = CharSet.Ansi)]
		public static extern bool _WriteHomingOffset(byte nNodeID, int nHomingOffset);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadPolarity", CharSet = CharSet.Ansi)]
		public static extern bool _ReadPolarity(byte nNodeID, ref byte nPolarity);

		[DllImport(DLL_FILENAME, EntryPoint = "WritePolarity", CharSet = CharSet.Ansi)]
		public static extern bool _WritePolarity(byte nNodeID, byte nPolarity);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadMaxProfileSpeed", CharSet = CharSet.Ansi)]
		public static extern bool _ReadMaxProfileSpeed(byte nNodeID, ref double dMaxProfileSpeed);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteMaxProfileSpeed", CharSet = CharSet.Ansi)]
		public static extern bool _WriteMaxProfileSpeed(byte nNodeID, double dMaxProfileSpeed);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadProfileVelocity", CharSet = CharSet.Ansi)]
		public static extern bool _ReadProfileVelocity(byte nNodeID, ref double dProfileVelocity);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteProfileVelocity", CharSet = CharSet.Ansi)]
		public static extern bool _WriteProfileVelocity(byte nNodeID, double dProfileVelocity);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadProfileAcceleration", CharSet = CharSet.Ansi)]
		public static extern bool _ReadProfileAcceleration(byte nNodeID, ref double dProfileAcceleration);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteProfileAcceleration", CharSet = CharSet.Ansi)]
		public static extern bool _WriteProfileAcceleration(byte nNodeID, double dProfileAcceleration);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadProfileDeceleration", CharSet = CharSet.Ansi)]
		public static extern bool _ReadProfileDeceleration(byte nNodeID, ref double dProfileAcceleration);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteProfileDeceleration", CharSet = CharSet.Ansi)]
		public static extern bool _WriteProfileDeceleration(byte nNodeID, double dProfileAcceleration);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadQuickStopDeceleration", CharSet = CharSet.Ansi)]
		public static extern bool _ReadQuickStopDeceleration(byte nNodeID, ref double dQuickStopDeceleration);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteQuickStopDeceleration", CharSet = CharSet.Ansi)]
		public static extern bool _WriteQuickStopDeceleration(byte nNodeID, double dQuickStopDeceleration);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadTorqueSlop", CharSet = CharSet.Ansi)]
		public static extern bool _ReadTorqueSlop(byte nNodeID, ref uint nTorqueSlop);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteTorqueSlop", CharSet = CharSet.Ansi)]
		public static extern bool _WriteTorqueSlop(byte nNodeID, uint nTorqueSlop);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadHomingMethod", CharSet = CharSet.Ansi)]
		public static extern bool _ReadHomingMethod(byte nNodeID, ref byte nHomingMethod);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteHomingMethod", CharSet = CharSet.Ansi)]
		public static extern bool _WriteHomingMethod(byte nNodeID, byte nHomingMethod);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadHomingSpeedSearchSwitch", CharSet = CharSet.Ansi)]
		public static extern bool _ReadHomingSpeedSearchSwitch(byte nNodeID, ref double dSpeed);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteHomingSpeedSearchSwitch", CharSet = CharSet.Ansi)]
		public static extern bool _WriteHomingSpeedSearchSwitch(byte nNodeID, double dSpeed);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadHomingSpeedSearchIndex", CharSet = CharSet.Ansi)]
		public static extern bool _ReadHomingSpeedSearchIndex(byte nNodeID, ref double dSpeed);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteHomingSpeedSearchIndex", CharSet = CharSet.Ansi)]
		public static extern bool _WriteHomingSpeedSearchIndex(byte nNodeID, double dSpeed);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadHomingAcceleration", CharSet = CharSet.Ansi)]
		public static extern bool _ReadHomingAcceleration(byte nNodeID, ref double dHomingAcceleration);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteHomingAcceleration", CharSet = CharSet.Ansi)]
		public static extern bool _WriteHomingAcceleration(byte nNodeID, double dHomingAcceleration);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadDriveOutputs", CharSet = CharSet.Ansi)]
		public static extern bool _ReadDriveOutputs(byte nNodeID, ref uint nDriveOutputs);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteDriveOutputs", CharSet = CharSet.Ansi)]
		public static extern bool _WriteDriveOutputs(byte nNodeID, uint nDriveOutputs);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadTargetVelocity", CharSet = CharSet.Ansi)]
		public static extern bool _ReadTargetVelocity(byte nNodeID, ref double pTargetVelocity);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteTargetVelocity", CharSet = CharSet.Ansi)]
		public static extern bool _WriteTargetVelocity(byte nNodeID, double dTargetVelocity);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadSupportedDriveModes", CharSet = CharSet.Ansi)]
		public static extern bool _ReadSupportedDriveModes(byte nNodeID, ref uint nModes);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadHomingSwitch", CharSet = CharSet.Ansi)]
		public static extern bool _ReadHomingSwitch(byte nNodeID, ref byte nHomingSwitch);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteHomingSwitch", CharSet = CharSet.Ansi)]
		public static extern bool _WriteHomingSwitch(byte nNodeID, byte nHomingSwitch);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadIdleCurrent", CharSet = CharSet.Ansi)]
		public static extern bool _ReadIdleCurrent(byte nNodeID, ref double dIdleCurrent);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteIdleCurrent", CharSet = CharSet.Ansi)]
		public static extern bool _WriteIdleCurrent(byte nNodeID, double dIdleCurrent);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadDisplayDriveInputs", CharSet = CharSet.Ansi)]
		public static extern bool _ReadDisplayDriveInputs(byte nNodeID, ref int nInputs);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadTorqueConstant", CharSet = CharSet.Ansi)]
		public static extern bool _ReadTorqueConstant(byte nNodeID, ref ushort nTorqueConstant);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteTorqueConstant", CharSet = CharSet.Ansi)]
		public static extern bool _WriteTorqueConstant(byte nNodeID, ushort nTorqueConstant);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteDSPClearAlarm", CharSet = CharSet.Ansi)]
		public static extern bool _WriteDSPClearAlarm(byte nNodeID);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadQSegment", CharSet = CharSet.Ansi)]
		public static extern bool _ReadQSegment(byte nNodeID, ref byte nQSegment);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteQSegment", CharSet = CharSet.Ansi)]
		public static extern bool _WriteQSegment(byte nNodeID, byte nQSegment);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadActualVelocity", CharSet = CharSet.Ansi)]
		public static extern bool _ReadActualVelocity(byte nNodeID, ref double dActualVelocity);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadActualPosition", CharSet = CharSet.Ansi)]
		public static extern bool _ReadActualPosition(byte nNodeID, ref int nActualPosition);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadDSPStatusCode", CharSet = CharSet.Ansi)]
		public static extern bool _ReadDSPStatusCode(byte nNodeID, ref ushort nStatusCode);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteClearPosition", CharSet = CharSet.Ansi)]
		public static extern bool _WriteClearPosition(byte nNodeID);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadAccelerationCurrent", CharSet = CharSet.Ansi)]
		public static extern bool _ReadAccelerationCurrent(byte nNodeID, ref double dAccelerationCurrent);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteAccelerationCurrent", CharSet = CharSet.Ansi)]
		public static extern bool _WriteAccelerationCurrent(byte nNodeID, double dAccelerationCurrent);

		[DllImport(DLL_FILENAME, EntryPoint = "ReadAnalogInput1", CharSet = CharSet.Ansi)]
		public static extern bool _ReadAnalogInput1(byte nNodeID, ref ushort nAnalogInput1);

		[DllImport(DLL_FILENAME, EntryPoint = "WriteProfileParam", CharSet = CharSet.Ansi)]
		public static extern bool _WriteProfileParam(byte nNodeID, IntPtr nModes, IntPtr nDistance, IntPtr dVelocity, IntPtr dAccel, IntPtr dDecel);

		[DllImport(DLL_FILENAME, EntryPoint = "SwitchControlWord", CharSet = CharSet.Ansi)]
		public static extern bool _SwitchControlWord(byte nNodeID, ushort nControlWord1, ushort nControlWord2);

		[DllImport(DLL_FILENAME, EntryPoint = "DriveEnable", CharSet = CharSet.Ansi)]
		public static extern bool _DriveEnable(byte nNodeID, bool bEnabled);

		[DllImport(DLL_FILENAME, EntryPoint = "Stop", CharSet = CharSet.Ansi)]
		public static extern bool _Stop(byte nNodeID);

		[DllImport(DLL_FILENAME, EntryPoint = "AlarmReset", CharSet = CharSet.Ansi)]
		public static extern bool _AlarmReset(byte nNodeID);

		[DllImport(DLL_FILENAME, EntryPoint = "RelMove", CharSet = CharSet.Ansi)]
		public static extern bool _RelMove(byte nNodeID, int nDistance, IntPtr dVelocity, IntPtr dAccel, IntPtr dDecel);

		[DllImport(DLL_FILENAME, EntryPoint = "AbsMove", CharSet = CharSet.Ansi)]
		public static extern bool _AbsMove(byte nNodeID, int nDistance, IntPtr dVelocity, IntPtr dAccel, IntPtr dDecel);

		[DllImport(DLL_FILENAME, EntryPoint = "MultipleAbsMoveWithStopping", CharSet = CharSet.Ansi)]
		public static extern bool _MultipleAbsMoveWithStopping(byte nNodeID, int nDistance1, int nDistance2, IntPtr dVelocity1, IntPtr dVelocity2, IntPtr dAccel, IntPtr dDecel);

		[DllImport(DLL_FILENAME, EntryPoint = "MultipleAbsMoveContinuous", CharSet = CharSet.Ansi)]
		public static extern bool _MultipleAbsMoveContinuous(byte nNodeID, int nDistance1, int nDistance2, IntPtr dVelocity1, IntPtr dVelocity2, IntPtr dAccel, IntPtr dDecel, [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.I1)] bool bImmediateChange);

		[DllImport(DLL_FILENAME, EntryPoint = "ExecuteNormalQProgram", CharSet = CharSet.Ansi)]
		public static extern bool _ExecuteNormalQProgram(byte nNodeID, byte nSegment);

		[DllImport(DLL_FILENAME, EntryPoint = "ExecuteSyncQProgram", CharSet = CharSet.Ansi)]
		public static extern bool _ExecuteSyncQProgram(byte nNodeID, byte nSegment, uint nSyncPulse);

		[DllImport(DLL_FILENAME, EntryPoint = "Homing", CharSet = CharSet.Ansi)]
		public static extern bool _Homing(byte nNodeID, int nHomingMethod, IntPtr dHomingVelocity, IntPtr dIndexVelocity, IntPtr dHomingAccel, IntPtr nHomingOffset, IntPtr nHomingSwitch);

		[DllImport(DLL_FILENAME, EntryPoint = "LaunchVelocityMode", CharSet = CharSet.Ansi)]
		public static extern bool _LaunchVelocityMode(byte nNodeID, IntPtr dVelocity, IntPtr dAccel, IntPtr dDecel);

		#endregion

		#region public Methods

		private static EventCallback m_HeartBeatReceiveCallback;

		private static EventCallback m_DataReceivedCallback;

		private static EventCallback m_DataSentCallback;

		private static EventCallback m_TPDOReceivedCallback;

		public delegate void OnCanMessageEventHandler(CanMessageEventHandle e);

		public delegate void OnTPDOEventHandler(TPDOReceivedEventHandle e);

		public event OnCanMessageEventHandler HeartBeatReceive;

		public event OnCanMessageEventHandler DataSent;

		public event OnCanMessageEventHandler DataReceived;

		public event OnTPDOEventHandler TPDOReceived;

		public void HeartBeatReceiveCallbackFunction()
		{
			if (HeartBeatReceive != null)
			{
				CanMessage canMessage = new CanMessage();

				GetLastHeartBeatMessage(ref canMessage);

				CanMessageEventHandle e = new CanMessageEventHandle(canMessage);
				HeartBeatReceive(e);
			}
		}

		public void DataSentCallbackFunction()
		{
			if (DataSent != null)
			{
				CanMessage canMessage = new CanMessage();



				GetLastSentMessage(ref canMessage);

				CanMessageEventHandle e = new CanMessageEventHandle(canMessage);
				DataSent(e);
			}
		}

		public void DataReceivedCallbackFunction()
		{
			if (DataReceived != null)
			{
				CanMessage canMessage = new CanMessage();

				GetLastReceivedMessage(ref canMessage);

				CanMessageEventHandle e = new CanMessageEventHandle(canMessage);
				DataReceived(e);

			}
		}

		public void TPDOReceivedCallbackFunction()
		{
			if (TPDOReceived != null)
			{
				PDOMessage PDOMessage = new PDOMessage();

				GetLastTPDOMessage(ref PDOMessage);

				TPDOReceivedEventHandle e = new TPDOReceivedEventHandle(PDOMessage);
				TPDOReceived(e);

			}
		}

		public CANopenLibHelper()
		{
			m_HeartBeatReceiveCallback = HeartBeatReceiveCallbackFunction;
			_OnHeartBeatReceive(m_HeartBeatReceiveCallback);

			m_DataSentCallback = DataSentCallbackFunction;
			_OnDataSend(m_DataSentCallback);

			m_DataReceivedCallback = DataReceivedCallbackFunction;
			_OnDataReceive(m_DataReceivedCallback);

			m_TPDOReceivedCallback = TPDOReceivedCallbackFunction;
			_OnTPDOReceived(m_TPDOReceivedCallback);
		}

		#region IDisposable Memberships

		public void Dispose()
		{
			Close();
		}
		#endregion

		public void GetLastHeartBeatMessage(ref CanMessage CANMessage)
		{
			_GetLastHeartBeatMessage(ref CANMessage);
		}

		public void GetLastSentMessage(ref CanMessage CANMessage)
		{
			_GetLastSentMessage(ref CANMessage);
		}

		public void GetLastReceivedMessage(ref CanMessage CANMessage)
		{
			_GetLastReceivedMessage(ref CANMessage);
		}

		public void GetLastTPDOMessage(ref PDOMessage TPDOMessage)
		{
			_GetLastTPDOMessage(ref TPDOMessage);
		}

		public void GetLastTPDOMessageByNodeID(byte nNodeID, byte nPDONo, ref PDOMessage TPDOMessage)
		{
			_GetLastTPDOMessageByNodeID(nNodeID, nPDONo, ref TPDOMessage);
		}

		public bool Open(EnumAdapter adapter, EnumBaudRate nBaudRate, byte nChannel)
		{
			return _Open((int)adapter, (int)nBaudRate, nChannel);
		}

		public bool IsOpen()
		{
			return _IsOpen();
		}

		public bool Close()
		{
			return _Close();
		}

		public void SetExecuteTimeOut(byte nExecuteTimeOut)
		{
			_SetExecuteTimeOut(nExecuteTimeOut);
		}

		public byte GetExecuteTimeOut()
		{
			return _GetExecuteTimeOut();
		}

		public void SetExecuteRetryTimes(byte nExecuteRetryTimes)
		{
			_SetExecuteRetryTimes(nExecuteRetryTimes);
		}

		public byte GetExecuteRetryTimes()
		{
			return _GetExecuteRetryTimes();
		}

		public bool ResetBuffer()
		{
			return _ResetBuffer();
		}

		public bool Write(CanMessage sSendCanMessage)
		{
			return _Write(sSendCanMessage);
		}

		public bool ExecuteCommand(CanMessage sSendCanMessage,
			ref CanMessage sReceivedCanMessage,
			int nCanFunction,
			bool bMatchNodeID,
			byte nNodeID,
			bool bMatchIndex = false,
			int nIndex = 0,
			bool bMatchFirstByte = false,
			byte nFirstByte = 0)
		{
			IntPtr pReceivedCanMessage = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(CanMessage)));

			bool ret = _ExecuteCommand(sSendCanMessage,
				pReceivedCanMessage,
				nCanFunction,
				bMatchNodeID,
				nNodeID,
				bMatchIndex,
				nIndex,
				bMatchFirstByte,
				nFirstByte);

			if (ret)
			{
				sReceivedCanMessage = (CanMessage)Marshal.PtrToStructure((IntPtr)((UInt32)pReceivedCanMessage), typeof(CanMessage));
			}
			Marshal.FreeHGlobal(pReceivedCanMessage);
			return ret;
		}


		public bool ReadSDOInt8(byte nNodeID, int nIndex, byte nSubIndex, ref sbyte nData)
		{
			return _ReadSDOInt8(nNodeID, nIndex, nSubIndex, ref nData);
		}

		public bool ReadSDOUInt8(byte nNodeID, int nIndex, byte nSubIndex, ref byte nData)
		{
			return _ReadSDOUInt8(nNodeID, nIndex, nSubIndex, ref nData);
		}

		public bool ReadSDOInt16(byte nNodeID, int nIndex, byte nSubIndex, ref short nData)
		{
			return _ReadSDOInt16(nNodeID, nIndex, nSubIndex, ref nData);
		}

		public bool ReadSDOUInt16(byte nNodeID, int nIndex, byte nSubIndex, ref ushort nData)
		{
			return _ReadSDOUInt16(nNodeID, nIndex, nSubIndex, ref nData);
		}

		public bool ReadSDOInt32(byte nNodeID, int nIndex, byte nSubIndex, ref int nData)
		{
			return _ReadSDOInt32(nNodeID, nIndex, nSubIndex, ref nData);
		}

		public bool ReadSDOUInt32(byte nNodeID, int nIndex, byte nSubIndex, ref uint nData)
		{
			return _ReadSDOUInt32(nNodeID, nIndex, nSubIndex, ref nData);
		}

		public bool WriteSDOInt8(byte nNodeID, int nIndex, byte nSubIndex, sbyte nData)
		{
			return _WriteSDOInt8(nNodeID, nIndex, nSubIndex, nData);
		}

		public bool WriteSDOUInt8(byte nNodeID, int nIndex, byte nSubIndex, byte nData)
		{
			return _WriteSDOUInt8(nNodeID, nIndex, nSubIndex, nData);
		}

		public bool WriteSDOInt16(byte nNodeID, int nIndex, byte nSubIndex, short nData)
		{
			return _WriteSDOInt16(nNodeID, nIndex, nSubIndex, nData);
		}

		public bool WriteSDOUInt16(byte nNodeID, int nIndex, byte nSubIndex, ushort nData)
		{
			return _WriteSDOUInt16(nNodeID, nIndex, nSubIndex, nData);
		}

		public bool WriteSDOInt32(byte nNodeID, int nIndex, byte nSubIndex, int nData)
		{
			return _WriteSDOInt32(nNodeID, nIndex, nSubIndex, nData);
		}

		public bool WriteSDOUInt32(byte nNodeID, int nIndex, byte nSubIndex, uint nData)
		{
			return _WriteSDOUInt32(nNodeID, nIndex, nSubIndex, nData);
		}

		public void GetLastErrorInfo(ref ErrorInfo errorInfo)
		{
			_GetLastErrorInfo(ref errorInfo);
		}

		public bool SetToPreoperationalMode(byte nNodeID)
		{
			return _SetToPreoperationalMode(nNodeID);
		}

		public bool SetToOperationalMode(byte nNodeID)
		{
			return _SetToOperationalMode(nNodeID);
		}

		public bool SetRPDOMapping(byte nNodeID, byte nRPDONo, int nLen, PDOMapping[] PdoMappingArr)
		{
			return _SetRPDOMapping(nNodeID, nRPDONo, nLen, PdoMappingArr);
		}

		public bool SetTPDOMapping(byte nNodeID, byte nTPDONo, int nLen, PDOMapping[] PdoMappingArr)
		{
			return _SetTPDOMapping(nNodeID, nTPDONo, nLen, PdoMappingArr);
		}

		public bool RestorePDOMappingSettings(byte nNodeID)
		{
			return _RestorePDOMappingSettings(nNodeID);
		}

		public bool WriteRPDO(byte nNodeID, byte nRPDONo, int nLen, byte[] aData)
		{
			if (aData.Length < nLen)
			{
				throw new IndexOutOfRangeException();
			}
			return _WriteRPDO(nNodeID, nRPDONo, nLen, aData);
		}

		public bool SaveParameters(byte nNodeID)
		{
			return _SaveParameters(nNodeID);
		}

		public bool ReadPositionGain(byte nNodeID, ref ushort nPositionGain)
		{
			return _ReadPositionGain(nNodeID, ref nPositionGain);
		}

		public bool WritePositionGain(byte nNodeID, ushort nPositionGain)
		{
			return _WritePositionGain(nNodeID, nPositionGain);
		}

		public bool ReadPositionDeriGain(byte nNodeID, ref ushort nPositionDeriGain)
		{
			return _ReadPositionDeriGain(nNodeID, ref nPositionDeriGain);
		}

		public bool WritePositionDeriGain(byte nNodeID, ushort nPositionDeriGain)
		{
			return _WritePositionDeriGain(nNodeID, nPositionDeriGain);
		}

		public bool ReadPositionDeriFilter(byte nNodeID, ref ushort nPositionDeriFilter)
		{
			return _ReadPositionDeriFilter(nNodeID, ref nPositionDeriFilter);
		}

		public bool WritePositionDeriFilter(byte nNodeID, ushort nPositionDeriFilter)
		{
			return _WritePositionDeriFilter(nNodeID, nPositionDeriFilter);
		}

		public bool ReadVelocityGain(byte nNodeID, ref ushort nVelocityGain)
		{
			return _ReadVelocityGain(nNodeID, ref nVelocityGain);
		}

		public bool WriteVelocityGain(byte nNodeID, ushort nVelocityGain)
		{
			return _WriteVelocityGain(nNodeID, nVelocityGain);
		}

		public bool ReadVelocityIntegGain(byte nNodeID, ref ushort nVelocityIntegGain)
		{
			return _ReadVelocityIntegGain(nNodeID, ref nVelocityIntegGain);
		}

		public bool WriteVelocityIntegGain(byte nNodeID, ushort nVelocityIntegGain)
		{
			return _WriteVelocityIntegGain(nNodeID, nVelocityIntegGain);
		}

		public bool ReadAccFeedForward(byte nNodeID, ref ushort nAccFeedForward)
		{
			return _ReadAccFeedForward(nNodeID, ref nAccFeedForward);
		}

		public bool WriteAccFeedForward(byte nNodeID, ushort nAccFeedForward)
		{
			return _WriteAccFeedForward(nNodeID, nAccFeedForward);
		}

		public bool ReadPIDFilter(byte nNodeID, ref ushort nPIDFilter)
		{
			return _ReadPIDFilter(nNodeID, ref nPIDFilter);
		}

		public bool WritePIDFilter(byte nNodeID, uint nPIDFilter)
		{
			return _WritePIDFilter(nNodeID, nPIDFilter);
		}

		public bool ReadNotchFilter(byte nNodeID, int nIndex, ref short nNotchFilter)
		{
			return _ReadNotchFilter(nNodeID, nIndex, ref nNotchFilter);
		}

		public bool WriteNotchFilter(byte nNodeID, int nIndex, short nNotchFilter)
		{
			return _WriteNotchFilter(nNodeID, nIndex, nNotchFilter);
		}

		public bool ReadPositionError(byte nNodeID, ref ushort nPositionError)
		{
			return _ReadPositionError(nNodeID, ref nPositionError);
		}

		public bool WritePositionError(byte nNodeID, ushort nPositionError)
		{
			return _WritePositionError(nNodeID, nPositionError);
		}

		public bool ReadVelocityMax(byte nNodeID, ref double nVelocityMax)
		{
			return _ReadVelocityMax(nNodeID, ref nVelocityMax);
		}

		public bool WriteVelocityMax(byte nNodeID, double nVelocityMax)
		{
			return _WriteVelocityMax(nNodeID, nVelocityMax);
		}

		public bool ReadSmoothFilter(byte nNodeID, ref ushort nSmoothFilter)
		{
			return _ReadSmoothFilter(nNodeID, ref nSmoothFilter);
		}

		public bool WriteSmoothFilter(byte nNodeID, ushort nSmoothFilter)
		{
			return _WriteSmoothFilter(nNodeID, nSmoothFilter);
		}

		public bool ReadDriverTemp(byte nNodeID, ref short nDriverTemp)
		{
			return _ReadDriverTemp(nNodeID, ref nDriverTemp);
		}

		public bool ReadErrorCode(byte nNodeID, ref ushort nErrorCode)
		{
			return _ReadErrorCode(nNodeID, ref nErrorCode);
		}

		public bool ReadErrorCodeUpper(byte nNodeID, ref ushort nErrorCode)
		{
			return _ReadErrorCodeUpper(nNodeID, ref nErrorCode);
		}

		public bool WriteControlWord(byte nNodeID, ushort nControlWord)
		{
			return _WriteControlWord(nNodeID, nControlWord);
		}

		public bool ReadStatusWord(byte nNodeID, ref ushort nStatusWord)
		{
			return _ReadStatusWord(nNodeID, ref nStatusWord);
		}

		public bool ReadQuickStopOptionCode(byte nNodeID, ref short nOptionCode)
		{
			return _ReadQuickStopOptionCode(nNodeID, ref nOptionCode);
		}

		public bool WriteQuickStopOptionCode(byte nNodeID, short ErrorCode)
		{
			return _WriteQuickStopOptionCode(nNodeID, ErrorCode);
		}

		public bool WriteModesofOperation(byte nNodeID, sbyte nModesofOperation)
		{
			return _WriteModesofOperation(nNodeID, nModesofOperation);
		}

		public bool ReadModesofOperation(byte nNodeID, ref sbyte nModesofOperation)
		{
			return _ReadModesofOperation(nNodeID, ref nModesofOperation);
		}

		public bool ReadPositionTargetValueCalculated(byte nNodeID, ref int nPositionTargetValueCalculated)
		{
			return _ReadPositionTargetValueCalculated(nNodeID, ref nPositionTargetValueCalculated);
		}

		public bool ReadFollowingErrorWindow(byte nNodeID, ref uint nFollowingError)
		{
			return _ReadFollowingErrorWindow(nNodeID, ref nFollowingError);
		}

		public bool WriteFollowingErrorWindow(byte nNodeID, uint nFollowingError)
		{
			return _WriteFollowingErrorWindow(nNodeID, nFollowingError);
		}

		public bool ReadVelocityTargetValueCalculated(byte nNodeID, ref double dVelocityTargetValueCalculated)
		{
			return _ReadVelocityTargetValueCalculated(nNodeID, ref dVelocityTargetValueCalculated);
		}

		public bool ReadTargetTorque(byte nNodeID, ref short nTargetTorque)
		{
			return _ReadTargetTorque(nNodeID, ref nTargetTorque);
		}

		public bool WriteTargetTorque(byte nNodeID, short nTargetTorque)
		{
			return _WriteTargetTorque(nNodeID, nTargetTorque);
		}

		public bool ReadMaxRunningCurrent(byte nNodeID, ref double dRunningCurrent)
		{
			return _ReadMaxRunningCurrent(nNodeID, ref dRunningCurrent);
		}

		public bool WriteMaxRunningCurrent(byte nNodeID, double dRunningCurrent)
		{
			return _WriteMaxRunningCurrent(nNodeID, dRunningCurrent);
		}

		public bool ReadTorqueDemandValue(byte nNodeID, ref double nTorqueDemandValue)
		{
			return _ReadTorqueDemandValue(nNodeID, ref nTorqueDemandValue);
		}

		public bool ReadCurrentActualValue(byte nNodeID, ref double nCurrentActualValue)
		{
			return _ReadCurrentActualValue(nNodeID, ref nCurrentActualValue);
		}

		public bool ReadTargetPosition(byte nNodeID, ref int nTargetPositiont)
		{
			return _ReadTargetPosition(nNodeID, ref nTargetPositiont);
		}

		public bool WriteTargetPosition(byte nNodeID, int nTargetPositiont)
		{
			return _WriteTargetPosition(nNodeID, nTargetPositiont);
		}

		public bool ReadHomingOffset(byte nNodeID, ref int nHomingOffset)
		{
			return _ReadHomingOffset(nNodeID, ref nHomingOffset);
		}

		public bool WriteHomingOffset(byte nNodeID, int nHomingOffset)
		{
			return _WriteHomingOffset(nNodeID, nHomingOffset);
		}

		public bool ReadPolarity(byte nNodeID, ref byte nPolarity)
		{
			return _ReadPolarity(nNodeID, ref nPolarity);
		}

		public bool WritePolarity(byte nNodeID, byte nPolarity)
		{
			return _WritePolarity(nNodeID, nPolarity);
		}

		public bool ReadMaxProfileSpeed(byte nNodeID, ref double dMaxProfileSpeed)
		{
			return _ReadMaxProfileSpeed(nNodeID, ref dMaxProfileSpeed);
		}

		public bool WriteMaxProfileSpeed(byte nNodeID, double dMaxProfileSpeed)
		{
			return _WriteMaxProfileSpeed(nNodeID, dMaxProfileSpeed);
		}

		public bool ReadProfileVelocity(byte nNodeID, ref double dProfileVelocity)
		{
			return _ReadProfileVelocity(nNodeID, ref dProfileVelocity);
		}

		public bool WriteProfileVelocity(byte nNodeID, double dProfileVelocity)
		{
			return _WriteProfileVelocity(nNodeID, dProfileVelocity);
		}

		public bool ReadProfileAcceleration(byte nNodeID, ref double dProfileAcceleration)
		{
			return _ReadProfileAcceleration(nNodeID, ref dProfileAcceleration);
		}

		public bool WriteProfileAcceleration(byte nNodeID, double dProfileAcceleration)
		{
			return _WriteProfileAcceleration(nNodeID, dProfileAcceleration);
		}

		public bool ReadProfileDeceleration(byte nNodeID, ref double dProfileAcceleration)
		{
			return _ReadProfileDeceleration(nNodeID, ref dProfileAcceleration);
		}

		public bool WriteProfileDeceleration(byte nNodeID, double dProfileAcceleration)
		{
			return _WriteProfileDeceleration(nNodeID, dProfileAcceleration);
		}

		public bool ReadQuickStopDeceleration(byte nNodeID, ref double dQuickStopDeceleration)
		{
			return _ReadQuickStopDeceleration(nNodeID, ref dQuickStopDeceleration);
		}

		public bool WriteQuickStopDeceleration(byte nNodeID, double dQuickStopDeceleration)
		{
			return _WriteQuickStopDeceleration(nNodeID, dQuickStopDeceleration);
		}

		public bool ReadTorqueSlop(byte nNodeID, ref uint nTorqueSlop)
		{
			return _ReadTorqueSlop(nNodeID, ref nTorqueSlop);
		}

		public bool WriteTorqueSlop(byte nNodeID, uint nTorqueSlop)
		{
			return _WriteTorqueSlop(nNodeID, nTorqueSlop);
		}

		public bool ReadHomingMethod(byte nNodeID, ref byte nHomingMethod)
		{
			return _ReadHomingMethod(nNodeID, ref nHomingMethod);
		}

		public bool WriteHomingMethod(byte nNodeID, byte nHomingMethod)
		{
			return _WriteHomingMethod(nNodeID, nHomingMethod);
		}

		public bool ReadHomingSpeedSearchSwitch(byte nNodeID, ref double dSpeed)
		{
			return _ReadHomingSpeedSearchSwitch(nNodeID, ref dSpeed);
		}

		public bool WriteHomingSpeedSearchSwitch(byte nNodeID, double dSpeed)
		{
			return _WriteHomingSpeedSearchSwitch(nNodeID, dSpeed);
		}

		public bool ReadHomingSpeedSearchIndex(byte nNodeID, ref double dSpeed)
		{
			return _ReadHomingSpeedSearchIndex(nNodeID, ref dSpeed);
		}

		public bool WriteHomingSpeedSearchIndex(byte nNodeID, double dSpeed)
		{
			return _WriteHomingSpeedSearchIndex(nNodeID, dSpeed);
		}

		public bool ReadHomingAcceleration(byte nNodeID, ref double dHomingAcceleration)
		{
			return _ReadHomingAcceleration(nNodeID, ref dHomingAcceleration);
		}

		public bool WriteHomingAcceleration(byte nNodeID, double dHomingAcceleration)
		{
			return _WriteHomingAcceleration(nNodeID, dHomingAcceleration);
		}

		public bool ReadDriveOutputs(byte nNodeID, ref uint nDriveOutputs)
		{
			return _ReadDriveOutputs(nNodeID, ref nDriveOutputs);
		}

		public bool WriteDriveOutputs(byte nNodeID, uint nDriveOutputs)
		{
			return _WriteDriveOutputs(nNodeID, nDriveOutputs);
		}

		public bool ReadTargetVelocity(byte nNodeID, ref double dTargetVelocity)
		{
			return _ReadTargetVelocity(nNodeID, ref dTargetVelocity);
		}

		public bool WriteTargetVelocity(byte nNodeID, double dTargetVelocity)
		{
			return _WriteTargetVelocity(nNodeID, dTargetVelocity);
		}

		public bool ReadSupportedDriveModes(byte nNodeID, ref uint nModes)
		{
			return _ReadSupportedDriveModes(nNodeID, ref nModes);
		}

		public bool ReadHomingSwitch(byte nNodeID, ref byte nHomingSwitch)
		{
			return _ReadHomingSwitch(nNodeID, ref nHomingSwitch);
		}

		public bool WriteHomingSwitch(byte nNodeID, byte nHomingSwitch)
		{
			return _WriteHomingSwitch(nNodeID, nHomingSwitch);
		}

		public bool ReadIdleCurrent(byte nNodeID, ref double dIdleCurrent)
		{
			return _ReadIdleCurrent(nNodeID, ref dIdleCurrent);
		}

		public bool WriteIdleCurrent(byte nNodeID, double dIdleCurrent)
		{
			return _WriteIdleCurrent(nNodeID, dIdleCurrent);
		}

		public bool ReadDisplayDriveInputs(byte nNodeID, ref int nInputs)
		{
			return _ReadDisplayDriveInputs(nNodeID, ref nInputs);
		}

		public bool ReadTorqueConstant(byte nNodeID, ref ushort nTorqueConstant)
		{
			return _ReadTorqueConstant(nNodeID, ref nTorqueConstant);
		}

		public bool WriteTorqueConstant(byte nNodeID, ushort nTorqueConstant)
		{
			return _WriteTorqueConstant(nNodeID, nTorqueConstant);
		}

		public bool WriteDSPClearAlarm(byte nNodeID)
		{
			return _WriteDSPClearAlarm(nNodeID);
		}

		public bool ReadQSegment(byte nNodeID, ref byte nQSegment)
		{
			return _ReadQSegment(nNodeID, ref nQSegment);
		}

		public bool WriteQSegment(byte nNodeID, byte nQSegment)
		{
			return _WriteQSegment(nNodeID, nQSegment);
		}

		public bool ReadActualVelocity(byte nNodeID, ref double dActualVelocity)
		{
			return _ReadActualVelocity(nNodeID, ref dActualVelocity);
		}

		public bool ReadActualPosition(byte nNodeID, ref int nActualPosition)
		{
			return _ReadActualPosition(nNodeID, ref nActualPosition);
		}

		public bool ReadDSPStatusCode(byte nNodeID, ref ushort nStatusCode)
		{
			return _ReadDSPStatusCode(nNodeID, ref nStatusCode);
		}

		public bool WriteClearPosition(byte nNodeID)
		{
			return _WriteClearPosition(nNodeID);
		}

		public bool ReadAccelerationCurrent(byte nNodeID, ref double dAccelerationCurrent)
		{
			return _ReadAccelerationCurrent(nNodeID, ref dAccelerationCurrent);
		}

		public bool WriteAccelerationCurrent(byte nNodeID, double dAccelerationCurrent)
		{
			return _WriteAccelerationCurrent(nNodeID, dAccelerationCurrent);
		}

		public bool ReadAnalogInput1(byte nNodeID, ref ushort nAnalogInput1)
		{
			return _ReadAnalogInput1(nNodeID, ref nAnalogInput1);
		}

		public bool WriteProfileParam(byte nNodeID, int? nModes, int? nDistance, double? dVelocity, double? dAccel, double? dDecel)
		{
			IntPtr ptrModes = IntPtr.Zero;
			if (nModes != null)
			{
				ptrModes = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)));
				Marshal.StructureToPtr(nModes.Value, ptrModes, true);
			}
			IntPtr ptrDistance = IntPtr.Zero;
			if (nDistance != null)
			{
				ptrDistance = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)));
				Marshal.StructureToPtr(nDistance.Value, ptrDistance, true);
			}
			IntPtr ptrVelocity = IntPtr.Zero;
			if (dVelocity != null)
			{
				ptrVelocity = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dVelocity.Value, ptrVelocity, true);
			}
			IntPtr ptrAccel = IntPtr.Zero;
			if (dAccel != null)
			{
				ptrAccel = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dAccel.Value, ptrAccel, true);
			}
			IntPtr ptrDecel = IntPtr.Zero;
			if (dDecel != null)
			{
				ptrDecel = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dDecel.Value, ptrDecel, true);
			}
			return _WriteProfileParam(nNodeID, ptrModes, ptrDistance, ptrVelocity, ptrAccel, ptrDecel);
		}

		public bool SwitchControlWord(byte nNodeID, ushort nControlWord1, ushort nControlWord2)
		{
			return _SwitchControlWord(nNodeID, nControlWord1, nControlWord2);
		}

		public bool DriveEnable(byte nNodeID, bool bEnabled)
		{
			return _DriveEnable(nNodeID, bEnabled);
		}

		public bool Stop(byte nNodeID)
		{
			return _Stop(nNodeID);
		}

		public bool AlarmReset(byte nNodeID)
		{
			return _AlarmReset(nNodeID);
		}

		public bool RelMove(byte nNodeID, int nDistance, double? dVelocity, double? dAccel, double? dDecel)
		{
			IntPtr ptrVelocity = IntPtr.Zero;
			if (dVelocity != null)
			{
				ptrVelocity = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dVelocity.Value, ptrVelocity, true);
			}
			IntPtr ptrAccel = IntPtr.Zero;
			if (dAccel != null)
			{
				ptrAccel = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dAccel.Value, ptrAccel, true);
			}
			IntPtr ptrDecel = IntPtr.Zero;
			if (dDecel != null)
			{
				ptrDecel = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dDecel.Value, ptrDecel, true);
			}
			return _RelMove(nNodeID, nDistance, ptrVelocity, ptrAccel, ptrDecel);
		}

		public bool AbsMove(byte nNodeID, int nDistance, double? dVelocity, double? dAccel, double? dDecel)
		{
			IntPtr ptrVelocity = IntPtr.Zero;
			if (dVelocity != null)
			{
				ptrVelocity = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dVelocity.Value, ptrVelocity, true);
			}
			IntPtr ptrAccel = IntPtr.Zero;
			if (dAccel != null)
			{
				ptrAccel = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dAccel.Value, ptrAccel, true);
			}
			IntPtr ptrDecel = IntPtr.Zero;
			if (dDecel != null)
			{
				ptrDecel = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dDecel.Value, ptrDecel, true);
			}
			return _AbsMove(nNodeID, nDistance, ptrVelocity, ptrAccel, ptrDecel);
		}

		public bool MultipleAbsMoveWithStopping(byte nNodeID, int nDistance1, int nDistance2, double? dVelocity1, double? dVelocity2, double? dAccel, double? dDecel)
		{
			IntPtr ptrVelocity1 = IntPtr.Zero;
			if (dVelocity1 != null)
			{
				ptrVelocity1 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dVelocity1.Value, ptrVelocity1, true);
			}
			IntPtr ptrVelocity2 = IntPtr.Zero;
			if (dVelocity2 != null)
			{
				ptrVelocity2 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dVelocity2.Value, ptrVelocity2, true);
			}
			IntPtr ptrAccel = IntPtr.Zero;
			if (dAccel != null)
			{
				ptrAccel = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dAccel.Value, ptrAccel, true);
			}
			IntPtr ptrDecel = IntPtr.Zero;
			if (dDecel != null)
			{
				ptrDecel = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dDecel.Value, ptrDecel, true);
			}
			return _MultipleAbsMoveWithStopping(nNodeID, nDistance1, nDistance2, ptrVelocity1, ptrVelocity2, ptrAccel, ptrDecel);
		}

		public bool MultipleAbsMoveContinuous(byte nNodeID, int nDistance1, int nDistance2, double? dVelocity1, double? dVelocity2, double? dAccel, double? dDecel, bool bImmediateChange)
		{
			IntPtr ptrVelocity1 = IntPtr.Zero;
			if (dVelocity1 != null)
			{
				ptrVelocity1 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dVelocity1.Value, ptrVelocity1, true);
			}
			IntPtr ptrVelocity2 = IntPtr.Zero;
			if (dVelocity2 != null)
			{
				ptrVelocity2 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dVelocity2.Value, ptrVelocity2, true);
			}
			IntPtr ptrAccel = IntPtr.Zero;
			if (dAccel != null)
			{
				ptrAccel = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dAccel.Value, ptrAccel, true);
			}
			IntPtr ptrDecel = IntPtr.Zero;
			if (dDecel != null)
			{
				ptrDecel = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dDecel.Value, ptrDecel, true);
			}
			return _MultipleAbsMoveContinuous(nNodeID, nDistance1, nDistance2, ptrVelocity1, ptrVelocity2, ptrAccel, ptrDecel, bImmediateChange);
		}

		public bool ExecuteNormalQProgram(byte nNodeID, byte nSegment)
		{
			return _ExecuteNormalQProgram(nNodeID, nSegment);
		}

		public bool ExecuteSyncQProgram(byte nNodeID, byte nSegment, uint nSyncPulse)
		{
			return _ExecuteSyncQProgram(nNodeID, nSegment, nSyncPulse);
		}

		public bool Homing(byte nNodeID, int nHomingMethod, double? dHomingVelocity, double? dIndexVelocity, double? dHomingAccel, int? nHomingOffset, int? nHomingSwitch)
		{
			IntPtr ptrHomingVelocity = IntPtr.Zero;
			if (dHomingVelocity != null)
			{
				ptrHomingVelocity = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dHomingVelocity.Value, ptrHomingVelocity, true);
			}
			IntPtr ptrIndexVelocity = IntPtr.Zero;
			if (dIndexVelocity != null)
			{
				ptrIndexVelocity = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dIndexVelocity.Value, ptrIndexVelocity, true);
			}
			IntPtr ptrHomingAccel = IntPtr.Zero;
			if (dHomingAccel != null)
			{
				ptrHomingAccel = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dHomingAccel.Value, ptrHomingAccel, true);
			}
			IntPtr ptrHomingOffset = IntPtr.Zero;
			if (nHomingOffset != null)
			{
				ptrHomingOffset = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)));
				Marshal.StructureToPtr(nHomingOffset.Value, ptrHomingOffset, true);
			}
			IntPtr ptrHomingSwitch = IntPtr.Zero;
			if (nHomingSwitch != null)
			{
				ptrHomingSwitch = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)));
				Marshal.StructureToPtr(nHomingSwitch.Value, ptrHomingSwitch, true);
			}
			return _Homing(nNodeID, nHomingMethod, ptrHomingVelocity, ptrIndexVelocity, ptrHomingAccel, ptrHomingOffset, ptrHomingSwitch);
		}

		public bool LaunchVelocityMode(byte nNodeID, double? dVelocity, double? dAccel, double? dDecel)
		{
			IntPtr ptrVelocity = IntPtr.Zero;
			if (dVelocity != null)
			{
				ptrVelocity = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dVelocity.Value, ptrVelocity, true);
			}
			IntPtr ptrAccel = IntPtr.Zero;
			if (dAccel != null)
			{
				ptrAccel = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dAccel.Value, ptrAccel, true);
			}
			IntPtr ptrDecel = IntPtr.Zero;
			if (dDecel != null)
			{
				ptrDecel = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(double)));
				Marshal.StructureToPtr(dDecel.Value, ptrDecel, true);
			}
			return _LaunchVelocityMode(nNodeID, ptrVelocity, ptrAccel, ptrDecel);
		}

		#endregion
	}
}

